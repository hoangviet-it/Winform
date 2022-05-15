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
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }

        public string conString = @"Data Source=DESKTOP-QTOPHD4\SQLEXPRESS;Initial Catalog=QL_DLS;Integrated Security=True";
        int DataDV_Row = 0;
        int DataDV_CT_Row = 0;
        Boolean them, sua, them_CT, sua_CT;

        // Tạo cột cho combobox 1
        public class TenCot
        {
            public string name { get; set; }
            public string acctualname { get; set; }
        }

        //Lấy bảng của phiếu nhập từ database 
        DataSet GetPhieuNhap()
        {

            DataSet data = new DataSet();
            string sql = "select pnk.maphieunhapkho as N'Mã phiếu nhập kho', " +
                "pn.maphieunhap as N'Mã phiếu nhập hàng', pnk.ngaylap as N'Ngày nhập',  pn.ngaylap, " +
                "pnk.manv as N'Mã nhân viên', tenncc as N'Tên nhà cung cấp'" +
                "from phieunhapkho pnk ,phieunhaphang pn, nhacungcap ncc   " +
                "where ncc.mancc = pn.mancc and pnk.maphieunhap = pn.maphieunhap";
            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(sql, con);
                adapter.Fill(data);
                con.Close();
            }
            return data;
        }

        //Lấy bảng của chi tiết phiếu nhập từ database
        DataSet GetCTPhieuNhap()
        {

            DataSet data = new DataSet();
            string sql = "select  ds.masach as N'Mã sản phẩm', ds.tensach as N'Tên sách', ct.dongia as N'Đơn giá', ct.soluong as N'Số lượng'," +
                         "(ct.soluong * ct.dongia) as N'Thành tiền' from phieunhaphang pn, chitietdondathang ct, " +
                         "dausach ds where pn.maphieunhap = ct.maphieunhap and ds.masach = ct.masach " +
                         "and pn.maphieunhap = '" + CB_PN.Text + "'";

            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(sql, con);
                adapter.Fill(data);
                con.Close();
            }
            return data;
        }

        // Load data từ database lên datagridview1       
        private void loadData()
        {
            DataDV_Row = 0;
            dataGridView1.DataSource = GetPhieuNhap().Tables[0];
            if (dataGridView1.Rows[DataDV_Row].Cells["Mã phiếu nhập kho"].Value != null)
            {
                dataGridView1.CurrentRow.Selected = true;
                dataGridView1.Refresh();
                dataGridView1.AutoResizeColumns();
                dataGridView1.AutoResizeRows();
                dataGridView1.AutoResizeColumnHeadersHeight();
            }

        }

        // Load data từ database lên datagridview2
        private void loaddata_CT()
        {
            DataDV_CT_Row = 0;
            dataGridView2.DataSource = GetCTPhieuNhap().Tables[0];
            if (dataGridView2.Rows[DataDV_CT_Row].Cells["Mã sản phẩm"].Value != null)
            {
                dataGridView2.CurrentRow.Selected = true;
                dataGridView2.Refresh();
                dataGridView2.AutoResizeColumns();
                dataGridView2.AutoResizeRows();
                dataGridView2.AutoResizeColumnHeadersHeight();
            }
        }

        //Đưa dữ liệu của cột đang được chọn từ datagirdview1 lên phần thông tin phiếu nhập
        private void Gandulieu()
        {
            if (DataDV_Row != (dataGridView1.Rows.Count - 1))
            {
                txtMaPNK.Text = dataGridView1.Rows[DataDV_Row].Cells["Mã phiếu nhập kho"].Value.ToString().Trim();
                txtTenNcc.Text = dataGridView1.Rows[DataDV_Row].Cells["Tên nhà cung cấp"].Value.ToString().Trim();
                DTPhieuNhapKho.Value = Convert.ToDateTime(dataGridView1.Rows[DataDV_Row].Cells["Ngày nhập"].Value);
                DTPhieuNhapHang.Value = Convert.ToDateTime(dataGridView1.Rows[DataDV_Row].Cells["ngaylap"].Value);

                CB_PN.Items.Clear();
                CB_PN.Items.Insert(0, dataGridView1.Rows[DataDV_Row].Cells["Mã phiếu nhập hàng"].Value.ToString().Trim());
                CB_PN.SelectedIndex = 0;

                CB_NV.Items.Clear();
                CB_NV.Items.Insert(0, dataGridView1.Rows[DataDV_Row].Cells["Mã nhân viên"].Value.ToString().Trim());
                CB_NV.SelectedIndex = 0;
                dataGridView1.CurrentCell = dataGridView1.Rows[DataDV_Row].Cells[0];
                txtMaPNH.Text = CB_PN.Text;

            }
            else
            {
                CB_PN.Items.Clear();
                CB_PN.Items.Insert(0, "");
                CB_PN.SelectedIndex = 0;

                CB_NV.Items.Clear();
                CB_NV.Items.Insert(0, "");
                CB_NV.SelectedIndex = 0;

                txtMaPNK.Text = "";
                txtTenNcc.Text = "";
                txtMaPNH.Text = "";
                dataGridView1.CurrentCell = dataGridView1.Rows[DataDV_Row].Cells[0];
            }



        }

        //Đưa dữ liệu của cột đang được chọn từ datagirdview2 lên phần thông tin chi tiết 
        private void GandulieuCT()
        {

            if (DataDV_CT_Row != (dataGridView2.Rows.Count - 1))
            {
                txtDonGia.Items.Clear();
                txtDonGia.Items.Insert(0, dataGridView2.Rows[DataDV_CT_Row].Cells["Đơn giá"].Value.ToString());
                txtDonGia.SelectedIndex = 0;
                txtSL.Text = dataGridView2.Rows[DataDV_CT_Row].Cells["Số lượng"].Value.ToString();

                y.Text = dataGridView2.Rows[DataDV_CT_Row].Cells["Thành tiền"].Value.ToString();
                CB_TSP.Items.Clear();
                CB_TSP.Items.Insert(0, dataGridView2.Rows[DataDV_CT_Row].Cells["Tên sách"].Value.ToString());
                CB_TSP.SelectedIndex = 0;

                txtMaSP.Items.Clear();
                txtMaSP.Items.Insert(0, dataGridView2.Rows[DataDV_CT_Row].Cells["Mã sản phẩm"].Value.ToString());
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

                txtDonGia.Text = "0";
                txtSL.Text = "0";
                y.Text = "0";

            }
            dataGridView2.CurrentCell = dataGridView2.Rows[DataDV_CT_Row].Cells[0];
        }

        // Đưa dữ liệu khi load form
        private void Form5_Load(object sender, EventArgs e)
        {
            List<TenCot> list;
            list = new List<TenCot>
            {
               new TenCot(){name = "Mã phiếu nhập kho", acctualname = "maphieunhapkho" },
               new TenCot(){name = "Mã phiếu nhập hàng", acctualname = "pn.maphieunhap" },
               new TenCot(){name = "Ngày lập", acctualname = "pnk.ngaylap" },
               new TenCot(){name = "Mã nhân viên", acctualname = "pnk.manv" },
               new TenCot(){name = "Tên nhà cung cấp", acctualname = "tenncc" },
            };

            txtTenNcc.ReadOnly = true;
            txtMaPNK.ReadOnly = true;
            txtDonGia.DropDownStyle = ComboBoxStyle.DropDownList;
            txtSL.ReadOnly = true;
            y.ReadOnly = true;

            dataGridView1.ReadOnly = true;
            dataGridView2.ReadOnly = true;

            DTPhieuNhapHang.Enabled = false;
            DTPhieuNhapKho.Enabled = false;

            btnLuu.Enabled = false;
            btnHuy.Enabled = false;
            btnHuy_CT.Enabled = false;
            btnLuu_CT.Enabled = false;

            loadData();
            Gandulieu();
            loaddata_CT();
            GandulieuCT();


            comboBox1.DataSource = list;
            comboBox1.DisplayMember = "name";
            comboBox2.Enabled = false;

            comboBox2.Items.Add("Ngày");
            comboBox2.Items.Add("Tháng");
            comboBox2.Items.Add("Năm");


            CB_PN.DropDownStyle = ComboBoxStyle.DropDownList;
            CB_NV.DropDownStyle = ComboBoxStyle.DropDownList;
            CB_TSP.DropDownStyle = ComboBoxStyle.DropDownList;

            dataGridView1.Columns["ngaylap"].Visible = false;
        }

        // ========================================
        //              Phiếu nhập kho

        // Xử lý sự kiện khi nhấn váo thành phần datagridview của phiếu nhập
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

        //Về đầu dòng datagridview1
        private void button6_Click(object sender, EventArgs e)
        {
            DataDV_Row = 0;
            Gandulieu();

        }

        //Nhảy tới cuối dòng datagridview1
        private void button9_Click(object sender, EventArgs e)
        {
            DataDV_Row = Convert.ToInt32(dataGridView1.Rows.Count) - 2;
            Gandulieu();
        }

        //Tiến lên một dòng datagridview1 
        private void button8_Click(object sender, EventArgs e)
        {
            if (DataDV_Row < (dataGridView1.Rows.Count - 2))
            {
                DataDV_Row++;
                Gandulieu();
            }
        }

        //Lui về trước một dòng datagridview1 
        private void button7_Click(object sender, EventArgs e)
        {
            if (DataDV_Row != 0)
            {
                DataDV_Row--;
                Gandulieu();
            }
        }

        // Xử lý sự kiến khi nhấn THÊM trên phiếu nhập 
        private void btnThem_Click_1(object sender, EventArgs e)
        {
            GetMaNV();
            GetMaPN();
            them = true;
            sua = false;

            if (dataGridView1.Rows.Count != 1)
            {
                string value = dataGridView1.Rows[dataGridView1.Rows.Count - 2].Cells["Mã phiếu nhập kho"].Value.ToString().Trim();
                Regex re = new Regex(@"([a-zA-Z]+)(\d+)");
                Match result = re.Match(value);
                string alphaPart = result.Groups[1].Value;
                string numberPart = result.Groups[2].Value;
                int number = int.Parse(numberPart) + 1;
                txtMaPNK.Text = alphaPart + number.ToString("D3");
            }
            else
                txtMaPNK.Text = "PNK001";

            txtTenNcc.Text = "";

            txtTenNcc.Enabled = false;
            txtTimkiem.Enabled = false;

            DTPhieuNhapKho.Enabled = true;
            comboBox1.Enabled = false;

            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnThem.Enabled = false;
            btnIn.Enabled = false;


            btnLuu.Enabled = true;
            btnHuy.Enabled = true;
        }

        // Xử lý sự kiến khi nhấn SỬA trên phiếu nhập 
        private void btnSua_Click_1(object sender, EventArgs e)
        {
            GetMaNV();
            GetMaPN();
            them = false;
            sua = true;

            txtMaPNK.Enabled = false;
            txtTimkiem.Enabled = false;
            txtTenNcc.Enabled = false;

            DTPhieuNhapKho.Enabled = true;
            comboBox1.Enabled = false;

            btnThem.Enabled = false;
            btnXoa.Enabled = false;

            btnLuu.Enabled = true;
            btnHuy.Enabled = true;

        }

        // Xử lý sự kiến khi nhấn LƯU trên phiếu nhập 
        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (them == true)
                Them();
            else
            {

                Sua();

            }

            them = false;
            sua = false;


            txtTenNcc.Enabled = true;
            txtTimkiem.Enabled = true;

            DTPhieuNhapKho.Enabled = false;
            DTPhieuNhapHang.Enabled = false;

            comboBox1.Enabled = true;

            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            btnThem.Enabled = true;
            btnIn.Enabled = true;

            btnSua.Enabled = true;


            btnLuu.Enabled = false;
            btnHuy.Enabled = false;

            loadData();
            Gandulieu();
        }

        // Xử lý sự kiến khi nhấn XÓA trên phiếu nhập 
        private void btnXoa_Click_1(object sender, EventArgs e)
        {
            DialogResult tl = MessageBox.Show("Bạn muốn xóa không?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (tl == DialogResult.OK)
            {
                Xoa();
            }
        }

        // Xử lý sự kiến khi nhấn HỦY trên phiếu nhập 
        private void btnHuy_Click_1(object sender, EventArgs e)
        {
            CB_PN.DataSource = null;
            CB_NV.DataSource = null;
            Gandulieu();

            txtMaPNK.Enabled = true;
            txtTenNcc.Enabled = true;

            txtTimkiem.Enabled = true;

            DTPhieuNhapKho.Enabled = false;
            comboBox1.Enabled = true;

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

        // Hàm lấy mã nhân viên từ database gắn lên combobox CB_NV
        public void GetMaNV()
        {
            String sql = "select MANV from NHANVIEN ";

            SqlConnection conn = new SqlConnection(conString);
            SqlDataAdapter da = new SqlDataAdapter(sql, conn);
            DataSet ds = new DataSet();
            da.Fill(ds);
            CB_NV.DataSource = ds.Tables[0];
            CB_NV.DisplayMember = "MANV";
        }

        // Hàm lấy mã phiếu nhập từ database gắn lên combobox CB_PN
        public void GetMaPN()
        {
            String sql = "select MAPHIEUNHAP from PHIEUNHAPHANG ";

            SqlConnection conn = new SqlConnection(conString);
            SqlDataAdapter da = new SqlDataAdapter(sql, conn);
            DataSet ds = new DataSet();
            da.Fill(ds);
            CB_PN.DataSource = ds.Tables[0];
            CB_PN.DisplayMember = "MAPHIEUNHAP";
        }

        // Hàm Them phiếu nhập lên database
        public void Them()
        {
            try
            {
                //Kết nối và khai báo tên Proc, lớp SqlCommand
                SqlConnection conn = new SqlConnection(conString);
                string proc = "ThemPhieuNhapKho";
                SqlCommand cmd = new SqlCommand(proc, conn);
                cmd.CommandText = proc;
                cmd.CommandType = CommandType.StoredProcedure;
                //khai báo 2 biến cho Proc 
                SqlParameter mapnk = cmd.Parameters.Add("@mapheunhapkho", SqlDbType.Char, 20);
                SqlParameter mapnh = cmd.Parameters.Add("@maphieunhaphang", SqlDbType.Char, 20);
                SqlParameter manv = cmd.Parameters.Add("@manv", SqlDbType.Char, 20);
                SqlParameter ngaynhap = cmd.Parameters.Add("@ngaynhap", SqlDbType.DateTime);
                //gán giá trị cho biến
                mapnk.Value = txtMaPNK.Text;
                mapnh.Value = CB_PN.Text;
                manv.Value = CB_NV.Text;
                ngaynhap.Value = DTPhieuNhapKho.Value;
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

            CB_PN.DataSource = null;
            CB_NV.DataSource = null;
        }

        // Hàm xóa phiếu nhập trên database
        public void Xoa()
        {
            try
            {
                //Kết nối và khai báo tên Proc, lớp SqlCommand
                SqlConnection conn = new SqlConnection(conString);
                string proc = "XoaPhieunhapkho";
                SqlCommand cmd = new SqlCommand(proc, conn);
                cmd.CommandText = proc;
                cmd.CommandType = CommandType.StoredProcedure;
                //khai báo 2 biến cho Proc 
                SqlParameter mapnk = cmd.Parameters.Add("@maphieunhapkho", SqlDbType.Char, 20);
                //gán giá trị cho biến

                mapnk.Value = txtMaPNK.Text;

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
            //Xử lý nút
            txtTenNcc.Enabled = true;

            CB_PN.DataSource = null;
            CB_NV.DataSource = null;

            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            btnThem.Enabled = true;
            btnIn.Enabled = true;


            btnLuu.Enabled = false;
            btnHuy.Enabled = false;
        }

        // Hàm sửa phiếu nhập trên database
        public void Sua()
        {
            try
            {
                //Kết nối và khai báo tên Proc, lớp SqlCommand
                SqlConnection conn = new SqlConnection(conString);
                string proc = "SuaPhieunhapkho";
                SqlCommand cmd = new SqlCommand(proc, conn);
                cmd.CommandText = proc;
                cmd.CommandType = CommandType.StoredProcedure;
                //khai báo 2 biến cho Proc 
                SqlParameter mapnk = cmd.Parameters.Add("@maphieunhapkho", SqlDbType.Char, 20);
                SqlParameter mapnh = cmd.Parameters.Add("@maphieunhap", SqlDbType.Char, 20);
                SqlParameter manv = cmd.Parameters.Add("@manv", SqlDbType.Char, 20);
                SqlParameter tenncc = cmd.Parameters.Add("@tennhacungcap", SqlDbType.NVarChar, 60);
                //SqlParameter ngaynhap = cmd.Parameters.Add("@ngaynhap", SqlDbType.DateTime);
                SqlParameter ngaylap = cmd.Parameters.Add("@ngaylap", SqlDbType.DateTime);
                //gán giá trị cho biến
                mapnk.Value = txtMaPNK.Text;
                mapnh.Value = CB_PN.Text;
                manv.Value = CB_NV.Text;
                tenncc.Value = txtTenNcc.Text;
                //ngaynhap.Value = DTPhieuNhapHang.Value;
                ngaylap.Value = DTPhieuNhapKho.Value;
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
            CB_PN.DataSource = null;
            CB_NV.DataSource = null;

        }

        // Kích hoạt combobox 2 khi chọn ngày lập trên combobox1
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtTimkiem.Text = "";
            if (comboBox1.Text == "Ngày lập")
            {


                comboBox2.Enabled = true;
            }
            else
                comboBox2.Enabled = false;
        }

        // Xử lý tìm kiếm trên phiếu nhập kho
        private void txtTimkiem_TextChanged(object sender, EventArgs e)
        {
            if (txtTimkiem.Text == "")
            {
                loadData();
                Gandulieu();
            }
            else
                search(txtTimkiem.Text);
        }

        // Hàm tiếm kiếm phiếu nhập kho
        public void search(string text)
        {
            TenCot list = comboBox1.SelectedValue as TenCot;
            string sql = "";
            if (comboBox1.Text == "Ngày lập")
            {
                if (comboBox2.Text == "Ngày")
                {
                    sql = "select pnk.maphieunhapkho as N'Mã phiếu nhập kho', pn.maphieunhap as N'Mã phiếu nhập hàng', pnk.ngaylap as N'Ngày lập', pnk.manv as N'Mã nhân viên', tenncc as N'Tên nhà cung cấp'" +
                    "from phieunhapkho pnk ,phieunhaphang pn, nhacungcap ncc   " +
                    "where ncc.mancc = pn.mancc and pnk.maphieunhap = pn.maphieunhap and Year( " + list.acctualname.ToString() + ") = Year(getdate())" + "and Month(" + list.acctualname.ToString() + ") = Month(getdate())" + "and Day(" + list.acctualname.ToString() + ") = " + text;

                }
                else if (comboBox2.Text == "Tháng")
                {
                    sql = "select pnk.maphieunhapkho as N'Mã phiếu nhập kho', pn.maphieunhap as N'Mã phiếu nhập hàng', pnk.ngaylap as N'Ngày lập', pnk.manv as N'Mã nhân viên', tenncc as N'Tên nhà cung cấp'" +
                    "from phieunhapkho pnk ,phieunhaphang pn, nhacungcap ncc   " +
                    "where ncc.mancc = pn.mancc and pnk.maphieunhap = pn.maphieunhap and Year(" + list.acctualname.ToString() + ") = Year(getdate())" + "and Month(" + list.acctualname.ToString() + ") = " + text;

                }
                else if (comboBox2.Text == "Năm")
                {
                    sql = "select pnk.maphieunhapkho as N'Mã phiếu nhập kho', pn.maphieunhap as N'Mã phiếu nhập hàng', pnk.ngaylap as N'Ngày lập', pnk.manv as N'Mã nhân viên', tenncc as N'Tên nhà cung cấp'" +
                    "from phieunhapkho pnk ,phieunhaphang pn, nhacungcap ncc   " +
                    "where ncc.mancc = pn.mancc and pnk.maphieunhap = pn.maphieunhap and YEAR(" + list.acctualname.ToString() + ") = " + text;

                }

            }
            else
            {
                sql = "select pnk.maphieunhapkho as N'Mã phiếu nhập kho', pn.maphieunhap as N'Mã phiếu nhập hàng', pn.ngaylap as N'Ngày lập', pnk.manv as N'Mã nhân viên', tenncc as N'Tên nhà cung cấp'" +
                "from phieunhapkho pnk ,phieunhaphang pn, nhacungcap ncc   " +
                "where ncc.mancc = pn.mancc and pnk.maphieunhap = pn.maphieunhap and " + list.acctualname.ToString() + " LIKE N'%" + text + "%'";
            }
            SqlConnection conn = new SqlConnection(conString);
            SqlDataAdapter da = new SqlDataAdapter(sql, conn);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }

        // Quay trở về form trung tâm
        private void button10_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //===========================================================
        //                  Chi tiết phiếu nhập

        //Về đầu bảng datagridview
        private void btnFirst_Click(object sender, EventArgs e)
        {
            DataDV_CT_Row = 0;
            GandulieuCT();
        }

        //Lui về trước 
        private void btnBefore_Click(object sender, EventArgs e)
        {
            if (DataDV_CT_Row != 0)
            {
                DataDV_CT_Row--;
                GandulieuCT();
            }
        }

        //Tiến lên một ô
        private void btnNext_Click(object sender, EventArgs e)
        {
            if (DataDV_CT_Row < (dataGridView2.Rows.Count - 2))
            {
                DataDV_CT_Row++;
                GandulieuCT();
            }
        }

        //Nhảy tới cuối bảng datagridview
        private void btnLast_Click(object sender, EventArgs e)
        {
            DataDV_CT_Row = Convert.ToInt32(dataGridView2.Rows.Count) - 2;
            GandulieuCT();
        }

        // Xử lý sự kiện khi nhấn vào thành phần datagridview của chi tiết phiếu nhập
        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //Lấy ví trị row trong datagridview
            if (them_CT == false && sua_CT == false)
            {
                DataDV_CT_Row = Convert.ToInt32(dataGridView2.CurrentRow.Index);
                GandulieuCT();
            }

        }

        // Thêm sản phẩm chi tiết phiếu nhập lên database
        private void button11_Click(object sender, EventArgs e)
        {
            them_CT = true;
            GetTenSach();
            txtDonGia.Text = "0";
            txtSL.Text = "0";
            y.Text = "0";


            y.Enabled = false;

            txtSL.ReadOnly = false;


            btnSua_CT.Enabled = false;
            btnXoa_CT.Enabled = false;
            btnThem_CT.Enabled = false;

            btnLuu_CT.Enabled = true;
            btnHuy_CT.Enabled = true;


        }

        // Xóa sản phẩm chi tiết phiếu nhập trên database
        private void btnXoa_CT_Click(object sender, EventArgs e)
        {
            DialogResult tl = MessageBox.Show("Bạn muốn xóa không?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (tl == DialogResult.OK)
            {
                DataGridViewRow dg = dataGridView2.CurrentRow;
                Xoa_CT();
            }
        }

        // Sủa sẩn phẩm chi tiết phiếu nhập trên database
        private void button4_Click_1(object sender, EventArgs e)
        {
            sua_CT = true;
            them_CT = false;

            txtSL.ReadOnly = false;

            btnSua_CT.Enabled = false;
            btnXoa_CT.Enabled = false;
            btnThem_CT.Enabled = false;

            btnLuu_CT.Enabled = true;
            btnHuy_CT.Enabled = true;
        }

        // Xử lý sự kiện khi nhấn nut lưu trên chi tiết phiếu nhập
        private void button3_Click(object sender, EventArgs e)
        {
            if (them_CT == true)
                Them_CT();
            else if (sua_CT == true)
            {
                Sua_CT();
            }

            them_CT = false;
            sua_CT = false;

            txtSL.ReadOnly = true;

            btnSua_CT.Enabled = true;
            btnXoa_CT.Enabled = true;
            btnThem_CT.Enabled = true;

            btnLuu_CT.Enabled = false;
            btnHuy_CT.Enabled = false;

            loaddata_CT();
            GandulieuCT();
        }

        // Xử lý sự kiện khi nhấn nut hủy trên chi tiết phiếu nhập
        private void btnHuy_CT_Click(object sender, EventArgs e)
        {
            txtDonGia.DataSource = null;
            CB_TSP.DataSource = null;
            txtMaSP.DataSource = null;
            GandulieuCT();

            txtSL.ReadOnly = true;

            btnSua_CT.Enabled = true;
            btnXoa_CT.Enabled = true;
            btnThem_CT.Enabled = true;

            btnLuu_CT.Enabled = false;
            btnHuy_CT.Enabled = false;

            them_CT = false;
            sua_CT = false;
            
        }

        // Hàm lấy tên sách từ database gắn lên combobox CB_TSP
        public void GetTenSach()
        {
            String sql = "select TENSACH,MASACH,DONGIA from DAUSACH WHERE NOT EXISTS(SELECT MASACH FROM CHITIETDONDATHANG WHERE CHITIETDONDATHANG.MASACH = DAUSACH.MASACH and MAPHIEUNHAP  = '"+CB_PN.Text+"')";

            SqlConnection conn = new SqlConnection(conString);
            SqlDataAdapter da = new SqlDataAdapter(sql, conn);
            DataSet ds = new DataSet();
            da.Fill(ds);

            if (ds.Tables[0].Rows.Count != 0)
            {
                CB_TSP.DataSource = ds.Tables[0];
                CB_TSP.DisplayMember = "TENSACH";
                txtMaSP.DataSource = ds.Tables[0];
                txtMaSP.DisplayMember = "MASACH";
                txtDonGia.DataSource = ds.Tables[0];
                txtDonGia.DisplayMember = "DONGIA";

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


        private void txtSL_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Chỉ cho người ta nhập số hoặc phím điều khiển thôi
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }

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

        private void txtSL_TextChanged(object sender, EventArgs e)
        {
            if (txtSL.Text != "")
            {
                int sum = int.Parse(txtSL.Text)* int.Parse(txtDonGia.Text);
                y.Text = sum.ToString();
            }
        }



        // Hàm lưu sản phẩm chi tiết phiếu nhập
        public void Them_CT()
        {
            try
            {
                //Kết nối và khai báo tên Proc, lớp SqlCommand
                SqlConnection conn = new SqlConnection(conString);
                string proc = "ThemCTPNH";
                SqlCommand cmd = new SqlCommand(proc, conn);
                cmd.CommandText = proc;
                cmd.CommandType = CommandType.StoredProcedure;
                //khai báo 2 biến cho Proc 
                SqlParameter mapn = cmd.Parameters.Add("@maphieunhap", SqlDbType.Char, 20);
                //SqlParameter mancc = cmd.Parameters.Add("@MaNCC", SqlDbType.Char, 20);
                SqlParameter tensach = cmd.Parameters.Add("@tensp", SqlDbType.NVarChar, 60);
                SqlParameter soluong = cmd.Parameters.Add("@soluong", SqlDbType.Int);
                SqlParameter dongia = cmd.Parameters.Add("@dongia", SqlDbType.Int);
                //gán giá trị cho biến
                mapn.Value = txtMaPNH.Text;
                tensach.Value = CB_TSP.Text;
                soluong.Value = int.Parse(txtSL.Text);
                dongia.Value = int.Parse(txtDonGia.Text);

                //khai báo DataAdapter và lấy dữ liệu
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                //tạo DataTable
                DataTable dt = new DataTable();
                da.Fill(dt);
                //Thông báo, làm tươi và load lại dữ liệu cho dataGridView
                MessageBox.Show("Đã lưu");
                dataGridView2.Refresh();


            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            CB_TSP.DataSource = null;
            txtMaSP.DataSource = null;
            txtDonGia.DataSource = null;
        }

        // Hàm xóa sản phẩm chi tiết phiếu nhập
        public void Xoa_CT()
        {
            try
            {
                //Kết nối và khai báo tên Proc, lớp SqlCommand
                SqlConnection conn = new SqlConnection(conString);
                string proc = "XoaCTPNH";
                SqlCommand cmd = new SqlCommand(proc, conn);
                cmd.CommandText = proc;
                cmd.CommandType = CommandType.StoredProcedure;
                //khai báo 2 biến cho Proc 
                SqlParameter mapn = cmd.Parameters.Add("@maphieunhap", SqlDbType.Char, 20);
                SqlParameter masp = cmd.Parameters.Add("@masp", SqlDbType.Char, 20);
                //gán giá trị cho biến

                mapn.Value = CB_PN.Text;
                masp.Value = txtMaSP.Text;

                //khai báo DataAdapter và lấy dữ liệu
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                //tạo DataTable
                DataTable dt = new DataTable();
                da.Fill(dt);
                //Thông báo, làm tươi và load lại dữ liệu cho dataGridView
                MessageBox.Show("Đã xóa");
                dataGridView2.Refresh();
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

        // Hàm sửa sản phẩm chi tiết phiếu nhập
        public void Sua_CT()
        {
            try
            {
                //Kết nối và khai báo tên Proc, lớp SqlCommand
                SqlConnection conn = new SqlConnection(conString);
                string proc = "SuaCTPNH";
                SqlCommand cmd = new SqlCommand(proc, conn);
                cmd.CommandText = proc;
                cmd.CommandType = CommandType.StoredProcedure;
                //khai báo 2 biến cho Proc 
                SqlParameter mapn = cmd.Parameters.Add("@maphieunhap", SqlDbType.Char, 20);
                SqlParameter masach = cmd.Parameters.Add("@masach", SqlDbType.Char, 20);
                SqlParameter tensp = cmd.Parameters.Add("@tensp", SqlDbType.NVarChar, 60);
                SqlParameter soluong = cmd.Parameters.Add("@soluong", SqlDbType.Int);
                SqlParameter dongia = cmd.Parameters.Add("@dongia", SqlDbType.Int);

                //gán giá trị cho biến
                mapn.Value = CB_PN.Text;
                masach.Value = txtMaSP.Text;
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

                //Thông báo, làm tươi và load lại dữ liệu cho dataGridView
                dataGridView2.Refresh();

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
       
    }
}
