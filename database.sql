use master
go
create database QLCuaHangMayTinh
go
use QLCuaHangMayTinh
go
create table TaiKhoan(
TenTK varchar(50) primary key,
MatKhau varchar(20) not null,
HoTen nvarchar(50),
SDT varchar(10),
DiaChi nvarchar(100),
IsActive bit default 1
)
go
create table LoaiSP(
Id int identity primary key,
TenLoai nvarchar(100),
HinhAnh nvarchar(max),
IsActive bit default 1
)
go
create table HangSX(
Id int identity primary key,
TenHang nvarchar(100),
HinhAnh nvarchar(max),
IsActive bit default 1
)
go
create table SanPham(
Id int identity primary key,
TenSP nvarchar(100),
HinhAnh nvarchar(max),
MoTa nvarchar(max),
ChiTiet nvarchar(max),
GiaBan int,
BanChay bit default 0,
SpMoi bit default 1,
LoaiSP int not null constraint fk_loai_sp foreign key(LoaiSP) references LoaiSP(Id),
HangSX int not null constraint fk_hang_sx foreign key(HangSX) references HangSX(Id),
IsActive bit default 1
)
go
create table HinhAnhSP(
Id int identity primary key,
SanPham int not null constraint fk_sp foreign key(SanPham) references SanPham(Id),
HinhAnh nvarchar(max),
IsActive bit default 1
)

go
create table HoaDon(
Id int identity primary key,
TenTK varchar(50) not null constraint fk_tentk foreign key(TenTK) references TaiKhoan(TenTK),
NgayMua date,
DiaChi nvarchar(100),
SDT varchar(20)
)
go
create table CTHoaDon(
Id int identity primary key,
IdHoaDon int not null constraint fk_hoadon foreign key(IdHoaDon) references HoaDon(Id),
SanPham int null constraint fk_sp_hd foreign key(SanPham) references SanPham(Id),
Gia int,
SoLuong int
)