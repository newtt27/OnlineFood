using System;
using System.Collections.Generic;

namespace OnlineFood.Models;

public partial class Staff
{
    public int Id { get; set; }

    public string Hoten { get; set; } = null!;

    public string Diachi { get; set; } = null!;

    public string Sodienthoai { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Image { get; set; } = null!;

    public int? Idquyen { get; set; }

    public virtual Role? IdquyenNavigation { get; set; }
}
