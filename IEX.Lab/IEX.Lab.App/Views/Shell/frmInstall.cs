using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using IEX.AutomaticInstaller;
using System.Threading;
using System.Diagnostics;

namespace IEX.Lab.App.Views
{
    public partial class frmInstall : Form
    {
        public frmInstall()
        {
            InitializeComponent();

            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;

            string lastBuild = "";           
            try
            {
                lastBuild = Installer.GetLastVersion();
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show("Didn't you read the 'ReadMe'?" + Environment.NewLine + @"You must have access to \\ilstore\iex_cd", "Don't Mess With The Zohan!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(-1);
            }

            try
            {
                rbtLastBuild.Text += "  --  " + lastBuild;
                rbtLastBuild.Tag = lastBuild;              
            }
            catch (Exception exc)
            {
                MessageBox.Show("Error. Please Change the version manually. Message: " + exc.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(-1);
            }
        }
             
        private void rbtThisBuild_CheckedChanged(object sender, EventArgs e)
        {
            this.txtThisBuild.Enabled = rbtThisBuild.Checked;
        }

        public string Version
        {
            get
            {
                string version = "";
                if (rbtThisBuild.Checked)
                    version = txtThisBuild.Text;

                if (rbtLastBuild.Checked)
                    version = rbtLastBuild.Tag.ToString();              

                return version;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {            
            //validate the version
            if (!Installer.IsValidVersion(Version))
            {
                MessageBox.Show(this, "The requested version: " + Version + " couldn't be found", "Invalid Version", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
