using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using OnlineFood.Models;

namespace OnlineFood.Data;

public partial class OnlineFoodContext : DbContext
{
    public OnlineFoodContext()
    {
    }

    public OnlineFoodContext(DbContextOptions<OnlineFoodContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Cart> Carts { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Event> Events { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Paymentdetail> Paymentdetails { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Staff> Staff { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=OnlineFoodDatabase");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.Username).HasName("PK__Account__F3DBC573E86863FE");

            entity.ToTable("Account");

            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .HasColumnName("username");
            entity.Property(e => e.Loaitaikhoan)
                .HasMaxLength(50)
                .HasColumnName("loaitaikhoan");
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .HasColumnName("password");
            entity.Property(e => e.Tenhienthi)
                .HasMaxLength(50)
                .HasColumnName("tenhienthi");
        });

        modelBuilder.Entity<Cart>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Cart__3213E83F20DED48B");

            entity.ToTable("Cart");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Giatien).HasColumnName("giatien");
            entity.Property(e => e.IdKhachhang).HasColumnName("idKhachhang");
            entity.Property(e => e.IdSanpham).HasColumnName("idSanpham");
            entity.Property(e => e.Souong).HasColumnName("souong");
            entity.Property(e => e.Trangthai).HasColumnName("trangthai");

            entity.HasOne(d => d.IdKhachhangNavigation).WithMany(p => p.Carts)
                .HasForeignKey(d => d.IdKhachhang)
                .HasConstraintName("FK__Cart__idKhachhan__36B12243");

            entity.HasOne(d => d.IdSanphamNavigation).WithMany(p => p.Carts)
                .HasForeignKey(d => d.IdSanpham)
                .HasConstraintName("FK__Cart__idSanpham__35BCFE0A");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Categori__3213E83FCD4869B4");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Image)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("image");
            entity.Property(e => e.TenDanhmuc)
                .HasMaxLength(50)
                .HasColumnName("tenDanhmuc");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Customer__3213E83FF9F7E20F");

            entity.ToTable("Customer");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Diachi)
                .HasMaxLength(50)
                .HasColumnName("diachi");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasColumnName("email");
            entity.Property(e => e.Hovaten)
                .HasMaxLength(50)
                .HasColumnName("hovaten");
            entity.Property(e => e.Image)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("image");
            entity.Property(e => e.Ngaylap)
                .HasMaxLength(50)
                .HasColumnName("ngaylap");
            entity.Property(e => e.Sodienthoai)
                .HasMaxLength(50)
                .HasColumnName("sodienthoai");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .HasColumnName("username");

            entity.HasOne(d => d.UsernameNavigation).WithMany(p => p.Customers)
                .HasForeignKey(d => d.Username)
                .HasConstraintName("FK__Customer__userna__2D27B809");
        });

        modelBuilder.Entity<Event>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Event__3213E83F1F3153FA");

            entity.ToTable("Event");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Image)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("image");
            entity.Property(e => e.Ngaybatdau).HasColumnName("ngaybatdau");
            entity.Property(e => e.Ngayketthuc).HasColumnName("ngayketthuc");
            entity.Property(e => e.Noidung)
                .HasMaxLength(100)
                .HasColumnName("noidung");
            entity.Property(e => e.Phantram).HasColumnName("phantram");
            entity.Property(e => e.Ten)
                .HasMaxLength(100)
                .HasColumnName("ten");
            entity.Property(e => e.Trangthai).HasColumnName("trangthai");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Payment__3213E83FCDD43B76");

            entity.ToTable("Payment");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.IdCart).HasColumnName("idCart");
            entity.Property(e => e.IdKhachhang).HasColumnName("idKhachhang");
            entity.Property(e => e.IdSanpham).HasColumnName("idSanpham");
            entity.Property(e => e.Soluong).HasColumnName("soluong");
            entity.Property(e => e.Tongtien).HasColumnName("tongtien");

            entity.HasOne(d => d.IdCartNavigation).WithMany(p => p.Payments)
                .HasForeignKey(d => d.IdCart)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Payment__idCart__3A81B327");

            entity.HasOne(d => d.IdKhachhangNavigation).WithMany(p => p.Payments)
                .HasForeignKey(d => d.IdKhachhang)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Payment__idKhach__398D8EEE");
        });

        modelBuilder.Entity<Paymentdetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Paymentd__3213E83F5B10CF37");

            entity.ToTable("Paymentdetail");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.GiaTienSau).HasColumnName("giaTienSau");
            entity.Property(e => e.GiatienTruoc).HasColumnName("giatienTruoc");
            entity.Property(e => e.HotenKhachhang)
                .HasMaxLength(100)
                .HasColumnName("hotenKhachhang");
            entity.Property(e => e.IdKhachhang).HasColumnName("idKhachhang");
            entity.Property(e => e.IdKuyenmai).HasColumnName("idKuyenmai");
            entity.Property(e => e.IdPayment).HasColumnName("idPayment");
            entity.Property(e => e.IdSanpham).HasColumnName("idSanpham");
            entity.Property(e => e.PaymentMode)
                .HasMaxLength(100)
                .HasColumnName("paymentMode");
            entity.Property(e => e.Soluong).HasColumnName("soluong");

            entity.HasOne(d => d.IdKhachhangNavigation).WithMany(p => p.Paymentdetails)
                .HasForeignKey(d => d.IdKhachhang)
                .HasConstraintName("FK__Paymentde__idKha__3D5E1FD2");

            entity.HasOne(d => d.IdKuyenmaiNavigation).WithMany(p => p.Paymentdetails)
                .HasForeignKey(d => d.IdKuyenmai)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK__Paymentde__idKuy__403A8C7D");

            entity.HasOne(d => d.IdPaymentNavigation).WithMany(p => p.Paymentdetails)
                .HasForeignKey(d => d.IdPayment)
                .HasConstraintName("FK__Paymentde__idPay__3E52440B");

            entity.HasOne(d => d.IdSanphamNavigation).WithMany(p => p.Paymentdetails)
                .HasForeignKey(d => d.IdSanpham)
                .HasConstraintName("FK__Paymentde__idSan__3F466844");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Product__3213E83F0559204F");

            entity.ToTable("Product");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Giatien).HasColumnName("giatien");
            entity.Property(e => e.IdDanhmuc).HasColumnName("idDanhmuc");
            entity.Property(e => e.Image)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("image");
            entity.Property(e => e.Mota)
                .HasMaxLength(255)
                .HasColumnName("mota");
            entity.Property(e => e.Soluong).HasColumnName("soluong");
            entity.Property(e => e.Ten)
                .HasMaxLength(100)
                .HasColumnName("ten");
            entity.Property(e => e.Trangthai).HasColumnName("trangthai");

            entity.HasOne(d => d.IdDanhmucNavigation).WithMany(p => p.Products)
                .HasForeignKey(d => d.IdDanhmuc)
                .HasConstraintName("FK__Product__idDanhm__2A4B4B5E");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Role__3213E83F596C93BB");

            entity.ToTable("Role");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Tenquyen)
                .HasMaxLength(50)
                .HasColumnName("tenquyen");
            entity.Property(e => e.Trangthai).HasColumnName("trangthai");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .HasColumnName("username");

            entity.HasOne(d => d.UsernameNavigation).WithMany(p => p.Roles)
                .HasForeignKey(d => d.Username)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK__Role__username__300424B4");
        });

        modelBuilder.Entity<Staff>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Staff__3213E83FC42A7DFC");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Diachi)
                .HasMaxLength(50)
                .HasColumnName("diachi");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasColumnName("email");
            entity.Property(e => e.Hoten)
                .HasMaxLength(50)
                .HasColumnName("hoten");
            entity.Property(e => e.Idquyen).HasColumnName("idquyen");
            entity.Property(e => e.Image)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("image");
            entity.Property(e => e.Sodienthoai)
                .HasMaxLength(50)
                .HasColumnName("sodienthoai");

            entity.HasOne(d => d.IdquyenNavigation).WithMany(p => p.Staff)
                .HasForeignKey(d => d.Idquyen)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK__Staff__idquyen__32E0915F");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
