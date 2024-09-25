using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using Image = System.Drawing.Image;

namespace QuanLyThuVien
{
    public partial class QLSach : Form
    {
        public QLSach()
        {
            InitializeComponent();
            LoadStatus();
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private string connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
        private void btnShow_Click(object sender, EventArgs e)
        {
            show();
        }


        public List<string> GetAllStatus()
        {
            List<string> statusList = new List<string>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT DISTINCT TRANGTHAI FROM QUANLYSACH";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string status = reader["TRANGTHAI"].ToString();
                            statusList.Add(status);
                        }
                    }
                }
            }

            return statusList;
        }
        private void LoadStatus()
        {
            List<string> statuses = GetAllStatus();
            cbbTrangThai.Items.Clear();
            foreach (string status in statuses)
            {
                cbbTrangThai.Items.Add(status);
            }
        }

        private void show()
        {
            try
            {
                // Tạo kết nối SQL
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Mở kết nối
                    connection.Open();

                    // Tạo câu truy vấn SQL để thay đổi các tên cột từ bảng
                    string query = "SELECT MASACH as ID, TENSACH as 'Tên Sách', LOAISACH as 'Loại Sách',  NXB as NXB, GIA as Giá, TENTACGIA as 'Tác Giả', SOLUONG as SL, TRANGTHAI as TT, HINHANH as 'Hình ảnh' FROM QUANLYSACH";

                    // Tạo đối tượng SqlDataAdapter để lấy dữ liệu từ câu truy vấn
                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                    {
                        // Tạo đối tượng DataTable để lưu trữ dữ liệu từ bảng
                        DataTable dataTable = new DataTable();

                        // Đổ dữ liệu từ SqlDataAdapter vào DataTable
                        adapter.Fill(dataTable);

                        // Gán DataTable làm nguồn dữ liệu cho DataGridView
                        tableTTSach.DataSource = dataTable;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
        private void QLSach_Load(object sender, EventArgs e)
        {

        }

        private void tableTTSach_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra xem có hàng nào được chọn hay không
                if (tableTTSach.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Vui lòng chọn hàng cần xóa!");
                    return;
                }

                // Lấy mã phiếu mượn của hàng được chọn
                string maSach = tableTTSach.SelectedRows[0].Cells[0].Value.ToString();

                string connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Mở kết nối
                    connection.Open();

                    // Xóa dữ liệu từ cơ sở dữ liệu dựa trên mã phiếu mượn đã chọn
                    string delQuery = "DELETE FROM QUANLYSACH WHERE MASACH = @maSach";
                    using (SqlCommand command = new SqlCommand(delQuery, connection))
                    {
                        command.Parameters.AddWithValue("@maSach", maSach);
                        command.ExecuteNonQuery();
                    }
                }

                // Cập nhật lại DataGridView sau khi xóa
                show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa dữ liệu: " + ex.Message);
            }
        }
        private void btnFix_Click(object sender, EventArgs e)
        {
            try
            {
                //// Kiểm tra xem trường mã sách có đang được sửa không
                //if (isMaSachEdited)
                //{
                //    MessageBox.Show("Không được phép thay đổi 'Mã sách' !");
                //    return;
                //}

                //kiểm tra các textbox đã nhập chưa!
                if (string.IsNullOrEmpty(txtMaSach.Text))
                {
                    MessageBox.Show("Vui lòng nhập Mã sách!");
                    return;
                }
                else if (string.IsNullOrEmpty(txtTenSach.Text))
                {
                    MessageBox.Show("Vui lòng nhập Tên sách!");
                    return;
                }
                else if (string.IsNullOrEmpty(txtLoaiSach.Text))
                {
                    MessageBox.Show("Vui lòng nhập Loại sách!");
                    return;
                }
                else if (string.IsNullOrEmpty(txtNXB.Text))
                {
                    MessageBox.Show("Vui lòng nhập Nhà xuất bản!");
                    return;
                }
                else if (string.IsNullOrEmpty(txtGia.Text))
                {
                    MessageBox.Show("Vui lòng nhập Giá sách!");
                    return;
                }
                else if (string.IsNullOrEmpty(txtTenTG.Text))
                {
                    MessageBox.Show("Vui lòng nhập Tên tác giả!");
                    return;
                }
                else if (string.IsNullOrEmpty(txtSoLuong.Text))
                {
                    MessageBox.Show("Vui lòng nhập Số lượng!");
                    return;
                }
                else if (string.IsNullOrEmpty(cbbTrangThai.Text))
                {
                    MessageBox.Show("Vui lòng chọn Trạng thái sách!");
                    return;
                }


                byte[] imageBytes = null;

                // Chuyển đổi hình ảnh từ PictureBox thành mảng byte
                if (imageBook.Image != null)
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        imageBook.Image.Save(ms, imageBook.Image.RawFormat);
                        imageBytes = ms.ToArray();
                    }
                }
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Mở kết nối
                    connection.Open();
                    // Thực hiện truy vấn UPDATE để cập nhật thông tin của phiếu mượn
                    string updateQuery = "UPDATE QUANLYSACH SET TENSACH = @tensach, LOAISACH = @loaisach, NXB = @nxb, GIA = @gia, TENTACGIA = @tentacgia, SOLUONG = @soluong, TRANGTHAI = @trangthai, HINHANH = @image WHERE MASACH = @masach";

                    using (SqlCommand command = new SqlCommand(updateQuery, connection))
                    {
                        command.Parameters.AddWithValue("@masach", txtMaSach.Text);
                        command.Parameters.AddWithValue("@tensach", txtTenSach.Text);
                        command.Parameters.AddWithValue("@loaisach", txtLoaiSach.Text);
                        command.Parameters.AddWithValue("@nxb", txtNXB.Text);
                        command.Parameters.AddWithValue("@gia", txtGia.Text);
                        command.Parameters.AddWithValue("@tentacgia", txtTenTG.Text);
                        command.Parameters.AddWithValue("@soluong", txtSoLuong.Text);
                        command.Parameters.AddWithValue("@trangthai", cbbTrangThai.Text);
                        command.Parameters.AddWithValue("@image", imageBytes == null ? (object)DBNull.Value : imageBytes);
                        // Thực hiện truy vấn UPDATE
                        command.ExecuteNonQuery();
                    }
                    // Sau khi cập nhật thành công, cập nhật lại hiển thị trong DataGridView
                    btnShow_Click(sender, e);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra các textbox đã nhập chưa!
                if (string.IsNullOrEmpty(txtMaSach.Text))
                {
                    MessageBox.Show("Vui lòng nhập Mã sách!");
                    return;
                }
                else if (string.IsNullOrEmpty(txtTenSach.Text))
                {
                    MessageBox.Show("Vui lòng nhập Tên sách!");
                    return;
                }
                else if (string.IsNullOrEmpty(txtLoaiSach.Text))
                {
                    MessageBox.Show("Vui lòng nhập Loại sách!");
                    return;
                }
                else if (string.IsNullOrEmpty(txtNXB.Text))
                {
                    MessageBox.Show("Vui lòng nhập Nhà xuất bản!");
                    return;
                }
                else if (string.IsNullOrEmpty(txtGia.Text))
                {
                    MessageBox.Show("Vui lòng nhập Giá sách!");
                    return;
                }
                else if (string.IsNullOrEmpty(txtTenTG.Text))
                {
                    MessageBox.Show("Vui lòng nhập Tên tác giả!");
                    return;
                }
                else if (string.IsNullOrEmpty(txtSoLuong.Text))
                {
                    MessageBox.Show("Vui lòng nhập Số lượng!");
                    return;
                }
                else if (string.IsNullOrEmpty(cbbTrangThai.Text))
                {
                    MessageBox.Show("Vui lòng chọn Trạng thái sách!");
                    return;
                }

                byte[] imageBytes = null;

                // Chuyển đổi hình ảnh từ PictureBox thành mảng byte
                if (imageBook.Image != null)
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        imageBook.Image.Save(ms, imageBook.Image.RawFormat);
                        imageBytes = ms.ToArray();
                    }
                }

                string connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Mở kết nối
                    connection.Open();

                    // Kiểm tra xem mã sách đã tồn tại hay chưa
                    string checkQuery = "SELECT COUNT(*) FROM QUANLYSACH WHERE MASACH = @masach";
                    using (SqlCommand checkCommand = new SqlCommand(checkQuery, connection))
                    {
                        checkCommand.Parameters.AddWithValue("@masach", txtMaSach.Text);
                        int count = (int)checkCommand.ExecuteScalar();
                        if (count > 0)
                        {
                            MessageBox.Show("Mã sách đã tồn tại!");
                            return;
                        }
                    }

                    // Thêm dữ liệu vào cơ sở dữ liệu
                    string insertQuery = "INSERT INTO QUANLYSACH(MASACH, TENSACH, LOAISACH, NXB, GIA, TENTACGIA, SOLUONG, TRANGTHAI, HINHANH) VALUES (@masach, @tensach, @loaisach, @nxb, @gia, @tentacgia, @soluong, @trangthai, @image)";
                    using (SqlCommand command = new SqlCommand(insertQuery, connection))
                    {
                        command.Parameters.AddWithValue("@masach", txtMaSach.Text);
                        command.Parameters.AddWithValue("@tensach", txtTenSach.Text);
                        command.Parameters.AddWithValue("@loaisach", txtLoaiSach.Text);
                        command.Parameters.AddWithValue("@nxb", txtNXB.Text);
                        command.Parameters.AddWithValue("@gia", txtGia.Text);
                        command.Parameters.AddWithValue("@tentacgia", txtTenTG.Text);
                        command.Parameters.AddWithValue("@soluong", txtSoLuong.Text);
                        command.Parameters.AddWithValue("@trangthai", cbbTrangThai.Text);
                        command.Parameters.AddWithValue("@image", imageBytes == null ? (object)DBNull.Value : imageBytes);

                        // Thực hiện truy vấn INSERT
                        command.ExecuteNonQuery();
                    }

                    // Hiển thị lại dữ liệu
                    show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void tableTTSach_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && e.RowIndex < tableTTSach.Rows.Count - 1)
                {
                    DataGridViewRow row = tableTTSach.Rows[e.RowIndex];

                    // Kiểm tra giá trị của từng ô trước khi chuyển đổi
                    string maSach = row.Cells[0].Value.ToString();
                    string tenSach = row.Cells[1].Value.ToString();
                    string loaiSach = row.Cells[2].Value.ToString();
                    string nxb = row.Cells[3].Value.ToString();
                    string gia = row.Cells[4].Value.ToString();
                    string tenTG = row.Cells[5].Value.ToString();
                    string soluong = row.Cells[6].Value.ToString();
                    string trangthai = row.Cells[7].Value.ToString();
                    // Lấy dữ liệu hình ảnh từ cơ sở dữ liệu và chuyển đổi thành Image
                    byte[] hinhanh = row.Cells[8].Value as byte[];
                    Image image = null;

                    if (hinhanh != null && hinhanh.Length > 0)
                    {
                        using (MemoryStream ms = new MemoryStream(hinhanh))
                        {
                            image = Image.FromStream(ms);
                        }
                    }

                    // Hiển thị giá trị của các ô trong các TextBox hoặc ComboBox tương ứng
                    txtMaSach.Text = maSach;
                    txtTenSach.Text = tenSach;
                    txtLoaiSach.Text = loaiSach;
                    txtNXB.Text = nxb;
                    txtGia.Text = gia;
                    txtTenTG.Text = tenTG;
                    txtSoLuong.Text = soluong;
                    cbbTrangThai.SelectedItem = trangthai;
                    imageBook.Image = image; // Gán hình ảnh cho PictureBox
                    //txtMaSach.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi hiển thị thông tin hàng: " + ex.Message);
            }
        }

        //nút clear
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            txtMaSach.Text = "";
            txtTenSach.Text = "";
            txtLoaiSach.Text = "";
            txtNXB.Text = "";
            txtGia.Text = "";
            txtTenTG.Text = "";
            txtSoLuong.Text = "";
            cbbTrangThai.SelectedItem = "";
            imageBook.Image = null;
            LoadStatus();
        }

        private bool isMaSachEdited = true;

        // Định nghĩa thuộc tính originalMaSach
        public string originalMaSach
        {
            // Getter: Lấy giá trị của txtMaSach.Text
            get => txtMaSach.Text;

            // Setter: Đặt giá trị cho txtMaSach.Text
            set => txtMaSach.Text = value;
        }

        private void txtMaSach_TextChanged(object sender, EventArgs e)
        {
            // So sánh giá trị hiện tại với giá trị ban đầu
            if (txtMaSach.Text != originalMaSach)
            {
                isMaSachEdited = false;
            }
            else
            {
                isMaSachEdited = true;
            } 
        }

        private void txtTenSach_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void txtLoaiSach_TextChanged(object sender, EventArgs e)
        {

        }

        private void cbbTrangThai_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void btnChooseImage_Click(object sender, EventArgs e)
        {
            // Tạo một OpenFileDialog để chọn file ảnh
            OpenFileDialog openFileDialog = new OpenFileDialog();

            // Chỉ cho phép chọn các file ảnh
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif;*.webp";

            // Hiển thị hộp thoại OpenFileDialog và kiểm tra xem người dùng đã chọn một file chưa
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Lấy đường dẫn file mà người dùng đã chọn
                string filePath = openFileDialog.FileName;

                // Hiển thị hình ảnh trong PictureBox
                imageBook.Image = Image.FromFile(filePath);

                // Thiết lập chế độ hiển thị của PictureBox để phù hợp với kích thước của PictureBox
                imageBook.SizeMode = PictureBoxSizeMode.Zoom;
            }
        }

        private void guna2HtmlLabel1_Click(object sender, EventArgs e)
        {

        }

        private void SearchAndUpdateDataGridView()
        {
            string keyword = txtFind.Text.Trim(); // Lấy giá trị từ TextBox và loại bỏ các khoảng trắng thừa

            // Tạo câu truy vấn SQL sử dụng LIKE để tìm kiếm dựa trên giá trị MAPT
            string searchQuery = @" SELECT * 
                                    FROM QUANLYSACH 
                                    WHERE TENSACH LIKE '%' + @tensach + '%'
                                    OR NXB LIKE '%' + @nxb + '%'
                                    OR TENTACGIA LIKE '%' + @tentacgia + '%'";

            // Tạo và mở kết nối đến cơ sở dữ liệu
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Tạo đối tượng SqlCommand
                    using (SqlCommand command = new SqlCommand(searchQuery, connection))
                    {
                        // Đảm bảo tham số @id có kiểu dữ liệu phù hợp với trường MAPT
                        command.Parameters.AddWithValue("@tensach", keyword);
                        command.Parameters.AddWithValue("@nxb", keyword);
                        command.Parameters.AddWithValue("@tentacgia", keyword);

                        // Tạo DataAdapter và DataTable để lưu trữ kết quả truy vấn
                        SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                        DataTable dataTable = new DataTable();

                        // Đổ dữ liệu từ SqlDataAdapter vào DataTable
                        dataAdapter.Fill(dataTable);

                        // Hiển thị dữ liệu trong DataTable trên DataGridView
                        tableTTSach.DataSource = dataTable;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void txtFind_TextChanged(object sender, EventArgs e)
        {
            SearchAndUpdateDataGridView();
        }
    }
}
