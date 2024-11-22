using System;
using System.Collections.Generic;

namespace OnlineFood.Models;

public partial class OrderSupply
{
    public int Id { get; set; }

    public string SoLuongNguyenLieu { get; set; } = null!;

    public string? Mota { get; set; }

    public int IdNhanVien { get; set; }

    public int IdNhaCungCap { get; set; }

    public DateTime Datetime { get; set; }

    public string TrangThai { get; set; } = null!;

    public string TenNguyenLieu { get; set; } = null!;

    public string Donvi { get; set; } = null!;

    public virtual ICollection<BillSupply> BillSupplies { get; set; } = new List<BillSupply>();

    public virtual Supplier IdNhaCungCapNavigation { get; set; } = null!;

    public virtual Employee IdNhanVienNavigation { get; set; } = null!;

    public virtual ICollection<Latest> Latests { get; set; } = new List<Latest>();

    public virtual ICollection<Supply> Idsupplies { get; set; } = new List<Supply>();
}
