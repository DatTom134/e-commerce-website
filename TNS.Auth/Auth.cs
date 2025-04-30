using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TNS.Auth
{
    public class Auth
    {
		private static readonly string ConnectionString = TNS.DBConnection.Connecting.SQL_MainDatabase;

		#region [ Is User Exisist ]
		public static bool IsUserExists(string username, string email)
		{
			string query = "SELECT COUNT(1) FROM Users WHERE Username = @Username OR Email = @Email";

			try
			{
				using (SqlConnection connection = new SqlConnection(ConnectionString))
				{
					connection.Open();
					using (SqlCommand command = new SqlCommand(query, connection))
					{
						command.Parameters.AddWithValue("@Username", username);
						command.Parameters.AddWithValue("@Email", email);

						int count = Convert.ToInt32(command.ExecuteScalar());
						return count > 0;
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error in IsUserExists: {ex.Message}");
				return false;
			}
		}

		#endregion

		#region [ Register User ]

		public static bool RegisterUser(string username, string email, string passwordHash)
		{
			string query = @"
                INSERT INTO Users (Username, Email, PasswordHash, CreatedAt, Role)
                VALUES (@Username, @Email, @PasswordHash, GETDATE(), 'Customer')";

			try
			{
				using (SqlConnection connection = new SqlConnection(ConnectionString))
				{
					connection.Open();
					using (SqlCommand command = new SqlCommand(query, connection))
					{
						command.Parameters.AddWithValue("@Username", username);
						command.Parameters.AddWithValue("@Email", email);
						command.Parameters.AddWithValue("@PasswordHash", passwordHash);

						int rowsAffected = command.ExecuteNonQuery();
						return true;
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error in RegisterUser: {ex.Message}");
				return false;
			}
		}

		#endregion

		#region [ Check Email Exists ]
		public static bool IsEmailExists(string email)
		{
			string query = "SELECT COUNT(1) FROM Users WHERE Email = @Email";
			try
			{
				using (SqlConnection connection = new SqlConnection(ConnectionString))
				{
					connection.Open();
					using (SqlCommand command = new SqlCommand(query, connection))
					{
						command.Parameters.AddWithValue("@Email", email);
						return Convert.ToInt32(command.ExecuteScalar()) > 0;
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error in IsEmailExists: {ex.Message}");
				return false;
			}
		}
		#endregion
	}
}
