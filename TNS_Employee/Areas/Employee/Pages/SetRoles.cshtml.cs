using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using System.Data;
using System.Security.Claims;
using System.Xml.Linq;

namespace TNS_Employee.Areas.Employee.Pages
{
    [IgnoreAntiforgeryToken]
    public class SetRolesModel : PageModel
    {
        #region [ Security ]
        public TNS.Auth.UserLogin_Info UserLogin;
        private void CheckAuth()
        {
            UserLogin = new TNS.Auth.UserLogin_Info(User);
            UserLogin.GetRole("Employee-UserList");
        }
        #endregion

        public string UserSetRole { get; set; }
        public IActionResult OnGet(string key)
        {
            CheckAuth();
            if (UserLogin.Role.IsApproval)
            {
                if (key != null && key.Length == 36)
                {
                    UserSetRole = key;
                    return Page();
                }
                else
                {
                    return LocalRedirect("~/Warning?id=300");
                }
            }
            else
            {
                return LocalRedirect("~/Warning?id=403");
            }


        }
        public IActionResult OnPostSearch([FromBody] ItemRequest request)
        {
            CheckAuth();
            List<ItemView> zList = new List<ItemView>();
            if (UserLogin.Role.IsApproval)
            {
                string zMessageLog = "";
                DataTable zDataEmployee = Models.User_AccessData.GetRolesOfUser(request.UserKey, out zMessageLog);
                
                if (zMessageLog.Length > 0)
                {
                    ItemView zItem = new ItemView();
                    zItem.RoleID = "ERROR";
                    zItem.RoleName = zMessageLog;
                    zList.Add(zItem);
                }
                else
                {
                    string btn_Edit = "";
                    string btn_Del = "";

                    int i = 0;
                    foreach (DataRow zRow in zDataEmployee.Rows)
                    {
                        ItemView zItem = new ItemView();
                        zItem.RoleKey = zRow["RoleKey"].ToString();
                        zItem.RoleID = zRow["RoleID"].ToString();
                        zItem.RoleName = zRow["RoleName"].ToString();
                        zItem.IsRead = zRow["RoleRead"].ToString();
                        zItem.IsAdd = zRow["RoleAdd"].ToString();
                        zItem.IsEdit = zRow["RoleEdit"].ToString();
                        zItem.IsDel = zRow["RoleDel"].ToString();
                        zItem.IsApproval = zRow["RoleApproval"].ToString();

                        zList.Add(zItem);
                        i++;
                    }
                }
            }
            return new JsonResult(zList);
        }
        public IActionResult OnPostUpdate([FromBody] ItemView itemUpdate)
        {
            CheckAuth();
            if (UserLogin.Role.IsApproval)
            {
                string zMessageLog = "";
                Models.UserRole_Record zRole = new Models.UserRole_Record();
                zRole.RoleKey = itemUpdate.RoleKey;
                zRole.UserKey = itemUpdate.UserKey;
                if (zRole.IsRecordExist())
                {
                    zRole.Update(itemUpdate.Action);
                }
                else
                {
                    zRole.Create(itemUpdate.Action);
                }

                itemUpdate.Code = zRole.Code;

                if (zRole.Code == "501")
                {
                    itemUpdate.RoleID = "ERROR";
                    itemUpdate.RoleName = zRole.Message;
                }
            }
            return new JsonResult(itemUpdate);
        }
       
        public class ItemRequest
        {
            public string SearchContent { get; set; }
            public string UserKey { get; set; }
        }
        public class ItemView
        {
            public string UserKey { get; set; }
            public string RoleKey { get; set; }
            public string RoleID { get; set; }
            public string RoleName { get; set; }
            public string IsRead { get; set; }
            public string IsAdd { get; set; }
            public string IsEdit { get; set; }
            public string IsDel { get; set; }
            public string IsApproval { get; set; }
            
            public string Action { get; set; }  
            public string Code { get; set; }
            public ItemView() { }

        }
    }
}
