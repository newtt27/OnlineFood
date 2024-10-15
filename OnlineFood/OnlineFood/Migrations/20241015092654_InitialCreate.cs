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
                name: "Account",
                columns: table => new
                {
                    username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    password = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    tenhienthi = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    loaitaikhoan = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Account__F3DBC573E86863FE", x => x.username);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    tenDanhmuc = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    image = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Categori__3213E83FCD4869B4", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Event",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    ten = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    noidung = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    phantram = table.Column<int>(type: "int", nullable: false),
                    ngaybatdau = table.Column<DateOnly>(type: "date", nullable: false),
                    ngayketthuc = table.Column<DateOnly>(type: "date", nullable: false),
                    trangthai = table.Column<int>(type: "int", nullable: false),
                    image = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Event__3213E83F1F3153FA", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    hovaten = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    diachi = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    sodienthoai = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ngaylap = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    image = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Customer__3213E83FF9F7E20F", x => x.id);
                    table.ForeignKey(
                        name: "FK__Customer__userna__2D27B809",
                        column: x => x.username,
                        principalTable: "Account",
                        principalColumn: "username",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    tenquyen = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    trangthai = table.Column<int>(type: "int", nullable: false),
                    username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Role__3213E83F596C93BB", x => x.id);
                    table.ForeignKey(
                        name: "FK__Role__username__300424B4",
                        column: x => x.username,
                        principalTable: "Account",
                        principalColumn: "username",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    ten = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    idDanhmuc = table.Column<int>(type: "int", nullable: false),
                    mota = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    soluong = table.Column<int>(type: "int", nullable: false),
                    giatien = table.Column<int>(type: "int", nullable: false),
                    trangthai = table.Column<int>(type: "int", nullable: false),
                    image = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Product__3213E83F0559204F", x => x.id);
                    table.ForeignKey(
                        name: "FK__Product__idDanhm__2A4B4B5E",
                        column: x => x.idDanhmuc,
                        principalTable: "Categories",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Staff",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    hoten = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    diachi = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    sodienthoai = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    image = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    idquyen = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Staff__3213E83FC42A7DFC", x => x.id);
                    table.ForeignKey(
                        name: "FK__Staff__idquyen__32E0915F",
                        column: x => x.idquyen,
                        principalTable: "Role",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Cart",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    idSanpham = table.Column<int>(type: "int", nullable: false),
                    souong = table.Column<int>(type: "int", nullable: false),
                    giatien = table.Column<int>(type: "int", nullable: false),
                    idKhachhang = table.Column<int>(type: "int", nullable: false),
                    trangthai = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Cart__3213E83F20DED48B", x => x.id);
                    table.ForeignKey(
                        name: "FK__Cart__idKhachhan__36B12243",
                        column: x => x.idKhachhang,
                        principalTable: "Customer",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__Cart__idSanpham__35BCFE0A",
                        column: x => x.idSanpham,
                        principalTable: "Product",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Payment",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    idKhachhang = table.Column<int>(type: "int", nullable: false),
                    idSanpham = table.Column<int>(type: "int", nullable: false),
                    soluong = table.Column<int>(type: "int", nullable: false),
                    tongtien = table.Column<int>(type: "int", nullable: false),
                    date = table.Column<DateOnly>(type: "date", nullable: false),
                    idCart = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Payment__3213E83FCDD43B76", x => x.id);
                    table.ForeignKey(
                        name: "FK__Payment__idCart__3A81B327",
                        column: x => x.idCart,
                        principalTable: "Cart",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__Payment__idKhach__398D8EEE",
                        column: x => x.idKhachhang,
                        principalTable: "Customer",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Paymentdetail",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    idKhachhang = table.Column<int>(type: "int", nullable: false),
                    idSanpham = table.Column<int>(type: "int", nullable: false),
                    soluong = table.Column<int>(type: "int", nullable: false),
                    giatienTruoc = table.Column<int>(type: "int", nullable: false),
                    hotenKhachhang = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    date = table.Column<DateOnly>(type: "date", nullable: false),
                    paymentMode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    giaTienSau = table.Column<int>(type: "int", nullable: false),
                    idKuyenmai = table.Column<int>(type: "int", nullable: true),
                    idPayment = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Paymentd__3213E83F5B10CF37", x => x.id);
                    table.ForeignKey(
                        name: "FK__Paymentde__idKha__3D5E1FD2",
                        column: x => x.idKhachhang,
                        principalTable: "Customer",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__Paymentde__idKuy__403A8C7D",
                        column: x => x.idKuyenmai,
                        principalTable: "Event",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK__Paymentde__idPay__3E52440B",
                        column: x => x.idPayment,
                        principalTable: "Payment",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__Paymentde__idSan__3F466844",
                        column: x => x.idSanpham,
                        principalTable: "Product",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cart_idKhachhang",
                table: "Cart",
                column: "idKhachhang");

            migrationBuilder.CreateIndex(
                name: "IX_Cart_idSanpham",
                table: "Cart",
                column: "idSanpham");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_username",
                table: "Customer",
                column: "username");

            migrationBuilder.CreateIndex(
                name: "IX_Payment_idCart",
                table: "Payment",
                column: "idCart");

            migrationBuilder.CreateIndex(
                name: "IX_Payment_idKhachhang",
                table: "Payment",
                column: "idKhachhang");

            migrationBuilder.CreateIndex(
                name: "IX_Paymentdetail_idKhachhang",
                table: "Paymentdetail",
                column: "idKhachhang");

            migrationBuilder.CreateIndex(
                name: "IX_Paymentdetail_idKuyenmai",
                table: "Paymentdetail",
                column: "idKuyenmai");

            migrationBuilder.CreateIndex(
                name: "IX_Paymentdetail_idPayment",
                table: "Paymentdetail",
                column: "idPayment");

            migrationBuilder.CreateIndex(
                name: "IX_Paymentdetail_idSanpham",
                table: "Paymentdetail",
                column: "idSanpham");

            migrationBuilder.CreateIndex(
                name: "IX_Product_idDanhmuc",
                table: "Product",
                column: "idDanhmuc");

            migrationBuilder.CreateIndex(
                name: "IX_Role_username",
                table: "Role",
                column: "username");

            migrationBuilder.CreateIndex(
                name: "IX_Staff_idquyen",
                table: "Staff",
                column: "idquyen");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Paymentdetail");

            migrationBuilder.DropTable(
                name: "Staff");

            migrationBuilder.DropTable(
                name: "Event");

            migrationBuilder.DropTable(
                name: "Payment");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "Cart");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "Account");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
