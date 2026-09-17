using System;
using System.Drawing;
using System.Windows.Forms;
using DynamoDBUI.Utils;

namespace DynamoDBUI.Forms
{
    public partial class ThemedMessageForm : Form
    {
        private readonly ThemedMessageIcon _icon;
        private bool _showSecondaryButton;

        public ThemedMessageForm(string message, string title, ThemedMessageIcon icon, ThemedMessageButtons buttons)
        {
            InitializeComponent();
            _icon = icon;
            Text = title;

            ApplyTheme();
            BuildButtons(buttons);

            lblMessage.Text = message;
            ResizeToFitMessage();
        }

        private void ApplyTheme()
        {
            BackColor = DarkTheme.DialogBackground;
            lblMessage.ForeColor = Color.Gainsboro;

            btnPrimary.BackColor = DarkTheme.AccentPrimary;
            btnPrimary.ForeColor = Color.Black;
            btnPrimary.FlatAppearance.MouseOverBackColor = ControlPaint.Light(DarkTheme.AccentPrimary, 0.25f);
            btnPrimary.FlatAppearance.MouseDownBackColor = DarkTheme.AccentPrimaryDark;

            btnSecondary.BackColor = DarkTheme.PanelHighlight;
            btnSecondary.ForeColor = Color.White;
            btnSecondary.FlatAppearance.MouseOverBackColor = ControlPaint.Light(DarkTheme.PanelHighlight, 0.3f);
        }

        private void BuildButtons(ThemedMessageButtons buttons)
        {
            _showSecondaryButton = buttons == ThemedMessageButtons.YesNo;

            if (_showSecondaryButton)
            {
                btnPrimary.Text = "Yes";
                btnPrimary.DialogResult = DialogResult.Yes;
                btnSecondary.Text = "No";
                btnSecondary.DialogResult = DialogResult.No;
                btnSecondary.Visible = true;
                AcceptButton = btnPrimary;
                CancelButton = btnSecondary;
            }
            else
            {
                btnPrimary.Text = "OK";
                btnPrimary.DialogResult = DialogResult.OK;
                btnSecondary.Visible = false;
                AcceptButton = btnPrimary;
                CancelButton = btnPrimary;
            }

            PositionButtons();
        }

        private void PositionButtons()
        {
            const int margin = 24;
            int y = ClientSize.Height - 46;

            if (_showSecondaryButton)
            {
                btnSecondary.Location = new Point(ClientSize.Width - margin - btnSecondary.Width, y);
                btnPrimary.Location = new Point(btnSecondary.Left - 10 - btnPrimary.Width, y);
            }
            else
            {
                btnPrimary.Location = new Point(ClientSize.Width - margin - btnPrimary.Width, y);
            }
        }

        private void ResizeToFitMessage()
        {
            const int maxWidth = 300;

            Size textSize;
            using (var g = CreateGraphics())
            {
                var measured = g.MeasureString(lblMessage.Text, lblMessage.Font, maxWidth);
                textSize = new Size(maxWidth, (int)Math.Ceiling(measured.Height) + 4);
            }

            lblMessage.Size = textSize;

            int contentBottom = Math.Max(lblMessage.Bottom, iconPanel.Bottom);
            int desiredHeight = Math.Max(contentBottom + 70, 156);
            ClientSize = new Size(ClientSize.Width, desiredHeight);

            PositionButtons();
        }

        private void iconPanel_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            var color = GetIconColor();
            var rect = new Rectangle(0, 0, iconPanel.Width - 1, iconPanel.Height - 1);

            using (var brush = new SolidBrush(Color.FromArgb(45, color)))
                e.Graphics.FillEllipse(brush, rect);
            using (var pen = new Pen(color, 2f))
                e.Graphics.DrawEllipse(pen, rect);

            string glyph = GetGlyph();
            using (var font = new Font("Segoe UI", 16F, FontStyle.Bold))
            {
                var textSize = e.Graphics.MeasureString(glyph, font);
                var pos = new PointF((iconPanel.Width - textSize.Width) / 2, (iconPanel.Height - textSize.Height) / 2 - 1);
                using (var textBrush = new SolidBrush(color))
                    e.Graphics.DrawString(glyph, font, textBrush, pos);
            }
        }

        private Color GetIconColor()
        {
            switch (_icon)
            {
                case ThemedMessageIcon.Warning: return DarkTheme.RunAccent;
                case ThemedMessageIcon.Error: return DarkTheme.DangerAccent;
                default: return DarkTheme.AccentPrimary;
            }
        }

        private string GetGlyph()
        {
            switch (_icon)
            {
                case ThemedMessageIcon.Warning: return "!";
                case ThemedMessageIcon.Error: return "\u00D7";
                case ThemedMessageIcon.Question: return "?";
                default: return "i";
            }
        }
    }
}
