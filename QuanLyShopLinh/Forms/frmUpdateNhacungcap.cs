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
	public partial class frmUpdateNhacungcap : Form
	{
		public frmUpdateNhacungcap()
		{
			InitializeComponent();
		}

		private void btnCapnhat_Click(object sender, EventArgs e)
		{
			string sql;
			if (txtTenNCC.Text.Trim().Length == 0)
			{
				MessageBox.Show("Bạn Chưa Điền Tên Nhà Cung Cấp", "Thông báo");
				txtTenNCC.Focus();
				return;
			}
			if (txtDiachi.Text.Trim().Length == 0)
			{
				MessageBox.Show("Bạn Chưa Điền Địa Chỉ Nhà Cung Cấp", "Thông báo");
				txtDiachi.Focus();
				return;
			}
			if (txtSdt.Text.Trim().Length == 0)
			{
				MessageBox.Show("Bạn Chưa Điền SĐT Nhà Cung Cấp", "Thông báo");
				txtSdt.Focus();
				return;
			}
			sql = "UPDATE NhaCungCap Set TenNCC =N'" + txtTenNCC.Text + "',DiaChi=N'" + txtDiachi.Text + "',SDT=N'" + txtSdt.Text + "' where MaNCC = N'" + txtMaNCC.Text + "'";
			ThucThiSQL.CapNhatDuLieu(sql);
			MessageBox.Show("Bạn Sửa Thành Công", "Success");
			this.Close();
		}
	}
}
