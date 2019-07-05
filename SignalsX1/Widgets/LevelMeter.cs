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
    class LevelMeter : UserControl
    {
        MixerMaster mixmaster;
        float level;

        public LevelMeter(MixerMaster _mixmaster)
        {
            mixmaster = _mixmaster;
            InitializeComponent();
            level = 0.0f;            
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // LevelMeter
            // 
            this.BackColor = System.Drawing.Color.LightGray;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DoubleBuffered = true;
            this.Enabled = false;
            this.Name = "LevelMeter";
            this.Size = new System.Drawing.Size(22, 161);
            this.ResumeLayout(false);

        }

        public void setLevel(float _level)
        {
            level = (float)((20.0 * Math.Log10(_level) / 1.5) + 10.0);
            if (level > 13.0f) level = 13.0f;
            if (level < 0.0f) level = 0.0f;
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            Brush levelBrush;

            //whole blocks
            Rectangle blockRect = new Rectangle(2, 146, 14, 10);
            Point blockofs = new Point(0, -12);
            int blocks = (int)level;
            for (int i = 0; i < blocks; i++)
            {
                levelBrush = (i > 11) ? Brushes.Red : (i > 9) ? Brushes.Yellow : Brushes.Green;
                g.FillRectangle(levelBrush, blockRect);
                blockRect.Offset(blockofs);
            }

            //partial block
            int blockHeight = ((int)(level * 10.0f)) % 10;
            blockRect.Height = blockHeight;
            blockRect.Offset(new Point(0, (10 - blockHeight)));
            levelBrush = (blocks > 11) ? Brushes.Red : (blocks > 9) ? Brushes.Yellow : Brushes.Green;
            g.FillRectangle(levelBrush, blockRect);
        }
    }
}
