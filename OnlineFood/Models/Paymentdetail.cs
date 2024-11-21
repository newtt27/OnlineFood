using System;
using System.Collections.Generic;

namespace OnlineFood.Models;

public partial class Paymentdetail
{
    public int Id { get; set; }

    public int IdKhachhang { get; set; }

    public int IdSanpham { get; set; }

    public int Soluong { get; set; }

    public int GiatienTruoc { get; set; }

    public string HotenKhachhang { get; set; } = null!;

    public DateOnly Date { get; set; }

    public string PaymentMode { get; set; } = null!;

    public int GiaTienSau { get; set; }

    public int? IdKuyenmai { get; set; }

    public int? IdPayment { get; set; }

    public virtual Customer IdKhachhangNavigation { get; set; } = null!;

    public virtual Event? IdKuyenmaiNavigation { get; set; }

    public virtual Payment? IdPaymentNavigation { get; set; }

    public virtual Product IdSanphamNavigation { get; set; } = null!;
}
public class PaymentDetailsViewModel
{
    public string OrderId { get; set; }
    public DateTime OrderDate { get; set; }
    public string CustomerName { get; set; }
    public string CustomerEmail { get; set; }
    public string CustomerContact { get; set; }
    public List<OrderItemViewModel> Items { get; set; }
    public decimal SubTotal { get; set; }
    public decimal DiscountPercent { get; set; }
    public decimal TotalPrice { get; set; }
    public string PaymentMethod { get; set; }
    public string Status { get; set; }
    public List<PromotionViewModel> Promotions { get; set; }
}

public class OrderItemViewModel
{
    public string ProductName { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Total => Quantity * UnitPrice;
}

public class PromotionViewModel
{
    public string Code { get; set; }
    public decimal DiscountAmount { get; set; }
}

