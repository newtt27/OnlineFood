using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations; // Import để sử dụng Data Annotations

namespace OnlineFood.Models
{
    public partial class Employee
    {
        public int Id { get; set; }

        [Display(Name = "Tên nhân viên")]
        public string TenNhanVien { get; set; } = null!;

        [Display(Name = "Điện thoại")]
        public string DienThoai { get; set; } = null!;

        [Display(Name = "Email")]
        public string Email { get; set; } = null!;

        [Display(Name = "Địa chỉ")]
        public string DiaChi { get; set; } = null!;

        [Display(Name = "Tài khoản")]
        public int Idaccount { get; set; }

        [Display(Name = "Trạng thái")]
        public int TrangThai { get; set; }

        [Display(Name = "Ca làm việc")]
        public int IdShift { get; set; }

        [Display(Name = "Ngày sinh")]
        [DataType(DataType.Date)] // Hiển thị chỉ ngày, không có giờ
        public DateOnly Birth { get; set; }

        [Display(Name = "Ngày bắt đầu làm")]
        [DataType(DataType.Date)]
        public DateOnly NgayBatDau { get; set; }

        public virtual ICollection<Bill> Bills { get; set; } = new List<Bill>();

        [Display(Name = "Chi tiết ca làm việc")]
        public virtual Shift IdShiftNavigation { get; set; } = null!;

        [Display(Name = "Tài khoản liên kết")]
        public virtual Account IdaccountNavigation { get; set; } = null!;

        public virtual ICollection<OrderSupply> OrderSupplies { get; set; } = new List<OrderSupply>();
    }
}
