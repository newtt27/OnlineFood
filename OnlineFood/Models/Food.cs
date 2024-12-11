using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnlineFood.Models;
public partial class Food
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Food name is required.")]
    public string TenMonAn { get; set; } = null!;
    [Required(ErrorMessage = "Category is required.")]
    [Range(1, int.MaxValue, ErrorMessage = "Please select a valid category.")]
    public int IdDanhMuc { get; set; }
    [Required(ErrorMessage = "Price is required.")]
    [Range(0, double.MaxValue, ErrorMessage = "Price must be a positive number.")]
    public int GiaTien { get; set; }
    public int TrangThai { get; set; }

    public int Soluong { get; set; }

    public string? Hinhanh { get; set; }

    public string Size { get; set; } = null!;

    public string? Mota { get; set; }

    public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

    public virtual FoodCategory? IdDanhMucNavigation { get; set; }

    public virtual ICollection<InfoFood> InfoFoods { get; set; } = new List<InfoFood>();

    public virtual ICollection<OrderInfo> OrderInfos { get; set; } = new List<OrderInfo>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
