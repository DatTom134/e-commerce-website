using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TNS.Auth;

namespace TNS_Employee.Areas.Employee.Models
{
    public class User_Record
    {

        #region [ Field Name ]
        private string _UserKey = "";
        private string _UserName = "";
        private string _Password = "";
        private string _Description = "";
        private bool _Activate;
        private DateTime? _ExpireDate = null;
        private DateTime? _LastLoginDate = null;
        private int _FailedPasswordAttemptCount = 0;
        private int _EmployeeKey = 0;
        private string _Employee_Key = "";
        private string _CreatedBy = "";
        private DateTime? _CreatedDateTime = null;
        private string _ModifiedBy = "";
        private DateTime? _ModifiedDateTime = null;
        private string _Message = "";
        #endregion

        #region [ Constructor Get Information ]
        public User_Record()
        {

        }
        public User_Record(string UserKey)
        {
            string zSQL = "SELECT * FROM [dbo].[SYS_Users] WHERE UserKey = @UserKey AND RecordStatus != 99 ";
            string zConnectionString = TNS.DBConnection.Connecting.SQL_MainDatabase;
            SqlConnection zConnect = new SqlConnection(zConnectionString);
            zConnect.Open();
            try
            {
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.CommandType = CommandType.Text;
                zCommand.Parameters.Add("@UserKey", SqlDbType.UniqueIdentifier).Value = new Guid(UserKey);
                SqlDataReader zReader = zCommand.ExecuteReader();
                if (zReader.HasRows)
                {
                    zReader.Read();
                    _UserKey = zReader["UserKey"].ToString();
                    _UserName = zReader["UserName"].ToString();
                    _Password = zReader["Password"].ToString();
                    _Description = zReader["Description"].ToString();
                    _Activate = (bool)zReader["Activate"];
                    if (zReader["ExpireDate"] != DBNull.Value)
                        _ExpireDate = (DateTime)zReader["ExpireDate"];
                    if (zReader["LastLoginDate"] != DBNull.Value)
                        _LastLoginDate = (DateTime)zReader["LastLoginDate"];
                    _FailedPasswordAttemptCount = int.Parse(zReader["FailedPasswordAttemptCount"].ToString());
                    _EmployeeKey = int.Parse(zReader["EmployeeKey"].ToString());

                    _Employee_Key = zReader["Employee_Key"].ToString();
                    _CreatedBy = zReader["CreatedBy"].ToString();
                    if (zReader["CreatedDateTime"] != DBNull.Value)
                        _CreatedDateTime = (DateTime)zReader["CreatedDateTime"];
                    _ModifiedBy = zReader["ModifiedBy"].ToString();
                    if (zReader["ModifiedDateTime"] != DBNull.Value)
                        _ModifiedDateTime = (DateTime)zReader["ModifiedDateTime"];
                    _Message = "200 OK";
                }
                else
                {
                    _Message = "404 Not Found";
                }
                zReader.Close();
                zCommand.Dispose();
            }
            catch (Exception Err)
            {
                _Message = "501 " + Err.ToString();
            }
            finally
            {
                zConnect.Close();
            }
        }

        #endregion

        #region [ Properties ]
        public string UserKey
        {
            get { return _UserKey; }
            set { _UserKey = value; }
        }
        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }
        public string Password
        {
            get { return _Password; }
            set { _Password = value; }
        }
        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }
        public bool Activate
        {
            get { return _Activate; }
            set { _Activate = value; }
        }
        public DateTime? ExpireDate
        {
            get { return _ExpireDate; }
            set { _ExpireDate = value; }
        }
        public DateTime? LastLoginDate
        {
            get { return _LastLoginDate; }
            set { _LastLoginDate = value; }
        }
        public int FailedPasswordAttemptCount
        {
            get { return _FailedPasswordAttemptCount; }
            set { _FailedPasswordAttemptCount = value; }
        }
        public int EmployeeKey
        {
            get { return _EmployeeKey; }
            set { _EmployeeKey = value; }
        }
        public string Employee_Key
        {
            get { return _Employee_Key; }
            set { _Employee_Key = value; }
        }
        public string CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; }
        }
        public DateTime? CreatedDateTime
        {
            get { return _CreatedDateTime; }
            set { _CreatedDateTime = value; }
        }
        public string ModifiedBy
        {
            get { return _ModifiedBy; }
            set { _ModifiedBy = value; }
        }
        public DateTime? ModifiedDateTime
        {
            get { return _ModifiedDateTime; }
            set { _ModifiedDateTime = value; }
        }
        public string Code
        {
            get
            {
                if (_Message.Length >= 3)
                    return _Message.Substring(0, 3);
                else return "";
            }
        }
        public string Message
        {
            get { return _Message; }
            set { _Message = value; }
        }
        #endregion

        #region [ Constructor Update Information ]

        public string Create_ServerKey()
        {
            //---------- String SQL Access Database ---------------
            string zSQL = "INSERT INTO [dbo].[SYS_Users] ("
        + " UserName ,Password ,Description ,Activate ,ExpireDate ,LastLoginDate ,FailedPasswordAttemptCount ,EmployeeKey ,Employee_Key ,CreatedBy ,CreatedDateTime ,ModifiedBy ,ModifiedDateTime ) "
         + " VALUES ( "
         + "@UserName ,@Password ,@Description ,@Activate ,@ExpireDate ,@LastLoginDate ,@FailedPasswordAttemptCount ,@EmployeeKey ,@Employee_Key ,@CreatedBy ,GetDate() ,@ModifiedBy ,GetDate() ) ";
            string zResult = "";
            string zConnectionString = TNS.DBConnection.Connecting.SQL_MainDatabase;
            SqlConnection zConnect = new SqlConnection(zConnectionString);
            zConnect.Open();
            try
            {
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.CommandType = CommandType.Text;
                zCommand.Parameters.Add("@UserName", SqlDbType.NVarChar).Value = _UserName;
                zCommand.Parameters.Add("@Password", SqlDbType.NVarChar).Value = MyCryptography.HashPass(_Password);
                zCommand.Parameters.Add("@Description", SqlDbType.NText).Value = _Description;
                zCommand.Parameters.Add("@Activate", SqlDbType.Bit).Value = _Activate;
                if (_ExpireDate == null)
                    zCommand.Parameters.Add("@ExpireDate", SqlDbType.DateTime).Value = DBNull.Value;
                else
                    zCommand.Parameters.Add("@ExpireDate", SqlDbType.DateTime).Value = _ExpireDate;
                if (_LastLoginDate == null)
                    zCommand.Parameters.Add("@LastLoginDate", SqlDbType.DateTime).Value = DateTime.Now;
                else
                    zCommand.Parameters.Add("@LastLoginDate", SqlDbType.DateTime).Value = _LastLoginDate;
                zCommand.Parameters.Add("@FailedPasswordAttemptCount", SqlDbType.Int).Value = _FailedPasswordAttemptCount;
                zCommand.Parameters.Add("@EmployeeKey", SqlDbType.Int).Value = _EmployeeKey;
                if (_Employee_Key.Length == 36)
                    zCommand.Parameters.Add("@Employee_Key", SqlDbType.UniqueIdentifier).Value = new Guid(_Employee_Key);
                else
                    zCommand.Parameters.Add("@Employee_Key", SqlDbType.UniqueIdentifier).Value = DBNull.Value;
                zCommand.Parameters.Add("@CreatedBy", SqlDbType.NVarChar).Value = _CreatedBy;
                zCommand.Parameters.Add("@ModifiedBy", SqlDbType.NVarChar).Value = _ModifiedBy;
                zResult = zCommand.ExecuteNonQuery().ToString();
                zCommand.Dispose();
                _Message = "201 Created";
            }
            catch (Exception Err)
            {
                _Message = "501 " + Err.ToString();
            }
            finally
            {
                zConnect.Close();
            }
            return zResult;
        }
        public string Create_ClientKey()
        {
            //---------- String SQL Access Database ---------------
            string zSQL = "INSERT INTO [dbo].[SYS_Users] ("
        + " UserKey ,UserName ,Password ,Description ,Activate ,ExpireDate ,LastLoginDate ,FailedPasswordAttemptCount ,EmployeeKey ,Employee_Key ,CreatedBy ,CreatedDateTime ,ModifiedBy ,ModifiedDateTime ) "
         + " VALUES ( "
         + "@UserKey ,@UserName ,@Password ,@Description ,@Activate ,@ExpireDate ,@LastLoginDate ,@FailedPasswordAttemptCount ,@EmployeeKey ,@Employee_Key ,@CreatedBy ,@CreatedDateTime ,@ModifiedBy ,@ModifiedDateTime ) ";
            string zResult = "";
            string zConnectionString = TNS.DBConnection.Connecting.SQL_MainDatabase;
            SqlConnection zConnect = new SqlConnection(zConnectionString);
            zConnect.Open();
            try
            {
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.CommandType = CommandType.Text;
                if (_UserKey.Length == 36)
                    zCommand.Parameters.Add("@UserKey", SqlDbType.UniqueIdentifier).Value = new Guid(_UserKey);
                else
                    zCommand.Parameters.Add("@UserKey", SqlDbType.UniqueIdentifier).Value = DBNull.Value;
                zCommand.Parameters.Add("@UserName", SqlDbType.NVarChar).Value = _UserName;
                zCommand.Parameters.Add("@Password", SqlDbType.NVarChar).Value = _Password;
                zCommand.Parameters.Add("@Description", SqlDbType.NText).Value = _Description;
                zCommand.Parameters.Add("@Activate", SqlDbType.Bit).Value = _Activate;
                if (_ExpireDate == null)
                    zCommand.Parameters.Add("@ExpireDate", SqlDbType.DateTime).Value = DBNull.Value;
                else
                    zCommand.Parameters.Add("@ExpireDate", SqlDbType.DateTime).Value = _ExpireDate;
                if (_LastLoginDate == null)
                    zCommand.Parameters.Add("@LastLoginDate", SqlDbType.DateTime).Value = DBNull.Value;
                else
                    zCommand.Parameters.Add("@LastLoginDate", SqlDbType.DateTime).Value = _LastLoginDate;
                zCommand.Parameters.Add("@FailedPasswordAttemptCount", SqlDbType.Int).Value = _FailedPasswordAttemptCount;
                zCommand.Parameters.Add("@EmployeeKey", SqlDbType.Int).Value = _EmployeeKey;
                if (_Employee_Key.Length == 36)
                    zCommand.Parameters.Add("@Employee_Key", SqlDbType.UniqueIdentifier).Value = new Guid(_Employee_Key);
                else
                    zCommand.Parameters.Add("@Employee_Key", SqlDbType.UniqueIdentifier).Value = DBNull.Value;
                zCommand.Parameters.Add("@CreatedBy", SqlDbType.NVarChar).Value = _CreatedBy;
                if (_CreatedDateTime == null)
                    zCommand.Parameters.Add("@CreatedDateTime", SqlDbType.DateTime).Value = DBNull.Value;
                else
                    zCommand.Parameters.Add("@CreatedDateTime", SqlDbType.DateTime).Value = _CreatedDateTime;
                zCommand.Parameters.Add("@ModifiedBy", SqlDbType.NVarChar).Value = _ModifiedBy;
                if (_ModifiedDateTime == null)
                    zCommand.Parameters.Add("@ModifiedDateTime", SqlDbType.DateTime).Value = DBNull.Value;
                else
                    zCommand.Parameters.Add("@ModifiedDateTime", SqlDbType.DateTime).Value = _ModifiedDateTime;
                zResult = zCommand.ExecuteNonQuery().ToString();
                zCommand.Dispose();
                _Message = "201 Created";
            }
            catch (Exception Err)
            {
                _Message = "501 " + Err.ToString();
            }
            finally
            {
                zConnect.Close();
            }
            return zResult;
        }

        public string Update()
        {
            string zSQL = "UPDATE [dbo].[SYS_Users] SET "
                        + " UserName = @UserName,"
                        + " Password = @Password,"
                        + " Description = @Description,"
                        + " Activate = @Activate,"
                        + " ExpireDate = @ExpireDate,"
                        + " LastLoginDate = @LastLoginDate,"
                        + " FailedPasswordAttemptCount = @FailedPasswordAttemptCount,"
                        + " EmployeeKey = @EmployeeKey,"
                        + " Employee_Key = @Employee_Key,"
                        + " CreatedDateTime = @CreatedDateTime,"
                        + " ModifiedBy = @ModifiedBy,"
                        + " ModifiedDateTime = @ModifiedDateTime"
                       + " WHERE UserKey = @UserKey";
            string zResult = "";
            string zConnectionString = TNS.DBConnection.Connecting.SQL_MainDatabase;
            SqlConnection zConnect = new SqlConnection(zConnectionString);
            zConnect.Open();
            try
            {
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.CommandType = CommandType.Text;
                if (_UserKey.Length == 36)
                    zCommand.Parameters.Add("@UserKey", SqlDbType.UniqueIdentifier).Value = new Guid(_UserKey);
                else
                    zCommand.Parameters.Add("@UserKey", SqlDbType.UniqueIdentifier).Value = DBNull.Value;
                zCommand.Parameters.Add("@UserName", SqlDbType.NVarChar).Value = _UserName;
                zCommand.Parameters.Add("@Password", SqlDbType.NVarChar).Value = _Password;
                zCommand.Parameters.Add("@Description", SqlDbType.NText).Value = _Description;
                zCommand.Parameters.Add("@Activate", SqlDbType.Bit).Value = _Activate;
                if (_ExpireDate == null)
                    zCommand.Parameters.Add("@ExpireDate", SqlDbType.DateTime).Value = DBNull.Value;
                else
                    zCommand.Parameters.Add("@ExpireDate", SqlDbType.DateTime).Value = _ExpireDate;
                if (_LastLoginDate == null)
                    zCommand.Parameters.Add("@LastLoginDate", SqlDbType.DateTime).Value = DBNull.Value;
                else
                    zCommand.Parameters.Add("@LastLoginDate", SqlDbType.DateTime).Value = _LastLoginDate;
                zCommand.Parameters.Add("@FailedPasswordAttemptCount", SqlDbType.Int).Value = _FailedPasswordAttemptCount;
                zCommand.Parameters.Add("@EmployeeKey", SqlDbType.Int).Value = _EmployeeKey;
                if (_Employee_Key.Length == 36)
                    zCommand.Parameters.Add("@Employee_Key", SqlDbType.UniqueIdentifier).Value = new Guid(_Employee_Key);
                else
                    zCommand.Parameters.Add("@Employee_Key", SqlDbType.UniqueIdentifier).Value = DBNull.Value;
                if (_CreatedDateTime == null)
                    zCommand.Parameters.Add("@CreatedDateTime", SqlDbType.DateTime).Value = DBNull.Value;
                else
                    zCommand.Parameters.Add("@CreatedDateTime", SqlDbType.DateTime).Value = _CreatedDateTime;
                zCommand.Parameters.Add("@ModifiedBy", SqlDbType.NVarChar).Value = _ModifiedBy;
                if (_ModifiedDateTime == null)
                    zCommand.Parameters.Add("@ModifiedDateTime", SqlDbType.DateTime).Value = DBNull.Value;
                else
                    zCommand.Parameters.Add("@ModifiedDateTime", SqlDbType.DateTime).Value = _ModifiedDateTime;
                zResult = zCommand.ExecuteNonQuery().ToString();
                zCommand.Dispose();
                _Message = "200 OK";
            }
            catch (Exception Err)
            {
                _Message = "501 " + Err.ToString();
            }
            finally
            {
                zConnect.Close();
            }
            return zResult;
        }

        public string Update_Basic()
        {
            string zSQL = "UPDATE [dbo].[SYS_Users] SET "
                        + " UserName = @UserName,";
            if (_Password.Length > 0)
            {
                zSQL += " Password = @Password,";

            }
              zSQL  +=" Description = @Description,"
                    + " Employee_Key = @EmployeeKey,"
                    + " ModifiedBy = @ModifiedBy,"
                    + " ModifiedDateTime = GetDate() "
                    + " WHERE UserKey = @UserKey";
            string zResult = "";
            string zConnectionString = TNS.DBConnection.Connecting.SQL_MainDatabase;
            SqlConnection zConnect = new SqlConnection(zConnectionString);
            zConnect.Open();
            try
            {
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.CommandType = CommandType.Text;
                zCommand.Parameters.Add("@UserKey", SqlDbType.UniqueIdentifier).Value = new Guid(_UserKey);
                zCommand.Parameters.Add("@UserName", SqlDbType.NVarChar).Value = _UserName;
                zCommand.Parameters.Add("@Password", SqlDbType.NVarChar).Value = MyCryptography.HashPass(_Password);
                zCommand.Parameters.Add("@Description", SqlDbType.NVarChar).Value = _Description;
                if (_Employee_Key.Length == 36)
                    zCommand.Parameters.Add("@EmployeeKey", SqlDbType.UniqueIdentifier).Value = new Guid(_Employee_Key);
                else
                    zCommand.Parameters.Add("@EmployeeKey", SqlDbType.UniqueIdentifier).Value = DBNull.Value;
                zCommand.Parameters.Add("@ModifiedBy", SqlDbType.NVarChar).Value = _ModifiedBy;
                zResult = zCommand.ExecuteNonQuery().ToString();
                zCommand.Dispose();
                _Message = "200 OK";
            }
            catch (Exception Err)
            {
                _Message = "501 " + Err.ToString();
            }
            finally
            {
                zConnect.Close();
            }
            return zResult;
        }
        public string Delete()
        {
            string zResult = "";
            //---------- String SQL Access Database ---------------
            string zSQL = "UPDATE [dbo].[SYS_Users] Set RecordStatus = 99 WHERE UserKey = @UserKey";
            string zConnectionString = TNS.DBConnection.Connecting.SQL_MainDatabase;
            SqlConnection zConnect = new SqlConnection(zConnectionString);
            zConnect.Open();
            try
            {
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.Parameters.Add("@UserKey", SqlDbType.UniqueIdentifier).Value = new Guid(_UserKey);
                zResult = zCommand.ExecuteNonQuery().ToString();
                zCommand.Dispose();
                _Message = "200 OK";
            }
            catch (Exception Err)
            {
                _Message = "501 " + Err.ToString();
            }
            finally
            {
                zConnect.Close();
            }
            return zResult;
        }
        public string Empty()
        {
            string zResult = "";
            //---------- String SQL Access Database ---------------
            string zSQL = "DELETE FROM [dbo].[SYS_Users] WHERE UserKey = @UserKey";
            string zConnectionString = TNS.DBConnection.Connecting.SQL_MainDatabase;
            SqlConnection zConnect = new SqlConnection(zConnectionString);
            zConnect.Open();
            try
            {
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.Parameters.Add("@UserKey", SqlDbType.UniqueIdentifier).Value = new Guid(_UserKey);
                zResult = zCommand.ExecuteNonQuery().ToString();
                zCommand.Dispose();
                _Message = "200 OK";
            }
            catch (Exception Err)
            {
                _Message = "501 " + Err.ToString();
            }
            finally
            {
                zConnect.Close();
            }
            return zResult;
        }
        #endregion
    }
}
