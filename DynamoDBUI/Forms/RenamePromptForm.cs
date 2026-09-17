using System;
using System.Drawing;
using System.Windows.Forms;
using DynamoDBUI.Utils;

namespace DynamoDBUI.Forms
{
    /// <summary>
    /// Dialog kecil untuk minta 1 input teks, dipakai untuk rename connection.
    /// </summary>
    public partial class RenamePromptForm : Form
    {
        public string Value { get; private set; }

        public RenamePromptForm(string title, string label, string currentValue)
        {
            InitializeComponent();
            ApplyTheme();

            Text = title;
            lblPrompt.Text = label;
            txtValue.Text = currentValue;
            txtValue.SelectAll();
        }

        private void ApplyTheme()
        {
            BackColor = DarkTheme.DialogBackground;

            lblPrompt.ForeColor = Color.Gainsboro;

            txtValue.BackColor = DarkTheme.InputBackground;
            txtValue.ForeColor = Color.White;
            txtValue.BorderStyle = BorderStyle.FixedSingle;

            btnOk.BackColor = DarkTheme.AccentPrimary;
            btnOk.ForeColor = Color.Black;
            btnOk.FlatAppearance.MouseOverBackColor = ControlPaint.Light(DarkTheme.AccentPrimary, 0.25f);
            btnOk.FlatAppearance.MouseDownBackColor = DarkTheme.AccentPrimaryDark;

            btnCancel.BackColor = DarkTheme.PanelHighlight;
            btnCancel.ForeColor = Color.White;
            btnCancel.FlatAppearance.MouseOverBackColor = ControlPaint.Light(DarkTheme.PanelHighlight, 0.3f);
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtValue.Text))
            {
                ThemedMessageBox.Show(this, "Nama tidak boleh kosong.", "Validasi", ThemedMessageIcon.Warning);
                return;
            }

            Value = txtValue.Text.Trim();
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
