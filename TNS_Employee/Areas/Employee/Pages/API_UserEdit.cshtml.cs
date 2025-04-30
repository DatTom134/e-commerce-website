using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Data;


namespace TNS_Employee.Areas.Employee.Pages
{
    public class API_UserEditModel : PageModel
    {
        private UserLogin_Cookie _UserLogin;

        public async Task<ActionResult> OnGet(string action, string userKey, string userName, string password, string employeeID)
        {
            CheckAuth();
            JsonResult zResult = new JsonResult("");
            if (!_UserLogin.RoleItem.IsRead || !_UserLogin.RoleItem.IsEdit)
            {
                ResultModel zResultReturn = new ResultModel();
                zResultReturn.Message = "Bạn không có quyền truy cập";
                zResult = new JsonResult(zResultReturn);
            }
            else
            {
                if (action == "Update")
                {
                    if (!string.IsNullOrEmpty(userKey) && !string.IsNullOrEmpty(userName))
                    {
                        if (userKey.Length == 36)
                        {
                            TNS.Auth.User_Record zRecord = new TNS.Auth.User_Record();
                            zRecord.UserKey = userKey;
                            zRecord.UserName = userName;
                            zRecord.EmployeeID = employeeID;
                            if (password.Length != 28)
                            {
                                zRecord.Password = TNS.Auth.MyCryptography.HashPass(password);
                                zRecord.IsPasswordChange = true;
                            }
                            else
                                zRecord.IsPasswordChange = false;
                            if (employeeID == null)
                            {
                                zRecord.Employee_Key = "";
                                zRecord.EmployeeName = "";
                            }
                            else
                            {
                                zRecord.GetEmployeeKey();
                            }
                            zRecord.ModifiedBy = _UserLogin.UserKey;
                            string zMessage = zRecord.Update_Short();
                            if (zMessage == "OK")
                            {
                                ResultModel zResultReturn = new ResultModel();
                                zResultReturn.Message = zMessage;
                                zResultReturn.Value = zRecord.EmployeeName;
                                zResult = new JsonResult(zResultReturn);
                            }

                        }
                    }
                }
                if (action == "Create")
                {
                    if (!string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(password))
                    {
                        TNS.Auth.User_Record zRecord = new TNS.Auth.User_Record();
                        zRecord.UserKey = userKey;
                        zRecord.UserName = userName;
                        zRecord.EmployeeID = employeeID;

                        zRecord.Password = TNS.Auth.MyCryptography.HashPass(password);
                        if (employeeID == null)
                        {
                            zRecord.Employee_Key = "";
                            zRecord.EmployeeName = "";
                        }
                        else
                        {
                            zRecord.GetEmployeeKey();
                        }
                        zRecord.CreatedBy = _UserLogin.UserKey;

                        string zMessage = zRecord.Create_Short();
                        if (zMessage == "OK")
                        {
                            ResultModel zResultReturn = new ResultModel();
                            zResultReturn.Message = zMessage;
                            zResultReturn.Value = zRecord.EmployeeName;
                            zResult = new JsonResult(zResultReturn);
                        }

                    }
                }
            }
            return zResult;

        }
        private void CheckAuth()
        {
            _UserLogin = new UserLogin_Cookie(User);
            _UserLogin.GetRole("SYS_USE");
        }

        private class ResultModel
        {
            public string Message { get; set; }

            public string Value { get; set; }
        }

    }


}
