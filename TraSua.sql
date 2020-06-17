--USE master
--alter database TraSua set single_user with rollback immediate
--drop database TraSua   

CREATE DATABASE TraSua
GO

USE TraSua
GO

--tables
CREATE TABLE [Admin](
	MaAdmin int PRIMARY KEY IDENTITY(1,1),
	TaiKhoan varchar(255) UNIQUE NOT NULL ,
	MatKhau nvarchar(255) NOT NULL ,
	TenAdmin nvarchar(255),
	NgayTao datetime DEFAULT getdate()
)
GO 

CREATE TABLE TheLoai(
	MaTL int PRIMARY KEY IDENTITY(1,1),
	TenTL nvarchar(255) NOT NULL 
)
GO

CREATE TABLE Size(
	MaSize int PRIMARY KEY IDENTITY(1,1),
	TenSize varchar(2) NOT NULL
)
GO

CREATE TABLE SanPham(
	MaSP int PRIMARY KEY IDENTITY(1,1),
	TenSP nvarchar(255) NOT NULL ,
	MoTaSP nvarchar(MAX),
	AnhSP nvarchar(255) DEFAULT N'',
	GiaSP DECIMAL(18, 0) NOT NULL,
	KhuyenMai DECIMAL(18, 0) DEFAULT 0,
	Rating INT CHECK (RATING >=0 AND RATING <= 5),
	UrlSP NVARCHAR(255),
	TrangThaiSP bit,
	NgayTao datetime DEFAULT getdate(),

	MaTL int REFERENCES dbo.TheLoai(MaTL) ON DELETE CASCADE
)
GO

CREATE TABLE CTSP(
	MaCTSP int PRIMARY KEY IDENTITY(1,1),
	
	MaSP int REFERENCES dbo.SanPham(MaSP) ON DELETE CASCADE,
	MaSize int REFERENCES dbo.Size(MaSize) ON DELETE CASCADE
)
GO

CREATE TABLE KhachHang(
	MaKH int PRIMARY KEY IDENTITY(1,1),
	TenKH nvarchar(255) NOT NULL ,
	TaiKhoan varchar(255) UNIQUE NOT NULL ,
	MatKhau varchar(255) NOT NULL ,
	Email varchar(255),
	SDT varchar(15),
	DiaChi nvarchar(255),
	NgayTao datetime DEFAULT getdate()
)
GO

CREATE TABLE TrangThai(
	MaTT int PRIMARY KEY,
	TenTT nvarchar(255),
	NgayTao datetime DEFAULT getdate()
)
GO

CREATE TABLE HoaDon(
	MaHD int PRIMARY KEY IDENTITY(1,1),
	NgayTao datetime DEFAULT getdate(),
	TenKH nvarchar(255),
	SDT varchar(15),
	DiaChi nvarchar(MAX),
	TongTien decimal(18, 2),

	MaKH int REFERENCES dbo.KhachHang(MaKH) ON DELETE CASCADE,
	MaTT int REFERENCES dbo.TrangThai(MaTT) ON DELETE CASCADE
)
GO

CREATE TABLE CTHoaDon(
	MaCTHD int PRIMARY KEY IDENTITY(1,1),
	SoLuong int,
	
	MaHD int REFERENCES dbo.HoaDon(MaHD) ON DELETE CASCADE,
	MaSP int REFERENCES dbo.SanPham(MaSP) ON DELETE CASCADE,
	MaSize int REFERENCES dbo.Size(MaSize) ON DELETE CASCADE
)
GO

--Data
INSERT INTO dbo.Size
VALUES
('M'), ('L'), ('S')
GO

INSERT INTO dbo.CTSP
VALUES
(1, 1),
(1, 2),
(1, 3),
(2, 2),
(2, 3)

INSERT INTO TrangThai
VALUES
    (1, N'Đang xử lý', GETDATE()),
    (2, N'Đang giao hàng', GETDATE()),
    (3, N'Đã giao hàng', GETDATE()),
    (4, N'Hàng có lỗi', GETDATE()),
    (5, N'Đã hủy', GETDATE())
GO

INSERT INTO dbo.TheLoai
VALUES
(N'Sữa tươi trân châu'), (N'Trà sữa trân châu'), (N'Bánh ngọt'), ('Kem tươi New Zealand')
GO

INSERT INTO dbo.SanPham
VALUES
(
    -- MaSP - int
    N'Sữa tươi trân châu Matcha', -- TenSP - nvarchar
    N'ngon bổ rẻ', -- MoTaSP - nvarchar
    N'https://congthucphache.com/wp-content/uploads/2020/01/e06ab4b897d66f8836c7.jpg', -- AnhSP - nvarchar
    40000, -- GiaSP - DECIMAL
    10000, -- KhuyenMai - DECIMAL
    5, -- Rating - INT
    N'sua-tuoi-tran-chau-matcha', -- UrlSP - NVARCHAR
    0, -- TrangThaiSP - bit
    '2020-06-16 21:36:49', -- NgayTao - datetime
    1 -- MaTL - int
),
(
	-- MaSP - int
    N'Trà sữa bạc hà', -- TenSP - nvarchar
    N'mát lạnh', -- MoTaSP - nvarchar
    N'https://images.foody.vn/res/g67/662390/s750x750/206fd4df-ad33-4e65-be37-a26f0e9fe9a9.jpg', -- AnhSP - nvarchar
    35000, -- GiaSP - DECIMAL
    10000, -- KhuyenMai - DECIMAL
    5, -- Rating - INT
    N'tra-sua-bac-ha', -- UrlSP - NVARCHAR
    0, -- TrangThaiSP - bit
    '2020-06-16 21:36:49', -- NgayTao - datetime
    2 -- MaTL - int
)
GO


