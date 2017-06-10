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

#if !defined(SIGNALS_H)
#define SIGNALS_H

#include <windows.h>
#include <mmsystem.h>
#include <stdio.h>

class Transport;
class WaveInDevice;
class WaveOutDevice;
class Project;

class Signals
{
public:
	Signals();
	~Signals();

	static Signals* SignalsB;		//for SignalsA communication

	Transport* transport;
	Project* currentProject;

	void newProject(int sampleRate, int duration);
	void openProject(char* filename);
	void closeCurrentProject();
	void saveCurrentProject(char* filename);

	WaveInDevice* waveIn;
	WaveOutDevice* waveOut;

protected:

	BOOL loadWaveInDevice(int devID, int nBufSz = 4410);
	BOOL loadWaveOutDevice(int devID);
};

#endif // SIGNALS_H
