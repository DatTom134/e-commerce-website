using System;
using System.Data.SqlClient;

namespace TNS_Ecommerce.Models
{
	public class Auth
	{
		private static readonly string ConnectionString = TNS.DBConnection.Connecting.SQL_MainDatabase;

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

		public static bool RegisterUser(string username, string email, string passwordHash)
		{
			string query = @"
                INSERT INTO Users (Username, Email, PasswordHash, CreatedAt, Role)
                VALUES (@Username, @Email, @PasswordHash, GETDATE(), 'User')";

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
	}
}
