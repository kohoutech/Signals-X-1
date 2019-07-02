/* ----------------------------------------------------------------------------
Signals X-1 : a digital audio multitrack recorder
Copyright (C) 2005-2019  George E Greaney

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
using Signals.Project;

namespace Signals.UI
{
    public class MixerWindow : Form
    {
        //obj graph
        public SignalsWindow signalsWindow;
        public X1Project project;

        public MixerMaster mixmaster;
        public List<MixerStrip> mixerStrips;
        private Panel stripPanel;

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

            //mix master goes on right of mixer window
            mixmaster = new MixerMaster(this);
            mixmaster.Dock = DockStyle.Right;
            this.Controls.Add(mixmaster);

            //strip scroll bar runs along bottom of window under channel strips
            stripScroll = new HScrollBar();
            stripScroll.Minimum = 0;
            stripScroll.Location = new Point(0, MixerStrip.STRIPHEIGHT);
            stripScroll.Size = new Size(MixerStrip.STRIPWIDTH, 20);
            stripScroll.LargeChange = MixerStrip.STRIPWIDTH / 2;
            stripScroll.ValueChanged += new EventHandler(stripScroll_ValueChanged);
            this.Controls.Add(stripScroll);

            //strip panel contains the channel strips
            stripPanel = new Panel();
            stripPanel.Location = new Point(0, 0);
            stripPanel.Size = new Size(0, 0);
            stripPanel.TabStop = false;
            this.Controls.Add(stripPanel);

            mixerStrips = new List<MixerStrip>();
            stripCount = 0;

            //fix height & width for initial one strip view
            this.ClientSize = new Size(mixmaster.Width + MixerStrip.STRIPWIDTH, MixerStrip.STRIPHEIGHT + stripScroll.Height);
            this.MinimumSize = new Size(this.Width, this.Height);

            addMixerStrip(null);
            addMixerStrip(null);
            addMixerStrip(null);
            addMixerStrip(null);
            addMixerStrip(null);
            addMixerStrip(null);
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MixerWindow));
            this.SuspendLayout();
            // 
            // MixerWindow
            // 
            this.BackColor = System.Drawing.Color.Chartreuse;
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

        //compensate for the fact that if the scrollbar max = 200, the greatest value will be 200 - THUMBWIDTH (value determined experimentally)
        const int THUMBWIDTH = 44;

        private void updateScrollBar()
        {
            if (stripScroll != null)
            {
                stripScroll.Maximum = (mixmaster.Left < stripPanel.Width) ? (stripPanel.Width - mixmaster.Left) + THUMBWIDTH : 0;
                if ((stripScroll.Maximum > THUMBWIDTH) && (stripScroll.Maximum - stripScroll.Value < THUMBWIDTH))
                {
                    stripScroll.Value = stripScroll.Maximum - THUMBWIDTH;
                }
            }
        }

        private void MixerWindow_Resize(object sender, EventArgs e)
        {
            stripScroll.Size = new Size(mixmaster.Left, 20);
            updateScrollBar();
        }

        void stripScroll_ValueChanged(object sender, EventArgs e)
        {
            stripPanel.Location = new Point(-stripScroll.Value, 0);
        }

//-----------------------------------------------------------------------------

        public void addMixerStrip(X1Track track)
        {
            MixerStrip strip = new MixerStrip(this, track);
            mixerStrips.Add(strip);
            stripPanel.Controls.Add(strip);
            stripCount++;

            int xpos = 0;
            foreach (MixerStrip mixStrip in mixerStrips)
            {
                mixStrip.Location = new Point(xpos, 0);
                xpos += MixerStrip.STRIPWIDTH;
            }
            stripPanel.Size = new Size(xpos, MixerStrip.STRIPHEIGHT);
            this.MaximumSize = new Size((this.Right - mixmaster.Left) + stripPanel.Width, this.Height);
            updateScrollBar();
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
            //this.MaximumSize = new Size(mixermaxWidth, this.Height);       //but don't allow mixer to be stretched past strips + master width
            Invalidate();
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

        public void stop()
        {

            mixmaster.quiet();
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
                    //mixerStrips[i].btnMute.Enabled = true;
                    //mixerStrips[i].btnSolo.Enabled = false;
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
                    //mixerStrips[i].btnSolo.Enabled = true;
                }
            }
        }

    }
}
