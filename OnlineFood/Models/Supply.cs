using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace OnlineFood.Models;

public partial class Supply
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("tenNguyenLieu")]
    [StringLength(100)]
    public string TenNguyenLieu { get; set; } = null!;

    public int SoLuong { get; set; }

    [StringLength(50)]
    public string DonVi { get; set; } = null!;

    [Column("idBill")]
    public int IdBill { get; set; }

    [ForeignKey("IdBill")]
    [InverseProperty("Supplies")]
    public virtual BillSupply IdBillNavigation { get; set; } = null!;

    [InverseProperty("IdNguyenLieuNavigation")]
    public virtual ICollection<InfoFood> InfoFoods { get; set; } = new List<InfoFood>();

    [ForeignKey("Idsupplies")]
    [InverseProperty("Idsupplies")]
    public virtual ICollection<OrderSupply> Idordersupplies { get; set; } = new List<OrderSupply>();
}
