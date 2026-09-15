using System.Drawing;
using System.Drawing.Drawing2D;
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
        public Button CommitButton => btnCommit;
        public Button UndoButton => btnUndo;

        public QueryEditorPanel()
        {
            InitializeComponent();
            ApplyTheme();

            rtbEditor.TextChanged += (s, e) => QuerySyntaxHighlighter.Highlight(rtbEditor);

            pnlToolbar.Resize += (s, e) => PositionToolbarButtons();
            PositionToolbarButtons();
        }

        private void PositionToolbarButtons()
        {
            const int margin = 10;
            const int gap = 8;

            int y = (pnlToolbar.Height - btnRun.Height) / 2;
            int xRun = pnlToolbar.ClientSize.Width - margin - btnRun.Width;
            int xCommit = xRun - gap - btnCommit.Width;
            int xUndo = xCommit - gap - btnUndo.Width;

            btnRun.Location = new Point(xRun, y);
            btnCommit.Location = new Point(xCommit, y);
            btnUndo.Location = new Point(xUndo, y);
        }

        private void ApplyTheme()
        {
            BackColor = DarkTheme.EditorBackground;

            // Toolbar editor
            pnlToolbar.BackColor = DarkTheme.PanelBackground;
            lblEditorTitle.ForeColor = Color.White;

            foreach (var btn in new[] { btnRun, btnCommit, btnUndo })
            {
                btn.BackColor = DarkTheme.PanelHighlight;
                btn.FlatAppearance.MouseOverBackColor = ControlPaint.Light(DarkTheme.PanelHighlight, 0.2f);
                btn.FlatAppearance.MouseDownBackColor = DarkTheme.PanelBackground;
            }

            // Editor
            rtbEditor.BackColor = DarkTheme.EditorBackground;
            rtbEditor.ForeColor = DarkTheme.EditorForeground;

            // Results header
            pnlResultsHeader.BackColor = DarkTheme.PanelBackground;
            lblResultsTitle.ForeColor = Color.White;

            // Results grid
            dgvResults.BackgroundColor = DarkTheme.ResultsBackground;
            dgvResults.GridColor = DarkTheme.GridLines;
            dgvResults.DefaultCellStyle.BackColor = DarkTheme.ResultsBackground;
            dgvResults.DefaultCellStyle.ForeColor = DarkTheme.ResultsText;
            dgvResults.DefaultCellStyle.SelectionBackColor = DarkTheme.AccentPrimaryDark;
            dgvResults.DefaultCellStyle.SelectionForeColor = Color.White;
            dgvResults.AlternatingRowsDefaultCellStyle.BackColor = DarkTheme.ResultsAltRow;
            dgvResults.AlternatingRowsDefaultCellStyle.ForeColor = DarkTheme.ResultsText;
            dgvResults.ColumnHeadersDefaultCellStyle.BackColor = DarkTheme.PanelBackground;
            dgvResults.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvResults.ColumnHeadersDefaultCellStyle.Font =
                new Font("Segoe UI", 9F, FontStyle.Bold);
            dgvResults.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            // Message bar
            lblMessage.BackColor = DarkTheme.PanelBackground;
            lblMessage.ForeColor = Color.Gainsboro;

            // Splitter
            splitEditorResults.BackColor = DarkTheme.PanelBackground;
        }

        // ============ ICON BUTTONS (Run / Commit / Un Commit) ============
        // Digambar manual pakai GDI+ supaya konsisten & tidak bergantung ke font icon tertentu.

        private void btnRun_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            var r = btnRun.ClientRectangle;
            int cx = r.Width / 2 - 1;
            int cy = r.Height / 2;

            var points = new[]
            {
                new PointF(cx - 5f, cy - 6.5f),
                new PointF(cx - 5f, cy + 6.5f),
                new PointF(cx + 7f, cy)
            };

            using (var brush = new SolidBrush(DarkTheme.RunAccent))
                e.Graphics.FillPolygon(brush, points);
        }

        private void btnCommit_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            var r = btnCommit.ClientRectangle;
            int cx = r.Width / 2;
            int cy = r.Height / 2;

            var points = new[]
            {
                new PointF(cx - 7f, cy),
                new PointF(cx - 2f, cy + 5f),
                new PointF(cx + 7f, cy - 6f)
            };

            using (var pen = new Pen(DarkTheme.CommitAccent, 2.4f)
            {
                StartCap = LineCap.Round,
                EndCap = LineCap.Round,
                LineJoin = LineJoin.Round
            })
            {
                e.Graphics.DrawLines(pen, points);
            }
        }

        private void btnUndo_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            var r = btnUndo.ClientRectangle;
            int cx = r.Width / 2;
            int cy = r.Height / 2;

            var arcRect = new RectangleF(cx - 6.5f, cy - 6.5f, 13f, 13f);

            using (var pen = new Pen(DarkTheme.UndoAccent, 2.1f)
            {
                StartCap = LineCap.Round,
                EndCap = LineCap.Round
            })
            {
                e.Graphics.DrawArc(pen, arcRect, -50, 260);
            }

            // Kepala panah di ujung awal arc, supaya kelihatan seperti icon undo.
            var arrowHead = new[]
            {
                new PointF(cx - 3f, cy - 8.5f),
                new PointF(cx + 3.5f, cy - 7.5f),
                new PointF(cx - 1f, cy - 2.5f)
            };

            using (var brush = new SolidBrush(DarkTheme.UndoAccent))
                e.Graphics.FillPolygon(brush, arrowHead);
        }
    }
}
