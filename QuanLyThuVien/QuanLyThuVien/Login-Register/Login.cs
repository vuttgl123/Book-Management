using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Text.RegularExpressions;
namespace QuanLyThuVien
{
    public partial class Login : Form
    {
        private string connectionString;
        private Form1 _form1;

        public Login(Form1 form1)
        {
            InitializeComponent();
            this.KeyPreview = true;
            this._form1 = form1;
            this.KeyDown += new KeyEventHandler(Form_KeyDown);
            this.KeyPreview = true;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Register frm3 = new Register(_form1);
            frm3.ShowDialog();
            frm3 = null;
            this.Show();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            this.KeyDown += new KeyEventHandler(Form_KeyDown);
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        // Xử lý sự kiện KeyDown
        private void Form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) // Kiểm tra nếu phím được nhấn là Enter
            {
                Loginn(); // Gọi phương thức đăng nhập
            }
        }

        // Phương thức đăng nhập
        private void Loginn()
        {
            try
            {
                connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
                // Kiểm tra trạng thái kết nối trước khi mở kết nối mới
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close(); // Đóng kết nối hiện tại nếu đã mở
                    }

                    string query = "SELECT * FROM login WHERE username = @username AND password = @password";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@username", txtusername.Text);
                    command.Parameters.AddWithValue("@password", txtpassword.Text);

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    // Regex pattern for special characters
                    string pattern = @"[!@#%^%&$%*&^*%(""?<>{})]";

                    // Check if the username contains special characters
                    if (Regex.IsMatch(txtusername.Text, pattern))
                    {
                        MessageBox.Show("Chuỗi chứa kí tự đặc biệt", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        if (reader.HasRows)
                        {
                            _form1.EnableButtonsAfterLogin();
                            // ẩn form 2
                            this.Hide();
                            // Mở form mới
                            _form1.ShowDialog();
                            this.Close();
                        }
                        else
                        {
                            // Hiển thị thông báo lỗi
                            MessageBox.Show("Tên người dùng hoặc mật khẩu không chính xác.", "Lỗi đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                
            }
        }



        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtpassword_TextChanged(object sender, KeyPressEventArgs e)
        {

        }

        private void label2_Click_1(object sender, EventArgs e)
        {

        }

        private void txtpassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {

        }


        private void guna2Button1_Click_1(object sender, EventArgs e)
        {
            Loginn();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            this.Hide();
            Register f = new Register(_form1);
            f.ShowDialog();
        }
    }
}
