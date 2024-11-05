using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace OnlineFood.Models;

[Table("Employee")]
public partial class Employee
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("tenNhanVien")]
    [StringLength(100)]
    public string TenNhanVien { get; set; } = null!;

    [Column("dienThoai")]
    [StringLength(15)]
    public string DienThoai { get; set; } = null!;

    [Column("email")]
    [StringLength(100)]
    public string Email { get; set; } = null!;

    [Column("diaChi")]
    [StringLength(100)]
    public string DiaChi { get; set; } = null!;

    [Column("idaccount")]
    public int Idaccount { get; set; }

    [Column("trangThai")]
    public int TrangThai { get; set; }

    [Column("idShift")]
    public int IdShift { get; set; }

    [Column("birth")]
    public DateOnly Birth { get; set; }

    [Column("ngayBatDau")]
    public DateOnly NgayBatDau { get; set; }

    [InverseProperty("IdNhanVienNavigation")]
    public virtual ICollection<Bill> Bills { get; set; } = new List<Bill>();

    [ForeignKey("IdShift")]
    [InverseProperty("Employees")]
    public virtual Shift IdShiftNavigation { get; set; } = null!;

    [ForeignKey("Idaccount")]
    [InverseProperty("Employees")]
    public virtual Account IdaccountNavigation { get; set; } = null!;

    [InverseProperty("IdNhanVienNavigation")]
    public virtual ICollection<OrderSupply> OrderSupplies { get; set; } = new List<OrderSupply>();
}
