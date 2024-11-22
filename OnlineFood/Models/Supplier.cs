using System;
using System.Collections.Generic;

namespace OnlineFood.Models;

public partial class Supplier
{
    public int Id { get; set; }

    public string TenNhaCungCap { get; set; } = null!;

    public string DiaChi { get; set; } = null!;

    public string Lienhe { get; set; } = null!;

    public string Email { get; set; } = null!;

    public virtual ICollection<BillSupply> BillSupplies { get; set; } = new List<BillSupply>();

    public virtual ICollection<OrderSupply> OrderSupplies { get; set; } = new List<OrderSupply>();
}
