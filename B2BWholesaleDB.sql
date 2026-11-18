CREATE DATABASE B2BWholesaleDB;
GO
USE B2BWholesaleDB;
GO

-- 1. Bảng Agent (20 đại lý phân phối)
CREATE TABLE Agent (
    AgentID INT IDENTITY(1,1) PRIMARY KEY,
    AgentName NVARCHAR(150) NOT NULL,
    Address NVARCHAR(250) NOT NULL,
    Phone VARCHAR(20) NOT NULL
);

-- 2. Bảng Item (20 mặt hàng bán sỉ, ràng buộc StockQuantity >= 0 và Price > 0)
CREATE TABLE Item (
    ItemID INT IDENTITY(1,1) PRIMARY KEY,
    ItemName NVARCHAR(150) NOT NULL,
    Size NVARCHAR(50) NOT NULL,
    Price DECIMAL(18,2) NOT NULL CONSTRAINT CK_Item_Price CHECK (Price > 0),
    StockQuantity INT NOT NULL CONSTRAINT CK_Item_Stock CHECK (StockQuantity >= 0)
);

-- 3. Bảng Users (Quản trị viên và Tài khoản Đại lý)
CREATE TABLE Users (
    UserID INT IDENTITY(1,1) PRIMARY KEY,
    UserName VARCHAR(50) NOT NULL CONSTRAINT UQ_Users_UserName UNIQUE,
    email VARCHAR(100) NOT NULL CONSTRAINT UQ_Users_Email UNIQUE,
    password VARCHAR(255) NOT NULL,
    [lock] BIT NOT NULL CONSTRAINT DF_Users_Lock DEFAULT 0,
    Role VARCHAR(20) NOT NULL CONSTRAINT CK_Users_Role CHECK (Role IN ('Admin', 'Agent')),
    AgentID INT NULL CONSTRAINT FK_Users_Agent FOREIGN KEY REFERENCES Agent(AgentID) ON DELETE SET NULL
);

-- 4. Bảng Order (ON DELETE NO ACTION để không mất lịch sử đơn hàng khi xóa Agent)
CREATE TABLE [Order] (
    OrderID INT IDENTITY(1,1) PRIMARY KEY,
    OrderDate DATETIME NOT NULL CONSTRAINT DF_Order_Date DEFAULT GETDATE(),
    AgentID INT NOT NULL CONSTRAINT FK_Order_Agent FOREIGN KEY REFERENCES Agent(AgentID) ON DELETE NO ACTION,
    OrderStatus NVARCHAR(30) NOT NULL CONSTRAINT DF_Order_Status DEFAULT 'Pending'
        CONSTRAINT CK_Order_Status CHECK (OrderStatus IN ('Pending', 'Approved', 'Shipped', 'Cancelled'))
);

-- 5. Bảng OrderDetail (ON DELETE CASCADE theo Order, NO ACTION theo Item)
CREATE TABLE OrderDetail (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    OrderID INT NOT NULL CONSTRAINT FK_OrderDetail_Order FOREIGN KEY REFERENCES [Order](OrderID) ON DELETE CASCADE,
    ItemID INT NOT NULL CONSTRAINT FK_OrderDetail_Item FOREIGN KEY REFERENCES Item(ItemID) ON DELETE NO ACTION,
    Quantity INT NOT NULL CONSTRAINT CK_OrderDetail_Qty CHECK (Quantity > 0),
    UnitAmount DECIMAL(18,2) NOT NULL CONSTRAINT CK_OrderDetail_Price CHECK (UnitAmount >= 0)
);
GO

-- =============================================
-- SEED DATA: 20 DÒNG MỖI BẢNG
-- =============================================

-- Seed 20 Agents
INSERT INTO Agent (AgentName, Address, Phone) VALUES
(N'Công ty TNHH Phân Phối Minh Phát', N'12 Nguyễn Văn Linh, Quận 7, TP.HCM', '0901234501'),
(N'Đại lý Sỉ Hoàng Gia', N'45 Lê Lợi, Quận 1, TP.HCM', '0901234502'),
(N'Nhà Phân Phối Tân Tiến', N'88 Điện Biên Phủ, Quận Bình Thạnh, TP.HCM', '0901234503'),
(N'Công ty CP Thương Mại Nam Việt', N'102 Võ Văn Ngân, TP. Thủ Đức, TP.HCM', '0901234504'),
(N'Đại lý Bách Hóa Đại Phát', N'23 Trần Hưng Đạo, TP. Long Xuyên, An Giang', '0901234505'),
(N'Nhà Phân Phối Phú Quý', N'56 Nguyễn Huệ, TP. Cần Thơ', '0901234506'),
(N'Công ty TNHH Tiếp Vận Đông Á', N'19 Quốc Lộ 1A, Biên Hòa, Đồng Nai', '0901234507'),
(N'Đại lý Phân Phối Thanh Bình', N'77 Đại Lộ Bình Dương, Thủ Dầu Một, Bình Dương', '0901234508'),
(N'Công ty Thương Mại Vạn Phúc', N'310 Cộng Hòa, Quận Tân Bình, TP.HCM', '0901234509'),
(N'Đại lý Sỉ Ánh Dương', N'15 Lý Thường Kiệt, Quận 10, TP.HCM', '0901234510'),
(N'Nhà Phân Phối Thành Công', N'64 Quang Trung, Quận Gò Vấp, TP.HCM', '0901234511'),
(N'Công ty TNHH TMDV Hải Đăng', N'29 Lê Duẩn, TP. Đà Nẵng', '0901234512'),
(N'Đại lý Sỉ Phương Nam', N'91 Nguyễn Trãi, Quận 5, TP.HCM', '0901234513'),
(N'Nhà Phân Phối An Khang', N'120 Cách Mạng Tháng 8, Quận 3, TP.HCM', '0901234514'),
(N'Công ty CP Chuỗi Cung Ứng Việt', N'205 Khánh Hội, Quận 4, TP.HCM', '0901234515'),
(N'Đại lý Thương Mại Hồng Hà', N'33 Hùng Vương, TP. Mỹ Tho, Tiền Giang', '0901234516'),
(N'Nhà Phân Phối Sao Mai', N'72 Trần Phú, TP. Nha Trang, Khánh Hòa', '0901234517'),
(N'Công ty TNHH Phân Phối Bảo Ngọc', N'148 Phan Đăng Lưu, Quận Phú Nhuận, TP.HCM', '0901234518'),
(N'Đại lý Tổng Hợp Tín Nghĩa', N'50 Nguyễn Văn Cừ, TP. Rạch Giá, Kiên Giang', '0901234519'),
(N'Nhà Phân Phối Quốc Tế Mekong', N'18 Hai Bà Trưng, TP. Vĩnh Long', '0901234520');

-- Seed 20 Items (có cả mặt hàng tồn kho thấp <= 10 để test cảnh báo Low-Stock)
INSERT INTO Item (ItemName, Size, Price, StockQuantity) VALUES
(N'Thùng Nước Khoáng Lavie 500ml', N'Thùng 24 chai', 85000, 150),
(N'Thùng Nước Ngọt Coca-Cola 320ml', N'Thùng 24 lon', 195000, 200),
(N'Thùng Bia Tiger Crystal 330ml', N'Thùng 24 lon', 390000, 120),
(N'Thùng Sữa Tươi Vinamilk Ít Đường', N'Thùng 48 hộp 180ml', 365000, 90),
(N'Thùng Mì Hảo Hảo Tôm Chua Cay', N'Thùng 30 gói', 118000, 300),
(N'Dầu Ăn Tường An Cooking Oil', N'Thùng 4 can x 5L', 820000, 45),
(N'Nước Mắm Nam Ngư Đệ Nhị', N'Thùng 12 chai 900ml', 420000, 80),
(N'Gạo ST25 Ông Cua Chính Hiệu', N'Bao 25kg', 780000, 60),
(N'Đường Tinh Luyện Biên Hòa', N'Bao 50kg', 1150000, 8),   -- Low stock (< 10)
(N'Cà Phê Hòa Tan G7 3in1', N'Thùng 24 hộp', 1280000, 5), -- Low stock (< 10)
(N'Trà Ô Long Tea+ Plus 450ml', N'Thùng 24 chai', 185000, 110),
(N'Nước Tăng Lực RedBull Thái', N'Thùng 24 lon', 260000, 140),
(N'Bánh Chocopie Orion Hộp Lớn', N'Thùng 8 hộp 660g', 540000, 75),
(N'Hạt Nêm Knorr Thịt Thăn Xương Ống', N'Thùng 6 gói 1.8kg', 760000, 9), -- Low stock (< 10)
(N'Tương Ớt Chinsu Phở 2kg', N'Thùng 6 can 2kg', 480000, 95),
(N'Bột Giặt OMO Hệ Bọt Thông Minh', N'Thùng 4 túi 4.5kg', 690000, 55),
(N'Nước Rửa Chén Sunlight Chanh', N'Thùng 4 can 3.8kg', 410000, 70),
(N'Giấy Vệ Sinh Puri Cuộn Lớn', N'Kiện 10 lốc', 650000, 4), -- Low stock (< 10)
(N'Sữa Đặc Ngôi Sao Phương Nam', N'Thùng 48 hộp 380g', 890000, 85),
(N'Ngũ Cốc Dinh Dưỡng Nesvita', N'Thùng 20 bịch', 1120000, 65);

-- Seed 20 Users (3 Admin, 17 Agent; mật khẩu demo: "123456")
INSERT INTO Users (UserName, email, password, [lock], Role, AgentID) VALUES
('admin', 'admin@b2bwholesale.vn', '123456', 0, 'Admin', NULL),
('warehouse_mgr', 'warehouse@b2bwholesale.vn', '123456', 0, 'Admin', NULL),
('admin_locked', 'lockedadmin@b2bwholesale.vn', '123456', 1, 'Admin', NULL),
('agent1', 'minhphat@agent.vn', '123456', 0, 'Agent', 1),
('agent2', 'hoanggia@agent.vn', '123456', 0, 'Agent', 2),
('agent3', 'tantien@agent.vn', '123456', 0, 'Agent', 3),
('agent4', 'namviet@agent.vn', '123456', 0, 'Agent', 4),
('agent5', 'daiphat@agent.vn', '123456', 0, 'Agent', 5),
('agent6', 'phuquy@agent.vn', '123456', 0, 'Agent', 6),
('agent7', 'donga@agent.vn', '123456', 0, 'Agent', 7),
('agent8', 'thanhbinh@agent.vn', '123456', 0, 'Agent', 8),
('agent9', 'vanphuc@agent.vn', '123456', 0, 'Agent', 9),
('agent10', 'anhduong@agent.vn', '123456', 0, 'Agent', 10),
('agent11', 'thanhcong@agent.vn', '123456', 0, 'Agent', 11),
('agent12', 'haidang@agent.vn', '123456', 0, 'Agent', 12),
('agent13', 'phuongnam@agent.vn', '123456', 0, 'Agent', 13),
('agent14', 'ankhang@agent.vn', '123456', 0, 'Agent', 14),
('agent15', 'supplyvn@agent.vn', '123456', 0, 'Agent', 15),
('agent16', 'hongha@agent.vn', '123456', 0, 'Agent', 16),
('agent_locked', 'locked@agent.vn', '123456', 1, 'Agent', 17);

-- Seed 20 Orders
INSERT INTO [Order] (OrderDate, AgentID, OrderStatus) VALUES
(DATEADD(DAY, -25, GETDATE()), 1, 'Shipped'),
(DATEADD(DAY, -24, GETDATE()), 2, 'Shipped'),
(DATEADD(DAY, -22, GETDATE()), 3, 'Shipped'),
(DATEADD(DAY, -20, GETDATE()), 4, 'Shipped'),
(DATEADD(DAY, -18, GETDATE()), 5, 'Shipped'),
(DATEADD(DAY, -16, GETDATE()), 1, 'Shipped'),
(DATEADD(DAY, -15, GETDATE()), 6, 'Approved'),
(DATEADD(DAY, -14, GETDATE()), 7, 'Approved'),
(DATEADD(DAY, -12, GETDATE()), 8, 'Shipped'),
(DATEADD(DAY, -10, GETDATE()), 9, 'Pending'),
(DATEADD(DAY, -9, GETDATE()), 10, 'Pending'),
(DATEADD(DAY, -8, GETDATE()), 2, 'Shipped'),
(DATEADD(DAY, -7, GETDATE()), 11, 'Approved'),
(DATEADD(DAY, -6, GETDATE()), 12, 'Pending'),
(DATEADD(DAY, -5, GETDATE()), 3, 'Shipped'),
(DATEADD(DAY, -4, GETDATE()), 13, 'Cancelled'),
(DATEADD(DAY, -3, GETDATE()), 14, 'Pending'),
(DATEADD(DAY, -2, GETDATE()), 15, 'Approved'),
(DATEADD(DAY, -1, GETDATE()), 1, 'Pending'),
(GETDATE(), 5, 'Pending');

-- Seed 25 OrderDetails
INSERT INTO OrderDetail (OrderID, ItemID, Quantity, UnitAmount) VALUES
(1, 1, 20, 85000), (1, 2, 15, 195000),
(2, 3, 10, 390000), (2, 5, 30, 118000),
(3, 4, 12, 365000), (4, 6, 8, 820000),
(5, 8, 15, 780000), (5, 7, 10, 420000),
(6, 2, 25, 195000), (6, 5, 40, 118000),
(7, 11, 20, 185000), (8, 12, 18, 260000),
(9, 13, 10, 540000), (10, 15, 12, 480000),
(11, 16, 10, 690000), (12, 3, 20, 390000),
(13, 17, 15, 410000), (14, 19, 10, 890000),
(15, 5, 50, 118000), (16, 1, 10, 85000),
(17, 20, 8, 1120000), (18, 4, 20, 365000),
(19, 2, 10, 195000), (19, 3, 5, 390000),
(20, 8, 10, 780000);
GO