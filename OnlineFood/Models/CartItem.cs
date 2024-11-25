using System;
using System.Collections.Generic;

namespace OnlineFood.Models;

public partial class CartItem
{
    public int IdCart { get; set; }

    public int IdFood { get; set; }

    public int SoLuong { get; set; }

    public virtual Cart IdCartNavigation { get; set; } = null!;

    public virtual Food IdFoodNavigation { get; set; } = null!;
}
