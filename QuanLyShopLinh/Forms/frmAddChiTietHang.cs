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
	public partial class frmAddChiTietHang : Form
	{
		public frmAddChiTietHang()
		{
			InitializeComponent();
		}

		private void HienThi_Luoi()
		{

			DataTable tblMH;
			string sql = "select * from Hang where MaMH='"+txtMaMH.Text+"'" ;
			tblMH = ThucThiSQL.DocBang(sql);
			dataGridView1.DataSource = tblMH;
			dataGridView1.Columns[0].HeaderText = "Mã Hàng";
			dataGridView1.Columns[1].HeaderText = "Size ";
			dataGridView1.Columns[2].HeaderText = "Màu Sắc";
			dataGridView1.Columns[3].HeaderText = "Mã Mặt Hàng";
			dataGridView1.Columns[4].HeaderText = "Số Lượng Nhập";
			dataGridView1.Columns[5].HeaderText = "Số Lượng Còn Lại";



			dataGridView1.AllowUserToAddRows = false;
			dataGridView1.EditMode = DataGridViewEditMode.EditProgrammatically;
			tblMH.Dispose();

		}
		private void ResetValue()
		{
			string key = txtMaMH.Text;
			txtMaHang.Text = ThucThiSQL.CreateKeyCTH(key);
			txtMaHang.ReadOnly = true;

			txtSize.Text = "";
			txtMausac.Text = "";
			txtSLcon.Text = "";
		}
		private void btnAdd_Click(object sender, EventArgs e)
		{
			if (txtMaHang.Text.Trim().Length == 0)
			{
				MessageBox.Show("Bạn phải nhập mã chi tiết hàng", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				txtMaHang.Focus();
				return;
			}
			if (txtSize.Text.Trim().Length == 0)
			{
				MessageBox.Show("Bạn phải nhập size mặt hàng", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				txtSize.Focus();
				return;
			}
			if (txtMausac.Text.Trim().Length == 0)
			{
				MessageBox.Show("Bạn phải nhập màu sắc mặt hàng", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				txtMausac.Focus();
				return;
			}
			if (txtSLcon.Text.Trim().Length == 0)
			{
				MessageBox.Show("Bạn phải nhập số lượng hàng", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				txtSLcon.Focus();
				return;
			}

			string sql = "select MaHang from Hang where MaMH='" + txtMaHang.Text + "'";
			if (ThucThiSQL.DocBang(sql).Rows.Count > 0) // kiểm tra xem mã hàng đã tồn tại trong bảng hàng chưa
			{
				MessageBox.Show("Mã hàng đã tồn tại , mời nhập lại", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				txtMaHang.Focus();
				txtMaHang.Text = "";
				return;
			}

			double total=0;
			for(int i=0;i< dataGridView1.Rows.Count;i++)// vòng lặp để tính ra số lượng hàng của mặt hàng đó
			{	
					total += Convert.ToDouble(dataGridView1.Rows[0].Cells[4].Value);	
			}
			total += Convert.ToDouble(txtSLcon.Text);// số lượng đã tồn tại cộng thêm với số lượng của hàng đang nhập để kiểm tra xem có vượt quá tổng cho phép không

			if (total > Convert.ToDouble(txtSLtong.Text)) // nếu vượt thì báo lỗi bắt nhập lại
			{
				MessageBox.Show("Đã vượt quá số lượng cho phép , mời nhập lại", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				txtSLcon.Text = "";
				txtSLcon.Focus();
				return;
			}
			else // không vượt quá thì cho phép thêm dữ liệu

			{
				sql = "insert into Hang values(N'" + txtMaHang.Text + "',N'" + txtSize.Text + "',N'" + txtMausac.Text + "',N'" + txtMaMH.Text + "',N'" + txtSLcon.Text + "',N'" + txtSLcon.Text + "')"; // lúc đầu nhập thì số lượng nhập bằng số lượng còn lại
				ThucThiSQL.CapNhatDuLieu(sql); // thêm chi tiết hàng vào trong bảng hàng theo mã mặt hàng có sẵn
			}
			HienThi_Luoi();
			ResetValue(); // sau khi thêm thì reset giá trị về rỗng để nhập tiếp
		}

		private void button1_Click(object sender, EventArgs e)
		{
			this.Close();
		}
		
		private void frmChiTietHang_Load(object sender, EventArgs e)
		{
			HienThi_Luoi();
			ResetValue();
		}

		private void txtSLcon_KeyPress(object sender, KeyPressEventArgs e)
		{
			if ((e.KeyChar < '0' || e.KeyChar > '9') && (e.KeyChar != 8) && (e.KeyChar != 46))
			{
				e.Handled = true;
			}
		}

		private void dataGridView1_DoubleClick(object sender, EventArgs e)
		{
			if (MessageBox.Show("Bạn có chắc chắn xóa không?", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
			{

				ThucThiSQL.CapNhatDuLieu("Delete Hang where MaHang='" + dataGridView1.CurrentRow.Cells["MaHang"].Value.ToString() + "'"); // đầu tiên xóa chi tiết hóa đơn theo mã hàng
				MessageBox.Show("Hủy thành công");
				HienThi_Luoi();
			}
		}
	}
}
