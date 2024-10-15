using System;
using System.Collections.Generic;

namespace OnlineFood.Models;

public partial class Account
{
    public string Username { get; set; } = null!;

    public string? Password { get; set; }

    public string? Tenhienthi { get; set; }

    public string? Loaitaikhoan { get; set; }

    public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();

    public virtual ICollection<Role> Roles { get; set; } = new List<Role>();
}
