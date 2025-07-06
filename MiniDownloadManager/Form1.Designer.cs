namespace MiniDownloadManager
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.Button btnDownload;
        private System.Windows.Forms.ProgressBar progressBar;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            labelTitle = new Label();
            pictureBox = new PictureBox();
            btnDownload = new Button();
            progressBar = new ProgressBar();
            ((System.ComponentModel.ISupportInitialize)pictureBox).BeginInit();
            SuspendLayout();
            // 
            // labelTitle
            // 
            labelTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            labelTitle.Location = new Point(12, 9);
            labelTitle.Name = "labelTitle";
            labelTitle.Size = new Size(360, 35);
            labelTitle.TabIndex = 0;
            labelTitle.Text = "Loading...";
            labelTitle.TextAlign = ContentAlignment.MiddleCenter;
            labelTitle.Click += labelTitle_Click;
            // 
            // pictureBox
            // 
            pictureBox.Location = new Point(12, 47);
            pictureBox.Name = "pictureBox";
            pictureBox.Size = new Size(360, 180);
            pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox.TabIndex = 1;
            pictureBox.TabStop = false;
            // 
            // btnDownload
            // 
            btnDownload.Font = new Font("Segoe UI", 10F);
            btnDownload.Location = new Point(135, 233);
            btnDownload.Name = "btnDownload";
            btnDownload.Size = new Size(120, 35);
            btnDownload.TabIndex = 2;
            btnDownload.Text = "Download File";
            btnDownload.UseVisualStyleBackColor = true;
            btnDownload.Click += btnDownload_Click;
            // 
            // progressBar
            // 
            progressBar.Location = new Point(12, 280);
            progressBar.Name = "progressBar";
            progressBar.Size = new Size(360, 20);
            progressBar.TabIndex = 3;
            // 
            // Form1
            // 
            ClientSize = new Size(384, 311);
            Controls.Add(progressBar);
            Controls.Add(btnDownload);
            Controls.Add(pictureBox);
            Controls.Add(labelTitle);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "Form1";
            Text = "Mini Download Manager";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox).EndInit();
            ResumeLayout(false);
        }
    }
}