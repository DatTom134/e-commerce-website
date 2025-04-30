using ChiTietSanPham.Areas.ChiTietSanPham.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.VisualBasic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Security.Claims;

namespace ChiTietSanPham.Areas.ChiTietSanPham.Pages
{
	[IgnoreAntiforgeryToken]
	public class TaskToDoModel : PageModel
	{
		public string ProductId { get; set; }

		List<string[]> product { get; set; }

		List<string[]> RelatedProducts { get; set; }

		public void OnGet(string id)
		{
			ProductId = id;

		}

		public JsonResult OnPostProductData(string id)
		{
			product = AcessData.getProduct(id);

			if (product != null || !product.Any())
			{
				OnPostRelatedProductsData(id);
				return new JsonResult(new { product });
			}

			return new JsonResult(new { error = "Sản phẩm không tồn tại" });
		}

		public JsonResult OnPostRelatedProductsData(string id)
		{
			RelatedProducts = AcessData.getProducts();


			if (RelatedProducts != null || !RelatedProducts.Any())
			{
				return new JsonResult(new { RelatedProducts });
			}

			return new JsonResult(new { error = "Sản phẩm không tồn tại" });
		}




	}
}
