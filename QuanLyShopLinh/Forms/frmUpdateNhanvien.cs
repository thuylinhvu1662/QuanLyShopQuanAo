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
	public partial class frmUpdateNhanvien : Form
	{
		public frmUpdateNhanvien()
		{
			InitializeComponent();
		}

		private void btnLuu_Click(object sender, EventArgs e)
		{
			string sql;
			if (txtHoten.Text.Trim().Length == 0)
			{
				MessageBox.Show("Bạn Chưa Điền Tên Nhân Viên", "Thông báo");
				txtHoten.Focus();
				return;
			}
			if (txtDiachi.Text.Trim().Length == 0)
			{
				MessageBox.Show("Bạn Chưa Điền Địa Chỉ Nhân Viên", "Thông báo");
				txtDiachi.Focus();
				return;
			}
			if (txtSdt.Text.Trim().Length == 0)
			{
				MessageBox.Show("Bạn Chưa Điền SĐT Nhân Viên", "Thông báo");
				txtSdt.Focus();
				return;
			}
			if (txtGioitinh.Text.Trim().Length == 0)
			{
				MessageBox.Show("Bạn Chưa Điền Giới Tính Nhân Viên", "Thông báo");
				txtGioitinh.Focus();
				return;
			}
			if (txtHocvan.Text.Trim().Length == 0)
			{
				MessageBox.Show("Bạn Chưa Điền Học Vấn Nhân Viên", "Thông báo");
				txtHocvan.Focus();
				return;
			}
			if (txtNgaysinh.Text.Trim().Length == 0)
			{
				MessageBox.Show("Bạn Chưa Điền Ngày Sinh Nhân Viên", "Thông báo");
				txtNgaysinh.Focus();
				return;
			}
			sql = "UPDATE NhanVien Set TenNV =N'" + txtHoten.Text + "',NgaySinh = N'" + txtNgaysinh.Text + "',GioiTinh =N'" + txtGioitinh.Text + "',DiaChi=N'" + txtDiachi.Text + "',HocVan=N'" + txtHocvan.Text + "',SDT=N'"+txtSdt.Text+"' where MaNV = N'" + txtMaNV.Text + "'";
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
