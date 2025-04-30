using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace TNS_Employee.Areas.Employee.Pages
{
    public class ProfileModel : PageModel
    {
        public TNS.Auth.UserLogin_Info UserLogin;
        public List<string[]> ListDepartment = Models.AccessData.Departments();

        [BindProperty]
        public Models.Employee_Record EmployeeLogin { get; set; }

        [BindProperty]
        public ChangePassModel ChangePass { get; set; }

        public string Message { get; set; }
        public string ViewReturn { get; set; }
       
        public IActionResult OnGet(string Key = null)
        {
            CheckAuth();
            ViewReturn = "display:none";
            ChangePass = new ChangePassModel();
            if (Key == null)
            {
                EmployeeLogin = new Models.Employee_Record(UserLogin.Employee.Key);
                return Page();
            }
            else
            {
                if (UserLogin.Role.IsRead)
                {
                    if (Key.Length == 36)
                    {
                        EmployeeLogin = new Models.Employee_Record(Key);
                        ViewReturn = "display:contents";
                        return Page();
                    }
                    else
                    {
                        return LocalRedirect("~/Warning?id=300");
                    }
                }
                else
                {
                    return LocalRedirect("~/Warning?id=405");
                }
            }

        }
        public async Task<ActionResult> OnPost(string action)
        {
            CheckAuth();
           // if (ModelState.IsValid)
            {
                if (action == "UploadImage")
                {
                    if (HttpContext.Request.Form.Files.Count > 0)
                    {
                        var zFile = HttpContext.Request.Form.Files[0];
                        if (zFile != null)
                        {
                            //await _FileUploadService.UploadFileAsync(zFile, "wwwroot\\images\\users\\" + _UserLogin.EmployeeKey);
                            //EmployeeLogin = new TNS.HumanResouce.Employee_Record(_UserLogin.EmployeeKey);
                            EmployeeLogin.PhotoPath = "/images/users/" + UserLogin.Employee.Key + "/" + zFile.FileName;
                            EmployeeLogin.Update_Avatar();
                        }
                    }
                }
                if (action == "PersonalSave")
                {
                    if (EmployeeLogin.MaritalStatusKey == 0)
                        EmployeeLogin.MaritalStatusName = "Độc thân";
                    else if (EmployeeLogin.MaritalStatusKey == 1)
                        EmployeeLogin.MaritalStatusName = "Đã kết hôn";
                    else
                        EmployeeLogin.MaritalStatusName = "Đã li dị";

                    EmployeeLogin.Update_PersonalInfo();


                }
                if (action == "WorkingSave")
                {
                    EmployeeLogin.JobKey = "";
                    EmployeeLogin.ContractTypeKey = 0;
                    EmployeeLogin.PositionKey = 0;
                    if (EmployeeLogin.ReportToID != null)
                    {
                        EmployeeLogin.GetReportTo_KeyName();
                    }
                    else
                    {
                        EmployeeLogin.ReportToKey = "";
                        EmployeeLogin.ReportToName = "";
                    }
                    EmployeeLogin.DepartmentName = GetDepartmentName(EmployeeLogin.DepartmentKey);
                    EmployeeLogin.Update_WorkingInfo();
                }


            }
            if (action == "ChangePass")
            {
                string zOldPass = ChangePass.OldPassword;
                string zNewPass = ChangePass.NewPassword;
                /*TNS.Auth.User_Record zRecord = new TNS.Auth.User_Record(_UserLogin.UserKey);
                string zResult = zRecord.Update_Password(zOldPass, zNewPass);
                if (zResult.Substring(0, 3) == "ERR")
                    Message = zResult;
                else
                    Message = "Thay đổi mật khẩu thành công !";
                */
            }

            EmployeeLogin = new Models.Employee_Record(EmployeeLogin.EmployeeKey);
            return Page();
        }
        private string GetDepartmentName(string DepartmentKey)
        {
            foreach (string[] zItem in ListDepartment)
            {
                if (zItem[0] == DepartmentKey)
                {
                    return zItem[1];
                }
            }
            return "";
        }
        private void CheckAuth()
        {
            UserLogin = new TNS.Auth.UserLogin_Info(User);
            UserLogin.GetRole("HRM002");
        }

        public class ChangePassModel
        {
            [Required]
            [DataType(DataType.Password)]
            public string OldPassword { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string NewPassword { get; set; }

            public ChangePassModel()
            {

            }
        }
    }
}
