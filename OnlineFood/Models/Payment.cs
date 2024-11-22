using System;
using System.Collections.Generic;

namespace OnlineFood.Models;

public partial class Payment
{
    public int Id { get; set; }

    public string PhuongThucThanhToan { get; set; } = null!;

    public string? Mota { get; set; }

    public string TrangThai { get; set; } = null!;

    public virtual ICollection<Bill> Bills { get; set; } = new List<Bill>();
}
