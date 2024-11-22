using System;
using System.Collections.Generic;

namespace OnlineFood.Models;

public partial class Notification
{
    public int Id { get; set; }

    public int IdCustomer { get; set; }

    public int IdOrder { get; set; }

    public string Name { get; set; } = null!;

    public string NoiDung { get; set; } = null!;

    public DateTime Date { get; set; }

    public int IdBill { get; set; }

    public int IdBillsupplies { get; set; }

    public virtual Bill IdBillNavigation { get; set; } = null!;

    public virtual BillSupply IdBillsuppliesNavigation { get; set; } = null!;

    public virtual Customer IdCustomerNavigation { get; set; } = null!;

    public virtual Order IdOrderNavigation { get; set; } = null!;
}
