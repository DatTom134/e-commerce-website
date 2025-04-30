using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.VisualBasic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Security.Claims;
using TNS_Product.Areas.Product.Models;

namespace ChiTietSanPham.Areas.ChiTietSanPham.Pages
{
	[IgnoreAntiforgeryToken]
	public class ChiTietSanPhamModel : PageModel
	{
		public string ProductId { get; set; }

		List<string[]> product { get; set; }

		List<string[]> RelatedProducts { get; set; }

		public void OnGet(string id)
		{
			ProductId = id;

		}

		[HttpPost]
		public IActionResult OnPostProductData([FromBody] string key)
		{
			Console.WriteLine("Product Key:", key);
			product = AcessData.getProduct(key);
			Console.WriteLine(product.Count);

			if (product != null || !product!.Any())
			{
				OnPostRelatedProductsData(key);
				return new JsonResult(new { success = true, data = product });
			}

			return new JsonResult(new { success = false, data = "Sản phẩm không tồn tại" });
		}

		[HttpPost]
		public IActionResult OnPostRelatedProductsData(string id)
		{
			RelatedProducts = AcessData.getProducts();


			if (RelatedProducts != null || !RelatedProducts.Any())
			{
				return new JsonResult(new { RelatedProducts });
			}

			return new JsonResult(new { error = "Sản phẩm không tồn tại" });
		}


		[HttpPost]
		public IActionResult OnPostAddToCart([FromBody] AddRequest request)
		{

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


		#region [ update product quantity in cart ]

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

		#endregion

	}
	public class AddRequest
	{
		public string CartKey { get; set; }
		public string ProductKey { get; set; }
		public int Quantity { get; set; }
		public float Price { get; set; }
	}

	public class UpdateRequest
	{
		public string CartKey { get; set; }
		public string ProductKey { get; set; }
		public int Quantity { get; set; }

	}
}
