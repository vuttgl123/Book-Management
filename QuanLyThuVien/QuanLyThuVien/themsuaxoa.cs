using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThuVien
{
    internal class themsuaxoa
    {
        SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-M82ODDR5;Initial Catalog=QuanLyThuVien;Integrated Security=True");//biến con sqlconection để kêt nối đến CSDL
        public void moketnoi() //Phương thức kết nối
        {
            if (con.State == ConnectionState.Closed) //kiểm tra nếu trường hợp chưa kết nối thì mở kêt nối
            {
                con.Open();
            }
        }

        public void dongketnoi()//kiểm tra nếu trường hợp mở kết nối thì đóng kêt nối
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }

        }
        public Boolean thucthidulieu(string cmd) //hàm thực thi dữ liệu có tham số cmd
        {
            moketnoi();//mở kết nối dữ liệu
            Boolean check = false;// gán check = sai
            try
            {
                SqlCommand sc = new SqlCommand(cmd, con);
                sc.ExecuteNonQuery();
                check = true;
            }
            catch (Exception)
            {
                check = false;
            }
            dongketnoi();
            return check;//nếu thực thi câu lệnh sql được thì hảm trả về check= đúng ngược lại check=sai.
        }
        public DataTable docdulieu(string cmd)//hàm đọc dữ liệu đỗ dữ liệu vào lưới sử dụng sqldatadapter để chuyển đổi dữ liệu từ lệnh cmd trả về. Sau đó thì dùng Fill để đổ dữ liệu từ adapter vào datatable và cuối cùng là trả về một bảng chứa dữ liệu đã truy vấn.
        {
            moketnoi();
            DataTable da = new DataTable();
            try
            {
                SqlCommand sc = new SqlCommand(cmd, con);
                SqlDataAdapter sda = new SqlDataAdapter(sc);
                sda.Fill(da);
            }
            catch (Exception)
            {
                da = null;
            }
            dongketnoi();
            return da;
        }
    }
}
