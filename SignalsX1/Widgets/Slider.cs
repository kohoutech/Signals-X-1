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

namespace Transonic.Widget
{
    class Slider : UserControl
    {
        int TRACKWIDTH = 20;
        int THUMBHEIGHT = 25;

        Rectangle trackRect;
        Rectangle thumbRect;

        double value;
        bool dragging;
        int dragOrgY;
        int thumbOrgY;

        public Slider()
        {
            this.DoubleBuffered = true;

            value = 0.0;
            dragging = false;
            trackRect = new Rectangle(0, 0, TRACKWIDTH, 100);
            thumbRect = new Rectangle(0, 0, 80, THUMBHEIGHT);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            trackRect.Location = new Point((this.Width - TRACKWIDTH)/2, 0);
            trackRect.Size = new Size(TRACKWIDTH, this.Height);
            thumbRect.Size = new Size(this.Width, THUMBHEIGHT);
        }

        //- mouse events --------------------------------------------------------------

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (thumbRect.Contains(e.X, e.Y))
            {
                dragOrgY = e.Y;
                thumbOrgY = thumbRect.Y;
                dragging = true;
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (dragging)
            {
                int deltaY = e.Y - dragOrgY;
                int thumbY = thumbOrgY + deltaY;
                if (thumbY < 0) thumbY = 0;
                if (thumbY > trackRect.Height) thumbY = trackRect.Height;
                thumbRect.Location = new Point(0, thumbY);
                Invalidate();
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            dragging = false;
        }

        //- key events ----------------------------------------------------------------

        protected override void OnKeyDown(KeyEventArgs e)
        {        
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {        
        }
        
//- painting ------------------------------------------------------------------

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            //paint track
            g.FillRectangle(Brushes.Black, trackRect); 

            //paint thumb
            g.FillRectangle(Brushes.Blue, thumbRect);             
        }
    }

}
