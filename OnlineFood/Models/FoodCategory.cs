using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace OnlineFood.Models;

[Table("FoodCategory")]
public partial class FoodCategory
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("tenDanhMuc")]
    [StringLength(100)]
    public string TenDanhMuc { get; set; } = null!;

    [Column("mota")]
    [StringLength(100)]
    public string? Mota { get; set; }

    [Column("hinhanh", TypeName = "image")]
    public byte[]? Hinhanh { get; set; }

    [InverseProperty("IdDanhMucNavigation")]
    public virtual ICollection<Food> Foods { get; set; } = new List<Food>();
}
