using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace IEX.Lab.App
{
    public partial class frmHostDialog : Form
    {
        public enum DialogMode { Add, Edit };
        public frmHostDialog()
        {
            InitializeComponent();
        }

        public DialogMode Mode
        {
            set { txtHost.Enabled = value == DialogMode.Add; }
        }

        private string _selected_host = string.Empty;
        public string SelectedHost
        {
            get { return txtHost.Text; }
            set 
            { 
                if (txtHost.Items.IndexOf(value) == -1)
                    txtHost.Items.Add(value);
                txtHost.Text = value;
                UpdateSettings(value);
            }
        }

        public void Hosts(string[] hosts)
        {
            txtHost.Items.AddRange(hosts);
            if (txtHost.Items.Count > 0)
                txtHost.SelectedIndex = 0;
        }

        private IEnumerable<string> _excluded_hosts;
        public void ExcludedHosts(string[] hosts)
        {
            _excluded_hosts = hosts;
        }

        public void ConfiguratedIdsServers(IList<int> ids)
        {
            if (ids == null)
                return;
            foreach (int id in ids)
                lstServers.Items.Add(id);
        }

        public void Servers(IEnumerable<int> ids)
        {
            foreach (int id in ids)
            {
                int idx = lstServers.Items.IndexOf(id);
                if (idx > -1)
                    lstServers.SetItemChecked(idx, true);
            }
        }

        public int[] Servers()
        {
            List<int> result = new List<int>();
            for (int i = 0; i < lstServers.Items.Count; ++i)
            {
                if (lstServers.GetItemChecked(i))
                    result.Add((int)lstServers.Items[i]);
            }
            return result.ToArray();
        }

        private void txtHost_Leave(object sender, EventArgs e)
        {
            string host_id = txtHost.Text;
            if (_excluded_hosts.FirstOrDefault(item => item == host_id) == null)
                UpdateSettings(host_id);
            else
            {
                System.Windows.Forms.MessageBox.Show(string.Format("A host '{0}' is already listed.", host_id));
                txtHost.Text = _selected_host;
            }
        }

        private void txtHost_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateSettings(txtHost.Text);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
        }

        private void UpdateSettings(string host_id)
        {
            int index = Properties.Settings.Default.Hosts.IndexOf(host_id);
            if (index != -1)
                Properties.Settings.Default.Hosts.RemoveAt(index);
            if (Properties.Settings.Default.Hosts.Count == 0)
                Properties.Settings.Default.Hosts.Add(txtHost.Text);
            else
                Properties.Settings.Default.Hosts.Insert(0, txtHost.Text);
            Properties.Settings.Default.Save();
            _selected_host = host_id;
        }       
    }
}
