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
	public partial class frmHoadon : Form
	{
		public frmHoadon()
		{
			InitializeComponent();
		}

		private void frmHoadon_Load(object sender, EventArgs e)// khi load lên thì chưa cho nhập gì cả
		{
			btnThem.Enabled = true;
			btnLuu.Enabled = false;
			btnIn.Enabled = false;
			btnHuy.Enabled = false;
			btnDong.Enabled = true;
			txtTennv.ReadOnly = true;
			txtThanhtien.ReadOnly = true;
			txtTongtien.Text = "0";
			txtTongtien.ReadOnly = true;
			txtSize.ReadOnly = true;
			txtMausac.ReadOnly = true;
			txtTenMH.ReadOnly = true;
			txtDongia.ReadOnly = true;
			grbThongtinHD.Enabled = false;
			grbChitiet.Enabled = false;
		}

		private void cbxMaNV_DropDown(object sender, EventArgs e)// để xổ ra danh sách các mã nhân viên
		{
			cbxMaNV.DataSource = ThucThiSQL.DocBang("Select MaNV from NhanVien");
			cbxMaNV.ValueMember = "MaNV";
			cbxMaNV.SelectedIndex = -1;
		}

		private void cbxMaNV_TextChanged(object sender, EventArgs e)// khi thay đổi mã nhân viên thì tên nv thay đổi theo
		{
			string sql;
			if (cbxMaNV.Text == "")
			{
				txtTennv.Text = "";
				return;
			}
			sql = "Select TenNV from NhanVien Where MaNV ='" + cbxMaNV.Text + "'";
			DataTable table = ThucThiSQL.DocBang(sql);
			if (table.Rows.Count > 0)
			{
				txtTennv.Text = table.Rows[0][0].ToString();
			}
		}
		private void ResetValue()// hàm để reset tất cả giá trị về rỗng
		{
			txtMaHD.Text = "";
			string date = DateTime.Now.ToString("dd/MM/yyyy"); // gán sẵn ngày nhập mặc định là hôm nay
			txtNgaylap.Text = date;
			cbxMaNV.Text = "";
			txtTennv.Text = "";
			txtTenkhach.Text = "";
			txtSdtkhach.Text = "";
			txtTongtien.Text = "0";
			cbxMahang.Text = "";
			txtTenMH.Text = "";
			txtSize.Text = "";
			txtSL.Text = "";
			txtMausac.Text = "";
			txtDongia.Text = "";
			txtThanhtien.Text = "";
		}
		private void ResetValueHang() // hàm để reset giá trị các ô về rỗng 
		{
			cbxMahang.Text = "";
			txtTenMH.Text = "";
			txtSize.Text = "";
			txtMausac.Text = "";
			txtDongia.Text = "";
			txtSL.Text = "";
		}

		private void btnThem_Click(object sender, EventArgs e)
		{
			btnHuy.Enabled = true;
			btnLuu.Enabled = true;
			btnIn.Enabled = true;
			grbThongtinHD.Enabled = true;
			grbChitiet.Enabled = false;
			dataGridView1.DataSource = null;
			ResetValue(); // gọi hàm để reset giá trị trong phiếu về rỗng

			string table = "HoaDon";
			string key = "HD";
			txtMaHD.Text = ThucThiSQL.CreateKey(table, key);
			txtMaHD.ReadOnly = true;
			txtNgaylap.ReadOnly = true;
		}

		private void btnDong_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void txtSL_TextChanged(object sender, EventArgs e)
		{
			double tt, sl, dg;
			if (txtDongia.Text == "")
			{
				dg = 0;
			}
			else
				dg = Convert.ToDouble(txtDongia.Text);
			if (txtSL.Text == "")
			{
				sl = 0;
			}
			else
				sl = Convert.ToDouble(txtSL.Text);
			tt = sl * dg;
			txtThanhtien.Text = tt.ToString();
		}

		private void btnLuu_Click(object sender, EventArgs e)
		{
			string sql; // đầu tiên phải kiểm tra xem các trường có rỗng hay không
			if (txtMaHD.Text.Trim().Length == 0)
			{
				MessageBox.Show("Bạn phải nhập mã hóa đơn", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				txtMaHD.Focus();
				return;
			}
			if (txtNgaylap.Text.Trim().Length == 0)
			{
				MessageBox.Show("Bạn phải nhập ngày lập", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				txtNgaylap.Focus();
				return;
			}
			if (txtTenkhach.Text.Trim().Length == 0)
			{
				MessageBox.Show("Bạn phải nhập tên khách", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				txtTenkhach.Focus();
				return;
			}
			if (txtSdtkhach.Text.Trim().Length == 0)
			{
				MessageBox.Show("Bạn phải nhập SĐT khách", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				txtSdtkhach.Focus();
				return;
			}
			if (cbxMaNV.Text.Trim().Length == 0)
			{
				MessageBox.Show("Bạn phải nhập mã nhân viên", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				cbxMaNV.Focus();
				return;
			}

			sql = "select MaHD from HoaDon where MaHD='" + txtMaHD.Text + "'";
			if (ThucThiSQL.DocBang(sql).Rows.Count > 0) // sau đó check xem mã phiếu có bị trùng hay không
			{
				MessageBox.Show("Mã hóa đơn bị trùng, mời nhập mã khác", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				txtMaHD.Text = "";
				txtMaHD.Focus();
				return;
			}

			sql = "Insert into HoaDon Values(N'" + txtMaHD.Text + "',N'" + txtSdtkhach.Text + "',N'" + txtTenkhach.Text + "'," + txtNgaylap.Text + ",N'" + cbxMaNV.Text + "','" + 0 + "')";
			ThucThiSQL.CapNhatDuLieu(sql); // cập nhật dữ liệu vào phiếu nhập trước sau đó bật thêm chi tiết phiếu nhập
			MessageBox.Show("Lập Hóa Đơn Thành Công, Mời Nhập Chi Tiết");
			btnLuu.Enabled = false;
			grbChitiet.Enabled = true;
		}

		private void txtSL_KeyPress(object sender, KeyPressEventArgs e)// k cho số lượng nhập chữ
		{
			if ((e.KeyChar < '0' || e.KeyChar > '9') && (e.KeyChar != 8) && (e.KeyChar != 46))
			{
				e.Handled = true;
			}
		}

		private void cbxMahang_DropDown(object sender, EventArgs e)
		{
			cbxMahang.DataSource = ThucThiSQL.DocBang("Select MaHang from Hang");
			cbxMahang.ValueMember = "MaHang";
			cbxMahang.SelectedIndex = -1;
		}

		private void cbxMahang_TextChanged(object sender, EventArgs e)
		{
			string sql;
			if (cbxMahang.Text == "")
			{
				txtTenMH.Text = "";
				txtSize.Text = "";
				txtMausac.Text = "";
				txtDongia.Text = "";
				return;
			}
			sql = "Select TenMH,Size,MauSac,DonGia from MatHang join Hang on MatHang.MaMH = Hang.MaMH Where MaHang ='" + cbxMahang.Text + "'";
			DataTable table = ThucThiSQL.DocBang(sql);
			if (table.Rows.Count > 0)
			{
				txtTenMH.Text = table.Rows[0][0].ToString();
				txtSize.Text = table.Rows[0][1].ToString();
				txtMausac.Text = table.Rows[0][2].ToString();
				txtDongia.Text = table.Rows[0][3].ToString();
			}
		}
		private void HienThi_Luoi()
		{
			DataTable tblMH;
			string sql = "select ChiTietHD.MaHang,TenMH,Size,MauSac,DonGia,ChiTietHD.SL,(ChiTietHD.SL*DonGia) From (Hang join ChiTietHD on Hang.MaHang = ChiTietHD.MaHang) join MatHang on Hang.MaMH=MatHang.MaMH  where MaHD = '"+txtMaHD.Text+"'"; ;
			tblMH = ThucThiSQL.DocBang(sql);
			dataGridView1.DataSource = tblMH;
			dataGridView1.Columns[0].HeaderText = "Mã Hàng";
			dataGridView1.Columns[1].HeaderText = "Tên Mặt Hàng";
			dataGridView1.Columns[2].HeaderText = "Size";
			dataGridView1.Columns[3].HeaderText = "Màu Sắc";
			dataGridView1.Columns[4].HeaderText = "Đơn Giá";
			dataGridView1.Columns[5].HeaderText = "Số Lượng";
			dataGridView1.Columns[6].HeaderText = "Thành Tiền";


			dataGridView1.AllowUserToAddRows = false;
			dataGridView1.EditMode = DataGridViewEditMode.EditProgrammatically;
			tblMH.Dispose();
		}
		private void btnAdd_Click(object sender, EventArgs e)
		{
			string sql;
			if (cbxMahang.Text.Trim().Length == 0) // trước hết kiểm tra xem các trường có rỗng hay không
			{
				MessageBox.Show("Bạn phải nhập mã mặt hàng", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				cbxMahang.Focus();
				return;
			}
			if (txtSL.Text.Trim().Length == 0)
			{
				MessageBox.Show("Bạn phải nhập số lượng mặt hàng", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				txtSL.Focus();
				return;
			}



			double slconlai = Convert.ToDouble(ThucThiSQL.DocBang("Select SLConLai from Hang where MaHang=N'"+cbxMahang.Text+"'").Rows[0][0].ToString());// lấy ra số lượng đang còn lại của sản phẩm trước khi bán
			double slsaukhiban = slconlai - Convert.ToDouble(txtSL.Text);// số lượng sau khi bán bằng số lượng trước khi bán trừ đi số lượng mua
			if (slsaukhiban < 0) // kiểm tra xem số lượng bán có vượt quá số lượng còn không , nếu có báo lỗi , không có thì thực thi tiếp
			{
				MessageBox.Show("Số lượng hàng chỉ còn '" + slconlai+"'", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				txtSL.Text = "";
				txtSL.Focus();
				return;
			}
			else
			{
				sql = "insert into ChiTietHD values(N'" + txtMaHD.Text + "',N'" + cbxMahang.Text + "',N'" + txtSL.Text + "')";
				ThucThiSQL.CapNhatDuLieu(sql); // Thêm thông tin vào chi tiết Hóa đơn
				sql = "Update Hang Set SLConLai ='" + slsaukhiban + "' where MaHang=N'" + cbxMahang.Text + "'";
				ThucThiSQL.CapNhatDuLieu(sql); // cập nhật lại cột số lượng còn lại của hàng
			}

			double tong = Convert.ToDouble(txtSL.Text) * Convert.ToDouble(txtDongia.Text); // gán tổng tiền bằng đơn giá nhân số lượng
			double tongmoi = tong + Convert.ToDouble(txtTongtien.Text); // tổng mới sẽ bằng tổng cũ và cộng với đơn giá và số lượng của mặt hàng đang nhập
			txtTongtien.Text = tongmoi.ToString(); //set textbox tổng tiền bằng tổng vừa tính
			sql = "Update HoaDon Set TongTien =N'" + txtTongtien.Text + "' where MaHD='" + txtMaHD.Text + "'";
			ThucThiSQL.CapNhatDuLieu(sql);// cập nhật tổng tiền của Hóa Đơn

			HienThi_Luoi(); // hiển thị danh sách mặt hàng của phiếu nhập lên
			ResetValueHang(); // sau khi thêm 1 mặt hàng thì reset giá trị các ô textbox về rỗng

			MessageBox.Show("CapNhatThanhCong");
		}

		private void btnHuy_Click(object sender, EventArgs e)
		{
			if (MessageBox.Show("Bạn có chắc chắn xóa không?", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
			{

				ThucThiSQL.CapNhatDuLieu("Delete ChiTietHD where MaHD='" + txtMaHD.Text + "'"); // đầu tiên xóa chi tiết hóa đơn theo mã phiếu hóa đơn
				for (int i = 0; i < dataGridView1.Rows.Count; i++) // vòng lặp xóa cập nhật lại số lượng ban đầu trước khi bán của từng mã hàng
				{
					double slconlai = Convert.ToDouble(ThucThiSQL.DocBang("Select SLConLai from Hang where MaHang=N'" + dataGridView1.Rows[i].Cells[0].Value.ToString() + "'").Rows[0][0].ToString());
					double slbandau = slconlai + Convert.ToDouble(dataGridView1.Rows[i].Cells[5].Value.ToString());
					ThucThiSQL.CapNhatDuLieu("Update Hang Set SLConLai =N'" + slbandau + "' Where MaHang=N'" + dataGridView1.Rows[i].Cells[0].Value.ToString() + "'");// xóa chi tiết hàng của mặt hàng

				}

				ThucThiSQL.CapNhatDuLieu("Delete HoaDon where MaHD='" + txtMaHD.Text + "'");//Tiếp đó là xóa trong bảng phiếu hóa đơn, done
				ResetValue();// r reset các ô trong phiếu nhập về rỗng
				dataGridView1.DataSource = null; // reset datagridview về rỗng
				MessageBox.Show("Hủy thành công");
				btnLuu.Enabled = false;
				btnHuy.Enabled = false;
				btnIn.Enabled = false;
				grbThongtinHD.Enabled = false;
				grbChitiet.Enabled = false;
			}
		}

		private void txtSdtkhach_KeyPress(object sender, KeyPressEventArgs e)
		{
			if ((e.KeyChar < '0' || e.KeyChar > '9') && (e.KeyChar != 8) && (e.KeyChar != 46))
			{
				e.Handled = true;
			}
		}

		private void dataGridView1_DoubleClick(object sender, EventArgs e)
		{
			if (dataGridView1.Rows.Count == 0)
			{
				MessageBox.Show("Không có dữ liệu!", "Thông báo");
				return;
			}
			if (MessageBox.Show("Bạn có chắc chắn xóa không?", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
			{

				ThucThiSQL.CapNhatDuLieu("Delete ChiTietHD where MaHang='" + dataGridView1.CurrentRow.Cells["MaHang"].Value.ToString() + "'"); // đầu tiên xóa chi tiết hóa đơn theo mã hàng
					
				
					double slconlai = Convert.ToDouble(ThucThiSQL.DocBang("Select SLConLai from Hang where MaHang=N'" + dataGridView1.CurrentRow.Cells["MaHang"].Value.ToString() + "'").Rows[0][0].ToString());
					double slbandau = slconlai + Convert.ToDouble(dataGridView1.CurrentRow.Cells["SL"].Value.ToString());
					ThucThiSQL.CapNhatDuLieu("Update Hang Set SLConLai =N'" + slbandau + "' Where MaHang=N'" + dataGridView1.CurrentRow.Cells["MaHang"].Value.ToString() + "'"); // cập nhật lại số lượng trc khi bán

	

				MessageBox.Show("Hủy thành công");
				HienThi_Luoi();
			}
		}
	}
}
