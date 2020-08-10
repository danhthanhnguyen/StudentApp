using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace QuanLySinhVien
{
    class Database
    {
        public string connect;
        public SqlConnection connection;
        //public DataTable data;
        public SqlDataReader reader;
        public Database()
        {
            connect = @"Data Source=NGUYENDANHTHANH\SQLEXPRESS;Initial Catalog=QLSinhVien;Integrated Security=True";
            connection = null;
            //data = new DataTable();
        }
        public void connectSql()
        {
            connection = new SqlConnection(connect);
            connection.Open();
        }
        public void disconnectSql()
        {
            connection.Close();
        }
        public void printData()
        {
            string query = "SELECT * FROM sinh_vien";
            //SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
            //adapter.Fill(data);
            SqlCommand cmd = new SqlCommand(query, connection);
            reader = cmd.ExecuteReader();
        }
        public void addData(string id, string hoTen, string ngaySinh, string lop, string que, double toan, double ly, double hoa, double anh, double tb)
        {
            string query = $"INSERT INTO sinh_vien VALUES('{id}', '{hoTen}', '{ngaySinh}', '{lop}', '{que}', {toan}, {ly}, {hoa}, {anh}, {tb})";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.ExecuteNonQuery();
        }
        public void deleteData(string id)
        {
            string query = $"DELETE FROM sinh_vien WHERE id = '{id}'";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.ExecuteNonQuery();
        }
    }
}
