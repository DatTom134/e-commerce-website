using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Text;

public class GoogleAuthHandler
{
    private readonly IConfiguration _configuration;

    public GoogleAuthHandler(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task ChallengeGoogleLogin(HttpContext context)
    {
        await context.ChallengeAsync(GoogleDefaults.AuthenticationScheme, new AuthenticationProperties
        {
            RedirectUri = "/google-auth"
        });
    }

    public async Task ProcessGoogleAuthCallback(HttpContext context)
    {
        // Kiểm tra xem người dùng đã được xác thực hay chưa
        if (!context.User.Identity!.IsAuthenticated)
        {
            context.Response.Redirect("/login-google");
            return;
        }

        // Lấy thông tin người dùng từ ClaimsPrincipal
        var claimsIdentity = context.User.Identity as ClaimsIdentity;
        var claims = claimsIdentity?.Claims;

        string email = claims?.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value!;
        string name = claims?.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value!;
        string profilePicture = claims?.FirstOrDefault(c => c.Type == "picture")?.Value!;

        // Lưu thông tin người dùng vào cơ sở dữ liệu
        await SaveUserToDatabaseAsync(email, name, profilePicture);

        // Chuyển hướng về trang chủ hoặc trang bạn muốn
        context.Response.Redirect("/");
    }
    private string GenerateRandomPassword(int length = 12)
    {
        const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
        StringBuilder res = new StringBuilder();
        Random rnd = new Random();
        while (0 < length--)
        {
            res.Append(valid[rnd.Next(valid.Length)]);
        }
        return res.ToString();
    }

    private async Task SaveUserToDatabaseAsync(string email, string userName, string profilePicture)
    {
        string zConnectionString = TNS.DBConnection.Connecting.SQL_MainDatabase;

        using (SqlConnection connection = new SqlConnection(zConnectionString))
        {
            await connection.OpenAsync();

            // Kiểm tra xem người dùng đã tồn tại chưa
            string checkUserQuery = "SELECT COUNT(1) FROM Users WHERE Email = @Email";
            using (SqlCommand checkUserCmd = new SqlCommand(checkUserQuery, connection))
            {
                checkUserCmd.Parameters.AddWithValue("@Email", email);
                int userExists = (int)await checkUserCmd.ExecuteScalarAsync();

                if (userExists == 0)
                {
                    string randomPassword = GenerateRandomPassword();

                    // Hash mật khẩu trước khi lưu (nếu cần)
                    string hashedPassword = BCrypt.Net.BCrypt.HashPassword(randomPassword);
                    // Thêm mới người dùng
                    string insertUserQuery = "INSERT INTO Users (Email, userName, ProfilePicture, PasswordHash) VALUES (@Email, @userName, @ProfilePicture,@PasswordHash)";
                    using (SqlCommand insertUserCmd = new SqlCommand(insertUserQuery, connection))
                    {
                        insertUserCmd.Parameters.AddWithValue("@Email", email);
                        insertUserCmd.Parameters.AddWithValue("@userName", userName ?? (object)DBNull.Value);
                        insertUserCmd.Parameters.AddWithValue("@ProfilePicture", profilePicture ?? (object)DBNull.Value);
                        insertUserCmd.Parameters.AddWithValue("@PasswordHash", hashedPassword);

                        await insertUserCmd.ExecuteNonQueryAsync();
                    }
                }
                else
                {
                    // Người dùng đã tồn tại, có thể cập nhật thông tin nếu cần
                    string updateUserQuery = "UPDATE Users SET userName = @userName, ProfilePicture = @ProfilePicture WHERE Email = @Email";
                    using (SqlCommand updateUserCmd = new SqlCommand(updateUserQuery, connection))
                    {
                        updateUserCmd.Parameters.AddWithValue("@Email", email);
                        updateUserCmd.Parameters.AddWithValue("@userName", userName ?? (object)DBNull.Value);
                        updateUserCmd.Parameters.AddWithValue("@ProfilePicture", profilePicture ?? (object)DBNull.Value);
                        await updateUserCmd.ExecuteNonQueryAsync();
                    }
                }
            }

            connection.Close();
        }
    }
}
