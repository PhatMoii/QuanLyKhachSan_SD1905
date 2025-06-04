namespace GUI_QuanLyKhachSan
{
    partial class frmWelcom
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            guna2PictureBox1 = new Guna.UI2.WinForms.Guna2PictureBox();
            progressBar1 = new ProgressBar();
            errorProvider1 = new ErrorProvider(components);
            ((System.ComponentModel.ISupportInitialize)guna2PictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).BeginInit();
            SuspendLayout();
            // 
            // guna2PictureBox1
            // 
            guna2PictureBox1.CustomizableEdges = customizableEdges5;
            guna2PictureBox1.Image = Properties.Resources._53c1aa9a29089c56c519;
            guna2PictureBox1.ImageRotate = 0F;
            guna2PictureBox1.Location = new Point(1, -2);
            guna2PictureBox1.Name = "guna2PictureBox1";
            guna2PictureBox1.ShadowDecoration.CustomizableEdges = customizableEdges6;
            guna2PictureBox1.Size = new Size(802, 454);
            guna2PictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            guna2PictureBox1.TabIndex = 0;
            guna2PictureBox1.TabStop = false;
            // 
            // progressBar1
            // 
            progressBar1.Location = new Point(1, 448);
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new Size(802, 29);
            progressBar1.TabIndex = 1;
            progressBar1.Click += progressBar1_Click;
            // 
            // errorProvider1
            // 
            errorProvider1.ContainerControl = this;
            // 
            // frmWelcom
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 447);
            Controls.Add(progressBar1);
            Controls.Add(guna2PictureBox1);
            Name = "frmWelcom";
            Text = "frmWelcom";
            Load += frmWelcom_Load;
            ((System.ComponentModel.ISupportInitialize)guna2PictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Guna.UI2.WinForms.Guna2PictureBox guna2PictureBox1;
        private ProgressBar progressBar1;
        private ErrorProvider errorProvider1;
    }
}