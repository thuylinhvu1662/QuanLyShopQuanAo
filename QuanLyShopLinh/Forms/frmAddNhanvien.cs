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
	public partial class frmAddNhanvien : Form
	{
		public frmAddNhanvien()
		{
			InitializeComponent();
		}

		private void btnHuy_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void btnLuu_Click(object sender, EventArgs e)
		{
			string sql;
			if (txtMaNV.Text.Trim().Length == 0)
			{
				MessageBox.Show("Bạn Chưa Điền Mã Nhân Viên", "Thông báo");
				txtMaNV.Focus();
				return;

			}
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
			sql = "Select MaNV From NhanVien where MaNV = N'" + txtMaNV.Text.Trim() + "'";
			DataTable tblNV = ThucThiSQL.DocBang(sql);
			if (tblNV.Rows.Count > 0)
			{
				MessageBox.Show("Mã Nhân Viên này đã có , bạn phải nhập mã khác", "Thông báo");
				txtMaNV.Focus();
				txtMaNV.Text = "";
				return;
			}
			else
			{
				sql = "Insert into NhanVien(MaNV,TenNV,NgaySinh,GioiTinh,DiaChi,HocVan,SDT) VALUES(N'" + txtMaNV.Text + "',N'" + txtHoten.Text + "'," +txtNgaysinh.Text + ",N'" + txtGioitinh.Text + "',N'" + txtDiachi.Text + "',N'" + txtHocvan.Text + "',N'" + txtSdt.Text + "')";
				ThucThiSQL.CapNhatDuLieu(sql);
				MessageBox.Show("Bạn Thêm Thành Công", "Success");
				this.Close();
			}
		}

		private void txtSdt_KeyPress(object sender, KeyPressEventArgs e)
		{
			if ((e.KeyChar < '0' || e.KeyChar > '9') && (e.KeyChar != 8) && (e.KeyChar != 46))
			{
				e.Handled = true;
			}
		}
		private void frmAddNhanvien_Load(object sender, EventArgs e)
		{
			DataTable dt = new DataTable();
			dt = ThucThiSQL.DocBang("Select * from NhanVien");
			int coso = 0;

			if (dt.Rows.Count == 0)// nếu danh sách nhân viên rỗng
			{
				coso = 1;
			}
			else if (dt.Rows.Count == 1 && int.Parse(dt.Rows[0][0].ToString().Substring(2, 3)) == 1) // nếu danh sách nhân viên có 1 nhân viên và mã người này là NV001
			{
				coso = 2;
			}
			else if (dt.Rows.Count == 1 && int.Parse(dt.Rows[0][0].ToString().Substring(2, 3)) > 1) // nếu danh sách có 1 nhân viên mà mã người này khác NV001
			{
				coso = 1;
			}
			else // nếu danh sách có hơn 1 nhân viên
			{
				for (int i = 0; i < dt.Rows.Count - 1; i++)
				{
					if (int.Parse(dt.Rows[i + 1][0].ToString().Substring(2, 3)) - int.Parse(dt.Rows[i][0].ToString().Substring(2, 3)) > 1)
					{
						coso = int.Parse(dt.Rows[i][0].ToString().Substring(2, 3)) + 1;
						break;
					}
				}
				coso = int.Parse(dt.Rows[dt.Rows.Count - 1][0].ToString().Substring(2, 3)) + 1;
			}
			string ma = "";
			if (coso < 10)
				ma = "NV00" + coso;
			else if (coso < 100)
				ma = "NV0" + coso;
			else
				ma = "NV" + coso;

			txtMaNV.Text = ma;
			txtMaNV.ReadOnly = true;
		}
	}
}
