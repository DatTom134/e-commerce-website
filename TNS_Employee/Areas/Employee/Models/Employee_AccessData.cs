using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace TNS_Employee.Areas.Employee.Models
{
    public class AccessData
    {
        public static DataTable List(int Amount)
        {
            DataTable zTable = new DataTable();
            string zSQL = "SELECT TOP " + Amount + " * FROM [dbo].[HRM_Employee] "
                        + "WHERE RecordStatus != 99 "
                        + "ORDER BY DepartmentName, FirstName  ";
            string zConnectionString = TNS.DBConnection.Connecting.SQL_MainDatabase;
            try
            {
                SqlConnection zConnect = new SqlConnection(zConnectionString);
                zConnect.Open();
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                SqlDataAdapter zAdapter = new SqlDataAdapter(zCommand);
                zAdapter.Fill(zTable);
                zCommand.Dispose();
                zConnect.Close();
            }
            catch (Exception ex)
            {
                string zstrMessage = ex.ToString();
            }
            return zTable;
        }
        public static DataTable Search(int Amount, string Content, out string Message)
        {
            Message = "";
            Content = Content.Trim();
            DataTable zTable = new DataTable();
            string zSQL = "SELECT TOP " + Amount + " B.UserName,A.EmployeeKey,A.EmployeeID,A.FirstName,A.LastName, A.PhotoPath,JobName,DepartmentName,PositionName,ReportToName "
                + "FROM [dbo].[HRM_Employee] A "
                + "LEFT JOIN [dbo].[SYS_Users] B ON A.EmployeeKey = B.Employee_Key "
                + "WHERE A.RecordStatus != 99 ";
            if (Content.Length > 0)
            {
                zSQL += "AND (EmployeeID LIKE @Content OR "
                        + "UserIPCAST LIKE @Content OR "
                        + "(LastName + ' ' + FirstName) LIKE @Content OR "
                        + "DepartmentName LIKE @Content OR "
                        + "ReportToName LIKE @Content OR "
                        + "PositionName LIKE @Content) ";
            }
            zSQL += "ORDER BY DepartmentName, FirstName ";
            string zConnectionString = TNS.DBConnection.Connecting.SQL_MainDatabase;
            try
            {
                SqlConnection zConnect = new SqlConnection(zConnectionString);
                zConnect.Open();
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.Parameters.Add("@Content", SqlDbType.NVarChar).Value = "%" + Content + "%";
                SqlDataAdapter zAdapter = new SqlDataAdapter(zCommand);
                zAdapter.Fill(zTable);
                zCommand.Dispose();
                zConnect.Close();
            }
            catch (Exception ex)
            {
                Message = ex.ToString();
            }
            return zTable;
        }
        public static List<string[]> Departments()
        {
            DataTable zTable = new DataTable();
            string zSQL = "SELECT DepartmentKey,DepartmentName FROM [dbo].[HRM_Department] "
                        + "WHERE Class = 'DP' "
                        + "ORDER BY Rank DESC";
            string zConnectionString = TNS.DBConnection.Connecting.SQL_MainDatabase;
            try
            {
                SqlConnection zConnect = new SqlConnection(zConnectionString);
                zConnect.Open();
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                SqlDataAdapter zAdapter = new SqlDataAdapter(zCommand);
                zAdapter.Fill(zTable);
                zCommand.Dispose();
                zConnect.Close();
            }
            catch (Exception ex)
            {
                string zstrMessage = ex.ToString();
            }
            List<string[]> zDepartment = new List<string[]>();
            foreach (DataRow zRow in zTable.Rows)
            {
                string[] zItem = new string[2];
                zItem[0] = zRow["DepartmentKey"].ToString();
                zItem[1] = zRow["DepartmentName"].ToString();
                zDepartment.Add(zItem);
            }
            return zDepartment;
        }
        public static List<string[]> Positions()
        {
            DataTable zTable = new DataTable();
            string zSQL = "SELECT PositionKey,PositionName FROM [dbo].[HRM_Position] "
                        + "ORDER BY Ranking";
            string zConnectionString = TNS.DBConnection.Connecting.SQL_MainDatabase;
            try
            {
                SqlConnection zConnect = new SqlConnection(zConnectionString);
                zConnect.Open();
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                SqlDataAdapter zAdapter = new SqlDataAdapter(zCommand);
                zAdapter.Fill(zTable);
                zCommand.Dispose();
                zConnect.Close();
            }
            catch (Exception ex)
            {
                string zstrMessage = ex.ToString();
            }
            List<string[]> zDepartment = new List<string[]>();
            foreach (DataRow zRow in zTable.Rows)
            {
                string[] zItem = new string[2];
                zItem[0] = zRow["PositionKey"].ToString();
                zItem[1] = zRow["PositionName"].ToString();
                zDepartment.Add(zItem);
            }
            return zDepartment;
        }
        public static string[] GetEmployeeInfoByID(string EmployeeID)
        {
            DataTable zTable = new DataTable();
            string zMessage = "";
            string zSQL = "SELECT EmployeeKey,EmployeeID,LastName,FirstName FROM [dbo].[HRM_Employee] WHERE EmployeeID = @EmployeeID AND RecordStatus != 99 ";
            string zConnectionString = TNS.DBConnection.Connecting.SQL_MainDatabase;
            try
            {
                SqlConnection zConnect = new SqlConnection(zConnectionString);
                zConnect.Open();
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.Parameters.Add("@EmployeeID", SqlDbType.NVarChar).Value = EmployeeID;
                SqlDataAdapter zAdapter = new SqlDataAdapter(zCommand);
                zAdapter.Fill(zTable);
                zCommand.Dispose();
                zConnect.Close();
            }
            catch (Exception ex)
            {
                zMessage = ex.ToString();
            }
            string[] zResult = new string[4];
            if (zTable.Rows.Count > 0)
            {
                zResult[0] = zTable.Rows[0]["EmployeeKey"].ToString();
                zResult[1] = zTable.Rows[0]["EmployeeID"].ToString();
                zResult[2] = zTable.Rows[0]["LastName"].ToString();
                zResult[3] = zTable.Rows[0]["FirstName"].ToString();
            }
            else
            {
                if(zMessage.Length > 0)
                {
                    zResult[0] = "ERROR";
                    zResult[1] = zMessage;
                }
                else
                {
                    zResult[0] = "NONE";
                }
            }
            return zResult;
        }
        public static string[] GetEmployeeInfoByKey(string EmployeeKey)
        {
            DataTable zTable = new DataTable();
            string zMessage = "";
            string zSQL = "SELECT EmployeeKey,EmployeeID,LastName,FirstName FROM [dbo].[HRM_Employee] WHERE EmployeeKey = @EmployeeKey AND RecordStatus != 99 ";
            string zConnectionString = TNS.DBConnection.Connecting.SQL_MainDatabase;
            try
            {
                SqlConnection zConnect = new SqlConnection(zConnectionString);
                zConnect.Open();
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.Parameters.Add("@EmployeeKey", SqlDbType.UniqueIdentifier).Value = new Guid(EmployeeKey);
                SqlDataAdapter zAdapter = new SqlDataAdapter(zCommand);
                zAdapter.Fill(zTable);
                zCommand.Dispose();
                zConnect.Close();
            }
            catch (Exception ex)
            {
                zMessage = ex.ToString();
            }
            string[] zResult = new string[4];
            if (zTable.Rows.Count > 0)
            {
                zResult[0] = zTable.Rows[0]["EmployeeKey"].ToString();
                zResult[1] = zTable.Rows[0]["EmployeeID"].ToString();
                zResult[2] = zTable.Rows[0]["LastName"].ToString();
                zResult[3] = zTable.Rows[0]["FirstName"].ToString();
            }
            else
            {
                if (zMessage.Length > 0)
                {
                    zResult[0] = "ERROR";
                    zResult[1] = zMessage;
                }
                else
                {
                    zResult[0] = "NONE";
                }
            }
            return zResult;
        }
        public static DataTable User_Search(int Amount, string Content, out string Message)
        {
            Message = "";
            Content = Content.Trim();
            DataTable zTable = new DataTable();
            string zSQL = "SELECT TOP " + Amount + " A.UserKey,A.UserName,A.LastLoginDate,A.FailedPasswordAttemptCount, "
                        + "B.EmployeeKey, B.EmployeeID,B.LastName + ' ' + B.FirstName AS EmployeeName FROM [dbo].[SYS_Users] A "
                        + "LEFT JOIN [dbo].[HRM_Employee] B ON A.Employee_Key = B.EmployeeKey "
                        + "WHERE A.RecordStatus != 99 "
                        + "";
            if (Content.Length > 0)
            {
                zSQL += "AND (A.UserName LIKE @Content OR (B.LastName + ' ' + B.FirstName) LIKE @Content) ";
            }
            zSQL += "ORDER BY A.UserName, B.FirstName ";
            string zConnectionString = TNS.DBConnection.Connecting.SQL_MainDatabase;
            try
            {
                SqlConnection zConnect = new SqlConnection(zConnectionString);
                zConnect.Open();
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.Parameters.Add("@Content", SqlDbType.NVarChar).Value = "%" + Content + "%";
                SqlDataAdapter zAdapter = new SqlDataAdapter(zCommand);
                zAdapter.Fill(zTable);
                zCommand.Dispose();
                zConnect.Close();
            }
            catch (Exception ex)
            {
                Message = ex.ToString();
            }
            return zTable;
        }
    }
}