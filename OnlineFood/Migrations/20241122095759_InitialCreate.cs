using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineFood.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DeliveryMethod",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    tenphuongthucthanhtoan = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    gia = table.Column<double>(type: "float", nullable: false),
                    trangthai = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Delivery__3213E83F434C64A6", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "FoodCategory",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    tenDanhMuc = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    mota = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    hinhanh = table.Column<byte[]>(type: "image", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__FoodCate__3213E83FB8440025", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Function",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    ten = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    mota = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    trangThai = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Function__3213E83FBA4A7A33", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "FunctionRole",
                columns: table => new
                {
                    FunctionId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FunctionRole", x => new { x.FunctionId, x.RoleId });
                });

            migrationBuilder.CreateTable(
                name: "OrderSupplySupply",
                columns: table => new
                {
                    Idordersupplies = table.Column<int>(type: "int", nullable: false),
                    Idsupplies = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderSupplySupply", x => new { x.Idordersupplies, x.Idsupplies });
                });

            migrationBuilder.CreateTable(
                name: "Payment",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    phuongThucThanhToan = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    mota = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    trangThai = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Payment__3213E83FA0251728", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Promotion",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    ten = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    idKM = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    phanTram = table.Column<int>(type: "int", nullable: false),
                    ngayBD = table.Column<DateOnly>(type: "date", nullable: false),
                    ngayKT = table.Column<DateOnly>(type: "date", nullable: false),
                    trangThai = table.Column<int>(type: "int", nullable: false),
                    code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    noidung = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Promotio__3213E83F1B2C5333", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    tenRole = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    trangThai = table.Column<int>(type: "int", nullable: false),
                    mota = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Role__3213E83FCA7186AB", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Shift",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    ngayCa = table.Column<DateOnly>(type: "date", nullable: false),
                    gioBatDau = table.Column<TimeOnly>(type: "time", nullable: false),
                    gioKetThuc = table.Column<TimeOnly>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Shift__3213E83F659C3EB0", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Supplier",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    tenNhaCungCap = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    diaChi = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    lienhe = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Supplier__3213E83F54B67DAD", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Food",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    tenMonAn = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    idDanhMuc = table.Column<int>(type: "int", nullable: false),
                    giaTien = table.Column<int>(type: "int", nullable: false),
                    trangThai = table.Column<int>(type: "int", nullable: false),
                    soluong = table.Column<int>(type: "int", nullable: false),
                    hinhanh = table.Column<byte[]>(type: "image", nullable: true),
                    size = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    mota = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Food__3213E83FCCEB0F11", x => x.id);
                    table.ForeignKey(
                        name: "FK__Food__idDanhMuc__36B12243",
                        column: x => x.idDanhMuc,
                        principalTable: "FoodCategory",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Account",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    userName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    tenHienThi = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    matKhau = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    idrole = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Account__3213E83F6ABF2C73", x => x.id);
                    table.ForeignKey(
                        name: "FK__Account__idrole__286302EC",
                        column: x => x.idrole,
                        principalTable: "Role",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "RoleFunction",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    FunctionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__RoleFunc__19E071B56DC0DA36", x => new { x.RoleId, x.FunctionId });
                    table.ForeignKey(
                        name: "FK__RoleFunct__Funct__72C60C4A",
                        column: x => x.FunctionId,
                        principalTable: "Function",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__RoleFunct__RoleI__71D1E811",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Cart",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    idKM = table.Column<int>(type: "int", nullable: false),
                    idFood = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Cart__3213E83F6D40E2AE", x => x.id);
                    table.ForeignKey(
                        name: "FK__Cart__idFood__49C3F6B7",
                        column: x => x.idFood,
                        principalTable: "Food",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__Cart__idKM__48CFD27E",
                        column: x => x.idKM,
                        principalTable: "Promotion",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    tenKhachHang = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    diaChi = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    soDienThoai = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    soTienTieu = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    idAccount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Customer__3213E83F56720074", x => x.id);
                    table.ForeignKey(
                        name: "FK__Customer__idAcco__440B1D61",
                        column: x => x.idAccount,
                        principalTable: "Account",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Employee",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    tenNhanVien = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    dienThoai = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    diaChi = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    idaccount = table.Column<int>(type: "int", nullable: false),
                    trangThai = table.Column<int>(type: "int", nullable: false),
                    idShift = table.Column<int>(type: "int", nullable: false),
                    birth = table.Column<DateOnly>(type: "date", nullable: false),
                    ngayBatDau = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Employee__3213E83F31CDC759", x => x.id);
                    table.ForeignKey(
                        name: "FK__Employee__idShif__300424B4",
                        column: x => x.idShift,
                        principalTable: "Shift",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__Employee__idacco__2F10007B",
                        column: x => x.idaccount,
                        principalTable: "Account",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    idKhachHang = table.Column<int>(type: "int", nullable: false),
                    date = table.Column<DateTime>(type: "datetime", nullable: false),
                    idFood = table.Column<int>(type: "int", nullable: false),
                    trangThai = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    tongSoLuong = table.Column<int>(type: "int", nullable: false),
                    idCart = table.Column<int>(type: "int", nullable: false),
                    idDelivery = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Order__3213E83FE6A07C37", x => x.id);
                    table.ForeignKey(
                        name: "FK__Order__idCart__5070F446",
                        column: x => x.idCart,
                        principalTable: "Cart",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__Order__idDeliver__5165187F",
                        column: x => x.idDelivery,
                        principalTable: "DeliveryMethod",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__Order__idFood__4F7CD00D",
                        column: x => x.idFood,
                        principalTable: "Food",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__Order__idKhachHa__4E88ABD4",
                        column: x => x.idKhachHang,
                        principalTable: "Customer",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "OrderInfo",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    idUser = table.Column<int>(type: "int", nullable: false),
                    idMonAn = table.Column<int>(type: "int", nullable: false),
                    mota = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    soLuongHangMon = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__OrderInf__3213E83F6CEDEB0F", x => x.id);
                    table.ForeignKey(
                        name: "FK__OrderInfo__idMon__5535A963",
                        column: x => x.idMonAn,
                        principalTable: "Food",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__OrderInfo__idUse__5441852A",
                        column: x => x.idUser,
                        principalTable: "Customer",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "OrderSupplies",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    soLuongNguyenLieu = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    mota = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    idNhanVien = table.Column<int>(type: "int", nullable: false),
                    idNhaCungCap = table.Column<int>(type: "int", nullable: false),
                    datetime = table.Column<DateTime>(type: "datetime", nullable: false),
                    trangThai = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    tenNguyenLieu = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    donvi = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__OrderSup__3213E83F101BD7AC", x => x.id);
                    table.ForeignKey(
                        name: "FK__OrderSupp__idNha__398D8EEE",
                        column: x => x.idNhanVien,
                        principalTable: "Employee",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__OrderSupp__idNha__3A81B327",
                        column: x => x.idNhaCungCap,
                        principalTable: "Supplier",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Bill",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    ngayCheckIn = table.Column<DateTime>(type: "datetime", nullable: false),
                    ngayCheckOut = table.Column<DateTime>(type: "datetime", nullable: false),
                    trangThai = table.Column<int>(type: "int", nullable: false),
                    idKhachHang = table.Column<int>(type: "int", nullable: false),
                    idNhanVien = table.Column<int>(type: "int", nullable: false),
                    idKM = table.Column<int>(type: "int", nullable: false),
                    tongTienTruoc = table.Column<double>(type: "float", nullable: false),
                    tongTienSau = table.Column<double>(type: "float", nullable: false),
                    idPhuongThuc = table.Column<int>(type: "int", nullable: false),
                    idOrderInfo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Bill__3213E83FCC72FE7F", x => x.id);
                    table.ForeignKey(
                        name: "FK__Bill__idKM__59FA5E80",
                        column: x => x.idKM,
                        principalTable: "Promotion",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__Bill__idKhachHan__5812160E",
                        column: x => x.idKhachHang,
                        principalTable: "Customer",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__Bill__idNhanVien__59063A47",
                        column: x => x.idNhanVien,
                        principalTable: "Employee",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__Bill__idOrderInf__5BE2A6F2",
                        column: x => x.idOrderInfo,
                        principalTable: "OrderInfo",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__Bill__idPhuongTh__5AEE82B9",
                        column: x => x.idPhuongThuc,
                        principalTable: "Payment",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "BillSupplies",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    idordersupplies = table.Column<int>(type: "int", nullable: false),
                    date = table.Column<DateTime>(type: "datetime", nullable: false),
                    mota = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    idNhaCungCap = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__BillSupp__3213E83F61ED6F47", x => x.id);
                    table.ForeignKey(
                        name: "FK__BillSuppl__idNha__3D5E1FD2",
                        column: x => x.idNhaCungCap,
                        principalTable: "Supplier",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__BillSuppl__idord__3E52440B",
                        column: x => x.idordersupplies,
                        principalTable: "OrderSupplies",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Latest",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    idBill = table.Column<int>(type: "int", nullable: false),
                    idBillSupplies = table.Column<int>(type: "int", nullable: false),
                    date = table.Column<DateTime>(type: "datetime", nullable: false),
                    noiDung = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    idCustomer = table.Column<int>(type: "int", nullable: false),
                    idOrderSupplies = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Latest__3213E83FCA79A96C", x => x.id);
                    table.ForeignKey(
                        name: "FK__Latest__idBillSu__693CA210",
                        column: x => x.idBillSupplies,
                        principalTable: "BillSupplies",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__Latest__idBill__68487DD7",
                        column: x => x.idBill,
                        principalTable: "Bill",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__Latest__idCustom__6A30C649",
                        column: x => x.idCustomer,
                        principalTable: "Customer",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__Latest__idOrderS__6B24EA82",
                        column: x => x.idOrderSupplies,
                        principalTable: "OrderSupplies",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    idCustomer = table.Column<int>(type: "int", nullable: false),
                    idOrder = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    noiDung = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    date = table.Column<DateTime>(type: "datetime", nullable: false),
                    idBill = table.Column<int>(type: "int", nullable: false),
                    idBillsupplies = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Notifica__3213E83F543FA400", x => x.id);
                    table.ForeignKey(
                        name: "FK__Notificat__idBil__6477ECF3",
                        column: x => x.idBill,
                        principalTable: "Bill",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__Notificat__idBil__656C112C",
                        column: x => x.idBillsupplies,
                        principalTable: "BillSupplies",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__Notificat__idCus__628FA481",
                        column: x => x.idCustomer,
                        principalTable: "Customer",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__Notificat__idOrd__6383C8BA",
                        column: x => x.idOrder,
                        principalTable: "Order",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Supplies",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    tenNguyenLieu = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SoLuong = table.Column<int>(type: "int", nullable: false),
                    DonVi = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    idBill = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Supplies__3213E83F8E96E3E7", x => x.id);
                    table.ForeignKey(
                        name: "FK__Supplies__idBill__412EB0B6",
                        column: x => x.idBill,
                        principalTable: "BillSupplies",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "InfoFood",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    songuyenlieucan = table.Column<int>(type: "int", nullable: false),
                    idFood = table.Column<int>(type: "int", nullable: false),
                    mota = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    idNguyenLieu = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__InfoFood__3213E83F7136D0CC", x => x.id);
                    table.ForeignKey(
                        name: "FK__InfoFood__idFood__5EBF139D",
                        column: x => x.idFood,
                        principalTable: "Food",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__InfoFood__idNguy__5FB337D6",
                        column: x => x.idNguyenLieu,
                        principalTable: "Supplies",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "OrderSuppliesInfo",
                columns: table => new
                {
                    idordersupplies = table.Column<int>(type: "int", nullable: false),
                    idsupplies = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__OrderSup__50A25EC26BBCAAB7", x => new { x.idordersupplies, x.idsupplies });
                    table.ForeignKey(
                        name: "FK__OrderSupp__idord__6E01572D",
                        column: x => x.idordersupplies,
                        principalTable: "OrderSupplies",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__OrderSupp__idsup__6EF57B66",
                        column: x => x.idsupplies,
                        principalTable: "Supplies",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Account_idrole",
                table: "Account",
                column: "idrole");

            migrationBuilder.CreateIndex(
                name: "IX_Bill_idKhachHang",
                table: "Bill",
                column: "idKhachHang");

            migrationBuilder.CreateIndex(
                name: "IX_Bill_idKM",
                table: "Bill",
                column: "idKM");

            migrationBuilder.CreateIndex(
                name: "IX_Bill_idNhanVien",
                table: "Bill",
                column: "idNhanVien");

            migrationBuilder.CreateIndex(
                name: "IX_Bill_idOrderInfo",
                table: "Bill",
                column: "idOrderInfo");

            migrationBuilder.CreateIndex(
                name: "IX_Bill_idPhuongThuc",
                table: "Bill",
                column: "idPhuongThuc");

            migrationBuilder.CreateIndex(
                name: "IX_BillSupplies_idNhaCungCap",
                table: "BillSupplies",
                column: "idNhaCungCap");

            migrationBuilder.CreateIndex(
                name: "IX_BillSupplies_idordersupplies",
                table: "BillSupplies",
                column: "idordersupplies");

            migrationBuilder.CreateIndex(
                name: "IX_Cart_idFood",
                table: "Cart",
                column: "idFood");

            migrationBuilder.CreateIndex(
                name: "IX_Cart_idKM",
                table: "Cart",
                column: "idKM");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_idAccount",
                table: "Customer",
                column: "idAccount");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_idaccount",
                table: "Employee",
                column: "idaccount");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_idShift",
                table: "Employee",
                column: "idShift");

            migrationBuilder.CreateIndex(
                name: "IX_Food_idDanhMuc",
                table: "Food",
                column: "idDanhMuc");

            migrationBuilder.CreateIndex(
                name: "IX_InfoFood_idFood",
                table: "InfoFood",
                column: "idFood");

            migrationBuilder.CreateIndex(
                name: "IX_InfoFood_idNguyenLieu",
                table: "InfoFood",
                column: "idNguyenLieu");

            migrationBuilder.CreateIndex(
                name: "IX_Latest_idBill",
                table: "Latest",
                column: "idBill");

            migrationBuilder.CreateIndex(
                name: "IX_Latest_idBillSupplies",
                table: "Latest",
                column: "idBillSupplies");

            migrationBuilder.CreateIndex(
                name: "IX_Latest_idCustomer",
                table: "Latest",
                column: "idCustomer");

            migrationBuilder.CreateIndex(
                name: "IX_Latest_idOrderSupplies",
                table: "Latest",
                column: "idOrderSupplies");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_idBill",
                table: "Notifications",
                column: "idBill");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_idBillsupplies",
                table: "Notifications",
                column: "idBillsupplies");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_idCustomer",
                table: "Notifications",
                column: "idCustomer");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_idOrder",
                table: "Notifications",
                column: "idOrder");

            migrationBuilder.CreateIndex(
                name: "IX_Order_idCart",
                table: "Order",
                column: "idCart");

            migrationBuilder.CreateIndex(
                name: "IX_Order_idDelivery",
                table: "Order",
                column: "idDelivery");

            migrationBuilder.CreateIndex(
                name: "IX_Order_idFood",
                table: "Order",
                column: "idFood");

            migrationBuilder.CreateIndex(
                name: "IX_Order_idKhachHang",
                table: "Order",
                column: "idKhachHang");

            migrationBuilder.CreateIndex(
                name: "IX_OrderInfo_idMonAn",
                table: "OrderInfo",
                column: "idMonAn");

            migrationBuilder.CreateIndex(
                name: "IX_OrderInfo_idUser",
                table: "OrderInfo",
                column: "idUser");

            migrationBuilder.CreateIndex(
                name: "IX_OrderSupplies_idNhaCungCap",
                table: "OrderSupplies",
                column: "idNhaCungCap");

            migrationBuilder.CreateIndex(
                name: "IX_OrderSupplies_idNhanVien",
                table: "OrderSupplies",
                column: "idNhanVien");

            migrationBuilder.CreateIndex(
                name: "IX_OrderSuppliesInfo_idsupplies",
                table: "OrderSuppliesInfo",
                column: "idsupplies");

            migrationBuilder.CreateIndex(
                name: "IX_RoleFunction_FunctionId",
                table: "RoleFunction",
                column: "FunctionId");

            migrationBuilder.CreateIndex(
                name: "IX_Supplies_idBill",
                table: "Supplies",
                column: "idBill");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FunctionRole");

            migrationBuilder.DropTable(
                name: "InfoFood");

            migrationBuilder.DropTable(
                name: "Latest");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "OrderSuppliesInfo");

            migrationBuilder.DropTable(
                name: "OrderSupplySupply");

            migrationBuilder.DropTable(
                name: "RoleFunction");

            migrationBuilder.DropTable(
                name: "Bill");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "Supplies");

            migrationBuilder.DropTable(
                name: "Function");

            migrationBuilder.DropTable(
                name: "OrderInfo");

            migrationBuilder.DropTable(
                name: "Payment");

            migrationBuilder.DropTable(
                name: "Cart");

            migrationBuilder.DropTable(
                name: "DeliveryMethod");

            migrationBuilder.DropTable(
                name: "BillSupplies");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropTable(
                name: "Food");

            migrationBuilder.DropTable(
                name: "Promotion");

            migrationBuilder.DropTable(
                name: "OrderSupplies");

            migrationBuilder.DropTable(
                name: "FoodCategory");

            migrationBuilder.DropTable(
                name: "Employee");

            migrationBuilder.DropTable(
                name: "Supplier");

            migrationBuilder.DropTable(
                name: "Shift");

            migrationBuilder.DropTable(
                name: "Account");

            migrationBuilder.DropTable(
                name: "Role");
        }
    }
}
