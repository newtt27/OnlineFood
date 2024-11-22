using System;
using System.Collections.Generic;

namespace OnlineFood.Models;

public partial class Employee
{
    public int Id { get; set; }

    public string TenNhanVien { get; set; } = null!;

    public string DienThoai { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string DiaChi { get; set; } = null!;

    public int Idaccount { get; set; }

    public int TrangThai { get; set; }

    public int IdShift { get; set; }

    public DateOnly Birth { get; set; }

    public DateOnly NgayBatDau { get; set; }

    public virtual ICollection<Bill> Bills { get; set; } = new List<Bill>();

    public virtual Shift IdShiftNavigation { get; set; } = null!;

    public virtual Account IdaccountNavigation { get; set; } = null!;

    public virtual ICollection<OrderSupply> OrderSupplies { get; set; } = new List<OrderSupply>();
}
