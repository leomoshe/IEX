namespace IEX.Lab.App.Views
{
    partial class frmInstall
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
            this.rbtLastBuild = new System.Windows.Forms.RadioButton();
            this.rbtThisBuild = new System.Windows.Forms.RadioButton();
            this.txtThisBuild = new System.Windows.Forms.TextBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // rbtLastBuild
            // 
            this.rbtLastBuild.AutoSize = true;
            this.rbtLastBuild.Location = new System.Drawing.Point(25, 16);
            this.rbtLastBuild.Name = "rbtLastBuild";
            this.rbtLastBuild.Size = new System.Drawing.Size(101, 17);
            this.rbtLastBuild.TabIndex = 0;
            this.rbtLastBuild.TabStop = true;
            this.rbtLastBuild.Text = "Install Last Build";
            this.rbtLastBuild.UseVisualStyleBackColor = true;
            // 
            // rbtThisBuild
            // 
            this.rbtThisBuild.AutoSize = true;
            this.rbtThisBuild.Location = new System.Drawing.Point(25, 39);
            this.rbtThisBuild.Name = "rbtThisBuild";
            this.rbtThisBuild.Size = new System.Drawing.Size(104, 17);
            this.rbtThisBuild.TabIndex = 1;
            this.rbtThisBuild.TabStop = true;
            this.rbtThisBuild.Text = "Install This Build:";
            this.rbtThisBuild.UseVisualStyleBackColor = true;
            this.rbtThisBuild.CheckedChanged += new System.EventHandler(this.rbtThisBuild_CheckedChanged);
            // 
            // txtThisBuild
            // 
            this.txtThisBuild.Enabled = false;
            this.txtThisBuild.Location = new System.Drawing.Point(135, 38);
            this.txtThisBuild.Name = "txtThisBuild";
            this.txtThisBuild.Size = new System.Drawing.Size(113, 20);
            this.txtThisBuild.TabIndex = 2;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnOK.Location = new System.Drawing.Point(107, 77);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(77, 25);
            this.btnOK.TabIndex = 3;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancel.Location = new System.Drawing.Point(190, 77);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(77, 25);
            this.btnCancel.TabIndex = 16;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // frmInstall
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(278, 112);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.txtThisBuild);
            this.Controls.Add(this.rbtThisBuild);
            this.Controls.Add(this.rbtLastBuild);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmInstall";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "IEX Remote Install";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton rbtLastBuild;
        private System.Windows.Forms.RadioButton rbtThisBuild;
        private System.Windows.Forms.TextBox txtThisBuild;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
    }
}

