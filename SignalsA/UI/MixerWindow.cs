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
using Signals.Engine;

namespace Signals.UI
{
    public class MixerWindow : Form
    {
        //obj graph
        public SignalsWindow signalsWindow;
        public X1Project project;
        public MixerMaster mixmaster;
        public List<MixerStrip> mixerStrips;

        //controls
        HScrollBar stripScroll;

        int stripCount;
        int stripWidth;         //width of strip section of mixer
        int mixermaxWidth;

        public MixerWindow(SignalsWindow _signalsWindow)
        {
            signalsWindow = _signalsWindow;
            project = null;
            InitializeComponent();

            mixerStrips = new List<MixerStrip>();
            stripCount = 0;

            stripScroll = new HScrollBar();
            stripScroll.Location = new Point(0, MixerStrip.STRIPHEIGHT);
            stripScroll.Size = new Size(MixerStrip.STRIPWIDTH, 20);
            stripScroll.LargeChange = MixerStrip.STRIPWIDTH / 2;
            stripScroll.Scroll  += new System.Windows.Forms.ScrollEventHandler(this.stripScroll_Scroll);
            this.Controls.Add(stripScroll);

            mixmaster = new MixerMaster(this);
            mixmaster.Dock = DockStyle.Right;
            this.Controls.Add(mixmaster);
            mixmaster.BringToFront();

            //fix height & width for initial no-strip view
            this.ClientSize = new Size(mixmaster.Width, MixerStrip.STRIPHEIGHT + stripScroll.Height); 
            this.MinimumSize = new Size(this.Width, this.Height);
            this.MaximumSize = new Size(this.Width, this.Height);
            stripWidth = 0;
            mixermaxWidth = this.Width;
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MixerWindow));
            this.SuspendLayout();
            // 
            // MixerWindow
            // 
            this.ClientSize = new System.Drawing.Size(284, 431);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MixerWindow";
            this.ShowInTaskbar = false;
            this.Text = "Signals X-1 Mixer";
            this.Resize += new System.EventHandler(this.MixerWindow_Resize);
            this.ResumeLayout(false);

        }

//-----------------------------------------------------------------------------

        public void setProject(X1Project _project)
        {
            project = _project;
            mixmaster.project = project;
            mixmaster.updateSettings();
            foreach (MixerStrip strip in mixerStrips)
            {
                strip.updateSettings();
            }
        }

        public void clearProject(X1Project _project)
        {
            project = null;
            mixmaster.project = null;
            stop();
        }

        public void stop() {

            mixmaster.quiet();
        }

//-----------------------------------------------------------------------------

        private void horizontalAdjust(int adj)
        {
            for (int i = 0; i < stripCount; i++)
            {
                mixerStrips[i].Left = (MixerStrip.STRIPWIDTH * i) - adj;
            }
        }

        private void MixerWindow_Resize(object sender, EventArgs e)
        {
            int diff = (mixmaster.Width + stripWidth) - this.ClientSize.Width;      //amount mixmaster is covering mixer strips
            stripScroll.Width = stripWidth - diff;                                  //resize scroll bar
            stripScroll.Maximum = diff + stripScroll.LargeChange - 1;
            if ((stripCount > 1) && (diff < (-1 * mixerStrips[0].Left)))
            {
                horizontalAdjust(diff);
            }
        }

        private void stripScroll_Scroll(object sender, ScrollEventArgs e)
        {
            horizontalAdjust(stripScroll.Value);
            this.Invalidate();
        }

//-----------------------------------------------------------------------------

        public void addMixerStrip(X1Track track)
        {
            this.MaximumSize = new Size(Int32.MaxValue, this.Height);           //allow temp resizing

            MixerStrip strip = new MixerStrip(this, track);
            this.Controls.Add(strip);
            mixerStrips.Add(strip);
            stripCount++;

            int diff = (mixmaster.Width + stripWidth) - this.ClientSize.Width;      //amount mixmaster is covering mixer strips
            if (diff == 0)      //if mixer window is full width
            {
                strip.Location = new Point(stripWidth, 0);
                stripWidth = mixerStrips.Count * MixerStrip.STRIPWIDTH;
                stripScroll.Width = stripWidth;
                mixmaster.Location = new Point(stripWidth, 0);
                this.ClientSize = new Size(stripWidth + mixmaster.Width, MixerStrip.STRIPHEIGHT + stripScroll.Height);
            }
            else
            {
                stripWidth = mixerStrips.Count * MixerStrip.STRIPWIDTH;     //respect cur mixer window width
                int leftPos = mixmaster.Left - stripWidth;
                for (int i = 0; i < stripCount; i++)
                {
                    mixerStrips[i].Left = leftPos;
                    leftPos += MixerStrip.STRIPWIDTH;
                }
                diff += MixerStrip.STRIPWIDTH;              //the amount the mixmaster can cover is now one strip wider
                stripScroll.Maximum = diff + stripScroll.LargeChange - 1;
                stripScroll.Value = diff;
            }

            //set minimum width to width of one track strip so one will always be visible
            if (stripCount == 1) {
                this.MinimumSize = new Size(this.Width, this.Height);
            }
            mixermaxWidth += MixerStrip.STRIPWIDTH;
            this.MaximumSize = new Size(mixermaxWidth, this.Height);       //but don't allow mixer to be stretched past strips + master width
            Invalidate();
        }

        public void deleteMixerStrip(MixerStrip strip)
        {
            //int stripPos = mixerStrips.IndexOf(strip);
            mixerStrips.Remove(strip);
            this.Controls.Remove(strip);
            strip.close();
            stripCount--;

            this.MaximumSize = new Size(Int32.MaxValue, this.Height);           //allow temp resizing

            stripWidth = mixerStrips.Count * MixerStrip.STRIPWIDTH;                
            int diff = (mixmaster.Width + stripWidth) - this.ClientSize.Width;      //amount mixmaster is covering mixer strips
            if (diff <= 0)      //if mixer window is full width
            {
                stripScroll.Width = stripWidth;
                int leftPos = 0;
                for (int i = 0; i < stripCount; i++)
                {
                    mixerStrips[i].Left = leftPos;
                    leftPos += MixerStrip.STRIPWIDTH;
                }
                mixmaster.Location = new Point(stripWidth, 0);
                this.ClientSize = new Size(stripWidth + mixmaster.Width, MixerStrip.STRIPHEIGHT + stripScroll.Height);
            }
            else
            {
                int leftPos = 0;
                for (int i = 0; i < stripCount; i++)
                {
                    mixerStrips[i].Left = leftPos;
                    leftPos += MixerStrip.STRIPWIDTH;
                }
                stripScroll.Maximum = diff + stripScroll.LargeChange - 1;
                stripScroll.Value = 0;
            }

            //set minimum width to width of one track strip so one will always be visible
            if (stripCount == 1)
            {
                this.MinimumSize = new Size(this.Width, this.Height);
            }
            mixermaxWidth -= MixerStrip.STRIPWIDTH;
            this.MaximumSize = new Size(mixermaxWidth, this.Height);       //but don't allow mixer to be stretched past strips + master width
            Invalidate();
        }

//- global strip controls ----------------------------------------------------

        public void setSoloTrack(MixerStrip strip)
        {
            for (int i = 0; i < stripCount; i++)
            {
                if (mixerStrips[i] != strip)
                {
                    mixerStrips[i].setMuted(strip.isSoloing);
                    mixerStrips[i].setSoloing(false);
                }
            }
        }

        public void unsetSoloTrack(MixerStrip strip)
        {
            for (int i = 0; i < stripCount; i++)
            {
                if (mixerStrips[i] != strip)
                {
                    mixerStrips[i].setSoloing(false);
                }
            }
        }

        public void setRecordingTrack(MixerStrip strip)
        {
            int recNum = strip.trackNumber;
            for (int i = 0; i < stripCount; i++)
            {
                if (mixerStrips[i] != strip)
                {
                    mixerStrips[i].setRecording(false);
                    mixerStrips[i].setSoloing(false);
                    mixerStrips[i].btnMute.Enabled = true;
                    mixerStrips[i].btnSolo.Enabled = false;
                }
            }
        }

        public void unsetRecordingTrack(MixerStrip strip)
        {
            int recNum = strip.trackNumber;
            for (int i = 0; i < stripCount; i++)
            {
                if (mixerStrips[i] != strip)
                {
                    mixerStrips[i].btnSolo.Enabled = true;
                }
            }
        }

    }
}
