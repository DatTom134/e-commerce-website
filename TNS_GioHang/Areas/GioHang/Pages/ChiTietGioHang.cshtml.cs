using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.VisualBasic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Security.Claims;
using TNS_GioHang.Areas.GioHang.Models;

namespace TNS_GioHang.Areas.GioHang.Pages
{
    [IgnoreAntiforgeryToken]
    public class ChiTietGioHangModel : PageModel
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
		public IActionResult OnPostGetCart([FromBody] UserRequest request)
		{
			List<string[]> Cart = AcessData.GetCart(request.UserID);

			if (Cart == null || Cart.Count == 0)
			{
				bool isCreated = AcessData.CreateCart(request.UserID);
				if (isCreated)
				{
					Cart = AcessData.GetCart(request.UserID);
				}
				else
				{
					return BadRequest(new { message = "Failed to get cart." });
				}
			}			
			return new JsonResult(Cart);
		}

		public IActionResult OnPostGetCartDetails([FromBody] UserRequest request)
		{			
			List<string[]> Cart = AcessData.GetCart(request.UserID);
			
			if (Cart == null || Cart.Count == 0)
			{				
				bool isCreated = AcessData.CreateCart(request.UserID);
				if (isCreated)
				{					
					Cart = AcessData.GetCart(request.UserID);
				}
				else
				{
					return BadRequest(new { message = "Failed to get cart details." });
				}
			}			
			List<string[]> CartDetails = AcessData.GetCartDetails(Cart[0][0]);
			return new JsonResult(CartDetails);
		}

		public IActionResult OnPostUpdateCartQuantity([FromBody] UpdateRequest request)
		{

			bool isUpdated = AcessData.UpdateCartQuantity(request.CartKey, request.ProductKey, request.Quantity);

			if (isUpdated)
			{				
				List<string[]> CartDetails = AcessData.GetCartDetails(request.CartKey);
				return new JsonResult(CartDetails);
			}
			else
			{				
				return BadRequest(new { message = "Failed to update cart quantity" });
			}
		}

		public IActionResult OnPostAddToCart([FromBody] AddRequest request)
		{
			try
			{
				Console.WriteLine("Product Key:", request.ProductKey);
				Console.WriteLine("Quantity:", request.Quantity);
				Console.WriteLine("Price:", request.Price);
				Console.WriteLine(request.CartKey != null ? request.CartKey : "cái này bị lỗi rồi nha");
				bool IsAdded = AcessData.AddToCart(request.CartKey, request.ProductKey, request.Quantity, request.Price);

				if (IsAdded)
				{
					List<string[]> CartDetails = AcessData.GetCartDetails(request.CartKey);
					return new JsonResult(CartDetails);
				}
				else
				{
					return BadRequest(new { message = "Failed to update cart quantity" });
				}
			}
			catch (Exception e)
			{
				Console.WriteLine("Error:", e.Message);
			}
			return new JsonResult(new { success = false });
			
		}
		public IActionResult OnPostDeleteItem([FromBody] DeleteRequest request)
		{
			bool IsDeleted = AcessData.DeleteItem(request.CartKey, request.ProductKey);

			if (IsDeleted)
			{
				List<string[]> CartDetails = AcessData.GetCartDetails(request.CartKey);
				return new JsonResult(CartDetails);
			}
			else
			{
				return BadRequest(new { message = "Failed to delete" });
			}
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
            public string UserID { get; set; }
        }		
        public class UpdateRequest
        {
			public string CartKey {  get; set; }
            public string ProductKey {  get; set; }
            public int Quantity {  get; set; }

		}
		public class AddRequest
		{
			public string CartKey { get; set; }
			public string ProductKey { get; set; }
			public int Quantity { get; set; }
			public float Price {  get; set; }
		}
		public class DeleteRequest
		{
			public string CartKey { get; set; }
			public string ProductKey { get; set; }			
		}
	}

}
