using System.Windows.Forms;
using DynamoDBUI.Utils;

namespace DynamoDBUI.Forms
{
    public partial class QueryEditorPanel : UserControl
    {
        public RichTextBox Editor => rtbEditor;
        public DataGridView ResultsGrid => dgvResults;
        public Label MessageLabel => lblMessage;
        public Button RunButton => btnRun;

        public QueryEditorPanel()
        {
            InitializeComponent();
            ApplyTheme();

            rtbEditor.TextChanged += (s, e) => QuerySyntaxHighlighter.Highlight(rtbEditor);
        }

        private void ApplyTheme()
        {
            BackColor = DarkTheme.EditorBackground;

            // Toolbar editor
            pnlToolbar.BackColor = DarkTheme.HeaderPurple;
            lblEditorTitle.ForeColor = System.Drawing.Color.White;
            btnRun.BackColor = DarkTheme.AccentPurple;
            btnRun.ForeColor = System.Drawing.Color.White;

            // Editor
            rtbEditor.BackColor = DarkTheme.EditorBackground;
            rtbEditor.ForeColor = DarkTheme.EditorForeground;

            // Results header
            pnlResultsHeader.BackColor = DarkTheme.HeaderPurple;
            lblResultsTitle.ForeColor = System.Drawing.Color.White;

            // Results grid
            dgvResults.BackgroundColor = DarkTheme.ResultsBackground;
            dgvResults.GridColor = DarkTheme.GridLines;
            dgvResults.DefaultCellStyle.BackColor = DarkTheme.ResultsBackground;
            dgvResults.DefaultCellStyle.ForeColor = DarkTheme.ResultsText;
            dgvResults.DefaultCellStyle.SelectionBackColor = DarkTheme.AccentPurple;
            dgvResults.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.White;
            dgvResults.AlternatingRowsDefaultCellStyle.BackColor = DarkTheme.ResultsAltRow;
            dgvResults.AlternatingRowsDefaultCellStyle.ForeColor = DarkTheme.ResultsText;
            dgvResults.ColumnHeadersDefaultCellStyle.BackColor = DarkTheme.HeaderPurple;
            dgvResults.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            dgvResults.ColumnHeadersDefaultCellStyle.Font =
                new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            dgvResults.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            // Message bar
            lblMessage.BackColor = DarkTheme.HeaderPurple;
            lblMessage.ForeColor = System.Drawing.Color.Gainsboro;

            // Splitter
            splitEditorResults.BackColor = DarkTheme.HeaderPurple;
        }
    }
}