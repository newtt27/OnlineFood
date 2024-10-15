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
