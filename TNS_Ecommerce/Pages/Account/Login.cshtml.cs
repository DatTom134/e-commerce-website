using System.Data.SqlClient;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Identity.Data;

namespace TNS_Ecommerce.Pages.Account
{
	[IgnoreAntiforgeryToken]
	public class LoginModel : PageModel
	{

		[HttpPost]
		public JsonResult OnPostLogin([FromBody] LoginRequestModel loginRequest)
		{
			if (string.IsNullOrEmpty(loginRequest.Email) || string.IsNullOrEmpty(loginRequest.Password))
			{
				return new JsonResult(new { success = false, message = "Vui lòng điền đầy đủ thông tin." });
			}

			try
			{
				string connectionString = TNS.DBConnection.Connecting.SQL_MainDatabase;
				using (SqlConnection connection = new SqlConnection(connectionString))
				{
					connection.Open();
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
							return new JsonResult(new { success = true, message = "Đăng nhập thành công!" });
						}
						else
						{
							return new JsonResult(new { success = false, message = "Email hoặc mật khẩu không đúng." });
						}
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("Lỗi khi đăng nhập: " + ex.Message);
				return new JsonResult(new { success = false, message = "Có lỗi xảy ra. Vui lòng thử lại." });
			}
		}

		[HttpPost]
		public JsonResult GetUserId([FromBody] UserEmailRequestModel request)
		{
			if (string.IsNullOrEmpty(request.Email))
			{
				return new JsonResult(new { success = false, message = "Vui lòng cung cấp email." });
			}

			try
			{
				string connectionString = TNS.DBConnection.Connecting.SQL_MainDatabase;
				using (SqlConnection connection = new SqlConnection(connectionString))
				{
					connection.Open();
					string query = "SELECT UserId FROM Users WHERE Email = @Email";
					using (SqlCommand command = new SqlCommand(query, connection))
					{
						command.Parameters.AddWithValue("@Email", request.Email);
						var result = command.ExecuteScalar();

						if (result != null)
						{
							return new JsonResult(new { success = true, userId = result.ToString() });
						}
						else
						{
							return new JsonResult(new { success = false, message = "Không tìm thấy người dùng." });
						}
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("Lỗi khi lấy user ID: " + ex.Message);
				return new JsonResult(new { success = false, message = "Có lỗi xảy ra." });
			}
		}

		[HttpPost]
		public IActionResult OnPostAuth([FromBody] LoginRequestModel request)
		{
			Console.WriteLine("Email:", request.Email);
			Console.WriteLine("Password:", request.Password);
			if (check_login(request))
			{
				var claims = new List<Claim>
				{
					new Claim(ClaimTypes.Email, request.Email)
					// Bạn có thể thêm các claims khác tại đây
				};

				var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
				var principal = new ClaimsPrincipal(identity);

				// Bước 3: Đăng nhập và lưu claims vào ngữ cảnh hiện tại
				HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

				// Chuyển hướng đến trang chính
				return new JsonResult(new { success = true });
			}
			else
			{
				return new JsonResult(new { success = false });
			}
		}

		public bool check_login(LoginRequestModel request)
		{
			Console.WriteLine("Email:", request?.Email);
			Console.WriteLine("Password:", request?.Password);
			if (string.IsNullOrEmpty(request?.Email) || string.IsNullOrEmpty(request?.Password))
			{
				return false;
			}

			try
			{
				string connectionString = TNS.DBConnection.Connecting.SQL_MainDatabase;
				using (SqlConnection connection = new SqlConnection(connectionString))
				{
					connection.Open();
					string query = @"
                        SELECT PasswordHash 
                        FROM Users 
                        WHERE Email = @Email";
					using (SqlCommand command = new SqlCommand(query, connection))
					{
						command.Parameters.AddWithValue("@Email", request.Email);
						var result = command.ExecuteScalar();

						if (result != null && BCrypt.Net.BCrypt.Verify(request.Password, result.ToString()))
						{
							return true;
						}
						else
						{
							return false;
						}
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("Lỗi khi đăng nhập: " + ex.Message);
				return false;
			}
		}
	}

	public class LoginRequestModel
	{
		public string? Email { get; set; }
		public string? Password { get; set; }
	}
	public class UserEmailRequestModel
	{
		public string? Email { get; set; }

	}
}
