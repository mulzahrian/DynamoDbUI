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

            this.splitContent = new System.Windows.Forms.SplitContainer();
            this.pnlEditorTop = new System.Windows.Forms.Panel();
            this.lblEditor = new System.Windows.Forms.Label();
            this.btnRunQuery = new System.Windows.Forms.Button();
            this.txtEditor = new System.Windows.Forms.TextBox();

            this.pnlResultsTop = new System.Windows.Forms.Panel();
            this.lblResults = new System.Windows.Forms.Label();
            this.dgvResults = new System.Windows.Forms.DataGridView();
            this.lblMessage = new System.Windows.Forms.Label();

            this.menuStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).BeginInit();
            this.splitMain.Panel1.SuspendLayout();
            this.splitMain.Panel2.SuspendLayout();
            this.splitMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContent)).BeginInit();
            this.splitContent.Panel1.SuspendLayout();
            this.splitContent.Panel2.SuspendLayout();
            this.splitContent.SuspendLayout();
            this.pnlEditorTop.SuspendLayout();
            this.pnlResultsTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResults)).BeginInit();
            this.SuspendLayout();

            // menuStrip
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.mnuFile, this.mnuDatabase, this.mnuWindow, this.mnuHelp });
            this.menuStrip.Dock = System.Windows.Forms.DockStyle.Top;

            // File
            this.mnuFile.Text = "File";
            this.mnuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.mnuFileNewQuery, this.mnuFileExit });
            this.mnuFileNewQuery.Text = "New Query";
            this.mnuFileNewQuery.Click += new System.EventHandler(this.mnuFileNewQuery_Click);
            this.mnuFileExit.Text = "Exit";
            this.mnuFileExit.Click += new System.EventHandler(this.mnuFileExit_Click);

            // Database
            this.mnuDatabase.Text = "Database";
            this.mnuDatabase.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.mnuDatabaseAddConnection, this.mnuDatabaseRefreshTables });
            this.mnuDatabaseAddConnection.Text = "Add Connection...";
            this.mnuDatabaseAddConnection.Click += new System.EventHandler(this.mnuDatabaseAddConnection_Click);
            this.mnuDatabaseRefreshTables.Text = "Refresh Tables";
            this.mnuDatabaseRefreshTables.Click += new System.EventHandler(this.mnuDatabaseRefreshTables_Click);

            // Window
            this.mnuWindow.Text = "Window";
            this.mnuWindow.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.mnuWindowToggleSidebar });
            this.mnuWindowToggleSidebar.Text = "Toggle Sidebar";
            this.mnuWindowToggleSidebar.Click += new System.EventHandler(this.mnuWindowToggleSidebar_Click);

            // Help
            this.mnuHelp.Text = "Help";
            this.mnuHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.mnuHelpAbout });
            this.mnuHelpAbout.Text = "About";
            this.mnuHelpAbout.Click += new System.EventHandler(this.mnuHelpAbout_Click);

            // statusStrip
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { this.tsslConnection });
            this.statusStrip.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tsslConnection.Text = "Tidak ada connection aktif";

            // splitMain (sidebar | content)
            this.splitMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitMain.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.splitMain.SplitterDistance = 220;
            this.splitMain.Panel1.Controls.Add(this.tvConnections);
            this.splitMain.Panel2.Controls.Add(this.splitContent);

            // tvConnections
            this.tvConnections.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvConnections.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvConnections_AfterSelect);
            this.tvConnections.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvConnections_NodeMouseDoubleClick);

            // splitContent (editor / results)
            this.splitContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContent.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.splitContent.SplitterDistance = 180;
            this.splitContent.Panel1.Controls.Add(this.txtEditor);
            this.splitContent.Panel1.Controls.Add(this.pnlEditorTop);
            this.splitContent.Panel2.Controls.Add(this.dgvResults);
            this.splitContent.Panel2.Controls.Add(this.lblMessage);
            this.splitContent.Panel2.Controls.Add(this.pnlResultsTop);

            // pnlEditorTop
            this.pnlEditorTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlEditorTop.Height = 35;
            this.pnlEditorTop.Controls.Add(this.lblEditor);
            this.pnlEditorTop.Controls.Add(this.btnRunQuery);

            this.lblEditor.Text = "Query Editor  (F5 = Run)";
            this.lblEditor.Location = new System.Drawing.Point(8, 10);
            this.lblEditor.Size = new System.Drawing.Size(250, 20);

            this.btnRunQuery.Text = "\u25B6 Run";
            this.btnRunQuery.Location = new System.Drawing.Point(460, 3);
            this.btnRunQuery.Size = new System.Drawing.Size(100, 28);
            this.btnRunQuery.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.btnRunQuery.Click += new System.EventHandler(this.btnRunQuery_Click);

            // txtEditor
            this.txtEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtEditor.Multiline = true;
            this.txtEditor.Font = new System.Drawing.Font("Consolas", 11F);
            this.txtEditor.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtEditor.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtEditor_KeyDown);

            // pnlResultsTop
            this.pnlResultsTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlResultsTop.Height = 25;
            this.pnlResultsTop.Controls.Add(this.lblResults);
            this.lblResults.Text = "Results";
            this.lblResults.Location = new System.Drawing.Point(8, 5);
            this.lblResults.Size = new System.Drawing.Size(200, 20);

            // dgvResults
            this.dgvResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvResults.AllowUserToAddRows = false;
            this.dgvResults.ReadOnly = true;

            // lblMessage
            this.lblMessage.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblMessage.Height = 22;
            this.lblMessage.Text = "";
            this.lblMessage.ForeColor = System.Drawing.Color.DimGray;

            // MainForm
            this.ClientSize = new System.Drawing.Size(1000, 650);
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
            this.splitContent.Panel1.ResumeLayout(false);
            this.splitContent.Panel1.PerformLayout();
            this.splitContent.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContent)).EndInit();
            this.splitContent.ResumeLayout(false);
            this.pnlEditorTop.ResumeLayout(false);
            this.pnlResultsTop.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvResults)).EndInit();
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

        private System.Windows.Forms.SplitContainer splitContent;
        private System.Windows.Forms.Panel pnlEditorTop;
        private System.Windows.Forms.Label lblEditor;
        private System.Windows.Forms.Button btnRunQuery;
        private System.Windows.Forms.TextBox txtEditor;

        private System.Windows.Forms.Panel pnlResultsTop;
        private System.Windows.Forms.Label lblResults;
        private System.Windows.Forms.DataGridView dgvResults;
        private System.Windows.Forms.Label lblMessage;
    }
}