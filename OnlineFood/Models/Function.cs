using System;
using System.Collections.Generic;

namespace OnlineFood.Models;

public partial class Function
{
    public int Id { get; set; }

    public string Ten { get; set; } = null!;

    public string? Mota { get; set; }

    public int TrangThai { get; set; }

    public virtual ICollection<Role> Roles { get; set; } = new List<Role>();
}
