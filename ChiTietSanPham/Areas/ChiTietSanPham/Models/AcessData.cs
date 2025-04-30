using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ChiTietSanPham.Areas.ChiTietSanPham.Models
{
	public class AcessData
	{
		public static List<string[]> TargetResultByDepartment(int InYear, string DepartmentKey)
		{
			DataTable zTable = new DataTable();
			string zSQL = "select n.Label\r\nfrom [dbo].[Nodes] n\r\njoin [dbo].[Relations_Between_Nodes] r on r.IDNodeChildren=n.IDNode";

			string zConnectionString = TNS.DBConnection.Connecting.SQL_MainDatabase;
			try
			{
				SqlConnection zConnect = new SqlConnection(zConnectionString);
				zConnect.Open();
				SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
				zCommand.CommandType = CommandType.Text;
				SqlDataAdapter zAdapter = new SqlDataAdapter(zCommand);
				zAdapter.Fill(zTable);
				zCommand.Dispose();
				zConnect.Close();
			}
			catch (Exception ex)
			{
				string zMessage = ex.ToString();
			}
			List<string[]> zResult = new List<string[]>();
			string[] zItem;

			int n = zTable.Columns.Count;
			foreach (DataRow zRow in zTable.Rows)
			{
				zItem = new string[n];
				for (int i = 0; i < n; i++)
				{
					zItem[i] = zRow[i].ToString()!;
				}
				zResult.Add(zItem);
			}

			return zResult;
		}

		public static List<string[]> getProduct(string id)
		{
			DataTable table = new DataTable();
			string query = $"select * from Products where Products.ProductKey = N'{id}'";

			try
			{
				using (SqlConnection connect = new SqlConnection(TNS.DBConnection.Connecting.SQL_MainDatabase))
				{
					using (SqlCommand command = new SqlCommand(query, connect))
					{
						command.CommandType = CommandType.Text;
						using (SqlDataAdapter adapter = new SqlDataAdapter(command))
						{
							adapter.Fill(table);
						}
						command.Dispose();
					}
					connect.Close();
				}
			}
			catch (Exception e)
			{
				string zMessage = e.ToString();
			}
			List<string[]> zResult = new List<string[]>();
			string[] zItem;

			int n = table.Columns.Count;
			foreach (DataRow zRow in table.Rows)
			{
				zItem = new string[n];
				for (int i = 0; i < n; i++)
				{
					zItem[i] = zRow[i].ToString()!;
				}
				zResult.Add(zItem);
			}

			return zResult;
		}

		public static List<string[]> getProducts()
		{
			DataTable table = new DataTable();
			string query = $"select * from Products";

			try
			{
				using (SqlConnection connect = new SqlConnection(TNS.DBConnection.Connecting.SQL_MainDatabase))
				{
					using (SqlCommand command = new SqlCommand(query, connect))
					{
						command.CommandType = CommandType.Text;
						using (SqlDataAdapter adapter = new SqlDataAdapter(command))
						{
							adapter.Fill(table);
						}
						command.Dispose();
					}
					connect.Close();
				}
			}
			catch (Exception e)
			{
				string zMessage = e.ToString();
			}
			List<string[]> zResult = new List<string[]>();
			string[] zItem;

			int n = table.Columns.Count;
			foreach (DataRow zRow in table.Rows)
			{
				zItem = new string[n];
				for (int i = 0; i < n; i++)
				{
					zItem[i] = zRow[i].ToString()!;
				}
				zResult.Add(zItem);
			}
			return zResult;
		}
	}
}
