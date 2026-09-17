namespace DynamoDBUI.Forms
{
    partial class AddConnectionForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblName = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblAccessKey = new System.Windows.Forms.Label();
            this.txtAccessKey = new System.Windows.Forms.TextBox();
            this.lblSecretKey = new System.Windows.Forms.Label();
            this.txtSecretKey = new System.Windows.Forms.TextBox();
            this.lblRegion = new System.Windows.Forms.Label();
            this.txtRegion = new System.Windows.Forms.TextBox();
            this.lblServiceUrl = new System.Windows.Forms.Label();
            this.txtServiceUrl = new System.Windows.Forms.TextBox();
            this.btnTest = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();

            var fieldFont = new System.Drawing.Font("Segoe UI", 9.5F);
            var labelFont = new System.Drawing.Font("Segoe UI", 9F);

            this.lblName.Text = "Connection Name:";
            this.lblName.Location = new System.Drawing.Point(15, 20);
            this.lblName.Size = new System.Drawing.Size(140, 20);
            this.lblName.Font = labelFont;
            this.txtName.Location = new System.Drawing.Point(165, 17);
            this.txtName.Size = new System.Drawing.Size(240, 22);
            this.txtName.Font = fieldFont;
            this.txtName.Text = "Local DynamoDB";

            this.lblAccessKey.Text = "AWS Access Key ID:";
            this.lblAccessKey.Location = new System.Drawing.Point(15, 55);
            this.lblAccessKey.Size = new System.Drawing.Size(140, 20);
            this.lblAccessKey.Font = labelFont;
            this.txtAccessKey.Location = new System.Drawing.Point(165, 52);
            this.txtAccessKey.Size = new System.Drawing.Size(240, 22);
            this.txtAccessKey.Font = fieldFont;
            this.txtAccessKey.Text = "local";

            this.lblSecretKey.Text = "AWS Secret Access Key:";
            this.lblSecretKey.Location = new System.Drawing.Point(15, 90);
            this.lblSecretKey.Size = new System.Drawing.Size(140, 20);
            this.lblSecretKey.Font = labelFont;
            this.txtSecretKey.Location = new System.Drawing.Point(165, 87);
            this.txtSecretKey.Size = new System.Drawing.Size(240, 22);
            this.txtSecretKey.Font = fieldFont;
            this.txtSecretKey.Text = "local";
            this.txtSecretKey.UseSystemPasswordChar = true;

            this.lblRegion.Text = "Default Region Name:";
            this.lblRegion.Location = new System.Drawing.Point(15, 125);
            this.lblRegion.Size = new System.Drawing.Size(140, 20);
            this.lblRegion.Font = labelFont;
            this.txtRegion.Location = new System.Drawing.Point(165, 122);
            this.txtRegion.Size = new System.Drawing.Size(240, 22);
            this.txtRegion.Font = fieldFont;
            this.txtRegion.Text = "ap-southeast-1";

            this.lblServiceUrl.Text = "Service URL:";
            this.lblServiceUrl.Location = new System.Drawing.Point(15, 160);
            this.lblServiceUrl.Size = new System.Drawing.Size(140, 20);
            this.lblServiceUrl.Font = labelFont;
            this.txtServiceUrl.Location = new System.Drawing.Point(165, 157);
            this.txtServiceUrl.Size = new System.Drawing.Size(240, 22);
            this.txtServiceUrl.Font = fieldFont;
            this.txtServiceUrl.Text = "http://localhost:8000";

            this.btnTest.Text = "Test Connection";
            this.btnTest.Location = new System.Drawing.Point(165, 195);
            this.btnTest.Size = new System.Drawing.Size(150, 30);
            this.btnTest.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTest.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTest.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);

            this.lblStatus.Location = new System.Drawing.Point(15, 235);
            this.lblStatus.Size = new System.Drawing.Size(390, 42);
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 8.5F);

            this.btnOk.Text = "Add Connection";
            this.btnOk.Location = new System.Drawing.Point(200, 285);
            this.btnOk.Size = new System.Drawing.Size(115, 32);
            this.btnOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOk.FlatAppearance.BorderSize = 0;
            this.btnOk.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOk.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);

            this.btnCancel.Text = "Cancel";
            this.btnCancel.Location = new System.Drawing.Point(320, 285);
            this.btnCancel.Size = new System.Drawing.Size(85, 32);
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;

            this.AcceptButton = this.btnOk;
            this.CancelButton = this.btnCancel;

            this.ClientSize = new System.Drawing.Size(420, 335);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.lblAccessKey);
            this.Controls.Add(this.txtAccessKey);
            this.Controls.Add(this.lblSecretKey);
            this.Controls.Add(this.txtSecretKey);
            this.Controls.Add(this.lblRegion);
            this.Controls.Add(this.txtRegion);
            this.Controls.Add(this.lblServiceUrl);
            this.Controls.Add(this.txtServiceUrl);
            this.Controls.Add(this.btnTest);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancel);
            this.Text = "Add Connection";
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.ShowInTaskbar = false;
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblAccessKey;
        private System.Windows.Forms.TextBox txtAccessKey;
        private System.Windows.Forms.Label lblSecretKey;
        private System.Windows.Forms.TextBox txtSecretKey;
        private System.Windows.Forms.Label lblRegion;
        private System.Windows.Forms.TextBox txtRegion;
        private System.Windows.Forms.Label lblServiceUrl;
        private System.Windows.Forms.TextBox txtServiceUrl;
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
    }
}
