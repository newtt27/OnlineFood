using System;
using System.Collections.Generic;

namespace OnlineFood.Models;

public partial class Customer
{
    public int Id { get; set; }

    public string TenKhachHang { get; set; } = null!;

    public string DiaChi { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string SoDienThoai { get; set; } = null!;

    public string SoTienTieu { get; set; } = null!;

    public int IdAccount { get; set; }

    public virtual ICollection<Bill> Bills { get; set; } = new List<Bill>();

    public virtual Account IdAccountNavigation { get; set; } = null!;

    public virtual ICollection<Latest> Latests { get; set; } = new List<Latest>();

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    public virtual ICollection<OrderInfo> OrderInfos { get; set; } = new List<OrderInfo>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
