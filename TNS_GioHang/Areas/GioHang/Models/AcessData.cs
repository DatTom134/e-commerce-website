using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TNS_GioHang.Areas.GioHang.Models
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
                    zItem[i] = zRow[i].ToString();
                }
                zResult.Add(zItem);
            }

            return zResult;
        }

		public static List<string[]> GetCart(string UserID)
		{
			DataTable zTable = new DataTable();
			string zSQL = "select * FROM [TNS_Ecommerce].[dbo].[ShoppingCart] WHERE UserID=@UserID";

			string zConnectionString = TNS.DBConnection.Connecting.SQL_MainDatabase;
			try
			{
				SqlConnection zConnect = new SqlConnection(zConnectionString);
				zConnect.Open();
				SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
				zCommand.CommandType = CommandType.Text;
                zCommand.Parameters.AddWithValue("@UserID", UserID);
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
		public static bool CreateCart(string userID)
		{
			bool isCreated = false;
			string zSQL = "INSERT INTO [TNS_Ecommerce].[dbo].[ShoppingCart] (UserID) VALUES (@UserID)";

			string zConnectionString = TNS.DBConnection.Connecting.SQL_MainDatabase;
			try
			{
				using (SqlConnection zConnect = new SqlConnection(zConnectionString))
				{
					zConnect.Open();
					using (SqlCommand zCommand = new SqlCommand(zSQL, zConnect))
					{
						zCommand.CommandType = CommandType.Text;
						zCommand.Parameters.AddWithValue("@UserID", userID);

						int rowsAffected = zCommand.ExecuteNonQuery();
						isCreated = rowsAffected > 0;
					}
				}
			}
			catch (Exception ex)
			{
				string zMessage = ex.ToString();				
			}

			return isCreated;
		}
		public static bool AddToCart(string CartKey,string ProductKey,int Quantity,float Price)
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
		public static bool DeleteItem(string CartKey,string ProductKey)
		{
			bool IsDeleted = false;
			string zSQL = "DELETE FROM [TNS_Ecommerce].[dbo].[ShoppingCartDetail] " +
				"WHERE ShoppingCardKey=@ShoppingCardKey AND ProductKey = @ProductKey";

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
						int rowsAffected = zCommand.ExecuteNonQuery();
						IsDeleted = rowsAffected > 0;
					}
				}
			}
			catch (Exception ex)
			{
				string zMessage = ex.ToString();
			}

			return IsDeleted;
		}

	}
}
