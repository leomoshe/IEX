using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IEX.Lab.App.Views
{
    using System.Windows.Forms;
    using System.Collections;
    using System.ComponentModel;
    public class MRUList : ArrayList
    {
        public event EventHandler ItemClicked;
        private ToolStripMenuItem Parent { get; set; }
        public int MaxRecentFiles { get; set; }
        public MRUList(ToolStripMenuItem parent)
        {
            MaxRecentFiles = 9;
            Parent = parent;
            if (Properties.Settings.Default.ProjectMRUList.Count == 0)
                InsertElement(System.IO.Path.Combine(IEX.Utilities.IEXConfiguration.GetIexInstallationFolder(), IEX.Lab.Client.XmlRepository.DefaultFileName));
            else
            {
                int i = 0;
                foreach (var value in Properties.Settings.Default.ProjectMRUList)
                    InsertMenuItem(i++, value);
            }
        }

        public string ValueAt(int index)
        {
            return Properties.Settings.Default.ProjectMRUList[index];
        }

        public void InsertElement(string data)
        {
            string[] values = data.Split(',');
            foreach(string value in values)
            {
                RemoveItem(value);
                if (base.Count >= MaxRecentFiles)
                    RemoveItem(Parent.DropDownItems.Count - 1);
                InsertItem(0, value);
            }
            Save();
        }

        void item_Click(object sender, EventArgs e)
        {
            string value = ((ToolStripMenuItem)sender).Text;
            InsertElement(value);
            if (ItemClicked != null)
                ItemClicked(value, e);
        }

        int IndexItem(string value)
        {
            for (int i = 0; i < Parent.DropDownItems.Count; ++i)
            {
                ToolStripMenuItem item = (ToolStripMenuItem)Parent.DropDownItems[i];
                if (item.Text == value)
                    return i;
            }
            return -1;
        }

        void InsertItem(int index, string value)
        {
            //base.Insert(0, value);
            Properties.Settings.Default.ProjectMRUList.Insert(0, value);
            InsertMenuItem(0, value);
        }

        void RemoveItem(string value)
        {
            int index = IndexItem(value);
            if (index != -1)
                RemoveItem(index);
        }

        void RemoveItem(int index)
        {
            ToolStripMenuItem item = (ToolStripMenuItem)Parent.DropDownItems[index];
            string value = item.Text;
            //base.Remove(item.Text);
            Properties.Settings.Default.ProjectMRUList.Remove(value);
            item.Click -= new EventHandler(item_Click);
            item.Dispose();
        }

        void InsertMenuItem(int index, string value)
        {
            ToolStripMenuItem menu_item = new ToolStripMenuItem(value);
            Parent.DropDownItems.Insert(index, menu_item);
            menu_item.Click += new EventHandler(item_Click);
        }

        void Save()
        {
            Properties.Settings.Default.Save();
        }
    }
}
