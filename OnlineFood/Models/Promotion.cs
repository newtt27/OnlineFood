using System;
using System.Collections.Generic;

namespace OnlineFood.Models;

public partial class Promotion
{
    public int Id { get; set; }

    public string Ten { get; set; } = null!;

    public string IdKm { get; set; } = null!;

    public int PhanTram { get; set; }

    public DateOnly NgayBd { get; set; }

    public DateOnly NgayKt { get; set; }

    public int TrangThai { get; set; }

    public string Code { get; set; } = null!;

    public string? Noidung { get; set; }

    public virtual ICollection<Bill> Bills { get; set; } = new List<Bill>();

    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();
}
