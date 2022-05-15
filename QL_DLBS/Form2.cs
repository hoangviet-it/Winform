using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QL_DLBS
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private Form currentFormChild;
        private void OpenChildForm(Form childForm)
        {
            if (currentFormChild != null)
            {
                currentFormChild.Close();
            }
            currentFormChild = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panel2.Controls.Add(childForm);
            panel2.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

        // cuộn danh sách các nút
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            panel1.AutoScroll = true;
        }

        // Home
        private void button11_Click(object sender, EventArgs e)
        {
            label1.Text = "     HỆ THỐNG QUẢN LÝ ĐẠI LÝ SÁCH";
            if (currentFormChild != null)
            {
                currentFormChild.Close();
            }
        }

        // vận chuyển
        private void button1_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Form1());
            label1.Text = "     QUẢN LÝ VẬN CHUYỂN";
        }

        // nhà cung cấp
        private void button4_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Form3());
            label1.Text = "     QUẢN LÝ NHÀ CUNG CẤP";
        }

        // đặt hàng
        private void button9_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Form4());
            label1.Text = "     QUẢN LÝ ĐẶT HÀNG";
        }

        // Nhập kho
        private void button7_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Form5());
            label1.Text = "     QUẢN LÝ NHẬP KHO";
        }

        // XUẤT KHO
        private void button12_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Form6());
            label1.Text = "     QUẢN LÝ XUẤT KHO";
        }
    }
}
