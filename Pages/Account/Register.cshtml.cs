using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using TNS_Ecommerce.Models;

namespace TNS_Ecommerce.Pages.Account
{
	[IgnoreAntiforgeryToken]
	public class RegisterModel : PageModel
	{
		[BindProperty]
		public string TenDangNhap { get; set; }
		[BindProperty]
		public string Email { get; set; }
		[BindProperty]
		public string MatKhau { get; set; }
		[BindProperty]
		public string XacNhanMatKhau { get; set; }

		[HttpPost]
		public IActionResult OnPostRegister([FromBody] UserRegistrationModel registrationModel)
		{
			
			Console.WriteLine(registrationModel.TenDangNhap);
			Console.WriteLine(registrationModel.Email);
			Console.WriteLine(registrationModel.MatKhau);
			if (string.IsNullOrEmpty(registrationModel.TenDangNhap) ||
				string.IsNullOrEmpty(registrationModel.Email) ||
				string.IsNullOrEmpty(registrationModel.MatKhau))
			{
				return new JsonResult(new { success = false, message = "Vui lòng điền đầy đủ thông tin." });
			}

			if (registrationModel.MatKhau != registrationModel.XacNhanMatKhau)
			{
				return new JsonResult(new { success = false, message = "Mật khẩu và xác nhận mật khẩu không khớp." });
			}

			if (Auth.IsUserExists(registrationModel.TenDangNhap, registrationModel.Email))
			{
				return new JsonResult(new { success = false, message = "Tên đăng nhập hoặc email đã tồn tại." });
			}

			string passwordHash = BCrypt.Net.BCrypt.HashPassword(registrationModel.MatKhau);

			Console.WriteLine("Fetched Data: " + JsonConvert.SerializeObject(Auth.RegisterUser(registrationModel.TenDangNhap, registrationModel.Email, passwordHash)));
			if (Auth.RegisterUser(registrationModel.TenDangNhap, registrationModel.Email, passwordHash))
			{
				return new JsonResult(new { success = true, message = "Đăng ký thành công!" });
			}
			else
			{
				return new JsonResult(new { success = false, message = "Đăng ký thất bại. Vui lòng thử lại." });
			}
		}
	}

	public class UserRegistrationModel
	{
		public string TenDangNhap { get; set; }
		public string Email { get; set; }
		public string MatKhau { get; set; }
		public string XacNhanMatKhau { get; set; }
	}
}
