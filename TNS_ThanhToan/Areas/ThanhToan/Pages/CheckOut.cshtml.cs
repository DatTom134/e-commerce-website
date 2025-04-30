using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using TNS_ThanhToan.Areas.ThanhToan.Models;

namespace TNS_ThanhToan.Areas.ThanhToan.Pages
{
    [IgnoreAntiforgeryToken]
    public class CheckOutModel : PageModel
    {

        // Thuộc tính lưu trữ thông tin người dùng
        //public List<string[]> OrderDetails { get; set; }

        // Phương thức OnGet: Kiểm tra thông tin đăng nhập và tải chi tiết đơn hàng
        public void OnGet()
        {

        }

        // Phương thức OnPostCheckLogin: Kiểm tra trạng thái đăng nhập
        public IActionResult OnPostCheckLogin()
        {
            try
            {
                // Lấy UserID từ Claims
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var email = User.FindFirst(ClaimTypes.Email)?.Value;
                var username = User.FindFirst(ClaimTypes.Name)?.Value;

                // Kiểm tra nếu UserID không tồn tại (người dùng chưa đăng nhập)
                if (string.IsNullOrEmpty(userId))
                {
                    return new JsonResult(new
                    {
                        success = false,
                        isAuthenticated = false,
                        message = "Người dùng chưa đăng nhập."
                    });
                }

                // Trả về thông tin người dùng
                return new JsonResult(new
                {
                    success = true,
                    isAuthenticated = true,
                    userId = userId,
                    email = email ?? "Không xác định",
                    username = username ?? "Không xác định"
                });
            }
            catch (Exception ex)
            {
                // Xử lý lỗi
                return new JsonResult(new
                {
                    success = false,
                    isAuthenticated = false,
                    message = "Đã xảy ra lỗi: " + ex.Message
                });
            }
        }

        // Phương thức OnPostCheckOut: Xử lý thanh toán
        //[HttpPost]
        //public IActionResult OnPostSubmitOrder([FromBody] OrderRequest request)
        //{
        //    if (request == null)
        //    {
        //        return BadRequest(new { success = false, message = "Invalid data received." });
        //    }

        //    if (request.TotalAmount < 0)
        //    {
        //        return BadRequest(new { success = false, message = "Invalid total amount." });
        //    }

        //    //if (!AcessData.AddOrder(request.UserID, request.ShippingAddress, request.TotalAmount))
        //    //{
        //    //    return new JsonResult(new { success = false, message = "Add Order Failed." });   
        //    //}

        //    Console.WriteLine(request.UserID);
        //    Console.WriteLine(request.ShippingAddress);
        //    Console.WriteLine(request.TotalAmount);
        //    Console.WriteLine($"Phướng thức thanh toán của bạn là: {request.PaymentMethod}");

        //    if (request.PaymentMethod == "1")
        //    {

        //        return new JsonResult(new { success = true, message = "Order processed successfully." });
        //    }
        //}


    }
    //public class OrderRequest
    //{
    //    public string UserID { get; set; }
    //    public string ShippingAddress { get; set; }
    //    public float TotalAmount { get; set; }
    //    public string PaymentMethod { get; set; }
    //}
}
