using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace OnlineFood.Models;

[Table("Cart")]
public partial class Cart
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("idKM")]
    public int IdKm { get; set; }

    [Column("idFood")]
    public int IdFood { get; set; }

    [ForeignKey("IdFood")]
    [InverseProperty("Carts")]
    public virtual Food IdFoodNavigation { get; set; } = null!;

    [ForeignKey("IdKm")]
    [InverseProperty("Carts")]
    public virtual Promotion IdKmNavigation { get; set; } = null!;

    [InverseProperty("IdCartNavigation")]
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
