namespace DynamoDBUI.Forms
{
    partial class RenamePromptForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblPrompt = new System.Windows.Forms.Label();
            this.txtValue = new System.Windows.Forms.TextBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();

            this.lblPrompt.Text = "Name:";
            this.lblPrompt.Location = new System.Drawing.Point(15, 18);
            this.lblPrompt.Size = new System.Drawing.Size(280, 20);

            this.txtValue.Location = new System.Drawing.Point(15, 42);
            this.txtValue.Size = new System.Drawing.Size(280, 22);

            this.btnOk.Text = "OK";
            this.btnOk.Location = new System.Drawing.Point(130, 82);
            this.btnOk.Size = new System.Drawing.Size(80, 28);
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);

            this.btnCancel.Text = "Cancel";
            this.btnCancel.Location = new System.Drawing.Point(215, 82);
            this.btnCancel.Size = new System.Drawing.Size(80, 28);
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;

            this.AcceptButton = this.btnOk;
            this.CancelButton = this.btnCancel;

            this.ClientSize = new System.Drawing.Size(310, 122);
            this.Controls.Add(this.lblPrompt);
            this.Controls.Add(this.txtValue);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.ShowInTaskbar = false;
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label lblPrompt;
        private System.Windows.Forms.TextBox txtValue;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
    }
}
