using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace OnlineFood.Models;

[Table("InfoFood")]
public partial class InfoFood
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("songuyenlieucan")]
    public int Songuyenlieucan { get; set; }

    [Column("idFood")]
    public int IdFood { get; set; }

    [Column("mota")]
    [StringLength(100)]
    public string? Mota { get; set; }

    [Column("idNguyenLieu")]
    public int IdNguyenLieu { get; set; }

    [ForeignKey("IdFood")]
    [InverseProperty("InfoFoods")]
    public virtual Food IdFoodNavigation { get; set; } = null!;

    [ForeignKey("IdNguyenLieu")]
    [InverseProperty("InfoFoods")]
    public virtual Supply IdNguyenLieuNavigation { get; set; } = null!;
}
