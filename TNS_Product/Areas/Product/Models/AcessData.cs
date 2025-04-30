using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TNS_Product.Areas.Product.Models
{
    public class AcessData
    {
		public static List<string[]> ListProduct()
		{
			string zMessage = "";
			string zSQL = "SELECT * " +
				" FROM [dbo].[Products] ";
			DataTable zTable = new DataTable();
			string zConnectionString = TNS.DBConnection.Connecting.SQL_MainDatabase;
			try
			{
				SqlConnection zConnect = new SqlConnection(zConnectionString);
				zConnect.Open();
				SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
				zCommand.CommandType = CommandType.Text;
				//zCommand.Parameters.Add("@StatusKey", SqlDbType.Int).Value = StatusKey;
				//zCommand.Parameters.Add("@Search", SqlDbType.NVarChar).Value = "%" + Search + "%";
				////Test
				////zCommand.Parameters.Add("@OnwerBy", SqlDbType.UniqueIdentifier).Value = new Guid(EmployeeKey);
				//zCommand.Parameters.Add("@OnwerBy", SqlDbType.UniqueIdentifier).Value = new Guid(EmployeeKey);
				//zCommand.Parameters.Add("@PageSize", SqlDbType.Int).Value = PageSize;
				//zCommand.Parameters.Add("@PageNumber", SqlDbType.Int).Value = PageNumber;
				SqlDataAdapter zAdapter = new SqlDataAdapter(zCommand);
				zAdapter.Fill(zTable);
				zCommand.Dispose();
				zConnect.Close();
			}
			catch (Exception ex)
			{
				zMessage = ex.ToString();
			}
			List<string[]> zResult = new List<string[]>();
			string[] zItem;
			if (zMessage.Length > 0)
			{
				zItem = new string[4];
				zItem[0] = "ERR";
				zItem[1] = "zMessage";
				zResult.Add(zItem);
			}
			int n = zTable.Columns.Count;
			foreach (DataRow zRow in zTable.Rows)
			{
				zItem = new string[n];
				for (int i = 0; i < n; i++)
				{
					zItem[i] = zRow[i].ToString();
				}
				zResult.Add(zItem);
			}

			return zResult;
		}
		public static List<string[]> getStock(string key)
		{
			//Console.WriteLine("Key dưới data:", key);
			DataTable table = new DataTable();
			string query = $@"select Stock
							from Products 
							where Products.ProductKey = N'{key}'";

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


		public static List<string[]> getProduct(string key)
		{
			//Console.WriteLine("Key dưới data:", key);
			DataTable table = new DataTable();
			string query = $@"select *
							from Products 
							where Products.ProductKey = N'{key}'";

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
                    zItem[i] = zRow[i].ToString();
                }
                zResult.Add(zItem);
            }

            return zResult;
        }

        #region [ lấy user theo email ]
        public static List<string[]> GetUser(string Email)
		{
			string zMessage = "";
			string zSQL = @"
						SELECT *
						FROM [TNS_Ecommerce].[dbo].[Users] 
						WHERE Email = @Email";
			DataTable zTable = new DataTable();
			string zConnectionString = TNS.DBConnection.Connecting.SQL_MainDatabase;
			try
			{
				SqlConnection zConnect = new SqlConnection(zConnectionString);
				zConnect.Open();
				SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
				zCommand.CommandType = CommandType.Text;
				zCommand.Parameters.AddWithValue("@Email", Email);
				SqlDataAdapter zAdapter = new SqlDataAdapter(zCommand);
				zAdapter.Fill(zTable);
				zCommand.Dispose();
				zConnect.Close();
			}
			catch (Exception ex)
			{
				zMessage = ex.ToString();
			}
			List<string[]> zResult = new List<string[]>();
			string[] zItem;
			if (zMessage.Length > 0)
			{
				zItem = new string[4];
				zItem[0] = "ERR";
				zItem[1] = "zMessage";
				zResult.Add(zItem);
			}
			int n = zTable.Columns.Count;
			foreach (DataRow zRow in zTable.Rows)
			{
				zItem = new string[n];
				for (int i = 0; i < n; i++)
				{
					zItem[i] = zRow[i].ToString();
				}
				zResult.Add(zItem);
			}
			return zResult;
		}
        #endregion

        public static bool AddToCart(string CartKey, string ProductKey, int Quantity, float Price)
		{
			bool IsAdded = false;
			string zSQL = "INSERT INTO [TNS_Ecommerce].[dbo].[ShoppingCartDetail] (ShoppingCardKey,ProductKey,Quantity,Price) " +
				"VALUES (@ShoppingCardKey,@ProductKey,@Quantity,@Price)";

			string zConnectionString = TNS.DBConnection.Connecting.SQL_MainDatabase;
			try
			{
				using (SqlConnection zConnect = new SqlConnection(zConnectionString))
				{
					zConnect.Open();
					using (SqlCommand zCommand = new SqlCommand(zSQL, zConnect))
					{
						zCommand.CommandType = CommandType.Text;
						zCommand.Parameters.AddWithValue("@ShoppingCardKey", CartKey);
						zCommand.Parameters.AddWithValue("@ProductKey", ProductKey);
						zCommand.Parameters.AddWithValue("@Quantity", Quantity);
						zCommand.Parameters.AddWithValue("@Price", Price);
						int rowsAffected = zCommand.ExecuteNonQuery();
						IsAdded = rowsAffected > 0;
					}
				}
			}
			catch (Exception ex)
			{
				string zMessage = ex.ToString();
			}

			return IsAdded;
		}

		public static List<string[]> GetCartDetails(string CartKey)
		{
			DataTable zTable = new DataTable();
			string zSQL = "select a.*,b.ProductName,b.Price,b.ImageURL " +
				"FROM [TNS_Ecommerce].[dbo].[ShoppingCartDetail] as a join [TNS_Ecommerce].[dbo].[Products] as b on a.ProductKey = b.ProductKey" +
				" WHERE [ShoppingCardKey]=@CartKey";

			string zConnectionString = TNS.DBConnection.Connecting.SQL_MainDatabase;
			try
			{
				SqlConnection zConnect = new SqlConnection(zConnectionString);
				zConnect.Open();
				SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
				zCommand.CommandType = CommandType.Text;
				zCommand.Parameters.AddWithValue("@CartKey", CartKey);
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
					zItem[i] = zRow[i].ToString();
				}
				zResult.Add(zItem);
			}
			return zResult;
		}

		#region [ update quantity ]
		public static bool UpdateCartQuantity(string cartKey, string productKey, int quantity)
		{
			bool isUpdated = false;
			string zSQL = "UPDATE [TNS_Ecommerce].[dbo].[ShoppingCartDetail] " +
						  "SET Quantity = @Quantity " +
						  "WHERE ShoppingCardKey = @CartKey AND ProductKey = @ProductKey";

			string zConnectionString = TNS.DBConnection.Connecting.SQL_MainDatabase;
			try
			{
				using (SqlConnection zConnect = new SqlConnection(zConnectionString))
				{
					zConnect.Open();
					using (SqlCommand zCommand = new SqlCommand(zSQL, zConnect))
					{
						zCommand.CommandType = CommandType.Text;
						zCommand.Parameters.AddWithValue("@CartKey", cartKey);
						zCommand.Parameters.AddWithValue("@ProductKey", productKey);
						zCommand.Parameters.AddWithValue("@Quantity", quantity);

						int rowsAffected = zCommand.ExecuteNonQuery();
						isUpdated = rowsAffected > 0;
					}
				}
			}
			catch (Exception ex)
			{
				string zMessage = ex.ToString();
			}

			return isUpdated;
		}

		#endregion

		#region [Tìm kiếm sản phẩm]
		public static List<string[]> GetItemByName(string Name)
		{
			string zMessage = "";
			string zSQL = "SELECT * " +
				"FROM [TNS_Ecommerce].[dbo].[Products] WHERE  ProductName LIKE @Name ";
			DataTable zTable = new DataTable();
			string zConnectionString = TNS.DBConnection.Connecting.SQL_MainDatabase;
			try
			{
				SqlConnection zConnect = new SqlConnection(zConnectionString);
				zConnect.Open();
				SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
				zCommand.CommandType = CommandType.Text;
				zCommand.Parameters.AddWithValue("@Name", "%" + Name + "%");
				SqlDataAdapter zAdapter = new SqlDataAdapter(zCommand);
				zAdapter.Fill(zTable);
				zCommand.Dispose();
				zConnect.Close();
			}
			catch (Exception ex)
			{
				zMessage = ex.ToString();
			}
			List<string[]> zResult = new List<string[]>();
			string[] zItem;
			if (zMessage.Length > 0)
			{
				zItem = new string[4];
				zItem[0] = "ERR";
				zItem[1] = "zMessage";
				zResult.Add(zItem);
			}
			int n = zTable.Columns.Count;
			foreach (DataRow zRow in zTable.Rows)
			{
				zItem = new string[n];
				for (int i = 0; i < n; i++)
				{
					zItem[i] = zRow[i].ToString();
				}
				zResult.Add(zItem);
			}
			return zResult;
		}
		#endregion	
	}
}
