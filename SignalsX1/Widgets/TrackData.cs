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
using System.Runtime.InteropServices;

using Signals.UI;
using Transonic.Wave;

namespace Signals.Widgets
{
    public class TrackData : UserControl
    {

//-----------------------------------------------------------------------------

        public Waverly waverly;            //shortcut to signals to cut down pinvoke time
        public TrackView trackView;
        public int trackNum;
        public int samplePos;        //sample that the view starts at on the left
        public int zoomFactor;      //num pixels per sec        

        public TrackData(TrackView _trackView)
        {
            trackView = _trackView;
            waverly = trackView.trackPanel.signalsWindow.waverly;
            trackNum = trackView.track.number;
            zoomFactor = TrackPanel.ZOOMFACTOR;
            samplePos = 0;

            this.Size = new System.Drawing.Size(300, 128);
            this.BackColor = Color.LightGray;
            this.Paint += paintTrackData;
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            //does nothing, just overridden to prevent app from painting background & cause flicker
        }

        private void paintTrackData(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            IntPtr hdc = g.GetHdc();
            waverly.paintTrackData(trackNum, hdc, this.Width, samplePos);
            g.ReleaseHdc(hdc);
        }

    }
}
