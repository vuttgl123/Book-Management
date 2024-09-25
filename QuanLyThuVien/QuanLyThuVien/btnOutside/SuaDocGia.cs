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

namespace QuanLyThuVien.btnOutside
{
    public partial class SuaDocGia : Form
    {
        private QLTTDocGia parentForm;
        string connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;

        public SuaDocGia(QLTTDocGia parentForm)
        {
            InitializeComponent();
            this.parentForm = parentForm; // Lưu tham chiếu đến form cha trong biến thành viên
            this.KeyDown += new KeyEventHandler(Form_KeyDown); // Đăng ký sự kiện KeyDown cho form
            this.KeyPreview = true; // Cho phép form nhận sự kiện bàn phím trước khi các điều khiển con nhận
            // Thêm các mục "Nam" và "Nữ" vào ComboBox
            cbbGender.Items.Add("Nam");
            cbbGender.Items.Add("Nữ");
            // Đăng ký sự kiện SelectedIndexChanged cho ComboBox
            cbbGender.SelectedIndexChanged += new EventHandler(cbbGender_SelectedIndexChanged);
            // Tùy chọn: Chọn mục mặc định
            cbbGender.SelectedIndex = 0; // Chọn 'Nam' mặc định
        }
        private void Form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) // Kiểm tra nếu phím được nhấn là Enter
            {
                suadocgia(); // Gọi phương thức themDocGia khi phím Enter được nhấn
            }
        }

        public string madocgia { get => txtMaDG.Text; set => txtMaDG.Text = value; }
        public string hoten { get => txtHoTen.Text; set => txtHoTen.Text = value; }
        public string gioitinh { get => cbbGender.SelectedItem?.ToString(); set => cbbGender.SelectedItem = value; }
        public DateTime Ngaysinh { get => dateNgaySinh.Value; set => dateNgaySinh.Value = value; }
        public string diachi { get => txtAddress.Text; set => txtAddress.Text = value; }
        public string sdt { get => txtSDT.Text; set => txtSDT.Text = value; }
        public string email { get => txtEmail.Text; set => txtEmail.Text = value; }

        private void suadocgia()
        {
            try
            {
                // Kiểm tra xem các giá trị cần thiết đã được chọn hay không
                if (string.IsNullOrEmpty(txtMaDG.Text))
                {
                    MessageBox.Show("Vui lòng nhập 'Mã độc giả' !");
                    return;
                }
                if (string.IsNullOrEmpty(txtHoTen.Text))
                {
                    MessageBox.Show("Vui lòng nhập 'Họ tên' !");
                    return;
                }
                if (string.IsNullOrEmpty(txtAddress.Text))
                {
                    MessageBox.Show("Vui lòng nhập 'Địa chỉ' !");
                    return;
                }
                if (string.IsNullOrEmpty(txtSDT.Text))
                {
                    MessageBox.Show("Vui lòng nhập 'SĐT' !");
                    return;
                }
                if (string.IsNullOrEmpty(cbbGender.Text))
                {
                    MessageBox.Show("Vui lòng chọn 'Giới tính' !");
                    return;
                }
                if (string.IsNullOrEmpty(txtEmail.Text))
                {
                    MessageBox.Show("Vui lòng nhập 'Email' !");
                    return;
                }

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Mở kết nối
                    connection.Open();

                    // Thực hiện truy vấn INSERT để sửa thông tin của độc giả
                    string insertQuery = @"UPDATE QUANLYDOCGIA SET TEN = @hoten, GIOITINH = @gioitinh, NGAYSINH = @ngaysinh, DIACHI = @diachi, SDT = @sdt, EMAIL = @email WHERE MADOCGIA = @madg";
                    using (SqlCommand command = new SqlCommand(insertQuery, connection))
                    {
                        command.Parameters.AddWithValue("@madg", txtMaDG.Text);
                        command.Parameters.AddWithValue("@hoten", txtHoTen.Text);
                        command.Parameters.AddWithValue("@gioitinh", cbbGender.Text);
                        command.Parameters.AddWithValue("@ngaysinh", dateNgaySinh.Value); // Sử dụng Value thay vì Text cho DateTimePicker
                        command.Parameters.AddWithValue("@diachi", txtAddress.Text);
                        command.Parameters.AddWithValue("@sdt", txtSDT.Text);
                        command.Parameters.AddWithValue("@email", txtEmail.Text);

                        // Thực hiện truy vấn INSERT
                        command.ExecuteNonQuery();
                        MessageBox.Show("Sửa độc giả thành công!");
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
        
        private void txtMaDG_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            suadocgia();
        }

        private void cbbGender_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }
    }
}
