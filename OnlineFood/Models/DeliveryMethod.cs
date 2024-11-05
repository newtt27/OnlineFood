using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace OnlineFood.Models;

[Table("DeliveryMethod")]
public partial class DeliveryMethod
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("tenphuongthucthanhtoan")]
    [StringLength(100)]
    public string Tenphuongthucthanhtoan { get; set; } = null!;

    [Column("gia")]
    public double Gia { get; set; }

    [Column("trangthai")]
    public int Trangthai { get; set; }

    [InverseProperty("IdDeliveryNavigation")]
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
