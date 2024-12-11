using System;
using System.Collections.Generic;

namespace OnlineFood.Models;

public partial class BillSupply
{
    public int Id { get; set; }

    public int Idordersupplies { get; set; }

    public DateTime Date { get; set; }

    public string Mota { get; set; } = null!;

    public int IdNhaCungCap { get; set; }

    public float TongTien { get; set; }

    public virtual Supplier IdNhaCungCapNavigation { get; set; } = null!;

    public virtual OrderSupply IdordersuppliesNavigation { get; set; } = null!;

    public virtual ICollection<Latest> Latests { get; set; } = new List<Latest>();

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    public virtual ICollection<Supply> Supplies { get; set; } = new List<Supply>();
}
