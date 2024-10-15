using System;
using System.Collections.Generic;

namespace OnlineFood.Models;

public partial class Payment
{
    public int Id { get; set; }

    public int IdKhachhang { get; set; }

    public int IdSanpham { get; set; }

    public int Soluong { get; set; }

    public int Tongtien { get; set; }

    public DateOnly Date { get; set; }

    public int IdCart { get; set; }

    public virtual Cart IdCartNavigation { get; set; } = null!;

    public virtual Customer IdKhachhangNavigation { get; set; } = null!;

    public virtual ICollection<Paymentdetail> Paymentdetails { get; set; } = new List<Paymentdetail>();
}
