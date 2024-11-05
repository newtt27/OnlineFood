using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace OnlineFood.Models;

public partial class OrderSupply
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("soLuongNguyenLieu")]
    [StringLength(50)]
    public string SoLuongNguyenLieu { get; set; } = null!;

    [Column("mota")]
    [StringLength(100)]
    public string? Mota { get; set; }

    [Column("idNhanVien")]
    public int IdNhanVien { get; set; }

    [Column("idNhaCungCap")]
    public int IdNhaCungCap { get; set; }

    [Column("datetime", TypeName = "datetime")]
    public DateTime Datetime { get; set; }

    [Column("trangThai")]
    [StringLength(50)]
    public string TrangThai { get; set; } = null!;

    [Column("tenNguyenLieu")]
    [StringLength(50)]
    public string TenNguyenLieu { get; set; } = null!;

    [Column("donvi")]
    [StringLength(50)]
    public string Donvi { get; set; } = null!;

    [InverseProperty("IdordersuppliesNavigation")]
    public virtual ICollection<BillSupply> BillSupplies { get; set; } = new List<BillSupply>();

    [ForeignKey("IdNhaCungCap")]
    [InverseProperty("OrderSupplies")]
    public virtual Supplier IdNhaCungCapNavigation { get; set; } = null!;

    [ForeignKey("IdNhanVien")]
    [InverseProperty("OrderSupplies")]
    public virtual Employee IdNhanVienNavigation { get; set; } = null!;

    [InverseProperty("IdOrderSuppliesNavigation")]
    public virtual ICollection<Latest> Latests { get; set; } = new List<Latest>();

    [ForeignKey("Idordersupplies")]
    [InverseProperty("Idordersupplies")]
    public virtual ICollection<Supply> Idsupplies { get; set; } = new List<Supply>();
}
