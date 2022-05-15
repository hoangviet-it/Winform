using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace QL_DLBS
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        SqlConnection conn;
        SqlCommand cm;
        string conString = "";
        SqlDataAdapter da = new SqlDataAdapter();
        DataTable dtb = new DataTable();


        //đọc dữ liệu từ DB, hiển thị ban đầu
        private void Loaddt()
        {
            //chuổi kết nối
            conString = @"Data Source=DESKTOP-QTOPHD4\SQLEXPRESS;Initial Catalog=QL_DLS;Integrated Security=True";

            //khai báo kết nối sql
            conn = new SqlConnection(conString);
            conn.Open();

            cm = conn.CreateCommand();
            cm.CommandText = "SELECT * FROM NHACUNGCAP";
            cm.ExecuteNonQuery();  // thực thi câu truy vấn
            da.SelectCommand = cm;
            dtb.Clear();
            da.Fill(dtb);
            dataGridView1.DataSource = dtb;
            dataGridView1.AutoResizeColumns();
            dataGridView1.AutoResizeRows();
            dataGridView1.AutoResizeColumnHeadersHeight();
        }
        private void Form3_Load(object sender, EventArgs e)
        {
            Loaddt();
        }

        //thêm
        private void button1_Click(object sender, EventArgs e)
        {
            cm = conn.CreateCommand();
            cm.CommandText = "insert into NHACUNGCAP values('"+ textBox3.Text + "', N'"+ textBox2.Text + "', '"+ textBox4.Text+ "', N'"+ textBox1.Text+ "')";
            cm.ExecuteNonQuery();  // thực thi câu truy vấn
            Loaddt();
            MessageBox.Show("Đã thêm thành công !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // xóa
        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult tl = MessageBox.Show("Bạn chắc chắn muốn xóa?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (tl == DialogResult.Yes)
            {
                int rowSelected = Convert.ToInt32(dataGridView1.CurrentRow.Index);
                // Dongbo(rowSelected);
                string ma = dataGridView1.Rows[rowSelected].Cells[0].Value.ToString();
                cm = conn.CreateCommand();
                cm.CommandText = "delete from NHACUNGCAP where MANCC = '" + ma + "'";
                cm.ExecuteNonQuery();  // thực thi câu truy vấn
                Loaddt();
                MessageBox.Show("Đã xóa dữ liệu !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // sửa
        private void button2_Click(object sender, EventArgs e)
        {
            // lấy vị trí hiện tại và đồng bộ form
            int rowSelected = Convert.ToInt32(dataGridView1.CurrentRow.Index);

            // khóa các nút và mã phiếu ko cho sửa
            textBox3.ReadOnly = true;
            button1.Enabled = false;
            button3.Enabled = false;
            textBox3.Text = dataGridView1.Rows[rowSelected].Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.Rows[rowSelected].Cells[1].Value.ToString();
            textBox4.Text = dataGridView1.Rows[rowSelected].Cells[2].Value.ToString();
            textBox1.Text = dataGridView1.Rows[rowSelected].Cells[3].Value.ToString();
        }

        // lưu
        private void button6_Click(object sender, EventArgs e)
        {
            string ma = textBox3.Text;
            DialogResult tl = MessageBox.Show("Bạn muốn cập nhật dữ liệu này ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (tl == DialogResult.Yes)
            {
                cm = conn.CreateCommand();
                cm.CommandText = "update NHACUNGCAP set MANCC = '" + textBox3.Text + "', TENNCC =N'" + textBox2.Text + "', SDT='" + textBox4.Text + "', DIACHI=N'" + textBox1.Text + "' where MANCC = '" + ma + "'";
                cm.ExecuteNonQuery();  // thực thi câu truy vấn
                Loaddt();
                MessageBox.Show("Đã cập nhật !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // đóng
        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //tìm kiếm
        private void button4_Click(object sender, EventArgs e)
        {
            string rowFilter1 = string.Format("{0} like '{1}'", "MANCC", "*" + textBox5.Text + "*");
            (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = rowFilter1;
        }
    }
}
