using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CinemaProject.Migrations
{
    /// <inheritdoc />
    public partial class AddDanhGiaPhim : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ComboMonAn",
                columns: table => new
                {
                    idMonAn = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
                    CacMonAn = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    GiaTien = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ComboMon__164239B628BA4B42", x => x.idMonAn);
                });

            migrationBuilder.CreateTable(
                name: "DoiTacPhim",
                columns: table => new
                {
                    idDoiTac = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
                    TenCongTy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    EmailDoiTac = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    SoDienThoaiDT = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    DiaChidt = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__DoiTacPh__AF8143C63278ABF2", x => x.idDoiTac);
                });

            migrationBuilder.CreateTable(
                name: "KhachHang",
                columns: table => new
                {
                    idKhach = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
                    Ten = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Email = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    SoDienThoai = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    NgayDangKy = table.Column<DateTime>(type: "date", nullable: true),
                    MatKhauKH = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__KhachHan__385411B93250E4B1", x => x.idKhach);
                });

            migrationBuilder.CreateTable(
                name: "KhuyenMai",
                columns: table => new
                {
                    idKhuyenMai = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
                    TenKhuyenMai = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MoTa = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    PhanTramGiam = table.Column<int>(type: "int", nullable: true),
                    NgayBatDau = table.Column<DateTime>(type: "date", nullable: false),
                    NgayKetThuc = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__KhuyenMa__637EEC7CBC51F554", x => x.idKhuyenMai);
                });

            migrationBuilder.CreateTable(
                name: "Rap",
                columns: table => new
                {
                    idRap = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
                    TenRap = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    DiaChi = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    SoDienThoai = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Rap__3C87B153392A9CEB", x => x.idRap);
                });

            migrationBuilder.CreateTable(
                name: "TheLoai",
                columns: table => new
                {
                    idTheLoai = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
                    TenTheLoai = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TheLoai__890D7EC8F0F7909F", x => x.idTheLoai);
                });

            migrationBuilder.CreateTable(
                name: "DonHangDoAn",
                columns: table => new
                {
                    idDonHang = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
                    idKhach = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: true),
                    DanhSachMon = table.Column<string>(type: "text", nullable: true),
                    TongTien = table.Column<int>(type: "int", nullable: false),
                    ThoiGianDat = table.Column<DateTime>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__DonHangD__D5DE7ED7B35708B6", x => x.idDonHang);
                    table.ForeignKey(
                        name: "FK__DonHangDo__idKha__5165187F",
                        column: x => x.idKhach,
                        principalTable: "KhachHang",
                        principalColumn: "idKhach");
                });

            migrationBuilder.CreateTable(
                name: "NhanVien",
                columns: table => new
                {
                    idNhanVien = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
                    idRap = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: true),
                    Ten = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ViTri = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Luong = table.Column<int>(type: "int", nullable: true),
                    NgayVaoLam = table.Column<DateTime>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__NhanVien__214E8258A2EC7880", x => x.idNhanVien);
                    table.ForeignKey(
                        name: "FK__NhanVien__idRap__4E88ABD4",
                        column: x => x.idRap,
                        principalTable: "Rap",
                        principalColumn: "idRap");
                });

            migrationBuilder.CreateTable(
                name: "PhongChieu",
                columns: table => new
                {
                    idPhong = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
                    idRap = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: true),
                    TenPhong = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    SoLuongGhe = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__PhongChi__E540EED4A143A496", x => x.idPhong);
                    table.ForeignKey(
                        name: "FK__PhongChie__idRap__398D8EEE",
                        column: x => x.idRap,
                        principalTable: "Rap",
                        principalColumn: "idRap");
                });

            migrationBuilder.CreateTable(
                name: "Phim",
                columns: table => new
                {
                    idPhim = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
                    TenPhim = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    idTheLoai = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: true),
                    ThoiLuong = table.Column<int>(type: "int", nullable: false),
                    NgayKhoiChieu = table.Column<DateTime>(type: "date", nullable: true),
                    DoTuoiPhuHop = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Phim__BFC6F683A1A705DC", x => x.idPhim);
                    table.ForeignKey(
                        name: "FK__Phim__idTheLoai__3E52440B",
                        column: x => x.idTheLoai,
                        principalTable: "TheLoai",
                        principalColumn: "idTheLoai");
                });

            migrationBuilder.CreateTable(
                name: "ApDungKhuyenMaiDoAn",
                columns: table => new
                {
                    idApDungDoAn = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
                    idDonHang = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: true),
                    idKhuyenMai = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: true),
                    SoTienGiam = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ApDungKh__CAA676886177205B", x => x.idApDungDoAn);
                    table.ForeignKey(
                        name: "FK__ApDungKhu__idDon__5BE2A6F2",
                        column: x => x.idDonHang,
                        principalTable: "DonHangDoAn",
                        principalColumn: "idDonHang");
                    table.ForeignKey(
                        name: "FK__ApDungKhu__idKhu__5CD6CB2B",
                        column: x => x.idKhuyenMai,
                        principalTable: "KhuyenMai",
                        principalColumn: "idKhuyenMai");
                });

            migrationBuilder.CreateTable(
                name: "TaiKhoanNhanVien",
                columns: table => new
                {
                    idTaiKhoan = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
                    idNhanVien = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: true),
                    TenDangNhap = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MatKhau = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TaiKhoan__8FA29E4A67B83B4A", x => x.idTaiKhoan);
                    table.ForeignKey(
                        name: "FK__TaiKhoanN__idNha__60A75C0F",
                        column: x => x.idNhanVien,
                        principalTable: "NhanVien",
                        principalColumn: "idNhanVien");
                });

            migrationBuilder.CreateTable(
                name: "DanhGiaPhim",
                columns: table => new
                {
                    idDanhGia = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
                    idKhach = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
                    idPhim = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
                    soSao = table.Column<int>(type: "int", nullable: false),
                    binhLuan = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ngayDanhGia = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DanhGiaPhim", x => x.idDanhGia);
                    table.ForeignKey(
                        name: "FK_DanhGiaPhim_KhachHang",
                        column: x => x.idKhach,
                        principalTable: "KhachHang",
                        principalColumn: "idKhach",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DanhGiaPhim_Phim",
                        column: x => x.idPhim,
                        principalTable: "Phim",
                        principalColumn: "idPhim",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LichChieu",
                columns: table => new
                {
                    idLich = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
                    idPhim = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: true),
                    idPhong = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: true),
                    NgayChieu = table.Column<DateTime>(type: "date", nullable: false),
                    GioChieu = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
                    GiaVe = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__LichChie__1439F14FF8768ABF", x => x.idLich);
                    table.ForeignKey(
                        name: "FK__LichChieu__idPhi__412EB0B6",
                        column: x => x.idPhim,
                        principalTable: "Phim",
                        principalColumn: "idPhim");
                    table.ForeignKey(
                        name: "FK__LichChieu__idPho__4222D4EF",
                        column: x => x.idPhong,
                        principalTable: "PhongChieu",
                        principalColumn: "idPhong");
                });

            migrationBuilder.CreateTable(
                name: "PhimDoiTac",
                columns: table => new
                {
                    idPhim = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
                    idDoiTac = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
                    NgayKiHopDong = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__PhimDoiT__C53EE2BF07F62161", x => new { x.idPhim, x.idDoiTac });
                    table.ForeignKey(
                        name: "FK__PhimDoiTa__idDoi__6754599E",
                        column: x => x.idDoiTac,
                        principalTable: "DoiTacPhim",
                        principalColumn: "idDoiTac");
                    table.ForeignKey(
                        name: "FK__PhimDoiTa__idPhi__66603565",
                        column: x => x.idPhim,
                        principalTable: "Phim",
                        principalColumn: "idPhim");
                });

            migrationBuilder.CreateTable(
                name: "BookVe",
                columns: table => new
                {
                    idBookVe = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
                    idKhach = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: true),
                    idLich = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
                    GheNgoi = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    ThoiGianDat = table.Column<DateTime>(type: "date", nullable: false),
                    TongTien = table.Column<int>(type: "int", nullable: true),
                    TrangThai = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__BookVe__...", x => x.idBookVe);
                    table.ForeignKey(
                        name: "FK_BookVe_KhachHang",
                        column: x => x.idKhach,
                        principalTable: "KhachHang",
                        principalColumn: "idKhach");
                    table.ForeignKey(
                        name: "FK_BookVe_LichChieu",
                        column: x => x.idLich,
                        principalTable: "LichChieu",
                        principalColumn: "idLich",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DatVe",
                columns: table => new
                {
                    idDatVe = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
                    idKhach = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: true),
                    idLich = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: true),
                    GheNgoi = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    ThoiGianDat = table.Column<DateTime>(type: "date", nullable: true),
                    TongTien = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__DatVe__B08CC224E1DC4A17", x => x.idDatVe);
                    table.ForeignKey(
                        name: "FK__DatVe__idKhach__47DBAE45",
                        column: x => x.idKhach,
                        principalTable: "KhachHang",
                        principalColumn: "idKhach");
                    table.ForeignKey(
                        name: "FK__DatVe__idLich__48CFD27E",
                        column: x => x.idLich,
                        principalTable: "LichChieu",
                        principalColumn: "idLich");
                });

            migrationBuilder.CreateTable(
                name: "ApDungKhuyenMaiVe",
                columns: table => new
                {
                    idApDung = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
                    idDatVe = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: true),
                    idKhuyenMai = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: true),
                    SoTienGiam = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ApDungKh__D551A5270F626334", x => x.idApDung);
                    table.ForeignKey(
                        name: "FK__ApDungKhu__idDat__5812160E",
                        column: x => x.idDatVe,
                        principalTable: "DatVe",
                        principalColumn: "idDatVe");
                    table.ForeignKey(
                        name: "FK__ApDungKhu__idKhu__59063A47",
                        column: x => x.idKhuyenMai,
                        principalTable: "KhuyenMai",
                        principalColumn: "idKhuyenMai");
                });

            migrationBuilder.CreateTable(
                name: "VeXemPhim",
                columns: table => new
                {
                    idVe = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
                    idDatVe = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: true),
                    Ghe = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    Gia = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__VeXemPhi__9DB83851A88F47BF", x => x.idVe);
                    table.ForeignKey(
                        name: "FK__VeXemPhim__idDat__4BAC3F29",
                        column: x => x.idDatVe,
                        principalTable: "DatVe",
                        principalColumn: "idDatVe");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApDungKhuyenMaiDoAn_idDonHang",
                table: "ApDungKhuyenMaiDoAn",
                column: "idDonHang");

            migrationBuilder.CreateIndex(
                name: "IX_ApDungKhuyenMaiDoAn_idKhuyenMai",
                table: "ApDungKhuyenMaiDoAn",
                column: "idKhuyenMai");

            migrationBuilder.CreateIndex(
                name: "IX_ApDungKhuyenMaiVe_idDatVe",
                table: "ApDungKhuyenMaiVe",
                column: "idDatVe");

            migrationBuilder.CreateIndex(
                name: "IX_ApDungKhuyenMaiVe_idKhuyenMai",
                table: "ApDungKhuyenMaiVe",
                column: "idKhuyenMai");

            migrationBuilder.CreateIndex(
                name: "IX_BookVe_idKhach",
                table: "BookVe",
                column: "idKhach");

            migrationBuilder.CreateIndex(
                name: "IX_BookVe_idLich",
                table: "BookVe",
                column: "idLich");

            migrationBuilder.CreateIndex(
                name: "IX_DanhGiaPhim_idKhach",
                table: "DanhGiaPhim",
                column: "idKhach");

            migrationBuilder.CreateIndex(
                name: "IX_DanhGiaPhim_idPhim",
                table: "DanhGiaPhim",
                column: "idPhim");

            migrationBuilder.CreateIndex(
                name: "IX_DatVe_idKhach",
                table: "DatVe",
                column: "idKhach");

            migrationBuilder.CreateIndex(
                name: "IX_DatVe_idLich",
                table: "DatVe",
                column: "idLich");

            migrationBuilder.CreateIndex(
                name: "UQ__DoiTacPh__82F98F8782557AAD",
                table: "DoiTacPhim",
                column: "EmailDoiTac",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DonHangDoAn_idKhach",
                table: "DonHangDoAn",
                column: "idKhach");

            migrationBuilder.CreateIndex(
                name: "UQ__KhachHan__A9D10534DE7EC02D",
                table: "KhachHang",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__KhuyenMa__A956B87C9A9E0698",
                table: "KhuyenMai",
                column: "TenKhuyenMai",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LichChieu_idPhim",
                table: "LichChieu",
                column: "idPhim");

            migrationBuilder.CreateIndex(
                name: "IX_LichChieu_idPhong",
                table: "LichChieu",
                column: "idPhong");

            migrationBuilder.CreateIndex(
                name: "IX_NhanVien_idRap",
                table: "NhanVien",
                column: "idRap");

            migrationBuilder.CreateIndex(
                name: "IX_Phim_idTheLoai",
                table: "Phim",
                column: "idTheLoai");

            migrationBuilder.CreateIndex(
                name: "IX_PhimDoiTac_idDoiTac",
                table: "PhimDoiTac",
                column: "idDoiTac");

            migrationBuilder.CreateIndex(
                name: "IX_PhongChieu_idRap",
                table: "PhongChieu",
                column: "idRap");

            migrationBuilder.CreateIndex(
                name: "IX_TaiKhoanNhanVien_idNhanVien",
                table: "TaiKhoanNhanVien",
                column: "idNhanVien");

            migrationBuilder.CreateIndex(
                name: "UQ__TaiKhoan__55F68FC0E94E4D30",
                table: "TaiKhoanNhanVien",
                column: "TenDangNhap",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VeXemPhim_idDatVe",
                table: "VeXemPhim",
                column: "idDatVe");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApDungKhuyenMaiDoAn");

            migrationBuilder.DropTable(
                name: "ApDungKhuyenMaiVe");

            migrationBuilder.DropTable(
                name: "BookVe");

            migrationBuilder.DropTable(
                name: "ComboMonAn");

            migrationBuilder.DropTable(
                name: "DanhGiaPhim");

            migrationBuilder.DropTable(
                name: "PhimDoiTac");

            migrationBuilder.DropTable(
                name: "TaiKhoanNhanVien");

            migrationBuilder.DropTable(
                name: "VeXemPhim");

            migrationBuilder.DropTable(
                name: "DonHangDoAn");

            migrationBuilder.DropTable(
                name: "KhuyenMai");

            migrationBuilder.DropTable(
                name: "DoiTacPhim");

            migrationBuilder.DropTable(
                name: "NhanVien");

            migrationBuilder.DropTable(
                name: "DatVe");

            migrationBuilder.DropTable(
                name: "KhachHang");

            migrationBuilder.DropTable(
                name: "LichChieu");

            migrationBuilder.DropTable(
                name: "Phim");

            migrationBuilder.DropTable(
                name: "PhongChieu");

            migrationBuilder.DropTable(
                name: "TheLoai");

            migrationBuilder.DropTable(
                name: "Rap");
        }
    }
}
