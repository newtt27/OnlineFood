using System;
using System.Collections.Generic;

namespace OnlineFood.Models;

public partial class FoodCategory
{
    public int Id { get; set; }

    public string TenDanhMuc { get; set; } = null!;

    public string? Mota { get; set; }

    public string? Hinhanh { get; set; }

    public virtual ICollection<Food> Foods { get; set; } = new List<Food>();
}
