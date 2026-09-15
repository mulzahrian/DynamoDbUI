namespace DynamoDBUI
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileNewQuery = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileExit = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDatabase = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDatabaseAddConnection = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDatabaseRefreshTables = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuWindow = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuWindowToggleSidebar = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHelpAbout = new System.Windows.Forms.ToolStripMenuItem();

            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.tsslConnection = new System.Windows.Forms.ToolStripStatusLabel();

            this.splitMain = new System.Windows.Forms.SplitContainer();
            this.tvConnections = new System.Windows.Forms.TreeView();
            this.tabQueries = new System.Windows.Forms.TabControl();

            this.ctxConnections = new System.Windows.Forms.ContextMenuStrip();
            this.mnuCtxRename = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuCtxDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuCtxSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.mnuCtxRefresh = new System.Windows.Forms.ToolStripMenuItem();

            this.menuStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).BeginInit();
            this.splitMain.Panel1.SuspendLayout();
            this.splitMain.Panel2.SuspendLayout();
            this.splitMain.SuspendLayout();
            this.SuspendLayout();

            // menuStrip
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.mnuFile, this.mnuDatabase, this.mnuWindow, this.mnuHelp });
            this.menuStrip.Dock = System.Windows.Forms.DockStyle.Top;

            this.mnuFile.Text = "File";
            this.mnuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.mnuFileNewQuery, this.mnuFileExit });
            this.mnuFileNewQuery.Text = "New Query";
            this.mnuFileNewQuery.Click += new System.EventHandler(this.mnuFileNewQuery_Click);
            this.mnuFileExit.Text = "Exit";
            this.mnuFileExit.Click += new System.EventHandler(this.mnuFileExit_Click);

            this.mnuDatabase.Text = "Database";
            this.mnuDatabase.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.mnuDatabaseAddConnection, this.mnuDatabaseRefreshTables });
            this.mnuDatabaseAddConnection.Text = "Add Connection...";
            this.mnuDatabaseAddConnection.Click += new System.EventHandler(this.mnuDatabaseAddConnection_Click);
            this.mnuDatabaseRefreshTables.Text = "Refresh Tables";
            this.mnuDatabaseRefreshTables.Click += new System.EventHandler(this.mnuDatabaseRefreshTables_Click);

            this.mnuWindow.Text = "Window";
            this.mnuWindow.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.mnuWindowToggleSidebar });
            this.mnuWindowToggleSidebar.Text = "Toggle Sidebar";
            this.mnuWindowToggleSidebar.Click += new System.EventHandler(this.mnuWindowToggleSidebar_Click);

            this.mnuHelp.Text = "Help";
            this.mnuHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.mnuHelpAbout });
            this.mnuHelpAbout.Text = "About";
            this.mnuHelpAbout.Click += new System.EventHandler(this.mnuHelpAbout_Click);

            // statusStrip
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { this.tsslConnection });
            this.statusStrip.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tsslConnection.Text = "Tidak ada connection aktif";

            // splitMain (sidebar lebih kecil | query editor lebih besar/fokus)
            this.splitMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitMain.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.splitMain.Size = new System.Drawing.Size(1100, 634);
            this.splitMain.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitMain.Panel1MinSize = 160;
            this.splitMain.Panel2MinSize = 300;
            this.splitMain.SplitterDistance = 220;
            this.splitMain.Panel1.Controls.Add(this.tvConnections);
            this.splitMain.Panel2.Controls.Add(this.tabQueries);

            // tvConnections
            this.tvConnections.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvConnections.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tvConnections.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.tvConnections.ContextMenuStrip = this.ctxConnections;
            this.tvConnections.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvConnections_AfterSelect);
            this.tvConnections.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvConnections_NodeMouseDoubleClick);
            this.tvConnections.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.tvConnections_BeforeExpand);

            // ctxConnections (rename/delete connection)
            this.ctxConnections.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.mnuCtxRename, this.mnuCtxDelete, this.mnuCtxSeparator, this.mnuCtxRefresh });
            this.ctxConnections.Opening += new System.ComponentModel.CancelEventHandler(this.ctxConnections_Opening);
            this.mnuCtxRename.Text = "Rename Connection...";
            this.mnuCtxRename.Click += new System.EventHandler(this.mnuCtxRename_Click);
            this.mnuCtxDelete.Text = "Delete Connection";
            this.mnuCtxDelete.Click += new System.EventHandler(this.mnuCtxDelete_Click);
            this.mnuCtxRefresh.Text = "Refresh Tables";
            this.mnuCtxRefresh.Click += new System.EventHandler(this.mnuDatabaseRefreshTables_Click);

            // tabQueries
            this.tabQueries.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabQueries.Font = new System.Drawing.Font("Segoe UI", 9.5F);

            // MainForm
            this.ClientSize = new System.Drawing.Size(1100, 680);
            this.Controls.Add(this.splitMain);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip);
            this.MainMenuStrip = this.menuStrip;
            this.Text = "DynamoDBUI - DynamoDB Local Manager";

            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.splitMain.Panel1.ResumeLayout(false);
            this.splitMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).EndInit();
            this.splitMain.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem mnuFile;
        private System.Windows.Forms.ToolStripMenuItem mnuFileNewQuery;
        private System.Windows.Forms.ToolStripMenuItem mnuFileExit;
        private System.Windows.Forms.ToolStripMenuItem mnuDatabase;
        private System.Windows.Forms.ToolStripMenuItem mnuDatabaseAddConnection;
        private System.Windows.Forms.ToolStripMenuItem mnuDatabaseRefreshTables;
        private System.Windows.Forms.ToolStripMenuItem mnuWindow;
        private System.Windows.Forms.ToolStripMenuItem mnuWindowToggleSidebar;
        private System.Windows.Forms.ToolStripMenuItem mnuHelp;
        private System.Windows.Forms.ToolStripMenuItem mnuHelpAbout;

        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel tsslConnection;

        private System.Windows.Forms.SplitContainer splitMain;
        private System.Windows.Forms.TreeView tvConnections;
        private System.Windows.Forms.TabControl tabQueries;

        private System.Windows.Forms.ContextMenuStrip ctxConnections;
        private System.Windows.Forms.ToolStripMenuItem mnuCtxRename;
        private System.Windows.Forms.ToolStripMenuItem mnuCtxDelete;
        private System.Windows.Forms.ToolStripSeparator mnuCtxSeparator;
        private System.Windows.Forms.ToolStripMenuItem mnuCtxRefresh;
    }
}