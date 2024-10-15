using System;
using System.Collections.Generic;

namespace OnlineFood.Models;

public partial class Cart
{
    public int Id { get; set; }

    public int IdSanpham { get; set; }

    public int Souong { get; set; }

    public int Giatien { get; set; }

    public int IdKhachhang { get; set; }

    public int Trangthai { get; set; }

    public virtual Customer IdKhachhangNavigation { get; set; } = null!;

    public virtual Product IdSanphamNavigation { get; set; } = null!;

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
}
