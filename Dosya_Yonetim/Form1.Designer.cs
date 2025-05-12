namespace Dosya_Yonetim
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

        private void InitializeComponent()
        {
            this.BackColor = System.Drawing.Color.FromArgb(30, 30, 30);
            this.ForeColor = System.Drawing.Color.White;
            this.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);

            btnAddFolder = new Button();
            btnAddFile = new Button();
            btnDelete = new Button();
            btnSearch = new Button();
            btnViewDetails = new Button();
            treeView1 = new TreeView();
            txtName = new TextBox();
            SuspendLayout();

            // Button Style
            var buttonStyle = new System.Drawing.Size(140, 40);

            // Add Folder Button
            btnAddFolder.Name = "btnAddFolder";
            btnAddFolder.Location = new System.Drawing.Point(30, 50);
            btnAddFolder.Size = buttonStyle;
            btnAddFolder.Text = "Add Folder";
            btnAddFolder.FlatStyle = FlatStyle.Flat;
            btnAddFolder.BackColor = System.Drawing.Color.FromArgb(50, 50, 50);
            btnAddFolder.ForeColor = System.Drawing.Color.White;
            btnAddFolder.Click += new System.EventHandler(this.btnAddFolder_Click);

            // Add File Button
            btnAddFile.Name = "btnAddFile";
            btnAddFile.Location = new System.Drawing.Point(30, 100);
            btnAddFile.Size = buttonStyle;
            btnAddFile.Text = "Add File";
            btnAddFile.FlatStyle = FlatStyle.Flat;
            btnAddFile.BackColor = System.Drawing.Color.FromArgb(50, 50, 50);
            btnAddFile.ForeColor = System.Drawing.Color.White;
            btnAddFile.Click += new System.EventHandler(this.btnAddFile_Click);

            // Delete Button
            btnDelete.Name = "btnDelete";
            btnDelete.Location = new System.Drawing.Point(30, 150);
            btnDelete.Size = buttonStyle;
            btnDelete.Text = "Delete";
            btnDelete.FlatStyle = FlatStyle.Flat;
            btnDelete.BackColor = System.Drawing.Color.FromArgb(50, 50, 50);
            btnDelete.ForeColor = System.Drawing.Color.White;
            btnDelete.Click += new System.EventHandler(this.btnDelete_Click);

            // Search Button
            btnSearch.Name = "btnSearch";
            btnSearch.Location = new System.Drawing.Point(30, 200);
            btnSearch.Size = buttonStyle;
            btnSearch.Text = "Search";
            btnSearch.FlatStyle = FlatStyle.Flat;
            btnSearch.BackColor = System.Drawing.Color.FromArgb(50, 50, 50);
            btnSearch.ForeColor = System.Drawing.Color.White;
            btnSearch.Click += new System.EventHandler(this.btnSearch_Click);

            // View Details Button
            btnViewDetails.Name = "btnViewDetails";
            btnViewDetails.Location = new System.Drawing.Point(30, 250);
            btnViewDetails.Size = buttonStyle;
            btnViewDetails.Text = "View Details";
            btnViewDetails.FlatStyle = FlatStyle.Flat;
            btnViewDetails.BackColor = System.Drawing.Color.FromArgb(50, 50, 50);
            btnViewDetails.ForeColor = System.Drawing.Color.White;
            btnViewDetails.Click += new System.EventHandler(this.btnViewDetails_Click);

            // TreeView
            treeView1.Name = "treeView1";
            treeView1.Location = new System.Drawing.Point(200, 50);
            treeView1.Size = new System.Drawing.Size(400, 300);
            treeView1.BackColor = System.Drawing.Color.FromArgb(40, 40, 40);
            treeView1.ForeColor = System.Drawing.Color.White;

            // TextBox
            txtName.Name = "txtName";
            txtName.Location = new System.Drawing.Point(30, 300);
            txtName.Size = new System.Drawing.Size(140, 30);
            txtName.BackColor = System.Drawing.Color.FromArgb(50, 50, 50);
            txtName.ForeColor = System.Drawing.Color.White;

            // Form1
            this.ClientSize = new System.Drawing.Size(650, 400);
            Controls.Add(txtName);
            Controls.Add(treeView1);
            Controls.Add(btnViewDetails);
            Controls.Add(btnSearch);
            Controls.Add(btnDelete);
            Controls.Add(btnAddFile);
            Controls.Add(btnAddFolder);
            Name = "Form1";
            Text = "Dosya Yönetim Sistemi";
            FormClosing += Form1_FormClosing;
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        private Button btnAddFolder;
        private Button btnAddFile;
        private Button btnDelete;
        private Button btnSearch;
        private Button btnViewDetails;
        private TreeView treeView1;
        private TextBox txtName;
    }
}
