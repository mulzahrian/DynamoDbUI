namespace DynamoDBUI
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.grpConnection = new System.Windows.Forms.GroupBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.btnConnect = new System.Windows.Forms.Button();
            this.txtServiceUrl = new System.Windows.Forms.TextBox();
            this.lblServiceUrl = new System.Windows.Forms.Label();
            this.txtRegion = new System.Windows.Forms.TextBox();
            this.lblRegion = new System.Windows.Forms.Label();
            this.txtSecretKey = new System.Windows.Forms.TextBox();
            this.lblSecretKey = new System.Windows.Forms.Label();
            this.txtAccessKey = new System.Windows.Forms.TextBox();
            this.lblAccessKey = new System.Windows.Forms.Label();

            this.grpCreateTable = new System.Windows.Forms.GroupBox();
            this.btnCreateTable = new System.Windows.Forms.Button();
            this.txtPartitionKey = new System.Windows.Forms.TextBox();
            this.lblPartitionKey = new System.Windows.Forms.Label();
            this.txtTableName = new System.Windows.Forms.TextBox();
            this.lblTableName = new System.Windows.Forms.Label();

            this.grpTables = new System.Windows.Forms.GroupBox();
            this.btnDeleteTable = new System.Windows.Forms.Button();
            this.btnRefreshTables = new System.Windows.Forms.Button();
            this.lstTables = new System.Windows.Forms.ListBox();

            this.grpData = new System.Windows.Forms.GroupBox();
            this.btnViewTable = new System.Windows.Forms.Button();
            this.dgvData = new System.Windows.Forms.DataGridView();

            this.grpConnection.SuspendLayout();
            this.grpCreateTable.SuspendLayout();
            this.grpTables.SuspendLayout();
            this.grpData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            this.SuspendLayout();

            // grpConnection
            this.grpConnection.Text = "Koneksi DynamoDB Local";
            this.grpConnection.Location = new System.Drawing.Point(12, 12);
            this.grpConnection.Size = new System.Drawing.Size(760, 150);
            this.grpConnection.Controls.Add(this.lblStatus);
            this.grpConnection.Controls.Add(this.btnConnect);
            this.grpConnection.Controls.Add(this.txtServiceUrl);
            this.grpConnection.Controls.Add(this.lblServiceUrl);
            this.grpConnection.Controls.Add(this.txtRegion);
            this.grpConnection.Controls.Add(this.lblRegion);
            this.grpConnection.Controls.Add(this.txtSecretKey);
            this.grpConnection.Controls.Add(this.lblSecretKey);
            this.grpConnection.Controls.Add(this.txtAccessKey);
            this.grpConnection.Controls.Add(this.lblAccessKey);

            // lblAccessKey
            this.lblAccessKey.Text = "AWS Access Key ID:";
            this.lblAccessKey.Location = new System.Drawing.Point(15, 30);
            this.lblAccessKey.Size = new System.Drawing.Size(140, 20);
            // txtAccessKey
            this.txtAccessKey.Text = "local";
            this.txtAccessKey.Location = new System.Drawing.Point(160, 27);
            this.txtAccessKey.Size = new System.Drawing.Size(150, 20);

            // lblSecretKey
            this.lblSecretKey.Text = "AWS Secret Access Key:";
            this.lblSecretKey.Location = new System.Drawing.Point(15, 60);
            this.lblSecretKey.Size = new System.Drawing.Size(140, 20);
            // txtSecretKey
            this.txtSecretKey.Text = "local";
            this.txtSecretKey.Location = new System.Drawing.Point(160, 57);
            this.txtSecretKey.Size = new System.Drawing.Size(150, 20);
            this.txtSecretKey.UseSystemPasswordChar = true;

            // lblRegion
            this.lblRegion.Text = "Default Region Name:";
            this.lblRegion.Location = new System.Drawing.Point(15, 90);
            this.lblRegion.Size = new System.Drawing.Size(140, 20);
            // txtRegion
            this.txtRegion.Text = "ap-southeast-1";
            this.txtRegion.Location = new System.Drawing.Point(160, 87);
            this.txtRegion.Size = new System.Drawing.Size(150, 20);

            // lblServiceUrl
            this.lblServiceUrl.Text = "Service URL:";
            this.lblServiceUrl.Location = new System.Drawing.Point(330, 30);
            this.lblServiceUrl.Size = new System.Drawing.Size(90, 20);
            // txtServiceUrl
            this.txtServiceUrl.Text = "http://localhost:8000";
            this.txtServiceUrl.Location = new System.Drawing.Point(420, 27);
            this.txtServiceUrl.Size = new System.Drawing.Size(180, 20);

            // btnConnect
            this.btnConnect.Text = "Connect";
            this.btnConnect.Location = new System.Drawing.Point(420, 60);
            this.btnConnect.Size = new System.Drawing.Size(100, 30);
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);

            // lblStatus
            this.lblStatus.Text = "Belum terhubung";
            this.lblStatus.ForeColor = System.Drawing.Color.Red;
            this.lblStatus.Location = new System.Drawing.Point(420, 100);
            this.lblStatus.Size = new System.Drawing.Size(320, 20);

            // grpCreateTable
            this.grpCreateTable.Text = "Create Table";
            this.grpCreateTable.Location = new System.Drawing.Point(12, 170);
            this.grpCreateTable.Size = new System.Drawing.Size(760, 90);
            this.grpCreateTable.Controls.Add(this.btnCreateTable);
            this.grpCreateTable.Controls.Add(this.txtPartitionKey);
            this.grpCreateTable.Controls.Add(this.lblPartitionKey);
            this.grpCreateTable.Controls.Add(this.txtTableName);
            this.grpCreateTable.Controls.Add(this.lblTableName);

            // lblTableName
            this.lblTableName.Text = "Table Name:";
            this.lblTableName.Location = new System.Drawing.Point(15, 30);
            this.lblTableName.Size = new System.Drawing.Size(100, 20);
            // txtTableName
            this.txtTableName.Location = new System.Drawing.Point(120, 27);
            this.txtTableName.Size = new System.Drawing.Size(200, 20);

            // lblPartitionKey
            this.lblPartitionKey.Text = "Partition Key (String):";
            this.lblPartitionKey.Location = new System.Drawing.Point(340, 30);
            this.lblPartitionKey.Size = new System.Drawing.Size(140, 20);
            // txtPartitionKey
            this.txtPartitionKey.Text = "Id";
            this.txtPartitionKey.Location = new System.Drawing.Point(485, 27);
            this.txtPartitionKey.Size = new System.Drawing.Size(120, 20);

            // btnCreateTable
            this.btnCreateTable.Text = "Create Table";
            this.btnCreateTable.Location = new System.Drawing.Point(620, 25);
            this.btnCreateTable.Size = new System.Drawing.Size(120, 30);
            this.btnCreateTable.Click += new System.EventHandler(this.btnCreateTable_Click);

            // grpTables
            this.grpTables.Text = "Daftar Table";
            this.grpTables.Location = new System.Drawing.Point(12, 270);
            this.grpTables.Size = new System.Drawing.Size(250, 300);
            this.grpTables.Controls.Add(this.btnDeleteTable);
            this.grpTables.Controls.Add(this.btnRefreshTables);
            this.grpTables.Controls.Add(this.lstTables);

            // lstTables
            this.lstTables.Location = new System.Drawing.Point(15, 25);
            this.lstTables.Size = new System.Drawing.Size(220, 220);

            // btnRefreshTables
            this.btnRefreshTables.Text = "Refresh Tables";
            this.btnRefreshTables.Location = new System.Drawing.Point(15, 255);
            this.btnRefreshTables.Size = new System.Drawing.Size(105, 30);
            this.btnRefreshTables.Click += new System.EventHandler(this.btnRefreshTables_Click);

            // btnDeleteTable
            this.btnDeleteTable.Text = "Delete Table";
            this.btnDeleteTable.Location = new System.Drawing.Point(130, 255);
            this.btnDeleteTable.Size = new System.Drawing.Size(105, 30);
            this.btnDeleteTable.Click += new System.EventHandler(this.btnDeleteTable_Click);

            // grpData
            this.grpData.Text = "Data Table";
            this.grpData.Location = new System.Drawing.Point(270, 270);
            this.grpData.Size = new System.Drawing.Size(502, 300);
            this.grpData.Controls.Add(this.btnViewTable);
            this.grpData.Controls.Add(this.dgvData);

            // btnViewTable
            this.btnViewTable.Text = "View Table Data";
            this.btnViewTable.Location = new System.Drawing.Point(15, 25);
            this.btnViewTable.Size = new System.Drawing.Size(150, 30);
            this.btnViewTable.Click += new System.EventHandler(this.btnViewTable_Click);

            // dgvData
            this.dgvData.Location = new System.Drawing.Point(15, 65);
            this.dgvData.Size = new System.Drawing.Size(470, 220);
            this.dgvData.AllowUserToAddRows = false;
            this.dgvData.ReadOnly = true;

            // Form1
            this.ClientSize = new System.Drawing.Size(784, 590);
            this.Controls.Add(this.grpConnection);
            this.Controls.Add(this.grpCreateTable);
            this.Controls.Add(this.grpTables);
            this.Controls.Add(this.grpData);
            this.Text = "DynamoDB Local Manager";

            this.grpConnection.ResumeLayout(false);
            this.grpConnection.PerformLayout();
            this.grpCreateTable.ResumeLayout(false);
            this.grpCreateTable.PerformLayout();
            this.grpTables.ResumeLayout(false);
            this.grpData.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.GroupBox grpConnection;
        private System.Windows.Forms.Label lblAccessKey;
        private System.Windows.Forms.TextBox txtAccessKey;
        private System.Windows.Forms.Label lblSecretKey;
        private System.Windows.Forms.TextBox txtSecretKey;
        private System.Windows.Forms.Label lblRegion;
        private System.Windows.Forms.TextBox txtRegion;
        private System.Windows.Forms.Label lblServiceUrl;
        private System.Windows.Forms.TextBox txtServiceUrl;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Label lblStatus;

        private System.Windows.Forms.GroupBox grpCreateTable;
        private System.Windows.Forms.Label lblTableName;
        private System.Windows.Forms.TextBox txtTableName;
        private System.Windows.Forms.Label lblPartitionKey;
        private System.Windows.Forms.TextBox txtPartitionKey;
        private System.Windows.Forms.Button btnCreateTable;

        private System.Windows.Forms.GroupBox grpTables;
        private System.Windows.Forms.ListBox lstTables;
        private System.Windows.Forms.Button btnRefreshTables;
        private System.Windows.Forms.Button btnDeleteTable;

        private System.Windows.Forms.GroupBox grpData;
        private System.Windows.Forms.Button btnViewTable;
        private System.Windows.Forms.DataGridView dgvData;
    }
}