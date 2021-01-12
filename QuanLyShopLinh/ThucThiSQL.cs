using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace QuanLyShopLinh
{
	class ThucThiSQL
	{
		public static SqlConnection conn;
		public static string connString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename="
											+ System.IO.Directory.GetCurrentDirectory().ToString() 
											+"\\Database\\Banhang.mdf;Integrated Security=True";

		public static void KetNoiCSDL()
		{
			conn = new SqlConnection();
			conn.ConnectionString = connString;
			if (conn.State != ConnectionState.Open)
			{
				conn.Open();
			}
		}
		public static void DongKetNoiCSDL()
		{
			if (conn.State != ConnectionState.Closed)
			{
				conn.Close();
				conn.Dispose();
				conn = null;
			}
		}
		public static void CapNhatDuLieu(string sql)
		{
			KetNoiCSDL();
			SqlCommand sqlcommand = new SqlCommand();
			sqlcommand.Connection = conn;
			sqlcommand.CommandText = sql;
			sqlcommand.ExecuteNonQuery();
			DongKetNoiCSDL();
		}
		public static DataTable DocBang(String sql)
		{
			DataTable dt = new DataTable();
			SqlDataAdapter Mydata = new SqlDataAdapter();
			Mydata.SelectCommand = new SqlCommand();
			KetNoiCSDL();
			Mydata.SelectCommand.Connection = conn;
			Mydata.SelectCommand.CommandText = sql;
			Mydata.Fill(dt);
			DongKetNoiCSDL();
			return dt;
		}
		public static string CreateKey(string table,string Key)// hàm tạo giá trị mã cho các bảng
		{
			DataTable dt = new DataTable();
			dt = DocBang("Select * from "+table+" ");
			int coso = 0;

			if (dt.Rows.Count == 0)
			{
				coso = 1;
			}
			else if (dt.Rows.Count == 1 && int.Parse(dt.Rows[0][0].ToString().Substring(2, 3)) == 1) // nếu danh sách nhân viên có 1 nhân viên và mã người này là NV001
			{
				coso = 2;
			}
			else if (dt.Rows.Count == 1 && int.Parse(dt.Rows[0][0].ToString().Substring(2, 3)) > 1) // nếu danh sách có 1 nhân viên mà mã người này khác NV001
			{
				coso = 1;
			}
			else
			{
				for (int i = 0; i < dt.Rows.Count - 1; i++)
				{
					if (int.Parse(dt.Rows[i + 1][0].ToString().Substring(2, 3)) - int.Parse(dt.Rows[i][0].ToString().Substring(2, 3)) > 1)
					{
						coso = int.Parse(dt.Rows[i][0].ToString().Substring(2, 3)) + 1;
						break;
					}
				}
				coso = int.Parse(dt.Rows[dt.Rows.Count - 1][0].ToString().Substring(2, 3)) + 1;
			}
			string ma = "";
			if (coso < 10)
				return ma = Key +"00" + coso;
			else if (coso < 100)
				return ma = Key + "0" + coso;
			else
				return ma = Key + coso;

		}
		public static string CreateKeyCTH(string Key)// hàm tạo giá trị mã cho các bảng hang`
		{
			DataTable dt = new DataTable();
			dt = DocBang("Select * from Hang where MaMH=N'"+Key+"'");
			int coso = 0;
			int slKey = Key.Length;
			
			if (dt.Rows.Count == 0)
			{
				coso = 1;
			}
			else if (dt.Rows.Count == 1 && int.Parse(dt.Rows[0][0].ToString().Substring(slKey, 3)) == 1) // nếu danh sách nhân viên có 1 nhân viên và mã người này là NV001
			{
				coso = 2;
			}
			else if (dt.Rows.Count == 1 && int.Parse(dt.Rows[0][0].ToString().Substring(slKey, 3)) > 1) // nếu danh sách có 1 nhân viên mà mã người này khác NV001
			{
				coso = 1;
			}
			else
			{
				for (int i = 0; i < dt.Rows.Count - 1; i++)
				{
					if (int.Parse(dt.Rows[i + 1][0].ToString().Substring(slKey, 3)) - int.Parse(dt.Rows[i][0].ToString().Substring(slKey, 3)) > 1)
					{
						coso = int.Parse(dt.Rows[i][0].ToString().Substring(slKey, 3)) + 1;
						break;
					}
				}
				coso = int.Parse(dt.Rows[dt.Rows.Count - 1][0].ToString().Substring(slKey, 3)) + 1;
			}
			string ma = "";
			if (coso < 10)
				return ma = Key + "00" + coso;
			else if (coso < 100)
				return ma = Key + "0" + coso;
			else
				return ma = Key + coso;

		}
	}
}
