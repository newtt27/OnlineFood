using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace OnlineFood.Models;

public partial class BillSupply
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("idordersupplies")]
    public int Idordersupplies { get; set; }

    [Column("date", TypeName = "datetime")]
    public DateTime Date { get; set; }

    [Column("mota")]
    [StringLength(50)]
    public string Mota { get; set; } = null!;

    [Column("idNhaCungCap")]
    public int IdNhaCungCap { get; set; }

    [ForeignKey("IdNhaCungCap")]
    [InverseProperty("BillSupplies")]
    public virtual Supplier IdNhaCungCapNavigation { get; set; } = null!;

    [ForeignKey("Idordersupplies")]
    [InverseProperty("BillSupplies")]
    public virtual OrderSupply IdordersuppliesNavigation { get; set; } = null!;

    [InverseProperty("IdBillSuppliesNavigation")]
    public virtual ICollection<Latest> Latests { get; set; } = new List<Latest>();

    [InverseProperty("IdBillsuppliesNavigation")]
    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    [InverseProperty("IdBillNavigation")]
    public virtual ICollection<Supply> Supplies { get; set; } = new List<Supply>();
}
