namespace Signals.Dialogs
{
    partial class newProjectDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(newProjectDialog));
            this.lblName = new System.Windows.Forms.Label();
            this.lblSampleRate = new System.Windows.Forms.Label();
            this.lblDuration = new System.Windows.Forms.Label();
            this.cbxSampleRate = new System.Windows.Forms.ComboBox();
            this.txtProjectName = new System.Windows.Forms.TextBox();
            this.txtDuration = new System.Windows.Forms.TextBox();
            this.OKButton = new System.Windows.Forms.Button();
            this.CancelButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(15, 23);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(69, 13);
            this.lblName.TabIndex = 5;
            this.lblName.Text = "Project name";
            // 
            // lblSampleRate
            // 
            this.lblSampleRate.AutoSize = true;
            this.lblSampleRate.Location = new System.Drawing.Point(15, 58);
            this.lblSampleRate.Name = "lblSampleRate";
            this.lblSampleRate.Size = new System.Drawing.Size(63, 13);
            this.lblSampleRate.TabIndex = 6;
            this.lblSampleRate.Text = "Sample rate";
            // 
            // lblDuration
            // 
            this.lblDuration.AutoSize = true;
            this.lblDuration.Location = new System.Drawing.Point(15, 94);
            this.lblDuration.Name = "lblDuration";
            this.lblDuration.Size = new System.Drawing.Size(94, 13);
            this.lblDuration.TabIndex = 7;
            this.lblDuration.Text = "Length in seconds";
            // 
            // cbxSampleRate
            // 
            this.cbxSampleRate.FormattingEnabled = true;
            this.cbxSampleRate.Location = new System.Drawing.Point(121, 55);
            this.cbxSampleRate.Name = "cbxSampleRate";
            this.cbxSampleRate.Size = new System.Drawing.Size(103, 21);
            this.cbxSampleRate.TabIndex = 1;
            // 
            // txtProjectName
            // 
            this.txtProjectName.Location = new System.Drawing.Point(121, 20);
            this.txtProjectName.Name = "txtProjectName";
            this.txtProjectName.Size = new System.Drawing.Size(148, 20);
            this.txtProjectName.TabIndex = 0;
            // 
            // txtDuration
            // 
            this.txtDuration.Location = new System.Drawing.Point(121, 91);
            this.txtDuration.Name = "txtDuration";
            this.txtDuration.Size = new System.Drawing.Size(75, 20);
            this.txtDuration.TabIndex = 2;
            // 
            // OKButton
            // 
            this.OKButton.Location = new System.Drawing.Point(121, 127);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(73, 23);
            this.OKButton.TabIndex = 3;
            this.OKButton.Text = "OK";
            this.OKButton.UseVisualStyleBackColor = true;
            this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
            // 
            // CancelButton
            // 
            this.CancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelButton.Location = new System.Drawing.Point(211, 127);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(73, 23);
            this.CancelButton.TabIndex = 4;
            this.CancelButton.Text = "Cancel";
            this.CancelButton.UseVisualStyleBackColor = true;
            this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // newProjectDialog
            // 
            this.AcceptButton = this.OKButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(314, 171);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.OKButton);
            this.Controls.Add(this.txtDuration);
            this.Controls.Add(this.txtProjectName);
            this.Controls.Add(this.cbxSampleRate);
            this.Controls.Add(this.lblDuration);
            this.Controls.Add(this.lblSampleRate);
            this.Controls.Add(this.lblName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "newProjectDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "New Signals X-1 Project";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblSampleRate;
        private System.Windows.Forms.Label lblDuration;
        private System.Windows.Forms.ComboBox cbxSampleRate;
        private System.Windows.Forms.TextBox txtProjectName;
        private System.Windows.Forms.TextBox txtDuration;
        private System.Windows.Forms.Button OKButton;
        private System.Windows.Forms.Button CancelButton;
    }
}