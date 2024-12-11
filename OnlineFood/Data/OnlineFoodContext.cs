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

    public virtual DbSet<Bill> Bills { get; set; }

    public virtual DbSet<BillSupply> BillSupplies { get; set; }

    public virtual DbSet<Cart> Carts { get; set; }

    public virtual DbSet<CartItem> CartItems { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<DeliveryMethod> DeliveryMethods { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Food> Foods { get; set; }

    public virtual DbSet<FoodCategory> FoodCategories { get; set; }

    public virtual DbSet<Function> Functions { get; set; }

    public virtual DbSet<InfoFood> InfoFoods { get; set; }

    public virtual DbSet<Latest> Latests { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderInfo> OrderInfos { get; set; }

    public virtual DbSet<OrderSupply> OrderSupplies { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Promotion> Promotions { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Shift> Shifts { get; set; }

    public virtual DbSet<Supplier> Suppliers { get; set; }

    public virtual DbSet<Supply> Supplies { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=localhost;Database=OnlineFood;User Id=sa;Password=123456789;TrustServerCertificate=True; ");


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Account__3213E83F5B38DF03");

            entity.ToTable("Account");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.IdCart).HasColumnName("idCart");
            entity.Property(e => e.Idrole).HasColumnName("idrole");
            entity.Property(e => e.MatKhau)
                .HasMaxLength(1000)
                .HasColumnName("matKhau");
            entity.Property(e => e.TenHienThi)
                .HasMaxLength(100)
                .HasColumnName("tenHienThi");
            entity.Property(e => e.UserName)
                .HasMaxLength(100)
                .HasColumnName("userName");

            entity.HasOne(d => d.IdCartNavigation).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.IdCart)
                .HasConstraintName("FK__Account__idCart__76969D2E");

            entity.HasOne(d => d.IdroleNavigation).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.Idrole)
                .HasConstraintName("FK__Account__idrole__286302EC");
        });

        modelBuilder.Entity<Bill>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Bill__3213E83F2098AA11");

            entity.ToTable("Bill");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.IdKhachHang).HasColumnName("idKhachHang");
            entity.Property(e => e.IdKm).HasColumnName("idKM");
            entity.Property(e => e.IdNhanVien).HasColumnName("idNhanVien");
            entity.Property(e => e.IdOrderInfo).HasColumnName("idOrderInfo");
            entity.Property(e => e.IdPhuongThuc).HasColumnName("idPhuongThuc");
            entity.Property(e => e.NgayCheckIn)
                .HasColumnType("datetime")
                .HasColumnName("ngayCheckIn");
            entity.Property(e => e.NgayCheckOut)
                .HasColumnType("datetime")
                .HasColumnName("ngayCheckOut");
            entity.Property(e => e.TongTienSau).HasColumnName("tongTienSau");
            entity.Property(e => e.TongTienTruoc).HasColumnName("tongTienTruoc");
            entity.Property(e => e.TrangThai).HasColumnName("trangThai");

            entity.HasOne(d => d.IdKhachHangNavigation).WithMany(p => p.Bills)
                .HasForeignKey(d => d.IdKhachHang)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Bill__idKhachHan__5AEE82B9");

            entity.HasOne(d => d.IdKmNavigation).WithMany(p => p.Bills)
                .HasForeignKey(d => d.IdKm)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Bill__idKM__5CD6CB2B");

            entity.HasOne(d => d.IdNhanVienNavigation).WithMany(p => p.Bills)
                .HasForeignKey(d => d.IdNhanVien)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Bill__idNhanVien__5BE2A6F2");

            entity.HasOne(d => d.IdOrderInfoNavigation).WithMany(p => p.Bills)
                .HasForeignKey(d => d.IdOrderInfo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Bill__idOrderInf__5EBF139D");

            entity.HasOne(d => d.IdPhuongThucNavigation).WithMany(p => p.Bills)
                .HasForeignKey(d => d.IdPhuongThuc)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Bill__idPhuongTh__5DCAEF64");
        });

        modelBuilder.Entity<BillSupply>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__BillSupp__3213E83F1AFF6150");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Date)
                .HasColumnType("datetime")
                .HasColumnName("date");
            entity.Property(e => e.IdNhaCungCap).HasColumnName("idNhaCungCap");
            entity.Property(e => e.Idordersupplies).HasColumnName("idordersupplies");
            entity.Property(e => e.Mota)
                .HasMaxLength(50)
                .HasColumnName("mota");

            entity.HasOne(d => d.IdNhaCungCapNavigation).WithMany(p => p.BillSupplies)
                .HasForeignKey(d => d.IdNhaCungCap)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BillSuppl__idNha__3D5E1FD2");

            entity.HasOne(d => d.IdordersuppliesNavigation).WithMany(p => p.BillSupplies)
                .HasForeignKey(d => d.Idordersupplies)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BillSuppl__idord__3E52440B");
        });

        modelBuilder.Entity<Cart>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Cart__3213E83F4F76A3E3");

            entity.ToTable("Cart");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.IdKm).HasColumnName("idKM");

            entity.HasOne(d => d.IdKmNavigation).WithMany(p => p.Carts)
                .HasForeignKey(d => d.IdKm)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Cart__idKM__48CFD27E");
        });

        modelBuilder.Entity<CartItem>(entity =>
        {
            entity.HasKey(e => new { e.IdCart, e.IdFood }).HasName("PK__CartItem__3D3E7A3D793BCF86");

            entity.Property(e => e.IdCart).HasColumnName("idCart");
            entity.Property(e => e.IdFood).HasColumnName("idFood");
            entity.Property(e => e.SoLuong).HasColumnName("soLuong");

            entity.HasOne(d => d.IdCartNavigation).WithMany(p => p.CartItems)
                .HasForeignKey(d => d.IdCart)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CartItems__idCar__4BAC3F29");

            entity.HasOne(d => d.IdFoodNavigation).WithMany(p => p.CartItems)
                .HasForeignKey(d => d.IdFood)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CartItems__idFoo__4CA06362");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Customer__3213E83FBAF7930A");

            entity.ToTable("Customer");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.DiaChi)
                .HasMaxLength(100)
                .HasColumnName("diaChi");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.IdAccount).HasColumnName("idAccount");
            entity.Property(e => e.SoDienThoai)
                .HasMaxLength(15)
                .HasColumnName("soDienThoai");
            entity.Property(e => e.SoTienTieu)
                .HasMaxLength(15)
                .HasColumnName("soTienTieu");
            entity.Property(e => e.TenKhachHang)
                .HasMaxLength(100)
                .HasColumnName("tenKhachHang");

            entity.HasOne(d => d.IdAccountNavigation).WithMany(p => p.Customers)
                .HasForeignKey(d => d.IdAccount)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Customer__idAcco__440B1D61");
        });

        modelBuilder.Entity<DeliveryMethod>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Delivery__3213E83F2CC4443E");

            entity.ToTable("DeliveryMethod");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Gia).HasColumnName("gia");
            entity.Property(e => e.Tenphuongthucthanhtoan)
                .HasMaxLength(100)
                .HasColumnName("tenphuongthucthanhtoan");
            entity.Property(e => e.Trangthai).HasColumnName("trangthai");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Employee__3213E83F8452A7D5");

            entity.ToTable("Employee");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Birth).HasColumnName("birth");
            entity.Property(e => e.DiaChi)
                .HasMaxLength(100)
                .HasColumnName("diaChi");
            entity.Property(e => e.DienThoai)
                .HasMaxLength(15)
                .HasColumnName("dienThoai");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.IdShift).HasColumnName("idShift");
            entity.Property(e => e.Idaccount).HasColumnName("idaccount");
            entity.Property(e => e.NgayBatDau).HasColumnName("ngayBatDau");
            entity.Property(e => e.TenNhanVien)
                .HasMaxLength(100)
                .HasColumnName("tenNhanVien");
            entity.Property(e => e.TrangThai).HasColumnName("trangThai");

            entity.HasOne(d => d.IdShiftNavigation).WithMany(p => p.Employees)
                .HasForeignKey(d => d.IdShift)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Employee__idShif__300424B4");

            entity.HasOne(d => d.IdaccountNavigation).WithMany(p => p.Employees)
                .HasForeignKey(d => d.Idaccount)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Employee__idacco__2F10007B");
        });

        modelBuilder.Entity<Food>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Food__3213E83F82D81976");

            entity.ToTable("Food");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.GiaTien).HasColumnName("giaTien");
            entity.Property(e => e.Hinhanh)
                .HasMaxLength(255)
                .HasColumnName("hinhanh");
            entity.Property(e => e.IdDanhMuc).HasColumnName("idDanhMuc");
            entity.Property(e => e.Mota)
                .HasMaxLength(100)
                .HasColumnName("mota");
            entity.Property(e => e.Size)
                .HasMaxLength(50)
                .HasColumnName("size");
            entity.Property(e => e.Soluong).HasColumnName("soluong");
            entity.Property(e => e.TenMonAn)
                .HasMaxLength(100)
                .HasColumnName("tenMonAn");
            entity.Property(e => e.TrangThai).HasColumnName("trangThai");

            entity.HasOne(d => d.IdDanhMucNavigation).WithMany(p => p.Foods)
                .HasForeignKey(d => d.IdDanhMuc)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Food__idDanhMuc__36B12243");
        });

        modelBuilder.Entity<FoodCategory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__FoodCate__3213E83F2442522D");

            entity.ToTable("FoodCategory");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Hinhanh)
                .HasMaxLength(255)
                .HasColumnName("hinhanh");
            entity.Property(e => e.Mota)
                .HasMaxLength(100)
                .HasColumnName("mota");
            entity.Property(e => e.TenDanhMuc)
                .HasMaxLength(100)
                .HasColumnName("tenDanhMuc");
        });

        modelBuilder.Entity<Function>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Function__3213E83F1DC3B41C");

            entity.ToTable("Function");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Mota)
                .HasMaxLength(100)
                .HasColumnName("mota");
            entity.Property(e => e.Ten)
                .HasMaxLength(100)
                .HasColumnName("ten");
            entity.Property(e => e.TrangThai).HasColumnName("trangThai");
        });

        modelBuilder.Entity<InfoFood>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__InfoFood__3213E83F97A071D9");

            entity.ToTable("InfoFood");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.IdFood).HasColumnName("idFood");
            entity.Property(e => e.IdNguyenLieu).HasColumnName("idNguyenLieu");
            entity.Property(e => e.Mota)
                .HasMaxLength(100)
                .HasColumnName("mota");
            entity.Property(e => e.Songuyenlieucan).HasColumnName("songuyenlieucan");

            entity.HasOne(d => d.IdFoodNavigation).WithMany(p => p.InfoFoods)
                .HasForeignKey(d => d.IdFood)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__InfoFood__idFood__619B8048");

            entity.HasOne(d => d.IdNguyenLieuNavigation).WithMany(p => p.InfoFoods)
                .HasForeignKey(d => d.IdNguyenLieu)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__InfoFood__idNguy__628FA481");
        });

        modelBuilder.Entity<Latest>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Latest__3213E83F8095B9FA");

            entity.ToTable("Latest");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Date)
                .HasColumnType("datetime")
                .HasColumnName("date");
            entity.Property(e => e.IdBill).HasColumnName("idBill");
            entity.Property(e => e.IdBillSupplies).HasColumnName("idBillSupplies");
            entity.Property(e => e.IdCustomer).HasColumnName("idCustomer");
            entity.Property(e => e.IdOrderSupplies).HasColumnName("idOrderSupplies");
            entity.Property(e => e.NoiDung)
                .HasMaxLength(100)
                .HasColumnName("noiDung");

            entity.HasOne(d => d.IdBillNavigation).WithMany(p => p.Latests)
                .HasForeignKey(d => d.IdBill)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Latest__idBill__6B24EA82");

            entity.HasOne(d => d.IdBillSuppliesNavigation).WithMany(p => p.Latests)
                .HasForeignKey(d => d.IdBillSupplies)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Latest__idBillSu__6C190EBB");

            entity.HasOne(d => d.IdCustomerNavigation).WithMany(p => p.Latests)
                .HasForeignKey(d => d.IdCustomer)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Latest__idCustom__6D0D32F4");

            entity.HasOne(d => d.IdOrderSuppliesNavigation).WithMany(p => p.Latests)
                .HasForeignKey(d => d.IdOrderSupplies)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Latest__idOrderS__6E01572D");
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Notifica__3213E83F4577FF1A");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Date)
                .HasColumnType("datetime")
                .HasColumnName("date");
            entity.Property(e => e.IdBill).HasColumnName("idBill");
            entity.Property(e => e.IdBillsupplies).HasColumnName("idBillsupplies");
            entity.Property(e => e.IdCustomer).HasColumnName("idCustomer");
            entity.Property(e => e.IdOrder).HasColumnName("idOrder");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.NoiDung)
                .HasMaxLength(50)
                .HasColumnName("noiDung");

            entity.HasOne(d => d.IdBillNavigation).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.IdBill)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Notificat__idBil__6754599E");

            entity.HasOne(d => d.IdBillsuppliesNavigation).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.IdBillsupplies)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Notificat__idBil__68487DD7");

            entity.HasOne(d => d.IdCustomerNavigation).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.IdCustomer)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Notificat__idCus__656C112C");

            entity.HasOne(d => d.IdOrderNavigation).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.IdOrder)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Notificat__idOrd__66603565");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Order__3213E83F3E5F13F4");

            entity.ToTable("Order");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Date)
                .HasColumnType("datetime")
                .HasColumnName("date");
            entity.Property(e => e.IdCart).HasColumnName("idCart");
            entity.Property(e => e.IdDelivery).HasColumnName("idDelivery");
            entity.Property(e => e.IdFood).HasColumnName("idFood");
            entity.Property(e => e.IdKhachHang).HasColumnName("idKhachHang");
            entity.Property(e => e.TongSoLuong).HasColumnName("tongSoLuong");
            entity.Property(e => e.TrangThai)
                .HasMaxLength(100)
                .HasColumnName("trangThai");

            entity.HasOne(d => d.IdCartNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.IdCart)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Order__idCart__534D60F1");

            entity.HasOne(d => d.IdDeliveryNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.IdDelivery)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Order__idDeliver__5441852A");

            entity.HasOne(d => d.IdFoodNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.IdFood)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Order__idFood__52593CB8");

            entity.HasOne(d => d.IdKhachHangNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.IdKhachHang)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Order__idKhachHa__5165187F");
        });

        modelBuilder.Entity<OrderInfo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__OrderInf__3213E83F0C80092F");

            entity.ToTable("OrderInfo");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.IdMonAn).HasColumnName("idMonAn");
            entity.Property(e => e.IdUser).HasColumnName("idUser");
            entity.Property(e => e.Mota)
                .HasMaxLength(100)
                .HasColumnName("mota");
            entity.Property(e => e.SoLuongHangMon).HasColumnName("soLuongHangMon");

            entity.HasOne(d => d.IdMonAnNavigation).WithMany(p => p.OrderInfos)
                .HasForeignKey(d => d.IdMonAn)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__OrderInfo__idMon__5812160E");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.OrderInfos)
                .HasForeignKey(d => d.IdUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__OrderInfo__idUse__571DF1D5");
        });

        modelBuilder.Entity<OrderSupply>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__OrderSup__3213E83F09BFF8CF");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Datetime)
                .HasColumnType("datetime")
                .HasColumnName("datetime");
            entity.Property(e => e.Donvi)
                .HasMaxLength(50)
                .HasColumnName("donvi");
            entity.Property(e => e.IdNhaCungCap).HasColumnName("idNhaCungCap");
            entity.Property(e => e.IdNhanVien).HasColumnName("idNhanVien");
            entity.Property(e => e.Mota)
                .HasMaxLength(100)
                .HasColumnName("mota");
            entity.Property(e => e.SoLuongNguyenLieu)
                .HasMaxLength(50)
                .HasColumnName("soLuongNguyenLieu");
            entity.Property(e => e.TenNguyenLieu)
                .HasMaxLength(50)
                .HasColumnName("tenNguyenLieu");
            entity.Property(e => e.TrangThai)
                .HasMaxLength(50)
                .HasColumnName("trangThai");

            entity.HasOne(d => d.IdNhaCungCapNavigation).WithMany(p => p.OrderSupplies)
                .HasForeignKey(d => d.IdNhaCungCap)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__OrderSupp__idNha__3A81B327");

            entity.HasOne(d => d.IdNhanVienNavigation).WithMany(p => p.OrderSupplies)
                .HasForeignKey(d => d.IdNhanVien)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__OrderSupp__idNha__398D8EEE");

            entity.HasMany(d => d.Idsupplies).WithMany(p => p.Idordersupplies)
                .UsingEntity<Dictionary<string, object>>(
                    "OrderSuppliesInfo",
                    r => r.HasOne<Supply>().WithMany()
                        .HasForeignKey("Idsupplies")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__OrderSupp__idsup__71D1E811"),
                    l => l.HasOne<OrderSupply>().WithMany()
                        .HasForeignKey("Idordersupplies")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__OrderSupp__idord__70DDC3D8"),
                    j =>
                    {
                        j.HasKey("Idordersupplies", "Idsupplies").HasName("PK__OrderSup__50A25EC20C215F52");
                        j.ToTable("OrderSuppliesInfo");
                        j.IndexerProperty<int>("Idordersupplies").HasColumnName("idordersupplies");
                        j.IndexerProperty<int>("Idsupplies").HasColumnName("idsupplies");
                    });
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Payment__3213E83F054AB161");

            entity.ToTable("Payment");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Mota)
                .HasMaxLength(100)
                .HasColumnName("mota");
            entity.Property(e => e.PhuongThucThanhToan)
                .HasMaxLength(100)
                .HasColumnName("phuongThucThanhToan");
            entity.Property(e => e.TrangThai)
                .HasMaxLength(100)
                .HasColumnName("trangThai");
        });

        modelBuilder.Entity<Promotion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Promotio__3213E83F5E1DBE23");

            entity.ToTable("Promotion");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Code)
                .HasMaxLength(50)
                .HasColumnName("code");
            entity.Property(e => e.IdKm)
                .HasMaxLength(100)
                .HasColumnName("idKM");
            entity.Property(e => e.NgayBd).HasColumnName("ngayBD");
            entity.Property(e => e.NgayKt).HasColumnName("ngayKT");
            entity.Property(e => e.Noidung)
                .HasMaxLength(50)
                .HasColumnName("noidung");
            entity.Property(e => e.PhanTram).HasColumnName("phanTram");
            entity.Property(e => e.Ten)
                .HasMaxLength(100)
                .HasColumnName("ten");
            entity.Property(e => e.TrangThai).HasColumnName("trangThai");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Role__3213E83F11E11968");

            entity.ToTable("Role");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Mota)
                .HasMaxLength(50)
                .HasColumnName("mota");
            entity.Property(e => e.TenRole)
                .HasMaxLength(100)
                .HasColumnName("tenRole");
            entity.Property(e => e.TrangThai).HasColumnName("trangThai");

            entity.HasMany(d => d.Functions).WithMany(p => p.Roles)
                .UsingEntity<Dictionary<string, object>>(
                    "RoleFunction",
                    r => r.HasOne<Function>().WithMany()
                        .HasForeignKey("FunctionId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__RoleFunct__Funct__75A278F5"),
                    l => l.HasOne<Role>().WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__RoleFunct__RoleI__74AE54BC"),
                    j =>
                    {
                        j.HasKey("RoleId", "FunctionId").HasName("PK__RoleFunc__19E071B507D06873");
                        j.ToTable("RoleFunction");
                    });
        });

        modelBuilder.Entity<Shift>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Shift__3213E83F32C890D8");

            entity.ToTable("Shift");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.GioBatDau).HasColumnName("gioBatDau");
            entity.Property(e => e.GioKetThuc).HasColumnName("gioKetThuc");
            entity.Property(e => e.NgayCa).HasColumnName("ngayCa");
        });

        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Supplier__3213E83FD1271E11");

            entity.ToTable("Supplier");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.DiaChi)
                .HasMaxLength(100)
                .HasColumnName("diaChi");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.Lienhe)
                .HasMaxLength(15)
                .HasColumnName("lienhe");
            entity.Property(e => e.TenNhaCungCap)
                .HasMaxLength(100)
                .HasColumnName("tenNhaCungCap");
        });

        modelBuilder.Entity<Supply>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Supplies__3213E83F1C280462");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.DonVi).HasMaxLength(50);
            entity.Property(e => e.IdBill).HasColumnName("idBill");
            entity.Property(e => e.TenNguyenLieu)
                .HasMaxLength(100)
                .HasColumnName("tenNguyenLieu");

            entity.HasOne(d => d.IdBillNavigation).WithMany(p => p.Supplies)
                .HasForeignKey(d => d.IdBill)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Supplies__idBill__412EB0B6");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
