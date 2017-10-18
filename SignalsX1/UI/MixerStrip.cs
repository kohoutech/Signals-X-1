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

namespace Signals.UI
{
    public class MixerStrip : UserControl
    {
        public static int STRIPHEIGHT = 450;
        public static int STRIPWIDTH = 90;

        //obj graph
        public MixerWindow mixer;
        public X1Track track;

        //params
        public int trackNumber;
        public String trackName;
        public int deviceNumber;
        public float level;
        public float pan;
        public bool isMuted;
        public bool isSoloing;
        public bool isRecording;

        //controls
        private TextBox txtTrackName;
        private Label lblTrackNum;
        private ComboBox cbxSource;
        private VScrollBar vsbFader;
        private HScrollBar hsbPan;
        public Button btnSolo;
        public Button btnRecord;
        public Button btnMute;
        private Label lblPan;
        private Label lblLevel;
        private Label lblLPan;
        private Label lblRPan;

        //cons
        public MixerStrip(MixerWindow _mixer, X1Track _track)
        {
            mixer = _mixer;
            track = _track;
            track.mixerStrip = this;
            trackNumber = track.number;

            InitializeComponent();

            String trackString = (track.number + 1).ToString("D");
            lblTrackNum.Text = trackString;
            txtTrackName.Text = "Track " + trackString;

            cbxSource.DataSource = track.getInputDeviceList();            
            cbxSource.SelectedIndex = 0;
            deviceNumber = -1;

            level = 1.0f;
            pan = 0.5f;

            isMuted = false;
            isSoloing = false;
            isRecording = false;
        }

        public void close()
        {
            mixer = null;
            track.mixerStrip = null;
            track = null;            
        }

//-----------------------------------------------------------------------------

        private void InitializeComponent()
        {
            this.vsbFader = new System.Windows.Forms.VScrollBar();
            this.btnSolo = new System.Windows.Forms.Button();
            this.btnRecord = new System.Windows.Forms.Button();
            this.btnMute = new System.Windows.Forms.Button();
            this.lblTrackNum = new System.Windows.Forms.Label();
            this.cbxSource = new System.Windows.Forms.ComboBox();
            this.hsbPan = new System.Windows.Forms.HScrollBar();
            this.lblPan = new System.Windows.Forms.Label();
            this.lblLevel = new System.Windows.Forms.Label();
            this.lblLPan = new System.Windows.Forms.Label();
            this.lblRPan = new System.Windows.Forms.Label();
            this.txtTrackName = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // vsbFader
            // 
            this.vsbFader.Location = new System.Drawing.Point(44, 218);
            this.vsbFader.Maximum = 109;
            this.vsbFader.Name = "vsbFader";
            this.vsbFader.Size = new System.Drawing.Size(23, 202);
            this.vsbFader.TabIndex = 0;
            this.vsbFader.TabStop = true;
            this.vsbFader.Scroll += new System.Windows.Forms.ScrollEventHandler(this.vsbFader_Scroll);
            // 
            // btnSolo
            // 
            this.btnSolo.BackColor = System.Drawing.Color.White;
            this.btnSolo.ForeColor = System.Drawing.Color.Black;
            this.btnSolo.Location = new System.Drawing.Point(45, 109);
            this.btnSolo.Name = "btnSolo";
            this.btnSolo.Size = new System.Drawing.Size(40, 24);
            this.btnSolo.TabIndex = 3;
            this.btnSolo.Text = "Solo";
            this.btnSolo.UseVisualStyleBackColor = false;
            this.btnSolo.Click += new System.EventHandler(this.btnSolo_Click);
            // 
            // btnRecord
            // 
            this.btnRecord.BackColor = System.Drawing.Color.White;
            this.btnRecord.ForeColor = System.Drawing.Color.Red;
            this.btnRecord.Location = new System.Drawing.Point(14, 138);
            this.btnRecord.Name = "btnRecord";
            this.btnRecord.Size = new System.Drawing.Size(60, 24);
            this.btnRecord.TabIndex = 4;
            this.btnRecord.Text = "Record";
            this.btnRecord.UseVisualStyleBackColor = false;
            this.btnRecord.Click += new System.EventHandler(this.btnRecord_Click);            
            // 
            // btnMute
            // 
            this.btnMute.BackColor = System.Drawing.Color.White;
            this.btnMute.Location = new System.Drawing.Point(2, 109);
            this.btnMute.Name = "btnMute";
            this.btnMute.Size = new System.Drawing.Size(40, 24);
            this.btnMute.TabIndex = 2;
            this.btnMute.Text = "Mute";
            this.btnMute.UseVisualStyleBackColor = false;
            this.btnMute.Click += new System.EventHandler(this.btnMute_Click);
            // 
            // lblTrackNum
            // 
            this.lblTrackNum.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblTrackNum.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTrackNum.Location = new System.Drawing.Point(0, 423);
            this.lblTrackNum.Name = "lblTrackNum";
            this.lblTrackNum.Size = new System.Drawing.Size(88, 25);
            this.lblTrackNum.TabIndex = 11;
            this.lblTrackNum.Text = "00";
            this.lblTrackNum.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cbxSource
            // 
            this.cbxSource.DropDownWidth = 150;
            this.cbxSource.FormattingEnabled = true;
            this.cbxSource.Location = new System.Drawing.Point(4, 4);
            this.cbxSource.Name = "cbxSource";
            this.cbxSource.Size = new System.Drawing.Size(81, 21);
            this.cbxSource.TabIndex = 6;
            this.cbxSource.Text = "no input";
            this.cbxSource.SelectedIndexChanged += new System.EventHandler(this.cbxSource_SelectedIndexChanged);
            // 
            // hsbPan
            // 
            this.hsbPan.Location = new System.Drawing.Point(4, 57);
            this.hsbPan.Maximum = 109;
            this.hsbPan.Name = "hsbPan";
            this.hsbPan.Size = new System.Drawing.Size(80, 20);
            this.hsbPan.TabIndex = 1;
            this.hsbPan.TabStop = true;
            this.hsbPan.Value = 50;
            this.hsbPan.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hsbPan_Scroll);
            // 
            // lblPan
            // 
            this.lblPan.AutoSize = true;
            this.lblPan.Location = new System.Drawing.Point(32, 39);
            this.lblPan.Name = "lblPan";
            this.lblPan.Size = new System.Drawing.Size(26, 13);
            this.lblPan.TabIndex = 7;
            this.lblPan.Text = "Pan";
            // 
            // lblLevel
            // 
            this.lblLevel.AutoSize = true;
            this.lblLevel.Location = new System.Drawing.Point(28, 197);
            this.lblLevel.Name = "lblLevel";
            this.lblLevel.Size = new System.Drawing.Size(33, 13);
            this.lblLevel.TabIndex = 10;
            this.lblLevel.Text = "Level";
            // 
            // lblLPan
            // 
            this.lblLPan.AutoSize = true;
            this.lblLPan.Location = new System.Drawing.Point(6, 78);
            this.lblLPan.Name = "lblLPan";
            this.lblLPan.Size = new System.Drawing.Size(13, 13);
            this.lblLPan.TabIndex = 8;
            this.lblLPan.Text = "L";
            // 
            // lblRPan
            // 
            this.lblRPan.AutoSize = true;
            this.lblRPan.Location = new System.Drawing.Point(71, 78);
            this.lblRPan.Name = "lblRPan";
            this.lblRPan.Size = new System.Drawing.Size(15, 13);
            this.lblRPan.TabIndex = 9;
            this.lblRPan.Text = "R";
            // 
            // txtTrackName
            // 
            this.txtTrackName.Location = new System.Drawing.Point(-1, 170);
            this.txtTrackName.Name = "txtTrackName";
            this.txtTrackName.Size = new System.Drawing.Size(90, 20);
            this.txtTrackName.TabIndex = 5;
            this.txtTrackName.Text = "Track 0";
            this.txtTrackName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtTrackName.TextChanged += new System.EventHandler(this.txtTrackName_TextChanged);
            // 
            // MixerStrip
            // 
            this.BackColor = System.Drawing.Color.LightSteelBlue;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.txtTrackName);
            this.Controls.Add(this.lblRPan);
            this.Controls.Add(this.lblLPan);
            this.Controls.Add(this.lblLevel);
            this.Controls.Add(this.lblPan);
            this.Controls.Add(this.hsbPan);
            this.Controls.Add(this.cbxSource);
            this.Controls.Add(this.lblTrackNum);
            this.Controls.Add(this.btnMute);
            this.Controls.Add(this.btnRecord);
            this.Controls.Add(this.btnSolo);
            this.Controls.Add(this.vsbFader);
            this.DoubleBuffered = true;
            this.Name = "MixerStrip";
            this.Size = new System.Drawing.Size(88, 448);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        public void updateSettings()
        {
            trackNumber = track.number;
            trackName = track.name;
            level = track.level;
            pan = track.pan;

            txtTrackName.Text = trackName;
            String trackString = (trackNumber + 1).ToString("D");
            lblTrackNum.Text = trackString;
            vsbFader.Value = 100 - (int)(level * 100);
            hsbPan.Value = (int)(pan * 100);           

            this.Invalidate();
        }


//-control methods ------------------------------------------------------------

        private void txtTrackName_TextChanged(object sender, EventArgs e)
        {
            track.setTrackName(txtTrackName.Text);
        }

        private void cbxSource_SelectedIndexChanged(object sender, EventArgs e)
        {
            deviceNumber = cbxSource.SelectedIndex - 1;
            track.setInputDevice(deviceNumber);
        }

        private void vsbFader_Scroll(object sender, ScrollEventArgs e)
        {
            int val = 100 - vsbFader.Value;
            level = val / 100.0f;
            track.setLevel(level);
        }

        private void hsbPan_Scroll(object sender, ScrollEventArgs e)
        {
            pan = hsbPan.Value / 100.0f;
            track.setPan(pan);
        }

        //two methods for each button - an event handler that only sets the state for this strip
        //and a general method that can be called by MixerWindow to set states of other strip's buttons
        public void setMuted(bool on)
        {
            isMuted = on;
            track.setMuted(on);
            btnMute.BackColor = (on) ? Color.Black : Color.White;
            btnMute.ForeColor = (on) ? Color.White : Color.Black;
            btnMute.Invalidate();
        }

        private void btnMute_Click(object sender, EventArgs e)
        {
            bool turnOn = !isMuted;
            setMuted(turnOn);
            if (turnOn) setSoloing(false);
            mixer.unsetSoloTrack(this);
        }

        public void setSoloing(bool on)
        {
            isSoloing = on;
            track.setSolo(on);
            btnSolo.BackColor = (on) ? Color.Blue : Color.White;
            btnSolo.ForeColor = (on) ? Color.White : Color.Black;
            btnSolo.Invalidate();
        }

        private void btnSolo_Click(object sender, EventArgs e)
        {
            bool turnOn = !isSoloing;
            setSoloing(turnOn);
            if (turnOn) setMuted(false);
            mixer.setSoloTrack(this);
        }

        public void setRecording(bool on)
        {
            isRecording = on;
            track.setRecording(on);
            btnRecord.BackColor = (on) ? Color.Red : Color.White;
            btnRecord.ForeColor = (on) ? Color.White : Color.Red;
            btnRecord.Invalidate();
        }

        private void btnRecord_Click(object sender, EventArgs e)
        {
            bool turnOn = !isRecording;
            setRecording(turnOn);
            if (turnOn)
            {
                setMuted(false);
                setSoloing(false);
                mixer.setRecordingTrack(this);
            }
            else
            {
                mixer.unsetRecordingTrack(this);
            }
            btnMute.Enabled = !turnOn;
            btnSolo.Enabled = !turnOn;            
        }

//- painting ------------------------------------------------------------------

        //all the strip graphics that aren't part of child controls
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            //section separators
            g.DrawLine(Pens.Black, 0, 30, 90, 30);
            g.DrawLine(Pens.Black, 0, 100, 90, 100);

            //fader gradients
            int gradlinex = 32;
            int gradliney = 237;
            Point gradNumPoint = new Point(14, 229);
            for (int i = 0; i <= 10; i++)
            {
                String gradNum = (10 - i).ToString("D");
                g.DrawString(gradNum, SystemFonts.DialogFont, Brushes.Black, gradNumPoint);
                gradNumPoint.Offset(0, 15);
                g.DrawLine(Pens.Black, gradlinex, gradliney + (i * 15), gradlinex + 8, gradliney + (i * 15));
                if (i == 0) gradNumPoint.Offset(6, 0);
            }
        }
    }
}

//  Console.WriteLine("there's no sun in the shadow of the wizard");
