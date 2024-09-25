using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyThuVien
{
    public partial class Register : Form
    {
        private Form1 _form1;
        public Register(Form1 form1)
        {
            InitializeComponent();
            _form1 = form1;
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra xem các giá trị cần thiết đã được nhập và mật khẩu có khớp hay không
                if (string.IsNullOrEmpty(txtUsername.Text))
                {
                    MessageBox.Show("Vui lòng nhập tên tài khoản!");
                    return;
                }
                else if (string.IsNullOrEmpty(txtPassword.Text))
                {
                    MessageBox.Show("Vui lòng nhập mật khẩu!");
                    return;
                }
                else if (string.IsNullOrEmpty(txtRepassword.Text))
                {
                    MessageBox.Show("Vui lòng xác nhận mật khẩu!");
                    return;
                }
                else if (txtPassword.Text != txtRepassword.Text)
                {
                    MessageBox.Show("Mật khẩu không trùng khớp, vui lòng nhập lại!");
                    return;
                }

                // Kiểm tra ký tự đặc biệt trong tên tài khoản
                string pattern = @"[!@#\$%\^&\*\(\)_\+\-=\[\]\{\};':""\\|,.<>\/? ~`]";
                if (Regex.IsMatch(txtUsername.Text, pattern))
                {
                    MessageBox.Show("Tên người dùng không được chứa ký tự đặc biệt!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Chuẩn bị chuỗi kết nối
                string connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Mở kết nối
                    connection.Open();

                    // Thực hiện truy vấn INSERT để thêm dữ liệu vào bảng register và login
                    string registerQuery = "INSERT INTO register (username, password, repassword) VALUES (@username, @password, @repassword)";
                    string insertQuery = "INSERT INTO login (username, password, confirmPassword) VALUES (@username, @password, @password)";

                    // Tạo lệnh SQL cho bảng register
                    SqlCommand registerCommand = new SqlCommand(registerQuery, connection);
                    registerCommand.Parameters.AddWithValue("@username", txtUsername.Text);
                    registerCommand.Parameters.AddWithValue("@password", txtPassword.Text);
                    registerCommand.Parameters.AddWithValue("@repassword", txtRepassword.Text);

                    // Tạo lệnh SQL cho bảng login
                    SqlCommand loginCommand = new SqlCommand(insertQuery, connection);
                    loginCommand.Parameters.AddWithValue("@username", txtUsername.Text);
                    loginCommand.Parameters.AddWithValue("@password", txtPassword.Text);

                    // Bắt đầu giao dịch
                    SqlTransaction transaction = connection.BeginTransaction();
                    try
                    {
                        // Gán giao dịch cho các lệnh SQL
                        registerCommand.Transaction = transaction;
                        loginCommand.Transaction = transaction;

                        // Thực thi truy vấn đăng ký
                        int registerRowsAffected = registerCommand.ExecuteNonQuery();
                        if (registerRowsAffected == 1)
                        {
                            // Nếu đăng ký thành công, thực hiện thêm vào bảng login
                            int loginRowsAffected = loginCommand.ExecuteNonQuery();
                            if (loginRowsAffected == 1)
                            {
                                // Commit giao dịch nếu thành công
                                transaction.Commit();
                                MessageBox.Show("Đăng ký thành công!");
                                this.Hide();

                                // Mở form đăng nhập
                                Login form2 = new Login(_form1);
                                form2.ShowDialog();
                            }
                            else
                            {
                                // Rollback giao dịch nếu có lỗi khi thêm vào bảng login
                                transaction.Rollback();
                                MessageBox.Show("Đăng ký không thành công!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            // Rollback giao dịch nếu có lỗi khi thêm vào bảng register
                            transaction.Rollback();
                            MessageBox.Show("Đăng ký không thành công!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        // Rollback giao dịch nếu có lỗi xảy ra
                        transaction.Rollback();
                        MessageBox.Show("Đăng ký không thành công! " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Chưa kết nối tới CSDL", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void guna2ControlBox1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login f = new Login(_form1);
            f.ShowDialog();
            
        }

        private void txtRepassword_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
