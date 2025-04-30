using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Rewrite;

namespace TNS_Employee.Areas.Employee.Models
{
    public class UserRole_Record
    {
        public string RoleKey { get; set; }
        public string RoleID { get; set; }
        public string RoleName { get; set; }
        public string UserKey { get; set; }
        public bool IsRead { get; set; }
        public bool IsAdd { get; set; }
        public bool IsEdit { get; set; }
        public bool IsDelete { get; set; }
        public bool IsApproval { get; set; }
        public string Message { get; set; }
        public string Code
        {
            get { return Message.Substring(0, 3); }
            set { }
        }
        public UserRole_Record() { }
        public UserRole_Record(string userKey, string roleKey)
        {
            string zSQL = "SELECT A.*,B.RoleName FROM [dbo].[SYS_Users_Roles] A "
                        + "LEFT JOIN [dbo].[SYS_Roles] B ON A.RoleKey = B.RoleKey "
                        + "WHERE B.RoleKey = @RoleKey AND B.UserKey = @UserKey "
                        + "ORDER BY B.Rank";
            string zConnectionString = TNS.DBConnection.Connecting.SQL_MainDatabase;
            SqlConnection zConnect = new SqlConnection(zConnectionString);
            zConnect.Open();
            try
            {
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.CommandType = CommandType.Text;
                zCommand.Parameters.Add("@RoleKey", SqlDbType.UniqueIdentifier).Value = new Guid(roleKey);
                zCommand.Parameters.Add("@UserKey", SqlDbType.UniqueIdentifier).Value = new Guid(userKey);
                SqlDataReader zReader = zCommand.ExecuteReader();
                if (zReader.HasRows)
                {
                    zReader.Read();
                    RoleKey = zReader["RoleKey"].ToString();
                    RoleName = zReader["RoleName"].ToString();
                    RoleID = zReader["RoleID"].ToString();
                    UserKey = zReader["UserKey"].ToString();
                    if (zReader["RoleRead"] == null)
                        IsRead = false;
                    else
                        IsRead = (bool)zReader["RoleRead"];

                    if (zReader["RoleAdd"] == null)
                        IsAdd = false;
                    else
                        IsAdd = (bool)zReader["RoleAdd"];

                    if (zReader["RoleEdit"] == null)
                        IsEdit = false;
                    else
                        IsEdit = (bool)zReader["RoleEdit"];

                    if (zReader["RoleDel"] == null)
                        IsDelete = false;
                    else
                        IsDelete = (bool)zReader["RoleDel"];

                    if (zReader["RoleApproval"] == null)
                        IsApproval = false;
                    else
                        IsApproval = (bool)zReader["RoleApproval"];
                    Message = "200 OK";

                }
                else
                {
                    Message = "404 Not Found";
                }
                zReader.Close();
                zCommand.Dispose();
            }
            catch (Exception Err)
            {
                Message = "501 " + Err.ToString();
            }
            finally
            {
                zConnect.Close();
            }

        }

        public string Create()
        {
            //---------- String SQL Access Database ---------------
            string zSQL = "INSERT INTO [dbo].[SYS_Users_Roles] ( "
            + "UserKey,RoleKey,RoleRead,RoleAdd,RoleEdit,RoleDel,RoleApproval) "
            + "VALUES ( "
            + "@UserKey,@RoleKey,@RoleRead,@RoleAdd,@RoleEdit,@RoleDel,@RoleApproval ) ";
            string zResult = "";
            string zConnectionString = TNS.DBConnection.Connecting.SQL_MainDatabase;
            SqlConnection zConnect = new SqlConnection(zConnectionString);
            zConnect.Open();
            try
            {
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.CommandType = CommandType.Text;

                zCommand.Parameters.Add("@UserKey", SqlDbType.UniqueIdentifier).Value = new Guid(UserKey);
                zCommand.Parameters.Add("@RoleKey", SqlDbType.UniqueIdentifier).Value = new Guid(RoleKey);
                zCommand.Parameters.Add("@RoleRead", SqlDbType.Bit).Value = IsRead;
                zCommand.Parameters.Add("@RoleAdd", SqlDbType.Bit).Value = IsAdd;
                zCommand.Parameters.Add("@RoleEdit", SqlDbType.Bit).Value = IsEdit;
                zCommand.Parameters.Add("@RoleDel", SqlDbType.Bit).Value = IsDelete;
                zCommand.Parameters.Add("@RoleApproval", SqlDbType.Bit).Value = IsApproval;
                zResult = zCommand.ExecuteNonQuery().ToString();
                zCommand.Dispose();
                Message = "201 Created";
            }
            catch (Exception Err)
            {
                Message = "501 " + Err.ToString();
            }
            finally
            {
                zConnect.Close();
            }
            return zResult;
        }

        public string Create(string Action)
        {
            string zResult = "";
            switch (Action)
            {
                case "Read":
                    IsRead = true;
                    break;
                case "Add":
                    IsAdd = true;
                    break;
                case "Edit":
                    IsEdit = true;
                    break;
                case "Del":
                    IsDelete = true; ;
                    break;
                case "Approval":
                    IsApproval = true;
                    break;
            }
            zResult = Create();
            return zResult;
        }
        public string Update()
        {
            string zSQL = "UPDATE [dbo].[SYS_Users_Roles] SET "
                        + "RoleRead = @RoleRead,"
                        + "RoleAdd = @RoleAdd,"
                        + "RoleEdit = @RoleEdit,"
                        + "RoleDel = @RoleDel,"
                        + "RoleApproval = @RoleApproval "
                       + " WHERE UserKey = @UserKey AND RoleKey = @RoleKey";
            string zResult = "";
            string zConnectionString = TNS.DBConnection.Connecting.SQL_MainDatabase;
            SqlConnection zConnect = new SqlConnection(zConnectionString);
            zConnect.Open();
            try
            {
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.CommandType = CommandType.Text;
                zCommand.Parameters.Add("@UserKey", SqlDbType.UniqueIdentifier).Value = new Guid(UserKey);
                zCommand.Parameters.Add("@RoleKey", SqlDbType.UniqueIdentifier).Value = new Guid(RoleKey);
                zCommand.Parameters.Add("@RoleRead", SqlDbType.Bit).Value = IsRead;
                zCommand.Parameters.Add("@RoleAdd", SqlDbType.Bit).Value = IsAdd;
                zCommand.Parameters.Add("@RoleEdit", SqlDbType.Bit).Value = IsEdit;
                zCommand.Parameters.Add("@RoleDel", SqlDbType.Bit).Value = IsDelete;
                zCommand.Parameters.Add("@RoleApproval", SqlDbType.Bit).Value = IsApproval;
                zResult = zCommand.ExecuteNonQuery().ToString();
                zCommand.Dispose();
                Message = "200 OK";
            }
            catch (Exception Err)
            {
                Message = "501 " + Err.ToString();
            }
            finally
            {
                zConnect.Close();
            }
            return zResult;
        }

        public string Update(string Action)
        {
            string zResult = "";
            switch (Action)
            {
                case "Read":
                    zResult = Update_Read();
                    break;
                case "Add":
                    zResult = Update_Add();
                    break;
                case "Edit":
                    zResult = Update_Edit();
                    break;
                case "Del":
                    zResult = Update_Del();
                    break;
                case "Approval":
                    zResult = Update_Approval();
                    break;
            }
            return zResult;
        }
        public string Update_Read()
        {
            string zSQL = "UPDATE [dbo].[SYS_Users_Roles] SET "
                        + "RoleRead = ~RoleRead "
                        + "WHERE UserKey = @UserKey AND RoleKey = @RoleKey";
            string zResult = "";
            string zConnectionString = TNS.DBConnection.Connecting.SQL_MainDatabase;
            SqlConnection zConnect = new SqlConnection(zConnectionString);
            zConnect.Open();
            try
            {
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.CommandType = CommandType.Text;
                zCommand.Parameters.Add("@UserKey", SqlDbType.UniqueIdentifier).Value = new Guid(UserKey);
                zCommand.Parameters.Add("@RoleKey", SqlDbType.UniqueIdentifier).Value = new Guid(RoleKey);
                zResult = zCommand.ExecuteNonQuery().ToString();
                zCommand.Dispose();
                Message = "200 OK";
            }
            catch (Exception Err)
            {
                Message = "501 " + Err.ToString();
            }
            finally
            {
                zConnect.Close();
            }
            return zResult;
        }
        public string Update_Add()
        {
            string zSQL = "UPDATE [dbo].[SYS_Users_Roles] SET "
                        + "RoleAdd = ~RoleAdd "
                        + "WHERE UserKey = @UserKey AND RoleKey = @RoleKey";
            string zResult = "";
            string zConnectionString = TNS.DBConnection.Connecting.SQL_MainDatabase;
            SqlConnection zConnect = new SqlConnection(zConnectionString);
            zConnect.Open();
            try
            {
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.CommandType = CommandType.Text;
                zCommand.Parameters.Add("@UserKey", SqlDbType.UniqueIdentifier).Value = new Guid(UserKey);
                zCommand.Parameters.Add("@RoleKey", SqlDbType.UniqueIdentifier).Value = new Guid(RoleKey);
                zResult = zCommand.ExecuteNonQuery().ToString();
                zCommand.Dispose();
                Message = "200 OK";
            }
            catch (Exception Err)
            {
                Message = "501 " + Err.ToString();
            }
            finally
            {
                zConnect.Close();
            }
            return zResult;
        }
        public string Update_Edit()
        {
            string zSQL = "UPDATE [dbo].[SYS_Users_Roles] SET "
                        + "RoleEdit = ~RoleEdit "
                        + "WHERE UserKey = @UserKey AND RoleKey = @RoleKey";
            string zResult = "";
            string zConnectionString = TNS.DBConnection.Connecting.SQL_MainDatabase;
            SqlConnection zConnect = new SqlConnection(zConnectionString);
            zConnect.Open();
            try
            {
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.CommandType = CommandType.Text;
                zCommand.Parameters.Add("@UserKey", SqlDbType.UniqueIdentifier).Value = new Guid(UserKey);
                zCommand.Parameters.Add("@RoleKey", SqlDbType.UniqueIdentifier).Value = new Guid(RoleKey);
                zResult = zCommand.ExecuteNonQuery().ToString();
                zCommand.Dispose();
                Message = "200 OK";
            }
            catch (Exception Err)
            {
                Message = "501 " + Err.ToString();
            }
            finally
            {
                zConnect.Close();
            }
            return zResult;
        }
        public string Update_Del()
        {
            string zSQL = "UPDATE [dbo].[SYS_Users_Roles] SET "
                        + "RoleDel = ~RoleDel "
                        + "WHERE UserKey = @UserKey AND RoleKey = @RoleKey";
            string zResult = "";
            string zConnectionString = TNS.DBConnection.Connecting.SQL_MainDatabase;
            SqlConnection zConnect = new SqlConnection(zConnectionString);
            zConnect.Open();
            try
            {
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.CommandType = CommandType.Text;
                zCommand.Parameters.Add("@UserKey", SqlDbType.UniqueIdentifier).Value = new Guid(UserKey);
                zCommand.Parameters.Add("@RoleKey", SqlDbType.UniqueIdentifier).Value = new Guid(RoleKey);
                zResult = zCommand.ExecuteNonQuery().ToString();
                zCommand.Dispose();
                Message = "200 OK";
            }
            catch (Exception Err)
            {
                Message = "501 " + Err.ToString();
            }
            finally
            {
                zConnect.Close();
            }
            return zResult;
        }
        public string Update_Approval()
        {
            string zSQL = "UPDATE [dbo].[SYS_Users_Roles] SET "
                        + "RoleApproval = ~RoleApproval "
                        + "WHERE UserKey = @UserKey AND RoleKey = @RoleKey";
            string zResult = "";
            string zConnectionString = TNS.DBConnection.Connecting.SQL_MainDatabase;
            SqlConnection zConnect = new SqlConnection(zConnectionString);
            zConnect.Open();
            try
            {
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.CommandType = CommandType.Text;
                zCommand.Parameters.Add("@UserKey", SqlDbType.UniqueIdentifier).Value = new Guid(UserKey);
                zCommand.Parameters.Add("@RoleKey", SqlDbType.UniqueIdentifier).Value = new Guid(RoleKey);
                zResult = zCommand.ExecuteNonQuery().ToString();
                zCommand.Dispose();
                Message = "200 OK";
            }
            catch (Exception Err)
            {
                Message = "501 " + Err.ToString();
            }
            finally
            {
                zConnect.Close();
            }
            return zResult;
        }
        public bool IsRecordExist()
        {
            bool zResult = false;
            string zSQL = "SELECT Count(*) FROM [dbo].[SYS_Users_Roles]  "
                      + "WHERE RoleKey = @RoleKey AND UserKey = @UserKey ";
            string zConnectionString = TNS.DBConnection.Connecting.SQL_MainDatabase;
            SqlConnection zConnect = new SqlConnection(zConnectionString);
            zConnect.Open();
            try
            {
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.CommandType = CommandType.Text;
                zCommand.Parameters.Add("@RoleKey", SqlDbType.UniqueIdentifier).Value = new Guid(RoleKey);
                zCommand.Parameters.Add("@UserKey", SqlDbType.UniqueIdentifier).Value = new Guid(UserKey);
                string zReturn = zCommand.ExecuteScalar().ToString();
                int zAmount = 0;
                if (int.TryParse(zReturn, out zAmount))
                {
                    if (zAmount > 0)
                        zResult = true;
                }

                zCommand.Dispose();
            }
            catch (Exception Err)
            {
                Message = "501 " + Err.ToString();
            }
            finally
            {
                zConnect.Close();
            }
            return zResult;
        }

    }
}
