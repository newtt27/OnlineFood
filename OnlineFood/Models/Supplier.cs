using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace OnlineFood.Models;

[Table("Supplier")]
public partial class Supplier
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("tenNhaCungCap")]
    [StringLength(100)]
    public string TenNhaCungCap { get; set; } = null!;

    [Column("diaChi")]
    [StringLength(100)]
    public string DiaChi { get; set; } = null!;

    [Column("lienhe")]
    [StringLength(15)]
    public string Lienhe { get; set; } = null!;

    [Column("email")]
    [StringLength(100)]
    public string Email { get; set; } = null!;

    [InverseProperty("IdNhaCungCapNavigation")]
    public virtual ICollection<BillSupply> BillSupplies { get; set; } = new List<BillSupply>();

    [InverseProperty("IdNhaCungCapNavigation")]
    public virtual ICollection<OrderSupply> OrderSupplies { get; set; } = new List<OrderSupply>();
}
