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
using Signals.IO;
using Signals.UI;

namespace Signals.Project
{
    public class X1Track
    {
        public X1Project project;
        public TrackView trackView;
        public MixerStrip mixerStrip;

        public String name;
        public int number;
        public float level;
        public float pan;
        public bool mute;
        public bool record;
        public int inputdevNum;

        public X1Track(X1Project _proj, int num)
        {
            project = _proj;
            trackView = null;
            mixerStrip = null;              //trackview + mixerstrip are created after the track and set these fields then

            name = "Track " + (num + 1);
            number = num;
            level = 1.0f;
            pan = 0.5f;
            mute = false;
            record = false;
            inputdevNum = -1;
        }

        public void close()        
        {
            project.deleteTrack(this);      //remove trackview, mixerstrip, backend track & this from project's track list
        }

//- track attributes ----------------------------------------------------------

        public void setTrackName(String _name)
        {
            name = _name;
            trackView.setTrackName(name);
        }

        public void setLevel(float _level)
        {
            level = _level;
            project.waverly.setChannelVolume(number, level);
        }

        public void setPan(float _pan)
        {
            pan = _pan;
            project.waverly.setChannelPan(number, pan);
        }

        public void setMuted(bool on)
        {
            mute = on;
            trackView.setMuteIndicator(on);
            project.waverly.setChannelMute(number, mute);
        }

        public void setSolo(bool on)
        {
            trackView.setSoloIndicator(on);
        }

        public void setRecording(bool on)
        {
            record = on;
            trackView.setRecordingIndicator(on);
            project.waverly.setChannelRecord(number, record);
        }

        public void setInputDevice(int _inputdevNum)
        {
            inputdevNum = _inputdevNum;
            project.waverly.setChannelWaveIn(number, inputdevNum);
        }

        public List<String> getInputDeviceList()
        {
            return project.getInputDeviceList();
        }
        
//- track i/o -----------------------------------------------------------------

        public List<byte> saveTrackSettings() {

            List<byte> trackBytes = new List<byte>();

            trackBytes.AddRange(BitConverter.GetBytes(name.Length));                        
            trackBytes.AddRange(Encoding.ASCII.GetBytes(name));
            trackBytes.AddRange(BitConverter.GetBytes(number));
            trackBytes.AddRange(BitConverter.GetBytes(level));
            trackBytes.AddRange(BitConverter.GetBytes(pan));
            return trackBytes;            
        }

        public void saveTrackData(IntPtr outHdl)
        {
            Console.WriteLine("saving track data");
            
            project.waverly.saveChannelData(number, outHdl);
        }

        static public int loadTrack(X1Project _project, byte[] trackdata, int dataPos, IntPtr inHdl)
        {
            int trackNameLen = BitConverter.ToInt32(trackdata, dataPos);
            String _trackName = Encoding.ASCII.GetString(trackdata, dataPos + 4, trackNameLen);
            dataPos = dataPos + 4 + trackNameLen;
            int _number = BitConverter.ToInt32(trackdata, dataPos);
            float _level = BitConverter.ToSingle(trackdata, dataPos + 4);
            float _pan = BitConverter.ToSingle(trackdata, dataPos + 8);

            X1Track track = new X1Track(_project, _number);
            _project.addTrack(track);
            _project.waverly.loadChannelData(_number, inHdl);

            track.setTrackName(_trackName);
            track.setLevel(_level);
            track.setPan(_pan);

            return dataPos + 12;
        }
    }
}

//  Console.WriteLine(" there's no sun in the shadow of the wizard");
