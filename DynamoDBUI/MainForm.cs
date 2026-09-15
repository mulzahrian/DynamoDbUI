using System;
using System.Windows.Forms;
using DynamoDBUI.Forms;
using DynamoDBUI.Models;
using DynamoDBUI.QueryEngine;
using DynamoDBUI.Services;

namespace DynamoDBUI
{
    public partial class MainForm : Form
    {
        private readonly ConnectionManager _connectionManager = new ConnectionManager();
        private string _activeConnectionName;

        public MainForm()
        {
            InitializeComponent();
            LoadConnectionsIntoSidebar();
            txtEditor.Text = "VIEW TableName";
        }

        // ============ SIDEBAR ============

        private void LoadConnectionsIntoSidebar()
        {
            tvConnections.Nodes.Clear();
            foreach (var profile in _connectionManager.Profiles)
            {
                var node = new TreeNode(profile.Name) { Tag = profile };
                tvConnections.Nodes.Add(node);
            }
        }

        private async void RefreshTablesForNode(TreeNode connectionNode)
        {
            var profile = connectionNode.Tag as ConnectionProfile;
            if (profile == null) return;

            connectionNode.Nodes.Clear();

            try
            {
                var service = _connectionManager.GetService(profile.Name);
                var tables = await service.ListTablesAsync();

                foreach (var t in tables)
                {
                    connectionNode.Nodes.Add(new TreeNode(t) { Tag = "table" });
                }

                connectionNode.Expand();
                SetStatus($"Terhubung: {profile.Name}", true);
            }
            catch (Exception ex)
            {
                connectionNode.Nodes.Add(new TreeNode("(gagal load tables: " + ex.Message + ")"));
                SetStatus($"Gagal terhubung: {profile.Name}", false);
            }
        }

        private void tvConnections_AfterSelect(object sender, TreeViewEventArgs e)
        {
            var node = e.Node;

            // Jika yang dipilih adalah node table, ambil parent-nya sebagai connection aktif
            var connectionNode = node.Tag is ConnectionProfile ? node : node.Parent;
            if (connectionNode?.Tag is ConnectionProfile profile)
            {
                _activeConnectionName = profile.Name;
                SetStatus($"Active connection: {profile.Name}", true);
            }
        }

        private void tvConnections_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            var node = e.Node;

            if (node.Tag is ConnectionProfile)
            {
                RefreshTablesForNode(node);
            }
            else if (node.Tag as string == "table")
            {
                txtEditor.Text = $"VIEW {node.Text}";
                RunQuery();
            }
        }

        // ============ MENU: DATABASE ============

        private void mnuDatabaseAddConnection_Click(object sender, EventArgs e)
        {
            using (var form = new AddConnectionForm())
            {
                if (form.ShowDialog(this) == DialogResult.OK && form.Result != null)
                {
                    _connectionManager.AddConnection(form.Result);
                    var node = new TreeNode(form.Result.Name) { Tag = form.Result };
                    tvConnections.Nodes.Add(node);
                    node.Expand();
                    RefreshTablesForNode(node);
                }
            }
        }

        private void mnuDatabaseRefreshTables_Click(object sender, EventArgs e)
        {
            if (tvConnections.SelectedNode == null)
            {
                MessageBox.Show("Pilih connection di sidebar dulu.");
                return;
            }

            var connectionNode = tvConnections.SelectedNode.Tag is ConnectionProfile
                ? tvConnections.SelectedNode
                : tvConnections.SelectedNode.Parent;

            RefreshTablesForNode(connectionNode);
        }

        // ============ MENU: FILE / WINDOW / HELP ============

        private void mnuFileNewQuery_Click(object sender, EventArgs e) => txtEditor.Text = "";

        private void mnuFileExit_Click(object sender, EventArgs e) => Application.Exit();

        private void mnuWindowToggleSidebar_Click(object sender, EventArgs e)
        {
            splitMain.Panel1Collapsed = !splitMain.Panel1Collapsed;
        }

        private void mnuHelpAbout_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "DynamoDBUI\nIDE sederhana untuk DynamoDB Local.\n\n" +
                "Query language:\nVIEW table [FINDBY col=val] [ORDER BY col ASC|DESC]\n" +
                "CREATE TABLE table (col TYPE PK, col TYPE SK, ...)\n" +
                "INSERT INTO table (col1,col2) VALUES (val1,val2)\nDROP TABLE table",
                "About DynamoDBUI");
        }

        // ============ QUERY EDITOR ============

        private void txtEditor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                e.SuppressKeyPress = true;
                RunQuery();
            }
        }

        private void btnRunQuery_Click(object sender, EventArgs e) => RunQuery();

        private async void RunQuery()
        {
            if (string.IsNullOrEmpty(_activeConnectionName))
            {
                MessageBox.Show("Pilih / connect ke database dulu di sidebar sebelah kiri.",
                    "Belum ada connection aktif", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var service = _connectionManager.GetService(_activeConnectionName);

            try
            {
                var parsed = QueryParser.Parse(txtEditor.Text);
                var result = await QueryExecutor.ExecuteAsync(parsed, service);

                dgvResults.DataSource = result.Data;
                lblMessage.ForeColor = System.Drawing.Color.DimGray;
                lblMessage.Text = $"{result.Message}  ({result.ElapsedMs} ms)";

                // Kalau perintahnya CREATE/DROP TABLE, refresh daftar table di sidebar
                if (parsed.Type == QueryType.CreateTable || parsed.Type == QueryType.DropTable)
                {
                    var connectionNode = FindConnectionNode(_activeConnectionName);
                    if (connectionNode != null) RefreshTablesForNode(connectionNode);
                }
            }
            catch (QueryParseException pex)
            {
                lblMessage.ForeColor = System.Drawing.Color.OrangeRed;
                lblMessage.Text = "Syntax error: " + pex.Message;
            }
            catch (Exception ex)
            {
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = "Error: " + ex.Message;
            }
        }

        private TreeNode FindConnectionNode(string name)
        {
            foreach (TreeNode node in tvConnections.Nodes)
            {
                if (node.Tag is ConnectionProfile p && p.Name == name)
                    return node;
            }
            return null;
        }

        private void SetStatus(string text, bool ok)
        {
            tsslConnection.Text = text;
            tsslConnection.ForeColor = ok ? System.Drawing.Color.Green : System.Drawing.Color.Red;
        }
    }
}