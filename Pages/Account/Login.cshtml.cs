using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using BCrypt.Net;


namespace TNS_Ecommerce.Pages.Account
{
	[IgnoreAntiforgeryToken]
	public class LoginModel : PageModel
	{
		[BindProperty]
		public string Email { get; set; }
		[BindProperty]
		public string Password { get; set; }

		[HttpPost]
		public JsonResult OnPostLogin([FromBody] LoginRequestModel loginRequest)
		{
			if (string.IsNullOrEmpty(loginRequest.Email) || string.IsNullOrEmpty(loginRequest.Password))
			{
				return new JsonResult(new { success = false, message = "Vui lòng điền đầy đủ thông tin." });
			}

			try
			{
				// Kết nối tới cơ sở dữ liệu
				string connectionString = TNS.DBConnection.Connecting.SQL_MainDatabase; // Lấy chuỗi kết nối từ DBConnection
				using (SqlConnection connection = new SqlConnection(connectionString))
				{
					connection.Open();

					// Truy vấn lấy PasswordHash từ cơ sở dữ liệu
					string query = @"
                        SELECT PasswordHash 
                        FROM Users 
                        WHERE Email = @Email";
					using (SqlCommand command = new SqlCommand(query, connection))
					{
						command.Parameters.AddWithValue("@Email", loginRequest.Email);
						var result = command.ExecuteScalar();

						if (result != null && BCrypt.Net.BCrypt.Verify(loginRequest.Password, result.ToString()))
						{
							Console.WriteLine("vô được");
							// Mật khẩu hợp lệ, trả về thành công
							return new JsonResult(new { success = true, message = "Đăng nhập thành công!" });
						}
						else
						{
							Console.WriteLine("không vô được");
							// Email hoặc mật khẩu không đúng
							return new JsonResult(new { success = false, message = "Email hoặc mật khẩu không đúng." });
						}
					}
				}
			}
			catch (Exception ex)
			{
				// Xử lý lỗi
				return new JsonResult(new { success = false, message = "Có lỗi xảy ra. Vui lòng thử lại." });
			}
		}
	}

	public class LoginRequestModel
	{
		public string Email { get; set; }
		public string Password { get; set; }
	}
}
