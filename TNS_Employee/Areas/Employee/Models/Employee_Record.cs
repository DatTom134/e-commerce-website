using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Data.SqlClient;

namespace TNS_Employee.Areas.Employee.Models
{
    public class Employee_Record
    {
        #region [ Field Name ]
        private string _EmployeeKey = "";
        private string _EmployeeID = "";
        private string _LastName = "";
        private string _FirstName = "";
        private string _CadresID = "";
        private string _UserIPCAST = "";
        private string _NickName = "";
        private string _ReportToKey = "";
        private string _ReportToID = "";
        private string _ReportToName = "";
        private string _DepartmentKey = "";
        private string _OrganizationPath = "";
        private string _DepartmentName = "";
        private string _BranchKey = "";
        private string _BranchName = "";
        private string _JobKey = "";
        private string _JobName = "";
        private int _PositionKey = 0;
        private string _PositionName = "";
        private int _WorkingStatusKey = 0;
        private string _WorkingStatusName = "";
        private DateTime? _StartingDate = null;
        private DateTime? _LeavingDate = null;
        private int _ContractTypeKey = 0;
        private string _ContractTypeName = "";
        private string _WorkingAddress = "";
        private string _WorkingLocation = "";
        private string _CompanyPhone = "";
        private string _CompanyEmail = "";
        private int _Gender = 0;
        private DateTime? _Birthday = null;
        private string _BirthPlace = "";
        private int _MaritalStatusKey = 0;
        private string _MaritalStatusName = "";
        private string _Nationality = "";
        private string _Ethnicity = "";
        private string _PassportNumber = "";
        private DateTime? _IssueDate = null;
        private DateTime? _ExpireDate = null;
        private string _IssuePlace = "";
        private string _PermanentAddress = "";
        private string _TemporaryAddress = "";
        private string _ContactAddress = "";
        private string _MobiPhone = "";
        private string _Email = "";
        private string _TaxNumber = "";
        private string _BankAccount = "";
        private string _BankName = "";
        //private Image _Photo;
        //private Image _Photo_Small;
        private string _PhotoPath = "";
        private string _Note = "";
        private int _RecordStatus = 0;
        private string _Style = "";
        private string _Class = "";
        private string _CodeLine = "";
        private DateTime? _CreatedOn = null;
        private string _CreatedBy = "";
        private string _CreatedName = "";
        private DateTime? _ModifiedOn = null;
        private string _ModifiedBy = "";
        private string _ModifiedName = "";
        private string _Message = "";
        #endregion

        #region [ Constructor Get Information ]
        public Employee_Record()
        {
            _PhotoPath = "/images/users/avatar-0.jpg";
        }
        public Employee_Record(string EmployeeKey)
        {
            string zSQL = "SELECT * FROM [dbo].[HRM_Employee] WHERE EmployeeKey = @EmployeeKey AND RecordStatus != 99 ";
            string zConnectionString = TNS.DBConnection.Connecting.SQL_MainDatabase;
            SqlConnection zConnect = new SqlConnection(zConnectionString);
            zConnect.Open();
            try
            {
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.CommandType = CommandType.Text;
                zCommand.Parameters.Add("@EmployeeKey", SqlDbType.UniqueIdentifier).Value = new Guid(EmployeeKey);
                SqlDataReader zReader = zCommand.ExecuteReader();
                if (zReader.HasRows)
                {
                    zReader.Read();
                    _EmployeeKey = zReader["EmployeeKey"].ToString();
                    _EmployeeID = zReader["EmployeeID"].ToString();
                    _LastName = zReader["LastName"].ToString();
                    _FirstName = zReader["FirstName"].ToString();
                    _CadresID = zReader["CadresID"].ToString();
                    _UserIPCAST = zReader["UserIPCAST"].ToString();
                    _NickName = zReader["NickName"].ToString();
                    _ReportToKey = zReader["ReportToKey"].ToString();
                    _ReportToName = zReader["ReportToName"].ToString();
                    _DepartmentKey = zReader["DepartmentKey"].ToString();
                    _OrganizationPath = zReader["OrganizationPath"].ToString();
                    _DepartmentName = zReader["DepartmentName"].ToString();
                    _BranchKey = zReader["BranchKey"].ToString();
                    _BranchName = zReader["BranchName"].ToString();
                    _JobKey = zReader["JobKey"].ToString();
                    _JobName = zReader["JobName"].ToString();
                    _PositionKey = int.Parse(zReader["PositionKey"].ToString());
                    _PositionName = zReader["PositionName"].ToString();
                    _WorkingStatusKey = int.Parse(zReader["WorkingStatusKey"].ToString());
                    _WorkingStatusName = zReader["WorkingStatusName"].ToString();
                    if (zReader["StartingDate"] != DBNull.Value)
                        _StartingDate = (DateTime)zReader["StartingDate"];
                    if (zReader["LeavingDate"] != DBNull.Value)
                        _LeavingDate = (DateTime)zReader["LeavingDate"];
                    _ContractTypeKey = int.Parse(zReader["ContractTypeKey"].ToString());
                    _ContractTypeName = zReader["ContractTypeName"].ToString();
                    _WorkingAddress = zReader["WorkingAddress"].ToString();
                    _WorkingLocation = zReader["WorkingLocation"].ToString();
                    _CompanyPhone = zReader["CompanyPhone"].ToString();
                    _CompanyEmail = zReader["CompanyEmail"].ToString();
                    _Gender = int.Parse(zReader["Gender"].ToString());
                    if (zReader["Birthday"] != DBNull.Value)
                        _Birthday = (DateTime)zReader["Birthday"];
                    _BirthPlace = zReader["BirthPlace"].ToString();
                    _MaritalStatusKey = int.Parse(zReader["MaritalStatusKey"].ToString());
                    _MaritalStatusName = zReader["MaritalStatusName"].ToString();
                    _Nationality = zReader["Nationality"].ToString();
                    _Ethnicity = zReader["Ethnicity"].ToString();
                    _PassportNumber = zReader["PassportNumber"].ToString();
                    if (zReader["IssueDate"] != DBNull.Value)
                        _IssueDate = (DateTime)zReader["IssueDate"];
                    if (zReader["ExpireDate"] != DBNull.Value)
                        _ExpireDate = (DateTime)zReader["ExpireDate"];
                    _IssuePlace = zReader["IssuePlace"].ToString();
                    _PermanentAddress = zReader["PermanentAddress"].ToString();
                    _TemporaryAddress = zReader["TemporaryAddress"].ToString();
                    _ContactAddress = zReader["ContactAddress"].ToString();
                    _MobiPhone = zReader["MobiPhone"].ToString();
                    _Email = zReader["Email"].ToString();
                    _TaxNumber = zReader["TaxNumber"].ToString();
                    _BankAccount = zReader["BankAccount"].ToString();
                    _BankName = zReader["BankName"].ToString();
                    _PhotoPath = zReader["PhotoPath"].ToString();
                    _Note = zReader["Note"].ToString();
                    _RecordStatus = int.Parse(zReader["RecordStatus"].ToString());
                    _Style = zReader["Style"].ToString();
                    _Class = zReader["Class"].ToString();
                    _CodeLine = zReader["CodeLine"].ToString();
                    if (zReader["CreatedOn"] != DBNull.Value)
                        _CreatedOn = (DateTime)zReader["CreatedOn"];
                    _CreatedBy = zReader["CreatedBy"].ToString();
                    _CreatedName = zReader["CreatedName"].ToString();
                    if (zReader["ModifiedOn"] != DBNull.Value)
                        _ModifiedOn = (DateTime)zReader["ModifiedOn"];
                    _ModifiedBy = zReader["ModifiedBy"].ToString();
                    _ModifiedName = zReader["ModifiedName"].ToString();
                    _Message = "200 OK";

                    if(_PhotoPath == null || _PhotoPath.Length == 0)
                    {
                        _PhotoPath = "/images/users/avatar-0.jpg";
                    }
                    GetReportTo_ID();
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

        public void GetReportTo_ID()
        {
            if (_ReportToKey != null)
            {
                string zSQL = "SELECT EmployeeID FROM [dbo].[HRM_Employee] WHERE EmployeeKey = @EmployeeKey AND RecordStatus != 99 ";
                string zConnectionString = TNS.DBConnection.Connecting.SQL_MainDatabase;
                SqlConnection zConnect = new SqlConnection(zConnectionString);
                zConnect.Open();
                try
                {
                    SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                    zCommand.CommandType = CommandType.Text;
                    zCommand.Parameters.Add("@EmployeeKey", SqlDbType.UniqueIdentifier).Value = new Guid(_ReportToKey);
                    SqlDataReader zReader = zCommand.ExecuteReader();
                    if (zReader.HasRows)
                    {
                        zReader.Read();
                        _ReportToID = zReader["EmployeeID"].ToString();
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
        }
        public void GetReportTo_KeyName()
        {
            _ReportToKey = "";
            _ReportToName = "";
            if (_ReportToKey != null)
            {
                string zSQL = "SELECT EmployeeKey,LastName,FirstName FROM [dbo].[HRM_Employee] WHERE EmployeeID = @EmployeeID AND RecordStatus != 99 ";
                string zConnectionString = TNS.DBConnection.Connecting.SQL_MainDatabase;
                SqlConnection zConnect = new SqlConnection(zConnectionString);
                zConnect.Open();
                try
                {
                    SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                    zCommand.CommandType = CommandType.Text;
                    zCommand.Parameters.Add("@EmployeeID", SqlDbType.NVarChar).Value = _ReportToID;
                    SqlDataReader zReader = zCommand.ExecuteReader();
                    if (zReader.HasRows)
                    {
                        zReader.Read();
                        _ReportToKey = zReader["EmployeeKey"].ToString();
                        _ReportToName = zReader["LastName"].ToString() + " " + zReader["FirstName"].ToString();
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
        }
        #endregion

        #region [ Properties ]
        public string EmployeeKey
        {
            get { return _EmployeeKey; }
            set { _EmployeeKey = value; }
        }
        public string EmployeeID
        {
            get { return _EmployeeID; }
            set { _EmployeeID = value; }
        }
        public string LastName
        {
            get { return _LastName; }
            set { _LastName = value; }
        }
        public string FirstName
        {
            get { return _FirstName; }
            set { _FirstName = value; }
        }
        public string CadresID
        {
            get { return _CadresID; }
            set { _CadresID = value; }
        }
        public string UserIPCAST
        {
            get { return _UserIPCAST; }
            set { _UserIPCAST = value; }
        }
        public string NickName
        {
            get { return _NickName; }
            set { _NickName = value; }
        }
        public string ReportToKey
        {
            get { return _ReportToKey; }
            set { _ReportToKey = value; }
        }
        public string? ReportToID
        {
            get { return _ReportToID; }
            set { _ReportToID = value; }
        }
        public string ReportToName
        {
            get { return _ReportToName; }
            set { _ReportToName = value; }
        }
        public string DepartmentKey
        {
            get { return _DepartmentKey; }
            set { _DepartmentKey = value; }
        }
        public string OrganizationPath
        {
            get { return _OrganizationPath; }
            set { _OrganizationPath = value; }
        }
        public string DepartmentName
        {
            get { return _DepartmentName; }
            set { _DepartmentName = value; }
        }
        public string BranchKey
        {
            get { return _BranchKey; }
            set { _BranchKey = value; }
        }
        public string BranchName
        {
            get { return _BranchName; }
            set { _BranchName = value; }
        }
        public string JobKey
        {
            get { return _JobKey; }
            set { _JobKey = value; }
        }
        public string? JobName
        {
            get { return _JobName; }
            set
            {
                _JobName = value;
                if (_JobName == null)
                {
                    _JobName = "";
                }
            }
        }
        public int PositionKey
        {
            get { return _PositionKey; }
            set { _PositionKey = value; }
        }
        public string? PositionName
        {
            get { return _PositionName; }
            set
            {
                _PositionName = value;
                if (_PositionName == null)
                {
                    _PositionName = "";
                }
            }
        }
        public int WorkingStatusKey
        {
            get { return _WorkingStatusKey; }
            set { _WorkingStatusKey = value; }
        }
        public string WorkingStatusName
        {
            get { return _WorkingStatusName; }
            set { _WorkingStatusName = value; }
        }
        public DateTime? StartingDate
        {
            get { return _StartingDate; }
            set { _StartingDate = value; }
        }
        public DateTime? LeavingDate
        {
            get { return _LeavingDate; }
            set { _LeavingDate = value; }
        }
        public int ContractTypeKey
        {
            get { return _ContractTypeKey; }
            set { _ContractTypeKey = value; }
        }
        public string? ContractTypeName
        {
            get { return _ContractTypeName; }
            set
            {
                _ContractTypeName = value;
                if (_ContractTypeName == null)
                {
                    _ContractTypeName = "";
                }
            }
        }
        public string WorkingAddress
        {
            get { return _WorkingAddress; }
            set { _WorkingAddress = value; }
        }
        public string WorkingLocation
        {
            get { return _WorkingLocation; }
            set { _WorkingLocation = value; }
        }
        public string CompanyPhone
        {
            get { return _CompanyPhone; }
            set { _CompanyPhone = value; }
        }
        public string CompanyEmail
        {
            get { return _CompanyEmail; }
            set { _CompanyEmail = value; }
        }
        public int Gender
        {
            get { return _Gender; }
            set { _Gender = value; }
        }
        public DateTime? Birthday
        {
            get { return _Birthday; }
            set { _Birthday = value; }
        }
        public string BirthPlace
        {
            get { return _BirthPlace; }
            set { _BirthPlace = value; }
        }
        public int MaritalStatusKey
        {
            get { return _MaritalStatusKey; }
            set { _MaritalStatusKey = value; }
        }
        public string MaritalStatusName
        {
            get { return _MaritalStatusName; }
            set { _MaritalStatusName = value; }
        }
        public string Nationality
        {
            get { return _Nationality; }
            set { _Nationality = value; }
        }
        public string Ethnicity
        {
            get { return _Ethnicity; }
            set { _Ethnicity = value; }
        }
        public string PassportNumber
        {
            get { return _PassportNumber; }
            set { _PassportNumber = value; }
        }
        public DateTime? IssueDate
        {
            get { return _IssueDate; }
            set { _IssueDate = value; }
        }
        public DateTime? ExpireDate
        {
            get { return _ExpireDate; }
            set { _ExpireDate = value; }
        }
        public string IssuePlace
        {
            get { return _IssuePlace; }
            set { _IssuePlace = value; }
        }
        public string PermanentAddress
        {
            get { return _PermanentAddress; }
            set { _PermanentAddress = value; }
        }
        public string TemporaryAddress
        {
            get { return _TemporaryAddress; }
            set { _TemporaryAddress = value; }
        }
        public string ContactAddress
        {
            get { return _ContactAddress; }
            set { _ContactAddress = value; }
        }
        public string? MobiPhone
        {
            get { return _MobiPhone; }
            set
            {
                _MobiPhone = value;
                if (_MobiPhone == null)
                {
                    _MobiPhone = "";
                }
            }
        }
        public string? Email
        {
            get { return _Email; }
            set
            {
                _Email = value;
                if (_Email == null)
                {
                    _Email = "";
                }
            }
        }
        public string TaxNumber
        {
            get { return _TaxNumber; }
            set { _TaxNumber = value; }
        }
        public string BankAccount
        {
            get { return _BankAccount; }
            set { _BankAccount = value; }
        }
        public string BankName
        {
            get { return _BankName; }
            set { _BankName = value; }
        }
        //public Image Photo
        //{
        //    get { return _Photo; }
        //    set { _Photo = value; }
        //}
        //public Image Photo_Small
        //{
        //    get { return _Photo_Small; }
        //    set { _Photo_Small = value; }
        //}
        public string PhotoPath
        {
            get { return _PhotoPath; }
            set { _PhotoPath = value; }
        }
        public string Note
        {
            get { return _Note; }
            set { _Note = value; }
        }
        public int RecordStatus
        {
            get { return _RecordStatus; }
            set { _RecordStatus = value; }
        }
        public string Style
        {
            get { return _Style; }
            set { _Style = value; }
        }
        public string Class
        {
            get { return _Class; }
            set { _Class = value; }
        }
        public string CodeLine
        {
            get { return _CodeLine; }
            set { _CodeLine = value; }
        }
        public DateTime? CreatedOn
        {
            get { return _CreatedOn; }
            set { _CreatedOn = value; }
        }
        public string CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; }
        }
        public string CreatedName
        {
            get { return _CreatedName; }
            set { _CreatedName = value; }
        }
        public DateTime? ModifiedOn
        {
            get { return _ModifiedOn; }
            set { _ModifiedOn = value; }
        }
        public string ModifiedBy
        {
            get { return _ModifiedBy; }
            set { _ModifiedBy = value; }
        }
        public string ModifiedName
        {
            get { return _ModifiedName; }
            set { _ModifiedName = value; }
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
            string zSQL = "INSERT INTO [dbo].[HRM_Employee] ("
        + " EmployeeID ,LastName ,FirstName ,CadresID ,UserIPCAST ,NickName ,ReportToKey ,ReportToName ,DepartmentKey ,OrganizationPath ,DepartmentName ,BranchKey ,BranchName ,JobKey ,JobName ,PositionKey ,PositionName ,WorkingStatusKey ,WorkingStatusName ,StartingDate ,LeavingDate ,ContractTypeKey ,ContractTypeName ,WorkingAddress ,WorkingLocation ,CompanyPhone ,CompanyEmail ,Gender ,Birthday ,BirthPlace ,MaritalStatusKey ,MaritalStatusName ,Nationality ,Ethnicity ,PassportNumber ,IssueDate ,ExpireDate ,IssuePlace ,PermanentAddress ,TemporaryAddress ,ContactAddress ,MobiPhone ,Email ,TaxNumber ,BankAccount ,BankName ,PhotoPath ,Note ,RecordStatus ,Style ,Class ,CodeLine ,CreatedBy ,CreatedName ,ModifiedBy ,ModifiedName ) "
         + " VALUES ( "
         + "@EmployeeID ,@LastName ,@FirstName ,@CadresID ,@UserIPCAST ,@NickName ,@ReportToKey ,@ReportToName ,@DepartmentKey ,@OrganizationPath ,@DepartmentName ,@BranchKey ,@BranchName ,@JobKey ,@JobName ,@PositionKey ,@PositionName ,@WorkingStatusKey ,@WorkingStatusName ,@StartingDate ,@LeavingDate ,@ContractTypeKey ,@ContractTypeName ,@WorkingAddress ,@WorkingLocation ,@CompanyPhone ,@CompanyEmail ,@Gender ,@Birthday ,@BirthPlace ,@MaritalStatusKey ,@MaritalStatusName ,@Nationality ,@Ethnicity ,@PassportNumber ,@IssueDate ,@ExpireDate ,@IssuePlace ,@PermanentAddress ,@TemporaryAddress ,@ContactAddress ,@MobiPhone ,@Email ,@TaxNumber ,@BankAccount ,@BankName ,@PhotoPath ,@Note ,@RecordStatus ,@Style ,@Class ,@CodeLine ,@CreatedBy ,@CreatedName ,@ModifiedBy ,@ModifiedName ) ";
            string zResult = "";
            string zConnectionString = TNS.DBConnection.Connecting.SQL_MainDatabase;
            SqlConnection zConnect = new SqlConnection(zConnectionString);
            zConnect.Open();
            try
            {
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.CommandType = CommandType.Text;
                zCommand.Parameters.Add("@EmployeeID", SqlDbType.NVarChar).Value = _EmployeeID;
                zCommand.Parameters.Add("@LastName", SqlDbType.NVarChar).Value = _LastName;
                zCommand.Parameters.Add("@FirstName", SqlDbType.NVarChar).Value = _FirstName;
                zCommand.Parameters.Add("@CadresID", SqlDbType.NVarChar).Value = _CadresID;
                zCommand.Parameters.Add("@UserIPCAST", SqlDbType.NVarChar).Value = _UserIPCAST;
                zCommand.Parameters.Add("@NickName", SqlDbType.NVarChar).Value = _NickName;
                if (_ReportToKey.Length == 36)
                    zCommand.Parameters.Add("@ReportToKey", SqlDbType.UniqueIdentifier).Value = new Guid(_ReportToKey);
                else
                    zCommand.Parameters.Add("@ReportToKey", SqlDbType.UniqueIdentifier).Value = DBNull.Value;
                zCommand.Parameters.Add("@ReportToName", SqlDbType.NVarChar).Value = _ReportToName;
                if (_DepartmentKey.Length == 36)
                    zCommand.Parameters.Add("@DepartmentKey", SqlDbType.UniqueIdentifier).Value = new Guid(_DepartmentKey);
                else
                    zCommand.Parameters.Add("@DepartmentKey", SqlDbType.UniqueIdentifier).Value = DBNull.Value;
                zCommand.Parameters.Add("@OrganizationPath", SqlDbType.NVarChar).Value = _OrganizationPath;
                zCommand.Parameters.Add("@DepartmentName", SqlDbType.NVarChar).Value = _DepartmentName;
                if (_BranchKey.Length == 36)
                    zCommand.Parameters.Add("@BranchKey", SqlDbType.UniqueIdentifier).Value = new Guid(_BranchKey);
                else
                    zCommand.Parameters.Add("@BranchKey", SqlDbType.UniqueIdentifier).Value = DBNull.Value;
                zCommand.Parameters.Add("@BranchName", SqlDbType.NVarChar).Value = _BranchName;
                if (_JobKey.Length == 36)
                    zCommand.Parameters.Add("@JobKey", SqlDbType.UniqueIdentifier).Value = new Guid(_JobKey);
                else
                    zCommand.Parameters.Add("@JobKey", SqlDbType.UniqueIdentifier).Value = DBNull.Value;
                zCommand.Parameters.Add("@JobName", SqlDbType.NVarChar).Value = _JobName;
                zCommand.Parameters.Add("@PositionKey", SqlDbType.Int).Value = _PositionKey;
                zCommand.Parameters.Add("@PositionName", SqlDbType.NVarChar).Value = _PositionName;
                zCommand.Parameters.Add("@WorkingStatusKey", SqlDbType.Int).Value = _WorkingStatusKey;
                zCommand.Parameters.Add("@WorkingStatusName", SqlDbType.NVarChar).Value = _WorkingStatusName;
                if (_StartingDate == null)
                    zCommand.Parameters.Add("@StartingDate", SqlDbType.Date).Value = DBNull.Value;
                else
                    zCommand.Parameters.Add("@StartingDate", SqlDbType.Date).Value = _StartingDate;
                if (_LeavingDate == null)
                    zCommand.Parameters.Add("@LeavingDate", SqlDbType.Date).Value = DBNull.Value;
                else
                    zCommand.Parameters.Add("@LeavingDate", SqlDbType.Date).Value = _LeavingDate;
                zCommand.Parameters.Add("@ContractTypeKey", SqlDbType.Int).Value = _ContractTypeKey;
                zCommand.Parameters.Add("@ContractTypeName", SqlDbType.NVarChar).Value = _ContractTypeName;
                zCommand.Parameters.Add("@WorkingAddress", SqlDbType.NVarChar).Value = _WorkingAddress;
                zCommand.Parameters.Add("@WorkingLocation", SqlDbType.NVarChar).Value = _WorkingLocation;
                zCommand.Parameters.Add("@CompanyPhone", SqlDbType.NVarChar).Value = _CompanyPhone;
                zCommand.Parameters.Add("@CompanyEmail", SqlDbType.NVarChar).Value = _CompanyEmail;
                zCommand.Parameters.Add("@Gender", SqlDbType.Int).Value = _Gender;
                if (_Birthday == null)
                    zCommand.Parameters.Add("@Birthday", SqlDbType.DateTime).Value = DBNull.Value;
                else
                    zCommand.Parameters.Add("@Birthday", SqlDbType.DateTime).Value = _Birthday;
                zCommand.Parameters.Add("@BirthPlace", SqlDbType.NVarChar).Value = _BirthPlace;
                zCommand.Parameters.Add("@MaritalStatusKey", SqlDbType.Int).Value = _MaritalStatusKey;
                zCommand.Parameters.Add("@MaritalStatusName", SqlDbType.NVarChar).Value = _MaritalStatusName;
                zCommand.Parameters.Add("@Nationality", SqlDbType.NVarChar).Value = _Nationality;
                zCommand.Parameters.Add("@Ethnicity", SqlDbType.NVarChar).Value = _Ethnicity;
                zCommand.Parameters.Add("@PassportNumber", SqlDbType.NVarChar).Value = _PassportNumber;
                if (_IssueDate == null)
                    zCommand.Parameters.Add("@IssueDate", SqlDbType.DateTime).Value = DBNull.Value;
                else
                    zCommand.Parameters.Add("@IssueDate", SqlDbType.DateTime).Value = _IssueDate;
                if (_ExpireDate == null)
                    zCommand.Parameters.Add("@ExpireDate", SqlDbType.DateTime).Value = DBNull.Value;
                else
                    zCommand.Parameters.Add("@ExpireDate", SqlDbType.DateTime).Value = _ExpireDate;
                zCommand.Parameters.Add("@IssuePlace", SqlDbType.NVarChar).Value = _IssuePlace;
                zCommand.Parameters.Add("@PermanentAddress", SqlDbType.NVarChar).Value = _PermanentAddress;
                zCommand.Parameters.Add("@TemporaryAddress", SqlDbType.NVarChar).Value = _TemporaryAddress;
                zCommand.Parameters.Add("@ContactAddress", SqlDbType.NVarChar).Value = _ContactAddress;
                zCommand.Parameters.Add("@MobiPhone", SqlDbType.NVarChar).Value = _MobiPhone;
                zCommand.Parameters.Add("@Email", SqlDbType.NVarChar).Value = _Email;
                zCommand.Parameters.Add("@TaxNumber", SqlDbType.NVarChar).Value = _TaxNumber;
                zCommand.Parameters.Add("@BankAccount", SqlDbType.NVarChar).Value = _BankAccount;
                zCommand.Parameters.Add("@BankName", SqlDbType.NVarChar).Value = _BankName;
                zCommand.Parameters.Add("@PhotoPath", SqlDbType.NVarChar).Value = _PhotoPath;
                zCommand.Parameters.Add("@Note", SqlDbType.NText).Value = _Note;
                zCommand.Parameters.Add("@RecordStatus", SqlDbType.Int).Value = _RecordStatus;
                zCommand.Parameters.Add("@Style", SqlDbType.NChar).Value = _Style;
                zCommand.Parameters.Add("@Class", SqlDbType.NChar).Value = _Class;
                zCommand.Parameters.Add("@CodeLine", SqlDbType.NChar).Value = _CodeLine;
                if (_CreatedBy.Length == 36)
                    zCommand.Parameters.Add("@CreatedBy", SqlDbType.UniqueIdentifier).Value = new Guid(_CreatedBy);
                else
                    zCommand.Parameters.Add("@CreatedBy", SqlDbType.UniqueIdentifier).Value = DBNull.Value;
                zCommand.Parameters.Add("@CreatedName", SqlDbType.NVarChar).Value = _CreatedName;
                if (_ModifiedBy.Length == 36)
                    zCommand.Parameters.Add("@ModifiedBy", SqlDbType.UniqueIdentifier).Value = new Guid(_ModifiedBy);
                else
                    zCommand.Parameters.Add("@ModifiedBy", SqlDbType.UniqueIdentifier).Value = DBNull.Value;
                zCommand.Parameters.Add("@ModifiedName", SqlDbType.NVarChar).Value = _ModifiedName;
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
            string zSQL = "INSERT INTO [dbo].[HRM_Employee] ("
        + " EmployeeKey ,EmployeeID ,LastName ,FirstName ,CadresID ,UserIPCAST ,NickName ,ReportToKey ,ReportToName ,DepartmentKey ,OrganizationPath ,DepartmentName ,BranchKey ,BranchName ,JobKey ,JobName ,PositionKey ,PositionName ,WorkingStatusKey ,WorkingStatusName ,StartingDate ,LeavingDate ,ContractTypeKey ,ContractTypeName ,WorkingAddress ,WorkingLocation ,CompanyPhone ,CompanyEmail ,Gender ,Birthday ,BirthPlace ,MaritalStatusKey ,MaritalStatusName ,Nationality ,Ethnicity ,PassportNumber ,IssueDate ,ExpireDate ,IssuePlace ,PermanentAddress ,TemporaryAddress ,ContactAddress ,MobiPhone ,Email ,TaxNumber ,BankAccount ,BankName ,PhotoPath ,Note ,RecordStatus ,Style ,Class ,CodeLine ,CreatedBy ,CreatedName ,ModifiedBy ,ModifiedName ) "
         + " VALUES ( "
         + "@EmployeeKey ,@EmployeeID ,@LastName ,@FirstName ,@CadresID ,@UserIPCAST ,@NickName ,@ReportToKey ,@ReportToName ,@DepartmentKey ,@OrganizationPath ,@DepartmentName ,@BranchKey ,@BranchName ,@JobKey ,@JobName ,@PositionKey ,@PositionName ,@WorkingStatusKey ,@WorkingStatusName ,@StartingDate ,@LeavingDate ,@ContractTypeKey ,@ContractTypeName ,@WorkingAddress ,@WorkingLocation ,@CompanyPhone ,@CompanyEmail ,@Gender ,@Birthday ,@BirthPlace ,@MaritalStatusKey ,@MaritalStatusName ,@Nationality ,@Ethnicity ,@PassportNumber ,@IssueDate ,@ExpireDate ,@IssuePlace ,@PermanentAddress ,@TemporaryAddress ,@ContactAddress ,@MobiPhone ,@Email ,@TaxNumber ,@BankAccount ,@BankName ,@PhotoPath ,@Note ,@RecordStatus ,@Style ,@Class ,@CodeLine ,@CreatedBy ,@CreatedName ,@ModifiedBy ,@ModifiedName ) ";
            string zResult = "";
            string zConnectionString = TNS.DBConnection.Connecting.SQL_MainDatabase;
            SqlConnection zConnect = new SqlConnection(zConnectionString);
            zConnect.Open();
            try
            {
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.CommandType = CommandType.Text;
                if (_EmployeeKey.Length == 36)
                    zCommand.Parameters.Add("@EmployeeKey", SqlDbType.UniqueIdentifier).Value = new Guid(_EmployeeKey);
                else
                    zCommand.Parameters.Add("@EmployeeKey", SqlDbType.UniqueIdentifier).Value = DBNull.Value;
                zCommand.Parameters.Add("@EmployeeID", SqlDbType.NVarChar).Value = _EmployeeID;
                zCommand.Parameters.Add("@LastName", SqlDbType.NVarChar).Value = _LastName;
                zCommand.Parameters.Add("@FirstName", SqlDbType.NVarChar).Value = _FirstName;
                zCommand.Parameters.Add("@CadresID", SqlDbType.NVarChar).Value = _CadresID;
                zCommand.Parameters.Add("@UserIPCAST", SqlDbType.NVarChar).Value = _UserIPCAST;
                zCommand.Parameters.Add("@NickName", SqlDbType.NVarChar).Value = _NickName;
                if (_ReportToKey.Length == 36)
                    zCommand.Parameters.Add("@ReportToKey", SqlDbType.UniqueIdentifier).Value = new Guid(_ReportToKey);
                else
                    zCommand.Parameters.Add("@ReportToKey", SqlDbType.UniqueIdentifier).Value = DBNull.Value;
                zCommand.Parameters.Add("@ReportToName", SqlDbType.NVarChar).Value = _ReportToName;
                if (_DepartmentKey.Length == 36)
                    zCommand.Parameters.Add("@DepartmentKey", SqlDbType.UniqueIdentifier).Value = new Guid(_DepartmentKey);
                else
                    zCommand.Parameters.Add("@DepartmentKey", SqlDbType.UniqueIdentifier).Value = DBNull.Value;
                zCommand.Parameters.Add("@OrganizationPath", SqlDbType.NVarChar).Value = _OrganizationPath;
                zCommand.Parameters.Add("@DepartmentName", SqlDbType.NVarChar).Value = _DepartmentName;
                if (_BranchKey.Length == 36)
                    zCommand.Parameters.Add("@BranchKey", SqlDbType.UniqueIdentifier).Value = new Guid(_BranchKey);
                else
                    zCommand.Parameters.Add("@BranchKey", SqlDbType.UniqueIdentifier).Value = DBNull.Value;
                zCommand.Parameters.Add("@BranchName", SqlDbType.NVarChar).Value = _BranchName;
                if (_JobKey.Length == 36)
                    zCommand.Parameters.Add("@JobKey", SqlDbType.UniqueIdentifier).Value = new Guid(_JobKey);
                else
                    zCommand.Parameters.Add("@JobKey", SqlDbType.UniqueIdentifier).Value = DBNull.Value;
                zCommand.Parameters.Add("@JobName", SqlDbType.NVarChar).Value = _JobName;
                zCommand.Parameters.Add("@PositionKey", SqlDbType.Int).Value = _PositionKey;
                zCommand.Parameters.Add("@PositionName", SqlDbType.NVarChar).Value = _PositionName;
                zCommand.Parameters.Add("@WorkingStatusKey", SqlDbType.Int).Value = _WorkingStatusKey;
                zCommand.Parameters.Add("@WorkingStatusName", SqlDbType.NVarChar).Value = _WorkingStatusName;
                if (_StartingDate == null)
                    zCommand.Parameters.Add("@StartingDate", SqlDbType.Date).Value = DBNull.Value;
                else
                    zCommand.Parameters.Add("@StartingDate", SqlDbType.Date).Value = _StartingDate;
                if (_LeavingDate == null)
                    zCommand.Parameters.Add("@LeavingDate", SqlDbType.Date).Value = DBNull.Value;
                else
                    zCommand.Parameters.Add("@LeavingDate", SqlDbType.Date).Value = _LeavingDate;
                zCommand.Parameters.Add("@ContractTypeKey", SqlDbType.Int).Value = _ContractTypeKey;
                zCommand.Parameters.Add("@ContractTypeName", SqlDbType.NVarChar).Value = _ContractTypeName;
                zCommand.Parameters.Add("@WorkingAddress", SqlDbType.NVarChar).Value = _WorkingAddress;
                zCommand.Parameters.Add("@WorkingLocation", SqlDbType.NVarChar).Value = _WorkingLocation;
                zCommand.Parameters.Add("@CompanyPhone", SqlDbType.NVarChar).Value = _CompanyPhone;
                zCommand.Parameters.Add("@CompanyEmail", SqlDbType.NVarChar).Value = _CompanyEmail;
                zCommand.Parameters.Add("@Gender", SqlDbType.Int).Value = _Gender;
                if (_Birthday == null)
                    zCommand.Parameters.Add("@Birthday", SqlDbType.DateTime).Value = DBNull.Value;
                else
                    zCommand.Parameters.Add("@Birthday", SqlDbType.DateTime).Value = _Birthday;
                zCommand.Parameters.Add("@BirthPlace", SqlDbType.NVarChar).Value = _BirthPlace;
                zCommand.Parameters.Add("@MaritalStatusKey", SqlDbType.Int).Value = _MaritalStatusKey;
                zCommand.Parameters.Add("@MaritalStatusName", SqlDbType.NVarChar).Value = _MaritalStatusName;
                zCommand.Parameters.Add("@Nationality", SqlDbType.NVarChar).Value = _Nationality;
                zCommand.Parameters.Add("@Ethnicity", SqlDbType.NVarChar).Value = _Ethnicity;
                zCommand.Parameters.Add("@PassportNumber", SqlDbType.NVarChar).Value = _PassportNumber;
                if (_IssueDate == null)
                    zCommand.Parameters.Add("@IssueDate", SqlDbType.DateTime).Value = DBNull.Value;
                else
                    zCommand.Parameters.Add("@IssueDate", SqlDbType.DateTime).Value = _IssueDate;
                if (_ExpireDate == null)
                    zCommand.Parameters.Add("@ExpireDate", SqlDbType.DateTime).Value = DBNull.Value;
                else
                    zCommand.Parameters.Add("@ExpireDate", SqlDbType.DateTime).Value = _ExpireDate;
                zCommand.Parameters.Add("@IssuePlace", SqlDbType.NVarChar).Value = _IssuePlace;
                zCommand.Parameters.Add("@PermanentAddress", SqlDbType.NVarChar).Value = _PermanentAddress;
                zCommand.Parameters.Add("@TemporaryAddress", SqlDbType.NVarChar).Value = _TemporaryAddress;
                zCommand.Parameters.Add("@ContactAddress", SqlDbType.NVarChar).Value = _ContactAddress;
                zCommand.Parameters.Add("@MobiPhone", SqlDbType.NVarChar).Value = _MobiPhone;
                zCommand.Parameters.Add("@Email", SqlDbType.NVarChar).Value = _Email;
                zCommand.Parameters.Add("@TaxNumber", SqlDbType.NVarChar).Value = _TaxNumber;
                zCommand.Parameters.Add("@BankAccount", SqlDbType.NVarChar).Value = _BankAccount;
                zCommand.Parameters.Add("@BankName", SqlDbType.NVarChar).Value = _BankName;
                zCommand.Parameters.Add("@PhotoPath", SqlDbType.NVarChar).Value = _PhotoPath;
                zCommand.Parameters.Add("@Note", SqlDbType.NText).Value = _Note;
                zCommand.Parameters.Add("@RecordStatus", SqlDbType.Int).Value = _RecordStatus;
                zCommand.Parameters.Add("@Style", SqlDbType.NChar).Value = _Style;
                zCommand.Parameters.Add("@Class", SqlDbType.NChar).Value = _Class;
                zCommand.Parameters.Add("@CodeLine", SqlDbType.NChar).Value = _CodeLine;
                if (_CreatedBy.Length == 36)
                    zCommand.Parameters.Add("@CreatedBy", SqlDbType.UniqueIdentifier).Value = new Guid(_CreatedBy);
                else
                    zCommand.Parameters.Add("@CreatedBy", SqlDbType.UniqueIdentifier).Value = DBNull.Value;
                zCommand.Parameters.Add("@CreatedName", SqlDbType.NVarChar).Value = _CreatedName;
                if (_ModifiedBy.Length == 36)
                    zCommand.Parameters.Add("@ModifiedBy", SqlDbType.UniqueIdentifier).Value = new Guid(_ModifiedBy);
                else
                    zCommand.Parameters.Add("@ModifiedBy", SqlDbType.UniqueIdentifier).Value = DBNull.Value;
                zCommand.Parameters.Add("@ModifiedName", SqlDbType.NVarChar).Value = _ModifiedName;
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
            string zSQL = "UPDATE [dbo].[HRM_Employee] SET "
                        + " EmployeeID = @EmployeeID,"
                        + " LastName = @LastName,"
                        + " FirstName = @FirstName,"
                        + " CadresID = @CadresID,"
                        + " UserIPCAST = @UserIPCAST,"
                        + " NickName = @NickName,"
                        + " ReportToKey = @ReportToKey,"
                        + " ReportToName = @ReportToName,"
                        + " DepartmentKey = @DepartmentKey,"
                        + " OrganizationPath = @OrganizationPath,"
                        + " DepartmentName = @DepartmentName,"
                        + " BranchKey = @BranchKey,"
                        + " BranchName = @BranchName,"
                        + " JobKey = @JobKey,"
                        + " JobName = @JobName,"
                        + " PositionKey = @PositionKey,"
                        + " PositionName = @PositionName,"
                        + " WorkingStatusKey = @WorkingStatusKey,"
                        + " WorkingStatusName = @WorkingStatusName,"
                        + " StartingDate = @StartingDate,"
                        + " LeavingDate = @LeavingDate,"
                        + " ContractTypeKey = @ContractTypeKey,"
                        + " ContractTypeName = @ContractTypeName,"
                        + " WorkingAddress = @WorkingAddress,"
                        + " WorkingLocation = @WorkingLocation,"
                        + " CompanyPhone = @CompanyPhone,"
                        + " CompanyEmail = @CompanyEmail,"
                        + " Gender = @Gender,"
                        + " Birthday = @Birthday,"
                        + " BirthPlace = @BirthPlace,"
                        + " MaritalStatusKey = @MaritalStatusKey,"
                        + " MaritalStatusName = @MaritalStatusName,"
                        + " Nationality = @Nationality,"
                        + " Ethnicity = @Ethnicity,"
                        + " PassportNumber = @PassportNumber,"
                        + " IssueDate = @IssueDate,"
                        + " ExpireDate = @ExpireDate,"
                        + " IssuePlace = @IssuePlace,"
                        + " PermanentAddress = @PermanentAddress,"
                        + " TemporaryAddress = @TemporaryAddress,"
                        + " ContactAddress = @ContactAddress,"
                        + " MobiPhone = @MobiPhone,"
                        + " Email = @Email,"
                        + " TaxNumber = @TaxNumber,"
                        + " BankAccount = @BankAccount,"
                        + " BankName = @BankName,"
                        + " PhotoPath = @PhotoPath,"
                        + " Note = @Note,"
                        + " RecordStatus = @RecordStatus,"
                        + " Style = @Style,"
                        + " Class = @Class,"
                        + " CodeLine = @CodeLine,"
                        + " ModifiedOn = GetDate(),"
                        + " ModifiedBy = @ModifiedBy,"
                        + " ModifiedName = @ModifiedName"
                       + " WHERE EmployeeKey = @EmployeeKey";
            string zResult = "";
            string zConnectionString = TNS.DBConnection.Connecting.SQL_MainDatabase;
            SqlConnection zConnect = new SqlConnection(zConnectionString);
            zConnect.Open();
            try
            {
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.CommandType = CommandType.Text;
                if (_EmployeeKey.Length == 36)
                    zCommand.Parameters.Add("@EmployeeKey", SqlDbType.UniqueIdentifier).Value = new Guid(_EmployeeKey);
                else
                    zCommand.Parameters.Add("@EmployeeKey", SqlDbType.UniqueIdentifier).Value = DBNull.Value;
                zCommand.Parameters.Add("@EmployeeID", SqlDbType.NVarChar).Value = _EmployeeID;
                zCommand.Parameters.Add("@LastName", SqlDbType.NVarChar).Value = _LastName;
                zCommand.Parameters.Add("@FirstName", SqlDbType.NVarChar).Value = _FirstName;
                zCommand.Parameters.Add("@CadresID", SqlDbType.NVarChar).Value = _CadresID;
                zCommand.Parameters.Add("@UserIPCAST", SqlDbType.NVarChar).Value = _UserIPCAST;
                zCommand.Parameters.Add("@NickName", SqlDbType.NVarChar).Value = _NickName;
                if (_ReportToKey.Length == 36)
                    zCommand.Parameters.Add("@ReportToKey", SqlDbType.UniqueIdentifier).Value = new Guid(_ReportToKey);
                else
                    zCommand.Parameters.Add("@ReportToKey", SqlDbType.UniqueIdentifier).Value = DBNull.Value;
                zCommand.Parameters.Add("@ReportToName", SqlDbType.NVarChar).Value = _ReportToName;
                if (_DepartmentKey.Length == 36)
                    zCommand.Parameters.Add("@DepartmentKey", SqlDbType.UniqueIdentifier).Value = new Guid(_DepartmentKey);
                else
                    zCommand.Parameters.Add("@DepartmentKey", SqlDbType.UniqueIdentifier).Value = DBNull.Value;
                zCommand.Parameters.Add("@OrganizationPath", SqlDbType.NVarChar).Value = _OrganizationPath;
                zCommand.Parameters.Add("@DepartmentName", SqlDbType.NVarChar).Value = _DepartmentName;
                if (_BranchKey.Length == 36)
                    zCommand.Parameters.Add("@BranchKey", SqlDbType.UniqueIdentifier).Value = new Guid(_BranchKey);
                else
                    zCommand.Parameters.Add("@BranchKey", SqlDbType.UniqueIdentifier).Value = DBNull.Value;
                zCommand.Parameters.Add("@BranchName", SqlDbType.NVarChar).Value = _BranchName;
                if (_JobKey.Length == 36)
                    zCommand.Parameters.Add("@JobKey", SqlDbType.UniqueIdentifier).Value = new Guid(_JobKey);
                else
                    zCommand.Parameters.Add("@JobKey", SqlDbType.UniqueIdentifier).Value = DBNull.Value;
                zCommand.Parameters.Add("@JobName", SqlDbType.NVarChar).Value = _JobName;
                zCommand.Parameters.Add("@PositionKey", SqlDbType.Int).Value = _PositionKey;
                zCommand.Parameters.Add("@PositionName", SqlDbType.NVarChar).Value = _PositionName;
                zCommand.Parameters.Add("@WorkingStatusKey", SqlDbType.Int).Value = _WorkingStatusKey;
                zCommand.Parameters.Add("@WorkingStatusName", SqlDbType.NVarChar).Value = _WorkingStatusName;
                if (_StartingDate == null)
                    zCommand.Parameters.Add("@StartingDate", SqlDbType.Date).Value = DBNull.Value;
                else
                    zCommand.Parameters.Add("@StartingDate", SqlDbType.Date).Value = _StartingDate;
                if (_LeavingDate == null)
                    zCommand.Parameters.Add("@LeavingDate", SqlDbType.Date).Value = DBNull.Value;
                else
                    zCommand.Parameters.Add("@LeavingDate", SqlDbType.Date).Value = _LeavingDate;
                zCommand.Parameters.Add("@ContractTypeKey", SqlDbType.Int).Value = _ContractTypeKey;
                zCommand.Parameters.Add("@ContractTypeName", SqlDbType.NVarChar).Value = _ContractTypeName;
                zCommand.Parameters.Add("@WorkingAddress", SqlDbType.NVarChar).Value = _WorkingAddress;
                zCommand.Parameters.Add("@WorkingLocation", SqlDbType.NVarChar).Value = _WorkingLocation;
                zCommand.Parameters.Add("@CompanyPhone", SqlDbType.NVarChar).Value = _CompanyPhone;
                zCommand.Parameters.Add("@CompanyEmail", SqlDbType.NVarChar).Value = _CompanyEmail;
                zCommand.Parameters.Add("@Gender", SqlDbType.Int).Value = _Gender;
                if (_Birthday == null)
                    zCommand.Parameters.Add("@Birthday", SqlDbType.DateTime).Value = DBNull.Value;
                else
                    zCommand.Parameters.Add("@Birthday", SqlDbType.DateTime).Value = _Birthday;
                zCommand.Parameters.Add("@BirthPlace", SqlDbType.NVarChar).Value = _BirthPlace;
                zCommand.Parameters.Add("@MaritalStatusKey", SqlDbType.Int).Value = _MaritalStatusKey;
                zCommand.Parameters.Add("@MaritalStatusName", SqlDbType.NVarChar).Value = _MaritalStatusName;
                zCommand.Parameters.Add("@Nationality", SqlDbType.NVarChar).Value = _Nationality;
                zCommand.Parameters.Add("@Ethnicity", SqlDbType.NVarChar).Value = _Ethnicity;
                zCommand.Parameters.Add("@PassportNumber", SqlDbType.NVarChar).Value = _PassportNumber;
                if (_IssueDate == null)
                    zCommand.Parameters.Add("@IssueDate", SqlDbType.DateTime).Value = DBNull.Value;
                else
                    zCommand.Parameters.Add("@IssueDate", SqlDbType.DateTime).Value = _IssueDate;
                if (_ExpireDate == null)
                    zCommand.Parameters.Add("@ExpireDate", SqlDbType.DateTime).Value = DBNull.Value;
                else
                    zCommand.Parameters.Add("@ExpireDate", SqlDbType.DateTime).Value = _ExpireDate;
                zCommand.Parameters.Add("@IssuePlace", SqlDbType.NVarChar).Value = _IssuePlace;
                zCommand.Parameters.Add("@PermanentAddress", SqlDbType.NVarChar).Value = _PermanentAddress;
                zCommand.Parameters.Add("@TemporaryAddress", SqlDbType.NVarChar).Value = _TemporaryAddress;
                zCommand.Parameters.Add("@ContactAddress", SqlDbType.NVarChar).Value = _ContactAddress;
                zCommand.Parameters.Add("@MobiPhone", SqlDbType.NVarChar).Value = _MobiPhone;
                zCommand.Parameters.Add("@Email", SqlDbType.NVarChar).Value = _Email;
                zCommand.Parameters.Add("@TaxNumber", SqlDbType.NVarChar).Value = _TaxNumber;
                zCommand.Parameters.Add("@BankAccount", SqlDbType.NVarChar).Value = _BankAccount;
                zCommand.Parameters.Add("@BankName", SqlDbType.NVarChar).Value = _BankName;
                zCommand.Parameters.Add("@PhotoPath", SqlDbType.NVarChar).Value = _PhotoPath;
                zCommand.Parameters.Add("@Note", SqlDbType.NText).Value = _Note;
                zCommand.Parameters.Add("@RecordStatus", SqlDbType.Int).Value = _RecordStatus;
                zCommand.Parameters.Add("@Style", SqlDbType.NChar).Value = _Style;
                zCommand.Parameters.Add("@Class", SqlDbType.NChar).Value = _Class;
                zCommand.Parameters.Add("@CodeLine", SqlDbType.NChar).Value = _CodeLine;
                if (_ModifiedBy.Length == 36)
                    zCommand.Parameters.Add("@ModifiedBy", SqlDbType.UniqueIdentifier).Value = new Guid(_ModifiedBy);
                else
                    zCommand.Parameters.Add("@ModifiedBy", SqlDbType.UniqueIdentifier).Value = DBNull.Value;
                zCommand.Parameters.Add("@ModifiedName", SqlDbType.NVarChar).Value = _ModifiedName;
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

        public string Create_Basic()
        {
            _EmployeeKey = Guid.NewGuid().ToString();
            //---------- String SQL Access Database ---------------
            string zSQL = "INSERT INTO [dbo].[HRM_Employee] ("
                        + " EmployeeKey ,EmployeeID ,LastName ,FirstName ,ReportToKey ,ReportToName ,DepartmentKey ,DepartmentName ,JobName ,PositionKey ,PositionName,RecordStatus ,CreatedBy ,CreatedName ,ModifiedBy ,ModifiedName ) "
                        + " VALUES ( "
                        + "@EmployeeKey ,@EmployeeID ,@LastName ,@FirstName ,@ReportToKey ,@ReportToName ,@DepartmentKey ,@DepartmentName ,@JobName ,@PositionKey ,@PositionName,@RecordStatus ,@CreatedBy ,@CreatedName ,@ModifiedBy ,@ModifiedName) ";
            string zResult = "";
            string zConnectionString = TNS.DBConnection.Connecting.SQL_MainDatabase;
            SqlConnection zConnect = new SqlConnection(zConnectionString);
            zConnect.Open();
            try
            {
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.CommandType = CommandType.Text;
                zCommand.Parameters.Add("@EmployeeKey", SqlDbType.UniqueIdentifier).Value = new Guid(_EmployeeKey);
                zCommand.Parameters.Add("@EmployeeID", SqlDbType.NVarChar).Value = _EmployeeID;
                zCommand.Parameters.Add("@LastName", SqlDbType.NVarChar).Value = _LastName;
                zCommand.Parameters.Add("@FirstName", SqlDbType.NVarChar).Value = _FirstName;
                if (_ReportToKey.Length == 36)
                    zCommand.Parameters.Add("@ReportToKey", SqlDbType.UniqueIdentifier).Value = new Guid(_ReportToKey);
                else
                    zCommand.Parameters.Add("@ReportToKey", SqlDbType.UniqueIdentifier).Value = DBNull.Value;
                zCommand.Parameters.Add("@ReportToName", SqlDbType.NVarChar).Value = _ReportToName;
                if (_DepartmentKey.Length == 36)
                    zCommand.Parameters.Add("@DepartmentKey", SqlDbType.UniqueIdentifier).Value = new Guid(_DepartmentKey);
                else
                    zCommand.Parameters.Add("@DepartmentKey", SqlDbType.UniqueIdentifier).Value = DBNull.Value;
                zCommand.Parameters.Add("@DepartmentName", SqlDbType.NVarChar).Value = _DepartmentName;
                zCommand.Parameters.Add("@JobName", SqlDbType.NVarChar).Value = _JobName;
                zCommand.Parameters.Add("@PositionKey", SqlDbType.Int).Value = _PositionKey;
                zCommand.Parameters.Add("@PositionName", SqlDbType.NVarChar).Value = _PositionName;
                zCommand.Parameters.Add("@ContractTypeKey", SqlDbType.Int).Value = _ContractTypeKey;
                zCommand.Parameters.Add("@RecordStatus", SqlDbType.Int).Value = _RecordStatus;
                if (_CreatedBy.Length == 36)
                    zCommand.Parameters.Add("@CreatedBy", SqlDbType.UniqueIdentifier).Value = new Guid(_CreatedBy);
                else
                    zCommand.Parameters.Add("@CreatedBy", SqlDbType.UniqueIdentifier).Value = DBNull.Value;
                zCommand.Parameters.Add("@CreatedName", SqlDbType.NVarChar).Value = _CreatedName;
                if (_ModifiedBy.Length == 36)
                    zCommand.Parameters.Add("@ModifiedBy", SqlDbType.UniqueIdentifier).Value = new Guid(_ModifiedBy);
                else
                    zCommand.Parameters.Add("@ModifiedBy", SqlDbType.UniqueIdentifier).Value = DBNull.Value;
                zCommand.Parameters.Add("@ModifiedName", SqlDbType.NVarChar).Value = _ModifiedName;
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
        public string Update_Basic()
        {
            string zSQL = "UPDATE [dbo].[HRM_Employee] SET "
                        + " EmployeeID = @EmployeeID,"
                        + " LastName = @LastName,"
                        + " FirstName = @FirstName,"
                        + " ReportToKey = @ReportToKey,"
                        + " ReportToName = @ReportToName,"
                        + " DepartmentKey = @DepartmentKey,"
                        + " DepartmentName = @DepartmentName,"
                        + " JobName = @JobName,"
                        + " PositionKey = @PositionKey,"
                        + " PositionName = @PositionName,"
                        + " ModifiedOn = GetDate(),"
                        + " ModifiedBy = @ModifiedBy,"
                        + " ModifiedName = @ModifiedName"
                       + " WHERE EmployeeKey = @EmployeeKey";
            string zResult = "";
            string zConnectionString = TNS.DBConnection.Connecting.SQL_MainDatabase;
            SqlConnection zConnect = new SqlConnection(zConnectionString);
            zConnect.Open();
            try
            {
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.CommandType = CommandType.Text;
                zCommand.Parameters.Add("@EmployeeKey", SqlDbType.UniqueIdentifier).Value = new Guid(_EmployeeKey);
                zCommand.Parameters.Add("@EmployeeID", SqlDbType.NVarChar).Value = _EmployeeID;
                zCommand.Parameters.Add("@LastName", SqlDbType.NVarChar).Value = _LastName;
                zCommand.Parameters.Add("@FirstName", SqlDbType.NVarChar).Value = _FirstName;
                if (_ReportToKey.Length == 36)
                    zCommand.Parameters.Add("@ReportToKey", SqlDbType.UniqueIdentifier).Value = new Guid(_ReportToKey);
                else
                    zCommand.Parameters.Add("@ReportToKey", SqlDbType.UniqueIdentifier).Value = DBNull.Value;
                zCommand.Parameters.Add("@ReportToName", SqlDbType.NVarChar).Value = _ReportToName;
                if (_DepartmentKey.Length == 36)
                    zCommand.Parameters.Add("@DepartmentKey", SqlDbType.UniqueIdentifier).Value = new Guid(_DepartmentKey);
                else
                    zCommand.Parameters.Add("@DepartmentKey", SqlDbType.UniqueIdentifier).Value = DBNull.Value;
                zCommand.Parameters.Add("@DepartmentName", SqlDbType.NVarChar).Value = _DepartmentName;
                zCommand.Parameters.Add("@BranchName", SqlDbType.NVarChar).Value = _BranchName;
                zCommand.Parameters.Add("@JobName", SqlDbType.NVarChar).Value = _JobName;
                zCommand.Parameters.Add("@PositionKey", SqlDbType.Int).Value = _PositionKey;
                zCommand.Parameters.Add("@PositionName", SqlDbType.NVarChar).Value = _PositionName;
                if (_ModifiedBy.Length == 36)
                    zCommand.Parameters.Add("@ModifiedBy", SqlDbType.UniqueIdentifier).Value = new Guid(_ModifiedBy);
                else
                    zCommand.Parameters.Add("@ModifiedBy", SqlDbType.UniqueIdentifier).Value = DBNull.Value;
                zCommand.Parameters.Add("@ModifiedName", SqlDbType.NVarChar).Value = _ModifiedName;
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
        public string Update_Avatar()
        {
            string zSQL = "UPDATE [dbo].[HRM_Employee] SET "
                        + " PhotoPath = @PhotoPath"
                        + " WHERE EmployeeKey = @EmployeeKey";
            string zResult = "";
            string zConnectionString = TNS.DBConnection.Connecting.SQL_MainDatabase;
            SqlConnection zConnect = new SqlConnection(zConnectionString);
            zConnect.Open();
            try
            {
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.CommandType = CommandType.Text;
                zCommand.Parameters.Add("@EmployeeKey", SqlDbType.UniqueIdentifier).Value = new Guid(_EmployeeKey);
                zCommand.Parameters.Add("@PhotoPath", SqlDbType.NVarChar).Value = _PhotoPath;

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
        public string Update_PersonalInfo()
        {
            string zSQL = "UPDATE [dbo].[HRM_Employee] SET "
                        + " LastName = @LastName,"
                        + " FirstName = @FirstName,"
                        + " Gender = @Gender,"
                        + " Birthday = @Birthday,"
                        + " MaritalStatusKey = @MaritalStatusKey,"
                        + " MaritalStatusName = @MaritalStatusName,"
                        + " MobiPhone = @MobiPhone,"
                        + " Email = @Email,"
                        + " ModifiedOn = GetDate(),"
                        + " ModifiedBy = @ModifiedBy,"
                        + " ModifiedName = @ModifiedName"
                       + " WHERE EmployeeKey = @EmployeeKey";
            string zResult = "";
            string zConnectionString = TNS.DBConnection.Connecting.SQL_MainDatabase;
            SqlConnection zConnect = new SqlConnection(zConnectionString);
            zConnect.Open();
            try
            {
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.CommandType = CommandType.Text;
                zCommand.Parameters.Add("@EmployeeKey", SqlDbType.UniqueIdentifier).Value = new Guid(_EmployeeKey);
                zCommand.Parameters.Add("@LastName", SqlDbType.NVarChar).Value = _LastName;
                zCommand.Parameters.Add("@FirstName", SqlDbType.NVarChar).Value = _FirstName;
                if (_Birthday == null)
                    zCommand.Parameters.Add("@Birthday", SqlDbType.Date).Value = DBNull.Value;
                else
                    zCommand.Parameters.Add("@Birthday", SqlDbType.Date).Value = _Birthday;
                zCommand.Parameters.Add("@Gender", SqlDbType.Int).Value = _Gender;
                zCommand.Parameters.Add("@MaritalStatusKey", SqlDbType.Int).Value = _MaritalStatusKey;
                zCommand.Parameters.Add("@MaritalStatusName", SqlDbType.NVarChar).Value = _MaritalStatusName;

                zCommand.Parameters.Add("@MobiPhone", SqlDbType.NVarChar).Value = _MobiPhone;

                zCommand.Parameters.Add("@Email", SqlDbType.NVarChar).Value = _Email;
                zCommand.Parameters.Add("@TaxNumber", SqlDbType.NVarChar).Value = _TaxNumber;
                if (_ModifiedBy.Length == 36)
                    zCommand.Parameters.Add("@ModifiedBy", SqlDbType.UniqueIdentifier).Value = new Guid(_ModifiedBy);
                else
                    zCommand.Parameters.Add("@ModifiedBy", SqlDbType.UniqueIdentifier).Value = DBNull.Value;
                zCommand.Parameters.Add("@ModifiedName", SqlDbType.NVarChar).Value = _ModifiedName;
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
        public string Update_WorkingInfo()
        {
            string zSQL = "UPDATE [dbo].[HRM_Employee] SET "
                        + " EmployeeID = @EmployeeID,"
                        + " UserIPCAST = @UserIPCAST,"
                        + " ReportToKey = @ReportToKey,"
                        + " ReportToName = @ReportToName,"
                        + " DepartmentKey = @DepartmentKey,"
                        + " DepartmentName = @DepartmentName,"
                        + " JobKey = @JobKey,"
                        + " JobName = @JobName,"
                        + " PositionKey = @PositionKey,"
                        + " PositionName = @PositionName,"
                        + " StartingDate = @StartingDate,"
                        + " ContractTypeKey = @ContractTypeKey,"
                        + " ContractTypeName = @ContractTypeName,"
                        + " CompanyEmail = @CompanyEmail,"
                        + " ModifiedOn = GetDate(),"
                        + " ModifiedBy = @ModifiedBy,"
                        + " ModifiedName = @ModifiedName"
                       + " WHERE EmployeeKey = @EmployeeKey";
            string zResult = "";
            string zConnectionString = TNS.DBConnection.Connecting.SQL_MainDatabase;
            SqlConnection zConnect = new SqlConnection(zConnectionString);
            zConnect.Open();
            try
            {
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.CommandType = CommandType.Text;
                zCommand.Parameters.Add("@EmployeeKey", SqlDbType.UniqueIdentifier).Value = new Guid(_EmployeeKey);
                zCommand.Parameters.Add("@EmployeeID", SqlDbType.NVarChar).Value = _EmployeeID;
                zCommand.Parameters.Add("@UserIPCAST", SqlDbType.NVarChar).Value = _UserIPCAST;
                if (_ReportToKey.Length == 36)
                    zCommand.Parameters.Add("@ReportToKey", SqlDbType.UniqueIdentifier).Value = new Guid(_ReportToKey);
                else
                    zCommand.Parameters.Add("@ReportToKey", SqlDbType.UniqueIdentifier).Value = DBNull.Value;
                zCommand.Parameters.Add("@ReportToName", SqlDbType.NVarChar).Value = _ReportToName;
                if (_DepartmentKey.Length == 36)
                    zCommand.Parameters.Add("@DepartmentKey", SqlDbType.UniqueIdentifier).Value = new Guid(_DepartmentKey);
                else
                    zCommand.Parameters.Add("@DepartmentKey", SqlDbType.UniqueIdentifier).Value = DBNull.Value;
                zCommand.Parameters.Add("@DepartmentName", SqlDbType.NVarChar).Value = _DepartmentName;
                if (_JobKey.Length == 36)
                    zCommand.Parameters.Add("@JobKey", SqlDbType.UniqueIdentifier).Value = new Guid(_JobKey);
                else
                    zCommand.Parameters.Add("@JobKey", SqlDbType.UniqueIdentifier).Value = DBNull.Value;
                zCommand.Parameters.Add("@JobName", SqlDbType.NVarChar).Value = _JobName;
                zCommand.Parameters.Add("@PositionKey", SqlDbType.Int).Value = _PositionKey;
                zCommand.Parameters.Add("@PositionName", SqlDbType.NVarChar).Value = _PositionName;
                if (_StartingDate == null)
                    zCommand.Parameters.Add("@StartingDate", SqlDbType.Date).Value = DBNull.Value;
                else
                    zCommand.Parameters.Add("@StartingDate", SqlDbType.Date).Value = _StartingDate;
                zCommand.Parameters.Add("@ContractTypeKey", SqlDbType.Int).Value = _ContractTypeKey;
                zCommand.Parameters.Add("@ContractTypeName", SqlDbType.NVarChar).Value = _ContractTypeName;
                zCommand.Parameters.Add("@CompanyEmail", SqlDbType.NVarChar).Value = _CompanyEmail;
                if (_ModifiedBy.Length == 36)
                    zCommand.Parameters.Add("@ModifiedBy", SqlDbType.UniqueIdentifier).Value = new Guid(_ModifiedBy);
                else
                    zCommand.Parameters.Add("@ModifiedBy", SqlDbType.UniqueIdentifier).Value = DBNull.Value;
                zCommand.Parameters.Add("@ModifiedName", SqlDbType.NVarChar).Value = _ModifiedName;
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
            string zSQL = "UPDATE [dbo].[HRM_Employee] Set RecordStatus = 99 WHERE EmployeeKey = @EmployeeKey";
            string zConnectionString = TNS.DBConnection.Connecting.SQL_MainDatabase;
            SqlConnection zConnect = new SqlConnection(zConnectionString);
            zConnect.Open();
            try
            {
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.Parameters.Add("@EmployeeKey", SqlDbType.UniqueIdentifier).Value = new Guid(_EmployeeKey);
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
            string zSQL = "DELETE FROM [dbo].[HRM_Employee] WHERE EmployeeKey = @EmployeeKey";
            string zConnectionString = TNS.DBConnection.Connecting.SQL_MainDatabase;
            SqlConnection zConnect = new SqlConnection(zConnectionString);
            zConnect.Open();
            try
            {
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.Parameters.Add("@EmployeeKey", SqlDbType.UniqueIdentifier).Value = new Guid(_EmployeeKey);
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
