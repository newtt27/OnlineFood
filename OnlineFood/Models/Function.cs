using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace OnlineFood.Models;

[Table("Function")]
public partial class Function
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("ten")]
    [StringLength(100)]
    public string Ten { get; set; } = null!;

    [Column("mota")]
    [StringLength(100)]
    public string? Mota { get; set; }

    [Column("trangThai")]
    public int TrangThai { get; set; }

    [ForeignKey("FunctionId")]
    [InverseProperty("Functions")]
    public virtual ICollection<Role> Roles { get; set; } = new List<Role>();
}
