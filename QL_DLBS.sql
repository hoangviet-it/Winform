/*
Created		3/10/2022
Modified		3/28/2022
Project		
Model			
Company		
Author		
Version		
Database		MS SQL 2005 
*/
CREATE DATABASE QL_DLS
go
USE [QL_DLS]
GO

Create table [NHANVIEN]
(
	[MANV] Char(20) NOT NULL,
	[HO] Nvarchar(50) NULL,
	[TEN] Nvarchar(30) NULL,
	[GIOITINH] Bit NULL,
	[SDT] Char(10) NULL,
	[MUCLUONG] Float NULL,
	[MABOPHAN] Char(20) NOT NULL,
Primary Key ([MANV])
) 
go

Create table [BOPHAN]
(
	[MABOPHAN] Char(20) NOT NULL,
	[TENBOPHAN] Nvarchar(50) NULL,
Primary Key ([MABOPHAN])
) 
go

Create table [BAOCAOKHO]
(
	[NGAYLAP] Datetime NOT NULL,
	[GHICHU] Nvarchar(100) NULL,
	[MANV] Char(20) NOT NULL,
	[MABC] Char(5) NOT NULL,
Primary Key ([MABC])
) 
go

Create table [NHACUNGCAP]
(
	[MANCC] Char(20) NOT NULL,
	[TENNCC] Nvarchar(60) NULL,
	[SDT] Char(10) NULL,
	[DIACHI] Nvarchar(50) NULL,
Primary Key ([MANCC])
) 
go

Create table [THELOAI]
(
	[MATHELOAI] Char(20) NOT NULL,
	[TENTHELOAI] Nvarchar(40) NULL,
Primary Key ([MATHELOAI])
) 
go

Create table [DAUSACH]
(
	[MASACH] Char(20) NOT NULL,
	[TENSACH] Nvarchar(60) NULL,
	[SOLUONGTON] Integer NULL,
	[MATHELOAI] Char(20) NOT NULL,
	[MATACGIA] Char(20) NOT NULL,
	[MANCC] Char(20) NOT NULL,
	[DONGIA] Integer NULL,
Primary Key ([MASACH])
) 
go

Create table [TACGIA]
(
	[MATACGIA] Char(20) NOT NULL,
	[HO] Nvarchar(50) NULL,
	[TEN] Nvarchar(30) NULL,
	[BUTDANH] Nvarchar(60) NULL,
Primary Key ([MATACGIA])
) 
go

Create table [CHITIETDANHSACH]
(
	[MASACH] Char(20) NOT NULL,
	[MABC] Char(5) NOT NULL,
Primary Key ([MASACH],[MABC])
) 
go

Create table [PHIEUGIAOHANGNCC]
(
	[MAPHIEUGIAO] Char(20) NOT NULL,
	[NGAYGIAO] Datetime NULL,
	[MAPHIEUNHAP] Char(20) NOT NULL,
	[MASACH] Char(20) NOT NULL,
Primary Key ([MAPHIEUGIAO])
) 
go

Create table [PHIEUNHAPHANG]
(
	[MAPHIEUNHAP] Char(20) NOT NULL,
	[GHICHU] Nvarchar(100) NULL,
	[NGAYLAP] Datetime NULL,
	[MANV] Char(20) NOT NULL,
	[MANCC] Char(20) NOT NULL,
Primary Key ([MAPHIEUNHAP])
) 
go

Create table [PHIEUNHAPKHO]
(
	[MAPHIEUNHAPKHO] Char(20) NOT NULL,
	[NGAYLAP] Datetime NULL,
	[MANV] Char(20) NOT NULL,
	[MAPHIEUNHAP] Char(20) NOT NULL,
Primary Key ([MAPHIEUNHAPKHO])
) 
go

Create table [KHACHHANG]
(
	[MAKHACHHANG] Char(20) NOT NULL,
	[HO] Nvarchar(50) NULL,
	[TEN] Nvarchar(30) NULL,
	[SDT] Char(10) NULL,
	[DIACHI] Nvarchar(60) NULL,
Primary Key ([MAKHACHHANG])
) 
go

Create table [CHITIETHOADON]
(
	[MASACH] Char(20) NOT NULL,
	[SOLUONGDAT] Integer NULL,
	[DONGIA] Float NULL,
	[MAHOADON] Char(20) NOT NULL,
Primary Key ([MASACH],[MAHOADON])
) 
go

Create table [HOADONBANHANG]
(
	[MAHOADON] Char(20) NOT NULL,
	[TONGTIEN] Float NULL,
	[NGAYLAP] Datetime NULL,
	[GHICHU] Nvarchar(20) NULL,
	[MANV] Char(20) NOT NULL,
	[MAKHACHHANG] Char(20) NOT NULL,
Primary Key ([MAHOADON])
) 
go

Create table [PHIEUGIAOHANG]
(
	[MAPHIEUGIAO] Char(20) NOT NULL,
	[NGAYGIAO] Datetime NULL,
	[GHICHU] Nvarchar(100) NULL,
	[TINHTRANG] Nvarchar(40) NULL,
	[MATAIXE] Char(20) NOT NULL,
	[MAHOADON] Char(20) NOT NULL,
	[MANV] Char(20) NOT NULL,
Primary Key ([MAPHIEUGIAO])
) 
go

Create table [THEVIP]
(
	[MATHE] Char(20) NOT NULL,
	[NGAYLAP] Datetime NULL,
	[NGAYHETHAN] Datetime NULL,
	[MAKHACHHANG] Char(20) NOT NULL,
	[MANV] Char(20) NOT NULL,
Primary Key ([MATHE])
) 
go

Create table [THONGKEDOANHTHU]
(
	[MABAOCAO] Char(20) NOT NULL,
	[NGAYLAP] Datetime NULL,
	[TONGDOANHTHU] Float NULL,
	[MANV] Char(20) NOT NULL,
Primary Key ([MABAOCAO])
) 
go

Create table [PHIEUHUYDONDAT]
(
	[MAPHIEUHUY] Char(20) NOT NULL,
	[NGAYLAP] Datetime NULL,
	[LYDOHUY] Nvarchar(100) NULL,
	[MAHOADON] Char(20) NOT NULL,
Primary Key ([MAPHIEUHUY])
) 
go

Create table [NHANVIENGIAOHANG]
(
	[MATAIXE] Char(20) NOT NULL,
	[HO] Nvarchar(50) NULL,
	[TEN] Nvarchar(30) NULL,
	[SDT] Char(10) NULL,
	[DIACHI] Nvarchar(60) NULL,
	[MABOPHAN] Char(20) NOT NULL,
	[LUONG] Integer NULL,
	[HINH] Varchar(100) NOT NULL,
Primary Key ([MATAIXE])
) 
go

Create table [THONGKEVANCHUYEN]
(
	[MABAOCAO] Char(20) NOT NULL,
	[NGAYLAP] Datetime NULL,
	[SODONVANCHUYEN] Integer NULL,
	[MANV] Char(20) NOT NULL,
Primary Key ([MABAOCAO])
) 
go

Create table [PHIEUXUATKHO]
(
	[MAPHIEUXUAT] Char(20) NOT NULL, UNIQUE ([MAPHIEUXUAT]),
	[NGAYXUAT] Datetime NULL,
	[MANV] Char(20) NOT NULL,
	[MAHOADON] Char(20) NOT NULL,
Primary Key ([MAPHIEUXUAT])
) 
go

Create table [CHITIETDONDATHANG]
(
	[MAPHIEUNHAP] Char(20) NOT NULL,
	[MASACH] Char(20) NOT NULL,
	[DONGIA] Integer NULL,
	[SOLUONG] Integer NULL,
Primary Key ([MAPHIEUNHAP],[MASACH])
) 
go


Alter table [BAOCAOKHO] add  foreign key([MANV]) references [NHANVIEN] ([MANV])  on update no action on delete no action 
go
Alter table [PHIEUNHAPHANG] add  foreign key([MANV]) references [NHANVIEN] ([MANV])  on update no action on delete no action 
go
Alter table [PHIEUNHAPKHO] add  foreign key([MANV]) references [NHANVIEN] ([MANV])  on update no action on delete no action 
go
Alter table [THONGKEDOANHTHU] add  foreign key([MANV]) references [NHANVIEN] ([MANV])  on update no action on delete no action 
go
Alter table [HOADONBANHANG] add  foreign key([MANV]) references [NHANVIEN] ([MANV])  on update no action on delete no action 
go
Alter table [THONGKEVANCHUYEN] add  foreign key([MANV]) references [NHANVIEN] ([MANV])  on update no action on delete no action 
go
Alter table [PHIEUGIAOHANG] add  foreign key([MANV]) references [NHANVIEN] ([MANV])  on update no action on delete no action 
go
Alter table [PHIEUXUATKHO] add  foreign key([MANV]) references [NHANVIEN] ([MANV])  on update no action on delete no action 
go
Alter table [THEVIP] add  foreign key([MANV]) references [NHANVIEN] ([MANV])  on update no action on delete no action 
go
Alter table [NHANVIEN] add  foreign key([MABOPHAN]) references [BOPHAN] ([MABOPHAN])  on update no action on delete no action 
go
Alter table [NHANVIENGIAOHANG] add  foreign key([MABOPHAN]) references [BOPHAN] ([MABOPHAN])  on update no action on delete no action 
go
Alter table [CHITIETDANHSACH] add  foreign key([MABC]) references [BAOCAOKHO] ([MABC])  on update no action on delete no action 
go
Alter table [DAUSACH] add  foreign key([MANCC]) references [NHACUNGCAP] ([MANCC])  on update no action on delete no action 
go
Alter table [PHIEUNHAPHANG] add  foreign key([MANCC]) references [NHACUNGCAP] ([MANCC])  on update no action on delete no action 
go
Alter table [DAUSACH] add  foreign key([MATHELOAI]) references [THELOAI] ([MATHELOAI])  on update no action on delete no action 
go
Alter table [CHITIETDANHSACH] add  foreign key([MASACH]) references [DAUSACH] ([MASACH])  on update no action on delete no action 
go
Alter table [CHITIETHOADON] add  foreign key([MASACH]) references [DAUSACH] ([MASACH])  on update no action on delete no action 
go
Alter table [CHITIETDONDATHANG] add  foreign key([MASACH]) references [DAUSACH] ([MASACH])  on update no action on delete no action 
go
Alter table [DAUSACH] add  foreign key([MATACGIA]) references [TACGIA] ([MATACGIA])  on update no action on delete no action 
go
Alter table [PHIEUNHAPKHO] add  foreign key([MAPHIEUNHAP]) references [PHIEUNHAPHANG] ([MAPHIEUNHAP])  on update no action on delete no action 
go
Alter table [CHITIETDONDATHANG] add  foreign key([MAPHIEUNHAP]) references [PHIEUNHAPHANG] ([MAPHIEUNHAP])  on update no action on delete no action 
go
Alter table [THEVIP] add  foreign key([MAKHACHHANG]) references [KHACHHANG] ([MAKHACHHANG])  on update no action on delete no action 
go
Alter table [HOADONBANHANG] add  foreign key([MAKHACHHANG]) references [KHACHHANG] ([MAKHACHHANG])  on update no action on delete no action 
go
Alter table [PHIEUGIAOHANG] add  foreign key([MAHOADON]) references [HOADONBANHANG] ([MAHOADON])  on update no action on delete no action 
go
Alter table [PHIEUHUYDONDAT] add  foreign key([MAHOADON]) references [HOADONBANHANG] ([MAHOADON])  on update no action on delete no action 
go
Alter table [CHITIETHOADON] add  foreign key([MAHOADON]) references [HOADONBANHANG] ([MAHOADON])  on update no action on delete no action 
go
Alter table [PHIEUXUATKHO] add  foreign key([MAHOADON]) references [HOADONBANHANG] ([MAHOADON])  on update no action on delete no action 
go
Alter table [PHIEUGIAOHANG] add  foreign key([MATAIXE]) references [NHANVIENGIAOHANG] ([MATAIXE])  on update no action on delete no action 
go
Alter table [PHIEUGIAOHANGNCC] add  foreign key([MAPHIEUNHAP],[MASACH]) references [CHITIETDONDATHANG] ([MAPHIEUNHAP],[MASACH])  on update no action on delete no action 
go


Set quoted_identifier on
go


Set quoted_identifier off
go


