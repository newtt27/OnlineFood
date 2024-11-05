using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace OnlineFood.Models;

[Table("Order")]
public partial class Order
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("idKhachHang")]
    public int IdKhachHang { get; set; }

    [Column("date", TypeName = "datetime")]
    public DateTime Date { get; set; }

    [Column("idFood")]
    public int IdFood { get; set; }

    [Column("trangThai")]
    [StringLength(100)]
    public string TrangThai { get; set; } = null!;

    [Column("tongSoLuong")]
    public int TongSoLuong { get; set; }

    [Column("idCart")]
    public int IdCart { get; set; }

    [Column("idDelivery")]
    public int IdDelivery { get; set; }

    [ForeignKey("IdCart")]
    [InverseProperty("Orders")]
    public virtual Cart IdCartNavigation { get; set; } = null!;

    [ForeignKey("IdDelivery")]
    [InverseProperty("Orders")]
    public virtual DeliveryMethod IdDeliveryNavigation { get; set; } = null!;

    [ForeignKey("IdFood")]
    [InverseProperty("Orders")]
    public virtual Food IdFoodNavigation { get; set; } = null!;

    [ForeignKey("IdKhachHang")]
    [InverseProperty("Orders")]
    public virtual Customer IdKhachHangNavigation { get; set; } = null!;

    [InverseProperty("IdOrderNavigation")]
    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();
}
