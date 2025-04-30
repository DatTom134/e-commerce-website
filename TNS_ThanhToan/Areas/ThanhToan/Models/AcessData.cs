using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Hosting.Internal;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Numerics;

namespace TNS_ThanhToan.Areas.ThanhToan.Models
{
    public class AcessData
    {
		// Lấy thông tin đơn hàng của người dùng theo UserID
		public static List<string[]> GetOrderDetail(string UserID)
		{
			DataTable zTable = new DataTable();
			string zSQL = @"SELECT
								u.UserID, 
								u.Username, 
								u.Email,
								p.ImageURL, 
								p.ProductName, 
								p.Price,
								scd.Quantity, 
						  		p.ProductKey
						  FROM dbo.Users u 
						  INNER JOIN dbo.ShoppingCart sc ON u.UserID = sc.UserID
						  INNER JOIN dbo.ShoppingCartDetail scd ON sc.ShoppingCardKey = scd.ShoppingCardKey 
						  INNER JOIN dbo.Products p ON scd.ProductKey = p.ProductKey
						  WHERE u.UserID = @UserID";

			string zConnectionString = TNS.DBConnection.Connecting.SQL_MainDatabase;

			try
			{
				using (SqlConnection zConnect = new SqlConnection(zConnectionString))
				{
					zConnect.Open();
					using (SqlCommand zCommand = new SqlCommand(zSQL, zConnect))
					{
						// Thêm tham số @UserID
						zCommand.Parameters.AddWithValue("@UserID", UserID);

						using (SqlDataAdapter zAdapter = new SqlDataAdapter(zCommand))
						{
							zAdapter.Fill(zTable);
						}
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("Error: " + ex.Message);
			}

			return DataTableToList(zTable);
		}



		// Cập nhật thông tin người dùng
		public static bool UpdateUserDetails(string userId, string phone, string fullAddress)
		{
			bool isUpdated = false;
			string zSQL = "UPDATE Users SET Phone = @Phone, Address = @Address WHERE UserID = @UserID";
			string zConnectionString = TNS.DBConnection.Connecting.SQL_MainDatabase;

			try
			{
				using (SqlConnection zConnect = new SqlConnection(zConnectionString))
				{
					zConnect.Open();
					using (SqlCommand zCommand = new SqlCommand(zSQL, zConnect))
					{
						zCommand.Parameters.AddWithValue("@UserID", userId);
						zCommand.Parameters.AddWithValue("@Phone", phone);
						zCommand.Parameters.AddWithValue("@Address", fullAddress);

						int rowsAffected = zCommand.ExecuteNonQuery();
						isUpdated = rowsAffected > 0;
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("Error: " + ex.Message);
			}

			return isUpdated;
		}

        public static string AddOrder(string userid, string address, float total)
        {
			DataTable ztable = new DataTable();
			string zSQL = @"
							DECLARE @InsertedOrderID TABLE (OrderID uniqueidentifier);

							INSERT INTO [TNS_Ecommerce].[dbo].[Orders] 
								([UserID], [OrderDate], [ShippingAddress], [TotalAmount], [OrderStatus])
							OUTPUT INSERTED.OrderID INTO @InsertedOrderID
							VALUES 
								(@userid, GETDATE(), @address, @totalamount, 0);

							SELECT OrderID FROM @InsertedOrderID;
			"; // chuỗi lấy được OrderID sau khi thêm vào table Orders
            string zConnectionString = TNS.DBConnection.Connecting.SQL_MainDatabase;
			string query_insert_order_detail = "";
			string OrderID = "";
            //try
            //{
            using (SqlConnection zConnect = new SqlConnection(zConnectionString))
			{
				zConnect.Open();
				using (SqlCommand zCommand = new SqlCommand(zSQL, zConnect))
				{
					zCommand.Parameters.AddWithValue("@userid", userid);
					zCommand.Parameters.AddWithValue("@address", address);
					zCommand.Parameters.AddWithValue("@totalamount", total);

					using (SqlDataAdapter zAdapter = new SqlDataAdapter(zCommand))
					{
						zAdapter.Fill(ztable);
					}
					OrderID = ztable.Rows[0]["OrderID"].ToString() ?? ""; // gán biến OrderID để tiện sử dụng cho mục đích khác
					List<string[]> CartDetails = GetOrderDetail(userid);


					if (CartDetails.Count > 0)
					{
						Console.WriteLine(OrderID);
						CartDetails.ForEach(CartDetail =>
						{
							query_insert_order_detail += $@"
								insert into 
								OrderDetails (OrderID, ProductKey, Quantity, UnitPrice) values 
								('{OrderID}', '{CartDetail[7]}', {CartDetail[6]}, {CartDetail[5]})
							";
						});
					}

					Console.WriteLine(query_insert_order_detail);
				}
			}
            return add_order_detail(query_insert_order_detail) ? OrderID : "";
        }

		private static bool add_order_detail(string query)
		{
            string zConnectionString = TNS.DBConnection.Connecting.SQL_MainDatabase;
            try
			{
				using (SqlConnection zConnect = new SqlConnection(zConnectionString))
				{
					zConnect.Open();
					using (SqlCommand zCommand = new SqlCommand(query, zConnect))
					{
						return zCommand.ExecuteNonQuery() > 0;
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("Error:", ex.Message);
			}
			return false;
        }



        // Chuyển đổi DataTable thành List<string[]>
        private static List<string[]> DataTableToList(DataTable table)
        {
            List<string[]> result = new List<string[]>();
            foreach (DataRow row in table.Rows)
            {
                string[] item = new string[table.Columns.Count];
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    item[i] = row[i].ToString();
                }
                result.Add(item);
            }
            return result;
        }
    }
}
