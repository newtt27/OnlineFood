using System;
using System.Collections.Generic;

namespace OnlineFood.Models;

public partial class Customer
{
    public int Id { get; set; }

    public string Hovaten { get; set; } = null!;

    public string Diachi { get; set; } = null!;

    public string Sodienthoai { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Ngaylap { get; set; } = null!;

    public string Username { get; set; } = null!;

    public string Image { get; set; } = null!;

    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    public virtual ICollection<Paymentdetail> Paymentdetails { get; set; } = new List<Paymentdetail>();

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual Account UsernameNavigation { get; set; } = null!;
}
