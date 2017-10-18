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
    public class TrackView : UserControl
    {
        public static int TRACKHEIGHT = 128;
        public static int TRACKHEADERWIDTH = 55;

        //obj graph
        public TrackPanel trackPanel;
        public X1Track track;

        //controls
        private Button bntClose;
        private Label lblTrackNum;
        private Label lblSolo;
        private Label lblMute;
        private Label lblRecord;

        public TrackData trackData;
        public String trackName;
        public bool isMuted;
        public bool isSoloing;
        public bool isRecording;

        public TrackView(TrackPanel _trackPanel, X1Track _track, int width)
        {
            trackPanel = _trackPanel;
            track = _track;
            track.trackView = this;
            trackName = track.name;

            InitializeComponent();
            this.Width = width + TRACKHEADERWIDTH + 2;
            lblTrackNum.Text = (track.number + 1).ToString("D");
            trackPanel.signalsWindow.setTooltip(lblTrackNum, trackName);

            trackData = new TrackData(this);
            trackData.Width = width;
            trackData.Location = new Point(TRACKHEADERWIDTH, 0);
            this.Controls.Add(trackData);

            isMuted = false;
            isSoloing = false;
            isRecording = false;
        }

        private void InitializeComponent()
        {
            this.lblTrackNum = new System.Windows.Forms.Label();
            this.bntClose = new System.Windows.Forms.Button();
            this.lblSolo = new System.Windows.Forms.Label();
            this.lblMute = new System.Windows.Forms.Label();
            this.lblRecord = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblTrackNum
            // 
            this.lblTrackNum.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTrackNum.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblTrackNum.Location = new System.Drawing.Point(0, 25);
            this.lblTrackNum.Name = "lblTrackNum";
            this.lblTrackNum.Size = new System.Drawing.Size(25, 40);
            this.lblTrackNum.TabIndex = 0;
            this.lblTrackNum.Text = "00";
            this.lblTrackNum.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // bntClose
            // 
            this.bntClose.BackColor = System.Drawing.Color.Red;
            this.bntClose.Location = new System.Drawing.Point(0, -1);
            this.bntClose.Name = "bntClose";
            this.bntClose.Size = new System.Drawing.Size(25, 25);
            this.bntClose.TabIndex = 1;
            this.bntClose.Text = "X";
            this.bntClose.UseVisualStyleBackColor = false;
            this.bntClose.Click += new System.EventHandler(this.bntClose_Click);
            // 
            // lblSolo
            // 
            this.lblSolo.BackColor = System.Drawing.Color.White;
            this.lblSolo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblSolo.ForeColor = System.Drawing.Color.Black;
            this.lblSolo.Location = new System.Drawing.Point(4, 108);
            this.lblSolo.Name = "lblSolo";
            this.lblSolo.Size = new System.Drawing.Size(18, 18);
            this.lblSolo.TabIndex = 2;
            this.lblSolo.Text = "S";
            this.lblSolo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblMute
            // 
            this.lblMute.BackColor = System.Drawing.Color.White;
            this.lblMute.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblMute.ForeColor = System.Drawing.Color.Black;
            this.lblMute.Location = new System.Drawing.Point(4, 87);
            this.lblMute.Name = "lblMute";
            this.lblMute.Size = new System.Drawing.Size(18, 18);
            this.lblMute.TabIndex = 3;
            this.lblMute.Text = "M";
            this.lblMute.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblRecord
            // 
            this.lblRecord.BackColor = System.Drawing.Color.White;
            this.lblRecord.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblRecord.ForeColor = System.Drawing.Color.Red;
            this.lblRecord.Location = new System.Drawing.Point(4, 66);
            this.lblRecord.Name = "lblRecord";
            this.lblRecord.Size = new System.Drawing.Size(18, 18);
            this.lblRecord.TabIndex = 4;
            this.lblRecord.Text = "R";
            this.lblRecord.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TrackView
            // 
            this.BackColor = System.Drawing.Color.LightSteelBlue;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.lblRecord);
            this.Controls.Add(this.lblMute);
            this.Controls.Add(this.lblSolo);
            this.Controls.Add(this.bntClose);
            this.Controls.Add(this.lblTrackNum);
            this.DoubleBuffered = true;
            this.Name = "TrackView";
            this.Size = new System.Drawing.Size(300, 130);
            this.MouseHover += new System.EventHandler(this.TrackView_MouseHover);
            this.ResumeLayout(false);

        }

        public void close()
        {
            trackPanel = null;
            track.trackView = null;
            track = null;
            trackData.trackView = null;
            trackData = null;
        }

        private void bntClose_Click(object sender, EventArgs e)
        {
            if (!trackPanel.signalsWindow.controlPanel.isPlaying)
                track.close();            
        }

//-----------------------------------------------------------------------------

        public void updateWidth(int width)
        {
            this.Width = width + TRACKHEADERWIDTH + 2;
            trackData.Width = width;            
        }


        public void updateTrackNumber()
        {
            lblTrackNum.Text = (track.number + 1).ToString("D");
        }

        public void setTrackName(String name)
        {
            trackName = name;
            trackPanel.signalsWindow.setTooltip(lblTrackNum, trackName);
        }

        public void setMuteIndicator(bool on) 
        {
            lblMute.BackColor = (on) ? Color.Black : Color.White;
            lblMute.ForeColor = (on) ? Color.White : Color.Black;
            lblMute.Invalidate();
        }

        public void setSoloIndicator(bool on)
        {
            lblSolo.BackColor = (on) ? Color.Blue : Color.White;
            lblSolo.ForeColor = (on) ? Color.White : Color.Black;
            lblSolo.Invalidate();
        }

        public void setRecordingIndicator(bool on)
        {
            lblRecord.BackColor = (on) ? Color.Red : Color.White;
            lblRecord.ForeColor = (on) ? Color.White : Color.Red;
            lblRecord.Invalidate();
        }

//- painting ------------------------------------------------------------------

        //all the strip graphics that aren't part of child controls
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            g.FillRectangle(Brushes.White, 25, 0, 30, 126);
            g.DrawLine(Pens.Black, 47, 32, 54, 32);
            g.DrawLine(Pens.Black, 47, 64, 54, 64);
            g.DrawLine(Pens.Black, 47, 96, 54, 96);

            g.DrawString("1.0", SystemFonts.DialogFont, Brushes.Black, 29, 2);
            g.DrawString("0.5", SystemFonts.DialogFont, Brushes.Black, 29, 26);
            g.DrawString("0.0", SystemFonts.DialogFont, Brushes.Black, 29, 58);
            g.DrawString("-0.5", SystemFonts.DialogFont, Brushes.Black, 26, 90);
            g.DrawString("-1.0", SystemFonts.DialogFont, Brushes.Black, 26, 111);

            int gradlinex = 32;
            int gradliney = 237;
            Point gradNumPoint = new Point(14, 229);
            for (int i = 0; i <= 10; i++)
            {
                gradNumPoint.Offset(0, 15);
                g.DrawLine(Pens.Black, gradlinex, gradliney + (i * 15), gradlinex + 8, gradliney + (i * 15));
                if (i == 0) gradNumPoint.Offset(6, 0);
            }
        }

        private void TrackView_MouseHover(object sender, EventArgs e)
        {
            //trackPanel.signalsWindow.showTooltip(track.name);
        }
    }
}
