create DATABASE RapChieuPhim
go
USE RapChieuPhim
go
set dateformat dmy
go

CREATE TABLE DoTuoiPhuHop (
    MaDoTuoi VARCHAR(10) PRIMARY KEY,         -- PG, PG-13, R, G, v.v.
    MoTa NVARCHAR(255),                       -- Mô tả ngắn gọn
    TuoiToiThieu INT CHECK (TuoiToiThieu >= 0) -- Độ tuổi tối thiểu
)
go
INSERT INTO DoTuoiPhuHop VALUES
('G', N'Phù hợp mọi lứa tuổi', 0),
('PG', N'Phụ huynh cần hướng dẫn', 10),
('PG-13', N'Không phù hợp dưới 13 tuổi', 13),
('PG-16', N'Không phù hợp dưới 16 tuổi', 16),
('R', N'Cấm trẻ em dưới 18 tuổi', 18)
go

create table Ghe(
	idGhe varchar(30) primary key,
	LoaiGhe nvarchar(255),
	Gia int not null
)
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
	SoLuongGheDoi int NULL,
	SoLuongGheVip INT NULL,
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
    MaDoTuoi VARCHAR(10),  -- độ tuổi phù hợp (pg-13, r, g...)
	HinhAnh nvarchar(255) NULL,
	MoTa nvarchar(MAX) NULL,
	foreign key (MaDoTuoi) references DoTuoiPhuHop(MaDoTuoi)
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
    NgaySinh date,
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
----option khác của đặt vé
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
	GiaTien DECIMAL(10, 2) NOT NULL
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
)
go
-- Dữ liệu giá tiền của ghế
INSERT INTO Ghe(idGhe, LoaiGhe, Gia)
VALUES
('GHE_DON', N'Ghế đơn', 70000),
('GHE_DOI', N'Ghế đôi', 145000),
('GHE_TRONG', N'Ghế trống', 0),
('GHE_VIP', N'Ghế Vip', 90000);
go
-- Dữ liệu cho bảng MonAnvaThucUong --
INSERT INTO MonAnvaThucUong (TenMon, Loai, Gia, MoTa, TrangThai) VALUES
(N'Coca-Cola', N'Thức uống', 15000, N'Nước ngọt có gas', 1),
(N'Bắp rang bơ', N'Món ăn', 25000, N'Bắp rang bơ ngọt', 1),
(N'Trà sữa', N'Thức uống', 20000, N'Trà sữa trân châu đường đen', 1);
go


-- 1. Dữ liệu cho bảng Rap --
INSERT INTO Rap (idRap, TenRap, DiaChi, SoDienThoai) 
VALUES ('R1', N'BHD STAR Lê Văn Việt', N'Tầng 4, Vincom Plaza Lê Văn Việt, 50 Lê Văn Việt, P.Hiệp Phú, Quận 9, TP.HCM', '011564215'),
('R2', N'BHD STAR Long Khánh', N'430 Hồ Thị Hương, Phường Xuân An, Thành Phố Long Khánh, Tỉnh Đồng Nai', '09129234567'),
('R3', N'BHD STAR Thảo Điền', N'Tầng 5, Vincom Mega Mall Thảo Điền, 159 Xa Lộ Hà Nội, P.Thảo Điền, TP.Thủ Đức, TPHCM', '012564865454')
go

-- 2. Dữ liệu cho bảng PhongChieu --
INSERT INTO PhongChieu (idPhong, idRap, TenPhong, SoLuongGhe, SoLuongGheDoi, SoLuongGheVip) 
VALUES ('PC1', 'R1', N'Phòng 1', 80, 3, 2),
('PC2', 'R1', N'Phòng 2', 80, 3, 3),
('PC3', 'R2', N'Phòng 1', 80, 4, 6),
('PC4', 'R2', N'Phòng 2', 100, 5, 8),
('PC5', 'R3', N'Phòng 1', 80, 5, 6),
('PC6', 'R3', N'Phòng 2', 90, 2, 5)
go

-- 3. Dữ liệu cho bảng TheLoai --
INSERT INTO TheLoai(idTheLoai, TenTheLoai)
VALUES
('HDG', N'Hành động'),
('TC', N'Tình cảm'),
('HH', N'Hoạt hình'),
('TL', N'Tâm lý'),
('CMD', N'Hài'),
('KD', N'Kinh dị'),
('PH', N'Phiêu lưu'),
('VT', N'Viễn tưởng'),
('TT', N'Tài liệu'),
('LS', N'Lịch sử'),
('CH', N'Chiến tranh'),
('TG', N'Trinh thám'),
('NH', N'Nhạc kịch'),
('GK', N'Gia đình'),
('PL', N'Pháp luật'),
('AN', N'Anime'),
('TV', N'Truyền hình'),
('CN', N'Cổ trang'),
('HDH', N'Học đường');


INSERT INTO Phim (idPhim, TenPhim, idTheLoai, ThoiLuong, NgayKhoiChieu, MaDoTuoi, HinhAnh, MoTa)
VALUES 
-- Phim đang chiếu
('P01', N'Avengers: Endgame', 'HDG', 120, '2025-06-01', 'PG-13', 'P01.jpg', N'Siêu phẩm Marvel, trận chiến cuối cùng với Thanos.'),
('P02', N'Mắt Biếc', 'TC', 120, '2025-06-10', 'PG', 'P02.jpg', N'Chuyện tình buồn tuổi học trò được chuyển thể từ truyện của Nguyễn Nhật Ánh.'),
('P03', N'The Batman', 'HDG', 176, '2025-05-15', 'PG-13', 'P03.jpg', N'Batman trở lại trong cuộc chiến chống lại tội ác tại Gotham.'),
('P04', N'Your Name', 'AN', 112, '2025-05-20', 'PG', 'P04.jpg', N'Bộ phim hoạt hình lãng mạn nổi tiếng của Nhật Bản, về hoán đổi thân xác.'),
('P05', N'Parasite', 'TL', 132, '2025-06-25', 'R', 'P05.jpg', N'Tác phẩm đoạt giải Oscar, kể về khoảng cách giai cấp trong xã hội Hàn Quốc.'),
('P13', N'Sono Bisque Doll wa Koi wo Suru Season 2 Tập 1', 'AN,TC,HDH', 45, '2025-07-07', 'R', 'P13.jpg', N'Mùa 2 của Sono Bisque Doll wa Koi wo Suru.'),
('P14', N'Cô Gái Đến Từ Hôm Qua', 'TC,HDH', 110, '2025-06-28', 'PG', 'P14.jpg', N'Phim học đường Việt Nam lãng mạn, chuyển thể từ truyện Nguyễn Nhật Ánh.'),
('P15', N'How to Train Your Dragon', 'HH,GK', 98, '2025-06-15', 'PG', 'P15.jpg', N'Câu chuyện cảm động giữa một cậu bé và rồng ở thế giới Viking.'),
('P16', N'Doraemon: Nobita và Mặt Trăng Phiêu Lưu Ký', 'HH,AN,PH', 111, '2025-06-30', 'PG', 'P16.jpg', N'Cuộc phiêu lưu mới của nhóm bạn Doraemon trên Mặt Trăng.'),
('P17', N'Tenet', 'HDG,VT,TG', 150, '2025-06-18', 'PG-13', 'P17.jpg', N'Bộ phim hành động nghẹt thở với yếu tố đảo ngược thời gian.'),
('P18', N'Chiếc Lá Cuốn Bay', 'TL,TC', 90, '2025-07-01', 'R', 'P18.jpg', N'Phim tâm lý Thái Lan xoay quanh những bí mật gia đình và danh tính.'),
('P19', N'Jujutsu Kaisen 0', 'AN,HDG', 105, '2025-07-06', 'R', 'P19.jpg', N'Tiền truyện nổi bật của series Jujutsu Kaisen, chiến đấu giữa người và lời nguyền.'),
('P20', N'Anh Hùng Xạ Điêu: Khởi Nguyên', 'CN,HDG,TC', 125, '2025-06-22', 'PG-13', 'P20.jpg', N'Khúc dạo đầu mới cho tiểu thuyết võ hiệp kinh điển Kim Dung, hành trình của Quách Tĩnh thời trẻ.'),
('P21', N'Thần Điêu Đại Hiệp: Duyên Phận Trùng Phùng', 'CN,TC,TL', 132, '2025-06-29', 'PG', 'P21.jpg', N'Chuyện tình sâu sắc giữa Dương Quá và Tiểu Long Nữ, trong bối cảnh giang hồ phân tranh.'),
('P22', N'Trường Tương Tư: Phần I', 'CN,TL', 140, '2025-07-05', 'PG', 'P22.jpg', N'Câu chuyện tình yêu và vận mệnh giữa các nhân vật hoàng tộc thời thượng cổ Trung Hoa.'),

('P23', N'Interstellar', 'VT,TL', 169, '2025-06-15', 'PG-13', 'P23.jpg', N'Hành trình xuyên không gian để cứu lấy nhân loại.'),
('P24', N'Venom 2: Đối Mặt Tử Thù', 'HDG,KD', 97, '2025-06-16', 'PG-13', 'P24.jpg', N'Cuộc chiến giữa Venom và Carnage đầy máu lửa.'),
('P25', N'Bố Già', 'TL,TC,GK', 128, '2025-06-17', 'PG', 'P25.jpg', N'Câu chuyện gia đình cảm động giữa Sài Gòn nhộn nhịp.'),
('P26', N'Weathering With You', 'AN,TC', 112, '2025-06-18', 'PG', 'P26.jpg', N'Tình yêu kỳ diệu giữa cậu bé và cô gái điều khiển thời tiết.'),
('P27', N'The Conjuring: The Devil Made Me Do It', 'KD,TG', 112, '2025-06-19', 'R', 'P27.jpg', N'Hành trình điều tra vụ án ám ảnh thực sự từ vợ chồng nhà Warren.'),
('P28', N'Spider-Man: No Way Home', 'HDG,VT', 148, '2025-06-20', 'PG-13', 'P28.jpg', N'Peter Parker gặp lại các Spider-Man khác qua đa vũ trụ.'),
('P29', N'Chiếc Hộp Ma Quái', 'KD,TL', 95, '2025-06-21', 'R', 'P29.jpg', N'Một chiếc hộp kỳ lạ thay đổi số phận cả gia đình.'),
('P30', N'Toy Story 4', 'HH,GK', 100, '2025-06-22', 'G', 'P30.jpg', N'Hành trình mới của Woody và các món đồ chơi quen thuộc.'),
('P31', N'Raya and the Last Dragon', 'HH,PH', 107, '2025-06-23', 'PG', 'P31.jpg', N'Một chiến binh Đông Nam Á tìm kiếm rồng cuối cùng để cứu thế giới.'),
('P32', N'Doraemon: Nobita và Đảo Giấu Vàng', 'HH,AN,PH', 108, '2025-06-24', 'PG', 'P32.jpg', N'Doraemon, Nobita và nhóm bạn cùng nhau lên đường khám phá một hòn đảo bí ẩn nơi cất giấu kho báu cổ xưa – nhưng điều họ tìm thấy còn lớn hơn cả vàng bạc.'),
('P33', N'Mulan (Live Action)', 'HDG,CN,TL', 115, '2025-06-25', 'PG-13', 'P33.jpg', N'Cô gái giả trai thay cha nhập ngũ, bảo vệ đất nước.'),
('P34', N'Trường Học Bá Đạo', 'CMD,HDH', 102, '2025-06-26', 'PG-13', 'P34.jpg', N'Học sinh quậy phá bất ngờ trở thành anh hùng cứu trường.'),
('P35', N'Cuộc Chiến Xuyên Không', 'VT,PH', 130, '2025-06-27', 'PG-13', 'P35.jpg', N'Cuộc Chiến Xuyên Không còn khiến người hâm mộ choáng ngợp với các pha hành động mãn nhãn vượt mọi thời đại từ đấu kiếm, bắn súng.'),
('P36', N'Ký Ức Kẻ Sát Nhân', 'TG,TL', 121, '2025-06-28', 'R', 'P36.jpg', N'Một cảnh sát điều tra kẻ sát nhân dựa trên ký ức mơ hồ của chính mình.'),
('P37', N'The Social Network', 'TT,PL', 120, '2025-06-29', 'PG-13', 'P37.jpg', N'Câu chuyện sáng lập Facebook và những tranh cãi pháp lý xoay quanh.'),
('P38', N'Nắng', 'TL,GK,TC', 100, '2025-06-30', 'PG', 'P38.jpg', N'Câu chuyện xúc động giữa bé Nắng và người mẹ thiểu năng – tình thân vượt lên số phận, đầy tiếng cười và nước mắt.'),
('P39', N'Frozen II', 'HH,AN,GK', 103, '2025-07-01', 'G', 'P39.jpg', N'Elsa lên đường tìm nguồn gốc sức mạnh kỳ diệu của mình.'),
('P40', N'Lật Mặt: Nhà Có Khách', 'CMD,KD', 100, '2025-07-02', 'PG-13', 'P40.jpg', N'Một chuyến về quê không yên bình với những bí ẩn rợn người.'),
('P41', N'La La Land', 'NH,TC,TL', 128, '2025-07-03', 'PG-13', 'P41.jpg', N'Một nhạc công jazz và một nữ diễn viên trẻ cùng theo đuổi ước mơ ở Los Angeles, giữa tình yêu và sự nghiệp.'),
('P42', N'Mission: Impossible – Fallout', 'HDG,TG', 147, '2025-07-04', 'PG-13', 'P42.jpg', N'Ethan Hunt đối đầu với kẻ thù nguy hiểm nhất từ trước đến nay.'),
('P43', N'Mắt Âm Dương', 'KD,TL', 108, '2025-07-05', 'R', 'P43.jpg', N'Cô gái nhìn thấy hồn ma sau một tai nạn kỳ lạ.'),
('P44', N'Encanto', 'HH,GK,AN', 102, '2025-07-06', 'PG', 'P44.jpg', N'Một gia đình kỳ diệu ở Colombia, mỗi người sở hữu phép thuật riêng.'),
('P45', N'The Greatest Showman', 'NH,TL,TC', 105, '2025-07-06', 'PG', 'P45.jpg', N'Câu chuyện về P.T. Barnum – người đàn ông đứng sau rạp xiếc vĩ đại nhất, với những màn trình diễn kết hợp âm nhạc lôi cuốn và cảm hứng sống mãnh liệt.'),
('P46', N'Diên Hi Công Lược', 'CN,TL', 138, '2025-07-05', 'PG', 'P46.jpg', N'Mưu lược chốn hậu cung và tình yêu giữa cung nữ và hoàng đế.'),
('P47', N'Coco', 'HH,GK,NH', 105, '2025-07-05', 'PG', 'P47.jpg', N'Một cậu bé đam mê âm nhạc vô tình bước vào thế giới người chết và khám phá bí mật gia đình mình.'),
('P48', N'Võ Tắc Thiên: Quyền Lực Đế Hậu', 'CN,TL,HDG', 124, '2025-07-05', 'PG-13', 'P48.jpg', N'Cuộc đời đầy biến động của Võ Tắc Thiên – từ cung nữ trở thành nữ hoàng quyền lực nhất lịch sử Trung Hoa.'),
('P49', N'Trò Đùa Của Tử Thần', 'KD,TG', 105, '2025-07-05', 'R', 'P49.jpg', N'Chuỗi sự kiện kinh hoàng xảy ra sau một trò đùa tưởng vô hại.'),
('P50', N'Her', 'TL,VT,TC', 126, '2025-07-05', 'PG-13', 'P50.jpg', N'Một người đàn ông cô đơn đem lòng yêu hệ điều hành trí tuệ nhân tạo – câu chuyện tình yêu giữa con người và công nghệ.'),

('P51', N'Thất Sơn Tâm Linh', 'KD,TG', 109, '2025-06-15', 'R', 'P51.jpg', N'Một nữ phóng viên trẻ khám phá những hiện tượng kỳ bí tại vùng núi thiêng Thất Sơn, nơi ẩn chứa tà thuật và những bí mật chết người.'),
('P52', N'Oppenheimer', 'LS,TL,TT', 180, '2025-06-16', 'R', 'P52.jpg', N'Câu chuyện về cha đẻ của bom nguyên tử và những hệ quả đạo đức sâu sắc.'),
('P53', N'Trolls Band Together', 'HH,NH,GK', 92, '2025-06-17', 'G', 'P53.jpg', N'Các chú Troll cùng nhau hợp lực để cứu lấy âm nhạc và tình bạn.'),
('P54', N'Insidious: The Red Door', 'KD,TG', 107, '2025-06-18', 'R', 'P54.jpg', N'Gia đình Lambert đối mặt với ác mộng quá khứ trong hành trình vượt qua cánh cửa đỏ.'),
('P55', N'Trong Vùng Đất Linh Hồn', 'AN,PH,TL', 125, '2025-06-19', 'PG', 'P55.jpg', N'Một cô bé lạc vào thế giới huyền bí của các linh hồn và phải vượt qua nhiều thử thách để cứu cha mẹ mình.'),
('P56', N'Poor Things', 'TG,TL,PL', 141, '2025-06-20', 'R', 'P56.jpg', N'Một phụ nữ được hồi sinh trong cơ thể khác và khám phá cuộc sống qua lăng kính mới.'),
('P57', N'Wish', 'HH,GK,NH', 95, '2025-06-21', 'PG', 'P57.jpg', N'Cô bé Asha chiến đấu với thế lực hắc ám bằng một ngôi sao ước nguyện.'),
('P58', N'The Flash', 'HDG,VT', 144, '2025-06-22', 'PG-13', 'P58.jpg', N'The Flash thay đổi dòng thời gian và gây ra một đa vũ trụ đầy nguy hiểm.'),
('P59', N'Nội Gián', 'TG,TL,PL', 118, '2025-06-23', 'R', 'P59.jpg', N'Một cảnh sát chìm rơi vào lằn ranh giữa công lý và tội ác trong thế giới ngầm.'),
('P60', N'Soul', 'HH,TL,GK', 100, '2025-06-24', 'PG', 'P60.jpg', N'Một giáo viên nhạc rơi vào thế giới linh hồn để tìm lại ý nghĩa cuộc sống.'),
('P61', N'Aquaman and the Lost Kingdom', 'HDG,PH,VT', 125, '2025-06-25', 'PG-13', 'P61.jpg', N'Aquaman cùng đồng minh khám phá vương quốc bị lãng quên để ngăn chặn hủy diệt.'),
('P62', N'Nàng Tiên Cá (Live Action)', 'HH,TC,PH', 135, '2025-06-26', 'PG', 'P62.jpg', N'Nàng tiên cá Ariel đấu tranh giữa tình yêu và tự do khi lên đất liền.'),
('P63', N'Elemental', 'HH,GK', 101, '2025-06-27', 'PG', 'P63.jpg', N'Câu chuyện tình yêu lạ lùng giữa hai nguyên tố Lửa và Nước trong thành phố nguyên tố.'),
('P64', N'John Wick: Chapter 4', 'HDG,TG', 169, '2025-06-28', 'R', 'P64.jpg', N'Sát thủ huyền thoại John Wick đối mặt với toàn bộ thế giới ngầm để giành lại tự do.'),
('P65', N'Em Và Trịnh', 'TL,TC,VT', 118, '2025-06-29', 'PG', 'P65.jpg', N'Bức chân dung nên thơ về cuộc đời và tình yêu của cố nhạc sĩ Trịnh Công Sơn qua góc nhìn của những người phụ nữ đã đi qua cuộc đời ông.'),
('P66', N'Stay Alive', 'KD,TG,VT', 102, '2025-06-30', 'R', 'P66.jpg', N'Một nhóm bạn bị mắc kẹt trong trò chơi sinh tử và phải tuân theo luật lệ bí ẩn.'),
('P67', N'Chị Chị Em Em 2', 'TL,TC,CN', 115, '2025-07-01', 'R', 'P67.jpg', N'Tình yêu, đố kỵ và âm mưu giữa hai người phụ nữ trong giới showbiz cổ trang.'),
('P68', N'The Marvels', 'HDG,VT', 110, '2025-07-02', 'PG-13', 'P68.jpg', N'Ba nữ siêu anh hùng liên kết sức mạnh trong một nhiệm vụ xuyên thiên hà.'),
('P69', N'Mưu Kế Thượng Lưu', 'TL,PL', 115, '2025-07-03', 'R', 'P69.jpg', N'Một nữ luật sư trẻ vô tình rơi vào vòng xoáy âm mưu giữa giới thượng lưu và phải chọn giữa sự thật và quyền lực.'),
('P70', N'Mirai', 'AN,GK,TL', 98, '2025-07-04', 'PG', 'P70.jpg', N'Cậu bé 4 tuổi gặp chị gái mình đến từ tương lai và học cách trưởng thành qua chuyến du hành kỳ lạ.'),

-- Phim ra mắt hôm nay
('P06', N'Top Gun: Maverick', 'HDG', 131, '2025-07-07', 'PG-13', 'P06.jpg', N'Phi công kỳ cựu Maverick trở lại cùng những trận không chiến mãn nhãn.'),
('P07', N'Spider-Man: No Way Home', 'HDG', 148, '2025-07-07', 'PG-13', 'P07.jpg', N'Spider-Man đối mặt đa vũ trụ và các phản diện từ nhiều thế giới.'),

-- Phim sắp chiếu
('P08', N'Conan: Viên đạn đỏ', 'HDG', 110, '2025-08-06', 'PG', 'P08.jpg', N'Thám tử lừng danh Conan tham gia phá án trong vụ ám sát tại hội nghị quốc tế.'),
('P09', N'Suzume', 'HH', 122, '2025-08-08', 'PG', 'P09.jpg', N'Cô gái trẻ cùng hành trình đóng cánh cửa dẫn đến thảm hoạ.'),
('P10', N'Nhà Bà Nữ', 'CMD', 98, '2025-08-10', 'PG', 'P10.jpg', N'Phim hài – gia đình của Trấn Thành về xung đột giữa các thế hệ.'),
('P11', N'The Conjuring', 'KD', 98, '2025-08-11', 'PG-16', 'P11.jpg', N'Cặp đôi trừ tà đối đầu với thế lực ma quái tại căn nhà ám.'),
('P12', N'The Conjuring 2', 'KD', 98, '2025-08-15', 'PG-16', 'P12.jpg', N'Câu chuyện trừ tà tiếp theo tại nước Anh, dựa trên sự kiện có thật.')
go

-- 5. Dữ liệu cho bảng LichChieu --
INSERT INTO LichChieu (idLich, idPhim, idPhong, NgayChieu, GioChieu, GiaVe) 
VALUES 
('L001', 'P01', 'PC1', '2024-07-13', '09:00 - 11:00', 70000),
('L002', 'P01', 'PC1', '2024-07-13', '11:00 - 13:00', 70000),
('L003', 'P01', 'PC2', '2024-07-16', '20:00 - 22:00', 70000),
('L004', 'P02', 'PC2', '2024-07-16', '18:00 - 20:00', 70000),
('L005', 'P02', 'PC3', '2024-07-18', '09:00 - 11:00', 70000),
('L006', 'P02', 'PC3', '2024-07-18', '15:00 - 17:00', 70000),

('L007', 'P03', 'PC1', '2024-07-12', '11:00 - 13:00', 70000),
('L008', 'P03', 'PC1', '2024-07-12', '13:00 - 15:00', 70000),
('L009', 'P03', 'PC2', '2024-07-14', '18:00 - 20:00', 70000),
('L010', 'P04', 'PC2', '2024-07-14', '20:00 - 22:00', 70000),
('L011', 'P04', 'PC3', '2024-07-23', '11:00 - 13:00', 70000),
('L012', 'P04', 'PC3', '2024-07-23', '13:00 - 15:00', 70000)
go

-- 6. Dữ liệu cho bảng KhachHang --
-- Dữ liệu cho bảng KhachHang với NgaySinh hợp lý
INSERT INTO KhachHang (idKhach, Ten, Email, SoDienThoai, NgaySinh, MatKhauKH) 
VALUES
('KH01', N'Nguyễn Văn A', 'vana@gmail.com', '0912345678', '2002-05-12', '123456'),
('KH02', N'Trần Thị B', 'thib@gmail.com', '0923456789', '2003-08-20', '123456'),
('KH03', N'Phạm Minh Tuấn', 'tuanpm@gmail.com', '0933123456', '2001-02-15', '123456'),
('KH04', N'Phạm Huy Minh Quang', 'quangpham@gmail.com', '0987456123', '2004-12-01', '123456'),
('KH05', N'Trịnh Quốc D', 'datquoc@gmail.com', '0967123874', '2005-07-18', '123456'),
('KH06', N'Châu Trần Minh Trí', 'minhtri@gmail.com', '0912233445', '2006-09-25', '123456'),
('KH07', N'Nguyễn Thanh Tùng', 'thanhtung@gmail.com', '0908765432', '2000-11-03', '123456'),
('KH08', N'Ngô Minh Thuận', 'hanhngo@gmail.com', '0977567890', '2001-04-08', '123456'),
('KH09', N'Võ Đức Huy', 'huyvo@gmail.com', '0934789651', '2007-06-28', '123456'),
('KH10', N'Hồ Phan Minh Đăng', 'dangho@gmail.com', '0945123789', '2003-10-14', '123456'),
('KH11', N'Tô Thanh Hà', 'hatho@gmail.com', '0967894321', '2002-03-21', '123456'),
('KH12', N'Đặng Văn Nam', 'namdang@gmail.com', '0978312465', '2005-01-01', '123456'),
('KH13', N'Trần Quang Hưng', 'hungtran@gmail.com', '0923678456', '2004-04-30', '123456'),
('KH14', N'Nguyễn Kim Oanh', 'oanhnk@gmail.com', '0912348765', '2006-07-07', '123456'),
('KH15', N'Lương Minh Khoa', 'khoaluong@gmail.com', '0909988776', '2002-06-16', '123456'),
('KH16', N'Phan Thị Thảo', 'thaophan@gmail.com', '0933467890', '2008-11-22', '123456'),
('KH17', N'Cao Văn Bình', 'binhcao@gmail.com', '0965456789', '2010-09-10', '123456'),
('KH18', N'Tống Ngọc Duy', 'duytong@gmail.com', '0977654321', '2012-12-12', '123456');
go


-- 7. Dữ liệu cho bảng DatVe --
INSERT INTO DatVe (idDatVe, idKhach, idLich, GheNgoi, ThoiGianDat, TongTien) 
VALUES ('DV1', 'KH01', 'L001', 'A1', '2024-04-08', 70000),
('DV2', 'KH02', 'L002', 'B1', '2024-04-08', 70000);
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
VALUES ('BV01', 'KH01', 'L001', 'A01, A02', '2024-04-08', 140000, N'Đang giữ chỗ'),
('BV02', 'KH02', 'L002', 'B03', '2024-04-09', 70000, N'Đang giữ chỗ'),
('BV03', 'KH03', 'L003', 'C05, C06, C07', '2024-04-10', 210000, N'Đang giữ chỗ'),
('BV04', 'KH04', 'L004', 'D01', '2024-04-11', 70000, N'Đã hủy'),
('BV05', 'KH05', 'L005', 'E02, E03', '2024-04-12', 140000, N'Đang giữ chỗ'),
('BV6', 'KH03', 'L003', 'H06', '2025-07-10', 70000, N'Đang giữ chỗ'),
('BV7', 'KH09', 'L004', 'C06', '2025-07-10', 70000, N'Đang giữ chỗ'),
('BV8', 'KH08', 'L010', 'H07', '2025-07-10', 70000, N'Đang giữ chỗ'),
('BV9', 'KH09', 'L012', 'B01, I01, E09', '2025-07-10', 210000, N'Đang giữ chỗ'),
('BV10', 'KH17', 'L012', 'D03, J03, H01', '2025-07-10', 210000, N'Đang giữ chỗ'),
('BV11', 'KH15', 'L012', 'J07', '2025-07-10', 70000, N'Đang giữ chỗ'),
('BV12', 'KH13', 'L006', 'D01, A07', '2025-07-10', 140000, N'Đang giữ chỗ'),
('BV13', 'KH16', 'L012', 'A05', '2025-07-10', 70000, N'Đang giữ chỗ'),
('BV14', 'KH09', 'L003', 'J08, C03', '2025-07-10', 140000, N'Đang giữ chỗ'),
('BV15', 'KH01', 'L007', 'E10, I07, F02', '2025-07-10', 210000, N'Đang giữ chỗ'),
('BV16', 'KH10', 'L006', 'E03', '2025-07-10', 70000, N'Đang giữ chỗ'),
('BV17', 'KH02', 'L003', 'H04', '2025-07-10', 70000, N'Đang giữ chỗ'),
('BV18', 'KH04', 'L007', 'J07, F02', '2025-07-10', 140000, N'Đang giữ chỗ'),
('BV19', 'KH12', 'L002', 'A10, I08', '2025-07-10', 140000, N'Đang giữ chỗ'),
('BV20', 'KH05', 'L009', 'A06, I03, H08', '2025-07-10', 210000, N'Đang giữ chỗ'),
('BV21', 'KH18', 'L005', 'D05, D08', '2025-07-10', 140000, N'Đang giữ chỗ'),
('BV22', 'KH05', 'L001', 'G01', '2025-07-10', 210000, N'Đang giữ chỗ');
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


 select * from BookVe
 select * from KhachHang
 delete from KhachHang where Ten = 'dang1'
 SELECT * FROM Phim
 SELECT * FROM Phim WHERE NgayKhoiChieu > GETDATE()
 select * from DanhGiaPhim
 SELECT * FROM MonAnvaThucUong WHERE TrangThai = 1



