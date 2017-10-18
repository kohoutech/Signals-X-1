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
    public partial class TransportSettingsDialog : Form
    {
        public int outputDeviceNum;
        public int inputLatency;
        public int outputLatency;

        public TransportSettingsDialog(List<String> outDeviceList)
        {
            InitializeComponent();
            cbxOutputDevice.DataSource = outDeviceList;

            outputDeviceNum = -1;            
            inputLatency = 50;
            outputLatency = 50;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            outputDeviceNum = cbxOutputDevice.SelectedIndex;
            inputLatency = hsbInputLatency.Value;
            outputLatency = hsbOutputLatency.Value;
            DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void hsbInputLatency_Scroll(object sender, ScrollEventArgs e)
        {
            lblInputLatency.Text = "Input Latency - " + hsbInputLatency.Value + " ms";
        }

        private void hsbOutputLatency_Scroll(object sender, ScrollEventArgs e)
        {
            lblOutputLatency.Text = "Output Latency - " + hsbOutputLatency.Value + " ms";
        }
    }
}
