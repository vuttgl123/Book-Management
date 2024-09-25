using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyThuVien
{
    public partial class ThayDoiThongTinTK : Form
    {
        public string idLogin { get => txtID.Text; set => txtID.Text = value; }
        public string Username { get => txtUsername.Text; set => txtUsername.Text = value; }
        public string Password { get => txtPassword.Text; set => txtPassword.Text = value; }
        public string ConfirmPassword { get => txtConfirm.Text; set => txtConfirm.Text = value; }

        // Khai báo một biến thành viên của lớp để giữ tham chiếu đến form cha
        private QLHT_Login parentForm;

        // Constructor của form ThayDoiThongTinTK
        public ThayDoiThongTinTK(QLHT_Login parentForm)
        {
            InitializeComponent(); // Khởi tạo các thành phần giao diện của form
            this.parentForm = parentForm; // Lưu tham chiếu đến form cha trong biến thành viên
            this.KeyDown += new KeyEventHandler(Form_KeyDown); // Đăng ký sự kiện KeyDown cho form
            this.KeyPreview = true; // Cho phép form nhận sự kiện bàn phím trước khi các điều khiển con nhận
        }
        private void Form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) // Kiểm tra nếu phím được nhấn là Enter
            {
                UpdateLogin(); // Gọi phương thức UpdateLogin khi phím Enter được nhấn
            }
        }

        private void UpdateLogin()
        {
            try
            {
                // Kiểm tra xem các giá trị cần thiết đã được chọn hay không
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
                else if (string.IsNullOrEmpty(txtConfirm.Text))
                {
                    MessageBox.Show("Vui lòng xác nhận mật khẩu!");
                    return;
                }
                else if (txtPassword.Text != txtConfirm.Text)
                {
                    MessageBox.Show("Mật khẩu không trùng khớp, Vui lòng nhập lại!");
                    return;
                }

                string connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Mở kết nối
                    connection.Open();

                    // Thực hiện truy vấn UPDATE để cập nhật thông tin của tài khoản
                    string updateQuery = @"UPDATE login SET username = @username, password = @password, confirmPassword = @password WHERE idLogin = @ID";
                    using (SqlCommand command = new SqlCommand(updateQuery, connection))
                    {
                        command.Parameters.AddWithValue("@ID", txtID.Text);
                        command.Parameters.AddWithValue("@username", txtUsername.Text);
                        command.Parameters.AddWithValue("@password", txtPassword.Text);

                        // Thực hiện truy vấn UPDATE
                        command.ExecuteNonQuery();
                        MessageBox.Show("Cập nhật thành công!");
                        parentForm.UpdateDataGridViewFromDatabase();
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi sửa dữ liệu: " + ex.Message);
            }

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            UpdateLogin();
        }

        private void txtConfirm_TextChanged(object sender, EventArgs e)
        {

        }

        private void ThayDoiThongTinTK_Load(object sender, EventArgs e)
        {

        }
    }
}
