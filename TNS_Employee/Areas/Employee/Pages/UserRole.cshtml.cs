using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using System.Security.Claims;
using TNS_Employee.Areas.Employee.Cookie;
using TNS_Employee.Areas.Employee.Models;

namespace TNS_Employee.Areas.Employee.Pages
{
    public class UserRoleModel : PageModel
    {
        private UserLogin_Cookie _UserLogin;
        [BindProperty]
        public RolesOfUser RoleInfo { get; set; }

        public IActionResult OnGet(string key)
        {
            if (key == null || key.Length != 36)
            {
                return LocalRedirect("~/Warning?id=300");
            }
            else
            {
                CheckAuth();
                if (_UserLogin.RoleItem.IsRead)
                {
                    RoleInfo = new RolesOfUser(key);
                    return Page();
                }
                else
                   return LocalRedirect("~/Warning?id=403");
            }
        }
        public async Task<ActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                CheckAuth();
                if (_UserLogin.RoleItem.IsApproval)
                {
                    RoleInfo.Save();
                    return LocalRedirect("~/Employee/UserList");
                }
                else
                {
                    return LocalRedirect("~/Warning?id=407");
                }
            }

            return Page();
        }
        private void CheckAuth()
        {
            _UserLogin = new UserLogin_Cookie(User);
            _UserLogin.GetRole("Employee/EmployeeList");
        }

    }
}
