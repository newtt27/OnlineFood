using System;
using System.Collections.Generic;

namespace OnlineFood.Models;

public partial class Role
{
    public int Id { get; set; }

    public string TenRole { get; set; } = null!;

    public int TrangThai { get; set; }

    public string? Mota { get; set; }

    public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();

    public virtual ICollection<Function> Functions { get; set; } = new List<Function>();
}
