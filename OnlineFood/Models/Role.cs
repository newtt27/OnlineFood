using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace OnlineFood.Models;

[Table("Role")]
public partial class Role
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("tenRole")]
    [StringLength(100)]
    public string TenRole { get; set; } = null!;

    [Column("trangThai")]
    public int TrangThai { get; set; }

    [Column("mota")]
    [StringLength(50)]
    public string? Mota { get; set; }

    [InverseProperty("IdroleNavigation")]
    public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();

    [ForeignKey("RoleId")]
    [InverseProperty("Roles")]
    public virtual ICollection<Function> Functions { get; set; } = new List<Function>();
}
