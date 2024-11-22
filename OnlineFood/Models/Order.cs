using System;
using System.Collections.Generic;

namespace OnlineFood.Models;

public partial class Order
{
    public int Id { get; set; }

    public int IdKhachHang { get; set; }

    public DateTime Date { get; set; }

    public int IdFood { get; set; }

    public string TrangThai { get; set; } = null!;

    public int TongSoLuong { get; set; }

    public int IdCart { get; set; }

    public int IdDelivery { get; set; }

    public virtual Cart IdCartNavigation { get; set; } = null!;

    public virtual DeliveryMethod IdDeliveryNavigation { get; set; } = null!;

    public virtual Food IdFoodNavigation { get; set; } = null!;

    public virtual Customer IdKhachHangNavigation { get; set; } = null!;

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();
}
