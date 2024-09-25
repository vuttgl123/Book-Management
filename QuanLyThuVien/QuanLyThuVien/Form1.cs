using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyThuVien
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private Form1 form1;
        private Form currentFormChild;
        private Form1 _form1;

        private void OpenChildForm(Form childForm)
        {
            if(currentFormChild != null)
            {
                currentFormChild.Close();
            }
            currentFormChild = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panel_Body.Controls.Add(childForm);
            panel_Body.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

        // Trong Form1

        // this : truyen form 1 vao form 2 
        private void OpenForm2()
        {
            Login form2 = new Login(this);
            form2.ShowDialog();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenChildForm(new QLSach());
        }

        internal void EnableButtonsAfterLogin()
        {
            //cho phép thực hiện click vào các nút button
            btnHome.Enabled = true;
            btnQuanLySach.Enabled = true;
            btnDanhMuc.Enabled = true;
            btnDocGia.Enabled = true;
            btnThongKe.Enabled = true;
            btnLogout.Enabled = true;
            btnLogout.Visible = true;
            btnQLHT.Enabled = true;
            btnLogin.Visible = false;
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            try
            {
                // Đóng Form Login
                this.Close();

                // Hiển thị lại Form1
                Form1 form1 = new Form1();
                if (form1 != null)
                {
                    form1.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi logout: " + ex.Message);
            }
        }


        private void panel_Body_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            if (currentFormChild != null)
            {
                currentFormChild.Close();
            }
        }

        private void btnDanhMuc_Click(object sender, EventArgs e)
        {
            OpenChildForm(new QLMuonTraSach());
        }

        //Quản lý thủ thư
        private void btnQuanLyPhieu_Click(object sender, EventArgs e)
        {

        }

        private void btnDocGia_Click(object sender, EventArgs e)
        {
            OpenChildForm(new QLTTDocGia());
        }

        private void btnThongKe_Click(object sender, EventArgs e)
        {
            OpenChildForm(new BaoCaoThongKe());
        }

        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            this.Hide();
            OpenForm2();
            this.Show();
        }

        private void btnQLHT_Click(object sender, EventArgs e)
        {
            OpenChildForm(new QLHT_Login());
        }

        //nút thoát form1
        private void guna2ControlBox1_Click(object sender, EventArgs e)
        {

        }

        
    }
}
