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

#if !defined(TRANSPORT_H)
#define TRANSPORT_H

#include <windows.h>                    

class Project;
class Track;
class WaveInDevice;
class WaveOutDevice;

class Transport
{
public:
	Transport();
	virtual ~Transport();

	void setProject(Project* _project) {project = _project; }
	void setWaveIn (WaveInDevice* _waveIn) { waveIn = _waveIn; }
	void setWaveOut (WaveOutDevice* _waveOut) { waveOut = _waveOut; }
	void setBlockSize (int _size) { blockSize = _size; }

	void play();
	void pause();
	void stop();
	void rewind();
	void fastForward(int speed);
	void record();

	BOOL isCurRunning() { return isRunning; }
	BOOL isCurPlaying() { return isPlaying; }
	BOOL isCurRecording() { return isRecording; }
	
	int getCurrentPos() { return playbackPos; }
	void setCurrentPos(int pos);
	void setLeftOutLevel(float level) { leftOutLevel = level; }
	void setRightOutLevel(float level) { rightOutLevel = level; }

	//input
	void audioIn(float** pBuffers, int dataSize, int channels, DWORD timestamp, Track* track);

protected:
	Project* project;
	WaveInDevice * waveIn;
	WaveOutDevice * waveOut;

	int sampleRate;
    long blockSize;
	float * outputBuf[2];

	UINT timerID;
	TIMECAPS tc;

	BOOL isRunning;
	BOOL isPaused;
	BOOL isPlaying;
	BOOL isRecording;

	float* dataBuf;
	int dataSize;
	int recordPos;
	int playbackPos;
	float leftOutLevel;
	float rightOutLevel;
	int playSpeed;
	CRITICAL_SECTION cs;

	//management
	void startUp();
	void shutDown();

	//timing
	BOOL startTimer(UINT msSec=0);
	void stopTimer();
	static void CALLBACK timerCallback(UINT uID, UINT uMsg, DWORD dwUser, DWORD dw1, DWORD dw2);

	//output
    void audioOut();
};

#endif // TRANSPORT_H
