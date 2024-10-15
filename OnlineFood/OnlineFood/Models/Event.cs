using System;
using System.Collections.Generic;

namespace OnlineFood.Models;

public partial class Event
{
    public int Id { get; set; }

    public string Ten { get; set; } = null!;

    public string Noidung { get; set; } = null!;

    public int Phantram { get; set; }

    public DateOnly Ngaybatdau { get; set; }

    public DateOnly Ngayketthuc { get; set; }

    public int Trangthai { get; set; }

    public string? Image { get; set; }

    public virtual ICollection<Paymentdetail> Paymentdetails { get; set; } = new List<Paymentdetail>();
}
