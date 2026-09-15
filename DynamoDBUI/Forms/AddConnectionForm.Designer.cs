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
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();

            this.lblName.Text = "Connection Name:";
            this.lblName.Location = new System.Drawing.Point(15, 20);
            this.lblName.Size = new System.Drawing.Size(150, 20);
            this.txtName.Location = new System.Drawing.Point(180, 17);
            this.txtName.Size = new System.Drawing.Size(200, 20);
            this.txtName.Text = "Local DynamoDB";

            this.lblAccessKey.Text = "AWS Access Key ID:";
            this.lblAccessKey.Location = new System.Drawing.Point(15, 55);
            this.lblAccessKey.Size = new System.Drawing.Size(150, 20);
            this.txtAccessKey.Location = new System.Drawing.Point(180, 52);
            this.txtAccessKey.Size = new System.Drawing.Size(200, 20);
            this.txtAccessKey.Text = "local";

            this.lblSecretKey.Text = "AWS Secret Access Key:";
            this.lblSecretKey.Location = new System.Drawing.Point(15, 90);
            this.lblSecretKey.Size = new System.Drawing.Size(150, 20);
            this.txtSecretKey.Location = new System.Drawing.Point(180, 87);
            this.txtSecretKey.Size = new System.Drawing.Size(200, 20);
            this.txtSecretKey.Text = "local";
            this.txtSecretKey.UseSystemPasswordChar = true;

            this.lblRegion.Text = "Default Region Name:";
            this.lblRegion.Location = new System.Drawing.Point(15, 125);
            this.lblRegion.Size = new System.Drawing.Size(150, 20);
            this.txtRegion.Location = new System.Drawing.Point(180, 122);
            this.txtRegion.Size = new System.Drawing.Size(200, 20);
            this.txtRegion.Text = "ap-southeast-1";

            this.lblServiceUrl.Text = "Service URL:";
            this.lblServiceUrl.Location = new System.Drawing.Point(15, 160);
            this.lblServiceUrl.Size = new System.Drawing.Size(150, 20);
            this.txtServiceUrl.Location = new System.Drawing.Point(180, 157);
            this.txtServiceUrl.Size = new System.Drawing.Size(200, 20);
            this.txtServiceUrl.Text = "http://localhost:8000";

            this.btnOk.Text = "OK";
            this.btnOk.Location = new System.Drawing.Point(180, 200);
            this.btnOk.Size = new System.Drawing.Size(90, 30);
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);

            this.btnCancel.Text = "Cancel";
            this.btnCancel.Location = new System.Drawing.Point(290, 200);
            this.btnCancel.Size = new System.Drawing.Size(90, 30);
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;

            this.ClientSize = new System.Drawing.Size(410, 250);
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
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancel);
            this.Text = "Add Connection";
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
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
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
    }
}