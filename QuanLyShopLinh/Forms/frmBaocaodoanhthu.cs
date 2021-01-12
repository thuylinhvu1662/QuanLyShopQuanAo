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
	public partial class frmBaocaodoanhthu : Form
	{
		public frmBaocaodoanhthu()
		{
			InitializeComponent();
		}
		private void HienThi_Luoi(string sql)
		{

			DataTable tblKH;
			tblKH = ThucThiSQL.DocBang(sql);
			dataGridView1.DataSource = tblKH;
			dataGridView1.Columns[0].HeaderText = "Ngày";
			dataGridView1.Columns[1].HeaderText = "Mã Hóa Đơn";
			dataGridView1.Columns[2].HeaderText = "Mã Hàng";
			dataGridView1.Columns[3].HeaderText = "Đơn Giá";
			dataGridView1.Columns[4].HeaderText = "Số Lượng";
			dataGridView1.Columns[5].HeaderText = "Thành Tiền";

			dataGridView1.AllowUserToAddRows = false;
			dataGridView1.EditMode = DataGridViewEditMode.EditProgrammatically;
			tblKH.Dispose();

		}


		private void frmBaocaodoanhthu_Load(object sender, EventArgs e)
		{
			string sql = "Select NgayLap,ChiTietHD.MaHD,ChiTietHD.MaHang,DonGia,ChiTietHD.SL,(ChiTietHD.SL * DonGia) FROM ((HoaDon join ChiTietHD on ChiTietHD.MaHD=HoaDon.MaHD) join Hang on Hang.MaHang =ChiTietHD.MaHang) join MatHang on MatHang.MaMH=Hang.MaMH where NgayLap = '"+dtpTungay.Text+"' order by NgayLap";
			HienThi_Luoi(sql);
			double total = 0; // biến tổng để tính doanh thu trong số ngày nhập
			for(int i = 0;i< dataGridView1.Rows.Count; i++)
			{
				total += Convert.ToDouble(dataGridView1.Rows[i].Cells[5].Value.ToString());
			}
			//String.Format("{ 0:0,0 vnđ}", 2000000);
			txtDoanhthu.Text = total.ToString();
		}

		private void dtpTungay_ValueChanged(object sender, EventArgs e)
		{
		    if (Convert.ToDateTime(dtpTungay.Text) > Convert.ToDateTime(dtpDenngay.Text))// nếu ngày đến bé hơn ngày từ
			{
				MessageBox.Show("Bạn k thể nhập ngày từ lớn hơn ngày đến");
				dtpTungay.Focus();
				return;
			}
			else
			{
				string sql = "Select NgayLap,ChiTietHD.MaHD,ChiTietHD.MaHang,DonGia,ChiTietHD.SL,(ChiTietHD.SL * DonGia) FROM ((HoaDon join ChiTietHD on ChiTietHD.MaHD=HoaDon.MaHD) join Hang on Hang.MaHang =ChiTietHD.MaHang) join MatHang on MatHang.MaMH=Hang.MaMH where NgayLap >= '" + dtpTungay.Text + "' and NgayLap <= '" + dtpDenngay.Text + "'  order by NgayLap";
				HienThi_Luoi(sql);
				double total = 0; // biến tổng để tính doanh thu trong số ngày nhập
				for (int i = 0; i < dataGridView1.Rows.Count; i++)
				{
					total += Convert.ToDouble(dataGridView1.Rows[i].Cells[5].Value.ToString());
				}
				txtDoanhthu.Text = total.ToString();
			}
		}

		private void dtpDenngay_ValueChanged(object sender, EventArgs e)
		{
			if (Convert.ToDateTime(dtpTungay.Text) > Convert.ToDateTime(dtpDenngay.Text))// nếu ngày đến bé hơn ngày từ
			{
				MessageBox.Show("Bạn k thể nhập ngày đến nhỏ hơn ngày từ");
				dtpDenngay.Focus();
				return;
			}
			else
			{
				string sql = "Select NgayLap,ChiTietHD.MaHD,ChiTietHD.MaHang,DonGia,ChiTietHD.SL,(ChiTietHD.SL * DonGia) FROM ((HoaDon join ChiTietHD on ChiTietHD.MaHD=HoaDon.MaHD) join Hang on Hang.MaHang =ChiTietHD.MaHang) join MatHang on MatHang.MaMH=Hang.MaMH where NgayLap >= '" + dtpTungay.Text + "' and NgayLap <= '" + dtpDenngay.Text + "'  order by NgayLap";
				HienThi_Luoi(sql);
				double total = 0; // biến tổng để tính doanh thu trong số ngày nhập
				for (int i = 0; i < dataGridView1.Rows.Count; i++)
				{
					total += Convert.ToDouble(dataGridView1.Rows[i].Cells[5].Value.ToString());
				}
				txtDoanhthu.Text = total.ToString();
			}
		}
	}
}
