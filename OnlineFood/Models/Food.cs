using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace OnlineFood.Models;

[Table("Food")]
public partial class Food
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("tenMonAn")]
    [StringLength(100)]
    public string TenMonAn { get; set; } = null!;

    [Column("idDanhMuc")]
    public int IdDanhMuc { get; set; }

    [Column("giaTien")]
    public int GiaTien { get; set; }

    [Column("trangThai")]
    public int TrangThai { get; set; }

    [Column("soluong")]
    public int Soluong { get; set; }

    [Column("hinhanh", TypeName = "image")]
    public byte[]? Hinhanh { get; set; }

    [Column("size")]
    [StringLength(50)]
    public string Size { get; set; } = null!;

    [Column("mota")]
    [StringLength(100)]
    public string? Mota { get; set; }

    [InverseProperty("IdFoodNavigation")]
    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    [ForeignKey("IdDanhMuc")]
    [InverseProperty("Foods")]
    public virtual FoodCategory IdDanhMucNavigation { get; set; } = null!;

    [InverseProperty("IdFoodNavigation")]
    public virtual ICollection<InfoFood> InfoFoods { get; set; } = new List<InfoFood>();

    [InverseProperty("IdMonAnNavigation")]
    public virtual ICollection<OrderInfo> OrderInfos { get; set; } = new List<OrderInfo>();

    [InverseProperty("IdFoodNavigation")]
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
