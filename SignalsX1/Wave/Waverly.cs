/* ----------------------------------------------------------------------------
LibTransWave : a library for playing, editing and storing audio wave data
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

using Transonic.Wave.System;

namespace Transonic.Wave
{
    public class Waverly
    {
        //communication with wave.dll
        [DllImport("Waverly.DLL", CallingConvention = CallingConvention.Cdecl)]
        public static extern void WaverlyInit();

        [DllImport("Waverly.DLL", CallingConvention = CallingConvention.Cdecl)]
        public static extern void WaverlyShutDown();

//transport calls -----------------------------------------------------

        [DllImport("Waverly.DLL", CallingConvention = CallingConvention.Cdecl)]
        public static extern void TransportPlay();

        [DllImport("Waverly.DLL", CallingConvention = CallingConvention.Cdecl)]
        public static extern void TransportStop();

        [DllImport("Waverly.DLL", CallingConvention = CallingConvention.Cdecl)]
        public static extern void TransportPause();

        [DllImport("Waverly.DLL", CallingConvention = CallingConvention.Cdecl)]
        public static extern void TransportRewind();

        [DllImport("Waverly.DLL", CallingConvention = CallingConvention.Cdecl)]
        public static extern void TransportFastForward(int speed);

        [DllImport("Waverly.DLL", CallingConvention = CallingConvention.Cdecl)]
        public static extern void TransportRecord();

        [DllImport("Waverly.DLL", CallingConvention = CallingConvention.Cdecl)]
        public static extern void TransportSetVolume(float volume);

        [DllImport("Waverly.DLL", CallingConvention = CallingConvention.Cdecl)]
        public static extern void TransportSetBalance(float balance);

        [DllImport("Waverly.DLL", CallingConvention = CallingConvention.Cdecl)]
        public static extern int TransportGetCurrentPos();

        [DllImport("Waverly.DLL", CallingConvention = CallingConvention.Cdecl)]
        public static extern int TransportSetCurrentPos(int pos);

//audio data calls -------------------------------------------------------

        [DllImport("Waverly.DLL", CallingConvention = CallingConvention.Cdecl)]
        public static extern void AudioNew(int SampleRate, int duration);

        [DllImport("Waverly.DLL", CallingConvention = CallingConvention.Cdecl)]
        public static extern void AudioOpen(string filename);

        [DllImport("Waverly.DLL", CallingConvention = CallingConvention.Cdecl)]
        public static extern void AudioClose();

        [DllImport("Waverly.DLL", CallingConvention = CallingConvention.Cdecl)]
        public static extern void AudioSave(string filename);

        [DllImport("Waverly.DLL", CallingConvention = CallingConvention.Cdecl)]
        public static extern int AudioImportWavFile(string filename);

        [DllImport("Waverly.DLL", CallingConvention = CallingConvention.Cdecl)]
        public static extern int AudioGetSampleRate();

        [DllImport("Waverly.DLL", CallingConvention = CallingConvention.Cdecl)]
        public static extern int AudioGetDataSize();

        [DllImport("Waverly.DLL", CallingConvention = CallingConvention.Cdecl)]
        public static extern int AudioGetDuration();

        [DllImport("Waverly.DLL", CallingConvention = CallingConvention.Cdecl)]
        public static extern float AudioGetLeftLevel();

        [DllImport("Waverly.DLL", CallingConvention = CallingConvention.Cdecl)]
        public static extern float AudioGetRightLevel();

        [DllImport("Waverly.DLL", CallingConvention = CallingConvention.Cdecl)]
        public static extern void ChannelSetWaveIn(int trackNum, int deviceIdx);

        [DllImport("Waverly.DLL", CallingConvention = CallingConvention.Cdecl)]
        public static extern void ChannelSetWaveOut(int deviceIdx);

//recorder calls --------------------------------------------------------------

        [DllImport("Waverly.DLL", CallingConvention = CallingConvention.Cdecl)]
        public static extern void ProjectAddTrack(int trackNum);

        [DllImport("Waverly.DLL", CallingConvention = CallingConvention.Cdecl)]
        public static extern void ProjectDeleteTrack(int trackNum);

        [DllImport("Waverly.DLL", CallingConvention = CallingConvention.Cdecl)]
        public static extern void ProjectLoadTrackData(int trackNum, IntPtr inhdl);

        [DllImport("Waverly.DLL", CallingConvention = CallingConvention.Cdecl)]
        public static extern void TrackSetVolume(int trackNum, float volume);

        [DllImport("Waverly.DLL", CallingConvention = CallingConvention.Cdecl)]
        public static extern void TrackSetPan(int trackNum, float pan);

        [DllImport("Waverly.DLL", CallingConvention = CallingConvention.Cdecl)]
        public static extern void TrackSetMute(int trackNum, bool mute);

        [DllImport("Waverly.DLL", CallingConvention = CallingConvention.Cdecl)]
        public static extern void TrackSetRecord(int trackNum, bool record);

        [DllImport("Waverly.DLL", CallingConvention = CallingConvention.Cdecl)]
        public static extern void TrackPaintData(int trackNum, IntPtr hdc, int width, int startpos);

        [DllImport("Waverly.DLL", CallingConvention = CallingConvention.Cdecl)]
        public static extern void TrackSaveData(int trackNum, IntPtr outhdl);

        [DllImport("Waverly.DLL", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ProjectExportWavFile(string filename);


//- signals methods -----------------------------------------------------------

        IWaveView waveWindow;        
        public WaveDevices waveDevices;

        public Waverly(IWaveView _mw)
        {
            waveWindow = _mw;
            waveDevices = new WaveDevices(this);
            WaverlyInit();
        }

        public void shutDown() 
        {
            WaverlyShutDown();
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
            //TranportSetLeftOutputLevel(level);
        }

        public void setRightOutputLevel(float level)
        {
            //TranportSetRightOutputLevel(level);
        }

        public void setVolume(float volume)
        {
            TransportSetVolume(volume);
        }

        public void setBalance(float balance)
        {
            TransportSetBalance(balance);
        }

        public int getCurrentPos()
        {
            return TransportGetCurrentPos();    //in samples
        }

        public void setCurrentPos(int pos)
        {
            TransportSetCurrentPos(pos);		//in samples
        }

        public void setWaveOutDevice(int devIdx)
        {
            ChannelSetWaveOut(devIdx);
        }
        
//- audio data methods --------------------------------------------------------

        public void newProject(int sampleRate, int duration)
        {
            AudioNew(sampleRate, duration);
        }

        public void openAudioFile(String filename)
        {
            AudioOpen(filename);
        }

        public void closeAudioFile()
        {
            AudioClose();
        }

        public void closeProject()
        {
            //AudioClose();
        }

        public int loadWaveFile(string filename)
        {
            return AudioImportWavFile(filename);
        }

        public int getDataSize()
        {
            return AudioGetDataSize();
        }

        public int getAudioSampleRate()
        {
            return AudioGetSampleRate();
        }

        public int getAudioDuration()
        {
            return AudioGetDuration();
        }

        public List<String> getInputDeviceList()
        {
            return waveDevices.getInDevNameList();
        }

        public List<String> getOutputDeviceList()
        {
            return waveDevices.getOutDevNameList();
        }

        public float getAudioLeftLevel()
        {
            return AudioGetLeftLevel();
        }

        public float getAudioRightLevel()
        {
            return AudioGetRightLevel();
        }

//- track methods -----------------------------------------------------------

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

        public void setTrackWaveIn(int trackNum, int deviceIdx)
        {
            ChannelSetWaveIn(trackNum, deviceIdx);
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

        public bool exportToWaveFile(string filename)
        {
            return ProjectExportWavFile(filename);
        }
    }
}

//  Console.WriteLine(" there's no sun in the shadow of the wizard");
