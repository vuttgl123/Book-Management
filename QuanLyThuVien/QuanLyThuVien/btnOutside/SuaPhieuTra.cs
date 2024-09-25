using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace QuanLyThuVien
{
    public partial class SuaPhieuTra : Form
    {
        private QLMuonTraSach parentForm;
        public SuaPhieuTra(QLMuonTraSach parent)
        {
            InitializeComponent();
            LoadMaDocGia();
            LoadMaSach();
            LoadMaThuThu();
            LoadMaPM();
            this.parentForm = parent;
        }

        private void SuaPhieuTra_Load(object sender, EventArgs e)
        {

        }

        private string connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;

        public string MaPT { get => txtMaPT.Text; set => txtMaPT.Text = value; }
        public string MaPM { get => cbbMaPM.SelectedItem.ToString(); set => cbbMaPM.SelectedItem = value; }
        public string MaTT { get => cbbMaTT.SelectedItem.ToString(); set => cbbMaTT.SelectedItem = value; }
        public string MaSach { get => cbbMaSach.SelectedItem.ToString(); set => cbbMaSach.SelectedItem = value; }
        public string MaDG { get => cbbMaDG.SelectedItem.ToString(); set => cbbMaDG.SelectedItem = value; }
        public int PhiTreHan { get => int.Parse(txtPhiTre.Text); set => txtPhiTre.Text = value.ToString(); }
        public DateTime Ngaytra { get => dateTra.Value; set => dateTra.Value = value; }
        private void LoadMaDocGia()
        {
            // Lấy danh sách danh mục từ cơ sở dữ liệu
            List<string> madocgiaList = GetMaDocGiaFromDbs(); // Viết phương thức này để lấy danh sách danh mục từ cơ sở dữ liệu

            // Đổ danh sách danh mục vào combo box
            foreach (string docgia in madocgiaList)
            {
                cbbMaDG.Items.Add(docgia);
            }

        }
        public void LoadMaSach()
        {
            // GET ALL MA SACH 

            List<string> masachList = GETMaSachFromDbs();

            foreach (string msitem in masachList)
            {
                cbbMaSach.Items.Add(msitem);
            }
        }

        public void LoadMaThuThu()
        {
            // GET ALL MA THU THU
            List<string> mattList = GetMaThuThuFromDbs();
            foreach (string matt in mattList)
            {
                cbbMaTT.Items.Add(matt);
            }
        }
        public void LoadMaPM()
        {
            // GET ALL MA THU THU
            List<string> mapmList = GetMaPMFromDbs();
            foreach (string matt in mapmList)
            {
                cbbMaPM.Items.Add(matt);
            }
        }

        private List<string> GetMaPMFromDbs()
        {
            List<string> mttList = new List<string>();
            // Khởi tạo kết nối tới cơ sở dữ liệu
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                // Mở kết nối
                conn.Open();

                // Khởi tạo câu lệnh SQL để lấy danh sách danh mục
                string query = "SELECT MAPM FROM PHIEUMUON";

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
                            string mtt = reader["MAPM"].ToString();

                            // Thêm vào danh sách danh mục
                            mttList.Add(mtt);
                        }
                    }
                }
                // Trả về danh sách danh mục đã lấy được từ cơ sở dữ liệu
                return mttList;
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


        private void btnFix_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra xem các giá trị cần thiết đã được chọn hay không
                if (string.IsNullOrEmpty(cbbMaPM.Text) || string.IsNullOrEmpty(cbbMaDG.Text) || string.IsNullOrEmpty(cbbMaSach.Text) || string.IsNullOrEmpty(cbbMaTT.Text))
                {
                    MessageBox.Show("Vui lòng chọn mã phiếu mượn, mã độc giả, mã sách và mã thủ thư!");
                    return;
                }
                string connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Mở kết nối
                    connection.Open();

                    // Thực hiện truy vấn UPDATE để cập nhật thông tin của phiếu mượn
                    string updateQuery = @"UPDATE PHIEUTRA SET MADOCGIA = @madocgia, MATHUTHU = @mathuthu, MASACH = @masach, NGAYTRA = @ngayhentra, PHITREHAN = @phitrehan, TRANGTHAI = @trangthai WHERE MAPT = @maPT";
                    using (SqlCommand command = new SqlCommand(updateQuery, connection))
                    {
                        command.Parameters.AddWithValue("@maPT", txtMaPT.Text);
                        command.Parameters.AddWithValue("@maPM", cbbMaPM.Text);
                        command.Parameters.AddWithValue("@madocgia", cbbMaDG.Text);
                        command.Parameters.AddWithValue("@mathuthu", cbbMaTT.Text);
                        command.Parameters.AddWithValue("@masach", cbbMaSach.Text);
                        command.Parameters.AddWithValue("@ngayhentra", dateTra.Value);
                        command.Parameters.AddWithValue("@phitrehan", txtPhiTre.Text);
                        command.Parameters.AddWithValue("@trangthai", "Đã trả"); // Cập nhật trạng thái

                        
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

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void dateTra_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
