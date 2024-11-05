using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace OnlineFood.Models;

[Table("Account")]
public partial class Account
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("userName")]
    [StringLength(100)]
    public string UserName { get; set; } = null!;

    [Column("tenHienThi")]
    [StringLength(100)]
    public string TenHienThi { get; set; } = null!;

    [Column("matKhau")]
    [StringLength(1000)]
    public string MatKhau { get; set; } = null!;

    [Column("idrole")]
    public int? Idrole { get; set; }

    [InverseProperty("IdAccountNavigation")]
    public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();

    [InverseProperty("IdaccountNavigation")]
    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

    [ForeignKey("Idrole")]
    [InverseProperty("Accounts")]
    public virtual Role? IdroleNavigation { get; set; }
}
