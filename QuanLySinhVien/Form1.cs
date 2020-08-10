using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLySinhVien
{
    public partial class Form1 : Form
    {
        Database dt = new Database();
        decimal score;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dt.connectSql();
            dt.printData();
            //for (int i = 0; i < dt.data.Rows.Count; i++)
            //{
            //    dataGridView1.Rows.Add(dt.data.Rows[i][0], dt.data.Rows[i][1], dt.data.Rows[i][2], dt.data.Rows[i][3], dt.data.Rows[i][4], dt.data.Rows[i][5].ToString(), dt.data.Rows[i][6].ToString(), dt.data.Rows[i][7].ToString(), dt.data.Rows[i][8].ToString(), dt.data.Rows[i][9].ToString());
            //}
            while(dt.reader.Read())
            {
                dataGridView1.Rows.Add($"{dt.reader["id"]}", $"{dt.reader["ho_ten"]}", $"{dt.reader["ngay_sinh"]}", $"{dt.reader["lop"]}", $"{dt.reader["que"]}", $"{dt.reader["diem_toan"].ToString()}", $"{dt.reader["diem_ly"].ToString()}", $"{dt.reader["diem_hoa"].ToString()}", $"{dt.reader["diem_anh"].ToString()}", $"{dt.reader["diem_tb"].ToString()}");
            }
            dt.disconnectSql();
        }

        public void checkScore(Control c)
        {
            if((!decimal.TryParse(c.Text, out score) || Convert.ToDecimal(c.Text) < 0 || Convert.ToDecimal(c.Text) > 10) && c.Text != "")
            {
                MessageBox.Show("Ban nhap sai diem!", "Canh Bao", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ((TextBox)c).Clear();
                c.Focus();
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            //dt.data.Rows.Clear();
            dataGridView1.Rows.Clear();
            dt.connectSql();
            try
            {
                double tb = (Convert.ToDouble(textBox3.Text) + Convert.ToDouble(textBox4.Text) + Convert.ToDouble(textBox5.Text) + Convert.ToDouble(textBox6.Text)) / 4;
                dt.addData(textBox1.Text, textBox2.Text, dateTimePicker1.Value.Date.ToString("yyyy/MM/dd"), comboBox1.Text, comboBox2.Text, Convert.ToDouble(textBox3.Text), Convert.ToDouble(textBox4.Text), Convert.ToDouble(textBox5.Text), Convert.ToDouble(textBox6.Text), tb);
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Ban can dieu chinh lai thong tin cua sinh vien!\n{ex.Message}", "Loi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            dt.disconnectSql();
            Form1_Load(sender, e);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            dt.connectSql();
            foreach(DataGridViewRow item in this.dataGridView1.SelectedRows)
            {
                dt.deleteData(item.Cells[0].Value.ToString());
            }
            dt.disconnectSql();
            //dt.data.Rows.Clear();
            dataGridView1.Rows.Clear();
            Form1_Load(sender, e);
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            checkScore(textBox3);
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            checkScore(textBox4);
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            checkScore(textBox5);
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            checkScore(textBox6);
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            Control[] list = { textBox1, textBox2, dateTimePicker1, comboBox1, comboBox2, textBox3, textBox4, textBox5, textBox6 };
            for(int i = 0; i < list.Length; i++)
            {
                if(dataGridView1.Rows[e.RowIndex].Cells[i].Value != null)
                {
                    if(list[i] is TextBox || list[i] is ComboBox)
                    {
                        list[i].Text = dataGridView1.Rows[e.RowIndex].Cells[i].Value.ToString();
                    }
                    if(list[i] is DateTimePicker)
                    {
                        ((DateTimePicker)list[i]).Value = DateTime.Parse(dataGridView1.Rows[e.RowIndex].Cells[i].Value.ToString());
                    }
                } else
                {
                    MessageBox.Show("Vui long chon lai sinh vien!", "Thong bao", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Ban can chon thong tin sinh vien truoc khi thay doi!", "Canh Bao", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            button3_Click(sender, e);
            button1_Click(sender, e);
        }
    }
}
