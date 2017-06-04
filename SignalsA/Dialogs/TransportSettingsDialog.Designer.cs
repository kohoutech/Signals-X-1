namespace Signals.Dialogs
{
    partial class TransportSettingsDialog
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
            this.cbxOutputDevice = new System.Windows.Forms.ComboBox();
            this.lblDevice = new System.Windows.Forms.Label();
            this.lblInputLatency = new System.Windows.Forms.Label();
            this.lblOutputLatency = new System.Windows.Forms.Label();
            this.hsbInputLatency = new System.Windows.Forms.HScrollBar();
            this.hsbOutputLatency = new System.Windows.Forms.HScrollBar();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cbxOutputDevice
            // 
            this.cbxOutputDevice.FormattingEnabled = true;
            this.cbxOutputDevice.Location = new System.Drawing.Point(19, 43);
            this.cbxOutputDevice.Name = "cbxOutputDevice";
            this.cbxOutputDevice.Size = new System.Drawing.Size(267, 21);
            this.cbxOutputDevice.TabIndex = 0;
            // 
            // lblDevice
            // 
            this.lblDevice.AutoSize = true;
            this.lblDevice.Location = new System.Drawing.Point(19, 17);
            this.lblDevice.Name = "lblDevice";
            this.lblDevice.Size = new System.Drawing.Size(80, 13);
            this.lblDevice.TabIndex = 1;
            this.lblDevice.Text = "Output channel";
            // 
            // lblInputLatency
            // 
            this.lblInputLatency.AutoSize = true;
            this.lblInputLatency.Location = new System.Drawing.Point(19, 77);
            this.lblInputLatency.Name = "lblInputLatency";
            this.lblInputLatency.Size = new System.Drawing.Size(109, 13);
            this.lblInputLatency.TabIndex = 2;
            this.lblInputLatency.Text = "Input Latency - 50 ms";
            // 
            // lblOutputLatency
            // 
            this.lblOutputLatency.AutoSize = true;
            this.lblOutputLatency.Location = new System.Drawing.Point(19, 134);
            this.lblOutputLatency.Name = "lblOutputLatency";
            this.lblOutputLatency.Size = new System.Drawing.Size(113, 13);
            this.lblOutputLatency.TabIndex = 3;
            this.lblOutputLatency.Text = "Output latency - 50 ms";
            // 
            // hsbInputLatency
            // 
            this.hsbInputLatency.Location = new System.Drawing.Point(19, 103);
            this.hsbInputLatency.Maximum = 209;
            this.hsbInputLatency.Minimum = 50;
            this.hsbInputLatency.Name = "hsbInputLatency";
            this.hsbInputLatency.Size = new System.Drawing.Size(267, 18);
            this.hsbInputLatency.TabIndex = 4;
            this.hsbInputLatency.Value = 50;
            this.hsbInputLatency.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hsbInputLatency_Scroll);
            // 
            // hsbOutputLatency
            // 
            this.hsbOutputLatency.Location = new System.Drawing.Point(19, 160);
            this.hsbOutputLatency.Maximum = 209;
            this.hsbOutputLatency.Minimum = 50;
            this.hsbOutputLatency.Name = "hsbOutputLatency";
            this.hsbOutputLatency.Size = new System.Drawing.Size(267, 18);
            this.hsbOutputLatency.TabIndex = 5;
            this.hsbOutputLatency.Value = 50;
            this.hsbOutputLatency.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hsbOutputLatency_Scroll);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(120, 201);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 6;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(218, 201);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // TransportSettingsDialog
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(317, 249);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.hsbOutputLatency);
            this.Controls.Add(this.hsbInputLatency);
            this.Controls.Add(this.lblOutputLatency);
            this.Controls.Add(this.lblInputLatency);
            this.Controls.Add(this.lblDevice);
            this.Controls.Add(this.cbxOutputDevice);
            this.Name = "TransportSettingsDialog";
            this.Text = "TransportSettings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbxOutputDevice;
        private System.Windows.Forms.Label lblDevice;
        private System.Windows.Forms.Label lblInputLatency;
        private System.Windows.Forms.Label lblOutputLatency;
        private System.Windows.Forms.HScrollBar hsbInputLatency;
        private System.Windows.Forms.HScrollBar hsbOutputLatency;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
    }
}