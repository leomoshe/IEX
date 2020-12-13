using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IEX.Lab.App
{
    using System.Windows.Forms;
    using System.ComponentModel;
    using IEX.Utilities.Types;
    public class NativeTreeView : TreeView
    {
        TreeNode groups_node;
        public NativeTreeView()
        {
            //this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(236)))), ((int)(((byte)(239)))));
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            ShowLines = false;
            TreeNode t0 = new TreeNode("Computers");
            TreeNode t1 = new TreeNode("");
            TreeNode t2 = new TreeNode("IEX Servers");
            TreeNode t20 = new TreeNode(IEX.Lab.Client.ServerDataState.Connected.GetDescription());
            TreeNode t21 = new TreeNode(IEX.Lab.Client.ServerDataState.Idle.GetDescription());
            TreeNode t22 = new TreeNode(IEX.Lab.Client.ServerDataState.NotRunning.GetDescription());
            TreeNode t23 = new TreeNode(IEX.Server.Monitor.Client.MonitoringServiceReference.ServicesStatus.Error.GetDescription());
            t2.Nodes.AddRange(new TreeNode[] { t20, t21, t22, t23});
            TreeNode t3 = new TreeNode("");
            groups_node = new TreeNode("Groups");
            this.Nodes.AddRange(new TreeNode[] { t0, t1, t2, t3, groups_node });
            this.ExpandAll();

            HideSelection = false;
        }

        [System.Runtime.InteropServices.DllImport("uxtheme.dll", CharSet = System.Runtime.InteropServices.CharSet.Unicode)]
        private extern static int SetWindowTheme(IntPtr hWnd, string pszSubAppName, string pszSubIdList);
        protected override void CreateHandle()
        {
            base.CreateHandle();
            SetWindowTheme(this.Handle, "explorer", null);
        }

        public void SetGroups(IList<IEX.Lab.Client.Group> groups)
        {
            IEnumerable<string> groups_names = groups.Select(item => item.Name);
            string[] nodes_names = new string[groups_node.Nodes.Count];
            foreach (TreeNode tree_node in groups_node.Nodes)
                nodes_names[tree_node.Index] = tree_node.Text;
            var add = groups_names.Except(nodes_names);
            var del = nodes_names.Except(groups_names);
            foreach (string group_name in add)
            {
                TreeNode tree_node = new TreeNode(group_name);
                tree_node.Name = group_name;
                groups_node.Nodes.Add(tree_node);
            }
            foreach (string group_name in del)
                groups_node.Nodes.RemoveByKey(group_name);
            this.ExpandAll();
        }

        //
        // Summary:
        //     Gets the collection of tree nodes that are assigned to the tree view control.
        //
        // Returns:
        //     A System.Windows.Forms.TreeNodeCollection that represents the tree nodes
        //     assigned to the tree view control.
        [MergableProperty(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Localizable(true)]
        public new TreeNodeCollection Nodes 
        {
            get { return base.Nodes; }
        }
    }
}
