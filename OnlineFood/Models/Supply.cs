using System;
using System.Collections.Generic;

namespace OnlineFood.Models;

public partial class Supply
{
    public int Id { get; set; }

    public string TenNguyenLieu { get; set; } = null!;

    public int SoLuong { get; set; }

    public string DonVi { get; set; } = null!;

    public int IdBill { get; set; }

    public virtual BillSupply IdBillNavigation { get; set; } = null!;

    public virtual ICollection<InfoFood> InfoFoods { get; set; } = new List<InfoFood>();

    public virtual ICollection<OrderSupply> Idordersupplies { get; set; } = new List<OrderSupply>();
}
