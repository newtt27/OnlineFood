using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace OnlineFood.Models;

[Table("Customer")]
public partial class Customer
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("tenKhachHang")]
    [StringLength(100)]
    public string TenKhachHang { get; set; } = null!;

    [Column("diaChi")]
    [StringLength(100)]
    public string DiaChi { get; set; } = null!;

    [Column("email")]
    [StringLength(100)]
    public string Email { get; set; } = null!;

    [Column("soDienThoai")]
    [StringLength(15)]
    public string SoDienThoai { get; set; } = null!;

    [Column("soTienTieu")]
    [StringLength(15)]
    public string SoTienTieu { get; set; } = null!;

    [Column("idAccount")]
    public int IdAccount { get; set; }

    [InverseProperty("IdKhachHangNavigation")]
    public virtual ICollection<Bill> Bills { get; set; } = new List<Bill>();

    [ForeignKey("IdAccount")]
    [InverseProperty("Customers")]
    public virtual Account IdAccountNavigation { get; set; } = null!;

    [InverseProperty("IdCustomerNavigation")]
    public virtual ICollection<Latest> Latests { get; set; } = new List<Latest>();

    [InverseProperty("IdCustomerNavigation")]
    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    [InverseProperty("IdUserNavigation")]
    public virtual ICollection<OrderInfo> OrderInfos { get; set; } = new List<OrderInfo>();

    [InverseProperty("IdKhachHangNavigation")]
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
