namespace IEX.Lab.App
{
    partial class IexStb
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
            Stop();
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.IexVideoStream = new IEX.Utilities.Controls.NetworkPlayer.StreamControl();
            this.pnlHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IexVideoStream)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.Controls.Add(this.dataGridView);
            this.pnlHeader.Cursor = System.Windows.Forms.Cursors.Default;
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.pnlHeader.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.pnlHeader.Size = new System.Drawing.Size(168, 24);
            this.pnlHeader.TabIndex = 1;
            this.pnlHeader.Click += new System.EventHandler(this.iexStb_Click);
            this.pnlHeader.MouseDown += new System.Windows.Forms.MouseEventHandler(this.iexStb_MouseDown);
            // 
            // dataGridView
            // 
            this.dataGridView.ColumnHeadersHeight = 17;
            this.dataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView.Location = new System.Drawing.Point(0, 0);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dataGridView.Size = new System.Drawing.Size(168, 24);
            this.dataGridView.TabIndex = 0;
            this.dataGridView.Click += new System.EventHandler(this.iexStb_Click);
            this.dataGridView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.iexStb_MouseDown);
            // 
            // IexVideoStream
            // 
            this.IexVideoStream.Dock = System.Windows.Forms.DockStyle.Fill;
            this.IexVideoStream.Location = new System.Drawing.Point(0, 24);
            this.IexVideoStream.Name = "IexVideoStream";
            this.IexVideoStream.Size = new System.Drawing.Size(168, 126);
            this.IexVideoStream.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.IexVideoStream.TabIndex = 2;
            this.IexVideoStream.TabStop = false;
            this.IexVideoStream.Url = null;
            this.IexVideoStream.Click += new System.EventHandler(this.iexStb_Click);
            this.IexVideoStream.MouseDown += new System.Windows.Forms.MouseEventHandler(this.iexStb_MouseDown);
            // 
            // IexStb
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.IexVideoStream);
            this.Controls.Add(this.pnlHeader);
            this.Name = "IexStb";
            this.Size = new System.Drawing.Size(168, 150);
            this.pnlHeader.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IexVideoStream)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private Utilities.Controls.NetworkPlayer.StreamControl IexVideoStream;
        private System.Windows.Forms.DataGridView dataGridView;
    }
}
