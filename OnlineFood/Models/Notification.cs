using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace OnlineFood.Models;

public partial class Notification
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("idCustomer")]
    public int IdCustomer { get; set; }

    [Column("idOrder")]
    public int IdOrder { get; set; }

    [Column("name")]
    [StringLength(100)]
    public string Name { get; set; } = null!;

    [Column("noiDung")]
    [StringLength(50)]
    public string NoiDung { get; set; } = null!;

    [Column("date", TypeName = "datetime")]
    public DateTime Date { get; set; }

    [Column("idBill")]
    public int IdBill { get; set; }

    [Column("idBillsupplies")]
    public int IdBillsupplies { get; set; }

    [ForeignKey("IdBill")]
    [InverseProperty("Notifications")]
    public virtual Bill IdBillNavigation { get; set; } = null!;

    [ForeignKey("IdBillsupplies")]
    [InverseProperty("Notifications")]
    public virtual BillSupply IdBillsuppliesNavigation { get; set; } = null!;

    [ForeignKey("IdCustomer")]
    [InverseProperty("Notifications")]
    public virtual Customer IdCustomerNavigation { get; set; } = null!;

    [ForeignKey("IdOrder")]
    [InverseProperty("Notifications")]
    public virtual Order IdOrderNavigation { get; set; } = null!;
}
