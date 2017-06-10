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

#include "Signals.h"
#include "Transport.h"
#include "WaveInDevice.h"
#include "WaveOutDevice.h"
#include "Project.h"
#include "Track.h"

#include <conio.h>

#define WAVEBUFCOUNT  10
#define WAVEBUFDURATION   100		//buf duration in ms

Signals* Signals::SignalsB;

//- SignalsA iface exports ----------------------------------------------------

extern "C" __declspec(dllexport) void SignalsInit() {

	Signals::SignalsB = new Signals();
}

extern "C" __declspec(dllexport) void SignalsShutDown() {

	delete Signals::SignalsB;
}

//- transport exports ---------------------------------------------------------

extern "C" __declspec(dllexport) void TransportPlay() {

	Signals::SignalsB->transport->play();	
}

extern "C" __declspec(dllexport) void TransportRecord() {

	Signals::SignalsB->transport->record();
}

extern "C" __declspec(dllexport) void TransportPause() {

	Signals::SignalsB->transport->pause();
}

extern "C" __declspec(dllexport) void TransportStop() {

	Signals::SignalsB->transport->stop();
}

extern "C" __declspec(dllexport) void TransportFastForward(int speed) {

	Signals::SignalsB->transport->fastForward(speed);
}

//not implemented yet
extern "C" __declspec(dllexport) void TransportSetWaveOut(int deviceIdx) {
}

extern "C" __declspec(dllexport) int TransportGetCurrentPos() {

	return Signals::SignalsB->transport->getCurrentPos();
}

extern "C" __declspec(dllexport) void TransportSetCurrentPos(int curPos) {

	Signals::SignalsB->transport->setCurrentPos(curPos);
}

extern "C" __declspec(dllexport) void TranportSetLeftOutputLevel(float level) {

	Signals::SignalsB->transport->setLeftOutLevel(level);
}

extern "C" __declspec(dllexport) void TranportSetRightOutputLevel(float level) {

	Signals::SignalsB->transport->setRightOutLevel(level);
}

//- project exports -----------------------------------------------------------

extern "C" __declspec(dllexport) void ProjectNew(int sampleRate, int duration) {

	Signals::SignalsB->newProject(sampleRate, duration);
}

extern "C" __declspec(dllexport) void ProjectOpen(char* filename) {

	Signals::SignalsB->openProject(filename);
}

extern "C" __declspec(dllexport) void ProjectClose() {

	Signals::SignalsB->closeCurrentProject();
}

extern "C" __declspec(dllexport) void ProjectSave(char* filename) {

	Signals::SignalsB->saveCurrentProject(filename);
}

extern "C" __declspec(dllexport) int ProjectAddTrack(int trackNum) {

	Track* track = Signals::SignalsB->currentProject->addTrack(trackNum);
	return track->trackNum;
}

extern "C" __declspec(dllexport) void ProjectDeleteTrack(int trackNum) {

	Signals::SignalsB->currentProject->deleteTrack(trackNum);
}

extern "C" __declspec(dllexport) int ProjectCopyTrack(int trackNum) {

	Track* track = Signals::SignalsB->currentProject->copyTrack(trackNum);
	return track->trackNum;
}

extern "C" __declspec(dllexport) void ProjectLoadTrackData(int trackNum, void* inhdl) {

	Signals::SignalsB->currentProject->loadTrackData(trackNum, inhdl);
}

extern "C" __declspec(dllexport) int ProjectImportWavFile(char* filename) {

	return Signals::SignalsB->currentProject->importTracksFromWavFile(filename);	
}

extern "C" __declspec(dllexport) BOOL ProjectExportWavFile(char* filename) {

	return Signals::SignalsB->currentProject->exportTracksToWavFile(filename);
}

extern "C" __declspec(dllexport) int ProjectGetDataSize() {

	return Signals::SignalsB->currentProject->dataSize;
}

extern "C" __declspec(dllexport) float ProjectGetLeftLevel() {

	return Signals::SignalsB->currentProject->getLeftLevel();
}

extern "C" __declspec(dllexport) float ProjectGetRightLevel() {

	return Signals::SignalsB->currentProject->getRightLevel();
}

//- track exports -------------------------------------------------------------

extern "C" __declspec(dllexport) void TrackSetVolume(int trackNum, float volume) {

	Signals::SignalsB->currentProject->getTrack(trackNum)->setVolume(volume);
}    

extern "C" __declspec(dllexport) void TrackSetPan(int trackNum, float pan) {

	Signals::SignalsB->currentProject->getTrack(trackNum)->setPan(pan);
}        

extern "C" __declspec(dllexport) void TrackSetMute(int trackNum, bool mute) {

	Signals::SignalsB->currentProject->getTrack(trackNum)->setMute(mute);
}        

extern "C" __declspec(dllexport) void TrackSetRecord(int trackNum, bool record) {

	Track* track = Signals::SignalsB->currentProject->getTrack(trackNum);
	track->setRecording(record);
	if (record) Signals::SignalsB->waveIn->recTrack = track;
}

//not implemented yet
extern "C" __declspec(dllexport) void TrackSetWaveIn(int trackNum, int deviceIdx) {
}

extern "C" __declspec(dllexport) void TrackPaintData(int trackNum, void* hdc, int width, int startpos) {

	Signals::SignalsB->currentProject->getTrack(trackNum)->paintTrackData(hdc, width, startpos);
}

extern "C" __declspec(dllexport) void TrackSaveData(int trackNum, void* outhdl) {

	Signals::SignalsB->currentProject->getTrack(trackNum)->saveTrackData(outhdl);
}

//-----------------------------------------------------------------------------
//-----------------------------------------------------------------------------

//cons
Signals::Signals()
{
	transport = new Transport();
	
	//use default in & out for now
	loadWaveInDevice(WAVE_MAPPER);		// open input devices 
	loadWaveOutDevice(WAVE_MAPPER);		// open output device

	currentProject = NULL;
}

//shut down
Signals::~Signals()
{
	closeCurrentProject();
	waveIn->close();
	waveOut->close();	
	delete transport;
}

//- project methods ------------------------------------------------------------

void Signals::newProject(int sampleRate, int duration) 
{
	int datasize = sampleRate * duration;
	currentProject = new Project(this, sampleRate, datasize, duration);
}

void Signals::openProject(char* filename) 
{
	currentProject = new Project(this, filename);
}

void Signals::closeCurrentProject() {

	if (currentProject != NULL) {
		delete currentProject;
	}
	currentProject = NULL;
}

void Signals::saveCurrentProject(char* filename) {

	currentProject->save(filename);
}

//- device methods ------------------------------------------------------------

BOOL Signals::loadWaveInDevice(int devID, int nBufSz)
{
	BOOL result = FALSE;

	waveIn = new WaveInDevice();
	waveIn->setBufferCount(WAVEBUFCOUNT);
	waveIn->setBufferDuration(WAVEBUFDURATION);
	result = waveIn->open(devID, 44100, 16, 1);		//mono in

	transport->setWaveIn(waveIn);
	waveIn->transport = transport;

	return result;
}

BOOL Signals::loadWaveOutDevice	(int devID)
{
	BOOL result = FALSE;

	waveOut = new WaveOutDevice();
	waveOut->setBufferCount(WAVEBUFCOUNT);
	waveOut->setBufferDuration(WAVEBUFDURATION);
	result = waveOut->open(devID, 44100, 16, 2);		//stereo out

	transport->setWaveOut(waveOut);
	transport->setBlockSize(4410);

	return result;
}

	//printf("there's no sun in the shadow of the wizard.\n");
