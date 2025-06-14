create DATABASE RapChieuPhim;
go
USE RapChieuPhim;
go
set dateformat dmy
go

-- bảng lưu thông tin các rạp chiếu phim
create table Rap (
    idRap varchar(30) primary key ,
    TenRap nvarchar(255) not null,
    DiaChi varchar(255) not null,
    SoDienThoai varchar(20)
)
go
-- bảng phòng chiếu thuộc mỗi rạp
create table PhongChieu(
    idPhong varchar(30) primary key ,
    idRap varchar(30),
    TenPhong nvarchar(50),
    SoLuongGhe int not null,
    foreign key (idRap) references rap(idRap)
)
go

create table TheLoai(
	idTheLoai varchar(30) primary key,
	TenTheLoai nvarchar(100)
)
go
-- bảng phim
create table Phim (
    idPhim varchar(30) primary key ,
    TenPhim nvarchar(255) not null,
    idTheLoai varchar(30),
    ThoiLuong int not null,  
    NgayKhoiChieu date,
    DoTuoiPhuHop varchar(10),  -- độ tuổi phù hợp (pg-13, r, g...)
	foreign key (idTheLoai) references TheLoai(idTheLoai),
)
go
-- bảng lịch chiếu
create table LichChieu (
    idLich varchar(30) primary key ,
    idPhim varchar(30),
    idPhong varchar(30),
    NgayChieu date not null,
    GioChieu date not null,
    GiaVe int not null,
    foreign key (idPhim) references Phim(idPhim),
    foreign key (idPhong) references PhongChieu(idPhong)
)
go
-- bảng khách hàng
create table KhachHang (
    idKhach varchar(30) primary key ,
    Ten nvarchar(255) not null,
    Email varchar(255) unique not null,
    SoDienThoai varchar(20),
    NgayDangKy date
)
go
-- bảng đặt vé
create table DatVe (
    idDatVe varchar(30) primary key ,
    idKhach varchar(30),
    idLich varchar(30),
    GheNgoi varchar(50),  -- lưu số ghế đặt, ví dụ: "a1, a2"
    ThoiGianDat date,
    TongTien int not null,
    foreign key (idKhach) references KhachHang(idKhach),
    foreign key (idLich) references LichChieu(idLich)
)
go
-- bảng vé xem phim
create table VeXemPhim (
    idVe varchar(30) primary key ,
    idDatVe varchar(30),
    Ghe varchar(10),
    Gia int,
    foreign key (idDatVe) references DatVe(idDatVe)
)
go
-- bảng nhân viên
create table NhanVien (
    idNhanVien varchar(30) primary key ,
    idRap varchar(30),
    Ten nvarchar(255) not null,
    ViTri nvarchar(100),
    Luong int,
    NgayVaoLam date,
    foreign key (idRap) references Rap(idRap)
)
go
-- bảng đơn hàng thức ăn
create table DonHangDoAn (
    idDonHang varchar(30) primary key ,
    idKhach varchar(30),
    DanhSachMon text,  -- danh sách đồ ăn đã mua (json hoặc dạng text)
    TongTien int not null,
    ThoiGianDat date,
    foreign key (idKhach) references KhachHang(idKhach)
)
go
--Khuyen Mai--
create table KhuyenMai (
    idKhuyenMai varchar(30) primary key ,
    TenKhuyenMai nvarchar(50) unique not null,
    MoTa text,
    PhanTramGiam int check (PhanTramGiam between 1 and 100),  -- giảm giá theo %
    NgayBatDau date not null,
    NgayKetThuc date not null
)
go
--Ap Dung--
create table ApDungKhuyenMaiVe (
    idApDung varchar(30) primary key ,
    idDatVe varchar(30),
    idKhuyenMai varchar(30),
    SoTienGiam int not null,
    foreign key (idDatVe) references DatVe(idDatVe),
    foreign key (idKhuyenMai) references KhuyenMai(idKhuyenMai)
)
go
--Khuyen mai do an--
create table ApDungKhuyenMaiDoAn (
    idApDungDoAn varchar(30) primary key ,
    idDonHang varchar(30),
    idKhuyenMai varchar(30),
    SoTienGiam int not null,
    foreign key (idDonHang) references DonHangDoAn(idDonHang),
    foreign key (idKhuyenMai) references KhuyenMai(idKhuyenMai)
)
go
create table TaiKhoanNhanVien (
    idTaiKhoan varchar(30) primary key ,
    idNhanVien varchar(30),
    TenDangNhap nvarchar(50) unique not null,
    MatKhau varchar(255) not null,  -- mật khẩu nên được mã hóa
    --QuyenHan enum('Quanli', 'BanVe', 'DichVu') not null,
    foreign key (idNhanVien) references NhanVien(idNhanVien)
)
go
create table DoiTacPhim (
    idDoiTac varchar(30) primary key ,
    TenCongTy nvarchar(255) not null,
    EmailDoiTac varchar(255) unique not null,
    SoDienThoaiDT varchar(20),
    DiaChidt varchar(255)
)
go
create table PhimDoiTac (
    idPhim varchar(30),
    idDoiTac varchar(30),
    NgayKiHopDong date not null,
    primary key (idPhim, idDoiTac),
    foreign key (idPhim) references Phim(idPhim),
    foreign key (idDoiTac) references DoiTacPhim(idDoiTac)
)
go


create table ComboMonAn(
	idMonAn varchar(30) primary key ,
	CacMonAn nvarchar(200),
	GiaTien int
)

-- 1. Dữ liệu cho bảng Rap --
INSERT INTO Rap (idRap, TenRap, DiaChi, SoDienThoai) 
VALUES ('R1', N'BHD STAR Lê Văn Việt', N'Tầng 4, Vincom Plaza Lê Văn Việt, 50 Lê Văn Việt, P.Hiệp Phú, Quận 9, TP.HCM', '011564215'),
('R2', N'BHD STAR Long Khánh', N'430 Hồ Thị Hương, Phường Xuân An, Thành Phố Long Khánh, Tỉnh Đồng Nai', '09129234567'),
('R3', N'BHD STAR Thảo Điền', N'Tầng 5, Vincom Mega Mall Thảo Điền, 159 Xa Lộ Hà Nội, P.Thảo Điền, TP.Thủ Đức, TPHCM', '012564865454');
go

-- 2. Dữ liệu cho bảng PhongChieu --
INSERT INTO PhongChieu (idPhong, idRap, TenPhong, SoLuongGhe) 
VALUES ('PC1', 'R1', N'Phòng 1', 100),
('PC2', 'R1', N'Phòng 2', 120),
('PC3', 'R2', N'Phòng 3', 90),
('PC4', 'R2', N'Phòng 4', 80),
('PC5', 'R3', N'Phòng 5', 110),
('PC6', 'R3', N'Phòng 6', 120);
go

-- 3. Dữ liệu cho bảng TheLoai --
INSERT INTO TheLoai(idTheLoai, TenTheLoai)
VALUES
('HD', N'Hành động'),
('TC', N'Tình cảm'),
('HH', N'Hoạt hình'),
('TL', N'Tâm lý'),
('H', N'Hài'),
('KD', N'Kinh Dị');


-- 4. Dữ liệu cho bảng Phim --
INSERT INTO Phim (idPhim, TenPhim, idTheLoai, ThoiLuong, NgayKhoiChieu, DoTuoiPhuHop) 
VALUES ('P1', N'Avengers: Endgame', 'HD', 181, '2023-12-01', 'PG-13'),
('P2', N'Mắt Biếc', 'TC', 120, '2024-01-15', 'PG'),
('P3', N'The Batman', 'HD', 176, '2023-03-01', 'PG-13'),
('P4', N'Your Name', 'HD', 112, '2023-05-15', 'PG'),
('P5', N'Parasite', 'TL', 132, '2023-09-20', 'R'),
('P6', N'Top Gun: Maverick', 'HD', 131, '2023-07-04', 'PG-13'),
('P7', N'Spider-Man: No Way Home', 'HD', 148, '2023-12-17', 'PG-13'),
('P8', N'Conan: Tàu Ngầm Đen', 'HD', 110, '2024-03-25', 'PG'),
('P9', N'Suzume', 'HH', 122, '2024-02-01', 'PG'),
('P10', N'Nhà Bà Nữ', 'H', 98, '2024-01-10', 'PG'),
('P11', N'The Conjuring', 'KD', 98, '2024-03-10', 'PG-16'),
('P12', N'The Conjuring 2', 'KD', 98, '2024-03-10', 'PG-16')

go

-- 5. Dữ liệu cho bảng LichChieu --
INSERT INTO LichChieu (idLich, idPhim, idPhong, NgayChieu, GioChieu, GiaVe) 
VALUES ('L1', 'P1', 'PC1', '2024-04-10', '18:00:00', 90000),
('L2', 'P2', 'PC2', '2024-04-10', '10:30:00', 80000),
('L3', 'P3', 'PC3', '2024-04-11', '20:30:00', 75000);
go

-- 6. Dữ liệu cho bảng KhachHang --
INSERT INTO KhachHang (idKhach, Ten, Email, SoDienThoai, NgayDangKy) 
VALUES
('KH1', N'Nguyễn Văn A', 'vana@gmail.com', '0912345678', '2024-04-01'),
('KH2', N'Trần Thị B', 'thib@gmail.com', '0923456789', '2024-04-02'),
('KH3', N'Phạm Minh Tuấn', 'tuanpm@gmail.com', '0933123456', '2024-04-05'),
('KH4', N'Phạm Huy Minh Quang', 'quangpham@gmail.com', '0987456123', '2024-04-06'),
('KH5', N'Trịnh Quốc D', 'datquoc@gmail.com', '0967123874', '2024-04-06'),
('KH6', N'Châu Trần Minh Trí', 'minhtri@gmail.com', '0912233445', '2024-04-07'),
('KH7', N'Nguyễn Thanh Tùng', 'thanhtung@gmail.com', '0908765432', '2024-04-07'),
('KH8', N'Ngô Minh Thuận', 'hanhngo@gmail.com', '0977567890', '2024-04-08'),
('KH9', N'Võ Đức Huy', 'huyvo@gmail.com', '0934789651', '2024-04-08'),
('KH10', N'Hồ Phan Minh Đăng', 'dangho@gmail.com', '0945123789', '2024-04-08'),
('KH11', N'Tô Thanh Hà', 'hatho@gmail.com', '0967894321', '2024-04-08'),
('KH12', N'Đặng Văn Nam', 'namdang@gmail.com', '0978312465', '2024-04-08'),
('KH13', N'Trần Quang Hưng', 'hungtran@gmail.com', '0923678456', '2024-04-08'),
('KH14', N'Nguyễn Kim Oanh', 'oanhnk@gmail.com', '0912348765', '2024-04-08'),
('KH15', N'Lương Minh Khoa', 'khoaluong@gmail.com', '0909988776', '2024-04-08'),
('KH16', N'Phan Thị Thảo', 'thaophan@gmail.com', '0933467890', '2024-04-08'),
('KH17', N'Cao Văn Bình', 'binhcao@gmail.com', '0965456789', '2024-04-08'),
('KH18', N'Tống Ngọc Duy', 'duytong@gmail.com', '0977654321', '2024-04-08');
go

-- 7. Dữ liệu cho bảng DatVe --
INSERT INTO DatVe (idDatVe, idKhach, idLich, GheNgoi, ThoiGianDat, TongTien) 
VALUES ('DV1', 'KH1', 'L1', 'A1,A2', '2024-04-08', 70000),
('DV2', 'KH2', 'L2', 'B1', '2024-04-08', 70000);
go

-- 8. Dữ liệu cho bảng VeXemPhim --
INSERT INTO VeXemPhim (idVe, idDatVe, Ghe, Gia) 
VALUES ('V1', 'DV1', 'A1', 70000),
('V2', 'DV1', 'A2', 70000),
('V3', 'DV2', 'B1', 70000);
go


-- 9. Dữ liệu cho bảng NhanVien --
INSERT INTO NhanVien (idNhanVien, idRap, Ten, ViTri, Luong, NgayVaoLam) 
VALUES ('NV1', 'R1', N'Lê Văn Nhân', N'Quản lý', 15000000, '2022-01-22'),
('NV2', 'R1', N'Nguyễn Văn Nhàn', N'Nhân viên phụ bán vé', 12000000, '2022-11-21'),
('NV3', 'R2', N'Trúc Ly', N'Quản lý', 15000000, '2022-10-01'),
('NV4', 'R2', N'Hoàng Mỹ Linh', N'Nhân viên bán vé', 8000000, '2023-06-15'),
('NV5', 'R3', N'Khánh Hoàng', N'Quản lý', 15000000, '2021-9-01'),
('NV6', 'R3', N'Nguyễn Trí Tài', N'Nhân viên bán vé', 6000000, '2023-06-15');
go

-- 10. Dữ liệu cho bảng KhuyenMai --
INSERT INTO KhuyenMai (idKhuyenMai, TenKhuyenMai, MoTa, PhanTramGiam, NgayBatDau, NgayKetThuc) 
VALUES ('KM001', N'Giảm giá hè', N'Khuyến mãi mùa hè lên đến 20%', 20, '2025-06-01', '2025-06-30'),
('KM002', N'Ưu đãi đầu năm', N'Giảm 15% cho đơn hàng đầu tiên', 15, '2025-01-01', '2025-01-15'),
('KM003', N'Tháng sinh nhật', N'Giảm 30% mừng sinh nhật khách hàng', 30, '2025-07-10', '2025-07-20'),
('KM004', N'Mua 1 tặng 1', N'Áp dụng cho một số sản phẩm nhất định', 50, '2025-08-01', '2025-08-07'),
('KM005', N'Lễ hội mua sắm', N'Khuyến mãi lớn dịp lễ hội', 25, '2025-11-20', '2025-11-30');
go


INSERT INTO ComboMonAn(idMonAn, CacMonAn, GiaTien)
VALUES ('', '01 bắp nhỏ vị ngọt + 01 ly nước 22Oz', 77000)