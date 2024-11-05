using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace OnlineFood.Models;

[Table("Bill")]
public partial class Bill
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("ngayCheckIn", TypeName = "datetime")]
    public DateTime NgayCheckIn { get; set; }

    [Column("ngayCheckOut", TypeName = "datetime")]
    public DateTime NgayCheckOut { get; set; }

    [Column("trangThai")]
    public int TrangThai { get; set; }

    [Column("idKhachHang")]
    public int IdKhachHang { get; set; }

    [Column("idNhanVien")]
    public int IdNhanVien { get; set; }

    [Column("idKM")]
    public int IdKm { get; set; }

    [Column("tongTienTruoc")]
    public double TongTienTruoc { get; set; }

    [Column("tongTienSau")]
    public double TongTienSau { get; set; }

    [Column("idPhuongThuc")]
    public int IdPhuongThuc { get; set; }

    [Column("idOrderInfo")]
    public int IdOrderInfo { get; set; }

    [ForeignKey("IdKhachHang")]
    [InverseProperty("Bills")]
    public virtual Customer IdKhachHangNavigation { get; set; } = null!;

    [ForeignKey("IdKm")]
    [InverseProperty("Bills")]
    public virtual Promotion IdKmNavigation { get; set; } = null!;

    [ForeignKey("IdNhanVien")]
    [InverseProperty("Bills")]
    public virtual Employee IdNhanVienNavigation { get; set; } = null!;

    [ForeignKey("IdOrderInfo")]
    [InverseProperty("Bills")]
    public virtual OrderInfo IdOrderInfoNavigation { get; set; } = null!;

    [ForeignKey("IdPhuongThuc")]
    [InverseProperty("Bills")]
    public virtual Payment IdPhuongThucNavigation { get; set; } = null!;

    [InverseProperty("IdBillNavigation")]
    public virtual ICollection<Latest> Latests { get; set; } = new List<Latest>();

    [InverseProperty("IdBillNavigation")]
    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();
}
