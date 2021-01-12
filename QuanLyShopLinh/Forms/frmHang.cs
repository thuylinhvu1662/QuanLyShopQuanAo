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
	public partial class frmHang : Form
	{
		public frmHang()
		{
			InitializeComponent();
		}
		private void HienThi_Luoi(string sql)
		{

			DataTable tblMH;
			
			tblMH = ThucThiSQL.DocBang(sql);
			dataGridView1.DataSource = tblMH;
			dataGridView1.Columns[0].HeaderText = "Mã Hàng";
			dataGridView1.Columns[1].HeaderText = "Size";
			dataGridView1.Columns[2].HeaderText = "Màu Sắc";
			dataGridView1.Columns[3].Visible = false;
			dataGridView1.Columns[4].HeaderText = "Số Lượng Nhập";
			dataGridView1.Columns[5].HeaderText = "Số Lượng Còn Lại";

			dataGridView1.AllowUserToAddRows = false;
			dataGridView1.EditMode = DataGridViewEditMode.EditProgrammatically;
			tblMH.Dispose();

		}
		private void frmHang_Load(object sender, EventArgs e)
		{
			string sql = "select * from Hang where MaMH=N'" + txtMaMH.Text + "'";
			HienThi_Luoi(sql);
		}

		private void btnSua_Click(object sender, EventArgs e)
		{
			if (dataGridView1.Rows.Count == 0)
			{
				MessageBox.Show("Không có dữ liệu!", "Thông báo");
				return;
			}
			Forms.frmUpdateHang f = new frmUpdateHang();
			f.StartPosition = FormStartPosition.CenterScreen;
			f.txtMahang.Text = dataGridView1.CurrentRow.Cells["MaHang"].Value.ToString();
			f.txtSize.Text = dataGridView1.CurrentRow.Cells["Size"].Value.ToString();
			f.txtMausac.Text = dataGridView1.CurrentRow.Cells["MauSac"].Value.ToString();
			f.txtSLNhap.Text = dataGridView1.CurrentRow.Cells["SLNhap"].Value.ToString();
			f.txtSLconlai.Text = dataGridView1.CurrentRow.Cells["SLConLai"].Value.ToString();
			f.txtMaMH.Text = txtMaMH.Text;
			f.txtSLTong.Text = txtSLTong.Text;
			f.txtMahang.Enabled = false;
			f.Show();
		}

		private void frmHang_Activated(object sender, EventArgs e)
		{
			string sql = "select * from Hang where MaMH=N'" + txtMaMH.Text + "'";
			HienThi_Luoi(sql);
		}

		private void btnHuy_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void btnTimkiem_Click(object sender, EventArgs e)
		{
			if (txtSize.Text == "" && txtMausac.Text == "")
			{
				MessageBox.Show("Bạn chưa nhập gì thông tin tìm kiếm");
				return;
			}
			if (txtMausac.Text == "")
			{
				string sql = "select * from Hang where Size Like N'%" + txtSize.Text + "%' and MaMH=N'" + txtMaMH.Text + "' ";
				HienThi_Luoi(sql);
			}
			else if (txtSize.Text == "")
			{
				string sql = "select * from Hang where MauSac Like N'%" + txtMausac.Text + "%' and MaMH=N'" + txtMaMH.Text + "' ";
				HienThi_Luoi(sql);
			}
			else
			{
				string sql = "select select * from Hang where Size Like N'%" + txtSize.Text + "%' and MauSac Like N'%" + txtMausac.Text + "%' and MaMH=N'" + txtMaMH.Text + "'";
			}
		}

        private void S_Click(object sender, EventArgs e)
        {

        }

        private void TxtSize_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
