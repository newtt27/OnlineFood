using System;
using System.Collections.Generic;

namespace OnlineFood.Models;

public partial class Role
{
    public int Id { get; set; }

    public string Tenquyen { get; set; } = null!;

    public int Trangthai { get; set; }

    public string? Username { get; set; }

    public virtual ICollection<Staff> Staff { get; set; } = new List<Staff>();

    public virtual Account? UsernameNavigation { get; set; }
}
