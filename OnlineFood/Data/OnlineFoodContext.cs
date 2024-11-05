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
        => optionsBuilder.UseSqlServer("name=ConnectionStrings:OnlineFoodDatabase");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Account__3213E83F6ABF2C73");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.IdroleNavigation).WithMany(p => p.Accounts).HasConstraintName("FK__Account__idrole__286302EC");
        });

        modelBuilder.Entity<Bill>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Bill__3213E83FCC72FE7F");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.IdKhachHangNavigation).WithMany(p => p.Bills)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Bill__idKhachHan__5812160E");

            entity.HasOne(d => d.IdKmNavigation).WithMany(p => p.Bills)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Bill__idKM__59FA5E80");

            entity.HasOne(d => d.IdNhanVienNavigation).WithMany(p => p.Bills)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Bill__idNhanVien__59063A47");

            entity.HasOne(d => d.IdOrderInfoNavigation).WithMany(p => p.Bills)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Bill__idOrderInf__5BE2A6F2");

            entity.HasOne(d => d.IdPhuongThucNavigation).WithMany(p => p.Bills)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Bill__idPhuongTh__5AEE82B9");
        });

        modelBuilder.Entity<BillSupply>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__BillSupp__3213E83F61ED6F47");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.IdNhaCungCapNavigation).WithMany(p => p.BillSupplies)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BillSuppl__idNha__3D5E1FD2");

            entity.HasOne(d => d.IdordersuppliesNavigation).WithMany(p => p.BillSupplies)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BillSuppl__idord__3E52440B");
        });

        modelBuilder.Entity<Cart>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Cart__3213E83F6D40E2AE");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.IdFoodNavigation).WithMany(p => p.Carts)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Cart__idFood__49C3F6B7");

            entity.HasOne(d => d.IdKmNavigation).WithMany(p => p.Carts)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Cart__idKM__48CFD27E");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Customer__3213E83F56720074");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.IdAccountNavigation).WithMany(p => p.Customers)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Customer__idAcco__440B1D61");
        });

        modelBuilder.Entity<DeliveryMethod>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Delivery__3213E83F434C64A6");

            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Employee__3213E83F31CDC759");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.IdShiftNavigation).WithMany(p => p.Employees)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Employee__idShif__300424B4");

            entity.HasOne(d => d.IdaccountNavigation).WithMany(p => p.Employees)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Employee__idacco__2F10007B");
        });

        modelBuilder.Entity<Food>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Food__3213E83FCCEB0F11");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.IdDanhMucNavigation).WithMany(p => p.Foods)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Food__idDanhMuc__36B12243");
        });

        modelBuilder.Entity<FoodCategory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__FoodCate__3213E83FB8440025");

            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<Function>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Function__3213E83FBA4A7A33");

            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<InfoFood>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__InfoFood__3213E83F7136D0CC");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.IdFoodNavigation).WithMany(p => p.InfoFoods)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__InfoFood__idFood__5EBF139D");

            entity.HasOne(d => d.IdNguyenLieuNavigation).WithMany(p => p.InfoFoods)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__InfoFood__idNguy__5FB337D6");
        });

        modelBuilder.Entity<Latest>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Latest__3213E83FCA79A96C");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.IdBillNavigation).WithMany(p => p.Latests)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Latest__idBill__68487DD7");

            entity.HasOne(d => d.IdBillSuppliesNavigation).WithMany(p => p.Latests)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Latest__idBillSu__693CA210");

            entity.HasOne(d => d.IdCustomerNavigation).WithMany(p => p.Latests)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Latest__idCustom__6A30C649");

            entity.HasOne(d => d.IdOrderSuppliesNavigation).WithMany(p => p.Latests)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Latest__idOrderS__6B24EA82");
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Notifica__3213E83F543FA400");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.IdBillNavigation).WithMany(p => p.Notifications)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Notificat__idBil__6477ECF3");

            entity.HasOne(d => d.IdBillsuppliesNavigation).WithMany(p => p.Notifications)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Notificat__idBil__656C112C");

            entity.HasOne(d => d.IdCustomerNavigation).WithMany(p => p.Notifications)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Notificat__idCus__628FA481");

            entity.HasOne(d => d.IdOrderNavigation).WithMany(p => p.Notifications)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Notificat__idOrd__6383C8BA");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Order__3213E83FE6A07C37");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.IdCartNavigation).WithMany(p => p.Orders)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Order__idCart__5070F446");

            entity.HasOne(d => d.IdDeliveryNavigation).WithMany(p => p.Orders)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Order__idDeliver__5165187F");

            entity.HasOne(d => d.IdFoodNavigation).WithMany(p => p.Orders)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Order__idFood__4F7CD00D");

            entity.HasOne(d => d.IdKhachHangNavigation).WithMany(p => p.Orders)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Order__idKhachHa__4E88ABD4");
        });

        modelBuilder.Entity<OrderInfo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__OrderInf__3213E83F6CEDEB0F");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.IdMonAnNavigation).WithMany(p => p.OrderInfos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__OrderInfo__idMon__5535A963");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.OrderInfos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__OrderInfo__idUse__5441852A");
        });

        modelBuilder.Entity<OrderSupply>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__OrderSup__3213E83F101BD7AC");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.IdNhaCungCapNavigation).WithMany(p => p.OrderSupplies)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__OrderSupp__idNha__3A81B327");

            entity.HasOne(d => d.IdNhanVienNavigation).WithMany(p => p.OrderSupplies)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__OrderSupp__idNha__398D8EEE");

            entity.HasMany(d => d.Idsupplies).WithMany(p => p.Idordersupplies)
                .UsingEntity<Dictionary<string, object>>(
                    "OrderSuppliesInfo",
                    r => r.HasOne<Supply>().WithMany()
                        .HasForeignKey("Idsupplies")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__OrderSupp__idsup__6EF57B66"),
                    l => l.HasOne<OrderSupply>().WithMany()
                        .HasForeignKey("Idordersupplies")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__OrderSupp__idord__6E01572D"),
                    j =>
                    {
                        j.HasKey("Idordersupplies", "Idsupplies").HasName("PK__OrderSup__50A25EC26BBCAAB7");
                        j.ToTable("OrderSuppliesInfo");
                        j.IndexerProperty<int>("Idordersupplies").HasColumnName("idordersupplies");
                        j.IndexerProperty<int>("Idsupplies").HasColumnName("idsupplies");
                    });
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Payment__3213E83FA0251728");

            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<Promotion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Promotio__3213E83F1B2C5333");

            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Role__3213E83FCA7186AB");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasMany(d => d.Functions).WithMany(p => p.Roles)
                .UsingEntity<Dictionary<string, object>>(
                    "RoleFunction",
                    r => r.HasOne<Function>().WithMany()
                        .HasForeignKey("FunctionId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__RoleFunct__Funct__72C60C4A"),
                    l => l.HasOne<Role>().WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__RoleFunct__RoleI__71D1E811"),
                    j =>
                    {
                        j.HasKey("RoleId", "FunctionId").HasName("PK__RoleFunc__19E071B56DC0DA36");
                        j.ToTable("RoleFunction");
                    });
        });

        modelBuilder.Entity<Shift>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Shift__3213E83F659C3EB0");

            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Supplier__3213E83F54B67DAD");

            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<Supply>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Supplies__3213E83F8E96E3E7");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.IdBillNavigation).WithMany(p => p.Supplies)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Supplies__idBill__412EB0B6");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
