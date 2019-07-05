/* ----------------------------------------------------------------------------
Transonic Widget Library
Copyright (C) 1996-2019  George E Greaney

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
using Signals.UI;

namespace Transonic.Widget
{
    public class TrackRuler : UserControl
    {
        TrackPanel trackPanel;
        public int zeroPos;         //actual point on screen we start drawing tick marks from
        public int startPos;        //pixel offset to start drawing ruler at
        public int zoomFactor;      //num pixels per sec        
        public int playPos;         //cur playing pos in pixels from left end of ruler

        public TrackRuler(TrackPanel _trackPanel)
        {
            trackPanel = _trackPanel;

            InitializeComponent();
            this.Height = 20;
            this.BackColor = Color.Snow;
            zoomFactor = TrackPanel.ZOOMFACTOR;
            zeroPos = TrackView.TRACKHEADERWIDTH + 1;
            startPos = 0;
            playPos = 0;
        }

        public String getTickTimeString(int time) 
        {
            int secVal = time % 60;
            int minVal = time / 60;
            String tickStr = minVal.ToString("D1") + ":" + secVal.ToString("D2");
            return tickStr;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;            
            g.SmoothingMode = SmoothingMode.AntiAlias;

            if (trackPanel.trackWidth > 0)
            {
                g.DrawLine(Pens.Black, zeroPos, 15, zeroPos, 20);     //left most tick

                double startTime = ((float)startPos) / zoomFactor;      //pixels -> sec - starting time of ruler
                int time = (int)Math.Ceiling(startTime / 5.0) * 5;      //round first tick time to next mult of 5 secs
                double diff = time - startTime;                         //time from rule start to first tick
                int pos = zeroPos + (int)(diff * zoomFactor);           //pos of first tick

                int ticks = (trackPanel.trackWidth) / (5 * zoomFactor);      //one tick every 5 seconds - for now, may depend on zoom factor later
                for (int tick = 0; tick <= ticks; tick++)
                {
                    g.DrawLine(Pens.Black, pos, 15, pos, 20);
                    if (time % 2 == 0)
                        g.DrawString(getTickTimeString(time), SystemFonts.DialogFont, Brushes.Black, pos - 10, 2);
                    pos += (5 * zoomFactor);
                    time += 5;
                }

                //show playing pos marker if it not hidden on left side of ruler
                if (playPos >= startPos)
                {
                    int playlinepos = zeroPos + (playPos - startPos);
                    g.DrawLine(Pens.Red, playlinepos, 0, playlinepos, 20);     //playing pos marker
                }
            }
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // TrackRuler
            // 
            this.DoubleBuffered = true;
            this.Name = "TrackRuler";
            this.ResumeLayout(false);
        }
    }
}
