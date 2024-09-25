using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyThuVien
{
    public partial class QLMuonTraSach : Form
    {
        private bool isUpdatingDate = false;
        private QLMuonTraSach formQL;
        public void UpdateDataGridViewFromDatabase()
        {
            tablePT.DataSource = GetDataFromDatabase();
        }

        private DataTable GetDataFromDatabase()
        {
            DataTable dataTable = new DataTable();
            // Ví dụ:
            string query = "SELECT * FROM PHIEUTRA";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dataTable);
                    }
                }
            }
            return dataTable;
        }

        public QLMuonTraSach()
        {
            InitializeComponent();
            LoadMaDocGia();
            LoadMaSach();
            LoadMaThuThu();
            GetMaDocGiaFromDbs();
            GETMaSachFromDbs();
            GetMaThuThuFromDbs();
            dateNgayMuon.ValueChanged += dateNgayMuon_ValueChanged;
            dateNgayTra.ValueChanged += dateNgayTra_ValueChanged;
            UpdateRecordCountLabel();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            TextBox textBox2 = new TextBox();

            // Chỉnh chiều cao thành 50px
            textBox2.Height = 50;
            textBox2.Width = 170;
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void Form9_Load(object sender, EventArgs e)
        {
            UpdateRecordCountLabel();
        }

        private void UpdateRecordCountLabel()
        {
            try
            {
                // Chuỗi kết nối từ cấu hình
                string connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;

                // Tạo câu truy vấn SQL để đếm tổng số bản ghi trong bảng PHIEUMUON
                string query = "SELECT COUNT(*) FROM PHIEUMUON";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Tạo lệnh SQL
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Mở kết nối
                        connection.Open();

                        // Thực thi lệnh và lấy kết quả
                        int recordCount = (int)command.ExecuteScalar();

                        // Hiển thị số lượng bản ghi trong Label
                        lblContPM.Text = recordCount.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi đếm số lượng bản ghi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void trasach_Click(object sender, EventArgs e)
        {

        }

        private void giahan_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private string connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;

        public object SearchCustomerEvent { get; private set; }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void cbMaDocGia_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        // Phương thức để load danh mục vào combo box
        private void LoadMaDocGia()
        {
            // Lấy danh sách danh mục từ cơ sở dữ liệu
            List<string> madocgiaList = GetMaDocGiaFromDbs(); // Viết phương thức này để lấy danh sách danh mục từ cơ sở dữ liệu

            // Đổ danh sách danh mục vào combo box
            foreach (string docgia in madocgiaList)
            {
                cbMaDocGia.Items.Add(docgia);
            }

        }
        public void LoadMaSach()
        {
            // GET ALL MA SACH 

            List<string> masachList = GETMaSachFromDbs();

            foreach (string msitem in masachList)
            {
                cbMaSach.Items.Add(msitem);
            }
        }

        public void LoadMaThuThu()
        {
            // GET ALL MA THU THU
            List<string> mattList = GetMaThuThuFromDbs();
            foreach (string matt in mattList)
            {
                cbMaThuThu.Items.Add(matt);
            }
        }
        private List<string> GetMaThuThuFromDbs()
        {
            List<string> mttList = new List<string>();
            // Khởi tạo kết nối tới cơ sở dữ liệu
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                // Mở kết nối
                conn.Open();

                // Khởi tạo câu lệnh SQL để lấy danh sách danh mục
                string query = "SELECT MATHUTHU FROM QUANLYTHUTHU";

                // Khởi tạo đối tượng SqlCommand
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    // Thực thi câu lệnh SQL và đọc kết quả
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Duyệt qua các dòng kết quả
                        while (reader.Read())
                        {
                            // Đọc giá trị của cột TenDanhMuc
                            string mtt = reader["MATHUTHU"].ToString();

                            // Thêm vào danh sách danh mục
                            mttList.Add(mtt);
                        }
                    }
                }
            }

            // Trả về danh sách danh mục đã lấy được từ cơ sở dữ liệu
            return mttList;
        }

        private List<string> GETMaSachFromDbs()
        {
            List<string> msList = new List<string>();
            // Khởi tạo kết nối tới cơ sở dữ liệu
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                // Mở kết nối
                conn.Open();

                // Khởi tạo câu lệnh SQL để lấy danh sách danh mục
                string query = "SELECT MASACH FROM QUANLYSACH";

                // Khởi tạo đối tượng SqlCommand
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    // Thực thi câu lệnh SQL và đọc kết quả
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Duyệt qua các dòng kết quả
                        while (reader.Read())
                        {
                            // Đọc giá trị của cột TenDanhMuc
                            string ms = reader["MASACH"].ToString();

                            // Thêm vào danh sách danh mục
                            msList.Add(ms);
                        }
                    }
                }
            }
            // Trả về danh sách danh mục đã lấy được từ cơ sở dữ liệu
            return msList;
        }

        private List<string> GetMaDocGiaFromDbs()
        {
            List<string> mdgList = new List<string>();
            // Khởi tạo kết nối tới cơ sở dữ liệu
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                // Mở kết nối
                conn.Open();

                // Khởi tạo câu lệnh SQL để lấy danh sách danh mục
                string query = "SELECT MADOCGIA FROM QUANLYDOCGIA";

                // Khởi tạo đối tượng SqlCommand
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    // Thực thi câu lệnh SQL và đọc kết quả
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Duyệt qua các dòng kết quả
                        while (reader.Read())
                        {
                            // Đọc giá trị của cột TenDanhMuc
                            string mdg = reader["MADOCGIA"].ToString();

                            // Thêm vào danh sách danh mục
                            mdgList.Add(mdg);
                        }
                    }
                }
            }

            // Trả về danh sách danh mục đã lấy được từ cơ sở dữ liệu
            return mdgList;
        }

        private void tablePM_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && e.RowIndex < tablePM.Rows.Count - 1)
                {
                    DataGridViewRow row = tablePM.Rows[e.RowIndex];

                    // Kiểm tra giá trị của từng ô trước khi chuyển đổi
                    string maPhieu = row.Cells["MAPM"].Value != DBNull.Value ? row.Cells["MAPM"].Value.ToString() : string.Empty;
                    string maDocGia = row.Cells["MADOCGIA"].Value != DBNull.Value ? row.Cells["MADOCGIA"].Value.ToString() : string.Empty;
                    string maSach = row.Cells["MASACH"].Value != DBNull.Value ? row.Cells["MASACH"].Value.ToString() : string.Empty;
                    string maThuThu = row.Cells["MATHUTHU"].Value != DBNull.Value ? row.Cells["MATHUTHU"].Value.ToString() : string.Empty;
                    DateTime ngayMuon = row.Cells["NGAYMUON"].Value != DBNull.Value ? Convert.ToDateTime(row.Cells["NGAYMUON"].Value) : DateTime.MinValue;
                    DateTime ngayHenTra = row.Cells["NGAYHENTRA"].Value != DBNull.Value ? Convert.ToDateTime(row.Cells["NGAYHENTRA"].Value) : DateTime.MinValue;

                    // Hiển thị giá trị của các ô trong các TextBox hoặc ComboBox tương ứng
                    txtMaPhieu.Text = maPhieu;
                    cbMaDocGia.Text = maDocGia;
                    cbMaSach.Text = maSach;
                    cbMaThuThu.Text = maThuThu;
                    dateNgayMuon.Value = ngayMuon != DateTime.MinValue ? ngayMuon : DateTime.Now;
                    dateNgayTra.Value = ngayHenTra != DateTime.MinValue ? ngayHenTra : DateTime.Now;

                    txtMaPhieu.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi hiển thị thông tin hàng: " + ex.Message);
            }
        }

        private void btnDel_Click_1(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra xem có hàng nào được chọn hay không
                if (tablePM.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Vui lòng chọn hàng cần xóa!");
                    return;
                }

                // Lấy mã phiếu mượn của hàng được chọn
                string maPhieuMuon = tablePM.SelectedRows[0].Cells["MAPM"].Value.ToString();

                string connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Mở kết nối
                    connection.Open();

                    // Xóa dữ liệu từ cơ sở dữ liệu dựa trên mã phiếu mượn đã chọn
                    string delQuery = "DECLARE @maphieu NVARCHAR(50) SET @maphieu = @maPM DELETE FROM PHIEUTRA WHERE MAPM = @maphieu DELETE FROM PHIEUMUON WHERE MAPM = @maPM";
                    using (SqlCommand command = new SqlCommand(delQuery, connection))
                    {
                        command.Parameters.AddWithValue("@maPM", maPhieuMuon);
                        command.ExecuteNonQuery();
                    }
                }

                // Cập nhật lại DataGridView sau khi xóa
                btnShow_Click_1(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa dữ liệu: " + ex.Message);
            }
            UpdateRecordCountLabel();
        }

        private void btnThem_Click_1(object sender, EventArgs e)
        {
            try
            {
                txtMaPhieu.Enabled = true;
                // Kiểm tra xem mã phiếu mượn đã được nhập hay chưa
                if (string.IsNullOrEmpty(txtMaPhieu.Text))
                {
                    MessageBox.Show("Vui lòng nhập mã phiếu mượn!");
                    return;
                }

                // Kiểm tra xem các giá trị cần thiết đã được chọn hay không
                if (string.IsNullOrEmpty(cbMaDocGia.Text) || string.IsNullOrEmpty(cbMaSach.Text) || string.IsNullOrEmpty(cbMaThuThu.Text))
                {
                    MessageBox.Show("Vui lòng chọn mã độc giả, mã sách và mã thủ thư!");
                    return;
                }

                string connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Mở kết nối
                    connection.Open();

                    // Kiểm tra xem mã phiếu mượn đã tồn tại hay chưa
                    string checkQuery = "SELECT COUNT(*) FROM PHIEUMUON WHERE MAPM = @maPM";
                    using (SqlCommand checkCommand = new SqlCommand(checkQuery, connection))
                    {
                        checkCommand.Parameters.AddWithValue("@maPM", txtMaPhieu.Text);
                        int count = (int)checkCommand.ExecuteScalar();
                        if (count > 0)
                        {
                            MessageBox.Show("Mã phiếu mượn đã tồn tại!");
                            return;
                        }
                    }

                    // Thêm dữ liệu vào cơ sở dữ liệu và lấy mã phiếu trả
                    string insertQuery = @" INSERT INTO PHIEUMUON(MAPM, MADOCGIA, MASACH, MATHUTHU, NGAYMUON, NGAYHENTRA, TRANGTHAI) 
                                VALUES (@maPM, @madocgia, @masach, @mathuthu, @ngaymuon, @ngayhentra, @trangthai) 
                                DECLARE @maPT NVARCHAR(20)
                                SET @maPT = LEFT(CAST(NEWID() AS NVARCHAR(36)), 5)
                                INSERT INTO PHIEUTRA(MAPT, MAPM, MADOCGIA, MASACH, MATHUTHU, NGAYTRA, PHITREHAN, TRANGTHAI) 
                                VALUES (@maPT, @maPM, @madocgia, @masach, @mathuthu, @ngayhentra, 0, N'Đang mượn')
                                
                                -- Trả về mã phiếu trả vừa được tạo ra
                                SELECT @maPT AS NewMapt;

                                -- Giảm số lượng sách
                                UPDATE QUANLYSACH SET SOLUONG = SOLUONG - 1 WHERE MASACH = @masach;

                                -- Cập nhật trạng thái sách nếu số lượng còn lại là 0
                                IF (SELECT SOLUONG FROM QUANLYSACH WHERE MASACH = @masach) = 0
                                BEGIN
                                    UPDATE QUANLYSACH SET TRANGTHAI = N'Đã hết' WHERE MASACH = @masach;
                                END;
                              ";
                    string masach = cbMaSach.Text;
                    int soluongSachConLai = 0;
                    string query = "SELECT SOLUONG FROM QUANLYSACH WHERE MASACH = @masach";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@masach", masach);
                        object result = command.ExecuteScalar();
                        if (result != null && result != DBNull.Value)
                        {
                            soluongSachConLai = Convert.ToInt32(result);
                        }
                        // Kiểm tra số lượng sách còn lại
                        if (soluongSachConLai <= 0)
                        {
                            MessageBox.Show("Hiện tại không còn sách để mượn.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return; // Ngăn chặn quá trình thêm dữ liệu tiếp theo
                        }
                    }

                    string newMapt = null;
                    using (SqlCommand command = new SqlCommand(insertQuery, connection))
                    {
                        command.Parameters.AddWithValue("@maPM", txtMaPhieu.Text);
                        command.Parameters.AddWithValue("@madocgia", cbMaDocGia.Text);
                        command.Parameters.AddWithValue("@masach", cbMaSach.Text);
                        command.Parameters.AddWithValue("@mathuthu", cbMaThuThu.Text);
                        command.Parameters.AddWithValue("@ngaymuon", dateNgayMuon.Value);
                        command.Parameters.AddWithValue("@ngayhentra", dateNgayTra.Value);
                        command.Parameters.AddWithValue("@trangthai", "Đang mượn");

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                newMapt = reader["NewMapt"].ToString();
                            }
                        }
                    }

                    // Kiểm tra và cập nhật phí trễ hạn nếu cần
                    if (newMapt != null)
                    {
                        DateTime ngayTraDuKien = dateNgayTra.Value;
                        DateTime ngayTraThucTe = DateTime.Now;

                        if (ngayTraThucTe > ngayTraDuKien)
                        {
                            TimeSpan soNgayTre = ngayTraThucTe - ngayTraDuKien;
                            int soNgayTreInt = (int)soNgayTre.TotalDays;

                            // Tính phí trễ dựa trên số ngày trễ và mức phạt của bạn
                            int phiTreHan = soNgayTreInt * 10; // Mỗi ngày trễ phạt 10 tiền

                            // Hiển thị thông báo về phí trễ
                            MessageBox.Show($"Quá hạn trả sách {soNgayTreInt} ngày. Phí trễ là {phiTreHan} tiền.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // Cập nhật cột PHITREHAN trong bảng PHIEUTRA
                            string updateQuery = "UPDATE PHIEUTRA SET PHITREHAN = @phiTreHan WHERE MAPT = @maPT";
                            using (SqlCommand command = new SqlCommand(updateQuery, connection))
                            {
                                command.Parameters.AddWithValue("@phiTreHan", phiTreHan);
                                command.Parameters.AddWithValue("@maPT", newMapt);
                                command.ExecuteNonQuery();
                            }
                        }
                    }
                }

                // Xóa nội dung của các TextBox
                txtMaPhieu.Clear();
                // Các TextBox khác cũng xóa tương tự
                cbMaDocGia.SelectedIndex = -1;
                cbMaSach.SelectedIndex = -1;
                cbMaThuThu.SelectedIndex = -1;
                dateNgayMuon.Value = DateTime.Now;
                dateNgayTra.Value = DateTime.Now;

                // Cập nhật DataGridView bằng cách lấy dữ liệu mới từ cơ sở dữ liệu
                btnShow_Click_1(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm dữ liệu: " + ex.Message);
            }
            UpdateRecordCountLabel();
        }

        private void btnShow_Click_1(object sender, EventArgs e)
        {
            try
            {
                // Tạo kết nối SQL
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Mở kết nối
                    connection.Open();

                    // Tạo câu truy vấn SQL để lấy dữ liệu từ bảng
                    string query = "SELECT * FROM PHIEUMUON";

                    // Tạo đối tượng SqlDataAdapter để lấy dữ liệu từ câu truy vấn
                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                    {
                        // Tạo đối tượng DataTable để lưu trữ dữ liệu từ bảng
                        DataTable dataTable = new DataTable();

                        // Đổ dữ liệu từ SqlDataAdapter vào DataTable
                        adapter.Fill(dataTable);

                        // Gán DataTable làm nguồn dữ liệu cho DataGridView
                        tablePM.DataSource = dataTable;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void btnFix_Click_1(object sender, EventArgs e)
        {
            try
            {

                // Kiểm tra xem các giá trị cần thiết đã được chọn hay không
                if (string.IsNullOrEmpty(cbMaDocGia.Text) || string.IsNullOrEmpty(cbMaSach.Text) || string.IsNullOrEmpty(cbMaThuThu.Text))
                {
                    MessageBox.Show("Vui lòng chọn mã độc giả, mã sách và mã thủ thư!");
                    return;
                }
                string connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Mở kết nối
                    connection.Open();

                    // Thực hiện truy vấn UPDATE để cập nhật thông tin của phiếu mượn
                    string updateQuery = "UPDATE PHIEUMUON SET MADOCGIA = @madocgia, MASACH = @masach, MATHUTHU = @mathuthu, NGAYMUON = @ngaymuon, NGAYHENTRA = @ngayhentra, TRANGTHAI = @trangthai WHERE MAPM = @maPM";
                    using (SqlCommand command = new SqlCommand(updateQuery, connection))
                    {
                        command.Parameters.AddWithValue("@maPM", txtMaPhieu.Text);
                        command.Parameters.AddWithValue("@madocgia", cbMaDocGia.Text);
                        command.Parameters.AddWithValue("@masach", cbMaSach.Text);
                        command.Parameters.AddWithValue("@mathuthu", cbMaThuThu.Text);
                        command.Parameters.AddWithValue("@ngaymuon", dateNgayMuon.Value);
                        command.Parameters.AddWithValue("@ngayhentra", dateNgayTra.Value);
                        command.Parameters.AddWithValue("@trangthai", "Đang mượn"); // Cập nhật trạng thái

                        // Thực hiện truy vấn UPDATE
                        command.ExecuteNonQuery();
                    }
                }

                // Sau khi cập nhật thành công, cập nhật lại hiển thị trong DataGridView
                btnShow_Click_1(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi sửa dữ liệu: " + ex.Message);
            }
        }

        private void btnExit_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            txtMaPhieu.Text = "";
            cbMaDocGia.Text = "";
            cbMaSach.Text = "";
            cbMaThuThu.Text = "";
            txtMaPhieu.Enabled = true;
        }

        private void btnHienThi_Click(object sender, EventArgs e)
        {
            try
            {
                // Tạo kết nối SQL
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Mở kết nối
                    connection.Open();

                    // Tạo câu truy vấn SQL để lấy dữ liệu từ bảng
                    string query = "SELECT * FROM PHIEUTRA";

                    // Tạo đối tượng SqlDataAdapter để lấy dữ liệu từ câu truy vấn
                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                    {
                        // Tạo đối tượng DataTable để lưu trữ dữ liệu từ bảng
                        DataTable dataTable = new DataTable();

                        // Đổ dữ liệu từ SqlDataAdapter vào DataTable
                        adapter.Fill(dataTable);

                        // Gán DataTable làm nguồn dữ liệu cho DataGridView
                        tablePT.DataSource = dataTable;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        //nút sửa
        private void guna2Button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (tablePT.SelectedRows.Count > 0)
                {
                    // Lấy hàng được chọn
                    DataGridViewRow selectedRow = tablePT.SelectedRows[0];

                    // Tạo một instance mới của form 'SuaPhieuTra'
                    SuaPhieuTra suaPhieuTra = new SuaPhieuTra(this);

                    // Thiết lập giá trị cho các điều khiển trong form 'SuaPhieuTra' từ các giá trị của hàng được chọn
                    suaPhieuTra.MaPT = selectedRow.Cells["MAPT"].Value.ToString();
                    suaPhieuTra.MaPM = selectedRow.Cells["MAPM"].Value.ToString();
                    suaPhieuTra.MaDG = selectedRow.Cells["MADOCGIA"].Value.ToString();
                    suaPhieuTra.MaTT = selectedRow.Cells["MATHUTHU"].Value.ToString();
                    suaPhieuTra.MaSach = selectedRow.Cells["MASACH"].Value.ToString();
                    suaPhieuTra.Ngaytra = Convert.ToDateTime(selectedRow.Cells["NGAYTRA"].Value);
                    suaPhieuTra.PhiTreHan = int.Parse(selectedRow.Cells["PHITREHAN"].Value.ToString());
                    // Hiển thị form 'SuaPhieuTra'
                    suaPhieuTra.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn một hàng để sửa.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fix: " + ex.Message, "Error");
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {

        }

        private void tablePT_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra xem có hàng nào được chọn hay không
                if (tablePT.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Vui lòng chọn hàng cần xóa!");
                    return;
                }

                // Lấy mã phiếu mượn của hàng được chọn
                string maPhieuTra = tablePT.SelectedRows[0].Cells["MAPT"].Value.ToString();

                string connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Mở kết nối
                    connection.Open();

                    // Xóa dữ liệu từ cơ sở dữ liệu dựa trên mã phiếu mượn đã chọn
                    string delQuery = "DELETE FROM PHIEUTRA WHERE MAPT = @maPT";
                    using (SqlCommand command = new SqlCommand(delQuery, connection))
                    {
                        command.Parameters.AddWithValue("@maPT", maPhieuTra);
                        command.ExecuteNonQuery();
                        MessageBox.Show("Xóa thành công phiếu trả!");
                    }
                }

                // Cập nhật lại DataGridView sau khi xóa
                btnHienThi_Click(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa dữ liệu: " + ex.Message);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand())
            {
                conn.Open();
                cmd.Connection = conn;
                var mapt = txtKeyword.Text; // Lấy giá trị từ TextBox

                // Câu truy vấn SQL sử dụng LIKE để tìm kiếm dựa trên giá trị MAPT
                string searchQuery = @"SELECT * 
                                FROM PHIEUTRA 
                                WHERE MAPT LIKE @id + '%' 
                                ORDER BY MAPT ASC";
                cmd.Parameters.Add("@id", SqlDbType.NVarChar).Value = mapt;
                cmd.CommandText = searchQuery;
                cmd.Parameters.AddWithValue("@maPM", txtMaPhieu.Text);
                cmd.Parameters.AddWithValue("@madocgia", cbMaDocGia.Text);
                cmd.Parameters.AddWithValue("@masach", cbMaSach.Text);
                cmd.Parameters.AddWithValue("@mathuthu", cbMaThuThu.Text);
                cmd.Parameters.AddWithValue("@ngaymuon", dateNgayMuon.Value);
                cmd.Parameters.AddWithValue("@ngayhentra", dateNgayTra.Value);
                cmd.Parameters.AddWithValue("@trangthai", "Đang mượn"); // Cập nhật trạng thái

                // Thực hiện truy vấn UPDATE
                cmd.ExecuteNonQuery();
            }
        }

        private void txtKeyword_TextChanged(object sender, EventArgs e)
        {
            SearchAndUpdateDataGridView();
        }

        private void SearchAndUpdateDataGridView()
        {
            string keyword = txtKeyword.Text.Trim(); // Lấy giá trị từ TextBox và loại bỏ các khoảng trắng thừa

            // Tạo câu truy vấn SQL sử dụng LIKE để tìm kiếm dựa trên giá trị MAPT
            string searchQuery = @"SELECT * 
                                  FROM PHIEUTRA 
                                  WHERE MAPT LIKE '%' + @id + '%'";

            // Tạo và mở kết nối đến cơ sở dữ liệu
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Tạo đối tượng SqlCommand
                using (SqlCommand command = new SqlCommand(searchQuery, connection))
                {
                    // Đảm bảo tham số @id có kiểu dữ liệu phù hợp với trường MAPT
                    command.Parameters.AddWithValue("@id", SqlDbType.NVarChar).Value = keyword;

                    // Tạo DataAdapter và DataTable để lưu trữ kết quả truy vấn
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();

                    // Đổ dữ liệu từ SqlDataAdapter vào DataTable
                    dataAdapter.Fill(dataTable);

                    // Hiển thị dữ liệu trong DataTable trên DataGridView
                    tablePT.DataSource = dataTable;
                }
            }
        }

        private void tablePM_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dateNgayTra_ValueChanged(object sender, EventArgs e)
        {
            if (isUpdatingDate)
                return;

            // Lấy giá trị ngày mượn và ngày trả
            DateTime ngayMuon = dateNgayMuon.Value;
            DateTime ngayTra = dateNgayTra.Value;

            try
            {
                // Kiểm tra nếu ngày trả nhỏ hơn ngày mượn
                if (ngayTra < ngayMuon)
                {
                    // Hiển thị thông báo lỗi
                    MessageBox.Show("Ngày trả phải sau ngày mượn.");
                    isUpdatingDate = true;
                    // Gán lại ngày trả bằng ngày mượn
                    dateNgayTra.Value = ngayMuon;
                    isUpdatingDate = false;
                    
                }
            }
            catch (Exception ex)
            {
                // Hiển thị thông báo nếu có lỗi xảy ra
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void dateNgayMuon_ValueChanged(object sender, EventArgs e)
        {
            //if (isUpdatingDate)
            //    return;

            //this.Invoke(new Action(() =>
            //{
            //    try
            //    {
            //        isUpdatingDate = true;

            //        // Lấy giá trị ngày mượn và ngày trả
            //        DateTime ngayMuon = dateNgayMuon.Value;
            //        DateTime ngayTra = dateNgayTra.Value;

            //        // Kiểm tra nếu ngày mượn lớn hơn ngày trả
            //        if (ngayMuon > ngayTra)
            //        {
            //            MessageBox.Show("Ngày trả phải sau ngày mượn.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //            dateNgayMuon.Value = ngayTra;
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show("Có lỗi xảy ra: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    }
            //    finally
            //    {
            //        isUpdatingDate = false; // Đặt lại isUpdatingDate về false sau khi thay đổi giá trị
            //        this.Activate(); // Đảm bảo form hiện tại được kích hoạt lại
            //    }
            //}));
        }
        private void CheckDates()
        {
            // Lấy giá trị ngày mượn và ngày trả
            DateTime ngayMuon = dateNgayMuon.Value;
            DateTime ngayTra = dateNgayTra.Value;

            // Kiểm tra nếu ngày mượn lớn hơn hoặc bằng ngày trả
            if (ngayMuon >= ngayTra)
            {
                MessageBox.Show("Ngày mượn phải trước ngày trả.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dateNgayMuon.Focus(); // Focus vào DateTimePicker để người dùng có thể sửa ngày mượn
            }
        }
        private void dateNgayTra_KeyDown(object sender, KeyEventArgs e)
        {

        }
        
        //tổng số lượng sách đang mượn
        private void label13_Click_1(object sender, EventArgs e)
        {
            
        }

        private void lblContPM_TextChanged(object sender, EventArgs e)
        {
            UpdateRecordCountLabel();
        }

        private void btnDaTra_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(selectedMapt))
                {
                    MessageBox.Show("Vui lòng chọn một phiếu trả hợp lệ!");
                    return;
                }

                string connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Mở kết nối
                    connection.Open();

                    // Truy vấn để lấy mã sách (MASACH) liên quan đến phiếu trả
                    string getMasachQuery = "SELECT MASACH FROM PHIEUTRA WHERE MAPT = @maPT";
                    string masach = null;
                    using (SqlCommand getMasachCommand = new SqlCommand(getMasachQuery, connection))
                    {
                        getMasachCommand.Parameters.AddWithValue("@maPT", selectedMapt);
                        object result = getMasachCommand.ExecuteScalar();
                        if (result != null)
                        {
                            masach = result.ToString();
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy mã sách cho phiếu trả này.");
                            return;
                        }
                    }

                    // Cập nhật trạng thái phiếu mượn thành "Đã Trả"
                    string updateQuery = "UPDATE PHIEUTRA SET TRANGTHAI = N'Đã Trả' WHERE MAPT = @maPT";
                    using (SqlCommand command = new SqlCommand(updateQuery, connection))
                    {
                        command.Parameters.AddWithValue("@maPT", selectedMapt);
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Cập nhật trạng thái thành 'Đã Trả' thành công!");

                            // Cập nhật lại DataGridView
                            foreach (DataGridViewRow row in tablePT.Rows)
                            {
                                if (row.Cells["MAPT"].Value.ToString() == selectedMapt)
                                {
                                    row.Cells["TRANGTHAI"].Value = "Đã Trả";
                                    break;
                                }
                            }

                            // Tăng số lượng sách lên 1 trong bảng QUANLYSACH
                            string updateSoluongQuery = "UPDATE QUANLYSACH SET SOLUONG = SOLUONG + 1 WHERE MASACH = @masach";
                            using (SqlCommand updateSoluongCommand = new SqlCommand(updateSoluongQuery, connection))
                            {
                                updateSoluongCommand.Parameters.AddWithValue("@masach", masach);
                                updateSoluongCommand.ExecuteNonQuery();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy phiếu trả với mã này.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật trạng thái: " + ex.Message);
            }
        }


        private string selectedMapt; 
        private void tablePT_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = tablePT.Rows[e.RowIndex];
                // Giả sử cột mã phiếu mượn là cột đầu tiên
                selectedMapt = row.Cells["MAPT"].Value.ToString();
            }
        }

        private void dateNgayMuon_CloseUp(object sender, EventArgs e)
        {
            CheckDates();
        }

        private void dateNgayTra_CloseUp(object sender, EventArgs e)
        {
            CheckDates();
        }

    }
}
