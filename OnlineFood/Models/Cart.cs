using System;
using System.Collections.Generic;

namespace OnlineFood.Models;

public partial class Cart
{
    public int Id { get; set; }

    public int IdKm { get; set; }

    public int IdFood { get; set; }

    public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();

    public virtual Food IdFoodNavigation { get; set; } = null!;

    public virtual Promotion IdKmNavigation { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
