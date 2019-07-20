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
    public class Knob : UserControl
    {
        Rectangle knobRect;

        public double value;

        public Knob()
        {
            knobRect = new Rectangle(0, 0, 100, 100);
            value = 0.0;
            
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            knobRect = new Rectangle(0, 0, this.Width - 2, this.Height - 2);
        }

        //- mouse events --------------------------------------------------------------

        protected override void OnMouseDown(MouseEventArgs e)
        {
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
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



            Bitmap knobBitmap = new Bitmap(knobRect.Height-4, knobRect.Width-4);
            Graphics knobg = Graphics.FromImage(knobBitmap);
            knobg.SmoothingMode = SmoothingMode.AntiAlias;

            Pen outlinePen = new Pen(Color.Black, 2.0f);
            knobg.DrawEllipse(outlinePen, 0, 0, knobBitmap.Width, knobBitmap.Height);
            knobg.FillEllipse(Brushes.Gray, 0, 0, knobBitmap.Width, knobBitmap.Height);

            double valX = knobBitmap.Width / 2;
            double valY = knobBitmap.Height / 2;
            knobg.FillEllipse(Brushes.Red, 2, (float)valY - 2, 5, 5);

            knobg.TranslateTransform((float)knobBitmap.Width / 2, (float)knobBitmap.Height / 2);
            knobg.RotateTransform(120);
            knobg.TranslateTransform(-(float)knobBitmap.Width / 2, -(float)knobBitmap.Height / 2);
            g.DrawImage(knobBitmap, new Point(1, 1));
        }
    }
}
