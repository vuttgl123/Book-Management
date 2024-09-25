using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QuanLyThuVien.btnOutside
{
    public partial class AddDocGia : Form
    {
        private QLTTDocGia parentForm;

        public AddDocGia(QLTTDocGia parentForm)
        {
            InitializeComponent(); // Khởi tạo các thành phần giao diện của form
            this.parentForm = parentForm; // Lưu tham chiếu đến form cha trong biến thành viên
            this.KeyDown += new KeyEventHandler(Form_KeyDown); // Đăng ký sự kiện KeyDown cho form
            this.KeyPreview = true; // Cho phép form nhận sự kiện bàn phím trước khi các điều khiển con nhận

            // Thêm các mục 'Nam' và 'Nữ' vào ComboBox
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
                themDocGia(); // Gọi phương thức themDocGia khi phím Enter được nhấn
            }
        }

        public string madocgia { get => txtMaDG.Text; set => txtMaDG.Text = value; }
        public string hoten { get => txtHoTen.Text; set => txtHoTen.Text = value; }
        public string gioitinh { get => cbbGender.SelectedItem?.ToString(); set => cbbGender.SelectedItem = value; }
        public DateTime Ngaysinh { get => dateNgaySinh.Value; set => dateNgaySinh.Value = value; }
        public string diachi { get => txtAddress.Text; set => txtAddress.Text = value; }
        public string sdt { get => txtSDT.Text; set => txtSDT.Text = value; }
        public string email { get => txtEmail.Text; set => txtEmail.Text = value; }

        private void themDocGia()
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

                string connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Mở kết nối
                    connection.Open();

                    // Kiểm tra xem Mã độc giả đã tồn tại chưa
                    string checkQuery = "SELECT COUNT(*) FROM QUANLYDOCGIA WHERE MADOCGIA = @madg";
                    using (SqlCommand checkCommand = new SqlCommand(checkQuery, connection))
                    {
                        checkCommand.Parameters.AddWithValue("@madg", txtMaDG.Text);
                        int count = (int)checkCommand.ExecuteScalar();
                        if (count > 0)
                        {
                            MessageBox.Show("Mã độc giả đã tồn tại, vui lòng nhập mã khác.");
                            return;
                        }
                    }

                    // Thực hiện truy vấn INSERT để thêm thông tin của độc giả
                    string insertQuery = @"INSERT INTO QUANLYDOCGIA(MADOCGIA, TEN, GIOITINH, NGAYSINH, DIACHI, SDT, EMAIL) 
                                   VALUES (@madg, @hoten, @gioitinh, @ngaysinh, @diachi, @sdt, @email)";
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
                        MessageBox.Show("Thêm độc giả thành công!");
                        parentForm.UpdateDataGridViewFromDatabase();
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm dữ liệu: " + ex.Message);
            }
        }


        private void AddDocGia_Load(object sender, EventArgs e)
        {
            // Xử lý khi form được tải, nếu cần thiết
        }

        private void dateNgaySinh_ValueChanged(object sender, EventArgs e)
        {
            // Xử lý khi giá trị của DateTimePicker thay đổi, nếu cần thiết
        }

        private void cbbGender_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Xử lý khi giá trị của ComboBox thay đổi, nếu cần thiết
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            themDocGia();
        }
    }
}
