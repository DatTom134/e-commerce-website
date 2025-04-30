using DangKy.Areas.DangKy.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.VisualBasic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Security.Claims;

namespace DangKy.Areas.DangKy.Pages
{
	[IgnoreAntiforgeryToken]
	public class LoginModel : PageModel
	{
		public IActionResult OnPostLoadData([FromBody] ItemRequest request)
		{

			DateTime zFromDate, zToDate;
			List<string[]> zData = new List<string[]>();
			if (request.FromDate.Trim().Length > 0 && request.ToDate.Trim().Length > 0)
			{
				zFromDate = DateTime.Parse(request.FromDate);
				zToDate = DateTime.Parse(request.ToDate);
				zData = AcessData.TargetResultByDepartment(1, "2");
			}
			else
			{
				zData = AcessData.TargetResultByDepartment(1, "2");
			}
			return new JsonResult(zData);
		}
		public class ItemRequest
		{
			public string FromDate { get; set; }
			public string ToDate { get; set; }
			public string Search { get; set; }
			public string StatusKey { get; set; }
			public int PageSize { get; set; }
			public int PageNumber { get; set; }
		}

	}

}
