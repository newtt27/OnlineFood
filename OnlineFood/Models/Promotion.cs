using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace OnlineFood.Models;

[Table("Promotion")]
public partial class Promotion
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("ten")]
    [StringLength(100)]
    public string Ten { get; set; } = null!;

    [Column("idKM")]
    [StringLength(100)]
    public string IdKm { get; set; } = null!;

    [Column("phanTram")]
    public int PhanTram { get; set; }

    [Column("ngayBD")]
    public DateOnly NgayBd { get; set; }

    [Column("ngayKT")]
    public DateOnly NgayKt { get; set; }

    [Column("trangThai")]
    public int TrangThai { get; set; }

    [Column("code")]
    [StringLength(50)]
    public string Code { get; set; } = null!;

    [Column("noidung")]
    [StringLength(50)]
    public string? Noidung { get; set; }

    [InverseProperty("IdKmNavigation")]
    public virtual ICollection<Bill> Bills { get; set; } = new List<Bill>();

    [InverseProperty("IdKmNavigation")]
    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();
}
