using System;
using System.Collections.Generic;

namespace OnlineFood.Models;

public partial class Account
{
    public int Id { get; set; }

    public string UserName { get; set; } = null!;

    public string TenHienThi { get; set; } = null!;

    public string MatKhau { get; set; } = null!;

    public int? Idrole { get; set; }

    public int? IdCart { get; set; }

    public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

    public virtual Cart? IdCartNavigation { get; set; }

    public virtual Role? IdroleNavigation { get; set; }
}
