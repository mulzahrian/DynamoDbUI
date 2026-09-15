using System;
using System.Windows.Forms;

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
            Text = title;
            lblPrompt.Text = label;
            txtValue.Text = currentValue;
            txtValue.SelectAll();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtValue.Text))
            {
                MessageBox.Show("Nama tidak boleh kosong.", "Validasi",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Value = txtValue.Text.Trim();
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
