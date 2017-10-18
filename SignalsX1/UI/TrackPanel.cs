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
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using Signals.Project;
using Signals.Widgets;

namespace Signals.UI
{
    public class TrackPanel : UserControl
    {
        static public int ZOOMFACTOR = 5;       //5 pixels per sec

        public SignalsWindow signalsWindow;
        public X1Project project;

        public TrackRuler ruler;
        public List<TrackView> tracks;
        private VScrollBar vsbTracks;
        private HScrollBar hsbTracks;

        public int zoomfactor;     //pixels per mSec
        public int tracksHeight;   //height of all the tracks in pixels
        public int trackWidth;     //width of data portion of track view for horizontal scrolling in pixels

        public TrackPanel(SignalsWindow _signalsWindow)
        {
            signalsWindow = _signalsWindow;
            project = null;
            InitializeComponent();

            ruler = new TrackRuler(this);
            ruler.Width = this.Width;
            ruler.Dock = DockStyle.Top;
            Controls.Add(ruler);

            tracks = new List<TrackView>();
            zoomfactor = ZOOMFACTOR;
            tracksHeight = 10;          //bottom gutter
            trackWidth = 0;
        }

        private void InitializeComponent()
        {
            this.vsbTracks = new System.Windows.Forms.VScrollBar();
            this.hsbTracks = new System.Windows.Forms.HScrollBar();
            this.SuspendLayout();
            // 
            // vsbTracks
            // 
            this.vsbTracks.Dock = System.Windows.Forms.DockStyle.Right;
            this.vsbTracks.Location = new System.Drawing.Point(133, 0);
            this.vsbTracks.Name = "vsbTracks";
            this.vsbTracks.Size = new System.Drawing.Size(17, 150);
            this.vsbTracks.TabIndex = 0;
            this.vsbTracks.Visible = false;
            this.vsbTracks.Scroll += new System.Windows.Forms.ScrollEventHandler(this.vsbTracks_Scroll);
            // 
            // hsbTracks
            // 
            this.hsbTracks.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.hsbTracks.Location = new System.Drawing.Point(0, 133);
            this.hsbTracks.Name = "hsbTracks";
            this.hsbTracks.Size = new System.Drawing.Size(133, 17);
            this.hsbTracks.TabIndex = 1;
            this.hsbTracks.Visible = false;
            this.hsbTracks.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hsbTracks_Scroll);
            // 
            // TrackPanel
            // 
            this.BackColor = System.Drawing.Color.SteelBlue;
            this.Controls.Add(this.hsbTracks);
            this.Controls.Add(this.vsbTracks);
            this.DoubleBuffered = true;
            this.Name = "TrackPanel";
            this.Resize += new System.EventHandler(this.TrackPanel_Resize);
            this.ResumeLayout(false);

        }

        public void setProject(X1Project _project)
        {
            project = _project;
            foreach (TrackView trackView in tracks)
            {
                trackView.updateTrackNumber();
                trackView.trackData.Invalidate();
            }
        }

        public void clearProject(X1Project _project)
        {
            //ruler
            project = null;
        }
        

//- track layout & scrolling --------------------------------------------------

        private void HorizontalAdjust(int adj)
        {
            ruler.startPos = adj;
            ruler.Invalidate();

            int samplePos = (adj * project.sampleRate / zoomfactor);
            foreach (TrackView track in tracks)
            {
                track.trackData.samplePos = samplePos;
                track.trackData.Invalidate();
            }
        }

        private void VerticalAdjust(int adj)
        {
            for (int i = 0; i < tracks.Count; i++)
            {
                tracks[i].Top = (i * TrackView.TRACKHEIGHT + ruler.Height) - adj;
                tracks[i].trackData.Invalidate();
            }
        }

        private void TrackPanel_Resize(object sender, EventArgs e)
        {            
            ruler.Width = this.Width;
            ruler.Invalidate();

            //horizontal
            int hdiff = TrackView.TRACKHEADERWIDTH + trackWidth - this.Width;
            if (hdiff >= 0)
            {
                hsbTracks.Visible = true;
                hsbTracks.Maximum = hdiff + hsbTracks.LargeChange - 1;
                if (hsbTracks.Value >= hsbTracks.Maximum)
                {
                    HorizontalAdjust(hdiff);
                }
            }
            else
            {
                hsbTracks.Visible = false;
                if (tracks.Count > 0)
                {
                    HorizontalAdjust(0);        //only if we have tracks in the panel
                }
            }

            //vertical
            int vdiff = (tracksHeight + ruler.Height + (hsbTracks.Visible ? hsbTracks.Height : 0)) - this.Height;
            if (vdiff >= 0)
            {
                vsbTracks.Visible = true;
                vsbTracks.Maximum = vdiff + vsbTracks.LargeChange - 1;
                if (vsbTracks.Value >= vsbTracks.Maximum)
                {
                    VerticalAdjust(vdiff);
                }
            }
            else
            {
                vsbTracks.Visible = false;
                VerticalAdjust(0);
            }
        }

        private void vsbTracks_Scroll(object sender, ScrollEventArgs e)
        {
            VerticalAdjust(vsbTracks.Value);
        }

        private void hsbTracks_Scroll(object sender, ScrollEventArgs e)
        {
            HorizontalAdjust(hsbTracks.Value);
        }

//- track management ----------------------------------------------------------

        public void addTrackView(X1Track track, int duration) 
        {
            trackWidth = duration * zoomfactor;
            TrackView trackView = new TrackView(this, track, trackWidth);
            trackView.Location = new Point(0, tracks.Count * TrackView.TRACKHEIGHT + ruler.Height);
            tracksHeight += TrackView.TRACKHEIGHT;
            this.Controls.Add(trackView);
            tracks.Add(trackView);            
            this.Height++;              //trigger a resize to draw scrollbars if needed
        }

        public void deleteTrackView(TrackView trackView)
        {
            tracks.Remove(trackView);
            this.Controls.Remove(trackView);
            trackView.close();
            if (tracks.Count == 0) trackWidth = 0;
            tracksHeight -= TrackView.TRACKHEIGHT;
            for (int i = 0; i < tracks.Count; i++) 
            {
                tracks[i].Top = (i * TrackView.TRACKHEIGHT + ruler.Height);
                    
            }
            this.Height++;
        }

        public void updateDuration(int duration)
        {
            trackWidth = duration * zoomfactor;
            foreach (TrackView trackView in tracks)
            {
                trackView.updateWidth(trackWidth);
                trackView.trackData.Invalidate();
            }
        }


//-----------------------------------------------------------------------------

        public void timerTick(int msTime)
        {
            int curpos = (int)(msTime * (zoomfactor / 1000.0f));
            ruler.playPos = curpos;
            ruler.Invalidate();

            //Rectangle invalidRect = new Rectangle(curpos - 5, 0, 10, 128);
            foreach (TrackView track in tracks)
            {
                track.trackData.Invalidate();
            }
        }

    }
}
