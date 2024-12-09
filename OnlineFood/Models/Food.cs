using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnlineFood.Models;

public partial class Food
{
    public int Id { get; set; }

    public string TenMonAn { get; set; } = null!;

    public int IdDanhMuc { get; set; }

    public int GiaTien { get; set; }

    public int TrangThai { get; set; }

    public int Soluong { get; set; }

    public string? Hinhanh { get; set; }

    public string Size { get; set; } = null!;

    public string? Mota { get; set; }

    public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

    public virtual FoodCategory IdDanhMucNavigation { get; set; } = null!;

    public virtual ICollection<InfoFood> InfoFoods { get; set; } = new List<InfoFood>();

    public virtual ICollection<OrderInfo> OrderInfos { get; set; } = new List<OrderInfo>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
