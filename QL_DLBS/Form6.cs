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
using System.Text.RegularExpressions;

namespace QL_DLBS
{
    public partial class Form6 : Form
    {
        public Form6()
        {
            InitializeComponent();
        }

        public string conString = @"Data Source=DESKTOP-QTOPHD4\SQLEXPRESS;Initial Catalog=QL_DLS;Integrated Security=True";
        int DataDV_Row = 0;
        Boolean them, sua;

        public string makh = "";
        int DataDVCT_Row = 0;
        Boolean them_CT, sua_CT;

        // Tạo cột cho combobox 1
        public class TenCot
        {
            public string name { get; set; }
            public string acctualname { get; set; }
        }

        //Lấy dữ liệu bảng phiếu xuất kho
        DataSet GetPhieuXuat()
        {

            DataSet data = new DataSet();
            string sql = "select pxk.MAPHIEUXUAT as N'Mã phiếu xuất kho',pxk.MANV as N'Mã nhân viên',"
                         + "pxk.MAHOADON as N'Mã hóa đơn',MAKHACHHANG as N'Mã khách hàng',"
                         + "NGAYXUAT as N'Ngày xuất',NGAYLAP as N'Ngày lập HD'"
                         + "from PHIEUXUATKHO pxk ,HOADONBANHANG hd where pxk.MAHOADON = hd.MAHOADON";
            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(sql, con);
                adapter.Fill(data);
                con.Close();
            }
            return data;
        }

        //Lấy dữ liệu chi tiết phiếu xuất kho
        DataSet GetCTPhieuNhap()
        {

            DataSet data = new DataSet();
            string sql = "select ct.MASACH as N'Mã sách',TENSACH as N'Tên sách', " +
                "SOLUONGDAT as N'Số lượng', ct.DONGIA as N'Đơn giá', " +
                "(SOLUONGDAT*ct.DONGIA) as N'Thành tiền'  " +
                "from CHITIETHOADON ct, DAUSACH ds where ds.MASACH = ct.MASACH  and ct.MAHOADON = N'" + CB_HD.Text + "'";
            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(sql, con);
                adapter.Fill(data);
                con.Close();
            }
            return data;
        }

        //Gán dữ liệu phiếu xuất kho vào datagridview1
        private void loadData()
        {
            DataDV_Row = 0;
            dataGridView1.DataSource = GetPhieuXuat().Tables[0];
            if (dataGridView1.Rows[DataDV_Row].Cells["Mã phiếu xuất kho"].Value != null)
            {
                dataGridView1.CurrentRow.Selected = true;
                dataGridView1.Refresh();
                dataGridView1.AutoResizeColumns();
                dataGridView1.AutoResizeRows();
                dataGridView1.AutoResizeColumnHeadersHeight();
            }

        }

        //Gán dữ liệu chi tiết phiếu xuất vào datagridview2
        public void loaddata_CT()
        {
            DataDVCT_Row = 0;
            dataGridView2.DataSource = GetCTPhieuNhap().Tables[0];
            if (dataGridView2.Rows[DataDVCT_Row].Cells["Mã sách"].Value != null)
            {

                dataGridView2.CurrentRow.Selected = true;
                dataGridView2.Refresh();
                dataGridView2.AutoResizeColumns();
                dataGridView2.AutoResizeRows();
                dataGridView2.AutoResizeColumnHeadersHeight();
            }
        }

        //Gán dữ liệu phiếu xuất vào textbox phiếu xuất
        private void Gandulieu()
        {
            if (DataDV_Row != (dataGridView1.Rows.Count - 1))
            {
                txtMaPXK.Text = dataGridView1.Rows[DataDV_Row].Cells["Mã phiếu xuất kho"].Value.ToString().Trim();
                txtMaKH.Text = dataGridView1.Rows[DataDV_Row].Cells["Mã khách hàng"].Value.ToString().Trim();
                DT_HD.Value = Convert.ToDateTime(dataGridView1.Rows[DataDV_Row].Cells["Ngày lập HD"].Value);
                DT_NgayXuat.Value = Convert.ToDateTime(dataGridView1.Rows[DataDV_Row].Cells["Ngày xuất"].Value);

                CB_NV.Items.Clear();
                CB_NV.Items.Insert(0, dataGridView1.Rows[DataDV_Row].Cells["Mã nhân viên"].Value.ToString().Trim());
                CB_NV.SelectedIndex = 0;


                CB_HD.Items.Clear();
                CB_HD.Items.Insert(0, dataGridView1.Rows[DataDV_Row].Cells["Mã hóa đơn"].Value.ToString().Trim());
                CB_HD.SelectedIndex = 0;

                txtMaHD.Text = CB_HD.Text;
                txtMaKHCT.Text = dataGridView1.Rows[DataDV_Row].Cells["Mã khách hàng"].Value.ToString().Trim();
            }
            else
            {
                CB_HD.Items.Clear();
                CB_HD.Items.Insert(0, "");
                CB_HD.SelectedIndex = 0;

                CB_NV.Items.Clear();
                CB_NV.Items.Insert(0, "");
                CB_NV.SelectedIndex = 0;

                txtMaPXK.Text = "";
                txtMaKH.Text = "";

                txtMaHD.Text = "";
                txtMaKHCT.Text = "";
            }
            dataGridView1.CurrentCell = dataGridView1.Rows[DataDV_Row].Cells[0];


        }

        //Gán dữ liệu chi tiết phiếu xuất vào textbox chi tiết phiếu xuất
        private void GandulieuCT()
        {
            if (DataDVCT_Row != (dataGridView2.Rows.Count - 1))
            {

                txtDonGia.Items.Clear();
                txtDonGia.Items.Insert(0, dataGridView2.Rows[DataDVCT_Row].Cells["Đơn giá"].Value.ToString());
                txtDonGia.SelectedIndex = 0;

                txtSL.Text = dataGridView2.Rows[DataDVCT_Row].Cells["Số lượng"].Value.ToString().Trim();
                txtThanhTien.Text = dataGridView2.Rows[DataDVCT_Row].Cells["Thành tiền"].Value.ToString().Trim();

                CB_TSP.Items.Clear();
                CB_TSP.Items.Insert(0, dataGridView2.Rows[DataDVCT_Row].Cells["Tên sách"].Value.ToString());
                CB_TSP.SelectedIndex = 0;

                txtMaSP.Items.Clear();
                txtMaSP.Items.Insert(0, dataGridView2.Rows[DataDVCT_Row].Cells["Mã sách"].Value.ToString());
                txtMaSP.SelectedIndex = 0;

            }
            else
            {
                CB_TSP.Items.Clear();
                CB_TSP.Items.Insert(0, "");
                CB_TSP.SelectedIndex = 0;

                txtMaSP.Items.Clear();
                txtMaSP.Items.Insert(0, "");
                txtMaSP.SelectedIndex = 0;

                txtDonGia.Items.Clear();
                txtDonGia.Items.Insert(0, "0");
                txtDonGia.SelectedIndex = 0;
                txtSL.Text = "0";
                txtThanhTien.Text = "0";

            }
            dataGridView2.CurrentCell = dataGridView2.Rows[DataDVCT_Row].Cells[0];
        }

        //Load dữ liệu khi mở form
        private void Form6_Load(object sender, EventArgs e)
        {
            List<TenCot> list;
            list = new List<TenCot>
            {
               new TenCot(){name = "Mã phiếu xuất kho", acctualname = "pxk.MAPHIEUXUAT" },
               new TenCot(){name = "Mã nhân viên", acctualname = "pxk.MANV" },
               new TenCot(){name = "Mã hóa đơn", acctualname = "pxk.MAHOADON" },
               new TenCot(){name = "Ngày lập hóa đơn", acctualname = "NGAYLAP" },
               new TenCot(){name = "Ngày xuất", acctualname = "NGAYXUAT" },
            };


            txtMaKH.ReadOnly = true;

            txtMaPXK.ReadOnly = true;
            txtDonGia.DropDownStyle = ComboBoxStyle.DropDownList;
            txtMaHD.ReadOnly = true;
            txtMaKHCT.ReadOnly = true;

            txtSL.ReadOnly = true;
            txtThanhTien.ReadOnly = true;

            CB_TSP.DropDownStyle = ComboBoxStyle.DropDownList;

            dataGridView1.ReadOnly = true;
            dataGridView2.ReadOnly = true;

            DT_HD.Enabled = false;
            DT_NgayXuat.Enabled = false;

            btnLuu.Enabled = false;
            btnHuy.Enabled = false;
            btnHuy_CT.Enabled = false;
            btnLuu_CT.Enabled = false;

            comboBox1.DataSource = list;
            comboBox1.DisplayMember = "name";
            comboBox2.Enabled = false;

            comboBox2.Items.Add("Ngày");
            comboBox2.Items.Add("Tháng");
            comboBox2.Items.Add("Năm");


            loadData();
            Gandulieu();
            loaddata_CT();
            GandulieuCT();

            CB_HD.DropDownStyle = ComboBoxStyle.DropDownList;
        }


        //=================================================
        //               Phiếu xuất kho

        //Xử lý sự kiện khi nhấn vào thành vần datagridview1
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //Lấy ví trị row trong datagridview
            DataDV_Row = Convert.ToInt32(dataGridView1.CurrentRow.Index);
            if ((them == false && sua == false) && (them_CT == false && sua_CT == false))
            {
                Gandulieu();
                loaddata_CT();
                GandulieuCT();
            }

        }

        //Về dòng đầu tiên bảng datagridview1
        private void button6_Click(object sender, EventArgs e)
        {
            DataDV_Row = 0;
            Gandulieu();
        }

        //Lùi về trước một dòng bảng datagridview1
        private void button7_Click(object sender, EventArgs e)
        {
            if (DataDV_Row != 0)
            {
                DataDV_Row--;
                Gandulieu();
            }
        }

        //Tiến về trước một dòng bảng datagridview1
        private void button8_Click(object sender, EventArgs e)
        {
            if (DataDV_Row < (dataGridView1.Rows.Count - 2))
            {
                DataDV_Row++;
                Gandulieu();
            }
        }

        //Về cuối dòng bảng datagridview1
        private void button9_Click(object sender, EventArgs e)
        {
            DataDV_Row = Convert.ToInt32(dataGridView1.Rows.Count) - 2;

            //Gán dữ liệu cho text box
            Gandulieu();
        }

        //Xử lý sự kiện khi nhấn nut THÊM phiếu nhập
        private void btnThem_Click(object sender, EventArgs e)
        {
            GetMaNV();
            GetMaHD();
            them = true;

            if (dataGridView1.Rows.Count != 1)
            {
                string value = dataGridView1.Rows[dataGridView1.Rows.Count - 2].Cells["Mã phiếu xuất kho"].Value.ToString().Trim();
                Regex re = new Regex(@"([a-zA-Z]+)(\d+)");
                Match result = re.Match(value);
                string alphaPart = result.Groups[1].Value;
                string numberPart = result.Groups[2].Value;
                int number = int.Parse(numberPart) + 1;
                txtMaPXK.Text = alphaPart + number.ToString("D3");
            }
            else
                txtMaPXK.Text = "PXK001";


            DT_NgayXuat.Enabled = true;

            txtMaKH.Enabled = false;
            txtTimkiem.Enabled = false;

            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnThem.Enabled = false;
            btnIn.Enabled = false;

            btnLuu.Enabled = true;
            btnHuy.Enabled = true;
        }

        //Xử lý sự kiện khi nhấn nút HỦY phiếu nhập
        private void btnHuy_Click(object sender, EventArgs e)
        {
            CB_HD.DataSource = null;
            CB_NV.DataSource = null;
            Gandulieu();

            txtMaPXK.Enabled = true;
            txtMaKH.Enabled = true;

            txtTimkiem.Enabled = true;

            DT_NgayXuat.Enabled = false;
            DT_HD.Enabled = false;

            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            btnThem.Enabled = true;
            btnIn.Enabled = true;
            btnSua.Enabled = true;

            btnLuu.Enabled = false;
            btnHuy.Enabled = false;

            them = false;
            sua = false;
        }

        //Xử lý sự kiện khi nhấn nút LƯU phiếu nhập
        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (them == true)
            {
                Them();
            }

            else if (sua == true)
            {
                Sua();

            }

            them = false;
            sua = false;


            txtMaKH.ReadOnly = true;

            txtTimkiem.ReadOnly = false;

            dataGridView1.ReadOnly = true;


            txtMaPXK.Enabled = true;
            txtMaKH.Enabled = true;

            txtTimkiem.Enabled = true;

            DT_NgayXuat.Enabled = false;
            DT_HD.Enabled = false;

            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            btnThem.Enabled = true;
            btnIn.Enabled = true;
            btnSua.Enabled = true;

            btnLuu.Enabled = false;
            btnHuy.Enabled = false;

            CB_HD.DataSource = null;
            CB_NV.DataSource = null;

            loadData();
            Gandulieu();
        }

        //Xử lý sự kiện khi nhấn nút XÓA phiếu nhập
        private void btnXoa_Click(object sender, EventArgs e)
        {
            DialogResult tl = MessageBox.Show("Bạn muốn xóa không?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (tl == DialogResult.OK)
            {
                Xoa();
            }
        }

        //Xử lý sự kiện khi nhấn nút SỬA phiếu nhập
        private void btnSua_Click(object sender, EventArgs e)
        {
            them = false;
            sua = true;

            DT_NgayXuat.Enabled = true;

            txtMaKH.ReadOnly = false;

            txtMaPXK.ReadOnly = false;
            dataGridView1.ReadOnly = false;

            txtMaPXK.Enabled = false;
            txtMaKH.Enabled = false;
            txtTimkiem.Enabled = false;

            DT_HD.Enabled = false;


            btnThem.Enabled = false;
            btnXoa.Enabled = false;

            btnLuu.Enabled = true;
            btnHuy.Enabled = true;
        }

        //Hàm lấy mã hóa đơn bán hàng gắn vào CB_HD
        public void GetMaHD()
        {
            String sql = "select MAHOADON, NGAYLAP from HOADONBANHANG where not exists (select MAHOADON from PHIEUXUATKHO where HOADONBANHANG.MAHOADON = PHIEUXUATKHO.MAHOADON)";



            SqlConnection conn = new SqlConnection(conString);
            SqlDataAdapter da = new SqlDataAdapter(sql, conn);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count != 0)
            {
                CB_HD.DataSource = ds.Tables[0];
                CB_HD.DisplayMember = "MAHOADON";
                DT_HD.Text = ds.Tables[0].Rows[0]["NGAYLAP"].ToString();
            }
            else
            {
                CB_HD.Items.Clear();
                CB_HD.Items.Insert(0, "");
                CB_HD.SelectedIndex = 0;
            }
        }

        //Hàm lấy nhân viên đơn bán hàng gắn vào CB_HD
        public void GetMaNV()
        {
            String sql = "select MANV FROM NHANVIEN WHERE MABOPHAN = 'BPK '";

            SqlConnection conn = new SqlConnection(conString);
            SqlDataAdapter da = new SqlDataAdapter(sql, conn);
            DataSet ds = new DataSet();
            da.Fill(ds);
            CB_NV.DataSource = ds.Tables[0];
            CB_NV.DisplayMember = "MANV";
        }

        //Hàm thêm dữ liệu từ text box vào database bẳng proc
        public void Them()
        {
            try
            {
                //Kết nối và khai báo tên Proc, lớp SqlCommand
                SqlConnection conn = new SqlConnection(conString);
                string proc = "ThemPXK";
                SqlCommand cmd = new SqlCommand(proc, conn);
                cmd.CommandText = proc;
                cmd.CommandType = CommandType.StoredProcedure;
                //khai báo 2 biến cho Proc 
                SqlParameter mapx = cmd.Parameters.Add("@maphieuxuat", SqlDbType.Char, 20);
                SqlParameter mahd = cmd.Parameters.Add("@mahd", SqlDbType.Char, 20);
                SqlParameter manv = cmd.Parameters.Add("@manv", SqlDbType.Char, 20);
                SqlParameter ngayxuat = cmd.Parameters.Add("@ngayxuat", SqlDbType.DateTime);

                //gán giá trị cho biến
                mapx.Value = txtMaPXK.Text;
                mahd.Value = CB_HD.Text;
                manv.Value = CB_NV.Text;
                ngayxuat.Value = DT_NgayXuat.Value;
                //khai báo DataAdapter và lấy dữ liệu
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                //tạo DataTable
                DataTable dt = new DataTable();
                da.Fill(dt);
                //Thông báo, làm tươi và load lại dữ liệu cho dataGridView
                MessageBox.Show("Đã lưu");
                dataGridView1.Refresh();

                //LoadData();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            CB_HD.DataSource = null;
            CB_NV.DataSource = null;
        }

        //Hàm luu dữ liệu từ text box sau khi sửa vào database bẳng proc
        public void Sua()
        {
            try
            {
                //Kết nối và khai báo tên Proc, lớp SqlCommand
                SqlConnection conn = new SqlConnection(conString);
                string proc = "SuaPXK";
                SqlCommand cmd = new SqlCommand(proc, conn);
                cmd.CommandText = proc;
                cmd.CommandType = CommandType.StoredProcedure;
                //khai báo 2 biến cho Proc 
                SqlParameter mapxk = cmd.Parameters.Add("@maphieuxuat", SqlDbType.Char, 20);
                SqlParameter mahd = cmd.Parameters.Add("@mahd", SqlDbType.Char, 20);
                SqlParameter manv = cmd.Parameters.Add("@manv", SqlDbType.Char, 20);
                SqlParameter ngayxuat = cmd.Parameters.Add("@ngayxuat", SqlDbType.DateTime);

                //gán giá trị cho biến
                mapxk.Value = txtMaPXK.Text;
                mahd.Value = CB_HD.Text;
                manv.Value = CB_NV.Text;
                ngayxuat.Value = DT_NgayXuat.Value;

                //khai báo DataAdapter và lấy dữ liệu
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                //tạo DataTable
                DataTable dt = new DataTable();
                da.Fill(dt);

                //Thông báo, làm tươi và load lại dữ liệu cho dataGridView
                dataGridView1.Refresh();

                MessageBox.Show("Đã cập nhật");
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            CB_HD.DataSource = null;
            CB_NV.DataSource = null;

        }

        //Hàm xóa dữ liệu database bẳng proc
        public void Xoa()
        {
            try
            {
                //Kết nối và khai báo tên Proc, lớp SqlCommand
                SqlConnection conn = new SqlConnection(conString);
                string proc = "XoaPhieuXuatkho";
                SqlCommand cmd = new SqlCommand(proc, conn);
                cmd.CommandText = proc;
                cmd.CommandType = CommandType.StoredProcedure;
                //khai báo 2 biến cho Proc 
                SqlParameter mapxk = cmd.Parameters.Add("@maphieuxuat", SqlDbType.Char, 20);
                //gán giá trị cho biến

                mapxk.Value = txtMaPXK.Text;

                //khai báo DataAdapter và lấy dữ liệu
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                //tạo DataTable
                DataTable dt = new DataTable();
                da.Fill(dt);
                //Thông báo, làm tươi và load lại dữ liệu cho dataGridView
                MessageBox.Show("Đã xóa");
                dataGridView1.Refresh();
                loadData();
                Gandulieu();


            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            CB_HD.DataSource = null;
            CB_NV.DataSource = null;
        }

        //Sử lý sự kiện khi chọn mục cần tìm kiếm
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtTimkiem.Text = "";
            if (comboBox1.Text == "Ngày lập hóa đơn" || comboBox1.Text == "Ngày xuất")
            {
                txtTimkiem.Text = "";

                comboBox2.Enabled = true;
            }
            else
                comboBox2.Enabled = false;
        }

        //Xử lý chỉ nhận số nhập từ bàn phím khi chọn mục tiềm kiếm bằng ngày lập hoặc ngấy nhập
        private void txtTimkiem_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (comboBox1.Text == "Ngày lập hóa đơn" || comboBox1.Text == "Ngày xuất")
            {
                //Chỉ cho người ta nhập số hoặc phím điều khiển thôi
                if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                    e.Handled = true;
            }
            else
                e.Handled = false;
        }

        //Tìm kiếm dữ liệu bằng txttimkiem
        private void txtTimkiem_TextChanged_1(object sender, EventArgs e)
        {
            if (txtTimkiem.Text == "")
            {
                loadData();
                Gandulieu();
            }
            else
                search(txtTimkiem.Text);
        }

        // Hàm tiếm kiếm trên database
        public void search(string text)
        {
            TenCot list = comboBox1.SelectedValue as TenCot;

            string sql = "";
            if (comboBox1.Text == "Ngày lập hóa đơn" || comboBox1.Text == "Ngày xuất")
            {
                if (comboBox2.Text == "Ngày")
                {
                    sql = "select pxk.MAPHIEUXUAT as N'Mã phiếu xuất kho',pxk.MANV as N'Mã nhân viên',"
                   + "pxk.MAHOADON as N'Mã hóa đơn',MAKHACHHANG as N'Mã khách hàng',"
                   + "NGAYXUAT as N'Ngày xuất',NGAYLAP as N'Ngày lập HD'"
                   + "from PHIEUXUATKHO pxk ,HOADONBANHANG hd where pxk.MAHOADON = hd.MAHOADON and Year( " + list.acctualname.ToString() + ") = Year(getdate())" + "and Month(" + list.acctualname.ToString() + ") = Month(getdate())" + "and Day(" + list.acctualname.ToString() + ") = " + text;

                }
                else if (comboBox2.Text == "Tháng")
                {
                    sql = "select pxk.MAPHIEUXUAT as N'Mã phiếu xuất kho',pxk.MANV as N'Mã nhân viên',"
                  + "pxk.MAHOADON as N'Mã hóa đơn',MAKHACHHANG as N'Mã khách hàng',"
                  + "NGAYXUAT as N'Ngày xuất',NGAYLAP as N'Ngày lập HD'"
                  + "from PHIEUXUATKHO pxk ,HOADONBANHANG hd where pxk.MAHOADON = hd.MAHOADON and Year(" + list.acctualname.ToString() + ") = Year(getdate())" + "and Month(" + list.acctualname.ToString() + ") = " + text;

                }
                else if (comboBox2.Text == "Năm")
                {
                    sql = "select pxk.MAPHIEUXUAT as N'Mã phiếu xuất kho',pxk.MANV as N'Mã nhân viên',"
                   + "pxk.MAHOADON as N'Mã hóa đơn',MAKHACHHANG as N'Mã khách hàng',"
                   + "NGAYXUAT as N'Ngày xuất',NGAYLAP as N'Ngày lập HD'"
                   + "from PHIEUXUATKHO pxk ,HOADONBANHANG hd where pxk.MAHOADON = hd.MAHOADON and Year(" + list.acctualname.ToString() + ") = " + text;

                }

            }
            else
            {
                sql = "select pxk.MAPHIEUXUAT as N'Mã phiếu xuất kho',pxk.MANV as N'Mã nhân viên',"
                    + "pxk.MAHOADON as N'Mã hóa đơn',MAKHACHHANG as N'Mã khách hàng',"
                    + "NGAYXUAT as N'Ngày xuất',NGAYLAP as N'Ngày lập HD'"
                    + "from PHIEUXUATKHO pxk ,HOADONBANHANG hd where pxk.MAHOADON = hd.MAHOADON and " + list.acctualname.ToString() + " LIKE N'%" + text + "%'";
            }

            SqlConnection conn = new SqlConnection(conString);
            SqlDataAdapter da = new SqlDataAdapter(sql, conn);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }

        //Quay về form trung tâm
        private void button10_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //==========================================
        //          Chi tiết phiếu nhập

        //Xử lý sự kiện khi nhấn vào datagridview2
        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //Lấy ví trị row trong datagridview
            if (them_CT == false && sua_CT == false)
            {
                DataDVCT_Row = Convert.ToInt32(dataGridView2.CurrentRow.Index);
                GandulieuCT();
            }
        }

        //Xử lý THÊM chi tiết phiếu nhập
        private void button5_Click(object sender, EventArgs e)
        {
            them_CT = true;

            GetTenSach();

            txtThanhTien.Text = "0";
            txtDonGia.Text = "0";
            txtSL.Text = "0";

            txtSL.ReadOnly = false;
            txtThanhTien.Enabled = false;


            btnSua_CT.Enabled = false;
            btnXoa_CT.Enabled = false;
            btnThem_CT.Enabled = false;


            btnLuu_CT.Enabled = true;
            btnHuy_CT.Enabled = true;
        }

        //Xử lý SỬA chi tiết phiếu nhập
        private void button3_Click(object sender, EventArgs e)
        {

            sua_CT = true;


            txtMaKH.ReadOnly = false;
            txtSL.ReadOnly = false;


            btnSua_CT.Enabled = false;
            btnXoa_CT.Enabled = false;
            btnThem_CT.Enabled = false;


            btnLuu_CT.Enabled = true;
            btnHuy_CT.Enabled = true;
        }

        //Xử lý XÓA chi tiết phiếu nhập
        private void button4_Click(object sender, EventArgs e)
        {
            DialogResult tl = MessageBox.Show("Bạn muốn xóa không?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (tl == DialogResult.OK)
            {
                Xoa_CT();
            }
        }

        //Xử lý LƯU chi tiết phiếu nhập
        private void button2_Click(object sender, EventArgs e)
        {
            if (them_CT == true)
            {
                Them_CT();
            }

            else if (sua_CT == true)
            {
                Sua_CT();

            }

            them_CT = false;
            sua_CT = false;

            txtMaHD.ReadOnly = true;
            txtSL.ReadOnly = true;


            btnSua_CT.Enabled = true;
            btnXoa_CT.Enabled = true;
            btnThem_CT.Enabled = true;


            btnLuu_CT.Enabled = false;
            btnHuy_CT.Enabled = false;

            loaddata_CT();
            GandulieuCT();
        }

        //Xử lý HỦY chi tiết phiếu nhập
        private void button1_Click(object sender, EventArgs e)
        {
            txtDonGia.DataSource = null;
            CB_TSP.DataSource = null;
            txtMaSP.DataSource = null;
            GandulieuCT();

            txtMaHD.ReadOnly = true;

            txtSL.ReadOnly = true;


            btnSua_CT.Enabled = true;
            btnXoa_CT.Enabled = true;
            btnThem_CT.Enabled = true;

            btnLuu_CT.Enabled = false;
            btnHuy_CT.Enabled = false;

            them_CT = false;
            sua_CT = false;
        }

        //Hàm lấy tên sách từ database gắn lên combobox CB_TSP
        public void GetTenSach()
        {
            String sql = "select TENSACH,MASACH,DONGIA from DAUSACH WHERE NOT EXISTS(SELECT MASACH FROM CHITIETHOADON WHERE CHITIETHOADON.MASACH = DAUSACH.MASACH and MAHOADON = '" + CB_HD.Text + "')";

            SqlConnection conn = new SqlConnection(conString);
            SqlDataAdapter da = new SqlDataAdapter(sql, conn);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count != 0)
            {
                txtDonGia.DataSource = ds.Tables[0];
                txtDonGia.DisplayMember = "DONGIA";

                CB_TSP.DataSource = ds.Tables[0];
                CB_TSP.DisplayMember = "TENSACH";
                txtMaSP.DataSource = ds.Tables[0];
                txtMaSP.DisplayMember = "MASACH";

            }
            else
            {
                CB_TSP.Items.Clear();
                CB_TSP.Items.Insert(0, "");
                CB_TSP.SelectedIndex = 0;

                txtMaSP.Items.Clear();
                txtMaSP.Items.Insert(0, "");
                txtMaSP.SelectedIndex = 0;

                txtDonGia.Items.Clear();
                txtDonGia.Items.Insert(0, "");
                txtDonGia.SelectedIndex = 0;
            }

        }

        //Hàm thêm dữ liệu lên database
        public void Them_CT()
        {
            try
            {
                //Kết nối và khai báo tên Proc, lớp SqlCommand
                SqlConnection conn = new SqlConnection(conString);
                string proc = "ThemCTHDBH";
                SqlCommand cmd = new SqlCommand(proc, conn);
                cmd.CommandText = proc;
                cmd.CommandType = CommandType.StoredProcedure;
                //khai báo 2 biến cho Proc 
                SqlParameter mahd = cmd.Parameters.Add("@mahd", SqlDbType.Char, 20);

                SqlParameter tensach = cmd.Parameters.Add("@tensp", SqlDbType.NVarChar, 60);
                SqlParameter soluong = cmd.Parameters.Add("@soluong", SqlDbType.Int);
                SqlParameter dongia = cmd.Parameters.Add("@dongia", SqlDbType.Int);
                //gán giá trị cho biến
                mahd.Value = txtMaHD.Text;
                tensach.Value = CB_TSP.Text;
                soluong.Value = txtSL.Text;
                dongia.Value = txtDonGia.Text;

                //khai báo DataAdapter và lấy dữ liệu
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                //tạo DataTable
                DataTable dt = new DataTable();
                da.Fill(dt);
                //Thông báo, làm tươi và load lại dữ liệu cho dataGridView
                MessageBox.Show("Đã lưu");
                dataGridView1.Refresh();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            CB_TSP.DataSource = null;
            txtMaSP.DataSource = null;
            txtDonGia.DataSource = null;

        }

        //Hàm xóa dữ liệu trên database
        public void Xoa_CT()
        {
            try
            {
                //Kết nối và khai báo tên Proc, lớp SqlCommand
                SqlConnection conn = new SqlConnection(conString);
                string proc = "XoaCTHDBH";
                SqlCommand cmd = new SqlCommand(proc, conn);
                cmd.CommandText = proc;
                cmd.CommandType = CommandType.StoredProcedure;
                //khai báo 2 biến cho Proc 
                SqlParameter mahd = cmd.Parameters.Add("@mahd", SqlDbType.Char, 20);
                SqlParameter masp = cmd.Parameters.Add("@masach", SqlDbType.Char, 20);
                //gán giá trị cho biến

                mahd.Value = CB_HD.Text;
                masp.Value = txtMaSP.Text;

                //khai báo DataAdapter và lấy dữ liệu
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                //tạo DataTable
                DataTable dt = new DataTable();
                da.Fill(dt);
                //Thông báo, làm tươi và load lại dữ liệu cho dataGridView
                MessageBox.Show("Đã xóa");
                dataGridView1.Refresh();

                loaddata_CT();
                GandulieuCT();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            CB_TSP.DataSource = null;
            txtMaSP.DataSource = null;
            txtDonGia.DataSource = null;
        }

        //Hàm sửa dữ liệu trên database
        public void Sua_CT()
        {
            try
            {
                //Kết nối và khai báo tên Proc, lớp SqlCommand
                SqlConnection conn = new SqlConnection(conString);
                string proc = "SuaCTHDBH";
                SqlCommand cmd = new SqlCommand(proc, conn);
                cmd.CommandText = proc;
                cmd.CommandType = CommandType.StoredProcedure;
                //khai báo 2 biến cho Proc 
                SqlParameter mahd = cmd.Parameters.Add("@mahd", SqlDbType.Char, 20);
                SqlParameter masp = cmd.Parameters.Add("@masach", SqlDbType.Char, 20);
                SqlParameter tensp = cmd.Parameters.Add("@tensp", SqlDbType.NVarChar, 60);
                SqlParameter soluong = cmd.Parameters.Add("@soluong", SqlDbType.Int);
                SqlParameter dongia = cmd.Parameters.Add("@dongia", SqlDbType.Int);

                //gán giá trị cho biến
                mahd.Value = CB_HD.Text;
                masp.Value = txtMaSP.Text;
                tensp.Value = CB_TSP.Text;
                if (txtSL.Text == "")
                {
                    soluong.Value = int.Parse("0");
                }
                else
                    soluong.Value = int.Parse(txtSL.Text);
                if (txtDonGia.Text == "")
                {
                    dongia.Value = int.Parse("0");
                }
                else
                    dongia.Value = int.Parse(txtDonGia.Text);


                //khai báo DataAdapter và lấy dữ liệu
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                //tạo DataTable
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView2.Refresh();
                loaddata_CT();
                GandulieuCT();
                MessageBox.Show("Đã cập nhật");
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            CB_TSP.DataSource = null;
            txtMaSP.DataSource = null;
            txtDonGia.DataSource = null;
        }

        //Về đầu dòng datagridview2
        private void btnFirst_Click(object sender, EventArgs e)
        {
            DataDVCT_Row = 0;
            GandulieuCT();
        }

        //Lùi về trước một dòng datagridview2
        private void btnBefore_Click(object sender, EventArgs e)
        {
            if (DataDVCT_Row != 0)
            {
                DataDVCT_Row--;
                GandulieuCT();
            }
        }

        private void txtSL_TextChanged(object sender, EventArgs e)
        {
            if (txtSL.Text != "")
            {
                int sum = int.Parse(txtSL.Text) * int.Parse(txtDonGia.Text);
                txtThanhTien.Text = sum.ToString();
            }
        }

        //Tiến về trước một dòng datagridview2
        private void btnNext_Click(object sender, EventArgs e)
        {
            if (DataDVCT_Row < (dataGridView2.Rows.Count - 2))
            {
                DataDVCT_Row++;
                GandulieuCT();
            }
        }

        //Về cuối dòng datagridview2
        private void btnLast_Click(object sender, EventArgs e)
        {
            DataDVCT_Row = Convert.ToInt32(dataGridView2.Rows.Count) - 2;
            GandulieuCT();
        }

    }
}
