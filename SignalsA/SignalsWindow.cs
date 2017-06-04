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
using Signals.Engine;
using Signals.UI;
using Signals.Widgets;
using Signals.Dialogs;

namespace Signals
{
    public partial class SignalsWindow : Form
    {
        //model
        public X1Project currentProject;
        public Signals signalsA;

        //view
        public ControlPanel controlPanel;
        public TrackPanel trackPanel;
        public MixerWindow mixerWindow;

        bool shuttingdown;
        bool isPaused;
        int playSpeed;

        //cons
        public SignalsWindow()
        {
            InitializeComponent();
            signalsA = new Signals(this);
            currentProject = null;

            //control panel
            controlPanel = new ControlPanel(this);
            controlPanel.Location = new Point(0, SignalsMenu.Height);
            controlPanel.Width = this.ClientSize.Width;
            this.Controls.Add(controlPanel);

            //track panel
            trackPanel = new TrackPanel(this);
            trackPanel.Location = new Point(0, controlPanel.Bottom);
            trackPanel.Size = new Size(this.ClientSize.Width, SignalsStatus.Top - controlPanel.Bottom);
            this.Controls.Add(trackPanel);

            //mixer window
            mixerWindow = new MixerWindow(this);
            mixerWindow.FormClosing += new FormClosingEventHandler(MixerWindow_FormClosing);
            shuttingdown = false;

            isPaused = false;
            playSpeed = 0;
        }

        private void SignalsWindow_Resize(object sender, EventArgs e)
        {
            controlPanel.Width = this.ClientSize.Width;
            trackPanel.Size = new Size(this.ClientSize.Width, (SignalsStatus.Top - controlPanel.Bottom));
        }

        private void SignalsWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            if (closeCurrentProject())
            {
                signalsA.shutDown();
                shuttingdown = true;
                mixerWindow.Close();
                e.Cancel = false;
            }
        }

        //keep mixer window from closing when user clicks on close button - unless we are shutting down for good
        private void MixerWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!shuttingdown)
            {
                mixerWindow.Hide();
                e.Cancel = true;
            }
        }

//- actions -------------------------------------------------------------------

        //project actions
        public void setCurrentProject(X1Project project)
        {
            currentProject = project;
            this.Text = "Signals X-1 [" + currentProject.projectName + "]";
            trackPanel.setProject(project);
            mixerWindow.setProject(project);
            controlPanel.setProject(project);
            enableWithProject(true);
            if (currentProject.tracks.Count > 0)
                enableWithTracks(true);
        }

        public bool closeCurrentProject()
        {
            //save current project if we have one
            if (currentProject != null)
            {
                if (currentProject.isChanged)
                {
                    String msg = "Save changes to project " + currentProject.projectName + "?";
                    DialogResult result = MessageBox.Show(msg, "Not so fast...", MessageBoxButtons.YesNoCancel);
                    if (result == DialogResult.Cancel) return false;
                    if (result == DialogResult.Yes)
                    {
                        //save current project, but if they cancel the saving then cancel the closing as well
                        if (!saveCurrentProject(false)) return false;
                    }
                }

                currentProject.close();
            }

            trackPanel.clearProject(currentProject);
            mixerWindow.clearProject(currentProject);
            controlPanel.clearProject(currentProject);
            enableWithTracks(false);
            enableWithProject(false);
            this.Text = "Signals X-1 [none]";
            currentProject = null;
            return true;
        }

        public bool saveCurrentProject(bool newName)
        {
            if (newName || currentProject.filename == null)
            {
                //call get save project filename dialog box
                String filename = "";
                saveFileDialog.InitialDirectory = Application.StartupPath;
                saveFileDialog.DefaultExt = "*.sx1";
                saveFileDialog.Filter = "Signals X-1 project files|*.sx1|All files|*.*";
                saveFileDialog.ShowDialog();
                filename = saveFileDialog.FileName;
                if (filename.Length == 0) return false;

                //add default extention if filename doesn't have one
                if (!filename.Contains('.'))
                    filename = filename + ".sx1";
                currentProject.filename = filename;
            }
            currentProject.save();
            String msg = "Project " + currentProject.projectName + " has been saved as\n " + currentProject.filename;
            MessageBox.Show(msg, "Saved");
            return true;
        }

        //transport actions
        public void playTransport()
        {
            signalsA.playTransport();
            masterTimer.Start();
        }

        public void pauseTransport(bool _isPaused)
        {
            signalsA.pauseTransport();
            if (_isPaused)
            {
                masterTimer.Stop();
            }
            else
            {
                masterTimer.Start();
            }
        }

        public void stopTransport()
        {
            signalsA.stopTransport();
            masterTimer.Stop();
            controlPanel.timerTick(0);
            trackPanel.timerTick(0);
            mixerWindow.stop();
        }

        public void fastForwardTransport()
        {
            //doesn't work for now!
            //playSpeed++;
            //if (playSpeed > 3) playSpeed = 3;
            //signalsA.fastForwardTransport(playSpeed);
        }

        public void recordTransport()
        {
            signalsA.recordTransport();
            masterTimer.Start();
        }        


//- file events ---------------------------------------------------------------

        private void newFileMenuItem_Click(object sender, EventArgs e)
        {
            if (!closeCurrentProject()) return;

            //call get import filename dialog box
            newProjectDialog newDialog = new newProjectDialog();
            newDialog.ShowDialog();
            if (newDialog.DialogResult == DialogResult.Cancel) return;

            String projectName = newDialog.projectName;
            int sampleRate = newDialog.sampleRate;
            int duration = newDialog.duration;

            X1Project project = new X1Project(this, projectName, sampleRate, duration);
            setCurrentProject(project);
        }

        private void openFileMenuItem_Click(object sender, EventArgs e)
        {
            if (!closeCurrentProject()) return;

            //call get new project filename dialog box
            String filename = "";
            openFileDialog.InitialDirectory = Application.StartupPath;
            openFileDialog.DefaultExt = "*.sx1";
            openFileDialog.Filter = "Signals X-1 project files|*.sx1|All files|*.*";                
            openFileDialog.ShowDialog();
            filename = openFileDialog.FileName;
            if (filename.Length == 0) return;

            X1Project project = X1Project.open(this, filename);
            setCurrentProject(project);
        }

        private void closeFileMenuItem_Click(object sender, EventArgs e)
        {
            closeCurrentProject();
        }

        private void saveFileMenuItem_Click(object sender, EventArgs e)
        {
            saveCurrentProject(false);
        }

        private void saveAsFileMenuItem_Click(object sender, EventArgs e)
        {
            saveCurrentProject(true);
        }

        private void exportFileMenuItem_Click(object sender, EventArgs e)
        {
            //call get save project filename dialog box
            String filename = "";
            saveFileDialog.InitialDirectory = Application.StartupPath;
            saveFileDialog.DefaultExt = "*.wav";
            saveFileDialog.Filter = "Wav files|*.wav|All files|*.*";
            saveFileDialog.ShowDialog();
            filename = saveFileDialog.FileName;
            if (filename.Length == 0) return;

            signalsA.exportToWaveFile(filename);
            String msg = "project " + currentProject.projectName + " successfully exported to " + filename;
            MessageBox.Show(msg, "Complete!");
        }

        private void exitFileMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

//- edit events ---------------------------------------------------------------

        private void undoEditMenuItem_Click(object sender, EventArgs e)
        {
            String msg = "Undo isn't implemented yet\nWe're working on even as you read this!\ncoming in the next major version";
            MessageBox.Show(msg, "Ooops!");
        }

        private void redoEditMenuItem_Click(object sender, EventArgs e)
        {
            String msg = "Redo isn't implemented yet\nWe're working on even as you read this!\ncoming in the next major version";
            MessageBox.Show(msg, "Ooops!");
        }

        private void cutEditMenuItem_Click(object sender, EventArgs e)
        {
            String msg = "Editing isn't implemented yet\nWe're working on even as you read this!\ncoming in the next major version";
            MessageBox.Show(msg, "Ooops!");
        }

        private void copyEditMenuItem_Click(object sender, EventArgs e)
        {
            String msg = "Editing isn't implemented yet\nWe're working on even as you read this!\ncoming in the next major version";
            MessageBox.Show(msg, "Ooops!");
        }

        private void pasteEditMenuItem_Click(object sender, EventArgs e)
        {
            String msg = "Editing isn't implemented yet\nWe're working on even as you read this!\ncoming in the next major version";
            MessageBox.Show(msg, "Ooops!");
        }

        private void selectAllEditMenuItem_Click(object sender, EventArgs e)
        {
            String msg = "Editing isn't implemented yet\nWe're working on even as you read this!\ncoming in the next major version";
            MessageBox.Show(msg, "Ooops!");
        }

//-view events ----------------------------------------------------------------

        private void mixerViewMenuItem_Click(object sender, EventArgs e)
        {
            mixerWindow.Show();
        }

//- track events --------------------------------------------------------------

        private void newTrackMenuItem_Click(object sender, EventArgs e)
        {
            currentProject.newTrack();
            enableWithTracks(true);
        }

        private void copyTrackMenuItem_Click(object sender, EventArgs e)
        {
            String msg = "Copying tracks isn't implemented yet\nIt's the last thing we think about before going to bed at night - promise!";
            MessageBox.Show(msg, "Wait for it...");
        }

        private void deleteTrackMenuItem_Click(object sender, EventArgs e)
        {
            String msg = "Deleting mutiple tracks at once isn't implemented yet\n " + 
                "To delete a single track, click on the red X in the upper left hand corner of the track view";
            MessageBox.Show(msg, "Wait for it...");
        }

        private void importTrackMenuItem_Click(object sender, EventArgs e)
        {
            //String fname = @"O:\music\sounds\needed.wav";
            //String fname = @"..\Riding On A Wave.wav";
            //String fname = @"..\Unfinished Song.wav";

            //call get import filename dialog box
            String filename = "";
            openFileDialog.InitialDirectory = Application.StartupPath;
            openFileDialog.DefaultExt = "*.wav";
            openFileDialog.Filter = "Wav files|*.wav|All files|*.*";
            openFileDialog.ShowDialog();
            filename = openFileDialog.FileName;
            if (filename.Length == 0) return;

            currentProject.importTracksFromFile(filename);
            enableWithTracks(true);
            String msg = "successfully imported tracks from " + filename;
            MessageBox.Show(msg, "Success!");

        }

//-transport events -----------------------------------------------------------

        private void settingsTransportMenuItem_Click(object sender, EventArgs e)
        {
            //call get import filename dialog box
            TransportSettingsDialog transDialog = new TransportSettingsDialog(signalsA.getOutputDeviceList());
            transDialog.ShowDialog();
            if (transDialog.DialogResult == DialogResult.Cancel) return;

            //not wired up yet
            int outDeviceNum = transDialog.outputDeviceNum;
            int inputLatency = transDialog.inputLatency;
            int outputLatency = transDialog.outputLatency;
        }

//- help events ---------------------------------------------------------------

        private void aboutHelpMenuItem_Click(object sender, EventArgs e)
        {
            String msg = "Signals X-1\nversion 1.0.0\n" + "\xA9 Transonic Software 2005-2017\n" + "http://transonic.kohoutech.com";
            MessageBox.Show(msg, "About");
        }

//- updating ------------------------------------------------------------------

        private void masterTimer_Tick(object sender, EventArgs e)
        {
            int curPos = signalsA.getCurrentPos();
            int msTime = (int)((curPos * 1000.0f) / currentProject.sampleRate);  

            controlPanel.timerTick(msTime);
            trackPanel.timerTick(msTime);
            mixerWindow.mixmaster.timerTick();
        }

        public void setCurrentTime(int msTime)
        {
            int curPos = (int)((msTime / 1000.0f) * currentProject.sampleRate);
            signalsA.setCurrentPos(curPos);

            controlPanel.timerTick(msTime);
            trackPanel.timerTick(msTime);
        }

        public void setTooltip(Control child, String tipText)
        {
            signalsToolTip.SetToolTip(child, tipText);
        }

        //enable controls that only are valid with a current project
        public void enableWithProject(bool on) {

            closeFileMenuItem.Enabled = on;
            saveFileMenuItem.Enabled = on;
            saveAsFileMenuItem.Enabled = on;
            viewMenuItem.Enabled = on;
            trackMenuItem.Enabled = on;
            newTrackMenuItem.Enabled = on;
            importTracksMenuItem.Enabled = on;            
            transportMenuItem.Enabled = on;
        }

        //enable controls that only are valid when the current project has tracks
        public void enableWithTracks(bool on)
        {
            exportFileMenuItem.Enabled = on;
            editMenuItem.Enabled = on;
            deleteTrackMenuItem.Enabled = on;
            copyTrackMenuItem.Enabled = on;
            controlPanel.enableTransport(on);
        }
    }
}
