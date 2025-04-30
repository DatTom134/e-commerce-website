using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TNS_Employee.Areas.Employee.Models
{
    public class User_AccessData
    {
        public static DataTable GetRolesOfUser(string UserKey, out string Message)
        {
            Message = "";
            DataTable zDataRoles = new DataTable();
            DataTable zDataRolesOfUser = new DataTable();
            string zSQL_Role = "SELECT RoleKey,RoleID, RoleName,0 AS RoleRead,0 AS RoleAdd,0 AS RoleEdit,0 AS RoleDel,0 AS RoleApproval FROM [dbo].[SYS_Roles] ORDER BY Rank ";
            string zSQL_RoleOfUser = "SELECT RoleKey, RoleRead,RoleAdd,RoleEdit,RoleDel,RoleApproval "
                                    + "FROM [dbo].[SYS_Users_Roles] WHERE UserKey = @UserKey";
            string zConnectionString = TNS.DBConnection.Connecting.SQL_MainDatabase;
            try
            {
                SqlConnection zConnect = new SqlConnection(zConnectionString);
                zConnect.Open();
                SqlCommand zCommand = new SqlCommand(zSQL_Role, zConnect);
                SqlDataAdapter zAdapter = new SqlDataAdapter(zCommand);
                zAdapter.Fill(zDataRoles);

                zCommand = new SqlCommand(zSQL_RoleOfUser, zConnect);
                zCommand.Parameters.Add("@UserKey", SqlDbType.UniqueIdentifier).Value = new Guid(UserKey);
                zAdapter = new SqlDataAdapter(zCommand);
                zAdapter.Fill(zDataRolesOfUser);

                zCommand.Dispose();
                zConnect.Close();
            }
            catch (Exception ex)
            {
                Message = ex.ToString();
            }
            foreach(DataRow zRow in zDataRoles.Rows)
            {
                foreach(DataRow zRole in zDataRolesOfUser.Rows)
                {
                    if (zRow["RoleKey"].ToString() == zRole["RoleKey"].ToString())
                    {
                        zRow["RoleRead"] = zRole["RoleRead"];
                        zRow["RoleAdd"] = zRole["RoleAdd"];
                        zRow["RoleEdit"] = zRole["RoleEdit"];
                        zRow["RoleDel"] = zRole["RoleDel"];
                        zRow["RoleApproval"] = zRole["RoleApproval"];
                 
                        break;
                    }
                }
            }
            return zDataRoles;
        }
    }
}
