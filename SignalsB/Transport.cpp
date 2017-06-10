/* ----------------------------------------------------------------------------
Signals X-1 : a digital audio multitrack recorder
Copyright (C) 2005-2017  George E Greaney

This program is free software; you can redistribute it and/or
modify it under the terms of the GNU General Public License
as published by the Free Software Foundation; either version 2
of the License, or (at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program; if not, write to the Free Software
Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.
----------------------------------------------------------------------------*/

#include <windows.h>
#include <stdio.h>
#include <math.h>

#include "Project.h"
#include "Track.h"
#include "Transport.h"
#include "WaveInDevice.h"
#include "WaveOutDevice.h"

//cons
Transport::Transport()
{
	project = NULL;
	waveIn = NULL;
	waveOut = NULL;

	//default vals
	sampleRate = 44100;
	blockSize = sampleRate / 10;		//default duration = 100ms

	isRunning = FALSE;
	isPlaying = FALSE;
	isRecording = FALSE;
	isPaused = FALSE;

	dataSize = 0;
	recordPos = 0;
	playbackPos = 0;
	leftOutLevel = 1.0f;
	rightOutLevel = 1.0f;
	playSpeed = 1;

	timerID = 0;                              
	timeGetDevCaps(&tc, sizeof(tc));       

	for (int i = 0; i < 2; i++)
		outputBuf[i] = new float[sampleRate/2];			//0.5 sec

	InitializeCriticalSection(&cs);
}

//destuct
Transport::~Transport()
{
	for (int i = 0; i < 2; i++)
		delete[] outputBuf[i];
}

//- transport control ---------------------------------------------------------

void Transport::play() {

	isPlaying = TRUE;
	startUp();
	if (isRecording) {
		waveIn->stop();
	}
	isRecording = FALSE;
}

void Transport::record(){

	isPlaying = TRUE;
	isRecording = TRUE;
	startUp();
	waveIn->start();
}

void Transport::pause(){

	isPaused = !isPaused;
}

void Transport::stop(){

	shutDown();
	isPlaying = FALSE;
	isRecording = FALSE;
	isPaused = FALSE;
	playbackPos = 0;
	recordPos = 0;
}

void Transport::rewind(){
}

//int speedFactor[4] = {1, 2, 4, 8};

void Transport::fastForward(int speed){

	//playSpeed = speedFactor[speed];
}

//- transport management ---------------------------------------------------------

void Transport::startUp()
{
	if (isRunning)
		return;

	sampleRate = project->sampleRate;
	dataSize = project->dataSize;	

//start output device and timer to send track data to it
	waveOut->start();		
	int timerDuration = (blockSize * 1000) / sampleRate;
	startTimer(timerDuration);

	isRunning = TRUE;
}

void Transport::shutDown()
{
	if (!isRunning)
		return;

	//stop input device if in recording mode
	if (isRecording) {
		waveIn->stop();                        
	//waveIn->reset();
	}

	//stop output device
	stopTimer(); 
	waveOut->stop();
	isRunning = FALSE;
}

void Transport::setCurrentPos(int pos) 
{ 
 	playbackPos = pos; 
	recordPos = pos;
}

//- timer methods -------------------------------------------------------------

BOOL Transport::startTimer(UINT msSec)
{
	if (msSec < tc.wPeriodMin)          
		msSec = tc.wPeriodMin;

	int resolution = msSec / 10;
	timeBeginPeriod(resolution);        

	timerID = timeSetEvent(msSec, (resolution > 1) ? resolution / 2 : 1, timerCallback, (DWORD)this, TIME_PERIODIC);

	return (timerID != NULL);
}

void Transport::stopTimer() 
{
	if (timerID != NULL)
	{
		timeKillEvent(timerID);
		timerID = 0;
		timeEndPeriod(tc.wPeriodMin);
	}
}

void CALLBACK Transport::timerCallback(UINT uTimerID, UINT uMsg, DWORD dwUser, DWORD dw1, DWORD dw2)
{
	if (dwUser)                             
		((Transport *)dwUser)->audioOut();
}

//- processing methods --------------------------------------------------------

//record in mono for now, ignoring channel num
void Transport::audioIn(float** pBuffers, int size, int channels, DWORD timestamp, Track* track) 
{
	if (isRecording && !isPaused) {
		if (track->getRecording()) {

			int trackPos = recordPos;
			float* dataBuf = track->dataBuf;
			float* inBuf = pBuffers[0];
			for (int samp = 0; samp < size; samp++) {
				dataBuf[trackPos++] = inBuf[samp];
				if (trackPos > dataSize)
					trackPos = 0;
			}
		}
		recordPos += size;
		if (recordPos > dataSize)
			recordPos = 0;
	}
}

void Transport::audioOut()
{	
	float* leftOut = outputBuf[0];
	float* rightOut = outputBuf[1];

//zero out left & right output bufs - if not playing, this is all we need
	for (int samp = 0; samp < blockSize; samp++) {
		leftOut[samp] = 0.0f;
		rightOut[samp] = 0.0f;
	}

	if (isPlaying && !isPaused) {

		//sum audio data from each track into left & right output buffers, based on vol & pan settings
		for (int i = 0; i < project->trackLimit; i++) {

			if (project->tracks[i] != NULL && !project->tracks[i]->getMute() && !project->tracks[i]->getRecording()) {

				int trackDataPos = playbackPos;
				float* dataBuf = project->tracks[i]->dataBuf;
				float trackVol = project->tracks[i]->getVolume();
				float leftPan = project->tracks[i]->getLeftPan();
				float rightPan = project->tracks[i]->getRightPan();

				for (int samp = 0; samp < blockSize; samp++) {

					leftOut[samp] += (dataBuf[trackDataPos] * trackVol * leftPan * leftOutLevel);
					rightOut[samp] += (dataBuf[trackDataPos] * trackVol * rightPan * rightOutLevel);
					trackDataPos++;
					if (trackDataPos > dataSize)
						trackDataPos = 0;
				}
			}
		}

		//update playback pos, wrap pos if at end of track buf
		playbackPos += blockSize;
		if (playbackPos > dataSize)
			playbackPos = 0;

		//scaling outputs - get max vales for both channels for SignalsA level meter display
		//use hard clipping to keep output values between -1.0 and 1.0, maybe use an actual clipping algorithm?
		float leftMax = 0.0f;
		float rightMax = 0.0f;
		float fMult = (float)sqrt(1.0f / (project->trackCount));
		for (int samp = 0; samp < blockSize; samp++) {

			leftOut[samp] *= fMult;
			if (leftOut[samp] > leftMax) leftMax = leftOut[samp];		
			if (leftOut[samp] > 1.0f) leftOut[samp] = 1.0f;				//HARD clip - not pleasent sounding

			rightOut[samp] *= fMult;
			if (rightOut[samp] > rightMax) rightMax = rightOut[samp];
			if (rightOut[samp] > 1.0f) rightOut[samp] = 1.0f;
		}
		project->leftLevel = leftMax;			//for level meter
		project->rightLevel = rightMax;
	}

	// send output buffers to the Wave Output device
	waveOut->writeOut(outputBuf, blockSize);	
}

//printf("there's no sun in the shadow of the wizard.\n");