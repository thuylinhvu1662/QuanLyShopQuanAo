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
	public partial class frmNhacungcap : Form
	{
		public frmNhacungcap()
		{
			InitializeComponent();
		}
		private void HienThi_Luoi()
		{

			DataTable tblKH;
			string sql = "select * from NhaCungCap";
			tblKH = ThucThiSQL.DocBang(sql);
			dataGridView1.DataSource = tblKH;
			dataGridView1.Columns[0].HeaderText = "Mã Nhà Cung Cấp";
			dataGridView1.Columns[1].HeaderText = "Tên Nhà Cung Cấp";
			dataGridView1.Columns[2].HeaderText = "Địa Chỉ";
			dataGridView1.Columns[3].HeaderText = "SĐT";

			dataGridView1.AllowUserToAddRows = false;
			dataGridView1.EditMode = DataGridViewEditMode.EditProgrammatically;
			tblKH.Dispose();

		}
		private void frmNhacungcap_Load(object sender, EventArgs e)
		{
			HienThi_Luoi();
		}

		private void frmNhacungcap_Activated(object sender, EventArgs e)
		{
			HienThi_Luoi();
		}

		private void btnSua_Click(object sender, EventArgs e)
		{
			if (dataGridView1.Rows.Count == 0)
			{
				MessageBox.Show("Không có dữ liệu!", "Thông báo");
				return;
			}
			Forms.frmUpdateNhacungcap f = new frmUpdateNhacungcap();
			f.StartPosition = FormStartPosition.CenterScreen;
			f.txtMaNCC.Text = dataGridView1.CurrentRow.Cells["MaNCC"].Value.ToString();
			f.txtTenNCC.Text = dataGridView1.CurrentRow.Cells["TenNCC"].Value.ToString();
			f.txtDiachi.Text = dataGridView1.CurrentRow.Cells["DiaChi"].Value.ToString();
			f.txtSdt.Text = dataGridView1.CurrentRow.Cells["SDT"].Value.ToString();
			f.txtMaNCC.Enabled = false;
			f.Show();
		}

		private void btnXoa_Click(object sender, EventArgs e)
		{
			string sql;
			if (dataGridView1.Rows.Count == 0)
			{
				MessageBox.Show("Không có dữ liệu!", "Thông báo");
				return;
			}
			string ma = dataGridView1.CurrentRow.Cells["MaNCC"].Value.ToString();

			if (MessageBox.Show("Bạn có muốn xóa không?", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
			{
				sql = "Delete NhaCungCap where MaNCC =N'" + ma + "'";
				ThucThiSQL.CapNhatDuLieu(sql);
				HienThi_Luoi();
			}
		}

		private void btnThem_Click(object sender, EventArgs e)
		{
			Forms.frmAddNhacungcap f = new Forms.frmAddNhacungcap();
			f.StartPosition = FormStartPosition.CenterScreen;
			f.Show();

		}

		private void btnHuy_Click(object sender, EventArgs e)
		{
			this.Close();
		}
	}
}
