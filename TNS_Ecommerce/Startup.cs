using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using System.Security.Claims;
using TNS_Ecommerce.Models;
using DotNetEnv;

namespace TNS_Ecommerce
    {
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            Env.Load();

            services.AddRazorPages();

            services.AddScoped<GoogleAuthHandler>();

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
            })
            .AddCookie(options =>
            {
                options.LoginPath = "/login-google";
                options.LogoutPath = "/logout";
                options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
                options.SlidingExpiration = true;
            })
            .AddGoogle(options =>
            {
                string? id = Environment.GetEnvironmentVariable("GOOGLE_CLIENT_ID");
                string? secret = Environment.GetEnvironmentVariable("GOOGLE_CLIENT_SECRET");
                
                if (id == null) throw new Exception("ID không được để NULL");

                if (secret == null) throw new Exception("SECRET không được để NULL");

                options.ClientId = id;
                options.ClientSecret = secret;

                options.ClaimActions.MapJsonKey(ClaimTypes.Name, "name");
                options.ClaimActions.MapJsonKey(ClaimTypes.Email, "email");
                options.ClaimActions.MapJsonKey("picture", "picture");
            });

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(60);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSession();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/login-google", async (HttpContext context, GoogleAuthHandler googleAuthHandler) =>
                {
                    await googleAuthHandler.ChallengeGoogleLogin(context);
                });

                endpoints.MapGet("/google-auth", async (HttpContext context, GoogleAuthHandler googleAuthHandler) =>
                {
                    await googleAuthHandler.ProcessGoogleAuthCallback(context);
                });

                endpoints.MapGet("/logout", async (HttpContext context) =>
                {
                    context.Session.Clear();

                    await context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                    context.Response.Redirect("/");
                });

                endpoints.MapControllers();
                endpoints.MapRazorPages();
            });
        }
    }
}
 
