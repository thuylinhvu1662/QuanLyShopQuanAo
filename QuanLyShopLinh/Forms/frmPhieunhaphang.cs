using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace QuanLyShopLinh.Forms
{
	public partial class frmPhieunhaphang : Form
	{
		public frmPhieunhaphang()
		{
			InitializeComponent();
		}


		private void frmPhieunhaphang_Load(object sender, EventArgs e)// khi load lên thì chưa cho nhập gì cả
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
			txtTenNCC.ReadOnly = true;
			txtDiachiNCC.ReadOnly = true;
			txtSdtNCC.ReadOnly = true;
			grbThongtinphieu.Enabled = false;
			grbChitiet.Enabled = false;
			
		}

		private void cbxMaNV_DropDown(object sender, EventArgs e) // để xổ ra danh sách các mã nhân viên
		{
			cbxMaNV.DataSource = ThucThiSQL.DocBang("Select MaNV from NhanVien");
			cbxMaNV.ValueMember = "MaNV";
			cbxMaNV.SelectedIndex = -1;
		}

		private void cbxMaNV_TextChanged(object sender, EventArgs e)// khi thay đổi mã nhân viên thì tên nv thay đổi theo
		{
			string sql;
			if(cbxMaNV.Text == "")
			{
				txtTennv.Text = "";
				return;
			}
			sql = "Select TenNV from NhanVien Where MaNV ='"+cbxMaNV.Text+"'";
			DataTable table = ThucThiSQL.DocBang(sql);
			if(table.Rows.Count > 0)
			{
				txtTennv.Text = table.Rows[0][0].ToString();
			}
		}

		private void ResetValue()// hàm để reset tất cả giá trị về rỗng
		{
			txtMaphieu.Text = "";
			string date = DateTime.Now.ToString("dd/MM/yyyy"); // gán sẵn ngày nhập mặc định là hôm nay
			txtNgaynhap.Text = date;
			cbxMaNV.Text = "";
			txtTennv.Text = "";
			txtTongtien.Text = "0";
			txtMaMH.Text = "";
			txtTenMH.Text = "";		
			cbxMaNCC.Text = "";
			txtSL.Text = "";
			txtThanhtien.Text = "";
		}	
		private void btnThem_Click(object sender, EventArgs e)
		{
			btnHuy.Enabled = true;
			btnLuu.Enabled = true;
			btnIn.Enabled = true;
			grbThongtinphieu.Enabled = true;
			grbChitiet.Enabled = false;
			dataGridView1.DataSource = null;
			ResetValue(); // gọi hàm để reset giá trị trong phiếu về rỗng

			string table = "PhieuNhapHang";
			string key = "PN";
			txtMaphieu.Text = ThucThiSQL.CreateKey(table,key);
			txtMaphieu.ReadOnly = true;
			txtNgaynhap.ReadOnly = true;
		}

		private void txtDongia_TextChanged(object sender, EventArgs e) // hàm để tính thành tiền khi đơn giá thay đổi
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

		private void txtSL_TextChanged(object sender, EventArgs e)// hàm tính thành tiền khi số lượng thay đổi
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
		private void HienThi_Luoi()
		{
			DataTable tblMH;
			string sql = "select ChiTietPN.MaMH,TenMH,LoaiMH,DonGia,SL,(DonGia*SL) as Total from MatHang join ChiTietPN on MatHang.MaMH = ChiTietPN.MaMH where MaPN='"+txtMaphieu.Text+"'";;
			tblMH = ThucThiSQL.DocBang(sql);
			dataGridView1.DataSource = tblMH;
			dataGridView1.Columns[0].HeaderText = "Mã Mặt Hàng";
			dataGridView1.Columns[1].HeaderText = "Tên Mặt Hàng";
			dataGridView1.Columns[2].HeaderText = "Loại Mặt Hàng";
			dataGridView1.Columns[3].HeaderText = "Đơn Giá";
			dataGridView1.Columns[4].HeaderText = "Số lượng";
			dataGridView1.Columns[5].HeaderText = "Thành Tiền";
	

			dataGridView1.AllowUserToAddRows = false;
			dataGridView1.EditMode = DataGridViewEditMode.EditProgrammatically;
			tblMH.Dispose();
		}
		private void ResetValueMatHang() // hàm để reset giá trị các ô về rỗng 
		{
			txtMaMH.Text = "";
			txtTenMH.Text = "";
			txtLoaiMH.Text = "";
			txtDongia.Text = "";
			txtSL.Text = "";
		}

		private void btnAdd_Click(object sender, EventArgs e)
		{
			if (txtMaMH.Text.Trim().Length == 0) // trước hết kiểm tra xem các trường có rỗng hay không
			{
				MessageBox.Show("Bạn phải nhập mã mặt hàng", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				txtMaMH.Focus();
				return;
			}
			if (txtTenMH.Text.Trim().Length == 0)
			{
				MessageBox.Show("Bạn phải nhập tên mặt hàng", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				txtTenMH.Focus();
				return;
			}
			if (txtLoaiMH.Text.Trim().Length == 0)
			{
				MessageBox.Show("Bạn phải nhập loại mặt hàng", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				txtLoaiMH.Focus();
				return;
			}
			if (txtDongia.Text.Trim().Length == 0)
			{
				MessageBox.Show("Bạn phải nhập đơn giá mặt hàng", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				txtDongia.Focus();
				return;
			}
			if (txtSL.Text.Trim().Length == 0)
			{
				MessageBox.Show("Bạn phải nhập số lượng mặt hàng", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				txtSL.Focus();
				return;
			}


			string sql = "select MaMH from MatHang where MaMH='" + txtMaMH.Text + "'";
			if (ThucThiSQL.DocBang(sql).Rows.Count > 0) // kiểm tra xem mã mặt hàng đã tồn tại trong bảng mặt hàng chưa
			{
				MessageBox.Show("Mã mặt hàng đã tồn tại , mời nhập lại", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				txtMaMH.Focus();
				txtMaMH.Text = "";
				return;
			}

			sql = "insert into MatHang values(N'"+txtMaMH.Text+ "',N'" + txtLoaiMH.Text + "',N'" + txtTenMH.Text + "',N'" + txtDongia.Text + "')";
			ThucThiSQL.CapNhatDuLieu(sql); // thêm dữ liệu vào mặt hàng trước

			sql = "insert into ChiTietPN values(N'" + txtMaphieu.Text + "',N'" + txtMaMH.Text + "',N'" + txtSL.Text + "')";
			ThucThiSQL.CapNhatDuLieu(sql); // tiếp đến là thêm thông tin vào chi tiết phiếu nhập

			double tong = Convert.ToDouble(txtSL.Text) * Convert.ToDouble(txtDongia.Text); // gán tổng tiền bằng đơn giá nhân số lượng
			double tongmoi = tong + Convert.ToDouble(txtTongtien.Text); // tổng mới sẽ bằng tổng cũ và cộng với đơn giá và số lượng của mặt hàng đang nhập
			txtTongtien.Text= tongmoi.ToString(); //set textbox tổng tiền bằng tổng vừa tính
			sql = "Update PhieuNhapHang Set TongTien =N'" + txtTongtien.Text + "' where MaPN='"+txtMaphieu.Text+"'";
			ThucThiSQL.CapNhatDuLieu(sql);// cập nhật tổng tiền của phiếu mua hàng

			HienThi_Luoi(); // hiển thị danh sách mặt hàng của phiếu nhập lên
			ResetValueMatHang(); // sau khi thêm 1 mặt hàng thì reset giá trị các ô textbox về rỗng

			MessageBox.Show("CapNhatThanhCong");
		}

		private void btnLuu_Click(object sender, EventArgs e)
		{
			string sql; // đầu tiên phải kiểm tra xem các trường có rỗng hay không
			if(txtMaphieu.Text.Trim().Length == 0)
			{
				MessageBox.Show("Bạn phải nhập mã phiếu", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				txtMaphieu.Focus();
				return;
			}
			if (txtNgaynhap.Text.Trim().Length == 0)
			{
				MessageBox.Show("Bạn phải nhập ngày lập", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				txtNgaynhap.Focus();
				return;
			}
			if (cbxMaNCC.Text.Trim().Length == 0)
			{
				MessageBox.Show("Bạn phải nhập nhà cung cấp", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				cbxMaNCC.Focus();
				return;
			}
			if (cbxMaNV.Text.Trim().Length == 0)
			{
				MessageBox.Show("Bạn phải nhập mã nhân viên", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				cbxMaNV.Focus();
				return;
			}

			sql ="select MaPN from PhieuNhapHang where MaPN='"+txtMaphieu.Text+"'";
			if (ThucThiSQL.DocBang(sql).Rows.Count > 0) // sau đó check xem mã phiếu có bị trùng hay không
			{
				MessageBox.Show("Mã Phiếu bị trùng, mời nhập mã khác", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				txtMaphieu.Text = "";
				txtMaphieu.Focus();
				return;
			}

			sql = "Insert into PhieuNhapHang Values(N'"+txtMaphieu.Text+ "'," + txtNgaynhap.Text + ",N'" + cbxMaNV.Text + "','"+0+ "',N'" + cbxMaNCC.Text + "')";
			ThucThiSQL.CapNhatDuLieu(sql); // cập nhật dữ liệu vào phiếu nhập trước sau đó bật thêm chi tiết phiếu nhập
			MessageBox.Show("Lập Phiếu Nhập Thành Công, Mời Nhập Chi Tiết");
			btnLuu.Enabled = false;
			grbChitiet.Enabled = true;
		}

		private void btnDong_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void btnHuy_Click(object sender, EventArgs e)
		{

			if (MessageBox.Show("Bạn có chắc chắn xóa không?", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
			{

				ThucThiSQL.CapNhatDuLieu("Delete ChiTietPN where MaPN='" + txtMaphieu.Text + "'"); // đầu tiên xóa chi tiết phiếu nhập theo mã phiếu nhập
				for (int i = 0; i < dataGridView1.Rows.Count ; i++) // vòng lặp xóa các mặt hàng trong bảng mặt hàng và chi tiết hàng của mặt hàng đó
				{
					ThucThiSQL.CapNhatDuLieu("Delete Hang Where MaMH=N'" + dataGridView1.Rows[i].Cells[0].Value.ToString() + "'");// xóa chi tiết hàng của mặt hàng
					ThucThiSQL.CapNhatDuLieu("Delete MatHang where MaMH=N'" + dataGridView1.Rows[i].Cells[0].Value.ToString() + "'");// r xóa mặt hàng đó luôn
				}
				ThucThiSQL.CapNhatDuLieu("Delete PhieuNhapHang where MaPN='" + txtMaphieu.Text + "'");//Tiếp đó là xóa trong bảng phiếu nhập hàng, done
				ResetValue();// r reset các ô trong phiếu nhập về rỗng
				dataGridView1.DataSource = null; // reset datagridview về rỗng
				MessageBox.Show("Hủy thành công");
				btnLuu.Enabled = false;
				btnHuy.Enabled = false;
				btnIn.Enabled = false;
				grbThongtinphieu.Enabled = false;
				grbChitiet.Enabled = false;
			}
		}

		private void btnChitiethang_Click(object sender, EventArgs e)
		{
			if (dataGridView1.Rows.Count == 0)
			{
				MessageBox.Show("Không có dữ liệu!", "Thông báo");
				return;
			}
			else if (dataGridView1.CurrentRow.Cells[0].Value is null)
			{
				MessageBox.Show("Trỏ đúng đến mặt hàng cần nhập!", "Thông báo");
				return;
			}
			Forms.frmAddChiTietHang f = new Forms.frmAddChiTietHang();
			f.StartPosition = FormStartPosition.CenterScreen;
			f.txtMaMH.Text = dataGridView1.CurrentRow.Cells["MaMH"].Value.ToString();
			f.txtTenMH.Text = dataGridView1.CurrentRow.Cells["TenMH"].Value.ToString();
			f.txtSLtong.Text = dataGridView1.CurrentRow.Cells["SL"].Value.ToString();
			f.txtDongia.Text = dataGridView1.CurrentRow.Cells["DonGia"].Value.ToString();
			f.txtMaMH.Enabled = false;
			f.txtTenMH.Enabled = false;
			f.txtSLtong.Enabled = false;
			f.txtDongia.Enabled = false;
			f.Show();
		}

		private void txtDongia_KeyPress(object sender, KeyPressEventArgs e)
		{
				if ((e.KeyChar < '0' || e.KeyChar > '9') && (e.KeyChar != 8) && (e.KeyChar != 46))
				{
					e.Handled = true;
				}
			
		}

		private void txtSL_KeyPress(object sender, KeyPressEventArgs e)
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
				string sql;
				ThucThiSQL.CapNhatDuLieu("Delete ChiTietPN where MaMH='" + dataGridView1.CurrentRow.Cells["MaMH"].Value.ToString() + "'"); // đầu tiên xóa chi tiết phiếu nhập theo mã mặt hàng
				ThucThiSQL.CapNhatDuLieu("Delete MatHang where MaMH=N'" + dataGridView1.CurrentRow.Cells["MaMH"].Value.ToString() + "'");// r xóa mặt hàng đó luôn
				ThucThiSQL.CapNhatDuLieu("Delete Hang where MaMH=N'" + dataGridView1.CurrentRow.Cells["MaMH"].Value.ToString() + "'");// r xóa các hàng của mặt hàng đó

				MessageBox.Show("Hủy thành công");
				HienThi_Luoi();
				double tong = 0;
				for(int i =0;i<dataGridView1.Rows.Count;i++)
				{
					tong += Convert.ToDouble(dataGridView1.Rows[i].Cells[5].Value.ToString());
				}
		
				txtTongtien.Text = tong.ToString(); //set textbox tổng tiền bằng tổng vừa tính
				sql = "Update PhieuNhapHang Set TongTien =N'" + txtTongtien.Text + "' where MaPN='" + txtMaphieu.Text + "'";
				ThucThiSQL.CapNhatDuLieu(sql);// cập nhật tổng tiền của phiếu mua hàng
			}
		}

		private void cbxMaNCC_DropDown(object sender, EventArgs e)
		{
			cbxMaNCC.DataSource = ThucThiSQL.DocBang("Select MaNCC from NhaCungCap");
			cbxMaNCC.ValueMember = "MaNCC";
			cbxMaNCC.SelectedIndex = -1;
		}

		private void cbxMaNCC_TextChanged(object sender, EventArgs e)
		{
			string sql;
			if (cbxMaNCC.Text == "")
			{
				txtTenNCC.Text = "";
				txtDiachiNCC.Text = "";
				txtSdtNCC.Text = "";
				return;
			}
			sql = "Select * from NhaCungCap Where MaNCC ='" + cbxMaNCC.Text + "'";
			DataTable table = ThucThiSQL.DocBang(sql);
			if (table.Rows.Count > 0)
			{
				txtTenNCC.Text = table.Rows[0][1].ToString();
				txtDiachiNCC.Text = table.Rows[0][2].ToString();
				txtSdtNCC.Text = table.Rows[0][3].ToString();
			}
		}

		private void btnIn_Click(object sender, EventArgs e)
		{

		}
	}
}
