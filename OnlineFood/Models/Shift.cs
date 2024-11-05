using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace OnlineFood.Models;

[Table("Shift")]
public partial class Shift
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("ngayCa")]
    public DateOnly NgayCa { get; set; }

    [Column("gioBatDau")]
    public TimeOnly GioBatDau { get; set; }

    [Column("gioKetThuc")]
    public TimeOnly GioKetThuc { get; set; }

    [InverseProperty("IdShiftNavigation")]
    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
