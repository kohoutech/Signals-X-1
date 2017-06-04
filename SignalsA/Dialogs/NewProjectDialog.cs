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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Signals.Dialogs
{
    public partial class newProjectDialog : Form
    {
        public String projectName;
        public int sampleRate;
        public int duration;

        List<int> sampleRates = new List<int>{ 44100, 22050, 11025, 8000};

        public newProjectDialog()
        {
            InitializeComponent();
            cbxSampleRate.DataSource = sampleRates;

            projectName = "";
            sampleRate = 44100;
            duration = 180;             //3 minutes @ 44.1kHz
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            projectName = txtProjectName.Text;
            sampleRate = sampleRates[cbxSampleRate.SelectedIndex];
            duration = Convert.ToInt32(txtDuration.Text);
            DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            projectName = "";
        }
    }
}
