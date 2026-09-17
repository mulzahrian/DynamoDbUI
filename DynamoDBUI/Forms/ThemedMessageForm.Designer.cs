namespace DynamoDBUI.Forms
{
    partial class ThemedMessageForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.iconPanel = new System.Windows.Forms.Panel();
            this.lblMessage = new System.Windows.Forms.Label();
            this.btnPrimary = new System.Windows.Forms.Button();
            this.btnSecondary = new System.Windows.Forms.Button();
            this.SuspendLayout();

            // iconPanel (lingkaran + glyph digambar manual di Paint event)
            this.iconPanel.Location = new System.Drawing.Point(24, 26);
            this.iconPanel.Size = new System.Drawing.Size(44, 44);
            this.iconPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.iconPanel_Paint);

            // lblMessage
            this.lblMessage.Location = new System.Drawing.Point(84, 28);
            this.lblMessage.Size = new System.Drawing.Size(300, 60);
            this.lblMessage.Font = new System.Drawing.Font("Segoe UI", 9.5F);

            // btnPrimary
            this.btnPrimary.Size = new System.Drawing.Size(88, 30);
            this.btnPrimary.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrimary.FlatAppearance.BorderSize = 0;
            this.btnPrimary.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPrimary.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnPrimary.TabStop = true;

            // btnSecondary
            this.btnSecondary.Size = new System.Drawing.Size(88, 30);
            this.btnSecondary.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSecondary.FlatAppearance.BorderSize = 0;
            this.btnSecondary.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSecondary.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnSecondary.TabStop = true;

            // ThemedMessageForm
            this.Controls.Add(this.iconPanel);
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.btnPrimary);
            this.Controls.Add(this.btnSecondary);
            this.ClientSize = new System.Drawing.Size(404, 160);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.ShowInTaskbar = false;
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Panel iconPanel;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.Button btnPrimary;
        private System.Windows.Forms.Button btnSecondary;
    }
}
