using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Security.Claims;
using System.Xml.Linq;

namespace TNS_Employee.Areas.Employee.Pages
{
    [IgnoreAntiforgeryToken]
    public class UserListModel : PageModel
    {
        #region [ Security ]
        public TNS.Auth.UserLogin_Info UserLogin;
        private void CheckAuth()
        {
            UserLogin = new TNS.Auth.UserLogin_Info(User);
            UserLogin.GetRole("Employee-UserList");
            UserLogin.Role.IsRead = true;
            UserLogin.Role.IsEdit = true;
            UserLogin.Role.IsApproval = true;
        }
        #endregion

        public IActionResult OnGet()
        {
            CheckAuth();
            if (UserLogin.Role.IsRead)
            {
                return Page();
            }
            else
                return LocalRedirect("~/Warning?id=403");

        }
        public IActionResult OnPostSearch([FromBody] ItemRequest request)
        {
            CheckAuth();
            string zMessageLog = "";
            DataTable zDataUser = Models.AccessData.User_Search(200, request.SearchContent, out zMessageLog);
            List<ItemView> zList = new List<ItemView>();
            if (zMessageLog.Length > 0)
            {
                ItemView zItem = new ItemView();
                zItem.ID = "ERROR";
                zItem.Name = zMessageLog;
                zList.Add(zItem);
            }
            else
            {
                string btn_Edit = "";
                string btn_Del = "";
                string btn_Approval = "";
                int i = 0;
                foreach (DataRow zRow in zDataUser.Rows)
                {
                    ItemView zItem = new ItemView();
                    zItem.Key = zRow["UserKey"].ToString();
                    zItem.Name = zRow["UserName"].ToString();
                    zItem.EmployeeKey = zRow["EmployeeKey"].ToString();
                    zItem.EmployeeID = zRow["EmployeeID"].ToString();
                    zItem.EmployeeName = zRow["EmployeeName"].ToString();

                    DateTime zDateTime;
                    if (zRow["LastLoginDate"] != null)
                    {
                        zDateTime = (DateTime)zRow["LastLoginDate"];
                        zItem.LastLoginDate = zDateTime.ToString("dd/MM/yyyy HH:mm");
                    }
                    else
                    {
                        zItem.LastLoginDate = "";
                    }
                    zItem.FailedPassword = zRow["FailedPasswordAttemptCount"].ToString();
                    zItem.btnAction = "";
                    if (UserLogin.Role.IsEdit)
                    {
                        btn_Edit = "<button type='button' class='btn btn-primary' onclick=\"Modal_Update_Show(" + i.ToString() + ",'Edit')\">";
                        btn_Edit += "   <i class='uil uil-pen font-size-18'></i>";
                        btn_Edit += "</button>";

                        zItem.btnAction = btn_Edit;
                    }
                    if (UserLogin.Role.IsDelete)
                    {
                        btn_Del = "<button type='button' class='btn btn-primary'  onclick=\"Modal_Update_Show(" + i.ToString() + ",'Del')\">";
                        btn_Del += "    <i class='uil uil-trash-alt font-size-18'></i>";
                        btn_Del += "</button>";
                        zItem.btnAction += btn_Del;
                    }
                    if (UserLogin.Role.IsApproval)
                    {
                        btn_Approval = "<button type='button' class='btn btn-primary'  onclick=\"LV_GotoPage('" + zItem.Key + "')\">";
                        btn_Approval += "    <i class='uil uil-check font-size-18'></i>";
                        btn_Approval += "</button>";

                        zItem.btnAction += btn_Approval;
                    }


                    zList.Add(zItem);
                    i++;
                }
            }
            return new JsonResult(zList);
        }
        public IActionResult OnPostCollectInfo([FromBody] ItemRequest request)
        {
            CheckAuth();
            string zMessageLog = "";
            ItemView zItem = new ItemView();
            if (request.EmployeeID.Length == 0)
            {
                zItem.ID = "ERROR";
                zItem.Name = "Vui lòng nhập Mã nhân viên";
            }
            else
            {
                string[] zInfo = Employee.Models.AccessData.GetEmployeeInfoByID(request.EmployeeID);
                if (zInfo[0] == "NONE")
                {
                    zItem.ID = "ERROR";
                    zItem.Name = "Không có nhân viên này";
                }
                else if (zInfo[0] == "ERROR")
                {
                    zItem.ID = "ERROR";
                    zItem.Name = zInfo[1];
                }
                else
                {
                    zItem.Key = zInfo[0];
                    zItem.ID = zInfo[1];
                    zItem.Name = zInfo[2] + " " + zInfo[3];
                }
            }
            return new JsonResult(zItem);
        }
        public IActionResult OnPostGetInfo([FromBody] ItemRequest request)
        {
            CheckAuth();
            string zMessageLog = "";
            Models.User_Record zUser;
            if (request.UserKey.Length == 36)
                zUser = new Models.User_Record(request.UserKey);
            else
                zUser = new Models.User_Record();

            ItemView zItem = new ItemView();
            zItem.Key = zUser.UserKey;
            zItem.Name = zUser.UserName;
            zItem.EmployeeKey = zUser.Employee_Key;
            zItem.Description = zUser.Description;
            string[] zEmployeeInfo = new string[4];
            if (zUser.Employee_Key.Length == 36)
                zEmployeeInfo = Models.AccessData.GetEmployeeInfoByKey(zUser.Employee_Key);
            zItem.EmployeeID = zEmployeeInfo[1];
            zItem.EmployeeName = zEmployeeInfo[2] + " " + zEmployeeInfo[3];

            return new JsonResult(zItem);
        }
        public IActionResult OnPostUpdate([FromBody] ItemView itemUpdate)
        {
            CheckAuth();
            string zMessageLog = "";

            Models.User_Record zUser = new Models.User_Record();
            zUser.UserKey = itemUpdate.Key;
            zUser.UserName = itemUpdate.Name;
            zUser.Employee_Key = itemUpdate.EmployeeKey;
            zUser.Description = itemUpdate.Description;
            zUser.CreatedBy = UserLogin.Employee.Name;
            zUser.ModifiedBy = UserLogin.Employee.Name;
            zUser.Password = itemUpdate.Password.Trim();

            if (zUser.UserKey.Trim().Length == 36)
            {
                if (UserLogin.Role.IsEdit)
                {
                    zUser.Update_Basic();
                }
                else
                {
                    itemUpdate.ID = "ERROR";
                    itemUpdate.Name = "Bạn không có quyền thay đổi thông tin này";
                }
            }
            else
            {
                if(UserLogin.Role.IsAdd)
                {
                    zUser.Create_ServerKey();
                }
                else
                {
                    itemUpdate.ID = "ERROR";
                    itemUpdate.Name = "Bạn không có quyền thêm thông tin này";
                }
              
            }
            if (itemUpdate.ID != "ERROR")
            {
                itemUpdate.Code = zUser.Code;

                if (zUser.Code == "501")
                {
                    itemUpdate.ID = "ERROR";
                    itemUpdate.Name = zUser.Message;
                }
            }
            return new JsonResult(itemUpdate);
        }
        public IActionResult OnPostDelete([FromBody] ItemRequest request)
        {
            CheckAuth();
            ItemView zItem = new ItemView();
            if (UserLogin.Role.IsDelete)
            {
                string zMessageLog = "";
                Models.User_Record zUser;
                if (request.UserKey.Length == 36)
                {
                    zUser = new Models.User_Record();
                    zUser.UserKey = request.UserKey;
                    zUser.Delete();
                    zItem.Code = zUser.Code;
                    if (zUser.Code != "200")
                    {
                        zItem.ID = "ERROR";
                        zItem.Name = zUser.Message;
                    }
                    else
                    {
                        zItem.ID = "OK";
                    }
                }
                else
                {
                    zItem.ID = "ERROR";
                    zItem.Name = "Key not correct format";
                }
            }
            else
            {
                zItem.ID = "ERROR";
                zItem.Name = "Bạn không có quyền xóa!";
            }
            return new JsonResult(zItem);
        }
        public class ItemRequest
        {
            public string SearchContent { get; set; }
            public string UserKey { get; set; }
            public string EmployeeKey { get; set; }
            public string EmployeeID { get; set; }
        }
        public class ItemView
        {
            public string Key { get; set; }
            public string ID { get; set; }
            public string Name { get; set; }
            public string Password { get; set; }
            public string EmployeeKey { get; set; }
            public string EmployeeName { get; set; }
            public string EmployeeID { get; set; }
            public string Description { get; set; }
            public string LastLoginDate { get; set; }
            public string FailedPassword { get; set; }
            public string btnAction { get; set; }
            public string Code { get; set; }
            public ItemView() { }

        }
    }
}
