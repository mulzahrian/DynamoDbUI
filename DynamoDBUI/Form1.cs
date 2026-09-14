using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Amazon.Runtime;

namespace DynamoDBUI
{
    public partial class Form1 : Form
    {
        private AmazonDynamoDBClient _client;

        public Form1()
        {
            InitializeComponent();
        }

        // ================= CONNECT =================
        private void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                var credentials = new BasicAWSCredentials(
                    txtAccessKey.Text.Trim(),
                    txtSecretKey.Text.Trim());

                var config = new AmazonDynamoDBConfig
                {
                    ServiceURL = txtServiceUrl.Text.Trim(),
                    // Set region untuk keperluan signature, bukan endpoint,
                    // supaya tidak konflik dengan ServiceURL.
                    AuthenticationRegion = txtRegion.Text.Trim()
                };

                _client = new AmazonDynamoDBClient(credentials, config);

                lblStatus.Text = "Terhubung ke " + txtServiceUrl.Text.Trim();
                lblStatus.ForeColor = System.Drawing.Color.Green;

                MessageBox.Show("Berhasil terhubung ke DynamoDB Local.", "Info",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                lblStatus.Text = "Gagal terhubung";
                lblStatus.ForeColor = System.Drawing.Color.Red;
                MessageBox.Show("Error: " + ex.Message, "Connection Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool EnsureConnected()
        {
            if (_client == null)
            {
                MessageBox.Show("Silakan Connect terlebih dahulu.", "Peringatan",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        // ================= CREATE TABLE =================
        private async void btnCreateTable_Click(object sender, EventArgs e)
        {
            if (!EnsureConnected()) return;

            string tableName = txtTableName.Text.Trim();
            string partitionKey = txtPartitionKey.Text.Trim();

            if (string.IsNullOrEmpty(tableName) || string.IsNullOrEmpty(partitionKey))
            {
                MessageBox.Show("Table Name dan Partition Key wajib diisi.");
                return;
            }

            try
            {
                var request = new CreateTableRequest
                {
                    TableName = tableName,
                    AttributeDefinitions = new List<AttributeDefinition>
                    {
                        new AttributeDefinition
                        {
                            AttributeName = partitionKey,
                            AttributeType = ScalarAttributeType.S
                        }
                    },
                    KeySchema = new List<KeySchemaElement>
                    {
                        new KeySchemaElement
                        {
                            AttributeName = partitionKey,
                            KeyType = KeyType.HASH
                        }
                    },
                    BillingMode = BillingMode.PAY_PER_REQUEST
                };

                await _client.CreateTableAsync(request);

                MessageBox.Show($"Table '{tableName}' berhasil dibuat.", "Sukses",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                btnRefreshTables_Click(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saat create table: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ================= LIST TABLE =================
        private async void btnRefreshTables_Click(object sender, EventArgs e)
        {
            if (!EnsureConnected()) return;

            try
            {
                var response = await _client.ListTablesAsync();
                lstTables.Items.Clear();
                foreach (var name in response.TableNames)
                {
                    lstTables.Items.Add(name);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saat list table: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ================= DELETE TABLE =================
        private async void btnDeleteTable_Click(object sender, EventArgs e)
        {
            if (!EnsureConnected()) return;

            if (lstTables.SelectedItem == null)
            {
                MessageBox.Show("Pilih table yang mau dihapus terlebih dahulu.");
                return;
            }

            string tableName = lstTables.SelectedItem.ToString();

            var confirm = MessageBox.Show($"Yakin mau hapus table '{tableName}'?",
                "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes) return;

            try
            {
                await _client.DeleteTableAsync(tableName);
                MessageBox.Show("Table berhasil dihapus.");
                btnRefreshTables_Click(sender, e);
                dgvData.DataSource = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saat delete table: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ================= VIEW TABLE DATA =================
        private async void btnViewTable_Click(object sender, EventArgs e)
        {
            if (!EnsureConnected()) return;

            if (lstTables.SelectedItem == null)
            {
                MessageBox.Show("Pilih table dari daftar terlebih dahulu.");
                return;
            }

            string tableName = lstTables.SelectedItem.ToString();

            try
            {
                var scanRequest = new ScanRequest
                {
                    TableName = tableName
                };

                var response = await _client.ScanAsync(scanRequest);
                DataTable dt = BuildDataTable(response.Items);
                dgvData.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saat mengambil data: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Helper: convert List<Dictionary<string, AttributeValue>> jadi DataTable
        private DataTable BuildDataTable(List<Dictionary<string, AttributeValue>> items)
        {
            var dt = new DataTable();

            // Kumpulkan semua nama kolom dari seluruh item (karena DynamoDB schemaless)
            var columnNames = items
                .SelectMany(item => item.Keys)
                .Distinct()
                .ToList();

            foreach (var col in columnNames)
            {
                dt.Columns.Add(col);
            }

            foreach (var item in items)
            {
                var row = dt.NewRow();
                foreach (var col in columnNames)
                {
                    row[col] = item.ContainsKey(col)
                        ? AttributeValueToString(item[col])
                        : string.Empty;
                }
                dt.Rows.Add(row);
            }

            return dt;
        }

        private string AttributeValueToString(AttributeValue av)
        {
            if (av.S != null) return av.S;
            if (av.N != null) return av.N;
            if (av.BOOL != null) return av.BOOL.ToString();
            if (av.NULL == true) return "NULL";
            if (av.SS != null && av.SS.Count > 0) return string.Join(",", av.SS);
            if (av.NS != null && av.NS.Count > 0) return string.Join(",", av.NS);
            if (av.M != null && av.M.Count > 0) return "[Map]";
            if (av.L != null && av.L.Count > 0) return "[List]";
            return string.Empty;
        }
    }
}