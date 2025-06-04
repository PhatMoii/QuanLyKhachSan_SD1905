using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace GUI_QuanLyKhachSan
{
    public partial class frmWelcom : Form
    {
        public frmWelcom()
        {
            InitializeComponent();
            this.Shown += frmWelcome_Shown;
        }
        private async void frmWelcome_Shown(object sender, EventArgs e)
        {
            await Task.Delay(3000); // Đợi 3 giây

            this.Hide(); // Ẩn form Welcome

            using (frmLogin loginForm = new frmLogin())
            {
                loginForm.ShowDialog();
            }

            this.Close(); // Đóng Welcome sau khi login
        }   

        private void frmWelcom_Load(object sender, EventArgs e)
        {

        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }
    }
}
