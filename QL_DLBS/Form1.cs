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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        SqlConnection con;
        SqlCommand cm;
        string conString = "";
        SqlDataAdapter da = new SqlDataAdapter();
        DataTable dtb = new DataTable();


        //đọc dữ liệu từ DB, hiển thị ban đầu
        public void Hienthi()
        {
            //chuổi kết nối
            conString = @"Data Source=DESKTOP-QTOPHD4\SQLEXPRESS;Initial Catalog=QL_DLS;Integrated Security=True";

            //khai báo kết nối sql
            con = new SqlConnection(conString);
            con.Open();

            cm = con.CreateCommand();
            cm.CommandText = "SELECT * FROM PHIEUGIAOHANG";
            cm.ExecuteNonQuery();  // thực thi câu truy vấn
            da.SelectCommand = cm;
            dtb.Clear();
            da.Fill(dtb);
            dataGridView1.DataSource = dtb;
            dataGridView1.AutoResizeColumns();
            dataGridView1.AutoResizeRows();
            dataGridView1.AutoResizeColumnHeadersHeight();
        }


        // Hàm show dữ liệu toàn bộ danh sách phiếu giao hàng
        public void Data_timkiem()
        {
            SqlConnection con1;
            SqlCommand cm1;
            string conString1 = "";
            SqlDataAdapter da1 = new SqlDataAdapter();
            DataTable dtb1 = new DataTable();
            //chuổi kết nối
            conString1 = @"Data Source=DESKTOP-QTOPHD4\SQLEXPRESS;Initial Catalog=QL_DLS;Integrated Security=True";

            //khai báo kết nối sql
            con1 = new SqlConnection(conString1);
            con1.Open();

            cm1 = con1.CreateCommand();
            cm1.CommandText = "SELECT * FROM PHIEUGIAOHANG";
            cm1.ExecuteNonQuery();  // thực thi câu truy vấn
            da1.SelectCommand = cm1;
            dtb1.Clear();
            da1.Fill(dtb1);
            dataGridView1.DataSource = dtb1;
            dataGridView1.AutoResizeColumns();
            dataGridView1.AutoResizeRows();
            dataGridView1.AutoResizeColumnHeadersHeight();
        }


        // hàm load data tài xế dgv2
        SqlConnection con2;
        SqlCommand cm2;
        string conString2 = "";
        SqlDataAdapter da2 = new SqlDataAdapter();
        DataTable dt2 = new DataTable();
        public void Hienthi_taixe()
        {
            //chuổi kết nối
            conString2 = @"Data Source=DESKTOP-QTOPHD4\SQLEXPRESS;Initial Catalog=QL_DLS;Integrated Security=True";

            //khai báo kết nối sql
            con2 = new SqlConnection(conString2);
            con2.Open();

            cm2 = con2.CreateCommand();
            cm2.CommandText = "SELECT * FROM NHANVIENGIAOHANG";
            cm2.ExecuteNonQuery();  // thực thi câu truy vấn
            da2.SelectCommand = cm2;
            dt2.Clear();
            da2.Fill(dt2);
            dataGridView2.DataSource = dt2;
            dataGridView2.AutoResizeColumns();
            dataGridView2.AutoResizeRows();
            dataGridView2.AutoResizeColumnHeadersHeight();
        }


        // Hàm show dữ liệu toàn bộ danh sách tài xế
        public void Data_all_tx()
        {
            SqlConnection con3;
            SqlCommand cm3;
            string conString3 = "";
            SqlDataAdapter da3 = new SqlDataAdapter();
            DataTable dtb3 = new DataTable();
            //chuổi kết nối
            conString3 = @"Data Source=DESKTOP-QTOPHD4\SQLEXPRESS;Initial Catalog=QL_DLS;Integrated Security=True";

            //khai báo kết nối sql
            con3 = new SqlConnection(conString3);
            con3.Open();

            cm3 = con3.CreateCommand();
            cm3.CommandText = "SELECT * FROM NHANVIENGIAOHANG";
            cm3.ExecuteNonQuery();  // thực thi câu truy vấn
            da3.SelectCommand = cm3;
            dtb3.Clear();
            da3.Fill(dtb3);
            dataGridView2.DataSource = dtb3;
            dataGridView2.AutoResizeColumns();
            dataGridView2.AutoResizeRows();
            dataGridView2.AutoResizeColumnHeadersHeight();
        }


        string sql = "select * from Phieugiaohang";
        //Hàm hiển thị dữ liệu chi tiết phiếu giao
        public void Hienthi_chitiet()
        {
            //khai báo biến kiểu chuổi
            string conString = @"Data Source=DESKTOP-QTOPHD4\SQLEXPRESS;Initial Catalog=QL_DLS;Integrated Security=True";

            //khai báo kết nối sql
            SqlConnection con4 = new SqlConnection(conString);
            SqlDataAdapter da4 = new SqlDataAdapter(sql, con4);
            DataSet ds4 = new DataSet();
            da4.Fill(ds4, "PHIEUGIAOHANG");
            dataGridView1.DataSource = ds4.Tables[0];
            dataGridView1.AutoResizeColumns();
            dataGridView1.AutoResizeRows();
            dataGridView1.AutoResizeColumnHeadersHeight();
        }


        // Hàm đưa dữ liệu vào combobox
        public void GetData_Combobox()
        {
            string connString5 = @"Data Source=DESKTOP-QTOPHD4\SQLEXPRESS;Initial Catalog=QL_DLS;Integrated Security=True";

            string sql5 = "SELECT * FROM NHANVIEN WHERE MABOPHAN = 'BPVC'";
            string sql6 = "select MAHOADON from HOADONBANHANG where MAHOADON not in (select MAHOADON from PHIEUGIAOHANG)";
            string sql7 = "select MATAIXE from NHANVIENGIAOHANG where MATAIXE not in (select MATAIXE from PHIEUGIAOHANG) or MATAIXE in (select MATAIXE from PHIEUGIAOHANG where TINHTRANG = N'Đã giao') EXCEPT(select MATAIXE from PHIEUGIAOHANG where TINHTRANG = N'Chưa giao')";
            string sql8 = "select MABOPHAN from BOPHAN where MABOPHAN = 'BPVC'";

            SqlConnection conn5 = new SqlConnection(connString5);

            SqlDataAdapter da5 = new SqlDataAdapter(sql5, conn5);
            SqlDataAdapter da6 = new SqlDataAdapter(sql6, conn5);
            SqlDataAdapter da7 = new SqlDataAdapter(sql7, conn5);
            SqlDataAdapter da8 = new SqlDataAdapter(sql8, conn5);

            DataTable dt5 = new DataTable();
            DataTable dt6 = new DataTable();
            DataTable dt7 = new DataTable();
            DataTable dt8 = new DataTable();

            da5.Fill(dt5);
            da6.Fill(dt6);
            da7.Fill(dt7);
            da8.Fill(dt8);

            cbbmanhanvien.DisplayMember = "MANV";
            cbbmanhanvien.ValueMember = "MANV";
            cbbmanhanvien.DataSource = dt5;

            cbbmahoadon.DisplayMember = "MAHOADON";
            cbbmahoadon.ValueMember = "MAHOADON";
            cbbmahoadon.DataSource = dt6;

            cbbmataixe.DisplayMember = "MATAIXE";
            cbbmataixe.ValueMember = "MATAIXE";
            cbbmataixe.DataSource = dt7;

            comboBox6.DisplayMember = "MABOPHAN";
            comboBox6.ValueMember = "MABOPHAN";
            comboBox6.DataSource = dt8;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Hienthi();          // hiển thi gidview1 (phiếu giao hàng)
            Hienthi_taixe();    // hiển thi gidview2 (tài xế)
            GetData_Combobox(); // hiển thị dữ liệu lên combobox
        }

        // datagidview1 phiếu giao
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // lấy vị trí hiện tại
            int rowSelected = Convert.ToInt32(dataGridView1.CurrentRow.Index);

            // chuyển con trỏ về vị trí chọn mới
            if (dataGridView1.Rows[rowSelected].Cells[0] != null)
            {
                // Đưa Control về vị trí của nó
                dataGridView1.CurrentCell = dataGridView1.Rows[rowSelected].Cells[0];

                // Set trạng thái Selected
                dataGridView1.CurrentRow.Selected = true;
            }
        }


        // nút thêm
        private void button4_Click(object sender, EventArgs e)
        {
            string maphieu = txtmaphieu.Text;
            if ((maphieu == "") || (cbbmahoadon.Text == "") || (cbbmanhanvien.Text == "") || (cbbmataixe.Text == "") || (dateTimePicker1.Text == "") || (cbbtinhtrang.Text == "") || (dateTimePicker2.Text == ""))
            {
                MessageBox.Show("Chưa nhập đầy đủ dữ liệu !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (kt_trung_ma(maphieu) == true)
            {
                MessageBox.Show("Mã phiếu đã tồn tại !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                cm = con.CreateCommand();
                cm.CommandText = "insert into PHIEUGIAOHANG values('" + txtmaphieu.Text + "','" + dateTimePicker1.Text + "', N'" + txtghichu.Text + "', N'" + cbbtinhtrang.Text + "', '" + cbbmataixe.Text + "', '" + cbbmahoadon.Text + "', '" + cbbmanhanvien.Text + "', '" + dateTimePicker2.Text + "')";
                cm.ExecuteNonQuery();  // thực thi câu truy vấn
                Data_timkiem();
                MessageBox.Show("Đã thêm thành công !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            // set lại dữ liệu trên combobox khi xóa dữ liệu liên quan
            GetData_Combobox();
        }



        // Hàm kiểm tra trùng mã khi thêm
        private bool kt_trung_ma(string ma)
        {
            try
            {
                string conString = @"Data Source=DESKTOP-QTOPHD4\SQLEXPRESS;Initial Catalog=QL_DLS;Integrated Security=True";
                //khai báo kết nối sql
                SqlConnection con = new SqlConnection(conString);
                con.Open();
                SqlCommand cm = new SqlCommand();
                cm.CommandType = CommandType.Text;
                cm.CommandText = "select * from PHIEUGIAOHANG where MAPHIEUGIAO = '" + ma + "'";
                cm.Connection = con;

                // chuyển string về int (đang dùng string nên ko cần chuyển)
                //SqlParameter pram = new SqlParameter("@ma", SqlDbType.Int);
                //pram.Value = ma;
                //cm.Parameters.Add(pram);

                SqlDataReader reader = cm.ExecuteReader();
                bool kq = reader.Read();  // kiểm tra mã đã có trong database
                reader.Close();
                return kq;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return false;
        }


        // nút xóa
        private void button6_Click(object sender, EventArgs e)
        {
            DialogResult tl = MessageBox.Show("Bạn chắc chắn muốn xóa?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (tl == DialogResult.Yes)
            {
                int rowSelected = Convert.ToInt32(dataGridView1.CurrentRow.Index);
                // Dongbo(rowSelected);
                string maph = dataGridView1.Rows[rowSelected].Cells[0].Value.ToString();
                cm = con.CreateCommand();
                cm.CommandText = "delete from PHIEUGIAOHANG where MAPHIEUGIAO = '" + maph + "'";
                cm.ExecuteNonQuery();  // thực thi câu truy vấn
                Data_timkiem();
                MessageBox.Show("Đã xóa dữ liệu !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            // set lại dữ liệu trên combobox khi xóa dữ liệu liên quan
            GetData_Combobox();
        }


        // nút sửa
        private void button5_Click(object sender, EventArgs e)
        {
            // lấy vị trí hiện tại và đồng bộ form
            int rowSelected = Convert.ToInt32(dataGridView1.CurrentRow.Index);

            // khóa các nút và mã phiếu ko cho sửa
            txtmaphieu.ReadOnly = true;
            button2.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = false;
            button6.Enabled = false;
            button8.Enabled = false;
            button9.Enabled = false;
            button10.Enabled = false;
            button11.Enabled = false;
            button12.Enabled = false;
            Dongbo(rowSelected);
        }


        // nút lưu
        private void button7_Click(object sender, EventArgs e)
        {
            string maphieu = txtmaphieu.Text;
            DialogResult tl = MessageBox.Show("Bạn muốn cập nhật dữ liệu này ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (tl == DialogResult.Yes)
            {
                cm = con.CreateCommand();
                cm.CommandText = "update PHIEUGIAOHANG set NGAYGIAO = '" + dateTimePicker1.Text + "', GHICHU =N'" + txtghichu.Text + "', TINHTRANG=N'" + cbbtinhtrang.Text + "', MATAIXE='" + cbbmataixe.Text + "', MAHOADON='" + cbbmahoadon.Text + "', MANV='" + cbbmanhanvien.Text + "', NGAYLAP='" + dateTimePicker2.Text + "' where MAPHIEUGIAO = '" + maphieu + "'";
                //  con.Open();
                cm.ExecuteNonQuery();  // thực thi câu truy vấn
                Data_timkiem();
                MessageBox.Show("Đã cập nhật !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            // set lại dữ liệu trên combobox khi xóa dữ liệu liên quan
            GetData_Combobox();
        }


        // Hàm đồng bộ
        private void Dongbo(int rowSelected)
        {
            // chuyển con trỏ về vị trí chọn mới
            if (dataGridView1.Rows[rowSelected].Cells[0] != null)
            {
                // Đưa Control về vị trí của nó
                dataGridView1.CurrentCell = dataGridView1.Rows[rowSelected].Cells[0];

                // Set trạng thái Selected
                dataGridView1.CurrentRow.Selected = true;
            }

            // đồng bộ
            txtmaphieu.Text = dataGridView1.Rows[rowSelected].Cells[0].Value.ToString().Trim();
            cbbmanhanvien.Text = dataGridView1.Rows[rowSelected].Cells[6].Value.ToString().Trim();
            cbbmataixe.Text = dataGridView1.Rows[rowSelected].Cells[4].Value.ToString().Trim();
            cbbmahoadon.Text = dataGridView1.Rows[rowSelected].Cells[5].Value.ToString().Trim();
            dateTimePicker1.Text = dataGridView1.Rows[rowSelected].Cells[1].Value.ToString().Trim();
            dateTimePicker2.Text = dataGridView1.Rows[rowSelected].Cells[7].Value.ToString().Trim();
            cbbtinhtrang.Text = dataGridView1.Rows[rowSelected].Cells[3].Value.ToString().Trim();
            txtghichu.Text = dataGridView1.Rows[rowSelected].Cells[2].Value.ToString().Trim();

            // khóa mã phiếu khi đồng bộ
            //txtmaphieu.Enabled = false;
            txtmaphieu.ReadOnly = true;
        }


        // nút làm mới
        private void button13_Click(object sender, EventArgs e)
        {
            // làm mới datagidview2 bên tài xế
            if (checkBox1.Checked == true)
            {
                Data_all_tx();

                //reset hình ảnh
                pictureBox1.Image = Properties.Resources.users;
                textBox4.ResetText();
                comboBox6.ResetText();
                textBox5.ResetText();
                textBox6.ResetText();
                comboBox7.ResetText();
                textBox7.ResetText();
                textBox8.ResetText();
                textBox9.ResetText();
                button20.Enabled = true;
                button16.Enabled = true;
                button19.Enabled = true;
                textBox4.ReadOnly = false;
            }
            else
            {
                Data_timkiem();
                txtmaphieu.ResetText();
                cbbmanhanvien.ResetText();
                cbbmataixe.ResetText();
                cbbmahoadon.ResetText();
                dateTimePicker2.ResetText();
                dateTimePicker1.ResetText();
                cbbtinhtrang.ResetText();
                txtghichu.ResetText();
                txttimkiem.ResetText();
                cbbmuctim.ResetText();
                txtmaphieu.ReadOnly = false;

                //mở khóa các nút
                button1.Enabled = true;
                button2.Enabled = true;
                button3.Enabled = true;
                button4.Enabled = true;
                button5.Enabled = true;
                button6.Enabled = true;
                button7.Enabled = true;
                button9.Enabled = true;
                button10.Enabled = true;
                button11.Enabled = true;
                button12.Enabled = true;
                button8.Enabled = true;
                cbbmuctim.Enabled = true;
                txttimkiem.Enabled = true;

                // set lại dữ liệu trên combobox khi xóa dữ liệu liên quan
                GetData_Combobox();
            }
        }


        // nút tìm kiếm
        private void button1_Click(object sender, EventArgs e)
        {
            if (cbbmuctim.SelectedIndex == 0)
            {
                string data = "MAPHIEUGIAO";
                Timkiem(data);
            }
            else if (cbbmuctim.SelectedIndex == 1)
            {
                string data = "MANV";
                Timkiem(data);
            }
            else if (cbbmuctim.SelectedIndex == 2)
            {
                string data = "MAHOADON";
                Timkiem(data);
            }
            else if (cbbmuctim.SelectedIndex == 3)
            {
                string data = "MATAIXE";
                Timkiem(data);
            }
            else if (cbbmuctim.SelectedIndex == 4)
            {
                string rowFilter1 = string.Format("convert(NGAYLAP, 'System.String') Like '%{0}%'", txttimkiem.Text);
                (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = rowFilter1;
            }
            else if (cbbmuctim.SelectedIndex == 5)
            {
                string rowFilter1 = string.Format("convert(NGAYGIAO, 'System.String') Like '%{0}%'", txttimkiem.Text);
                (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = rowFilter1;
            }
            else if (cbbmuctim.SelectedIndex == 6)
            {
                string data = "TINHTRANG";
                Timkiem(data);
            }
            else if (cbbmuctim.SelectedIndex == 7)
            {
                Data_timkiem();
            }
            else if (cbbmuctim.SelectedIndex == 10)
            {
                string data = "MATAIXE";
                Timkiem_tx_ma(data);
            }
            else if (cbbmuctim.SelectedIndex == 11)
            {
                string data = "TEN";
                Timkiem_tx_ma(data);
            }
            else
            {
                MessageBox.Show("Chưa chọn mục tìm kiếm !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            if (txttimkiem.Text == "" && cbbmuctim.Text != "")
            {
                MessageBox.Show("Chưa nhập dữ liệu tìm kiếm !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        // Hàm tìm kiếm
        private void Timkiem(string data)
        {
            string rowFilter1 = string.Format("{0} like '{1}'", data, "*" + txttimkiem.Text + "*");
            (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = rowFilter1;
        }

        // Hàm tìm kiếm thông tin mã và họ tài xế
        private void Timkiem_tx_ma(string data)
        {
            string rowFilter2 = string.Format("{0} like '{1}'", data, "*" + txttimkiem.Text + "*");
            (dataGridView2.DataSource as DataTable).DefaultView.RowFilter = rowFilter2;
        }


        // hướng dẫn tìm kiếm
        private void cbbmuctim_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbmuctim.SelectedIndex == 5 || cbbmuctim.SelectedIndex == 4)
            {
                MessageBox.Show("Nhập theo thứ tự: Tháng / Ngày / Năm.", "Hướng dẫn nhập dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        // nút xem chi tiết
        private void button3_Click(object sender, EventArgs e)
        {
            // các chức năng ko sử dụng khi xem chi tiết phiếu giao
            button1.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = false;
            button5.Enabled = false;
            button6.Enabled = false;
            button7.Enabled = false;
            button8.Enabled = false;
            cbbmuctim.Enabled = false;

            int rowSelected = Convert.ToInt32(dataGridView1.CurrentRow.Index);
            string str = dataGridView1.Rows[rowSelected].Cells[0].Value.ToString();
            sql = "select d.TENSACH as N'TÊN SẢN PHẨM', c.SOLUONGDAT as N'SỐ LƯỢNG', c.DONGIA as N'ĐƠN GIÁ', c.DONGIA*c.SOLUONGDAT as N'TỔNG TIỀN', k.HO + ' ' + k.TEN as N'TÊN KHÁCH HÀNG', k.DIACHI as N'Đ/C NHẬN HÀNG', p.MAPHIEUGIAO, p.NGAYGIAO, n.HO + ' ' + n.TEN as N'NHÂN VIÊN GIAO' from PHIEUGIAOHANG p, HOADONBANHANG h, KHACHHANG k, NHANVIENGIAOHANG n, CHITIETHOADON c, DAUSACH d where p.MAHOADON = h.MAHOADON and h.MAKHACHHANG = k.MAKHACHHANG and n.MATAIXE = p.MATAIXE and h.MAHOADON = c.MAHOADON and c.MASACH = d.MASACH and p.MAPHIEUGIAO = '" + str + "'";

            Hienthi_chitiet();
        }


        // nút về đầu
        private void button9_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                int rowSelected1 = Convert.ToInt32(dataGridView2.CurrentRow.Index);

                //trừ đi 1 vị trí
                if (rowSelected1 != 0)
                {
                    rowSelected1 = 0;
                }
                else
                {
                    rowSelected1 = rowSelected1;
                }

                // chuyển con trỏ về vị trí chọn mới
                if (dataGridView2.Rows[rowSelected1].Cells[0] != null)
                {
                    // Đưa Control về vị trí của nó
                    dataGridView2.CurrentCell = dataGridView2.Rows[rowSelected1].Cells[0];

                    // Set trạng thái Selected
                    dataGridView2.CurrentRow.Selected = true;
                }
            }
            else
            {
                int rowSelected = Convert.ToInt32(dataGridView1.CurrentRow.Index);

                //trừ đi 1 vị trí
                if (rowSelected != 0)
                {
                    rowSelected = 0;
                }
                else
                {
                    rowSelected = rowSelected;
                }

                // chuyển con trỏ về vị trí chọn mới
                if (dataGridView1.Rows[rowSelected].Cells[0] != null)
                {
                    // Đưa Control về vị trí của nó
                    dataGridView1.CurrentCell = dataGridView1.Rows[rowSelected].Cells[0];

                    // Set trạng thái Selected
                    dataGridView1.CurrentRow.Selected = true;
                }
            }
        }


        // nút lùi 1
        private void button10_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                // lấy vị trí đang chọn trong datagidview
                int rowSelected1 = Convert.ToInt32(dataGridView2.CurrentRow.Index);

                //trừ đi 1 vị trí
                if (rowSelected1 != 0)
                {
                    rowSelected1--;
                }
                else
                {
                    rowSelected1 = rowSelected1;
                }

                // chuyển con trỏ về vị trí chọn mới
                if (dataGridView2.Rows[rowSelected1].Cells[0] != null)
                {
                    // Đưa Control về vị trí của nó
                    dataGridView2.CurrentCell = dataGridView2.Rows[rowSelected1].Cells[0];

                    // Set trạng thái Selected
                    dataGridView2.CurrentRow.Selected = true;
                }
            }
            else
            {
                // lấy vị trí đang chọn trong datagidview
                int rowSelected = Convert.ToInt32(dataGridView1.CurrentRow.Index);

                //trừ đi 1 vị trí
                if (rowSelected != 0)
                {
                    rowSelected--;
                }
                else
                {
                    rowSelected = rowSelected;
                }

                // chuyển con trỏ về vị trí chọn mới
                if (dataGridView1.Rows[rowSelected].Cells[0] != null)
                {
                    // Đưa Control về vị trí của nó
                    dataGridView1.CurrentCell = dataGridView1.Rows[rowSelected].Cells[0];

                    // Set trạng thái Selected
                    dataGridView1.CurrentRow.Selected = true;
                }
            }
        }


        // nút tới 1
        private void button11_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                // lấy vị trí đang chọn trong datagidview
                int rowSelected1 = Convert.ToInt32(dataGridView2.CurrentRow.Index);

                //trừ đi 1 vị trí
                if (rowSelected1 != dataGridView2.Rows.Count - 2)
                {
                    rowSelected1++;
                }
                else
                {
                    rowSelected1 = rowSelected1;
                }

                // chuyển con trỏ về vị trí chọn mới
                if (dataGridView2.Rows[rowSelected1].Cells[0] != null)
                {
                    // Đưa Control về vị trí của nó
                    dataGridView2.CurrentCell = dataGridView2.Rows[rowSelected1].Cells[0];

                    // Set trạng thái Selected
                    dataGridView2.CurrentRow.Selected = true;
                }
            }
            else
            {
                // lấy vị trí đang chọn trong datagidview
                int rowSelected = Convert.ToInt32(dataGridView1.CurrentRow.Index);

                //trừ đi 1 vị trí
                if (rowSelected != dataGridView1.Rows.Count - 2)
                {
                    rowSelected++;
                }
                else
                {
                    rowSelected = rowSelected;
                }

                // chuyển con trỏ về vị trí chọn mới
                if (dataGridView1.Rows[rowSelected].Cells[0] != null)
                {
                    // Đưa Control về vị trí của nó
                    dataGridView1.CurrentCell = dataGridView1.Rows[rowSelected].Cells[0];

                    // Set trạng thái Selected
                    dataGridView1.CurrentRow.Selected = true;
                }
            }
        }


        // nút về cuối ds
        private void button12_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                // lấy vị trí đang chọn trong datagidview
                int rowSelected1 = Convert.ToInt32(dataGridView2.CurrentRow.Index);

                //trừ đi 1 vị trí
                if (rowSelected1 != dataGridView2.Rows.Count - 2)
                {
                    rowSelected1 = dataGridView2.Rows.Count - 2;
                }
                else
                {
                    rowSelected1 = rowSelected1;
                }

                // chuyển con trỏ về vị trí chọn mới
                if (dataGridView2.Rows[rowSelected1].Cells[0] != null)
                {
                    // Đưa Control về vị trí của nó
                    dataGridView2.CurrentCell = dataGridView2.Rows[rowSelected1].Cells[0];

                    // Set trạng thái Selected
                    dataGridView2.CurrentRow.Selected = true;
                }
            }
            else
            {
                // lấy vị trí đang chọn trong datagidview
                int rowSelected = Convert.ToInt32(dataGridView1.CurrentRow.Index);

                //trừ đi 1 vị trí
                if (rowSelected != dataGridView1.Rows.Count - 2)
                {
                    rowSelected = dataGridView1.Rows.Count - 2;
                }
                else
                {
                    rowSelected = rowSelected;
                }

                // chuyển con trỏ về vị trí chọn mới
                if (dataGridView1.Rows[rowSelected].Cells[0] != null)
                {
                    // Đưa Control về vị trí của nó
                    dataGridView1.CurrentCell = dataGridView1.Rows[rowSelected].Cells[0];

                    // Set trạng thái Selected
                    dataGridView1.CurrentRow.Selected = true;
                }
            }
        }


        // nút tạo mã phiếu tự động
        private void button8_Click(object sender, EventArgs e)
        {
            int Last_index = dataGridView1.Rows.Count - 2;      // lấy số index dòng cuối
            string Last_data = dataGridView1.Rows[Last_index].Cells[0].Value.ToString();    // lấy mã phiếu tại dòng cuối
            string cat_str = Last_data.Substring(2);       // cắt bỏ "PG", chỉ lấy phần số
            int Last_data_int = int.Parse(cat_str);        // chuyển phần đã cắt về int   (dữ liệu dòng cuối chưa tăng)

            if ((Last_data_int == 99) || (Last_data_int >= 100))
            {
                Last_data_int++;
                string stt = Last_data_int.ToString();
                txtmaphieu.Text = "PG" + stt;
            }
            else if ((Last_data_int == 9) || (Last_data_int >= 10))
            {
                Last_data_int++;
                string stt = Last_data_int.ToString();
                txtmaphieu.Text = "PG0" + stt;
            }
            else
            {
                Last_data_int++;
                string stt = Last_data_int.ToString();
                txtmaphieu.Text = "PG00" + stt;
            }
        }


        // mở khóa các nút điều khiển đầu cuối khi check trong chức năng sửa
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            button9.Enabled = true;
            button10.Enabled = true;
            button11.Enabled = true;
            button12.Enabled = true;
        }


        // nút đóng form
        private void button14_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        ///////////////////************************************************************************************////////////////////////////
        // phần thông tị tài xế

        // datagidview2 tài xế
        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowSelected = Convert.ToInt32(dataGridView2.CurrentRow.Index);

            // chuyển con trỏ về vị trí chọn mới
            if (dataGridView2.Rows[rowSelected].Cells[0] != null)
            {
                // Đưa Control về vị trí của nó
                dataGridView2.CurrentCell = dataGridView2.Rows[rowSelected].Cells[0];

                // Set trạng thái Selected
                dataGridView2.CurrentRow.Selected = true;
            }
           // if (rowSelected != dataGridView2.Rows.Count - 1)
              //  Dongbo_tx(rowSelected);
        }


        // Hàm đồng bộ tài xế
        private void Dongbo_tx(int rowSelected)
        {
            textBox4.Text = dataGridView2.Rows[rowSelected].Cells[0].Value.ToString().Trim();
            comboBox6.Text = dataGridView2.Rows[rowSelected].Cells[5].Value.ToString().Trim();
            textBox5.Text = dataGridView2.Rows[rowSelected].Cells[1].Value.ToString().Trim();
            textBox6.Text = dataGridView2.Rows[rowSelected].Cells[2].Value.ToString().Trim();
            comboBox7.Text = dataGridView2.Rows[rowSelected].Cells[8].Value.ToString().Trim();
            textBox7.Text = dataGridView2.Rows[rowSelected].Cells[4].Value.ToString().Trim();
            textBox8.Text = dataGridView2.Rows[rowSelected].Cells[6].Value.ToString().Trim();
            textBox9.Text = dataGridView2.Rows[rowSelected].Cells[3].Value.ToString().Trim();
            label21.Text = dataGridView2.Rows[rowSelected].Cells[7].Value.ToString().Trim();

            // hình
            string matx = dataGridView2.Rows[rowSelected].Cells[0].Value.ToString();
            SqlConnection con3 = new SqlConnection(@"Data Source=DESKTOP-QTOPHD4\SQLEXPRESS;Initial Catalog=QL_DLS;Integrated Security=True");
            con3.Open();
            SqlCommand cm3 = new SqlCommand("select Hinh from NHANVIENGIAOHANG where MATAIXE = '" + matx + "'", con3);
            string img = cm3.ExecuteScalar().ToString();
            pictureBox1.Image = Image.FromFile(img);
            con.Close();
        }


        // upload hình tài xế
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            // lấy đường dẫn của file visual đang thực thi
            // string Link = System.IO.Directory.GetCurrentDirectory();

            // load hình từ máy tính
            string imageLocation = "";
            string link = "";
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "jpg files(*.jpg)|*.jpg| PNG files(*.png)|*.png| All Files(*.*)|*.*";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                // hiển thị hình lên picturebox
                imageLocation = dialog.FileName;
                pictureBox1.ImageLocation = imageLocation;

                // lấy đường dẫn hình
                link = dialog.FileName;
            }
            label21.Text = link;
        }


        // nút tạo mã tài xế tự động
        private void button19_Click(object sender, EventArgs e)
        {
            int Last_index2 = dataGridView2.Rows.Count - 2;      // lấy số index dòng cuối
            string Last_data2 = dataGridView2.Rows[Last_index2].Cells[0].Value.ToString();    // lấy mã phiếu tại dòng cuối
            string cat_str2 = Last_data2.Substring(2);       // cắt bỏ "PG", chỉ lấy phần số
            int Last_data_int2 = int.Parse(cat_str2);        // chuyển phần đã cắt về int   (dữ liệu dòng cuối chưa tăng)

            if ((Last_data_int2 == 99) || (Last_data_int2 >= 100))
            {
                Last_data_int2++;
                string tt = Last_data_int2.ToString();
                textBox4.Text = "TX" + tt;
            }
            else if ((Last_data_int2 == 9) || (Last_data_int2 >= 10))
            {
                Last_data_int2++;
                string tt = Last_data_int2.ToString();
                textBox4.Text = "TX0" + tt;
            }
            else
            {
                Last_data_int2++;
                string tt = Last_data_int2.ToString();
                textBox4.Text = "TX00" + tt;
            }
        }


        // nút thêm tài xế
        private void button20_Click(object sender, EventArgs e)
        {
            string matx = textBox4.Text;
            string sđt = textBox9.Text;
            if ((matx == "") || (comboBox6.Text == "") || (textBox5.Text == "") || (textBox6.Text == "") || (comboBox7.Text == "") || (textBox7.Text == "") || (textBox8.Text == "") || (textBox9.Text == "") || (label21.Text == ""))
            {
                MessageBox.Show("Chưa nhập đầy đủ dữ liệu !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (kt_trung_ma_taixe(matx) == true)
            {
                MessageBox.Show("Mã tài xế đã tồn tại !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if ((sđt.Length > 10) || (sđt.Length < 10))
            {
                MessageBox.Show("Số điện thoại không hợp lệ !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                cm2 = con2.CreateCommand();
                cm2.CommandText = "insert into NHANVIENGIAOHANG values('" + textBox4.Text + "', N'" + textBox5.Text + "', N'" + textBox6.Text + "', '" + textBox9.Text + "', N'" + textBox7.Text + "', '" + comboBox6.Text + "', '" + textBox8.Text + "', '" + label21.Text + "', N'" + comboBox7.Text + "')";
                cm2.ExecuteNonQuery();  // thực thi câu truy vấn
                Data_all_tx();
                MessageBox.Show("Đã thêm thành công !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        // kiểm tra trùng tài xế
        private bool kt_trung_ma_taixe(string ma)
        {
            try
            {
                string conString = @"Data Source=DESKTOP-QTOPHD4\SQLEXPRESS;Initial Catalog=QL_DLS;Integrated Security=True";
                //khai báo kết nối sql
                SqlConnection con = new SqlConnection(conString);
                con.Open();
                SqlCommand cm = new SqlCommand();
                cm.CommandType = CommandType.Text;
                cm.CommandText = "select * from NHANVIENGIAOHANG where MATAIXE = '" + ma + "'";
                cm.Connection = con;
                SqlDataReader reader = cm.ExecuteReader();
                bool kq = reader.Read();  // kiểm tra mã đã có trong database
                reader.Close();
                return kq;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return false;
        }


        // nút xóa tài xế
        private void button16_Click(object sender, EventArgs e)
        {
            DialogResult tl = MessageBox.Show("Bạn chắc chắn muốn xóa?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (tl == DialogResult.Yes)
            {
                int rowSelected1 = Convert.ToInt32(dataGridView2.CurrentRow.Index);
                string matx = dataGridView2.Rows[rowSelected1].Cells[0].Value.ToString();
                if (kt_taixe_exist(matx) == true)
                {
                    MessageBox.Show("Không thể xóa vì tài xế này đang tồn tại trong phiếu giao hàng !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    MessageBox.Show("Hãy xóa các phiếu giao hàng có tài xế này hoặc xóa tài xế này khỏi các phiếu giao hàng  !", "Gợi ý ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    cm2 = con2.CreateCommand();
                    cm2.CommandText = "delete from NHANVIENGIAOHANG where MATAIXE = '" + matx + "'";
                    cm2.ExecuteNonQuery();  // thực thi câu truy vấn
                    Data_all_tx();
                    MessageBox.Show("Đã xóa dữ liệu !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }


        // kiểm tra tài xế đang tồn tại trong phiếu giao hàng
        private bool kt_taixe_exist(string ma)
        {
            try
            {
                string conString = @"Data Source=DESKTOP-QTOPHD4\SQLEXPRESS;Initial Catalog=QL_DLS;Integrated Security=True";
                //khai báo kết nối sql
                SqlConnection con = new SqlConnection(conString);
                con.Open();
                SqlCommand cm = new SqlCommand();
                cm.CommandType = CommandType.Text;
                cm.CommandText = "select MATAIXE from NHANVIENGIAOHANG where MATAIXE in (select MATAIXE from PHIEUGIAOHANG where MATAIXE = '" + ma + "')";
                cm.Connection = con;
                SqlDataReader reader = cm.ExecuteReader();
                bool kq = reader.Read();  // kiểm tra mã đã có trong database
                reader.Close();
                return kq;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return false;
        }


        // nút sửa tài xế
        private void button15_Click(object sender, EventArgs e)
        {
            // lấy vị trí hiện tại và đồng bộ form
            int rowSelected = Convert.ToInt32(dataGridView2.CurrentRow.Index);
            string str = dataGridView2.Rows[rowSelected].Cells[7].Value.ToString();

            textBox4.ReadOnly = true;
            button20.Enabled = false;
            button16.Enabled = false;
            button19.Enabled = false;
            Dongbo_tx(rowSelected);
        }


        // nút lưu tài xế
        private void button17_Click(object sender, EventArgs e)
        {
            string matx = textBox4.Text;
            DialogResult tl = MessageBox.Show("Bạn muốn cập nhật dữ liệu này ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (tl == DialogResult.Yes)
            {
                cm2 = con2.CreateCommand();
                cm2.CommandText = "update NHANVIENGIAOHANG set MATAIXE = '" + textBox4.Text + "', HO = N'" + textBox5.Text + "', TEN = N'" + textBox6.Text + "', SDT = '" + textBox9.Text + "', DIACHI = N'" + textBox7.Text + "', MABOPHAN = '" + comboBox6.Text + "', LUONG = '" + textBox8.Text + "', HINH = '" + label21.Text + "', GIOITINH = N'" + comboBox7.Text + "' where MATAIXE = '" + matx + "'";
                cm2.ExecuteNonQuery();  // thực thi câu truy vấn
                Data_all_tx();
                MessageBox.Show("Đã cập nhật !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
