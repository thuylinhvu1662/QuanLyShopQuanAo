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
	public partial class frmUpdateHang : Form
	{
		public frmUpdateHang()
		{
			InitializeComponent();
		}

		private void btnCapnhat_Click(object sender, EventArgs e)
		{
			string sql;
			if (txtSize.Text.Trim().Length == 0)
			{
				MessageBox.Show("Bạn Chưa Điền Size Hàng", "Thông báo");
				txtSize.Focus();
				return;
			}
			if (txtMausac.Text.Trim().Length == 0)
			{
				MessageBox.Show("Bạn Chưa Điền Màu Sắc Hàng", "Thông báo");
				txtMausac.Focus();
				return;
			}
			if (txtSLNhap.Text.Trim().Length == 0)
			{
				MessageBox.Show("Bạn Chưa Điền Số Lượng Nhập Hàng", "Thông báo");
				txtSLNhap.Focus();
				return;
			}
			if (txtSLconlai.Text.Trim().Length == 0)
			{
				MessageBox.Show("Bạn Chưa Điền Số Lượng Hàng Còn Lại", "Thông báo");
				txtSLconlai.Focus();
				return;
			}
			if(Convert.ToDouble(txtSLconlai.Text) > Convert.ToDouble(txtSLNhap.Text)) // kiểm tra xem số lượng còn lại có vượt quá số lượng nhập vào hay không
			{
				MessageBox.Show("Số Lượng Còn Lại Vượt Quá Số Lượng Nhập", "Thông báo");
				txtSLconlai.Focus();
				txtSLconlai.Text = "";
				return;
			}

			double total = 0;
			DataTable sl = ThucThiSQL.DocBang("Select Sum(SLNhap) from Hang where MaMH=N'" + txtMaMH.Text + "' Group by MaMH");
			total = Convert.ToDouble(sl.Rows[0][0].ToString()) + Convert.ToDouble(txtSLNhap.Text) - Convert.ToDouble(ThucThiSQL.DocBang("select SLNhap from Hang where MaHang=N'"+txtMahang.Text+"'").Rows[0][0].ToString());// số lượng đã tồn tại cộng thêm với số lượng của hàng đang nhập để kiểm tra xem có vượt quá tổng cho phép không


			if (total > Convert.ToDouble(txtSLTong.Text)) // nếu vượt thì báo lỗi bắt nhập lại
			{
				MessageBox.Show("Đã vượt quá số lượng cho phép , mời nhập lại", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				txtSLNhap.Text = "";
				txtSLNhap.Focus();
				return;
			}
			// nếu qua hết thì update lại hàng
			sql = "UPDATE Hang Set Size =N'" + txtSize.Text + "',MauSac=N'" + txtMausac.Text + "',SLNhap=N'" + txtSLNhap.Text + "',SLConLai=N'" + txtSLconlai.Text + "' where MaHang = N'" + txtMahang.Text + "'";
			ThucThiSQL.CapNhatDuLieu(sql);
			MessageBox.Show("Bạn Sửa Thành Công", "Success");
			this.Close();
		}

        private void BtnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
