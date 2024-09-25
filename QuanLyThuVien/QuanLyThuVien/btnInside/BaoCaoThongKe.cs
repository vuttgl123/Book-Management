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
using System.Windows.Forms.DataVisualization.Charting;

namespace QuanLyThuVien
{
    public partial class BaoCaoThongKe : Form
    {
        public BaoCaoThongKe()
        {
            InitializeComponent();
            LoadChartData();
            getMaSach();

        }
        private string connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
        private void LoadChartData()
        {
            string query = @"
                            SELECT DISTINCT TOP 5 MASACH, 
                            ((COUNT(MASACH) OVER (PARTITION BY MASACH) * 100) / COUNT(MASACH) OVER ()) AS PHANTRAM
                            FROM PHIEUMUON";
            //Tính phần trăm của số lần xuất hiện của mỗi giá trị trong cột MASACH so với tổng số lần xuất hiện của tất cả các giá trị trong cột MASACH

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string masach = reader["MASACH"].ToString();
                                int phantram = int.Parse(reader["PHANTRAM"].ToString());
                                // Thêm dữ liệu vào Series
                                this.chart1.Series["Số lượng sách được mượn"].Points.AddXY(masach, phantram);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void chart1_Click_1(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel9_Click(object sender, EventArgs e)
        {

        }

        public void getMaSach()
        {
            try
            {
                // Chuỗi kết nối từ cấu hình
                string connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;

                // Tạo câu truy vấn SQL để chọn ra mã sách được mượn nhiều nhất
                string query = "SELECT TOP 1 MASACH FROM PHIEUMUON GROUP BY MASACH ORDER BY COUNT(*) DESC;";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Tạo lệnh SQL
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Mở kết nối
                        connection.Open();

                        // Thực thi lệnh và lấy kết quả
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Lấy mã sách từ kết quả truy vấn
                                string maSach = reader["MASACH"].ToString();

                                // Hiển thị mã sách trong Label
                                lblMaSach.Text = maSach;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lấy thông tin mượn sách: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //Hiển thị ra mã sách được mượn nhiều nhất
        private void guna2HtmlLabel12_TextChanged(object sender, EventArgs e)
        {
            getMaSach();
        }

    }
}
