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
using System.Drawing.Drawing2D;
using Signals.Project;

namespace Signals.UI
{
    public class MixerStrip : UserControl
    {
        public static int STRIPHEIGHT = 900;
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
        private Transonic.Widget.Knob knbGain;
        private Transonic.Widget.Knob knbEqLow;
        private Transonic.Widget.Knob knbSend;
        private Transonic.Widget.Knob knbPan;
        private Transonic.Widget.Knob knbEqPara;
        private Transonic.Widget.Knob knbEqMid;
        private Transonic.Widget.Knob knbEqHi;
        private Transonic.Widget.SelectionList selDest;
        private Transonic.Widget.SelectionList selReturn;
        private Transonic.Widget.SelectionList selSend;
        private Transonic.Widget.SelectionList selSource;
        private Label lblChannelName;
        private Transonic.Widget.PushButton btnMute;
        private Transonic.Widget.PushButton btnSolo;
        private Transonic.Widget.PushButton btnRecord;
        private Transonic.Widget.Slider sldLevel;

        //controls
        private Label lblTrackNum;

        //cons
        public MixerStrip(MixerWindow _mixer, X1Track _track)
        {
            mixer = _mixer;
            track = _track;
            //track.mixerStrip = this;
            //trackNumber = track.number;

            InitializeComponent();

            //String trackString = (track.number + 1).ToString("D");
            //lblTrackNum.Text = trackString;
            //txtTrackName.Text = "Track " + trackString;

            //cbxSource.DataSource = track.getInputDeviceList();            
            //cbxSource.SelectedIndex = 0;
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
            this.lblTrackNum = new System.Windows.Forms.Label();
            this.lblChannelName = new System.Windows.Forms.Label();
            this.sldLevel = new Transonic.Widget.Slider();
            this.btnRecord = new Transonic.Widget.PushButton();
            this.btnSolo = new Transonic.Widget.PushButton();
            this.btnMute = new Transonic.Widget.PushButton();
            this.selSource = new Transonic.Widget.SelectionList();
            this.selSend = new Transonic.Widget.SelectionList();
            this.selReturn = new Transonic.Widget.SelectionList();
            this.selDest = new Transonic.Widget.SelectionList();
            this.knbEqHi = new Transonic.Widget.Knob();
            this.knbEqMid = new Transonic.Widget.Knob();
            this.knbEqPara = new Transonic.Widget.Knob();
            this.knbPan = new Transonic.Widget.Knob();
            this.knbSend = new Transonic.Widget.Knob();
            this.knbEqLow = new Transonic.Widget.Knob();
            this.knbGain = new Transonic.Widget.Knob();
            this.SuspendLayout();
            // 
            // lblTrackNum
            // 
            this.lblTrackNum.BackColor = System.Drawing.Color.Transparent;
            this.lblTrackNum.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTrackNum.Location = new System.Drawing.Point(24, 870);
            this.lblTrackNum.Name = "lblTrackNum";
            this.lblTrackNum.Size = new System.Drawing.Size(40, 25);
            this.lblTrackNum.TabIndex = 11;
            this.lblTrackNum.Text = "00";
            this.lblTrackNum.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblChannelName
            // 
            this.lblChannelName.AutoSize = true;
            this.lblChannelName.BackColor = System.Drawing.Color.Transparent;
            this.lblChannelName.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblChannelName.Location = new System.Drawing.Point(7, 7);
            this.lblChannelName.Name = "lblChannelName";
            this.lblChannelName.Size = new System.Drawing.Size(80, 18);
            this.lblChannelName.TabIndex = 23;
            this.lblChannelName.Text = "CHANNEL";
            // 
            // sldLevel
            // 
            this.sldLevel.BackColor = System.Drawing.Color.Orange;
            this.sldLevel.Location = new System.Drawing.Point(5, 35);
            this.sldLevel.Name = "sldLevel";
            this.sldLevel.Size = new System.Drawing.Size(50, 250);
            this.sldLevel.TabIndex = 27;
            // 
            // btnRecord
            // 
            this.btnRecord.BackColor = System.Drawing.Color.Red;
            this.btnRecord.Location = new System.Drawing.Point(14, 527);
            this.btnRecord.Name = "btnRecord";
            this.btnRecord.Size = new System.Drawing.Size(60, 25);
            this.btnRecord.TabIndex = 26;
            // 
            // btnSolo
            // 
            this.btnSolo.BackColor = System.Drawing.Color.Red;
            this.btnSolo.Location = new System.Drawing.Point(46, 500);
            this.btnSolo.Name = "btnSolo";
            this.btnSolo.Size = new System.Drawing.Size(40, 24);
            this.btnSolo.TabIndex = 25;
            // 
            // btnMute
            // 
            this.btnMute.BackColor = System.Drawing.Color.Red;
            this.btnMute.Location = new System.Drawing.Point(4, 500);
            this.btnMute.Name = "btnMute";
            this.btnMute.Size = new System.Drawing.Size(40, 24);
            this.btnMute.TabIndex = 24;
            // 
            // selSource
            // 
            this.selSource.BackColor = System.Drawing.Color.Gold;
            this.selSource.Location = new System.Drawing.Point(5, 35);
            this.selSource.Name = "selSource";
            this.selSource.Size = new System.Drawing.Size(80, 20);
            this.selSource.TabIndex = 22;
            // 
            // selSend
            // 
            this.selSend.BackColor = System.Drawing.Color.Gold;
            this.selSend.Location = new System.Drawing.Point(5, 121);
            this.selSend.Name = "selSend";
            this.selSend.Size = new System.Drawing.Size(80, 20);
            this.selSend.TabIndex = 21;
            // 
            // selReturn
            // 
            this.selReturn.BackColor = System.Drawing.Color.Gold;
            this.selReturn.Location = new System.Drawing.Point(5, 146);
            this.selReturn.Name = "selReturn";
            this.selReturn.Size = new System.Drawing.Size(80, 20);
            this.selReturn.TabIndex = 20;
            // 
            // selDest
            // 
            this.selDest.BackColor = System.Drawing.Color.Gold;
            this.selDest.Location = new System.Drawing.Point(5, 231);
            this.selDest.Name = "selDest";
            this.selDest.Size = new System.Drawing.Size(80, 20);
            this.selDest.TabIndex = 19;
            // 
            // knbEqHi
            // 
            this.knbEqHi.BackColor = System.Drawing.Color.Transparent;
            this.knbEqHi.Location = new System.Drawing.Point(20, 268);
            this.knbEqHi.Name = "knbEqHi";
            this.knbEqHi.Size = new System.Drawing.Size(50, 50);
            this.knbEqHi.TabIndex = 18;
            // 
            // knbEqMid
            // 
            this.knbEqMid.BackColor = System.Drawing.Color.Transparent;
            this.knbEqMid.Location = new System.Drawing.Point(20, 324);
            this.knbEqMid.Name = "knbEqMid";
            this.knbEqMid.Size = new System.Drawing.Size(50, 50);
            this.knbEqMid.TabIndex = 17;
            // 
            // knbEqPara
            // 
            this.knbEqPara.BackColor = System.Drawing.Color.Transparent;
            this.knbEqPara.Location = new System.Drawing.Point(20, 380);
            this.knbEqPara.Name = "knbEqPara";
            this.knbEqPara.Size = new System.Drawing.Size(50, 50);
            this.knbEqPara.TabIndex = 16;
            // 
            // knbPan
            // 
            this.knbPan.BackColor = System.Drawing.Color.Transparent;
            this.knbPan.Location = new System.Drawing.Point(20, 560);
            this.knbPan.Name = "knbPan";
            this.knbPan.Size = new System.Drawing.Size(50, 50);
            this.knbPan.TabIndex = 15;
            // 
            // knbSend
            // 
            this.knbSend.BackColor = System.Drawing.Color.Transparent;
            this.knbSend.Location = new System.Drawing.Point(20, 175);
            this.knbSend.Name = "knbSend";
            this.knbSend.Size = new System.Drawing.Size(50, 50);
            this.knbSend.TabIndex = 14;
            // 
            // knbEqLow
            // 
            this.knbEqLow.BackColor = System.Drawing.Color.Transparent;
            this.knbEqLow.Location = new System.Drawing.Point(20, 436);
            this.knbEqLow.Name = "knbEqLow";
            this.knbEqLow.Size = new System.Drawing.Size(50, 50);
            this.knbEqLow.TabIndex = 13;
            // 
            // knbGain
            // 
            this.knbGain.BackColor = System.Drawing.Color.Transparent;
            this.knbGain.Location = new System.Drawing.Point(20, 65);
            this.knbGain.Name = "knbGain";
            this.knbGain.Size = new System.Drawing.Size(50, 50);
            this.knbGain.TabIndex = 12;
            // 
            // MixerStrip
            // 
            this.BackColor = System.Drawing.Color.LightSteelBlue;
            this.BackgroundImage = global::Signals.Properties.Resources.strip;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.Controls.Add(this.sldLevel);
            this.Controls.Add(this.btnRecord);
            this.Controls.Add(this.btnSolo);
            this.Controls.Add(this.btnMute);
            this.Controls.Add(this.lblChannelName);
            this.Controls.Add(this.selSource);
            this.Controls.Add(this.selSend);
            this.Controls.Add(this.selReturn);
            this.Controls.Add(this.selDest);
            this.Controls.Add(this.knbEqHi);
            this.Controls.Add(this.knbEqMid);
            this.Controls.Add(this.knbEqPara);
            this.Controls.Add(this.knbPan);
            this.Controls.Add(this.knbSend);
            this.Controls.Add(this.knbEqLow);
            this.Controls.Add(this.knbGain);
            this.Controls.Add(this.lblTrackNum);
            this.DoubleBuffered = true;
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "MixerStrip";
            this.Size = new System.Drawing.Size(90, 900);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        public void updateSettings()
        {
            //trackNumber = track.number;
            //trackName = track.name;
            //level = track.level;
            //pan = track.pan;

            //txtTrackName.Text = trackName;
            //String trackString = (trackNumber + 1).ToString("D");
            //lblTrackNum.Text = trackString;
            //vsbFader.Value = 100 - (int)(level * 100);
            //hsbPan.Value = (int)(pan * 100);           

            this.Invalidate();
        }


//-control methods ------------------------------------------------------------

        private void txtTrackName_TextChanged(object sender, EventArgs e)
        {
            //track.setTrackName(txtTrackName.Text);
        }

        private void cbxSource_SelectedIndexChanged(object sender, EventArgs e)
        {
            //deviceNumber = cbxSource.SelectedIndex - 1;
            //track.setInputDevice(deviceNumber);
        }

        private void vsbFader_Scroll(object sender, ScrollEventArgs e)
        {
            //int val = 100 - vsbFader.Value;
            //level = val / 100.0f;
            //track.setLevel(level);
        }

        private void hsbPan_Scroll(object sender, ScrollEventArgs e)
        {
            //pan = hsbPan.Value / 100.0f;
            //track.setPan(pan);
        }

        //two methods for each button - an event handler that only sets the state for this strip
        //and a general method that can be called by MixerWindow to set states of other strip's buttons
        public void setMuted(bool on)
        {
            isMuted = on;
            //track.setMuted(on);
            //btnMute.BackColor = (on) ? Color.Black : Color.White;
            //btnMute.ForeColor = (on) ? Color.White : Color.Black;
            //btnMute.Invalidate();
        }

        private void btnMute_Click(object sender, EventArgs e)
        {
            bool turnOn = !isMuted;
            //setMuted(turnOn);
            //if (turnOn) setSoloing(false);
            //mixer.unsetSoloTrack(this);
        }

        public void setSoloing(bool on)
        {
            isSoloing = on;
            //track.setSolo(on);
            //btnSolo.BackColor = (on) ? Color.Blue : Color.White;
            //btnSolo.ForeColor = (on) ? Color.White : Color.Black;
            //btnSolo.Invalidate();
        }

        private void btnSolo_Click(object sender, EventArgs e)
        {
            bool turnOn = !isSoloing;
            //setSoloing(turnOn);
            //if (turnOn) setMuted(false);
            //mixer.setSoloTrack(this);
        }

        public void setRecording(bool on)
        {
            isRecording = on;
            //track.setRecording(on);
            //btnRecord.BackColor = (on) ? Color.Red : Color.White;
            //btnRecord.ForeColor = (on) ? Color.White : Color.Red;
            //btnRecord.Invalidate();
        }

        private void btnRecord_Click(object sender, EventArgs e)
        {
            bool turnOn = !isRecording;
            setRecording(turnOn);
            if (turnOn)
            {
                setMuted(false);
                setSoloing(false);
                //mixer.setRecordingTrack(this);
            }
            else
            {
                //mixer.unsetRecordingTrack(this);
            }
            //btnMute.Enabled = !turnOn;
            //btnSolo.Enabled = !turnOn;            
        }

//- painting ------------------------------------------------------------------

        //all the strip graphics that aren't part of child controls
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            //Graphics g = e.Graphics;
            //g.SmoothingMode = SmoothingMode.AntiAlias;

            ////section separators
            //g.DrawLine(Pens.Black, 0, 30, 90, 30);
            //g.DrawLine(Pens.Black, 0, 100, 90, 100);

            ////fader gradients
            //int gradlinex = 32;
            //int gradliney = 237;
            //Point gradNumPoint = new Point(14, 229);
            //for (int i = 0; i <= 10; i++)
            //{
            //    String gradNum = (10 - i).ToString("D");
            //    g.DrawString(gradNum, SystemFonts.DialogFont, Brushes.Black, gradNumPoint);
            //    gradNumPoint.Offset(0, 15);
            //    g.DrawLine(Pens.Black, gradlinex, gradliney + (i * 15), gradlinex + 8, gradliney + (i * 15));
            //    if (i == 0) gradNumPoint.Offset(6, 0);
            //}
        }
    }
}

//  Console.WriteLine("there's no sun in the shadow of the wizard");
