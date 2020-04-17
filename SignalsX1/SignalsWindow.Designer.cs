namespace Signals
{
    partial class SignalsWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SignalsWindow));
            this.SignalsStatus = new System.Windows.Forms.StatusStrip();
            this.SignalsMenu = new System.Windows.Forms.MenuStrip();
            this.fileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newFileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeFileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveFileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsFileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fileMenuSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exportFileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fileMenuSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.exitFileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.undoEditMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.redoEditMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.EditMenuItemSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.cutEditMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyEditMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteEditMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.EditMenuItemSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.selectEditMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mixerViewMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.trackMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newTrackMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteTrackMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyTrackMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importTracksMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.transportMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsTransportMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutHelpMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.masterTimer = new System.Windows.Forms.Timer(this.components);
            this.signalsToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.SignalsMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // SignalsStatus
            // 
            this.SignalsStatus.Location = new System.Drawing.Point(0, 339);
            this.SignalsStatus.Name = "SignalsStatus";
            this.SignalsStatus.Size = new System.Drawing.Size(574, 22);
            this.SignalsStatus.TabIndex = 0;
            this.SignalsStatus.Text = "statusStrip1";
            // 
            // SignalsMenu
            // 
            this.SignalsMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileMenuItem,
            this.editMenuItem,
            this.viewMenuItem,
            this.trackMenuItem,
            this.transportMenuItem,
            this.helpMenuItem});
            this.SignalsMenu.Location = new System.Drawing.Point(0, 0);
            this.SignalsMenu.Name = "SignalsMenu";
            this.SignalsMenu.Size = new System.Drawing.Size(574, 24);
            this.SignalsMenu.TabIndex = 1;
            this.SignalsMenu.Text = "menuStrip1";
            // 
            // fileMenuItem
            // 
            this.fileMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newFileMenuItem,
            this.openFileMenuItem,
            this.closeFileMenuItem,
            this.saveFileMenuItem,
            this.saveAsFileMenuItem,
            this.fileMenuSeparator1,
            this.exportFileMenuItem,
            this.fileMenuSeparator2,
            this.exitFileMenuItem});
            this.fileMenuItem.Name = "fileMenuItem";
            this.fileMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileMenuItem.Text = "&File";
            this.fileMenuItem.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            // 
            // newFileMenuItem
            // 
            this.newFileMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.newFileMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("newFileMenuItem.Image")));
            this.newFileMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.newFileMenuItem.Name = "newFileMenuItem";
            this.newFileMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.newFileMenuItem.Size = new System.Drawing.Size(186, 22);
            this.newFileMenuItem.Text = "&New Project";
            this.newFileMenuItem.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.newFileMenuItem.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.newFileMenuItem.Click += new System.EventHandler(this.newFileMenuItem_Click);
            // 
            // openFileMenuItem
            // 
            this.openFileMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.openFileMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("openFileMenuItem.Image")));
            this.openFileMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openFileMenuItem.Name = "openFileMenuItem";
            this.openFileMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openFileMenuItem.Size = new System.Drawing.Size(186, 22);
            this.openFileMenuItem.Text = "&Open Project";
            this.openFileMenuItem.Click += new System.EventHandler(this.openFileMenuItem_Click);
            // 
            // closeFileMenuItem
            // 
            this.closeFileMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.closeFileMenuItem.Enabled = false;
            this.closeFileMenuItem.Name = "closeFileMenuItem";
            this.closeFileMenuItem.Size = new System.Drawing.Size(186, 22);
            this.closeFileMenuItem.Text = "&Close Project";
            this.closeFileMenuItem.Click += new System.EventHandler(this.closeFileMenuItem_Click);
            // 
            // saveFileMenuItem
            // 
            this.saveFileMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.saveFileMenuItem.Enabled = false;
            this.saveFileMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("saveFileMenuItem.Image")));
            this.saveFileMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveFileMenuItem.Name = "saveFileMenuItem";
            this.saveFileMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveFileMenuItem.Size = new System.Drawing.Size(186, 22);
            this.saveFileMenuItem.Text = "&Save Project";
            this.saveFileMenuItem.Click += new System.EventHandler(this.saveFileMenuItem_Click);
            // 
            // saveAsFileMenuItem
            // 
            this.saveAsFileMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.saveAsFileMenuItem.Enabled = false;
            this.saveAsFileMenuItem.Name = "saveAsFileMenuItem";
            this.saveAsFileMenuItem.Size = new System.Drawing.Size(186, 22);
            this.saveAsFileMenuItem.Text = "Save Project &As";
            this.saveAsFileMenuItem.Click += new System.EventHandler(this.saveAsFileMenuItem_Click);
            // 
            // fileMenuSeparator1
            // 
            this.fileMenuSeparator1.Name = "fileMenuSeparator1";
            this.fileMenuSeparator1.Size = new System.Drawing.Size(183, 6);
            // 
            // exportFileMenuItem
            // 
            this.exportFileMenuItem.Enabled = false;
            this.exportFileMenuItem.Name = "exportFileMenuItem";
            this.exportFileMenuItem.Size = new System.Drawing.Size(186, 22);
            this.exportFileMenuItem.Text = "&Export to Audio File";
            this.exportFileMenuItem.Click += new System.EventHandler(this.exportFileMenuItem_Click);
            // 
            // fileMenuSeparator2
            // 
            this.fileMenuSeparator2.Name = "fileMenuSeparator2";
            this.fileMenuSeparator2.Size = new System.Drawing.Size(183, 6);
            // 
            // exitFileMenuItem
            // 
            this.exitFileMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.exitFileMenuItem.Name = "exitFileMenuItem";
            this.exitFileMenuItem.Size = new System.Drawing.Size(186, 22);
            this.exitFileMenuItem.Text = "E&xit";
            this.exitFileMenuItem.Click += new System.EventHandler(this.exitFileMenuItem_Click);
            // 
            // editMenuItem
            // 
            this.editMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.undoEditMenuItem,
            this.redoEditMenuItem,
            this.EditMenuItemSeparator1,
            this.cutEditMenuItem,
            this.copyEditMenuItem,
            this.pasteEditMenuItem,
            this.EditMenuItemSeparator2,
            this.selectEditMenuItem});
            this.editMenuItem.Enabled = false;
            this.editMenuItem.Name = "editMenuItem";
            this.editMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editMenuItem.Text = "&Edit";
            // 
            // undoEditMenuItem
            // 
            this.undoEditMenuItem.Name = "undoEditMenuItem";
            this.undoEditMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this.undoEditMenuItem.Size = new System.Drawing.Size(174, 22);
            this.undoEditMenuItem.Text = "&Undo";
            this.undoEditMenuItem.Click += new System.EventHandler(this.undoEditMenuItem_Click);
            // 
            // redoEditMenuItem
            // 
            this.redoEditMenuItem.Name = "redoEditMenuItem";
            this.redoEditMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.Z)));
            this.redoEditMenuItem.Size = new System.Drawing.Size(174, 22);
            this.redoEditMenuItem.Text = "&Redo";
            this.redoEditMenuItem.Click += new System.EventHandler(this.redoEditMenuItem_Click);
            // 
            // EditMenuItemSeparator1
            // 
            this.EditMenuItemSeparator1.Name = "EditMenuItemSeparator1";
            this.EditMenuItemSeparator1.Size = new System.Drawing.Size(171, 6);
            // 
            // cutEditMenuItem
            // 
            this.cutEditMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.cutEditMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("cutEditMenuItem.Image")));
            this.cutEditMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cutEditMenuItem.Name = "cutEditMenuItem";
            this.cutEditMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.cutEditMenuItem.Size = new System.Drawing.Size(174, 22);
            this.cutEditMenuItem.Text = "Cu&t";
            this.cutEditMenuItem.Click += new System.EventHandler(this.cutEditMenuItem_Click);
            // 
            // copyEditMenuItem
            // 
            this.copyEditMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.copyEditMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("copyEditMenuItem.Image")));
            this.copyEditMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.copyEditMenuItem.Name = "copyEditMenuItem";
            this.copyEditMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.copyEditMenuItem.Size = new System.Drawing.Size(174, 22);
            this.copyEditMenuItem.Text = "&Copy";
            this.copyEditMenuItem.Click += new System.EventHandler(this.copyEditMenuItem_Click);
            // 
            // pasteEditMenuItem
            // 
            this.pasteEditMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.pasteEditMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("pasteEditMenuItem.Image")));
            this.pasteEditMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.pasteEditMenuItem.Name = "pasteEditMenuItem";
            this.pasteEditMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.pasteEditMenuItem.Size = new System.Drawing.Size(174, 22);
            this.pasteEditMenuItem.Text = "&Paste";
            this.pasteEditMenuItem.Click += new System.EventHandler(this.pasteEditMenuItem_Click);
            // 
            // EditMenuItemSeparator2
            // 
            this.EditMenuItemSeparator2.Name = "EditMenuItemSeparator2";
            this.EditMenuItemSeparator2.Size = new System.Drawing.Size(171, 6);
            // 
            // selectEditMenuItem
            // 
            this.selectEditMenuItem.Name = "selectEditMenuItem";
            this.selectEditMenuItem.Size = new System.Drawing.Size(174, 22);
            this.selectEditMenuItem.Text = "Select &All";
            this.selectEditMenuItem.Click += new System.EventHandler(this.selectAllEditMenuItem_Click);
            // 
            // viewMenuItem
            // 
            this.viewMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mixerViewMenuItem});
            this.viewMenuItem.Enabled = false;
            this.viewMenuItem.Name = "viewMenuItem";
            this.viewMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewMenuItem.Text = "&View";
            // 
            // mixerViewMenuItem
            // 
            this.mixerViewMenuItem.Name = "mixerViewMenuItem";
            this.mixerViewMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.M)));
            this.mixerViewMenuItem.Size = new System.Drawing.Size(177, 22);
            this.mixerViewMenuItem.Text = "Mi&xer View";
            this.mixerViewMenuItem.Click += new System.EventHandler(this.mixerViewMenuItem_Click);
            // 
            // trackMenuItem
            // 
            this.trackMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newTrackMenuItem,
            this.deleteTrackMenuItem,
            this.copyTrackMenuItem,
            this.importTracksMenuItem});
            this.trackMenuItem.Enabled = false;
            this.trackMenuItem.Name = "trackMenuItem";
            this.trackMenuItem.Size = new System.Drawing.Size(46, 20);
            this.trackMenuItem.Text = "&Track";
            // 
            // newTrackMenuItem
            // 
            this.newTrackMenuItem.Name = "newTrackMenuItem";
            this.newTrackMenuItem.Size = new System.Drawing.Size(145, 22);
            this.newTrackMenuItem.Text = "&New Track";
            this.newTrackMenuItem.Click += new System.EventHandler(this.newTrackMenuItem_Click);
            // 
            // deleteTrackMenuItem
            // 
            this.deleteTrackMenuItem.Enabled = false;
            this.deleteTrackMenuItem.Name = "deleteTrackMenuItem";
            this.deleteTrackMenuItem.Size = new System.Drawing.Size(145, 22);
            this.deleteTrackMenuItem.Text = "&Delete Track";
            this.deleteTrackMenuItem.Click += new System.EventHandler(this.deleteTrackMenuItem_Click);
            // 
            // copyTrackMenuItem
            // 
            this.copyTrackMenuItem.Enabled = false;
            this.copyTrackMenuItem.Name = "copyTrackMenuItem";
            this.copyTrackMenuItem.Size = new System.Drawing.Size(145, 22);
            this.copyTrackMenuItem.Text = "&Copy Track";
            this.copyTrackMenuItem.Click += new System.EventHandler(this.copyTrackMenuItem_Click);
            // 
            // importTracksMenuItem
            // 
            this.importTracksMenuItem.Name = "importTracksMenuItem";
            this.importTracksMenuItem.Size = new System.Drawing.Size(145, 22);
            this.importTracksMenuItem.Text = "&Import Tracks";
            this.importTracksMenuItem.Click += new System.EventHandler(this.importTrackMenuItem_Click);
            // 
            // transportMenuItem
            // 
            this.transportMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingsTransportMenuItem});
            this.transportMenuItem.Enabled = false;
            this.transportMenuItem.Name = "transportMenuItem";
            this.transportMenuItem.Size = new System.Drawing.Size(68, 20);
            this.transportMenuItem.Text = "Trans&port";
            // 
            // settingsTransportMenuItem
            // 
            this.settingsTransportMenuItem.Name = "settingsTransportMenuItem";
            this.settingsTransportMenuItem.Size = new System.Drawing.Size(168, 22);
            this.settingsTransportMenuItem.Text = "&Transport Settings";
            this.settingsTransportMenuItem.Click += new System.EventHandler(this.settingsTransportMenuItem_Click);
            // 
            // helpMenuItem
            // 
            this.helpMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutHelpMenuItem});
            this.helpMenuItem.Name = "helpMenuItem";
            this.helpMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpMenuItem.Text = "&Help";
            // 
            // aboutHelpMenuItem
            // 
            this.aboutHelpMenuItem.Name = "aboutHelpMenuItem";
            this.aboutHelpMenuItem.Size = new System.Drawing.Size(116, 22);
            this.aboutHelpMenuItem.Text = "&About...";
            this.aboutHelpMenuItem.Click += new System.EventHandler(this.aboutHelpMenuItem_Click);
            // 
            // masterTimer
            // 
            this.masterTimer.Interval = 50;
            this.masterTimer.Tick += new System.EventHandler(this.masterTimer_Tick);
            // 
            // signalsToolTip
            // 
            this.signalsToolTip.BackColor = System.Drawing.Color.Yellow;
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.DefaultExt = "sx1";
            // 
            // SignalsWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(574, 361);
            this.Controls.Add(this.SignalsStatus);
            this.Controls.Add(this.SignalsMenu);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.SignalsMenu;
            this.MinimumSize = new System.Drawing.Size(590, 160);
            this.Name = "SignalsWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Signals X-1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SignalsWindow_FormClosing);
            this.Resize += new System.EventHandler(this.SignalsWindow_Resize);
            this.SignalsMenu.ResumeLayout(false);
            this.SignalsMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip SignalsStatus;
        private System.Windows.Forms.MenuStrip SignalsMenu;
        private System.Windows.Forms.ToolStripMenuItem fileMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newFileMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openFileMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveFileMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsFileMenuItem;
        private System.Windows.Forms.ToolStripSeparator fileMenuSeparator1;
        private System.Windows.Forms.ToolStripMenuItem exitFileMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editMenuItem;
        private System.Windows.Forms.ToolStripMenuItem undoEditMenuItem;
        private System.Windows.Forms.ToolStripMenuItem redoEditMenuItem;
        private System.Windows.Forms.ToolStripSeparator EditMenuItemSeparator1;
        private System.Windows.Forms.ToolStripMenuItem cutEditMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyEditMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteEditMenuItem;
        private System.Windows.Forms.ToolStripSeparator EditMenuItemSeparator2;
        private System.Windows.Forms.ToolStripMenuItem selectEditMenuItem;
        private System.Windows.Forms.ToolStripMenuItem transportMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutHelpMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewMenuItem;
        private System.Windows.Forms.ToolStripMenuItem trackMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mixerViewMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newTrackMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeFileMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyTrackMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteTrackMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importTracksMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportFileMenuItem;
        private System.Windows.Forms.ToolStripSeparator fileMenuSeparator2;
        private System.Windows.Forms.Timer masterTimer;
        private System.Windows.Forms.ToolStripMenuItem settingsTransportMenuItem;
        private System.Windows.Forms.ToolTip signalsToolTip;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
    }
}

