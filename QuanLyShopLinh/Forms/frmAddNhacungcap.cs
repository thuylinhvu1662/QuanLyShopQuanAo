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
	public partial class frmAddNhacungcap : Form
	{
		public frmAddNhacungcap()
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
			if (txtMaNCC.Text.Trim().Length == 0)
			{
				MessageBox.Show("Bạn Chưa Điền Mã Nhân Viên", "Thông báo");
				txtMaNCC.Focus();
				return;

			}
			if (txtTenNCC.Text.Trim().Length == 0)
			{
				MessageBox.Show("Bạn Chưa Điền Tên Nhân Viên", "Thông báo");
				txtTenNCC.Focus();
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
		
			sql = "Select MaNCC From NhaCungCap where MaNCC = N'" + txtMaNCC.Text.Trim() + "'";
			DataTable tblNCC = ThucThiSQL.DocBang(sql);
			if (tblNCC.Rows.Count > 0)
			{
				MessageBox.Show("Mã nhà cung cấp này đã có , bạn phải nhập mã khác", "Thông báo");
				txtMaNCC.Focus();
				txtMaNCC.Text = "";
				return;
			}
			else
			{
				sql = "Insert into NhaCungCap VALUES(N'" + txtMaNCC.Text + "',N'" + txtTenNCC.Text + "',N'" + txtDiachi.Text + "',N'" + txtSdt.Text + "')";
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

		private void frmAddNhacungcap_Load(object sender, EventArgs e)
		{
			string table = "NhaCungCap";
			string key = "CC";
			txtMaNCC.Text = ThucThiSQL.CreateKey(table, key);
			txtMaNCC.ReadOnly = true;
		}

        private void TxtTenNCC_TextChanged(object sender, EventArgs e)
        {

        }

        private void TxtSdt_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
