namespace DynamoDBUI.Forms
{
    partial class QueryEditorPanel
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.splitEditorResults = new System.Windows.Forms.SplitContainer();
            this.pnlToolbar = new System.Windows.Forms.Panel();
            this.btnRun = new System.Windows.Forms.Button();
            this.btnCommit = new System.Windows.Forms.Button();
            this.btnUndo = new System.Windows.Forms.Button();
            this.toolTip = new System.Windows.Forms.ToolTip();
            this.lblEditorTitle = new System.Windows.Forms.Label();
            this.rtbEditor = new System.Windows.Forms.RichTextBox();
            this.pnlResultsHeader = new System.Windows.Forms.Panel();
            this.lblResultsTitle = new System.Windows.Forms.Label();
            this.dgvResults = new System.Windows.Forms.DataGridView();
            this.lblMessage = new System.Windows.Forms.Label();

            ((System.ComponentModel.ISupportInitialize)(this.splitEditorResults)).BeginInit();
            this.splitEditorResults.Panel1.SuspendLayout();
            this.splitEditorResults.Panel2.SuspendLayout();
            this.splitEditorResults.SuspendLayout();
            this.pnlToolbar.SuspendLayout();
            this.pnlResultsHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResults)).BeginInit();
            this.SuspendLayout();

            // splitEditorResults
            this.splitEditorResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitEditorResults.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.splitEditorResults.SplitterDistance = 200;
            this.splitEditorResults.Panel1.Controls.Add(this.rtbEditor);
            this.splitEditorResults.Panel1.Controls.Add(this.pnlToolbar);
            this.splitEditorResults.Panel2.Controls.Add(this.dgvResults);
            this.splitEditorResults.Panel2.Controls.Add(this.lblMessage);
            this.splitEditorResults.Panel2.Controls.Add(this.pnlResultsHeader);

            // pnlToolbar
            this.pnlToolbar.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlToolbar.Height = 40;
            this.pnlToolbar.Controls.Add(this.lblEditorTitle);
            this.pnlToolbar.Controls.Add(this.btnUndo);
            this.pnlToolbar.Controls.Add(this.btnCommit);
            this.pnlToolbar.Controls.Add(this.btnRun);

            this.lblEditorTitle.Text = "Query Editor";
            this.lblEditorTitle.Location = new System.Drawing.Point(10, 12);
            this.lblEditorTitle.Size = new System.Drawing.Size(200, 20);
            this.lblEditorTitle.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);

            // Tiga tombol icon-only: Undo | Commit | Run, posisi dihitung ulang di code-behind
            // (lihat PositionToolbarButtons) supaya selalu nempel rapi di kanan toolbar berapa pun lebarnya.
            this.btnRun.Text = "";
            this.btnRun.Size = new System.Drawing.Size(32, 30);
            this.btnRun.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRun.FlatAppearance.BorderSize = 0;
            this.btnRun.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRun.TabStop = false;
            this.toolTip.SetToolTip(this.btnRun, "Run Query (F5)");
            this.btnRun.Paint += new System.Windows.Forms.PaintEventHandler(this.btnRun_Paint);

            this.btnCommit.Text = "";
            this.btnCommit.Size = new System.Drawing.Size(32, 30);
            this.btnCommit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCommit.FlatAppearance.BorderSize = 0;
            this.btnCommit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCommit.TabStop = false;
            this.toolTip.SetToolTip(this.btnCommit, "Commit (jalankan perubahan)");
            this.btnCommit.Paint += new System.Windows.Forms.PaintEventHandler(this.btnCommit_Paint);

            this.btnUndo.Text = "";
            this.btnUndo.Size = new System.Drawing.Size(32, 30);
            this.btnUndo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUndo.FlatAppearance.BorderSize = 0;
            this.btnUndo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnUndo.TabStop = false;
            this.toolTip.SetToolTip(this.btnUndo, "Un Commit (undo, Ctrl+Z)");
            this.btnUndo.Paint += new System.Windows.Forms.PaintEventHandler(this.btnUndo_Paint);

            // rtbEditor
            this.rtbEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbEditor.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbEditor.Font = new System.Drawing.Font("Consolas", 11.5F);
            this.rtbEditor.WordWrap = false;
            this.rtbEditor.AcceptsTab = true;

            // pnlResultsHeader
            this.pnlResultsHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlResultsHeader.Height = 28;
            this.pnlResultsHeader.Controls.Add(this.lblResultsTitle);

            this.lblResultsTitle.Text = "Results";
            this.lblResultsTitle.Location = new System.Drawing.Point(10, 6);
            this.lblResultsTitle.Size = new System.Drawing.Size(200, 20);
            this.lblResultsTitle.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);

            // dgvResults
            this.dgvResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvResults.AllowUserToAddRows = false;
            this.dgvResults.ReadOnly = true;
            this.dgvResults.RowHeadersVisible = false;
            this.dgvResults.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvResults.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvResults.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvResults.EnableHeadersVisualStyles = false;
            this.dgvResults.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dgvResults.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.dgvResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvResults.ColumnHeadersHeight = 32;

            // lblMessage
            this.lblMessage.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblMessage.Height = 26;
            this.lblMessage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblMessage.Padding = new System.Windows.Forms.Padding(8, 0, 0, 0);

            // QueryEditorPanel
            this.Controls.Add(this.splitEditorResults);
            this.Size = new System.Drawing.Size(700, 500);

            this.splitEditorResults.Panel1.ResumeLayout(false);
            this.splitEditorResults.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitEditorResults)).EndInit();
            this.splitEditorResults.ResumeLayout(false);
            this.pnlToolbar.ResumeLayout(false);
            this.pnlResultsHeader.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvResults)).EndInit();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.SplitContainer splitEditorResults;
        private System.Windows.Forms.Panel pnlToolbar;
        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.Button btnCommit;
        private System.Windows.Forms.Button btnUndo;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.Label lblEditorTitle;
        private System.Windows.Forms.RichTextBox rtbEditor;
        private System.Windows.Forms.Panel pnlResultsHeader;
        private System.Windows.Forms.Label lblResultsTitle;
        private System.Windows.Forms.DataGridView dgvResults;
        private System.Windows.Forms.Label lblMessage;
    }
}