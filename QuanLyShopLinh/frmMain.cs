using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyShopLinh
{
	public partial class frmMain : Form
	{
		public frmMain()
		{
			InitializeComponent();
		}


		private void thoátToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Forms.frmBaocaodoanhthu f = new Forms.frmBaocaodoanhthu();
			f.StartPosition = FormStartPosition.CenterScreen;
			f.Show();
		}

		private void NhanvienToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Forms.frmNhanvien f = new Forms.frmNhanvien();
			f.StartPosition = FormStartPosition.CenterScreen;
			f.Show();
		}

		private void NhaphangToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Forms.frmPhieunhaphang f = new Forms.frmPhieunhaphang();
			f.StartPosition = FormStartPosition.CenterScreen;
			f.Show();
		}

		private void HoadonToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Forms.frmHoadon f = new Forms.frmHoadon();
			f.StartPosition = FormStartPosition.CenterScreen;
			f.Show();
		}

		private void NhacungcapToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Forms.frmNhacungcap f = new Forms.frmNhacungcap();
			f.StartPosition = FormStartPosition.CenterScreen;
			f.Show();
		}

		private void HangHoaToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Forms.frmMatHang f = new Forms.frmMatHang();
			f.StartPosition = FormStartPosition.CenterScreen;
			f.Show();
		}

		private void thoátToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			this.Close();
		}

        private void MenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}
