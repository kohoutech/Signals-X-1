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

using Transonic.Wave;

namespace Signals.UI
{
    public class ControlPanel : UserControl
    {
        private Button btnPlay;
        private Button btnPause;
        private Button btnStop;
        private Button btnRewind;
        private Button btnFastForward;
        private Button btnRecord;
        private Label lblPosCounter;
        private HScrollBar hsbPosSelector;
        private Button btnMixer;

        public SignalsWindow signalsWindow;
        public Waverly waverly;
        public X1Project project;

        public bool isPlaying;
        public bool isPaused;
        public bool isRecording;

        public ControlPanel(SignalsWindow _signalsWindow)
        {
            InitializeComponent();
            signalsWindow = _signalsWindow;
            waverly = signalsWindow.waverly;

            isPlaying = false;
            isPaused = false;
            isRecording = false;
        }
    
        private void InitializeComponent()
        {
            this.btnPlay = new System.Windows.Forms.Button();
            this.btnPause = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnRewind = new System.Windows.Forms.Button();
            this.btnFastForward = new System.Windows.Forms.Button();
            this.btnRecord = new System.Windows.Forms.Button();
            this.lblPosCounter = new System.Windows.Forms.Label();
            this.hsbPosSelector = new System.Windows.Forms.HScrollBar();
            this.btnMixer = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnPlay
            // 
            this.btnPlay.BackColor = System.Drawing.Color.Gainsboro;
            this.btnPlay.Enabled = false;
            this.btnPlay.Location = new System.Drawing.Point(8, 8);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(52, 50);
            this.btnPlay.TabIndex = 0;
            this.btnPlay.Text = "Play";
            this.btnPlay.UseVisualStyleBackColor = false;
            this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
            // 
            // btnPause
            // 
            this.btnPause.BackColor = System.Drawing.Color.Gainsboro;
            this.btnPause.Enabled = false;
            this.btnPause.Location = new System.Drawing.Point(66, 8);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(52, 50);
            this.btnPause.TabIndex = 1;
            this.btnPause.Text = "Pause";
            this.btnPause.UseVisualStyleBackColor = false;
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // btnStop
            // 
            this.btnStop.BackColor = System.Drawing.Color.Gainsboro;
            this.btnStop.Enabled = false;
            this.btnStop.Location = new System.Drawing.Point(123, 8);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(52, 50);
            this.btnStop.TabIndex = 2;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = false;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnRewind
            // 
            this.btnRewind.BackColor = System.Drawing.Color.Gainsboro;
            this.btnRewind.Enabled = false;
            this.btnRewind.Location = new System.Drawing.Point(180, 8);
            this.btnRewind.Name = "btnRewind";
            this.btnRewind.Size = new System.Drawing.Size(52, 50);
            this.btnRewind.TabIndex = 3;
            this.btnRewind.Text = "Rewind";
            this.btnRewind.UseVisualStyleBackColor = false;
            this.btnRewind.Click += new System.EventHandler(this.btnRewind_Click);
            // 
            // btnFastForward
            // 
            this.btnFastForward.BackColor = System.Drawing.Color.Gainsboro;
            this.btnFastForward.Enabled = false;
            this.btnFastForward.Location = new System.Drawing.Point(237, 8);
            this.btnFastForward.Name = "btnFastForward";
            this.btnFastForward.Size = new System.Drawing.Size(52, 50);
            this.btnFastForward.TabIndex = 4;
            this.btnFastForward.Text = "FF";
            this.btnFastForward.UseVisualStyleBackColor = false;
            this.btnFastForward.Click += new System.EventHandler(this.btnFastForward_Click);
            // 
            // btnRecord
            // 
            this.btnRecord.BackColor = System.Drawing.Color.Gainsboro;
            this.btnRecord.Enabled = false;
            this.btnRecord.FlatAppearance.BorderColor = System.Drawing.Color.Red;
            this.btnRecord.FlatAppearance.BorderSize = 2;
            this.btnRecord.ForeColor = System.Drawing.Color.Red;
            this.btnRecord.Location = new System.Drawing.Point(295, 8);
            this.btnRecord.Name = "btnRecord";
            this.btnRecord.Size = new System.Drawing.Size(52, 50);
            this.btnRecord.TabIndex = 5;
            this.btnRecord.Text = "Record";
            this.btnRecord.UseVisualStyleBackColor = false;
            this.btnRecord.Click += new System.EventHandler(this.btnRecord_Click);
            // 
            // lblPosCounter
            // 
            this.lblPosCounter.BackColor = System.Drawing.Color.Black;
            this.lblPosCounter.Font = new System.Drawing.Font("Arial Rounded MT Bold", 14F);
            this.lblPosCounter.ForeColor = System.Drawing.Color.Lime;
            this.lblPosCounter.Location = new System.Drawing.Point(354, 8);
            this.lblPosCounter.Name = "lblPosCounter";
            this.lblPosCounter.Size = new System.Drawing.Size(150, 25);
            this.lblPosCounter.TabIndex = 6;
            this.lblPosCounter.Text = "00:00:00.000";
            this.lblPosCounter.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // hsbPosSelector
            // 
            this.hsbPosSelector.Location = new System.Drawing.Point(353, 38);
            this.hsbPosSelector.Maximum = 1009;
            this.hsbPosSelector.Name = "hsbPosSelector";
            this.hsbPosSelector.Size = new System.Drawing.Size(150, 20);
            this.hsbPosSelector.TabIndex = 7;
            this.hsbPosSelector.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hsbPosSelector_Scroll);
            // 
            // btnMixer
            // 
            this.btnMixer.BackColor = System.Drawing.Color.Gainsboro;
            this.btnMixer.Location = new System.Drawing.Point(513, 8);
            this.btnMixer.Name = "btnMixer";
            this.btnMixer.Size = new System.Drawing.Size(52, 50);
            this.btnMixer.TabIndex = 8;
            this.btnMixer.Text = "Mixer";
            this.btnMixer.UseVisualStyleBackColor = false;
            this.btnMixer.Click += new System.EventHandler(this.btnMixer_Click);
            // 
            // ControlPanel
            // 
            this.BackColor = System.Drawing.Color.LightSteelBlue;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.btnMixer);
            this.Controls.Add(this.hsbPosSelector);
            this.Controls.Add(this.lblPosCounter);
            this.Controls.Add(this.btnRecord);
            this.Controls.Add(this.btnFastForward);
            this.Controls.Add(this.btnRewind);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnPause);
            this.Controls.Add(this.btnPlay);
            this.Name = "ControlPanel";
            this.Size = new System.Drawing.Size(575, 66);
            this.ResumeLayout(false);

        }

        public void setProject(X1Project _project)
        {
            project = _project;
            enableTransport(true);
        }

        public void clearProject(X1Project _project)
        {
            project = null;
            enableTransport(false);
        }

//-----------------------------------------------------------------------------

        //these buttons only make sense if we have tracks to play
        public void enableTransport(Boolean on)
        {
            btnPlay.Enabled = on;
            btnPause.Enabled = on;
            btnStop.Enabled = on;
            btnRecord.Enabled = on;
        }

        public void setPlayButton(Boolean on)
        {
            isPlaying = on;
            btnPlay.BackColor = (on) ? Color.Blue : Color.Gainsboro;
            btnPlay.ForeColor = (on) ? Color.White : Color.Black;
        }

        public void setRecordButton(Boolean on)
        {
            isRecording = on;
            btnRecord.BackColor = (on) ? Color.Red : Color.Gainsboro;
            btnRecord.ForeColor = (on) ? Color.White : Color.Red;
        }

        public void setPauseButton(Boolean on)
        {
            isPaused = on;
            hsbPosSelector.Enabled = on || !isRecording;
            btnPause.BackColor = (on) ? Color.Black : Color.Gainsboro;
            btnPause.ForeColor = (on) ? Color.White : Color.Black;
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            signalsWindow.playTransport();
            setPlayButton(true);
            setRecordButton(false);
            if (!isPaused) btnRecord.Enabled = false;
        }

        private void btnRecord_Click(object sender, EventArgs e)
        {
            signalsWindow.recordTransport();
            setRecordButton(true);
            setPlayButton(false);
            if (!isPaused) btnPlay.Enabled = false;
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            signalsWindow.pauseTransport(!isPaused);
            setPauseButton(!isPaused);
            if (isPaused)
            {
                btnPlay.Enabled = true;
                btnRecord.Enabled = true;
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            signalsWindow.stopTransport();
            setPlayButton(false);
            setRecordButton(false);
            setPauseButton(false);
            btnPlay.Enabled = true;
            btnRecord.Enabled = true;
        }

        private void btnRewind_Click(object sender, EventArgs e)
        {

        }

        private void btnFastForward_Click(object sender, EventArgs e)
        {
            signalsWindow.fastForwardTransport();       //yikes!!
        }

        private void hsbPosSelector_Scroll(object sender, ScrollEventArgs e)
        {
            int msTime = hsbPosSelector.Value * project.duration;
            signalsWindow.setCurrentTime(msTime);
        }

        public void timerTick(int msTime)
        {
            int msVal = msTime % 1000;
            int secPos = msTime / 1000;
            int secVal = secPos % 60;
            int minPos = secPos / 60;
            int minVal = minPos % 60;
            int hrVal = minPos / 60;
            lblPosCounter.Text = hrVal.ToString("D2") + ":" + minVal.ToString("D2") + ":" +
                secVal.ToString("D2") + "." + msVal.ToString("D3");

            int sliderPos = msTime / project.duration;
            hsbPosSelector.Value = sliderPos;

            lblPosCounter.Invalidate();
            hsbPosSelector.Invalidate();
        }

        private void btnMixer_Click(object sender, EventArgs e)
        {
            signalsWindow.mixerWindow.Show();
        }
    }
}
