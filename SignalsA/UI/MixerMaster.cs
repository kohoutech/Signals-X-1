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
using Signals.Widgets;
using Signals.Engine;

namespace Signals.UI
{
    public class MixerMaster : UserControl
    {
        //obj graph
        public MixerWindow mixerWindow;
        public X1Project project;

        //controls
        private VScrollBar vsbLeftFader;
        private VScrollBar vsbRightFader;
        private CheckBox chkLock;
        LevelMeter rightMeter;
        LevelMeter leftMeter;
        private Label lblMaster;
        private Label lblLevel;
        private Label lblMeter;

        //params
        float leftLevel;
        float rightLevel;
        bool fadersAreLocked;

        public MixerMaster(MixerWindow _mixer)
        {
            mixerWindow = _mixer;
            project = mixerWindow.project;
            InitializeComponent();
            chkLock.Checked = true;
            fadersAreLocked = true;

            leftMeter = new LevelMeter(this);
            leftMeter.Location = new Point(45, 26);
            this.Controls.Add(leftMeter);
            leftMeter.BringToFront();

            rightMeter = new LevelMeter(this);
            rightMeter.Location = new Point(80, 26);
            rightMeter.BringToFront();
            this.Controls.Add(rightMeter);
        }

        private void InitializeComponent()
        {
            this.vsbLeftFader = new System.Windows.Forms.VScrollBar();
            this.vsbRightFader = new System.Windows.Forms.VScrollBar();
            this.lblMaster = new System.Windows.Forms.Label();
            this.lblLevel = new System.Windows.Forms.Label();
            this.chkLock = new System.Windows.Forms.CheckBox();
            this.lblMeter = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // vsbLeftFader
            // 
            this.vsbLeftFader.Location = new System.Drawing.Point(45, 219);
            this.vsbLeftFader.Maximum = 109;
            this.vsbLeftFader.Name = "vsbLeftFader";
            this.vsbLeftFader.Size = new System.Drawing.Size(23, 200);
            this.vsbLeftFader.TabIndex = 0;
            this.vsbLeftFader.TabStop = true;
            this.vsbLeftFader.Scroll += new System.Windows.Forms.ScrollEventHandler(this.vsbLeftFader_Scroll);
            // 
            // vsbRightFader
            // 
            this.vsbRightFader.Location = new System.Drawing.Point(82, 219);
            this.vsbRightFader.Maximum = 109;
            this.vsbRightFader.Name = "vsbRightFader";
            this.vsbRightFader.Size = new System.Drawing.Size(23, 202);
            this.vsbRightFader.TabIndex = 1;
            this.vsbRightFader.TabStop = true;
            this.vsbRightFader.Scroll += new System.Windows.Forms.ScrollEventHandler(this.vsbRightFader_Scroll);
            // 
            // lblMaster
            // 
            this.lblMaster.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblMaster.ForeColor = System.Drawing.Color.White;
            this.lblMaster.Location = new System.Drawing.Point(0, 443);
            this.lblMaster.Name = "lblMaster";
            this.lblMaster.Size = new System.Drawing.Size(138, 25);
            this.lblMaster.TabIndex = 5;
            this.lblMaster.Text = "Master";
            this.lblMaster.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblLevel
            // 
            this.lblLevel.AutoSize = true;
            this.lblLevel.ForeColor = System.Drawing.Color.White;
            this.lblLevel.Location = new System.Drawing.Point(53, 197);
            this.lblLevel.Name = "lblLevel";
            this.lblLevel.Size = new System.Drawing.Size(33, 13);
            this.lblLevel.TabIndex = 4;
            this.lblLevel.Text = "Level";
            // 
            // chkLock
            // 
            this.chkLock.AutoSize = true;
            this.chkLock.Checked = true;
            this.chkLock.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkLock.Location = new System.Drawing.Point(85, 428);
            this.chkLock.Name = "chkLock";
            this.chkLock.Size = new System.Drawing.Size(50, 17);
            this.chkLock.TabIndex = 2;
            this.chkLock.Text = "Lock";
            this.chkLock.UseVisualStyleBackColor = true;
            this.chkLock.CheckedChanged += new System.EventHandler(this.chkLock_CheckedChanged);
            // 
            // lblMeter
            // 
            this.lblMeter.AutoSize = true;
            this.lblMeter.ForeColor = System.Drawing.Color.White;
            this.lblMeter.Location = new System.Drawing.Point(53, 7);
            this.lblMeter.Name = "lblMeter";
            this.lblMeter.Size = new System.Drawing.Size(34, 13);
            this.lblMeter.TabIndex = 3;
            this.lblMeter.Text = "Meter";
            // 
            // MixerMaster
            // 
            this.BackColor = System.Drawing.Color.SteelBlue;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.lblMeter);
            this.Controls.Add(this.chkLock);
            this.Controls.Add(this.lblLevel);
            this.Controls.Add(this.lblMaster);
            this.Controls.Add(this.vsbRightFader);
            this.Controls.Add(this.vsbLeftFader);
            this.Name = "MixerMaster";
            this.Size = new System.Drawing.Size(138, 468);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        public void updateSettings()
        {
            vsbLeftFader.Value = 100 - (int)(project.leftOutLevel * 100);
            vsbRightFader.Value = 100 - (int)(project.rightOutLevel * 100);
            this.Invalidate();
        }

        public void quiet()
        {
            leftMeter.setLevel(0.0f);
            rightMeter.setLevel(0.0f);
        }


//- event handlers ------------------------------------------------------------

        private void vsbLeftFader_Scroll(object sender, ScrollEventArgs e)
        {
            int val = 100 - vsbLeftFader.Value;
            leftLevel = val / 100.0f;
            project.setLeftOutLevel(leftLevel);
            if (fadersAreLocked)
            {
                project.setRightOutLevel(leftLevel);
                vsbRightFader.Value = vsbLeftFader.Value;
                vsbRightFader.Invalidate();
            }
        }

        private void vsbRightFader_Scroll(object sender, ScrollEventArgs e)
        {
            int val = 100 - vsbRightFader.Value;
            rightLevel = val / 100.0f;
            project.setRightOutLevel(rightLevel);
            if (fadersAreLocked)
            {
                project.setLeftOutLevel(rightLevel);
                vsbLeftFader.Value = vsbRightFader.Value;
                vsbLeftFader.Invalidate();
            }
        }

        private void chkLock_CheckedChanged(object sender, EventArgs e)
        {
            fadersAreLocked = chkLock.Checked;
        }

        public void timerTick()
        {
            float leftLevel = project.signalsA.getLeftLevel();
            leftMeter.setLevel(leftLevel);            
            float rightLevel = project.signalsA.getRightLevel();
            rightMeter.setLevel(rightLevel);            
        }

//-----------------------------------------------------------------------------

        String[] metergrad = { "+3", "0", "-3", "-6", "-9", "-12", "-15"};
        int[] metergradofs = { 0, 8, -4, 0, 0, -6, 0 };

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;


            //meter gradients
            int gradlinex = 32;
            int gradliney = 40;
            Point gradNumPoint = new Point(14, 32);
            for (int i = 0; i < 7; i++)
            {
                String gradNum = metergrad[i];
                gradNumPoint.Offset(metergradofs[i], 0);
                g.DrawString(gradNum, SystemFonts.DialogFont, Brushes.White, gradNumPoint);
                gradNumPoint.Offset(0, 24);
                g.DrawLine(Pens.White, gradlinex, gradliney + (i * 24), gradlinex + 8, gradliney + (i * 24));
                g.DrawLine(Pens.White, gradlinex + 37, gradliney + (i * 24), gradlinex + 45, gradliney + (i * 24));
            }

            //fader gradients
            gradlinex = 32;
            gradliney = 237;
            gradNumPoint = new Point(14, 229);
            for (int i = 0; i < 11; i++)
            {
                String gradNum = (10 - i).ToString("D");
                g.DrawString(gradNum, SystemFonts.DialogFont, Brushes.White, gradNumPoint);
                gradNumPoint.Offset(0, 15);
                g.DrawLine(Pens.White, gradlinex, gradliney + (i * 15), gradlinex + 8, gradliney + (i * 15));
                g.DrawLine(Pens.White, gradlinex + 38, gradliney + (i * 15), gradlinex + 46, gradliney + (i * 15));
                if (i == 0) gradNumPoint.Offset(6, 0);
            }
        }
    }
}
