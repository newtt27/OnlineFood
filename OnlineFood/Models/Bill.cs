using System;
using System.Collections.Generic;

namespace OnlineFood.Models;

public partial class Bill
{
    public int Id { get; set; }

    public DateTime NgayCheckIn { get; set; }

    public DateTime NgayCheckOut { get; set; }

    public int TrangThai { get; set; }

    public int IdKhachHang { get; set; }

    public int IdNhanVien { get; set; }

    public int IdKm { get; set; }

    public double TongTienTruoc { get; set; }

    public double TongTienSau { get; set; }

    public int IdPhuongThuc { get; set; }

    public int IdOrderInfo { get; set; }

    public virtual Customer IdKhachHangNavigation { get; set; } = null!;

    public virtual Promotion IdKmNavigation { get; set; } = null!;

    public virtual Employee IdNhanVienNavigation { get; set; } = null!;

    public virtual OrderInfo IdOrderInfoNavigation { get; set; } = null!;

    public virtual Payment IdPhuongThucNavigation { get; set; } = null!;

    public virtual ICollection<Latest> Latests { get; set; } = new List<Latest>();

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();
}
