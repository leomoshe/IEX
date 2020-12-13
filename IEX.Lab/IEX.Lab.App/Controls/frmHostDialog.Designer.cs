namespace IEX.Lab.App
{
    partial class frmHostDialog
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
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lstServers = new System.Windows.Forms.CheckedListBox();
            this.lblServers = new System.Windows.Forms.Label();
            this.lblHost = new System.Windows.Forms.Label();
            this.txtHost = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.btnOK.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnOK.Location = new System.Drawing.Point(104, 137);
            this.btnOK.Name = "btnOK";
            this.btnOK.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.btnCancel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnCancel.Location = new System.Drawing.Point(185, 137);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // lstServers
            // 
            this.lstServers.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstServers.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstServers.CheckOnClick = true;
            this.lstServers.ColumnWidth = 30;
            this.lstServers.Cursor = System.Windows.Forms.Cursors.Default;
            this.lstServers.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.lstServers.FormattingEnabled = true;
            this.lstServers.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lstServers.Location = new System.Drawing.Point(16, 74);
            this.lstServers.MultiColumn = true;
            this.lstServers.Name = "lstServers";
            this.lstServers.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lstServers.ScrollAlwaysVisible = true;
            this.lstServers.Size = new System.Drawing.Size(244, 47);
            this.lstServers.TabIndex = 1;
            // 
            // lblServers
            // 
            this.lblServers.AutoSize = true;
            this.lblServers.Cursor = System.Windows.Forms.Cursors.Default;
            this.lblServers.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.lblServers.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblServers.Location = new System.Drawing.Point(13, 48);
            this.lblServers.Name = "lblServers";
            this.lblServers.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblServers.Size = new System.Drawing.Size(46, 13);
            this.lblServers.TabIndex = 8;
            this.lblServers.Text = "Servers:";
            // 
            // lblHost
            // 
            this.lblHost.AutoSize = true;
            this.lblHost.Cursor = System.Windows.Forms.Cursors.Default;
            this.lblHost.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.lblHost.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblHost.Location = new System.Drawing.Point(13, 15);
            this.lblHost.Name = "lblHost";
            this.lblHost.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblHost.Size = new System.Drawing.Size(32, 13);
            this.lblHost.TabIndex = 6;
            this.lblHost.Text = "Host:";
            // 
            // txtHost
            // 
            this.txtHost.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtHost.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.txtHost.FormattingEnabled = true;
            this.txtHost.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.txtHost.Location = new System.Drawing.Point(51, 12);
            this.txtHost.Name = "txtHost";
            this.txtHost.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtHost.Size = new System.Drawing.Size(209, 21);
            this.txtHost.TabIndex = 0;
            this.txtHost.SelectedIndexChanged += new System.EventHandler(this.txtHost_SelectedIndexChanged);
            this.txtHost.Leave += new System.EventHandler(this.txtHost_Leave);
            // 
            // frmHostDialog
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(278, 175);
            this.Controls.Add(this.txtHost);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.lstServers);
            this.Controls.Add(this.lblServers);
            this.Controls.Add(this.lblHost);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "frmHostDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Host Editor";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.CheckedListBox lstServers;
        private System.Windows.Forms.Label lblServers;
        private System.Windows.Forms.Label lblHost;
        private System.Windows.Forms.ComboBox txtHost;
    }
}