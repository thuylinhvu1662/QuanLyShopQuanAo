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
	public partial class frmNhanvien : Form
	{
		public frmNhanvien()
		{
			InitializeComponent();
		}
		private void HienThi_Luoi()
		{

			DataTable tblKH;
			string sql = "select * from NhanVien";
			tblKH = ThucThiSQL.DocBang(sql);
			dataGridView1.DataSource = tblKH;
			dataGridView1.Columns[0].HeaderText = "Mã Nhân Viên";
			dataGridView1.Columns[1].HeaderText = "Tên Nhân Viên";
			dataGridView1.Columns[2].HeaderText = "Ngày Sinh";
			dataGridView1.Columns[3].HeaderText = "Giới Tính";
			dataGridView1.Columns[4].HeaderText = "Địa Chỉ";
			dataGridView1.Columns[5].HeaderText = "Học Vấn";
			dataGridView1.Columns[6].HeaderText = "SĐT";
			dataGridView1.Columns[2].DefaultCellStyle.Format = "dd/MM/yyyy";
			dataGridView1.AllowUserToAddRows = false;
			dataGridView1.EditMode = DataGridViewEditMode.EditProgrammatically;
			tblKH.Dispose();

		}
		private void frmNhanvien_Load(object sender, EventArgs e)
		{
			HienThi_Luoi();
		}

		private void btnHuy_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void btnThem_Click(object sender, EventArgs e)
		{
			Forms.frmAddNhanvien f = new frmAddNhanvien();
			f.StartPosition = FormStartPosition.CenterScreen;
			f.Show();
		}

		private void frmNhanvien_Activated(object sender, EventArgs e)
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
			Forms.frmUpdateNhanvien f = new frmUpdateNhanvien();
			f.StartPosition = FormStartPosition.CenterScreen;
			f.txtMaNV.Text = dataGridView1.CurrentRow.Cells["MaNV"].Value.ToString();
			f.txtHoten.Text = dataGridView1.CurrentRow.Cells["TenNV"].Value.ToString();
			f.txtNgaysinh.Text = dataGridView1.CurrentRow.Cells["NgaySinh"].Value.ToString();
			f.txtDiachi.Text = dataGridView1.CurrentRow.Cells["DiaChi"].Value.ToString();
			f.txtHocvan.Text = dataGridView1.CurrentRow.Cells["HocVan"].Value.ToString();
			f.txtSdt.Text = dataGridView1.CurrentRow.Cells["SDT"].Value.ToString();
			f.txtGioitinh.Text = dataGridView1.CurrentRow.Cells["GioiTinh"].Value.ToString();
			f.txtMaNV.Enabled = false;
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
			string ma = dataGridView1.CurrentRow.Cells["MaNV"].Value.ToString();

			if (MessageBox.Show("Bạn có muốn xóa không?", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
			{
				sql = "Delete NhanVien where MaNV =N'" + ma + "'";
				ThucThiSQL.CapNhatDuLieu(sql);
				HienThi_Luoi();
			}
		}
	}
}
