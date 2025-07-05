create DATABASE RapChieuPhim
go
USE RapChieuPhim
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
	foreign key (idTheLoai) references TheLoai(idTheLoai)
)
go
-- bảng lịch chiếu
create table LichChieu (
    idLich varchar(30) primary key ,
    idPhim varchar(30),
    idPhong varchar(30),
    NgayChieu date not null,
    GioChieu varchar(30) not null,
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
    NgayDangKy date,
	MatKhauKH varchar(255) not null
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
----option khác cảu đặt vé
CREATE TABLE BookVe (
    idBookVe varchar(30) primary key,
    idKhach varchar(30),
    idLich varchar(30),
    GheNgoi varchar(50),
    ThoiGianDat date,
    TongTien int not null,
	TrangThai NVARCHAR(50) DEFAULT N'Đang giữ chỗ',
    foreign key (idKhach) references KhachHang(idKhach),
    foreign key (idLich) references LichChieu(idLich)
);
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
    MoTa nvarchar(200),
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
go

CREATE TABLE DanhGiaPhim (
    idDanhGia VARCHAR(30) PRIMARY KEY,
    idKhach VARCHAR(30),
    idPhim VARCHAR(30),
    SoSao INT CHECK (SoSao BETWEEN 1 AND 10),
    BinhLuan NVARCHAR(500),
    NgayDanhGia DATE DEFAULT GETDATE(),
    FOREIGN KEY (idKhach) REFERENCES KhachHang(idKhach),
    FOREIGN KEY (idPhim) REFERENCES Phim(idPhim)
)
GO
CREATE TABLE MonAnvaThucUong (
    MaMon INT IDENTITY(1,1) PRIMARY KEY,
    TenMon NVARCHAR(100) NOT NULL,
    Loai NVARCHAR(50) NOT NULL,
    Gia DECIMAL(10, 2) NOT NULL,
    MoTa NVARCHAR(255),
    TrangThai BIT DEFAULT 1
);
go
-- 10. Dữ liệu cho bảng MonAnvaThucUong --
INSERT INTO MonAnvaThucUong (TenMon, Loai, Gia, MoTa, TrangThai) VALUES
(N'Coca-Cola', N'Thức uống', 15000, N'Nước ngọt có gas', 1),
(N'Bắp rang bơ', N'Món ăn', 25000, N'Bắp rang bơ ngọt', 1),
(N'Trà sữa', N'Thức uống', 20000, N'Trà sữa trân châu đường đen', 1);
go
-- Dữ liệu đánh giá phim
INSERT INTO DanhGiaPhim (idDanhGia, idKhach, idPhim, SoSao, BinhLuan, NgayDanhGia)
VALUES 
('DG001', 'KH01', 'P01', 9, N'Phim rất hay, kỹ xảo ấn tượng, đáng xem!', '2025-07-01'),
('DG002', 'KH02', 'P02', 8, N'Nội dung cảm động, diễn xuất tốt.', '2025-07-02'),
('DG003', 'KH03', 'P03', 7, N'Phim ổn, có vài đoạn hơi dài dòng.', '2025-07-02'),
('DG004', 'KH04', 'P06', 10, N'Bom tấn! Cảnh hành động đỉnh cao!', '2025-07-04'),
('DG005', 'KH05', 'P05', 8, N'Phim có chiều sâu và đáng suy ngẫm.', '2025-07-03'),
('DG006', 'KH06', 'P07', 9, N'Mãn nhãn, nội dung hấp dẫn!', '2025-07-04'),
('DG007', 'KH07', 'P04', 10, N'Âm nhạc và hình ảnh tuyệt vời, rất xúc động.', '2025-06-28'),
('DG008', 'KH08', 'P01', 8, N'Kết thúc hơi buồn nhưng rất hợp lý.', '2025-06-30'),
('DG009', 'KH09', 'P10', 6, N'Phim vui nhộn nhưng nội dung hơi nhạt.', '2025-07-04'),
('DG010', 'KH10', 'P03', 9, N'Phim có màu sắc riêng, rất nghệ thuật.', '2025-07-01');
go

-- 1. Dữ liệu cho bảng Rap --
INSERT INTO Rap (idRap, TenRap, DiaChi, SoDienThoai) 
VALUES ('R1', N'BHD STAR Lê Văn Việt', N'Tầng 4, Vincom Plaza Lê Văn Việt, 50 Lê Văn Việt, P.Hiệp Phú, Quận 9, TP.HCM', '011564215'),
('R2', N'BHD STAR Long Khánh', N'430 Hồ Thị Hương, Phường Xuân An, Thành Phố Long Khánh, Tỉnh Đồng Nai', '09129234567'),
('R3', N'BHD STAR Thảo Điền', N'Tầng 5, Vincom Mega Mall Thảo Điền, 159 Xa Lộ Hà Nội, P.Thảo Điền, TP.Thủ Đức, TPHCM', '012564865454')
go

-- 2. Dữ liệu cho bảng PhongChieu --
INSERT INTO PhongChieu (idPhong, idRap, TenPhong, SoLuongGhe) 
VALUES ('PC1', 'R1', N'Phòng 1', 80),
('PC2', 'R1', N'Phòng 2', 80),
('PC3', 'R2', N'Phòng 3', 80),
('PC4', 'R2', N'Phòng 4', 80),
('PC5', 'R3', N'Phòng 5', 80),
('PC6', 'R3', N'Phòng 6', 80)
go

-- 3. Dữ liệu cho bảng TheLoai --
INSERT INTO TheLoai(idTheLoai, TenTheLoai)
VALUES
('HD', N'Hành động'),
('TC', N'Tình cảm'),
('HH', N'Hoạt hình'),
('TL', N'Tâm lý'),
('H', N'Hài'),
('KD', N'Kinh Dị')


-- 4. Dữ liệu cho bảng Phim --
-- Phim đang chiếu (<= hôm nay)
INSERT INTO Phim (idPhim, TenPhim, idTheLoai, ThoiLuong, NgayKhoiChieu, DoTuoiPhuHop)
VALUES 
('P01', N'Avengers_Endgame', 'HD', 181, '2025-06-01', 'PG-13'),
('P02', N'Mắt Biếc', 'TC', 120, '2025-06-10', 'PG'),
('P03', N'The Batman', 'HD', 176, '2025-05-15', 'PG-13'),
('P04', N'Your Name', 'HD', 112, '2025-05-20', 'PG'),
('P05', N'Parasite', 'TL', 132, '2025-06-25', 'R'),

-- Phim ra mắt hôm nay (== 2025-07-04)
('P06', N'Top Gun_Maverick', 'HD', 131, '2025-07-04', 'PG-13'),
('P07', N'Spider-Man_No Way Home', 'HD', 148, '2025-07-04', 'PG-13'),

-- Phim sắp chiếu (> hôm nay)
('P08', N'Conan_Viên đạn đỏ', 'HD', 110, '2025-07-06', 'PG'),
('P09', N'Suzume', 'HH', 122, '2025-07-08', 'PG'),
('P10', N'Nhà Bà Nữ', 'H', 98, '2025-07-10', 'PG'),
('P11', N'The Conjuring', 'KD', 98, '2025-07-11', 'PG-16'),
('P12', N'The Conjuring 2', 'KD', 98, '2025-07-15', 'PG-16');
go

-- 5. Dữ liệu cho bảng LichChieu --
INSERT INTO LichChieu (idLich, idPhim, idPhong, NgayChieu, GioChieu, GiaVe) 
VALUES ('L1', 'P01', 'PC1', '2024-04-10', '9:00 - 11:00', 70000),
('L2', 'P02', 'PC2', '2024-04-10', '11:00 - 13:00', 70000),
('L3', 'P03', 'PC3', '2024-04-11', '20:00 - 22:00', 70000),
('L4', 'P04', 'PC3', '2024-04-11', '18:00 - 20:00', 70000),
('L5', 'P04', 'PC3', '2024-04-11', '15:00 - 17:00', 70000);
go

-- 6. Dữ liệu cho bảng KhachHang --
INSERT INTO KhachHang (idKhach, Ten, Email, SoDienThoai, NgayDangKy, MatKhauKH) 
VALUES
('KH01', N'Nguyễn Văn A', 'vana@gmail.com', '0912345678', '2024-04-01', '123456'),
('KH02', N'Trần Thị B', 'thib@gmail.com', '0923456789', '2024-04-02', '123456'),
('KH03', N'Phạm Minh Tuấn', 'tuanpm@gmail.com', '0933123456', '2024-04-05', '123456'),
('KH04', N'Phạm Huy Minh Quang', 'quangpham@gmail.com', '0987456123', '2024-04-06', '123456'),
('KH05', N'Trịnh Quốc D', 'datquoc@gmail.com', '0967123874', '2024-04-06', '123456'),
('KH06', N'Châu Trần Minh Trí', 'minhtri@gmail.com', '0912233445', '2024-04-07', '123456'),
('KH07', N'Nguyễn Thanh Tùng', 'thanhtung@gmail.com', '0908765432', '2024-04-07', '123456'),
('KH08', N'Ngô Minh Thuận', 'hanhngo@gmail.com', '0977567890', '2024-04-08', '123456'),
('KH09', N'Võ Đức Huy', 'huyvo@gmail.com', '0934789651', '2024-04-08', '123456'),
('KH10', N'Hồ Phan Minh Đăng', 'dangho@gmail.com', '0945123789', '2024-04-08', '123456'),
('KH11', N'Tô Thanh Hà', 'hatho@gmail.com', '0967894321', '2024-04-08', '123456'),
('KH12', N'Đặng Văn Nam', 'namdang@gmail.com', '0978312465', '2024-04-08', '123456'),
('KH13', N'Trần Quang Hưng', 'hungtran@gmail.com', '0923678456', '2024-04-08', '123456'),
('KH14', N'Nguyễn Kim Oanh', 'oanhnk@gmail.com', '0912348765', '2024-04-08', '123456'),
('KH15', N'Lương Minh Khoa', 'khoaluong@gmail.com', '0909988776', '2024-04-08', '123456'),
('KH16', N'Phan Thị Thảo', 'thaophan@gmail.com', '0933467890', '2024-04-08', '123456'),
('KH17', N'Cao Văn Bình', 'binhcao@gmail.com', '0965456789', '2024-04-08', '123456'),
('KH18', N'Tống Ngọc Duy', 'duytong@gmail.com', '0977654321', '2024-04-08', '123456');
go


-- 7. Dữ liệu cho bảng DatVe --
INSERT INTO DatVe (idDatVe, idKhach, idLich, GheNgoi, ThoiGianDat, TongTien) 
VALUES ('DV1', 'KH01', 'L1', 'A1', '2024-04-08', 70000),
('DV2', 'KH02', 'L2', 'B1', '2024-04-08', 70000);
go

-- 8. Dữ liệu cho bảng VeXemPhim --
INSERT INTO VeXemPhim (idVe, idDatVe, Ghe, Gia) 
VALUES ('V1', 'DV1', 'A1', 70000),
('V2', 'DV1', 'A2', 70000),
('V3', 'DV2', 'B1', 70000);
go


-- 9. Dữ liệu cho bảng NhanVien --
INSERT INTO NhanVien (idNhanVien, idRap, Ten, ViTri, Luong, NgayVaoLam) 
VALUES ('NV01', 'R1', N'Lê Văn Nhân', N'Quản lý', 15000000, '2022-01-22'),
('NV02', 'R1', N'Nguyễn Văn Nhàn', N'Nhân viên', 12000000, '2022-11-21'),
('NV03', 'R2', N'Trúc Ly', N'Quản lý', 15000000, '2022-10-01'),
('NV04', 'R2', N'Hoàng Mỹ Linh', N'Nhân viên', 8000000, '2023-06-15'),
('NV05', 'R3', N'Khánh Hoàng', N'Quản lý', 15000000, '2021-9-01'),
('NV06', 'R3', N'Nguyễn Trí Tài', N'Nhân viên', 6000000, '2023-06-15');
go

-- 10. Dữ liệu cho bảng KhuyenMai --
INSERT INTO KhuyenMai (idKhuyenMai, TenKhuyenMai, MoTa, PhanTramGiam, NgayBatDau, NgayKetThuc) 
VALUES ('KM001', N'Giảm giá hè', N'Khuyến mãi mùa hè lên đến 20%', 20, '2025-06-01', '2025-06-30'),
('KM002', N'Ưu đãi đầu năm', N'Giảm 15% cho đơn hàng đầu tiên', 15, '2025-01-01', '2025-01-15'),
('KM003', N'Tháng sinh nhật', N'Giảm 30% mừng sinh nhật khách hàng', 30, '2025-07-10', '2025-07-20'),
('KM004', N'Mua 1 tặng 1', N'Áp dụng cho một số sản phẩm nhất định', 50, '2025-08-01', '2025-08-07'),
('KM005', N'Lễ hội mua sắm', N'Khuyến mãi lớn dịp lễ hội', 25, '2025-11-20', '2025-11-30');
go

INSERT INTO TaiKhoanNhanVien(idTaiKhoan, idNhanVien, TenDangNhap, MatKhau) 
VALUES ('TK01', 'NV01', 'abcde', '123456'),
('TK02', 'NV02', 'kk123', '123456'),
('TK03', 'NV03', 'thuan369', '123456');
go


INSERT INTO ComboMonAn(idMonAn, CacMonAn, GiaTien)
VALUES ('cb1', N'01 bắp nhỏ vị ngọt + 01 ly nước 22Oz', 77000),
('cb2', N'01 bắp nhỏ vị ngọt + 02 ly nước 22Oz', 109000),
('cb3', N'01 bắp nhỏ vị ngọt + 01 ly nước 22Oz + 01 gà vòng chiên', 114000),
('cb4', N'01 bắp nhỏ vị ngọt + 01 ly nước 22Oz + 01 khoai tây chiên', 114000),
('cb5', N'01 bắp nhỏ vị ngọt + 01 ly nước 22Oz + 01 xúc xích lốc xoáy', 114000)
go
INSERT INTO BookVe (idBookVe, idKhach, idLich, GheNgoi, ThoiGianDat, TongTien, TrangThai)
VALUES ('BV01', 'KH01', 'L1', 'A1,A2', '2024-04-08', 140000, N'Đang giữ chỗ'),
('BV02', 'KH02', 'L2', 'B3', '2024-04-09', 70000, N'Đang giữ chỗ'),
('BV03', 'KH03', 'L3', 'C5,C6,C7', '2024-04-10', 210000, N'Đang giữ chỗ'),
('BV04', 'KH04', 'L4', 'D1', '2024-04-11', 70000, N'Đã hủy'),
('BV05', 'KH05', 'L5', 'E2,E3', '2024-04-12', 140000, N'Đang giữ chỗ');
go

 select * from BookVe
 select * from KhachHang
 delete from KhachHang where Ten = 'dang1'
 SELECT * FROM Phim
 SELECT * FROM Phim WHERE NgayKhoiChieu > GETDATE()


