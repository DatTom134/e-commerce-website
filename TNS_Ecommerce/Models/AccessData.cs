using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TNS_Ecommerce.Models
{
    public class AccessData
    {
		public static List<string[]>ListProduct()
		{
			string zMessage = "";
			string zSQL = "SELECT * " +
				" FROM [dbo].[Products] ";
			DataTable zTable = new DataTable();
			string zConnectionString = TNS.DBConnection.Connecting.SQL_MainDatabase;
			try
			{
				SqlConnection zConnect = new SqlConnection(zConnectionString);
				zConnect.Open();
				SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
				zCommand.CommandType = CommandType.Text;
				//zCommand.Parameters.Add("@StatusKey", SqlDbType.Int).Value = StatusKey;
				//zCommand.Parameters.Add("@Search", SqlDbType.NVarChar).Value = "%" + Search + "%";
				////Test
				////zCommand.Parameters.Add("@OnwerBy", SqlDbType.UniqueIdentifier).Value = new Guid(EmployeeKey);
				//zCommand.Parameters.Add("@OnwerBy", SqlDbType.UniqueIdentifier).Value = new Guid(EmployeeKey);
				//zCommand.Parameters.Add("@PageSize", SqlDbType.Int).Value = PageSize;
				//zCommand.Parameters.Add("@PageNumber", SqlDbType.Int).Value = PageNumber;
				SqlDataAdapter zAdapter = new SqlDataAdapter(zCommand);
				zAdapter.Fill(zTable);
				zCommand.Dispose();
				zConnect.Close();
			}
			catch (Exception ex)
			{
				zMessage = ex.ToString();
			}
			List<string[]> zResult = new List<string[]>();
			string[] zItem;
			if (zMessage.Length > 0)
			{
				zItem = new string[4];
				zItem[0] = "ERR";
				zItem[1] = "zMessage";
				zResult.Add(zItem);
			}
			int n = zTable.Columns.Count;
			foreach (DataRow zRow in zTable.Rows)
			{
				zItem = new string[n];
				for (int i = 0; i < n; i++)
				{
					zItem[i] = zRow[i].ToString();
				}
				zResult.Add(zItem);
			}

			return zResult;
		}		

	}
}
