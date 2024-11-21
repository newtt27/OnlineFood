namespace OnlineFood.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using OnlineFood.Data;
    using Microsoft.EntityFrameworkCore;
    using OnlineFood.Models;

    public class PaymentDetailsController : Controller
    {

        private readonly OnlineFoodContext _context;

        //public PaymentDetailsController(OnlineFoodContext context)
        //{
        //    _context = context;
        //}

        //[HttpGet]
        //public async Task<IActionResult> GetDetails(int id)
        //{
        //    var payment = await _context.Paymentdetail
        //        .Include(p => p.IdKhachhangNavigation)
        //        .Include(p => p.IdSanphamNavigation)
        //        .Include(p => p.IdKuyenmaiNavigation)
        //        .Include(p => p.IdPaymentNavigation)
        //        .FirstOrDefaultAsync(p => p.Id == id);

        //    if (payment == null)
        //    {
        //        return NotFound();
        //    }

        //    var viewModel = new PaymentDetailsViewModel
        //    {
        //        OrderId = payment.Id.ToString(),
        //        OrderDate = payment.Date.ToDateTime(TimeOnly.MinValue),
        //        CustomerName = payment.HotenKhachhang,
        //        CustomerEmail = payment.IdKhachhangNavigation.Email,
        //        CustomerContact = payment.IdKhachhangNavigation.Phone,
        //        Items = new List<OrderItemViewModel>
        //    {
        //        new OrderItemViewModel
        //        {
        //            ProductName = payment.IdSanphamNavigation.Ten,
        //            Quantity = payment.Soluong,
        //            UnitPrice = payment.GiatienTruoc
        //        }
        //    },
        //        SubTotal = payment.GiatienTruoc,
        //        DiscountPercent = payment.IdKuyenmaiNavigation?.PhanTram ?? 0,
        //        TotalPrice = payment.GiaTienSau,
        //        PaymentMethod = payment.PaymentMode,
        //        Status = payment.IdPaymentNavigation?.Status ?? "Pending",
        //        Promotions = payment.IdKuyenmaiNavigation != null
        //            ? new List<PromotionViewModel>
        //            {
        //            new PromotionViewModel
        //            {
        //                Code = payment.IdKuyenmaiNavigation.Code,
        //                DiscountAmount = payment.GiatienTruoc - payment.GiaTienSau
        //            }
        //            }
        //            : new List<PromotionViewModel>()
        //    };

        //    return Json(viewModel);
        //}
    }
}
