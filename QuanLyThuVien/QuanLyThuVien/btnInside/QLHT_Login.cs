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
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace QuanLyThuVien
{
    public partial class QLHT_Login : Form
    {
        public QLHT_Login()
        {
            InitializeComponent();
        }

        string connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
        private void dataTableQLHT_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            try
            {
                // Tạo kết nối SQL
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Mở kết nối
                    connection.Open();

                    // Tạo câu truy vấn SQL để lấy dữ liệu từ bảng
                    string query = "SELECT * FROM login";

                    // Tạo đối tượng SqlDataAdapter để lấy dữ liệu từ câu truy vấn
                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                    {
                        // Tạo đối tượng DataTable để lưu trữ dữ liệu từ bảng
                        DataTable dataTable = new DataTable();

                        // Đổ dữ liệu từ SqlDataAdapter vào DataTable
                        adapter.Fill(dataTable);

                        // Gán DataTable làm nguồn dữ liệu cho DataGridView
                        dataTableQLHT.DataSource = dataTable;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
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

                    // Tạo câu truy vấn SQL để lấy dữ liệu từ bảng
                    string query = "SELECT * FROM login";

                    // Tạo đối tượng SqlDataAdapter để lấy dữ liệu từ câu truy vấn
                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                    {
                        // Tạo đối tượng DataTable để lưu trữ dữ liệu từ bảng
                        DataTable dataTable = new DataTable();

                        // Đổ dữ liệu từ SqlDataAdapter vào DataTable
                        adapter.Fill(dataTable);

                        // Gán DataTable làm nguồn dữ liệu cho DataGridView
                        dataTableQLHT.DataSource = dataTable;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra xem có hàng nào được chọn hay không
                if (dataTableQLHT.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Vui lòng chọn hàng cần xóa!");
                    return;
                }

                // Lấy mã phiếu mượn của hàng được chọn
                string idLogin = dataTableQLHT.SelectedRows[0].Cells["idLogin"].Value.ToString();

                string connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Mở kết nối
                    connection.Open();

                    // Xóa dữ liệu từ cơ sở dữ liệu dựa trên mã phiếu mượn đã chọn
                    string delQuery = "DELETE FROM login WHERE idLogin = @id";
                    using (SqlCommand command = new SqlCommand(delQuery, connection))
                    {
                        command.Parameters.AddWithValue("@id", idLogin);
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
        public void UpdateDataGridViewFromDatabase()
        {
            dataTableQLHT.DataSource = GetDataFromDatabase();
        }

        private DataTable GetDataFromDatabase()
        {
            DataTable dataTable = new DataTable();
            // Ví dụ:
            string query = "SELECT * FROM login";
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
        private void dataTableQLHT_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && e.RowIndex < dataTableQLHT.Rows.Count - 1)
                {
                    DataGridViewRow row = dataTableQLHT.Rows[e.RowIndex];
                    ThayDoiThongTinTK tt = new ThayDoiThongTinTK(this);
                    // Kiểm tra giá trị của từng ô trước khi chuyển đổi
                    string id = row.Cells["idLogin"].Value != DBNull.Value ? row.Cells["idLogin"].Value.ToString() : string.Empty;
                    string user = row.Cells["username"].Value != DBNull.Value ? row.Cells["username"].Value.ToString() : string.Empty;
                    string pass = row.Cells["password"].Value != DBNull.Value ? row.Cells["password"].Value.ToString() : string.Empty;

                    // Hiển thị giá trị của các ô trong các TextBox hoặc ComboBox tương ứng
                    tt.idLogin = id;
                    tt.Username = user;
                    tt.Password = pass;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi hiển thị thông tin hàng: " + ex.Message);
            }
        }

        private void btnSuaMK_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataTableQLHT.SelectedRows.Count > 0)
                {
                    // Lấy hàng được chọn
                    DataGridViewRow selectedRow = dataTableQLHT .SelectedRows[0];

                    // Tạo một instance mới của form 'SuaPhieuTra'
                    ThayDoiThongTinTK tt = new ThayDoiThongTinTK(this);

                    // Thiết lập giá trị cho các điều khiển trong form 'SuaPhieuTra' từ các giá trị của hàng được chọn
                    tt.idLogin = selectedRow.Cells["idLogin"].Value.ToString();
                    tt.Username = selectedRow.Cells["username"].Value.ToString();
                    tt.Password = selectedRow.Cells["password"].Value.ToString();
                    tt.ConfirmPassword = selectedRow.Cells["confirmpassword"].Value.ToString();

                    // Hiển thị form 'SuaPhieuTra'
                    tt.ShowDialog();
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
        private void SearchAndUpdateDataGridView()
        {
            string keyword = txtFind.Text.Trim(); // Lấy giá trị từ TextBox và loại bỏ các khoảng trắng thừa

            // Tạo câu truy vấn SQL sử dụng LIKE để tìm kiếm dựa trên giá trị MAPT
            string searchQuery = @"SELECT * 
                                  FROM login 
                                  WHERE username LIKE '%' + @id + '%'";

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
                    dataTableQLHT.DataSource = dataTable;
                }
            }
        }
        private void txtFind_TextChanged(object sender, EventArgs e)
        {
            SearchAndUpdateDataGridView();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
