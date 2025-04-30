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
    public class SanPhamModel : PageModel
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
		public IActionResult OnPostSearchItem([FromBody] ProductRequest request)
		{
			List<string[]> zData = new List<string[]>();
			zData = AcessData.GetItemByName(request.ProductName);
			return new JsonResult(zData);
		}
        public IActionResult OnPostGetStock([FromBody] StockRequest request)
        {
            List<string[]> zData = new List<string[]>();
            zData = AcessData.getStock(request.ProductKey);
            return new JsonResult(zData);
        }
        public class StockRequest
        {
			public string ProductKey { get; set; }
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
