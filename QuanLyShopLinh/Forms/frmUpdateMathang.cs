using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyShopLinh.Forms
{
	public partial class frmUpdateMathang : Form
	{
		public frmUpdateMathang()
		{
			InitializeComponent();
		}

		private void btnLuu_Click(object sender, EventArgs e)
		{
			string sql;
			if (txtLoaiMH.Text.Trim().Length == 0)
			{
				MessageBox.Show("Bạn Chưa Điền Tên Loại Mặt Hàng", "Thông báo");
				txtMaMH.Focus();
				return;
			}
			if (txtTenMH.Text.Trim().Length == 0)
			{
				MessageBox.Show("Bạn Chưa Điền Tên Mặt Hàng", "Thông báo");
				txtTenMH.Focus();
				return;
			}
			if (txtDongia.Text.Trim().Length == 0)
			{
				MessageBox.Show("Bạn Chưa Điền Đơn Giá Mặt Hàng", "Thông báo");
				txtDongia.Focus();
				return;
			}
			if (txtSL.Text.Trim().Length == 0)
			{
				MessageBox.Show("Bạn Chưa Điền Số Lượng Mặt Hàng", "Thông báo");
				txtSL.Focus();
				return;
			}
			sql = "UPDATE MatHang Set TenMH =N'" + txtTenMH.Text + "',LoaiMH=N'" + txtLoaiMH.Text + "' where MaMH = N'" + txtMaMH.Text + "'";
			ThucThiSQL.CapNhatDuLieu(sql);
			MessageBox.Show("Bạn Sửa Thành Công", "Success");
			this.Close();
		}

		private void btnHuy_Click(object sender, EventArgs e)
		{
			this.Close();
		}
	}
}
