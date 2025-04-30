using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Data;
using System.Security.Claims;
using System.Xml.Linq;

namespace TNS_Employee.Areas.Employee.Pages
{
    [IgnoreAntiforgeryToken]
    public class EmployeeListModel : PageModel
    {
        #region [ Security ]
        public TNS.Auth.UserLogin_Info UserLogin;
        private void CheckAuth()
        {
            UserLogin = new TNS.Auth.UserLogin_Info(User);
            UserLogin.GetRole("Employee-EmployeeList");
        }
        #endregion

        public List<string[]> ListDepartment = Models.AccessData.Departments();
        public List<string[]> ListPosition = Models.AccessData.Positions();
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
            DataTable zDataEmployee = Models.AccessData.Search(200, request.SearchContent, out zMessageLog);
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

                int i = 0;
                foreach (DataRow zRow in zDataEmployee.Rows)
                {
                    ItemView zItem = new ItemView();
                    zItem.Key = zRow["EmployeeKey"].ToString();
                    zItem.ID = zRow["EmployeeID"].ToString();
                    zItem.Name = zRow["LastName"].ToString() + " " + zRow["FirstName"].ToString();
                    zItem.UserName = zRow["UserName"].ToString();
                    zItem.JobName = zRow["JobName"].ToString();
                    zItem.DepartmentName = zRow["DepartmentName"].ToString();
                    zItem.PositionName = zRow["PositionName"].ToString();
                    zItem.ReportToName = zRow["ReportToName"].ToString();
                    zItem.PhotoPath = zRow["PhotoPath"].ToString();
                    if (zItem.PhotoPath.Length == 0)
                        zItem.PhotoPath = "/images/users/avatar-0.jpg";
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
            if (request.ReportToID.Length == 0)
            {
                zItem.ID = "ERROR";
                zItem.Name = "Vui lòng nhập Mã nhân viên";
            }
            else
            {
                string[] zInfo = Employee.Models.AccessData.GetEmployeeInfoByID(request.ReportToID);
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
            Models.Employee_Record zEmployee;
            if (request.EmployeeKey.Length == 36)
                zEmployee = new Models.Employee_Record(request.EmployeeKey);
            else
                zEmployee = new Models.Employee_Record();

            ItemView zItem = new ItemView();
            zItem.Key = zEmployee.EmployeeKey;
            zItem.ID = zEmployee.EmployeeID;
            zItem.FirstName = zEmployee.FirstName;
            zItem.LastName = zEmployee.LastName;
            zItem.DepartmentKey = zEmployee.DepartmentKey;
            zItem.DepartmentName = zEmployee.DepartmentName;
            zItem.PositionKey = zEmployee.PositionKey.ToString();
            zItem.PositionName = zEmployee.PositionName;
            zItem.JobName = zEmployee.JobName;
            zItem.ReportToKey = zEmployee.ReportToKey;
            zItem.ReportToID = zEmployee.ReportToID;
            zItem.ReportToName = zEmployee.ReportToName;

            return new JsonResult(zItem);
        }
        public IActionResult OnPostUpdate([FromBody] ItemView itemUpdate)
        {
            CheckAuth();
            string zMessageLog = "";
            Models.Employee_Record zEmployee = new Models.Employee_Record();
            zEmployee.EmployeeKey = itemUpdate.Key;
            zEmployee.EmployeeID = itemUpdate.ID;
            zEmployee.FirstName = itemUpdate.FirstName;
            zEmployee.LastName = itemUpdate.LastName;
            if (itemUpdate.DepartmentKey == "None")
            {
                zEmployee.DepartmentKey = "";
                zEmployee.DepartmentName = "";
                itemUpdate.DepartmentName = "";
            }
            else
            {
                zEmployee.DepartmentKey = itemUpdate.DepartmentKey;
                zEmployee.DepartmentName = itemUpdate.DepartmentName;
            }

            zEmployee.PositionKey = int.Parse(itemUpdate.PositionKey);
            if (zEmployee.PositionKey > 0)
            {
                zEmployee.PositionName = itemUpdate.PositionName;
            }
            else
            {
                zEmployee.PositionName = "";
                itemUpdate.PositionName = "";
            }
            zEmployee.JobName = itemUpdate.JobName;
            zEmployee.ReportToKey = itemUpdate.ReportToKey;
            zEmployee.ReportToID = itemUpdate.ReportToID;
            zEmployee.ReportToName = itemUpdate.ReportToName;
            zEmployee.ModifiedBy = UserLogin.Employee.Key;
            zEmployee.ModifiedName = UserLogin.Employee.Name;

            itemUpdate.Name = zEmployee.LastName + " " + zEmployee.FirstName;
            if (zEmployee.EmployeeKey.Trim().Length == 36)
            {
                if (UserLogin.Role.IsEdit)
                {
                    zEmployee.Update_Basic();
                }
                else
                {
                    itemUpdate.ID = "ERROR";
                    itemUpdate.Name = "Bạn không có quyền sửa thông tin !";
                }
            }
            else
            {
                if (UserLogin.Role.IsAdd)
                {
                    zEmployee.Create_Basic();
                    itemUpdate.Key = zEmployee.EmployeeKey;
                }
                else
                {
                    itemUpdate.ID = "ERROR";
                    itemUpdate.Name = "Bạn không có quyền thêm thông tin !";
                }
            }
            if (itemUpdate.ID != "ERROR")
            {
                itemUpdate.Code = zEmployee.Code;

                if (zEmployee.Code == "501")
                {
                    itemUpdate.ID = "ERROR";
                    itemUpdate.Name = zEmployee.Message;
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
                
                Models.Employee_Record zEmployee;
                if (request.EmployeeKey.Length == 36)
                {
                    zEmployee = new Models.Employee_Record();
                    zEmployee.EmployeeKey = request.EmployeeKey;
                    zEmployee.Delete();
                    zItem.Code = zEmployee.Code;
                    if (zEmployee.Code != "200")
                    {
                        zItem.ID = "ERROR";
                        zItem.Name = zEmployee.Message;
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
                zItem.Name = "Bạn không có quyền xóa thông tin";
            }
           
            return new JsonResult(zItem);
        }
        public class ItemRequest
        {
            public string SearchContent { get; set; }
            public string EmployeeKey { get; set; }
            public string ReportToID { get; set; }
        }
        public class ItemView
        {
            public string Key { get; set; }
            public string ID { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Name { get; set; }
            public string UserName { get; set; }
            public string PhotoPath { get; set; }
            public string DepartmentKey { get; set; }
            public string DepartmentName { get; set; }
            public string PositionKey { get; set; }
            public string PositionName { get; set; }
            public string JobName { get; set; }
            public string ReportToKey { get; set; }
            public string ReportToID { get; set; }
            public string ReportToName { get; set; }
            public string btnAction { get; set; }
            public string Code { get; set; }
            public ItemView() { }

        }
    }
}
