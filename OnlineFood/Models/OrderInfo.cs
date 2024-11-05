using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace OnlineFood.Models;

[Table("OrderInfo")]
public partial class OrderInfo
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("idUser")]
    public int IdUser { get; set; }

    [Column("idMonAn")]
    public int IdMonAn { get; set; }

    [Column("mota")]
    [StringLength(100)]
    public string? Mota { get; set; }

    [Column("soLuongHangMon")]
    public int SoLuongHangMon { get; set; }

    [InverseProperty("IdOrderInfoNavigation")]
    public virtual ICollection<Bill> Bills { get; set; } = new List<Bill>();

    [ForeignKey("IdMonAn")]
    [InverseProperty("OrderInfos")]
    public virtual Food IdMonAnNavigation { get; set; } = null!;

    [ForeignKey("IdUser")]
    [InverseProperty("OrderInfos")]
    public virtual Customer IdUserNavigation { get; set; } = null!;
}
