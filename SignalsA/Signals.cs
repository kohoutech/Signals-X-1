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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using Signals.IO;

namespace Signals
{
    public class Signals
    {
        //communication with signalsB
        [DllImport("SignalsB.DLL", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SignalsInit();

        [DllImport("SignalsB.DLL", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SignalsShutDown();

        //transport calls -----------------------------------------------------

        [DllImport("SignalsB.DLL", CallingConvention = CallingConvention.Cdecl)]
        public static extern void TransportPlay();

        [DllImport("SignalsB.DLL", CallingConvention = CallingConvention.Cdecl)]
        public static extern void TransportStop();

        [DllImport("SignalsB.DLL", CallingConvention = CallingConvention.Cdecl)]
        public static extern void TransportPause();

        [DllImport("SignalsB.DLL", CallingConvention = CallingConvention.Cdecl)]
        public static extern void TransportRewind();

        [DllImport("SignalsB.DLL", CallingConvention = CallingConvention.Cdecl)]
        public static extern void TransportFastForward(int speed);

        [DllImport("SignalsB.DLL", CallingConvention = CallingConvention.Cdecl)]
        public static extern void TransportRecord();

        [DllImport("SignalsB.DLL", CallingConvention = CallingConvention.Cdecl)]
        public static extern void TransportSetWaveOut(int deviceIdx);

        [DllImport("SignalsB.DLL", CallingConvention = CallingConvention.Cdecl)]
        public static extern void TranportSetLeftOutputLevel(float level);

        [DllImport("SignalsB.DLL", CallingConvention = CallingConvention.Cdecl)]
        public static extern void TranportSetRightOutputLevel(float level);

        [DllImport("SignalsB.DLL", CallingConvention = CallingConvention.Cdecl)]
        public static extern int TransportGetCurrentPos();

        [DllImport("SignalsB.DLL", CallingConvention = CallingConvention.Cdecl)]
        public static extern int TransportSetCurrentPos(int pos);

        //project calls -------------------------------------------------------

        [DllImport("SignalsB.DLL", CallingConvention = CallingConvention.Cdecl)]
        public static extern void ProjectNew(int SampleRate, int duration);

        [DllImport("SignalsB.DLL", CallingConvention = CallingConvention.Cdecl)]
        public static extern void ProjectOpen(string filename);

        [DllImport("SignalsB.DLL", CallingConvention = CallingConvention.Cdecl)]
        public static extern void ProjectClose();

        [DllImport("SignalsB.DLL", CallingConvention = CallingConvention.Cdecl)]
        public static extern void ProjectSave(string filename);

        [DllImport("SignalsB.DLL", CallingConvention = CallingConvention.Cdecl)]
        public static extern void ProjectAddTrack(int trackNum);        

        [DllImport("SignalsB.DLL", CallingConvention = CallingConvention.Cdecl)]
        public static extern void ProjectDeleteTrack(int trackNum);

        [DllImport("SignalsB.DLL", CallingConvention = CallingConvention.Cdecl)]
        public static extern void ProjectLoadTrackData(int trackNum, IntPtr inhdl);
        
        [DllImport("SignalsB.DLL", CallingConvention = CallingConvention.Cdecl)]
        public static extern int ProjectImportWavFile(string filename);

        [DllImport("SignalsB.DLL", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ProjectExportWavFile(string filename);

        [DllImport("SignalsB.DLL", CallingConvention = CallingConvention.Cdecl)]
        public static extern int ProjectGetDataSize();

        [DllImport("SignalsB.DLL", CallingConvention = CallingConvention.Cdecl)]
        public static extern float ProjectGetLeftLevel();

        [DllImport("SignalsB.DLL", CallingConvention = CallingConvention.Cdecl)]
        public static extern float ProjectGetRightLevel();

        //track calls ---------------------------------------------------------

        [DllImport("SignalsB.DLL", CallingConvention = CallingConvention.Cdecl)]
        public static extern void TrackSetWaveIn(int trackNum, int deviceIdx);

        [DllImport("SignalsB.DLL", CallingConvention = CallingConvention.Cdecl)]
        public static extern void TrackSetVolume(int trackNum, float volume);

        [DllImport("SignalsB.DLL", CallingConvention = CallingConvention.Cdecl)]
        public static extern void TrackSetPan(int trackNum, float pan);

        [DllImport("SignalsB.DLL", CallingConvention = CallingConvention.Cdecl)]
        public static extern void TrackSetMute(int trackNum, bool mute);

        [DllImport("SignalsB.DLL", CallingConvention = CallingConvention.Cdecl)]
        public static extern void TrackSetRecord(int trackNum, bool record);

        [DllImport("SignalsB.DLL", CallingConvention = CallingConvention.Cdecl)]
        public static extern void TrackPaintData(int trackNum, IntPtr hdc, int width, int startpos);

        [DllImport("SignalsB.DLL", CallingConvention = CallingConvention.Cdecl)]
        public static extern void TrackSaveData(int trackNum, IntPtr outhdl);        

//- signals methods -----------------------------------------------------------

        SignalsWindow signalsWindow;        
        public WaveDevices waveDevices;

        public Signals(SignalsWindow _sw)
        {
            signalsWindow = _sw;
            waveDevices = new WaveDevices(this);
            SignalsInit();
        }

        public void shutDown() 
        {
            //SignalsShutDown();
        }

//- transport methods ---------------------------------------------------------

        public void playTransport()
        {
            TransportPlay();
        }

        public void pauseTransport()
        {
            TransportPause();
        }

        public void stopTransport()
        {
            TransportStop();
        }

        public void rewindTransport()
        {
            TransportRewind();
        }

        public void fastForwardTransport(int speed)
        {
            //TransportFastForward(speed);      //doesn't work!
        }

        public void recordTransport()
        {
            TransportRecord();
        }

        public void setLeftOutputLevel(float level)
        {
            TranportSetLeftOutputLevel(level);
        }

        public void setRightOutputLevel(float level)
        {
            TranportSetRightOutputLevel(level);
        }

        public int getCurrentPos()
        {
            return TransportGetCurrentPos();
        }

        public void setCurrentPos(int pos)
        {
            TransportSetCurrentPos(pos);
        }

//- project methods -----------------------------------------------------------

        public void newProject(int sampleRate, int duration)
        {
            ProjectNew(sampleRate, duration);
        }

        public void closeProject()
        {
            ProjectClose();
        }

        public void addTrack(int trackNum)
        {
            ProjectAddTrack(trackNum);
        }

        public void deleteTrack(int trackNum)
        {
            ProjectDeleteTrack(trackNum);
        }

        public void loadTrackData(int trackNum, IntPtr inhdl)
        {
            ProjectLoadTrackData(trackNum, inhdl);
        }

        public int loadWaveFile(string filename)
        {
            return ProjectImportWavFile(filename);
        }

        public bool exportToWaveFile(string filename)
        {
            return ProjectExportWavFile(filename);
        }

        public int getDataSize()
        {
            return ProjectGetDataSize();
        }

        public List<String> getInputDeviceList()
        {
            return waveDevices.getInDevNameList();
        }

        public List<String> getOutputDeviceList()
        {
            return waveDevices.getOutDevNameList();
        }

        public float getLeftLevel()
        {
            return ProjectGetLeftLevel();
        }

        public float getRightLevel()
        {
            return ProjectGetRightLevel();
        }

//- track methods -----------------------------------------------------------

        public void setTrackWaveIn(int trackNum, int deviceIdx)
        {
            TrackSetWaveIn(trackNum, deviceIdx);
        }

        public void setTrackVolume(int trackNum, float volume)
        {
            TrackSetVolume(trackNum, volume);
        }

        public void setTrackPan(int trackNum, float volume)
        {
            TrackSetPan(trackNum, volume);
        }

        public void setTrackMute(int trackNum, bool mute)
        {
            TrackSetMute(trackNum, mute);
        }

        public void setTrackRecord(int trackNum, bool record)
        {
            TrackSetRecord(trackNum, record);
        }
        
        public void paintTrackData(int trackNum, IntPtr hdc, int width, int startpos)
        {
            TrackPaintData(trackNum, hdc, width, startpos);
        }

        public void saveTrackData(int trackNum, IntPtr outhdl)
        {
            TrackSaveData(trackNum, outhdl);
        }
    }
}

//  Console.WriteLine(" there's no sun in the shadow of the wizard");
