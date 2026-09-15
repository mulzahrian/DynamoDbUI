using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DynamoDBUI.Forms;
using DynamoDBUI.Models;
using DynamoDBUI.QueryEngine;
using DynamoDBUI.Services;
using DynamoDBUI.Utils;

namespace DynamoDBUI
{
    public partial class MainForm : Form
    {
        private readonly ConnectionManager _connectionManager = new ConnectionManager();
        private string _activeConnectionName;

        private TabPage _tabPlus;
        private int _queryCounter = 1;

        public MainForm()
        {
            InitializeComponent();
            ApplyDarkTheme();
            LoadConnectionsIntoSidebar();
            SetupTabs();
        }

        // ============ THEME ============

        private void ApplyDarkTheme()
        {
            BackColor = DarkTheme.EditorBackground;

            menuStrip.Renderer = new ToolStripProfessionalRenderer(new DarkPurpleColorTable());
            menuStrip.BackColor = DarkTheme.HeaderPurple;
            foreach (ToolStripMenuItem item in menuStrip.Items)
                SetMenuItemColors(item);

            statusStrip.Renderer = new ToolStripProfessionalRenderer(new DarkPurpleColorTable());
            statusStrip.BackColor = DarkTheme.HeaderPurple;
            tsslConnection.ForeColor = Color.Gainsboro;

            tvConnections.BackColor = DarkTheme.HeaderPurple;
            tvConnections.ForeColor = Color.White;
            tvConnections.DrawMode = TreeViewDrawMode.OwnerDrawText;
            tvConnections.DrawNode += tvConnections_DrawNode;

            tabQueries.DrawMode = TabDrawMode.OwnerDrawFixed;
            tabQueries.SizeMode = TabSizeMode.Fixed;
            tabQueries.ItemSize = new Size(110, 32);
            tabQueries.Padding = new Point(12, 6);
            tabQueries.DrawItem += tabQueries_DrawItem;
            tabQueries.Selecting += tabQueries_Selecting;
            tabQueries.MouseDoubleClick += tabQueries_MouseDoubleClick;
        }

        private void SetMenuItemColors(ToolStripMenuItem item)
        {
            item.ForeColor = Color.White;
            foreach (var sub in item.DropDownItems.OfType<ToolStripMenuItem>())
                sub.ForeColor = Color.White;
        }

        private void tvConnections_DrawNode(object sender, DrawTreeNodeEventArgs e)
        {
            bool selected = (e.State & TreeNodeStates.Selected) != 0;
            var bg = selected ? DarkTheme.HeaderPurpleLight : DarkTheme.HeaderPurple;

            using (var brush = new SolidBrush(bg))
                e.Graphics.FillRectangle(brush, e.Bounds);

            TextRenderer.DrawText(e.Graphics, e.Node.Text, tvConnections.Font, e.Bounds,
                Color.White, TextFormatFlags.VerticalCenter | TextFormatFlags.Left);

            e.DrawDefault = false;
        }

        private void tabQueries_DrawItem(object sender, DrawItemEventArgs e)
        {
            var tabPage = tabQueries.TabPages[e.Index];
            var bounds = tabQueries.GetTabRect(e.Index);
            bool selected = e.Index == tabQueries.SelectedIndex;

            var bg = selected ? DarkTheme.AccentPurple : DarkTheme.HeaderPurple;
            using (var brush = new SolidBrush(bg))
                e.Graphics.FillRectangle(brush, bounds);

            TextRenderer.DrawText(e.Graphics, tabPage.Text, tabQueries.Font, bounds,
                Color.White, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
        }

        // ============ TABS ============

        private void SetupTabs()
        {
            _tabPlus = new TabPage("+");
            tabQueries.TabPages.Add(_tabPlus);
            AddNewQueryTab();
        }

        private QueryEditorPanel AddNewQueryTab()
        {
            var tabPage = new TabPage($"Query {_queryCounter++}");
            var panel = new QueryEditorPanel { Dock = DockStyle.Fill };
            panel.Editor.Text = "VIEW TableName";

            panel.RunButton.Click += (s, e) => RunQuery(panel);
            panel.Editor.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.F5)
                {
                    e.SuppressKeyPress = true;
                    RunQuery(panel);
                }
            };

            tabPage.Controls.Add(panel);
            int insertIndex = tabQueries.TabPages.Count - 1; // sebelum tab "+"
            tabQueries.TabPages.Insert(insertIndex, tabPage);
            tabQueries.SelectedTab = tabPage;

            QuerySyntaxHighlighter.Highlight(panel.Editor);
            return panel;
        }

        private void tabQueries_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (e.TabPage == _tabPlus)
            {
                e.Cancel = true;
                AddNewQueryTab();
            }
        }

        private void tabQueries_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            for (int i = 0; i < tabQueries.TabCount; i++)
            {
                if (!tabQueries.GetTabRect(i).Contains(e.Location)) continue;
                if (tabQueries.TabPages[i] == _tabPlus) return;

                // minimal harus ada 1 tab query aktif selain tab "+"
                if (tabQueries.TabPages.Count <= 2)
                {
                    MessageBox.Show("Minimal harus ada 1 query tab yang terbuka.");
                    return;
                }

                tabQueries.TabPages.RemoveAt(i);
                break;
            }
        }

        private QueryEditorPanel GetActivePanel()
        {
            return tabQueries.SelectedTab?.Controls.OfType<QueryEditorPanel>().FirstOrDefault();
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
                    connectionNode.Nodes.Add(new TreeNode(t) { Tag = "table" });

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
                var panel = GetActivePanel() ?? AddNewQueryTab();
                panel.Editor.Text = $"VIEW {node.Text}";
                QuerySyntaxHighlighter.Highlight(panel.Editor);
                RunQuery(panel);
            }
        }

        // ============ MENU ============

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

        private void mnuFileNewQuery_Click(object sender, EventArgs e) => AddNewQueryTab();

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

        // ============ QUERY EXECUTION ============

        private async void RunQuery(QueryEditorPanel panel)
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
                var parsed = QueryParser.Parse(panel.Editor.Text);
                var result = await QueryExecutor.ExecuteAsync(parsed, service);

                panel.ResultsGrid.DataSource = result.Data;
                panel.MessageLabel.ForeColor = Color.Gainsboro;
                panel.MessageLabel.Text = $"{result.Message}  ({result.ElapsedMs} ms)";

                if (parsed.Type == QueryType.CreateTable || parsed.Type == QueryType.DropTable)
                {
                    var connectionNode = FindConnectionNode(_activeConnectionName);
                    if (connectionNode != null) RefreshTablesForNode(connectionNode);
                }
            }
            catch (QueryParseException pex)
            {
                panel.MessageLabel.ForeColor = Color.Orange;
                panel.MessageLabel.Text = "Syntax error: " + pex.Message;
            }
            catch (Exception ex)
            {
                panel.MessageLabel.ForeColor = Color.OrangeRed;
                panel.MessageLabel.Text = "Error: " + ex.Message;
            }
        }

        private TreeNode FindConnectionNode(string name)
        {
            foreach (TreeNode node in tvConnections.Nodes)
                if (node.Tag is ConnectionProfile p && p.Name == name) return node;
            return null;
        }

        private void SetStatus(string text, bool ok)
        {
            tsslConnection.Text = text;
            tsslConnection.ForeColor = ok ? Color.MediumSpringGreen : Color.OrangeRed;
        }
    }
}