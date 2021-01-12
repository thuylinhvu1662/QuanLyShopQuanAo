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
	public partial class frmMatHang : Form
	{
		public frmMatHang()
		{
			InitializeComponent();
		}
		private void HienThi_Luoi(string sql)
		{

			DataTable tblMH;
			tblMH = ThucThiSQL.DocBang(sql);
			dataGridView1.DataSource = tblMH;
			dataGridView1.Columns[0].HeaderText = "Mã Mặt Hàng";
			dataGridView1.Columns[1].HeaderText = "Loại Mặt Hàng";
			dataGridView1.Columns[2].HeaderText = "Tên Mặt Hàng";
			dataGridView1.Columns[3].HeaderText = "Đơn Giá";
			dataGridView1.Columns[4].HeaderText = "Tổng Nhập Về";

			dataGridView1.AllowUserToAddRows = false;
			dataGridView1.EditMode = DataGridViewEditMode.EditProgrammatically;
			tblMH.Dispose();

		}
		private void frmMatHang_Load(object sender, EventArgs e)
		{
			string sql = "select Hang.MaMH,LoaiMH,TenMH,DonGia,Sum(SLNhap) as SL from MatHang join Hang on MatHang.MaMH =Hang.MaMH Group by Hang.MaMH,LoaiMH,TenMH,DonGia";
			HienThi_Luoi(sql);
		}

		private void btnSua_Click(object sender, EventArgs e)
		{
			if (dataGridView1.Rows.Count == 0)
			{
				MessageBox.Show("Không có dữ liệu!", "Thông báo");
				return;
			}
			Forms.frmUpdateMathang f = new frmUpdateMathang();
			f.StartPosition = FormStartPosition.CenterScreen;
			f.txtMaMH.Text = dataGridView1.CurrentRow.Cells["MaMH"].Value.ToString();
			f.txtTenMH.Text = dataGridView1.CurrentRow.Cells["TenMH"].Value.ToString();
			f.txtLoaiMH.Text = dataGridView1.CurrentRow.Cells["LoaiMH"].Value.ToString();
			f.txtDongia.Text = dataGridView1.CurrentRow.Cells["DonGia"].Value.ToString();
			f.txtSL.Text = dataGridView1.CurrentRow.Cells["SL"].Value.ToString();
			f.txtDongia.Enabled = false;
			f.txtSL.Enabled = false;
			f.txtMaMH.Enabled = false;
			f.Show();
		}

		private void btnXoa_Click(object sender, EventArgs e)
		{
			//string sql;
			//if (dataGridView1.Rows.Count == 0)
			//{
			//	MessageBox.Show("Không có dữ liệu!", "Thông báo");
			//	return;
			//}
			//string ma = dataGridView1.CurrentRow.Cells["MaMH"].Value.ToString();

			//if (MessageBox.Show("Bạn có muốn xóa không?", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
			//{

			//	sql = "Delete MatHang where MaMH =N'" + ma + "'";
			//	ThucThiSQL.CapNhatDuLieu(sql);
			//	HienThi_Luoi();
			//}
		}

		private void btnHuy_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void btnChitiet_Click(object sender, EventArgs e)
		{
			if (dataGridView1.Rows.Count == 0)
			{
				MessageBox.Show("Không có dữ liệu!", "Thông báo");
				return;
			}
			Forms.frmHang f = new frmHang();
			f.StartPosition = FormStartPosition.CenterScreen;
			f.txtMaMH.Text = dataGridView1.CurrentRow.Cells["MaMH"].Value.ToString();
			f.txtSLTong.Text = dataGridView1.CurrentRow.Cells["SL"].Value.ToString();
			f.txtTenMH.Text = dataGridView1.CurrentRow.Cells["TenMH"].Value.ToString();
			f.txtMaMH.Enabled = false;
			f.txtSLTong.Enabled = false;
			f.txtTenMH.Enabled = false;
			f.Show();

		}

		private void frmMatHang_Activated(object sender, EventArgs e)
		{
			string sql = "select Hang.MaMH,LoaiMH,TenMH,DonGia,Sum(SLNhap) as SL from MatHang join Hang on MatHang.MaMH =Hang.MaMH Group by Hang.MaMH,LoaiMH,TenMH,DonGia";
			HienThi_Luoi(sql);
		}

		private void btnTimkiem_Click(object sender, EventArgs e)
		{
			if(txtTenMH.Text == "" && txtMaMH.Text == "")
			{
				MessageBox.Show("Bạn chưa nhập gì thông tin tìm kiếm");
				return;
			}
			if (txtTenMH.Text =="")
			{
				string sql = "select Hang.MaMH,LoaiMH,TenMH,DonGia,Sum(SLNhap) as SL from MatHang join Hang on MatHang.MaMH =Hang.MaMH where Hang.MaMH Like N'%"+txtMaMH.Text+"%' Group by Hang.MaMH,LoaiMH,TenMH,DonGia ";
				HienThi_Luoi(sql);
			}
			else if (txtMaMH.Text == "")
			{
				string sql = "select Hang.MaMH,LoaiMH,TenMH,DonGia,Sum(SLNhap) as SL from MatHang join Hang on MatHang.MaMH =Hang.MaMH where TenMH Like N'%" + txtTenMH.Text + "%' Group by Hang.MaMH,LoaiMH,TenMH,DonGia ";
				HienThi_Luoi(sql);
			}
			else
			{
				string sql = "select Hang.MaMH,LoaiMH,TenMH,DonGia,Sum(SLNhap) as SL from MatHang join Hang on MatHang.MaMH =Hang.MaMH where TenMH Like N'%" + txtTenMH.Text + "%' and Hang.MaMH Like N'%" + txtMaMH.Text + "%'   Group by Hang.MaMH,LoaiMH,TenMH,DonGia ";
				HienThi_Luoi(sql);
			}
			
		}

        private void Label7_Click(object sender, EventArgs e)
        {

        }
    }
}
