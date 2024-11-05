using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace OnlineFood.Models;

[Table("Latest")]
public partial class Latest
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("idBill")]
    public int IdBill { get; set; }

    [Column("idBillSupplies")]
    public int IdBillSupplies { get; set; }

    [Column("date", TypeName = "datetime")]
    public DateTime Date { get; set; }

    [Column("noiDung")]
    [StringLength(100)]
    public string NoiDung { get; set; } = null!;

    [Column("idCustomer")]
    public int IdCustomer { get; set; }

    [Column("idOrderSupplies")]
    public int IdOrderSupplies { get; set; }

    [ForeignKey("IdBill")]
    [InverseProperty("Latests")]
    public virtual Bill IdBillNavigation { get; set; } = null!;

    [ForeignKey("IdBillSupplies")]
    [InverseProperty("Latests")]
    public virtual BillSupply IdBillSuppliesNavigation { get; set; } = null!;

    [ForeignKey("IdCustomer")]
    [InverseProperty("Latests")]
    public virtual Customer IdCustomerNavigation { get; set; } = null!;

    [ForeignKey("IdOrderSupplies")]
    [InverseProperty("Latests")]
    public virtual OrderSupply IdOrderSuppliesNavigation { get; set; } = null!;
}
