USE OnlineFood;
GO

-- Create Function table first (since Role references it)
CREATE TABLE [Function] (
    id INT PRIMARY KEY NOT NULL,
    ten NVARCHAR(100) NOT NULL,
    mota NVARCHAR(100) NULL,
    trangThai int NOT NULL
);

-- Create Role table
CREATE TABLE Role (
    id INT PRIMARY KEY NOT NULL,
    tenRole NVARCHAR(100) NOT NULL,
    trangThai INT NOT NULL,
    mota NVARCHAR(50),
);

-- Create Account table
CREATE TABLE Account (
    id INT PRIMARY KEY NOT NULL,
    userName NVARCHAR(100) NOT NULL,
    tenHienThi NVARCHAR(100) NOT NULL,
    matKhau NVARCHAR(1000) NOT NULL,
    idrole INT,
    FOREIGN KEY (idrole) REFERENCES Role(id)
);

-- Create DeliveryMethod table
CREATE TABLE DeliveryMethod (
    id INT PRIMARY KEY NOT NULL,
    tenphuongthucthanhtoan NVARCHAR(100) NOT NULL,
    gia FLOAT NOT NULL,
    trangthai int NOT NULL
);

-- Create Shift table
CREATE TABLE Shift (
    id INT PRIMARY KEY NOT NULL,
    ngayCa DATE NOT NULL,
    gioBatDau TIME NOT NULL,
    gioKetThuc TIME NOT NULL
);

-- Create Employee table
CREATE TABLE Employee (
    id INT PRIMARY KEY NOT NULL,
    tenNhanVien NVARCHAR(100) NOT NULL,
    dienThoai NVARCHAR(15) NOT NULL,
    email NVARCHAR(100) NOT NULL,
    diaChi NVARCHAR(100) NOT NULL,
    idaccount INT NOT NULL,
    trangThai int NOT NULL,
    idShift INT NOT NULL,
    birth DATE NOT NULL,
    ngayBatDau DATE NOT NULL,
    FOREIGN KEY (idaccount) REFERENCES Account(id),
    FOREIGN KEY (idShift) REFERENCES Shift(id)
);

-- Create Supplier table
CREATE TABLE Supplier (
    id INT PRIMARY KEY NOT NULL,
    tenNhaCungCap NVARCHAR(100) NOT NULL,
    diaChi NVARCHAR(100) NOT NULL,
    lienhe NVARCHAR(15) NOT NULL,
    email NVARCHAR(100) NOT NULL
);

-- Create FoodCategory table
CREATE TABLE FoodCategory (
    id INT PRIMARY KEY NOT NULL,
    tenDanhMuc NVARCHAR(100) NOT NULL,
    mota NVARCHAR(100) NULL,
	hinhanh NVARCHAR(255) NULL,
);

-- Create Food table
CREATE TABLE Food (
    id INT PRIMARY KEY NOT NULL,
    tenMonAn NVARCHAR(100) NOT NULL,
    idDanhMuc INT NOT NULL,
    giaTien INT NOT NULL,
    trangThai INT NOT NULL,
    soluong INT NOT NULL,
	hinhanh NVARCHAR(255) NULL,
    size NVARCHAR(50) NOT NULL,
    mota NVARCHAR(100) NULL,
    FOREIGN KEY (idDanhMuc) REFERENCES FoodCategory(id)
);

-- Create OrderSupplies table
CREATE TABLE OrderSupplies (
    id INT PRIMARY KEY NOT NULL,
    soLuongNguyenLieu NVARCHAR(50) NOT NULL,
    mota NVARCHAR(100),
    idNhanVien INT NOT NULL,
    idNhaCungCap INT NOT NULL,
    datetime DATETIME NOT NULL,
    trangThai NVARCHAR(50) NOT NULL,
    tenNguyenLieu NVARCHAR(50) NOT NULL,
    donvi NVARCHAR(50) NOT NULL,
    FOREIGN KEY (idNhanVien) REFERENCES Employee(id),
    FOREIGN KEY (idNhaCungCap) REFERENCES Supplier(id)
);

-- Create BillSupplies table
CREATE TABLE BillSupplies (
    id INT PRIMARY KEY NOT NULL,
    idordersupplies INT NOT NULL,
    date DATETIME NOT NULL,
    mota NVARCHAR(50) NOT NULL,
    idNhaCungCap INT NOT NULL,
    FOREIGN KEY (idNhaCungCap) REFERENCES Supplier(id),
    FOREIGN KEY (idordersupplies) REFERENCES OrderSupplies(id)
);

-- Create Supplies table 
CREATE TABLE Supplies (
    id INT PRIMARY KEY NOT NULL,
    tenNguyenLieu NVARCHAR(100) NOT NULL,
    SoLuong INT NOT NULL,
    DonVi NVARCHAR(50) NOT NULL,
    idBill INT NOT NULL,
    FOREIGN KEY (idBill) REFERENCES BillSupplies(id)
);

-- Create Customer table 
CREATE TABLE Customer (
    id INT PRIMARY KEY NOT NULL,
    tenKhachHang NVARCHAR(100) NOT NULL,
    diaChi NVARCHAR(100) NOT NULL,
    email NVARCHAR(100) NOT NULL,
    soDienThoai NVARCHAR(15) NOT NULL,
    soTienTieu NVARCHAR(15) NOT NULL,
    idAccount INT NOT NULL, 
    FOREIGN KEY (idAccount) REFERENCES Account(id)
);

-- Create Promotion table
CREATE TABLE Promotion (
    id INT PRIMARY KEY NOT NULL,
    ten NVARCHAR(100) NOT NULL,
    idKM NVARCHAR(100) NOT NULL,
    phanTram INT NOT NULL,
    ngayBD DATE NOT NULL,
    ngayKT DATE NOT NULL,
    trangThai INT NOT NULL,
    code NVARCHAR(50) NOT NULL,
    noidung NVARCHAR(50) 
);

-- Create Cart table
CREATE TABLE Cart (
    id INT PRIMARY KEY NOT NULL,
    idKM INT NOT NULL,
    FOREIGN KEY (idKM) REFERENCES Promotion(id)
);
-- Create CartItems table
CREATE TABLE CartItems (
    idCart INT NOT NULL,             -- ID của giỏ hàng
    idFood INT NOT NULL,             -- ID của món ăn
    soLuong INT NOT NULL,            -- Số lượng món ăn trong giỏ hàng
    PRIMARY KEY (idCart, idFood),    -- Đảm bảo mỗi cặp (idCart, idFood) là duy nhất
    FOREIGN KEY (idCart) REFERENCES Cart(id),  -- Liên kết với bảng Cart
    FOREIGN KEY (idFood) REFERENCES Food(id)   -- Liên kết với bảng Food
);
-- Create Payment table
CREATE TABLE Payment (
    id INT PRIMARY KEY NOT NULL,
    phuongThucThanhToan NVARCHAR(100) NOT NULL,
    mota NVARCHAR(100),
    trangThai NVARCHAR(100) NOT NULL
);

-- Create Order table
CREATE TABLE [Order] (
    id INT PRIMARY KEY NOT NULL,
    idKhachHang INT NOT NULL,
    date DATETIME NOT NULL,
    idFood INT NOT NULL,
    trangThai NVARCHAR(100) NOT NULL,
    tongSoLuong INT NOT NULL,
    idCart INT NOT NULL,
    idDelivery INT NOT NULL,
    FOREIGN KEY (idKhachHang) REFERENCES Customer(id),
    FOREIGN KEY (idFood) REFERENCES Food(id),
    FOREIGN KEY (idCart) REFERENCES Cart(id),
    FOREIGN KEY (idDelivery) REFERENCES DeliveryMethod(id)
);

-- Create OrderInfo table
CREATE TABLE OrderInfo (
    id INT PRIMARY KEY NOT NULL,
    idUser INT NOT NULL,
    idMonAn INT NOT NULL,
    mota NVARCHAR(100),
    soLuongHangMon INT NOT NULL,
    FOREIGN KEY (idUser) REFERENCES Customer(id),
    FOREIGN KEY (idMonAn) REFERENCES Food(id)
);

-- Create Bill table
CREATE TABLE Bill (
    id INT PRIMARY KEY NOT NULL,
    ngayCheckIn DATETIME NOT NULL,
    ngayCheckOut DATETIME NOT NULL,
    trangThai INT NOT NULL,
    idKhachHang INT NOT NULL,
    idNhanVien INT NOT NULL,
    idKM INT NOT NULL,
    tongTienTruoc FLOAT NOT NULL,
    tongTienSau FLOAT NOT NULL,
    idPhuongThuc INT NOT NULL,
    idOrderInfo INT NOT NULL,
    FOREIGN KEY (idKhachHang) REFERENCES Customer(id),
    FOREIGN KEY (idNhanVien) REFERENCES Employee(id),
    FOREIGN KEY (idKM) REFERENCES Promotion(id),
    FOREIGN KEY (idPhuongThuc) REFERENCES Payment(id),
    FOREIGN KEY (idOrderInfo) REFERENCES OrderInfo(id)
);

-- Create InfoFood table
CREATE TABLE InfoFood (
    id INT PRIMARY KEY NOT NULL,
    songuyenlieucan INT NOT NULL,
    idFood INT NOT NULL,
    mota NVARCHAR(100) NULL,
    idNguyenLieu INT NOT NULL,
    FOREIGN KEY (idFood) REFERENCES Food(id),
    FOREIGN KEY (idNguyenLieu) REFERENCES Supplies(id)
);

-- Create Notifications table
CREATE TABLE Notifications (
    id INT PRIMARY KEY NOT NULL,
    idCustomer INT NOT NULL,
    idOrder INT NOT NULL,
    name NVARCHAR(100) NOT NULL,
    noiDung NVARCHAR(50) NOT NULL,
    date DATETIME NOT NULL,
    idBill INT NOT NULL,
    idBillsupplies INT NOT NULL,
    FOREIGN KEY (idCustomer) REFERENCES Customer(id),
    FOREIGN KEY (idOrder) REFERENCES [Order](id),
    FOREIGN KEY (idBill) REFERENCES Bill(id),
    FOREIGN KEY (idBillsupplies) REFERENCES BillSupplies(id)
);

-- Create Latest table
CREATE TABLE Latest (
    id INT PRIMARY KEY NOT NULL,
    idBill INT NOT NULL,
    idBillSupplies INT NOT NULL,
    date DATETIME NOT NULL,
    noiDung NVARCHAR(100) NOT NULL,
    idCustomer INT NOT NULL,
    idOrderSupplies INT NOT NULL,
    FOREIGN KEY (idBill) REFERENCES Bill(id),
    FOREIGN KEY (idBillSupplies) REFERENCES BillSupplies(id),
    FOREIGN KEY (idCustomer) REFERENCES Customer(id),
    FOREIGN KEY (idOrderSupplies) REFERENCES OrderSupplies(id)
);

-- Create OrderSuppliesInfo
CREATE TABLE OrderSuppliesInfo(
    idordersupplies INT NOT NULL,
    idsupplies INT NOT NULL,
    FOREIGN KEY (idordersupplies) REFERENCES OrderSupplies(id),
    FOREIGN KEY (idsupplies) REFERENCES Supplies(id),
    PRIMARY KEY (idordersupplies, idsupplies)
);

--Create RoleFunction table 
CREATE TABLE RoleFunction(
	RoleId INT NOT NULL,
	FunctionId INT NOT NULL,
	PRIMARY KEY (RoleId, FunctionId),
	FOREIGN KEY (RoleId) REFERENCES Role(id),
	FOREIGN KEY (FunctionId) REFERENCES [Function](id)
)

ALTER TABLE Account
ADD idCart INT NULL,
FOREIGN KEY (idCart) REFERENCES Cart(id)

-- Insert sample data
INSERT INTO [Function] (id, ten, mota, trangThai)
VALUES 
(1, N'Quản ly thống kê', N'Chịu trách nhiệm quản lý thống kê', 1),
(2, N'Quản lý nhân viên', N'Chịu trách nhiệm quản lý nhân viên', 1),
(3, N'Quản lý khách hàng', N'Chịu trách nhiệm quản lý khách hàng', 1),
(4, N'Quản lý sản phẩm', N'Chịu trách nhiệm quản lý sản phẩm', 1),
(5, N'Quản lý khuyến mãi', N'Chịu trách nhiệm quản lý khuyến mãi', 1),
(6, N'Quản lý nguyên liệu', N'Chịu trách nhiệm quản lý nguyên liệu', 1),
(7, N'Quản lý đặt nguyên liệu và hóa đơn nguyên liệu', N'Chịu trách nhiệm quản lý đặt nguyên liệu và hóa đơn nguyên liệu', 1),
(8, N'Quản lý đặt hàng', N'Chịu trách nhiệm quản lý đặt hàng', 1),
(9, N'Quản lý nhà cung cấp', N'Chịu trách nhiệm quản lý nhà cung cấp', 1),
(10, N'Quản lý hóa đơn', N'Chịu trách nhiệm quản lý hóa đơn', 1),
(11, N'Quản lý phương thức vận chuyển', N'Chịu trách nhiệm quản lý phương thức vận chuyển', 1),
(12, N'Quản lý phương thức thanh toán', N'Chịu trách nhiệm quản lý phương thức thanh toán', 1),
(13, N'Quản lý settings', N'Chịu trách nhiệm quản lý settings', 1),
(14, N'Phục vụ', N'Chịu trách nhiệm quản lý phục vụ món ăn', 2),
(15, N'Đầu bếp', N'Chịu trách nhiệm quản lý xử lí món ăn', 2);

INSERT INTO Role (id, tenRole, trangThai, mota)
VALUES (1, N'Admin', 1, N'Quản trị viên'),
       (2, N'Nhân viên', 2, N'Nhân viên bán hàng');

INSERT INTO Account (id, userName, tenHienThi, matKhau, idrole)
VALUES (1, 'admin', N'Quản trị viên', 'admin123', 1),
       (2, 'nhanvien1', N'Nhân viên 1', 'nv123', 2);

INSERT INTO DeliveryMethod (id, tenphuongthucthanhtoan, gia, trangthai)
VALUES (1, N'Giao hàng nhanh', 20000, 1),
       (2, N'Giao hàng tiêu chuẩn', 15000, 1);

INSERT INTO Shift (id, ngayCa, gioBatDau, gioKetThuc)
VALUES (1, '2023-06-01', '08:00:00', '16:00:00'),
       (2, '2023-06-01', '16:00:00', '00:00:00');

INSERT INTO Employee (id, tenNhanVien, dienThoai, email, diaChi, idaccount, trangThai, idShift, birth, ngayBatDau)
VALUES (1, N'Nguyễn Văn A', '0123456789', 'nva@example.com', N'Hà Nội', 2, 1, 1, '1990-01-01', '2023-01-01');

INSERT INTO Supplier (id, tenNhaCungCap, diaChi, lienhe, email)
VALUES (1, N'Nhà cung cấp A', N'Hà Nội', '0987654321', 'ncca@example.com');

INSERT INTO FoodCategory (id, tenDanhMuc, mota ,hinhanh)
VALUES (1, N'Meals', N'Các món ăn chính',NULL),
       (2, N'Desserts', N'Các món tráng miệng',NULL),
	   (3, N'Chickens', N'Các món gà',NULL),
	   (4, N'Pizzas', N'Các món pizza',NULL),
	   (5, N'Hamburgers', N'Các món hamburger',NULL),
	   (6, N'Drinks', N'Các món nước',NULL);

INSERT INTO Food (id, tenMonAn, idDanhMuc, giaTien, trangThai, soluong, hinhanh, size, mota)
VALUES
-- Meals
(1, N'Phở', 1, 100, 1, 50, NULL, N' bowl', N'Món ăn truyền thống của Việt Nam.'),
(2, N'Bún bò', 1, 150, 1, 40, NULL, N' bowl', N'Bún với thịt bò và gia vị thơm ngon.'),
(3, N'Bún riêu', 1, 120, 1, 35, NULL, N' bowl', N'Bún riêu cua đặc trưng.'),
(4, N'Bún thịt nướng', 1, 130, 1, 30, NULL, N' bowl', N'Bún với thịt nướng và rau sống.'),
(5, N'Cơm tấm', 1, 80, 1, 50, NULL, N' plate', N'Cơm tấm với sườn nướng.'),
(6, N'Xôi gà', 1, 110, 1, 45, NULL, N' bowl', N'Xôi gà thơm ngon.'),
(7, N'Bánh mì chảo', 1, 90, 1, 25, NULL, N' plate', N'Bánh mì ăn kèm với trứng và thịt.'),
(8, N'Hủ tiếu', 1, 140, 1, 30, NULL, N' bowl', N'Hủ tiếu nước dùng ngọt.'),
(9, N'Bánh mì thịt', 1, 160, 1, 30, NULL, N' piece', N'Bánh mì thịt với sốt mayonnaise.'),
(10, N'Súp cua', 1, 180, 1, 20, NULL, N' bowl', N'Súp cua hấp dẫn.'),

-- Desserts
(11, N'Chocolate Cake', 2, 50, 1, 25, NULL, N' piece', N'Tháo ngọt thơm ngon.'),
(12, N'Fruit Tart', 2, 60, 1, 20, NULL, N' piece', N'Bánh tart trái cây tươi.'),
(13, N'Brownie', 2, 70, 1, 30, NULL, N' piece', N'Brownie socola hấp dẫn.'),
(14, N'Tiramisu', 2, 80, 1, 15, NULL, N' piece', N'Tiramisu truyền thống.'),
(15, N'Sorbet', 2, 40, 1, 50, NULL, N' scoop', N'Sorbet trái cây mát lạnh.'),
(16, N'Panna Cotta', 2, 90, 1, 40, NULL, N' piece', N'Panna cotta kem béo.'),
(17, N'Cupcake', 2, 100, 1, 35, NULL, N' piece', N'Cupcake ngọt ngào.'),
(18, N'Macarons', 2, 110, 1, 20, NULL, N' piece', N'Macarons đa dạng hương vị.'),
(19, N'Milkshake', 2, 120, 1, 30, NULL, N' glass', N'Sinh tố kem sữa.'),
(20, N'Ice Cream', 2, 130, 1, 50, NULL, N' scoop', N'Kem tươi mát.'),

-- Chickens
(21, N'Fried Chicken', 3, 70, 1, 40, NULL, N' piece', N'Thịt gà rán giòn.'),
(22, N'Grilled Chicken Salad', 3, 90, 1, 35, NULL, N' plate', N'Salad gà nướng tươi ngon.'),
(23, N'Chicken Nuggets', 3, 60, 1, 50, NULL, N' piece', N'Gà viên chiên giòn.'),
(24, N'Sweet and Sour Chicken', 3, 80, 1, 30, NULL, N' plate', N'Gà xào chua ngọt.'),
(25, N'Chicken Teriyaki', 3, 100, 1, 25, NULL, N' plate', N'Gà teriyaki hấp dẫn.'),
(26, N'Chicken Alfredo', 3, 110, 1, 40, NULL, N' plate', N'Mì Ý gà sốt Alfredo.'),
(27, N'BBQ Chicken Pizza', 3, 120, 1, 30, NULL, N' pizza', N'Pizza gà BBQ thơm ngon.'),
(28, N'Spinach and Chicken Wrap', 3, 130, 1, 20, NULL, N' wrap', N'Wrap gà và rau chân vịt.'),
(29, N'Southern Fried Chicken', 3, 140, 1, 25, NULL, N' piece', N'Gà chiên kiểu miền Nam.'),
(30, N'Chicken Parmesan', 3, 150, 1, 30, NULL, N' plate', N'Gà parmesan với phô mai.'),

-- Pizzas
(31, N'Pepperoni Pizza', 4, 90, 1, 35, NULL, N' pizza', N'Pizza pepperoni ngon miệng.'),
(32, N'Margherita Pizza', 4, 80, 1, 30, NULL, N' pizza', N'Pizza Margherita tươi ngon.'),
(33, N'BBQ Chicken Pizza', 4, 100, 1, 20, NULL, N' pizza', N'Pizza BBQ gà.'),
(34, N'Mushroom Pizza', 4, 70, 1, 40, NULL, N' pizza', N'Pizza nấm thơm ngon.'),
(35, N'Veggie Pizza', 4, 75, 1, 35, NULL, N' pizza', N'Pizza rau củ tươi.'),
(36, N'Supreme Pizza', 4, 120, 1, 30, NULL, N' pizza', N'Pizza thập cẩm hấp dẫn.'),
(37, N'Stuffed Crust Pizza', 4, 140, 1, 25, NULL, N' pizza', N'Pizza với vỏ nhồi phô mai.'),
(38, N'Hawaiian Pizza', 4, 110, 1, 20, NULL, N' pizza', N'Pizza Hawaii với thịt xông khói.'),
(39, N'Buffalo Chicken Pizza', 4, 130, 1, 30, NULL, N' pizza', N'Pizza Buffalo gà cay.'),
(40, N'Four Cheese Pizza', 4, 150, 1, 25, NULL, N' pizza', N'Pizza bốn loại phô mai.'),

-- Hamburgers
(41, N'Cheeseburger', 5, 50, 1, 40, NULL, N' piece', N'Hamburger phô mai thơm ngon.'),
(42, N'Veggie Burger', 5, 60, 1, 35, NULL, N' piece', N'Hamburger chay tươi ngon.'),
(43, N'BBQ Bacon Burger', 5, 70, 1, 30, NULL, N' piece', N'Hamburger thịt xông khói BBQ.'),
(44, N'Mushroom Swiss Burger',5 , 80, 1, 20, NULL, N' piece', N'Hamburger nấm và phô mai Thụy Sĩ.'),
(45, N'Double Cheeseburger', 5, 90, 1, 30, NULL, N' piece', N'Hamburger đôi phô mai.'),
(46, N'Sriracha Burger', 5, 100, 1, 25, NULL, N' piece', N'Hamburger Sriracha cay.'),
(47, N'Breakfast Burger', 5, 110, 1, 20, NULL, N' piece', N'Hamburger bữa sáng.'),
(48, N'Jalapeño Burger', 5, 120, 1, 15, NULL, N' piece', N'Hamburger Jalapeño cay.'),
(49, N'Fish Burger', 5, 130, 1, 30, NULL, N' piece', N'Hamburger cá tươi.'),
(50, N'Pulled Pork Burger', 5, 140, 1, 25, NULL, N' piece', N'Hamburger thịt heo xé.'),

-- Drinks
(51, N'Cà phê', 6, 20, 1, 100, NULL, N' cup', N'Cà phê nóng hoặc lạnh.'),
(52, N'Trà', 6, 15, 1, 100, NULL, N' cup', N'Trà thảo mộc hoặc trà đen.'),
(53, N'Nước ngọt', 6, 10, 1, 100, NULL, N' can', N'Nước ngọt có ga.'),
(54, N'Nước', 6, 5, 1, 200, NULL, N' bottle', N'Nước khoáng tinh khiết.'),
(55, N'Nước trái cây', 6, 25, 1, 150, NULL, N' cup', N'Nước ép trái cây tươi.'),
(56, N'Smoothie', 6, 30, 1, 100, NULL, N' cup', N'Smoothie trái cây mát lạnh.'),
(57, N'Sinh tố', 6, 35, 1, 80, NULL, N' cup', N'Sinh tố kem sữa ngon.'),
(58, N'Nước tăng lực', 6, 40, 1, 50, NULL, N' can', N'Nước tăng lực bổ sung năng lượng.'),
(59, N'Nước khoáng có ga', 6, 20, 1, 100, NULL, N' bottle', N'Nước khoáng có ga.'),
(60, N'Cocktail', 6, 50, 1, 40, NULL, N' glass', N'Cocktail thơm ngon và hấp dẫn.');

INSERT INTO OrderSupplies (id, soLuongNguyenLieu, mota, idNhanVien, idNhaCungCap, datetime, trangThai, tenNguyenLieu, donvi)
VALUES (1, '10', N'Đặt hàng gạo', 1, 1, '2023-06-01 10:00:00', N'Đã đặt hàng', N'Gạo', N'kg');

INSERT INTO BillSupplies (id, idordersupplies, date, mota, idNhaCungCap)
VALUES (1, 1, '2023-06-02 10:00:00', N'Hóa đơn nhập gạo', 1);

INSERT INTO Supplies (id, tenNguyenLieu, SoLuong, DonVi, idBill)
VALUES (1, N'Gạo', 10, N'kg', 1), -- Gạo cho Cơm tấm
(2, N'Thịt bò', 5, N'kg', 1), -- Thịt bò cho Phở
(3, N'Bún', 8, N'kg', 1), -- Bún cho Bún bò
(4, N'Cua đồng', 4, N'kg', 1), -- Cua cho Bún riêu
(5, N'Thịt heo', 6, N'kg', 1), -- Thịt heo cho Bún thịt nướng
(6, N'Sườn nướng', 3, N'kg', 1), -- Sườn nướng cho Cơm tấm
(7, N'Gà', 4, N'kg', 1), -- Gà cho Xôi gà
(8, N'Trứng', 20, N'cái', 1), -- Trứng cho Bánh mì chảo
(9, N'Rau sống', 2, N'kg', 1), -- Rau sống cho nhiều món
(10, N'Hủ tiếu', 5, N'kg', 1), -- Hủ tiếu cho Hủ tiếu
(11, N'Chả cua', 3, N'kg', 1), -- Chả cua cho Súp cua
(12, N'Nước mắm', 1, N'lít', 1), -- Nước mắm cho các món
(13, N'Gia vị', 1, N'kg', 1), -- Gia vị cho các món
(14, N'Bánh mì', 10, N'cái', 1), -- Bánh mì cho Bánh mì thịt
(15, N'Rau củ', 5, N'kg', 1), -- Rau củ cho các món
(16, N'Phô mai', 2, N'kg', 1), -- Phô mai cho Pizza
(17, N'Xúc xích', 3, N'kg', 1), -- Xúc xích cho Pizza
(18, N'Nguyên liệu chiên', 1, N'kg', 1), -- Nguyên liệu chiên cho Hamburgers
(19, N'Vỏ bánh pizza', 10, N'cái', 1), -- Vỏ bánh pizza
(20, N'Siro', 0.5, N'lít', 1), -- Siro cho đồ uống
(21, N'Kem', 2, N'kg', 1), -- Kem cho món tráng miệng
(22, N'Sữa đặc', 1, N'lít', 1), -- Sữa đặc cho các món tráng miệng
(23, N'Chè', 1, N'kg', 1), -- Chè cho món tráng miệng
(24, N'Nước ép trái cây', 3, N'lít', 1), -- Nước ép trái cây cho món uống
(25, N'Đường', 2, N'kg', 1), -- Đường cho các món ngọt
(26, N'Táo', 5, N'kg', 1), -- Táo cho món tráng miệng
(27, N'Chuối', 5, N'kg', 1), -- Chuối cho món tráng miệng
(28, N'Vani', 0.2, N'kg', 1), -- Vani cho món tráng miệng
(29, N'Bột mì', 4, N'kg', 1), -- Bột mì cho các món bánh
(30, N'Bột ngô', 2, N'kg', 1); -- Bột ngô cho món ăn


INSERT INTO Customer (id, tenKhachHang, diaChi, email, soDienThoai, soTienTieu, idAccount)
VALUES (1, N'Trần Thị B', N'Hồ Chí Minh', 'ttb@example.com', '0123456789', '500000', 2);

INSERT INTO Promotion (id, ten, idKM, phanTram, ngayBD, ngayKT, trangThai, code, noidung)
VALUES (1, N'Khuyến mãi hè', 'KM001', 10, '2023-06-01', '2023-08-31', 1, 'SUMMER10', N'Giảm 10% cho đơn hàng từ 200k');

INSERT INTO Cart (id, idKM)
VALUES (1, 1);

INSERT INTO Payment (id, phuongThucThanhToan, mota, trangThai)
VALUES (1, N'Tiền mặt', N'Thanh toán bằng tiền mặt', N'Hoạt động');

-- Thêm vào bảng Order
INSERT INTO [Order] (id, idKhachHang, date, idFood, trangThai, tongSoLuong, idCart, idDelivery)
VALUES (1, 1, '2023-06-01 12:00:00', 1, N'Đã đặt hàng', 1, 1, 1);

-- Thêm vào bảng OrderInfo
INSERT INTO OrderInfo (id, idUser, idMonAn, mota, soLuongHangMon)
VALUES (1, 1, 1, N'Món ăn yêu thích', 1); -- Cập nhật mô tả món ăn

-- Thêm vào bảng Bill
INSERT INTO Bill (id, ngayCheckIn, ngayCheckOut, trangThai, idKhachHang, idNhanVien, idKM, tongTienTruoc, tongTienSau, idPhuongThuc, idOrderInfo)
VALUES (1, '2023-06-01 12:00:00', '2023-06-01 12:30:00', 1, 1, 1, 1, 50000, 45000, 1, 1);

INSERT INTO InfoFood (id, songuyenlieucan, idFood, mota, idNguyenLieu)
VALUES 
-- Món ăn
(1, 5, 1, N'Phở bò với nước dùng đậm đà, bánh phở mềm mịn', 1),
(2, 6, 2, N'Bún bò với thịt bò và nước dùng thơm ngon', 2),
(3, 4, 3, N'Bún riêu với cua đồng và nước dùng chua thanh', 3),
(4, 5, 4, N'Bún thịt nướng kèm rau sống và nước mắm', 4),
(5, 4, 5, N'Cơm tấm với sườn nướng và trứng ốp la', 5),
(6, 5, 6, N'Xôi gà hấp dẫn, ăn kèm với nước tương', 6),
(7, 4, 7, N'Bánh mì chảo với trứng và thịt đầy đặn', 7),
(8, 5, 8, N'Hủ tiếu với thịt và nước dùng ngọt thanh', 8),
(9, 4, 9, N'Bánh mì thịt với nhân đầy đặn và rau sống', 9),
(10, 3, 10, N'Súp cua thơm ngon với ngò rí và chả cua', 10),

-- Món tráng miệng
(11, 4, 11, N'Bánh socola mềm mịn', 11),
(12, 3, 12, N'Tart trái cây tươi mát', 12),
(13, 3, 13, N'Bánh brownie ngọt ngào', 13),
(14, 5, 14, N'Tiramisu lớp kem béo ngậy', 14),
(15, 2, 15, N'Sorbet mát lạnh, giải khát', 15),
(16, 3, 16, N'Panna Cotta kem béo, thơm ngon', 16),
(17, 2, 17, N'Cupcake trang trí bắt mắt', 17),
(18, 4, 18, N'Macarons ngọt ngào, thơm phức', 18),
(19, 3, 19, N'Sinh tố trái cây thơm ngon', 19),
(20, 2, 20, N'Kem lạnh với hương vị đa dạng', 20),

-- Món gà
(21, 4, 21, N'Gà rán giòn rụm', 21),
(22, 5, 22, N'Salad gà tươi ngon, bổ dưỡng', 22),
(23, 6, 23, N'Gà viên chiên giòn', 23),
(24, 5, 24, N'Gà xào chua ngọt', 24),
(25, 4, 25, N'Gà teriyaki với sốt thơm', 25),
(26, 5, 26, N'Mì gà sốt Alfredo béo ngậy', 26),
(27, 4, 27, N'Pizza gà BBQ', 27),
(28, 5, 28, N'Wrap gà và rau xanh tươi', 28),
(29, 4, 29, N'Gà rán kiểu miền Nam', 29),
(30, 5, 30, N'Gà parmesan thơm ngon', 30),

-- Pizza
(31, 4, 31, N'Pizza pepperoni với phô mai', 1),
(32, 3, 32, N'Pizza Margherita tươi ngon', 2),
(33, 5, 33, N'Pizza gà BBQ thơm phức', 3),
(34, 4, 34, N'Pizza nấm tươi', 4),
(35, 5, 35, N'Pizza chay với rau củ', 5),
(36, 5, 36, N'Pizza đặc biệt với nhiều topping', 6),
(37, 5, 37, N'Pizza với đế nhồi phô mai', 7),
(38, 4, 38, N'Pizza Hawaii ngọt ngào', 8),
(39, 5, 39, N'Pizza gà cay với sốt', 9),
(40, 4, 40, N'Pizza bốn loại phô mai', 10),

-- Hamburger
(41, 5, 41, N'Hamburger phô mai thơm ngon', 11),
(42, 4, 42, N'Burger chay với rau củ', 12),
(43, 5, 43, N'Burger thịt xông khói BBQ', 13),
(44, 4, 44, N'Burger nấm với phô mai', 14),
(45, 5, 45, N'Hamburger đôi với phô mai', 15),
(46, 4, 46, N'Burger cay Sriracha', 16),
(47, 5, 47, N'Burger sáng tạo với trứng', 17),
(48, 4, 48, N'Burger jalapeño cay', 18),
(49, 5, 49, N'Burger cá thơm ngon', 19),
(50, 4, 50, N'Burger heo kéo đầy hương vị', 20),

-- Đồ uống
(51, 2, 51, N'Cà phê rang xay tươi', 21),
(52, 1, 52, N'Trái cây tươi nghiền', 22),
(53, 1, 53, N'Nước ngọt lạnh', 23),
(54, 1, 54, N'Nước lọc tinh khiết', 24),
(55, 2, 55, N'Sinh tố trái cây mát lạnh', 25),
(56, 3, 56, N'Sinh tố rau củ bổ dưỡng', 26),
(57, 2, 57, N'Sinh tố ngũ cốc giàu dinh dưỡng', 27),
(58, 1, 58, N'Nước khoáng có ga', 28),
(59, 1, 59, N'Nước trái cây tự nhiên', 29),
(60, 3, 60, N'Cocktail với hương vị độc đáo', 30);

-- Thêm vào bảng Notifications
INSERT INTO Notifications (id, idCustomer, idOrder, name, noiDung, date, idBill, idBillsupplies)
VALUES (1, 1, 1, N'Đơn hàng mới', N'Bạn có một đơn hàng mới', GETDATE(), 1, 1);

-- Thêm vào bảng Latest
INSERT INTO Latest (id, idBill, idBillSupplies, date, noiDung, idCustomer, idOrderSupplies)
VALUES (1, 1, 1, GETDATE(), N'Đơn hàng mới được tạo', 1, 1);

INSERT INTO OrderSuppliesInfo (idordersupplies, idsupplies)
VALUES (1, 1);

INSERT INTO RoleFunction (RoleId, FunctionId) 
VALUES 
(1, 1), 
(1, 2), 
(1, 3),
(1, 4),
(1, 5),
(1, 6),
(1, 7),
(1, 8),
(1, 9),
(1, 10),
(1, 11),
(1, 12),
(1, 13),
(1, 14),
(1, 15);

-- Thêm dữ liệu vào bảng CartItems
INSERT INTO CartItems (idCart, idFood, soLuong) VALUES 
(1, 1, 2),  -- Giỏ hàng 1 có món ăn 1 với số lượng 2
(1, 2, 1),  -- Giỏ hàng 1 có món ăn 2 với số lượng 1
(1, 3, 3);  -- Giỏ hàng 1 có món ăn 3 với số lượng 3