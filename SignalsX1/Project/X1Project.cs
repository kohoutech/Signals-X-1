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
using System.IO;

using Signals;
using Signals.IO;
using Signals.UI;

using Transonic.Wave;

namespace Signals.Project
{
    public class X1Project
    {
        //persistance
        public String fileSig = "SX-1";
        public int fileVersion = 0001;

        //obj graph
        public SignalsWindow signalsWindow;
        public Waverly waverly;
        public MixerWindow mixerWindow;
        public MixerMaster mixerMaster;
        public TrackPanel trackPanel;

        //params
        public int sampleRate;
        public int dataSize;
        public int duration;        //in mSec
        public float leftOutLevel;
        public float rightOutLevel;

        public List<X1Track> tracks;

        public String projectName;
        public String filename;
        public bool isChanged;

//-----------------------------------------------------------------------------

        //cons
        public X1Project(SignalsWindow _signalsWindow, String _name, int _sampleRate, int _duration)
        {
            signalsWindow = _signalsWindow;
            waverly = signalsWindow.waverly;
            mixerWindow = signalsWindow.mixerWindow;
            mixerMaster = signalsWindow.mixerWindow.mixmaster;
            trackPanel = signalsWindow.trackPanel;

            projectName = _name;
            filename = null;

            sampleRate = _sampleRate;
            duration = _duration;
            dataSize = sampleRate * duration;

            leftOutLevel = 1.0f;
            rightOutLevel = 1.0f;

            tracks = new List<X1Track>();
            isChanged = true;                   //work out a better determinization of when the project's changes later

            waverly.newAudioProject(sampleRate, duration);    //create new project in back end
        }

//- track I/O -----------------------------------------------------------------

        static public X1Project open(SignalsWindow signalsWindow, String filename)
        {
            X1Project newProject = null;
            FileStream infile = File.Open(filename, FileMode.Open, FileAccess.Read);            
            byte[] inbuf = new byte[12];
            infile.Read(inbuf, 0, 12);
            String sig = Encoding.ASCII.GetString(inbuf, 0, 4);
            int version = BitConverter.ToInt32(inbuf, 4);
            int hdrSize = BitConverter.ToInt32(inbuf, 8);

            inbuf = new byte[hdrSize];
            infile.Read(inbuf, 0, hdrSize);
            int projectNameLen = BitConverter.ToInt32(inbuf, 0);
            String _projectName = Encoding.ASCII.GetString(inbuf, 4, projectNameLen);
            int hdrOfs = projectNameLen + 4;
            int _sampleRate = BitConverter.ToInt32(inbuf, hdrOfs);
            int _duration = BitConverter.ToInt32(inbuf, hdrOfs + 4);
            int dataSize = BitConverter.ToInt32(inbuf, hdrOfs + 8);
            float _leftOutLevel = BitConverter.ToSingle(inbuf, hdrOfs + 12);
            float _rightOutLevel = BitConverter.ToSingle(inbuf, hdrOfs + 16);
            int trackCount = BitConverter.ToInt32(inbuf, hdrOfs + 20);

            newProject = new X1Project(signalsWindow, _projectName, _sampleRate, _duration);
            newProject.setLeftOutLevel(_leftOutLevel);
            newProject.setRightOutLevel(_rightOutLevel);
            newProject.filename = filename;

            int trackDataPos = hdrOfs + 24;
            IntPtr inhdl = infile.SafeFileHandle.DangerousGetHandle();
            for (int tracknum = 0; tracknum < trackCount; tracknum++)
            {
                trackDataPos = X1Track.loadTrack(newProject, inbuf, trackDataPos, inhdl);   
            }

            return newProject;
        }

        public void close()
        {
            List<X1Track> temp = new List<X1Track>(tracks);
            foreach (X1Track track in temp)
            {
                deleteTrack(track);
            }
            waverly.closeAudioProject();
        }

        public void save()
        {
            List<byte> projectBytes = new List<byte>();
            projectBytes.AddRange(BitConverter.GetBytes(projectName.Length));            
            projectBytes.AddRange(Encoding.ASCII.GetBytes(projectName));
            projectBytes.AddRange(BitConverter.GetBytes(sampleRate));
            projectBytes.AddRange(BitConverter.GetBytes(duration));
            projectBytes.AddRange(BitConverter.GetBytes(dataSize));
            projectBytes.AddRange(BitConverter.GetBytes(leftOutLevel));
            projectBytes.AddRange(BitConverter.GetBytes(rightOutLevel));
            projectBytes.AddRange(BitConverter.GetBytes(tracks.Count));

            foreach (X1Track track in tracks)
            {
                List<byte> trackBytes = track.saveTrackSettings();
                projectBytes.AddRange(trackBytes);
            }

            List<byte> headerBytes = new List<byte>();
            headerBytes.AddRange(Encoding.ASCII.GetBytes(fileSig));
            headerBytes.AddRange(BitConverter.GetBytes(fileVersion));
            headerBytes.AddRange(BitConverter.GetBytes(projectBytes.Count));
            headerBytes.AddRange(projectBytes);

            byte[] outBytes = headerBytes.ToArray();
            Console.WriteLine("opening output file " + filename);

            FileStream outfile = File.Open(filename, FileMode.OpenOrCreate, FileAccess.Write);
            Console.WriteLine("output file opened" + filename);

            outfile.Write(outBytes, 0, outBytes.Length);
            Console.WriteLine("wrote header data ");
            IntPtr outhdl = outfile.SafeFileHandle.DangerousGetHandle();
            foreach (X1Track track in tracks)
            {
                track.saveTrackData(outhdl);
            }
            outfile.Close();
        }

//- signals i/o ---------------------------------------------------------------

        public List<String> getInputDeviceList()
        {
            return waverly.getInputDeviceList();
        }

        public void setLeftOutLevel(float level)
        {
            leftOutLevel = level;
            waverly.setTransportLeftLevel(level);
        }

        public void setRightOutLevel(float level)
        {
            rightOutLevel = level;
            waverly.setTransportRightLevel(level);
        }

//- track management ----------------------------------------------------------

        //create a new empty track
        public void newTrack()
        {
            //create backend track first
            //later we'll scan through the track list and find an open slot
            //for now, just stick at end of list
            int nextTrackNum = (tracks.Count > 0) ? tracks[tracks.Count - 1].number + 1 : 0;
            X1Track track = new X1Track(this, nextTrackNum);
            addTrack(track);
            waverly.addChannel(nextTrackNum);
        }

        //add existing track to project and UI windows
        public void addTrack(X1Track track)
        {
            tracks.Add(track);
            trackPanel.addTrackView(track, duration);
            mixerWindow.addMixerStrip(track);
        }

        public void deleteTrack(X1Track track)
        {
            tracks.Remove(track);
            trackPanel.deleteTrackView(track.trackView);
            mixerWindow.deleteMixerStrip(track.mixerStrip);
            waverly.deleteChannel(track.number);
        }

        public void importTracksFromFile(String filename)
        {
            int trackCount = waverly.importWaveFile(filename);
            dataSize = waverly.getAudioDataSize();
            int importDuration = (int)((dataSize + sampleRate - 1) / sampleRate);

            //need to re-adjust durations of existing tracks both in view & in back end
            if (importDuration > duration) {
                duration = importDuration;
                trackPanel.updateDuration(duration);
            }

            int trackNum = tracks.Count;
            for (int i = 0; i < trackCount; i++)
            {
                X1Track track = new X1Track(this, trackNum++);
                tracks.Add(track);
                trackPanel.addTrackView(track, duration);
                mixerWindow.addMixerStrip(track);
            }
        }
    }
}

//  Console.WriteLine("there's no sun in the shadow of the wizard");
