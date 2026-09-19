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

        private Button _btnAddTab;
        private int _queryCounter = 1;

        private ImageList _treeImages;
        private const int IconConnection = 0;
        private const int IconTable = 1;
        private const int IconColumn = 2;

        private Font _tabCloseFont;

        public MainForm()
        {
            InitializeComponent();
            LoadIcon();
            LoadTreeIcons();
            ApplyDarkTheme();
            LoadConnectionsIntoSidebar();
            SetupTabs();
        }

        // ============ THEME ============

        private void LoadIcon()
        {
            try
            {
                string iconPath = System.IO.Path.Combine(Application.StartupPath, "src", "dynamo.ico");
                if (System.IO.File.Exists(iconPath))
                {
                    this.Icon = new System.Drawing.Icon(iconPath);
                }
            }
            catch
            {
                // Kalau file icon tidak ketemu/corrupt, biarkan pakai icon default. Tidak perlu crash aplikasi.
            }
        }

        private void LoadTreeIcons()
        {
            _treeImages = new ImageList { ImageSize = new Size(16, 16), ColorDepth = ColorDepth.Depth32Bit };
            _treeImages.Images.Add(LoadSidebarImage("DynamoDB.png"));
            _treeImages.Images.Add(LoadSidebarImage("table.png"));
            _treeImages.Images.Add(LoadSidebarImage("column.png"));
            tvConnections.ImageList = _treeImages;
        }

        private Image LoadSidebarImage(string fileName)
        {
            var size = _treeImages.ImageSize;
            try
            {
                string path = System.IO.Path.Combine(Application.StartupPath, "src", fileName);
                if (System.IO.File.Exists(path))
                {
                    using (var original = Image.FromFile(path))
                    {
                        var resized = new Bitmap(size.Width, size.Height);
                        using (var g = Graphics.FromImage(resized))
                        {
                            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                            g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                            g.DrawImage(original, 0, 0, size.Width, size.Height);
                        }
                        return resized;
                    }
                }
            }
            catch
            {
                // Kalau file icon gagal dimuat, biarkan placeholder kosong; UI tetap jalan tanpa icon custom.
            }
            return new Bitmap(size.Width, size.Height);
        }

        private void ApplyDarkTheme()
        {
            BackColor = DarkTheme.EditorBackground;

            menuStrip.Renderer = new ToolStripProfessionalRenderer(new AppColorTable());
            menuStrip.BackColor = DarkTheme.PanelBackground;
            foreach (ToolStripMenuItem item in menuStrip.Items)
                SetMenuItemColors(item);

            statusStrip.Renderer = new ToolStripProfessionalRenderer(new AppColorTable());
            statusStrip.BackColor = DarkTheme.PanelBackground;
            tsslConnection.ForeColor = Color.Gainsboro;

            tvConnections.BackColor = DarkTheme.PanelBackground;
            tvConnections.ForeColor = Color.White;
            tvConnections.DrawMode = TreeViewDrawMode.OwnerDrawText;
            tvConnections.DrawNode += tvConnections_DrawNode;

            tabQueries.DrawMode = TabDrawMode.OwnerDrawFixed;
            tabQueries.SizeMode = TabSizeMode.Fixed;
            tabQueries.ItemSize = new Size(130, 28);
            tabQueries.Padding = new Point(10, 5);
            _tabCloseFont = new Font("Segoe UI", 9F, FontStyle.Bold);
            tabQueries.DrawItem += tabQueries_DrawItem;
            tabQueries.Selecting += tabQueries_Selecting;
            tabQueries.MouseDown += tabQueries_MouseDown;
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
            var bg = selected ? DarkTheme.PanelHighlight : DarkTheme.PanelBackground;

            using (var brush = new SolidBrush(bg))
                e.Graphics.FillRectangle(brush, e.Bounds);

            TextRenderer.DrawText(e.Graphics, e.Node.Text, tvConnections.Font, e.Bounds,
                Color.White, TextFormatFlags.VerticalCenter | TextFormatFlags.Left);

            e.DrawDefault = false;
        }

        private void tabQueries_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0 || e.Index >= tabQueries.TabCount)
                return;

            var tabPage = tabQueries.TabPages[e.Index];
            var bounds = tabQueries.GetTabRect(e.Index);
            bool selected = e.Index == tabQueries.SelectedIndex;

            var bg = selected ? DarkTheme.AccentPrimary : DarkTheme.TabInactive;
            using (var brush = new SolidBrush(bg))
                e.Graphics.FillRectangle(brush, bounds);

            if (selected)
            {
                using (var accentBrush = new SolidBrush(DarkTheme.TabActiveAccent))
                    e.Graphics.FillRectangle(accentBrush, bounds.Left, bounds.Bottom - 2, bounds.Width, 2);
            }

            var textRect = bounds;
            textRect.Width -= CloseButtonSize + 8;

            var textColor = selected ? Color.Black : Color.Gainsboro;
            TextRenderer.DrawText(e.Graphics, tabPage.Text, tabQueries.Font, textRect, textColor,
                TextFormatFlags.Left | TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis);

            var closeRect = GetTabCloseButtonRect(bounds);
            TextRenderer.DrawText(e.Graphics, "\u00D7", _tabCloseFont, closeRect,
                selected ? Color.Black : Color.Silver, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
        }

        private const int CloseButtonSize = 16;

        private static Rectangle GetTabCloseButtonRect(Rectangle tabBounds)
        {
            int x = tabBounds.Right - CloseButtonSize - 6;
            int y = tabBounds.Top + (tabBounds.Height - CloseButtonSize) / 2;
            return new Rectangle(x, y, CloseButtonSize, CloseButtonSize);
        }

        // ============ TABS ============

        private void SetupTabs()
        {
            SetupAddTabButton();
            AddNewQueryTab();
        }

        private void SetupAddTabButton()
        {
            _btnAddTab = new Button
            {
                Text = "+",
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                ForeColor = DarkTheme.AccentPrimary,
                BackColor = DarkTheme.TabInactive,
                Size = new Size(30, tabQueries.ItemSize.Height),
                Cursor = Cursors.Hand,
                TabStop = false
            };
            _btnAddTab.FlatAppearance.BorderSize = 0;
            _btnAddTab.FlatAppearance.MouseOverBackColor = DarkTheme.PanelHighlight;
            _btnAddTab.Click += (s, e) => AddNewQueryTab();
            // TabControl.Controls cuma nerima TabPage, jadi tombol "+" ditaruh sebagai sibling
            // di splitMain.Panel2 lalu diposisikan manual supaya nempel di ujung kanan tab terakhir.
            splitMain.Panel2.Controls.Add(_btnAddTab);
            _btnAddTab.BringToFront();
        }

        private void RepositionAddTabButton()
        {
            if (_btnAddTab == null || tabQueries.TabCount == 0) return;
            var lastRect = tabQueries.GetTabRect(tabQueries.TabCount - 1);
            _btnAddTab.Size = new Size(30, lastRect.Height);
            _btnAddTab.Location = new Point(lastRect.Right + 4, lastRect.Top);
            _btnAddTab.BringToFront();
        }

        private QueryEditorPanel AddNewQueryTab()
        {
            var tabPage = new TabPage($"Query {_queryCounter++}");
            var panel = new QueryEditorPanel { Dock = DockStyle.Fill };
            panel.Editor.Text = "VIEW TableName";

            panel.RunButton.Click += (s, e) => RunQuery(panel);
            panel.CommitButton.Click += (s, e) => RunQuery(panel);
            panel.UndoButton.Click += (s, e) =>
            {
                if (panel.Editor.CanUndo) panel.Editor.Undo();
            };
            panel.Editor.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.F5)
                {
                    e.SuppressKeyPress = true;
                    RunQuery(panel);
                }
            };

            tabPage.Controls.Add(panel);
            tabQueries.TabPages.Add(tabPage);
            tabQueries.SelectedTab = tabPage;

            QuerySyntaxHighlighter.Highlight(panel.Editor);
            RepositionAddTabButton();
            return panel;
        }

        private void tabQueries_Selecting(object sender, TabControlCancelEventArgs e)
        {
            // tab "+" sudah tidak lagi jadi TabPage, jadi tidak perlu di-cancel di sini.
        }

        private void tabQueries_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;

            for (int i = 0; i < tabQueries.TabCount; i++)
            {
                var rect = tabQueries.GetTabRect(i);
                if (GetTabCloseButtonRect(rect).Contains(e.Location))
                {
                    CloseQueryTab(i);
                    return;
                }
            }
        }

        private void CloseQueryTab(int index)
        {
            // minimal harus ada 1 query tab yang terbuka
            if (tabQueries.TabPages.Count <= 1)
            {
                ThemedMessageBox.Show(this, "Minimal harus ada 1 query tab yang terbuka.", "Info", ThemedMessageIcon.Info);
                return;
            }

            tabQueries.TabPages.RemoveAt(index);
            RepositionAddTabButton();
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
                var node = CreateConnectionNode(profile);
                tvConnections.Nodes.Add(node);
            }
        }

        private TreeNode CreateConnectionNode(ConnectionProfile profile)
        {
            return new TreeNode(profile.Name)
            {
                Tag = profile,
                ImageIndex = IconConnection,
                SelectedImageIndex = IconConnection
            };
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
                    connectionNode.Nodes.Add(CreateTableNode(t));

                connectionNode.Expand();
                SetStatus($"Terhubung: {profile.Name}", true);
            }
            catch (Exception ex)
            {
                connectionNode.Nodes.Add(new TreeNode("(gagal load tables: " + ex.Message + ")"));
                SetStatus($"Gagal terhubung: {profile.Name}", false);
            }
        }

        private TreeNode CreateTableNode(string tableName)
        {
            var tableNode = new TreeNode(tableName)
            {
                Tag = "table",
                ImageIndex = IconTable,
                SelectedImageIndex = IconTable
            };
            // Placeholder supaya panah expand muncul; kolom baru di-load waktu user expand node ini.
            tableNode.Nodes.Add(new TreeNode("Loading columns...") { Name = "placeholder" });
            return tableNode;
        }

        private void tvConnections_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            var node = e.Node;
            if (node.Tag as string == "table" &&
                node.Nodes.Count == 1 &&
                node.Nodes[0].Name == "placeholder")
            {
                LoadColumnsForNode(node);
            }
        }

        private async void LoadColumnsForNode(TreeNode tableNode)
        {
            var connectionNode = FindAncestorConnectionNode(tableNode);
            var profile = connectionNode?.Tag as ConnectionProfile;
            if (profile == null) return;

            try
            {
                var service = _connectionManager.GetService(profile.Name);
                var columns = await service.GetColumnsAsync(tableNode.Text);

                tableNode.Nodes.Clear();
                if (columns.Count == 0)
                {
                    tableNode.Nodes.Add(new TreeNode("(tidak ada kolom terdeteksi)"));
                }
                else
                {
                    foreach (var column in columns)
                    {
                        tableNode.Nodes.Add(new TreeNode(column)
                        {
                            Tag = "column",
                            ImageIndex = IconColumn,
                            SelectedImageIndex = IconColumn
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                tableNode.Nodes.Clear();
                tableNode.Nodes.Add(new TreeNode("(gagal load columns: " + ex.Message + ")"));
            }
        }

        private static TreeNode FindAncestorConnectionNode(TreeNode node)
        {
            var current = node;
            while (current != null && !(current.Tag is ConnectionProfile))
                current = current.Parent;
            return current;
        }

        private void tvConnections_AfterSelect(object sender, TreeViewEventArgs e)
        {
            var connectionNode = FindAncestorConnectionNode(e.Node);
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

        // ============ SIDEBAR CONTEXT MENU (rename/delete connection, delete table) ============

        private void ctxConnections_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var node = tvConnections.GetNodeAt(tvConnections.PointToClient(Cursor.Position));
            bool isConnection = node?.Tag is ConnectionProfile;
            bool isTable = node?.Tag as string == "table";

            if (node == null || (!isConnection && !isTable))
            {
                e.Cancel = true;
                return;
            }

            tvConnections.SelectedNode = node;

            mnuCtxRename.Visible = isConnection;
            mnuCtxDelete.Visible = isConnection;
            mnuCtxSeparator.Visible = isConnection;
            mnuCtxRefresh.Visible = isConnection;

            mnuCtxDeleteTable.Visible = isTable;
            mnuCtxRefreshColumns.Visible = isTable;
        }

        private void mnuCtxRename_Click(object sender, EventArgs e)
        {
            if (!(tvConnections.SelectedNode?.Tag is ConnectionProfile profile)) return;

            using (var dlg = new RenamePromptForm("Rename Connection", "Connection Name:", profile.Name))
            {
                if (dlg.ShowDialog(this) != DialogResult.OK) return;

                var newName = dlg.Value;
                if (string.IsNullOrEmpty(newName) || newName == profile.Name) return;

                if (_connectionManager.Profiles.Any(p =>
                        p != profile && p.Name.Equals(newName, StringComparison.OrdinalIgnoreCase)))
                {
                    ThemedMessageBox.Show(this, "Nama connection sudah dipakai.", "Validasi", ThemedMessageIcon.Warning);
                    return;
                }

                bool wasActive = _activeConnectionName == profile.Name;
                var oldName = profile.Name;
                _connectionManager.RenameConnection(oldName, newName);

                tvConnections.SelectedNode.Text = newName;

                if (wasActive)
                {
                    _activeConnectionName = newName;
                    SetStatus($"Active connection: {newName}", true);
                }
            }
        }

        private void mnuCtxDelete_Click(object sender, EventArgs e)
        {
            var node = tvConnections.SelectedNode;
            if (!(node?.Tag is ConnectionProfile profile)) return;

            var confirm = ThemedMessageBox.Show(this,
                $"Hapus connection \"{profile.Name}\"?\nIni hanya menghapus profile connection dari aplikasi, database tidak terpengaruh.",
                "Konfirmasi Hapus", ThemedMessageIcon.Question, ThemedMessageButtons.YesNo);
            if (confirm != DialogResult.Yes) return;

            _connectionManager.RemoveConnection(profile.Name);
            node.Remove();

            if (_activeConnectionName == profile.Name)
            {
                _activeConnectionName = null;
                SetStatus("Tidak ada connection aktif", false);
            }
        }

        private void mnuCtxRefreshColumns_Click(object sender, EventArgs e)
        {
            var node = tvConnections.SelectedNode;
            if (node?.Tag as string != "table") return;

            LoadColumnsForNode(node);
            node.Expand();
        }

        private async void mnuCtxDeleteTable_Click(object sender, EventArgs e)
        {
            var node = tvConnections.SelectedNode;
            if (node?.Tag as string != "table") return;

            var connectionNode = FindAncestorConnectionNode(node);
            if (!(connectionNode?.Tag is ConnectionProfile profile)) return;

            var confirm = ThemedMessageBox.Show(this,
                $"Hapus table \"{node.Text}\"?\nSemua data di dalam table ini akan hilang secara permanen.",
                "Konfirmasi Hapus Table", ThemedMessageIcon.Warning, ThemedMessageButtons.YesNo);
            if (confirm != DialogResult.Yes) return;

            try
            {
                var service = _connectionManager.GetService(profile.Name);
                await service.DropTableAsync(node.Text);
                node.Remove();
                SetStatus($"Table \"{node.Text}\" berhasil dihapus.", true);
            }
            catch (Exception ex)
            {
                ThemedMessageBox.Show(this, "Gagal menghapus table: " + ex.Message, "Error", ThemedMessageIcon.Error);
            }
        }

        // ============ MENU ============

        private void mnuDatabaseAddConnection_Click(object sender, EventArgs e)
        {
            using (var form = new AddConnectionForm(_connectionManager.Profiles.Select(p => p.Name)))
            {
                if (form.ShowDialog(this) == DialogResult.OK && form.Result != null)
                {
                    _connectionManager.AddConnection(form.Result);
                    var node = CreateConnectionNode(form.Result);
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
                ThemedMessageBox.Show(this, "Pilih connection di sidebar dulu.", "Info", ThemedMessageIcon.Info);
                return;
            }

            var connectionNode = FindAncestorConnectionNode(tvConnections.SelectedNode);
            if (connectionNode == null)
            {
                ThemedMessageBox.Show(this, "Pilih connection di sidebar dulu.", "Info", ThemedMessageIcon.Info);
                return;
            }

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
            ThemedMessageBox.Show(this,
                "DynamoDBUI\nIDE sederhana untuk DynamoDB Local.\n\n" +
                "Query language:\nVIEW table [FINDBY col=val] [ORDER BY col ASC|DESC]\n" +
                "CREATE TABLE table (col TYPE PK, col TYPE SK, ...)\n" +
                "INSERT INTO table (col1,col2) VALUES (val1,val2)\nDROP TABLE table",
                "About DynamoDBUI", ThemedMessageIcon.Info);
        }

        // ============ QUERY EXECUTION ============

        private async void RunQuery(QueryEditorPanel panel)
        {
            if (string.IsNullOrEmpty(_activeConnectionName))
            {
                ThemedMessageBox.Show(this, "Pilih / connect ke database dulu di sidebar sebelah kiri.",
                    "Belum ada connection aktif", ThemedMessageIcon.Warning);
                return;
            }

            var service = _connectionManager.GetService(_activeConnectionName);

            // Kalau user nge-block (select) sebagian teks di editor, jalankan hanya
            // bagian yang di-select itu. Kalau tidak ada selection, jalankan seluruh
            // isi editor seperti biasa. Ini yang memungkinkan banyak query
            // (dipisah per baris/blok) ditulis dalam satu tab lalu dijalankan satu-satu.
            string queryText = panel.Editor.SelectionLength > 0
                ? panel.Editor.SelectedText
                : panel.Editor.Text;

            try
            {
                var parsed = QueryParser.Parse(queryText);
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