using System;
using System.Collections.Generic;

namespace OnlineFood.Models;

public partial class Product
{
    public int Id { get; set; }

    public string Ten { get; set; } = null!;

    public int IdDanhmuc { get; set; }

    public string Mota { get; set; } = null!;

    public int Soluong { get; set; }

    public int Giatien { get; set; }

    public int Trangthai { get; set; }

    public string? Image { get; set; }

    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    public virtual Category IdDanhmucNavigation { get; set; } = null!;

    public virtual ICollection<Paymentdetail> Paymentdetails { get; set; } = new List<Paymentdetail>();
}
