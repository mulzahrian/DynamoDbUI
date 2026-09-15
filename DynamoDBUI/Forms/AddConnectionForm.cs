using System;
using System.Windows.Forms;
using DynamoDBUI.Models;

namespace DynamoDBUI.Forms
{
    public partial class AddConnectionForm : Form
    {
        public ConnectionProfile Result { get; private set; }

        public AddConnectionForm()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text) || string.IsNullOrWhiteSpace(txtServiceUrl.Text))
            {
                MessageBox.Show("Connection Name dan Service URL wajib diisi.", "Validasi",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Result = new ConnectionProfile
            {
                Name = txtName.Text.Trim(),
                AccessKey = txtAccessKey.Text.Trim(),
                SecretKey = txtSecretKey.Text.Trim(),
                Region = txtRegion.Text.Trim(),
                ServiceUrl = txtServiceUrl.Text.Trim()
            };

            DialogResult = DialogResult.OK;
            Close();
        }
    }
}