using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.VisualBasic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Security.Claims;
using TNS_Product.Areas.Product.Models;

namespace TNS_Product.Areas.Product.Pages
{
    [IgnoreAntiforgeryToken]
    public class TimKiemSanPhamModel : PageModel
    {
		public IActionResult OnPostLoadData1([FromBody] ItemRequest request)
		{

			DateTime zFromDate, zToDate;
			List<string[]> zData = new List<string[]>();
			zData = AcessData.ListProduct();
			return new JsonResult(zData);
		}
		public IActionResult OnPostGetUser([FromBody] UserRequest request)
		{
			List<string[]> zData = new List<string[]>();
			zData = AcessData.GetUser(request.Email);
			return new JsonResult(zData);
		}		
		public IActionResult OnPostSearchItem([FromBody] ProductRequest request)
		{
			List<string[]> zData = new List<string[]>();
			zData = AcessData.GetItemByName(request.ProductName);
			return new JsonResult(zData);
		}
		public class ProductRequest
		{
			public string ProductName { get; set; }
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
		public class UserRequest
		{
			public string Email { get; set; }
		}
	}

}
