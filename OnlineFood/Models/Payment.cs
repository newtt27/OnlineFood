using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace OnlineFood.Models;

[Table("Payment")]
public partial class Payment
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("phuongThucThanhToan")]
    [StringLength(100)]
    public string PhuongThucThanhToan { get; set; } = null!;

    [Column("mota")]
    [StringLength(100)]
    public string? Mota { get; set; }

    [Column("trangThai")]
    [StringLength(100)]
    public string TrangThai { get; set; } = null!;

    [InverseProperty("IdPhuongThucNavigation")]
    public virtual ICollection<Bill> Bills { get; set; } = new List<Bill>();
}
