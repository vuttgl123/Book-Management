using QuanLyThuVien.btnOutside;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace QuanLyThuVien
{
    public partial class QLTTDocGia : Form
    {
        public QLTTDocGia()
        {
            InitializeComponent();
        }

        private string connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;


        public void show()
        {
            try
            {
                // Tạo kết nối SQL
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Mở kết nối
                    connection.Open();

                    // Tạo câu truy vấn SQL để lấy dữ liệu từ bảng
                    string query = "SELECT * FROM QUANLYDOCGIA";

                    // Tạo đối tượng SqlDataAdapter để lấy dữ liệu từ câu truy vấn
                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                    {
                        // Tạo đối tượng DataTable để lưu trữ dữ liệu từ bảng
                        DataTable dataTable = new DataTable();

                        // Đổ dữ liệu từ SqlDataAdapter vào DataTable
                        adapter.Fill(dataTable);

                        // Gán DataTable làm nguồn dữ liệu cho DataGridView
                        dataTableQLDocGia.DataSource = dataTable;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
        private void btnShow_Click(object sender, EventArgs e)
        {
            show();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void UpdateDataGridViewFromDatabase()
        {
            // Gán nguồn dữ liệu (DataSource) của DataGridView (dataTableQLDocGia) bằng dữ liệu lấy từ cơ sở dữ liệu.
            dataTableQLDocGia.DataSource = GetDataFromDatabase();
        }

        private DataTable GetDataFromDatabase()
        {
            // Tạo một đối tượng DataTable để lưu trữ dữ liệu lấy từ cơ sở dữ liệu
            DataTable dataTable = new DataTable();

            // Chuỗi truy vấn SQL để lấy tất cả dữ liệu từ bảng QUANLYDOCGIA
            string query = "SELECT * FROM QUANLYDOCGIA";

            // Tạo một đối tượng SqlConnection với chuỗi kết nối đến cơ sở dữ liệu
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Mở kết nối đến cơ sở dữ liệu
                connection.Open();

                // Tạo một đối tượng SqlCommand với câu truy vấn SQL và đối tượng kết nối
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Tạo một đối tượng SqlDataAdapter để thực hiện câu truy vấn và điền dữ liệu vào DataTable
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        // Đổ dữ liệu từ cơ sở dữ liệu vào DataTable
                        adapter.Fill(dataTable);
                    }
                }
            }

            // Trả về đối tượng DataTable chứa dữ liệu
            return dataTable;
        }


        private void dataTableQLDocGia_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Xử lý khi một ô trong DataGridView được nhấn, nếu cần thiết
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            AddDocGia adddocgia = new AddDocGia(this);
            adddocgia.Show(); // Hiển thị adddocgia
        }

        private void btnFix_Click(object sender, EventArgs e)
        {
            
            try
            {
                if (dataTableQLDocGia.SelectedRows.Count > 0)
                {
                    // Lấy hàng được chọn
                    DataGridViewRow selectedRow = dataTableQLDocGia.SelectedRows[0];

                    // Tạo một instance mới của form 'SuaPhieuTra'
                    SuaDocGia suadocgiaa = new SuaDocGia(this);

                    // Thiết lập giá trị cho các điều khiển trong form 'SuaPhieuTra' từ các giá trị của hàng được chọn
                    suadocgiaa.madocgia = selectedRow.Cells["MADOCGIA"].Value.ToString();
                    //MessageBox.Show("madg: " + selectedRow.Cells["MADOCGIA"].Value.ToString());
                    suadocgiaa.hoten = selectedRow.Cells["TEN"].Value.ToString();
                    suadocgiaa.gioitinh = selectedRow.Cells["GIOITINH"].Value.ToString();
                    suadocgiaa.Ngaysinh = Convert.ToDateTime(selectedRow.Cells["NGAYSINH"].Value);
                    suadocgiaa.diachi = selectedRow.Cells["DIACHI"].Value.ToString();
                    suadocgiaa.sdt = selectedRow.Cells["SDT"].Value.ToString();
                    suadocgiaa.email = selectedRow.Cells["EMAIL"].Value.ToString();
                    suadocgiaa.Show();
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

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra xem có hàng nào được chọn hay không
                if (dataTableQLDocGia.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Vui lòng chọn hàng cần xóa!");
                    return;
                }

                // Lấy mã phiếu mượn của hàng được chọn
                string madocgia = dataTableQLDocGia.SelectedRows[0].Cells["MADOCGIA"].Value.ToString();

                string connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Mở kết nối
                    connection.Open();

                    // Xóa dữ liệu từ cơ sở dữ liệu dựa trên mã phiếu mượn đã chọn
                    string delQuery = "DELETE FROM QUANLYDOCGIA WHERE MADOCGIA = @madg";
                    using (SqlCommand command = new SqlCommand(delQuery, connection))
                    {
                        command.Parameters.AddWithValue("@madg", madocgia);
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

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            SearchMaDocGiaDataGridView();
        }
        private void SearchMaDocGiaDataGridView()
        {
            string keyword = txtTimKiem.Text.Trim(); // Lấy giá trị từ TextBox và loại bỏ các khoảng trắng thừa

            // Tạo câu truy vấn SQL sử dụng LIKE để tìm kiếm dựa trên giá trị MAPT
            string searchQuery = @"SELECT * 
                                  FROM QUANLYDOCGIA 
                                  WHERE MADOCGIA LIKE '%' + @id + '%' OR TEN LIKE '%' + @hoten + '%'";

            // Tạo và mở kết nối đến cơ sở dữ liệu
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Tạo đối tượng SqlCommand
                using (SqlCommand command = new SqlCommand(searchQuery, connection))
                {
                    // Đảm bảo tham số @id có kiểu dữ liệu phù hợp với trường MAPT
                    command.Parameters.AddWithValue("@id", SqlDbType.NVarChar).Value = keyword;
                    command.Parameters.AddWithValue("@hoten", SqlDbType.NVarChar).Value = keyword;


                    // Tạo DataAdapter và DataTable để lưu trữ kết quả truy vấn
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();

                    // Đổ dữ liệu từ SqlDataAdapter vào DataTable
                    dataAdapter.Fill(dataTable);

                    // Hiển thị dữ liệu trong DataTable trên DataGridView
                    dataTableQLDocGia.DataSource = dataTable;
                }
            }
        }
    }
}
