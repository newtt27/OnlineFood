using System;
using System.Collections.Generic;

namespace OnlineFood.Models;

public partial class DeliveryMethod
{
    public int Id { get; set; }

    public string Tenphuongthucthanhtoan { get; set; } = null!;

    public double Gia { get; set; }

    public int Trangthai { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
