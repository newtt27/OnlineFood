using System;
using System.Collections.Generic;

namespace OnlineFood.Models;

public partial class Shift
{
    public int Id { get; set; }

    public DateOnly NgayCa { get; set; }

    public TimeOnly GioBatDau { get; set; }

    public TimeOnly GioKetThuc { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
