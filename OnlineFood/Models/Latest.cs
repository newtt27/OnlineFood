using System;
using System.Collections.Generic;

namespace OnlineFood.Models;

public partial class Latest
{
    public int Id { get; set; }

    public int IdBill { get; set; }

    public int IdBillSupplies { get; set; }

    public DateTime Date { get; set; }

    public string NoiDung { get; set; } = null!;

    public int IdCustomer { get; set; }

    public int IdOrderSupplies { get; set; }

    public virtual Bill IdBillNavigation { get; set; } = null!;

    public virtual BillSupply IdBillSuppliesNavigation { get; set; } = null!;

    public virtual Customer IdCustomerNavigation { get; set; } = null!;

    public virtual OrderSupply IdOrderSuppliesNavigation { get; set; } = null!;
}
