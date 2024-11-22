using System;
using System.Collections.Generic;

namespace OnlineFood.Models;

public partial class InfoFood
{
    public int Id { get; set; }

    public int Songuyenlieucan { get; set; }

    public int IdFood { get; set; }

    public string? Mota { get; set; }

    public int IdNguyenLieu { get; set; }

    public virtual Food IdFoodNavigation { get; set; } = null!;

    public virtual Supply IdNguyenLieuNavigation { get; set; } = null!;
}
