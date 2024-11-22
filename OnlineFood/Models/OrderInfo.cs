using System;
using System.Collections.Generic;

namespace OnlineFood.Models;

public partial class OrderInfo
{
    public int Id { get; set; }

    public int IdUser { get; set; }

    public int IdMonAn { get; set; }

    public string? Mota { get; set; }

    public int SoLuongHangMon { get; set; }

    public virtual ICollection<Bill> Bills { get; set; } = new List<Bill>();

    public virtual Food IdMonAnNavigation { get; set; } = null!;

    public virtual Customer IdUserNavigation { get; set; } = null!;
}
