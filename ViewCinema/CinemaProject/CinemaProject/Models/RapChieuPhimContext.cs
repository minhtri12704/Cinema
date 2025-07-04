using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CinemaProject.Models;

public partial class RapChieuPhimContext : DbContext
{
    public RapChieuPhimContext()
    {
    }

    public RapChieuPhimContext(DbContextOptions<RapChieuPhimContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ApDungKhuyenMaiDoAn> ApDungKhuyenMaiDoAns { get; set; }

    public virtual DbSet<ApDungKhuyenMaiVe> ApDungKhuyenMaiVes { get; set; }

    public virtual DbSet<ComboMonAn> ComboMonAns { get; set; }

    public virtual DbSet<DatVe> DatVes { get; set; }
    public virtual DbSet<BookVe> BookVes { get; set; }

    public virtual DbSet<DoiTacPhim> DoiTacPhims { get; set; }

    public virtual DbSet<DonHangDoAn> DonHangDoAns { get; set; }

    public virtual DbSet<KhachHang> KhachHangs { get; set; }

    public virtual DbSet<KhuyenMai> KhuyenMais { get; set; }

    public virtual DbSet<LichChieu> LichChieus { get; set; }

    public virtual DbSet<NhanVien> NhanViens { get; set; }

    public virtual DbSet<Phim> Phims { get; set; }

    public virtual DbSet<PhimDoiTac> PhimDoiTacs { get; set; }

    public virtual DbSet<PhongChieu> PhongChieus { get; set; }

    public virtual DbSet<Rap> Raps { get; set; }

    public virtual DbSet<TaiKhoanNhanVien> TaiKhoanNhanViens { get; set; }

    public virtual DbSet<TheLoai> TheLoais { get; set; }

    public virtual DbSet<VeXemPhim> VeXemPhims { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=RapChieuPhim;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ApDungKhuyenMaiDoAn>(entity =>
        {
            entity.HasKey(e => e.IdApDungDoAn).HasName("PK__ApDungKh__CAA676886177205B");

            entity.ToTable("ApDungKhuyenMaiDoAn");

            entity.Property(e => e.IdApDungDoAn)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("idApDungDoAn");
            entity.Property(e => e.IdDonHang)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("idDonHang");
            entity.Property(e => e.IdKhuyenMai)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("idKhuyenMai");

            entity.HasOne(d => d.IdDonHangNavigation).WithMany(p => p.ApDungKhuyenMaiDoAns)
                .HasForeignKey(d => d.IdDonHang)
                .HasConstraintName("FK__ApDungKhu__idDon__5BE2A6F2");

            entity.HasOne(d => d.IdKhuyenMaiNavigation).WithMany(p => p.ApDungKhuyenMaiDoAns)
                .HasForeignKey(d => d.IdKhuyenMai)
                .HasConstraintName("FK__ApDungKhu__idKhu__5CD6CB2B");
        });

        modelBuilder.Entity<ApDungKhuyenMaiVe>(entity =>
        {
            entity.HasKey(e => e.IdApDung).HasName("PK__ApDungKh__D551A5270F626334");

            entity.ToTable("ApDungKhuyenMaiVe");

            entity.Property(e => e.IdApDung)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("idApDung");
            entity.Property(e => e.IdDatVe)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("idDatVe");
            entity.Property(e => e.IdKhuyenMai)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("idKhuyenMai");

            entity.HasOne(d => d.IdDatVeNavigation).WithMany(p => p.ApDungKhuyenMaiVes)
                .HasForeignKey(d => d.IdDatVe)
                .HasConstraintName("FK__ApDungKhu__idDat__5812160E");

            entity.HasOne(d => d.IdKhuyenMaiNavigation).WithMany(p => p.ApDungKhuyenMaiVes)
                .HasForeignKey(d => d.IdKhuyenMai)
                .HasConstraintName("FK__ApDungKhu__idKhu__59063A47");
        });

        modelBuilder.Entity<ComboMonAn>(entity =>
        {
            entity.HasKey(e => e.IdMonAn).HasName("PK__ComboMon__164239B628BA4B42");

            entity.ToTable("ComboMonAn");

            entity.Property(e => e.IdMonAn)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("idMonAn");
            entity.Property(e => e.CacMonAn).HasMaxLength(200);
        });

        modelBuilder.Entity<DatVe>(entity =>
        {
            entity.HasKey(e => e.IdDatVe).HasName("PK__DatVe__B08CC224E1DC4A17");

            entity.ToTable("DatVe");

            entity.Property(e => e.IdDatVe)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("idDatVe");
            entity.Property(e => e.GheNgoi)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.IdKhach)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("idKhach");
            entity.Property(e => e.IdLich)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("idLich");
            entity.Property(e => e.ThoiGianDat).HasColumnType("date");

            entity.HasOne(d => d.IdKhachNavigation).WithMany(p => p.DatVes)
                .HasForeignKey(d => d.IdKhach)
                .HasConstraintName("FK__DatVe__idKhach__47DBAE45");

            entity.HasOne(d => d.IdLichNavigation).WithMany(p => p.DatVes)
                .HasForeignKey(d => d.IdLich)
                .HasConstraintName("FK__DatVe__idLich__48CFD27E");
        });

        modelBuilder.Entity<DoiTacPhim>(entity =>
        {
            entity.HasKey(e => e.IdDoiTac).HasName("PK__DoiTacPh__AF8143C63278ABF2");

            entity.ToTable("DoiTacPhim");

            entity.HasIndex(e => e.EmailDoiTac, "UQ__DoiTacPh__82F98F8782557AAD").IsUnique();

            entity.Property(e => e.IdDoiTac)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("idDoiTac");
            entity.Property(e => e.DiaChidt)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.EmailDoiTac)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.SoDienThoaiDt)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("SoDienThoaiDT");
            entity.Property(e => e.TenCongTy).HasMaxLength(255);
        });

        modelBuilder.Entity<DonHangDoAn>(entity =>
        {
            entity.HasKey(e => e.IdDonHang).HasName("PK__DonHangD__D5DE7ED7B35708B6");

            entity.ToTable("DonHangDoAn");

            entity.Property(e => e.IdDonHang)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("idDonHang");
            entity.Property(e => e.DanhSachMon).HasColumnType("text");
            entity.Property(e => e.IdKhach)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("idKhach");
            entity.Property(e => e.ThoiGianDat).HasColumnType("date");

            entity.HasOne(d => d.IdKhachNavigation).WithMany(p => p.DonHangDoAns)
                .HasForeignKey(d => d.IdKhach)
                .HasConstraintName("FK__DonHangDo__idKha__5165187F");
        });

        modelBuilder.Entity<KhachHang>(entity =>
        {
            entity.HasKey(e => e.IdKhach).HasName("PK__KhachHan__385411B93250E4B1");

            entity.ToTable("KhachHang");

            entity.HasIndex(e => e.Email, "UQ__KhachHan__A9D10534DE7EC02D").IsUnique();

            entity.Property(e => e.IdKhach)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("idKhach");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.NgayDangKy).HasColumnType("date");
            entity.Property(e => e.SoDienThoai)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Ten).HasMaxLength(255);
        });

        modelBuilder.Entity<KhuyenMai>(entity =>
        {
            entity.HasKey(e => e.IdKhuyenMai).HasName("PK__KhuyenMa__637EEC7CBC51F554");

            entity.ToTable("KhuyenMai");

            entity.HasIndex(e => e.TenKhuyenMai, "UQ__KhuyenMa__A956B87C9A9E0698").IsUnique();

            entity.Property(e => e.IdKhuyenMai)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("idKhuyenMai");
            entity.Property(e => e.MoTa).HasMaxLength(200);
            entity.Property(e => e.NgayBatDau).HasColumnType("date");
            entity.Property(e => e.NgayKetThuc).HasColumnType("date");
            entity.Property(e => e.TenKhuyenMai).HasMaxLength(50);
        });

        modelBuilder.Entity<LichChieu>(entity =>
        {
            entity.HasKey(e => e.IdLich).HasName("PK__LichChie__1439F14FF8768ABF");

            entity.ToTable("LichChieu");

            entity.Property(e => e.IdLich)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("idLich");
            entity.Property(e => e.GioChieu)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.IdPhim)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("idPhim");
            entity.Property(e => e.IdPhong)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("idPhong");
            entity.Property(e => e.NgayChieu).HasColumnType("date");

            entity.HasOne(d => d.IdPhimNavigation).WithMany(p => p.LichChieus)
                .HasForeignKey(d => d.IdPhim)
                .HasConstraintName("FK__LichChieu__idPhi__412EB0B6");

            entity.HasOne(d => d.IdPhongNavigation).WithMany(p => p.LichChieus)
                .HasForeignKey(d => d.IdPhong)
                .HasConstraintName("FK__LichChieu__idPho__4222D4EF");
        });

        modelBuilder.Entity<NhanVien>(entity =>
        {
            entity.HasKey(e => e.IdNhanVien).HasName("PK__NhanVien__214E8258A2EC7880");

            entity.ToTable("NhanVien");

            entity.Property(e => e.IdNhanVien)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("idNhanVien");
            entity.Property(e => e.IdRap)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("idRap");
            entity.Property(e => e.NgayVaoLam).HasColumnType("date");
            entity.Property(e => e.Ten).HasMaxLength(255);
            entity.Property(e => e.ViTri).HasMaxLength(100);

            entity.HasOne(d => d.IdRapNavigation).WithMany(p => p.NhanViens)
                .HasForeignKey(d => d.IdRap)
                .HasConstraintName("FK__NhanVien__idRap__4E88ABD4");
        });

        modelBuilder.Entity<Phim>(entity =>
        {
            entity.HasKey(e => e.IdPhim).HasName("PK__Phim__BFC6F683A1A705DC");

            entity.ToTable("Phim");

            entity.Property(e => e.IdPhim)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("idPhim");
            entity.Property(e => e.DoTuoiPhuHop)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.IdTheLoai)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("idTheLoai");
            entity.Property(e => e.NgayKhoiChieu).HasColumnType("date");
            entity.Property(e => e.TenPhim).HasMaxLength(255);

            entity.HasOne(d => d.IdTheLoaiNavigation).WithMany(p => p.Phims)
                .HasForeignKey(d => d.IdTheLoai)
                .HasConstraintName("FK__Phim__idTheLoai__3E52440B");
        });

        modelBuilder.Entity<PhimDoiTac>(entity =>
        {
            entity.HasKey(e => new { e.IdPhim, e.IdDoiTac }).HasName("PK__PhimDoiT__C53EE2BF07F62161");

            entity.ToTable("PhimDoiTac");

            entity.Property(e => e.IdPhim)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("idPhim");
            entity.Property(e => e.IdDoiTac)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("idDoiTac");
            entity.Property(e => e.NgayKiHopDong).HasColumnType("date");

            entity.HasOne(d => d.IdDoiTacNavigation).WithMany(p => p.PhimDoiTacs)
                .HasForeignKey(d => d.IdDoiTac)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PhimDoiTa__idDoi__6754599E");

            entity.HasOne(d => d.IdPhimNavigation).WithMany(p => p.PhimDoiTacs)
                .HasForeignKey(d => d.IdPhim)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PhimDoiTa__idPhi__66603565");
        });

        modelBuilder.Entity<PhongChieu>(entity =>
        {
            entity.HasKey(e => e.IdPhong).HasName("PK__PhongChi__E540EED4A143A496");

            entity.ToTable("PhongChieu");

            entity.Property(e => e.IdPhong)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("idPhong");
            entity.Property(e => e.IdRap)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("idRap");
            entity.Property(e => e.TenPhong).HasMaxLength(50);

            entity.HasOne(d => d.IdRapNavigation).WithMany(p => p.PhongChieus)
                .HasForeignKey(d => d.IdRap)
                .HasConstraintName("FK__PhongChie__idRap__398D8EEE");
        });

        modelBuilder.Entity<Rap>(entity =>
        {
            entity.HasKey(e => e.IdRap).HasName("PK__Rap__3C87B153392A9CEB");

            entity.ToTable("Rap");

            entity.Property(e => e.IdRap)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("idRap");
            entity.Property(e => e.DiaChi)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.SoDienThoai)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.TenRap).HasMaxLength(255);
        });

        modelBuilder.Entity<TaiKhoanNhanVien>(entity =>
        {
            entity.HasKey(e => e.IdTaiKhoan).HasName("PK__TaiKhoan__8FA29E4A67B83B4A");

            entity.ToTable("TaiKhoanNhanVien");

            entity.HasIndex(e => e.TenDangNhap, "UQ__TaiKhoan__55F68FC0E94E4D30").IsUnique();

            entity.Property(e => e.IdTaiKhoan)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("idTaiKhoan");
            entity.Property(e => e.IdNhanVien)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("idNhanVien");
            entity.Property(e => e.MatKhau)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.TenDangNhap).HasMaxLength(50);

            entity.HasOne(d => d.IdNhanVienNavigation).WithMany(p => p.TaiKhoanNhanViens)
                .HasForeignKey(d => d.IdNhanVien)
                .HasConstraintName("FK__TaiKhoanN__idNha__60A75C0F");
        });

        modelBuilder.Entity<TheLoai>(entity =>
        {
            entity.HasKey(e => e.IdTheLoai).HasName("PK__TheLoai__890D7EC8F0F7909F");

            entity.ToTable("TheLoai");

            entity.Property(e => e.IdTheLoai)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("idTheLoai");
            entity.Property(e => e.TenTheLoai).HasMaxLength(100);
        });

        modelBuilder.Entity<VeXemPhim>(entity =>
        {
            entity.HasKey(e => e.IdVe).HasName("PK__VeXemPhi__9DB83851A88F47BF");

            entity.ToTable("VeXemPhim");

            entity.Property(e => e.IdVe)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("idVe");
            entity.Property(e => e.Ghe)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.IdDatVe)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("idDatVe");

            entity.HasOne(d => d.IdDatVeNavigation).WithMany(p => p.VeXemPhims)
                .HasForeignKey(d => d.IdDatVe)
                .HasConstraintName("FK__VeXemPhim__idDat__4BAC3F29");
        });

        modelBuilder.Entity<BookVe>(entity =>
        {
            entity.HasKey(e => e.IdBookVe).HasName("PK__BookVe__..."); // Bạn có thể tự đặt tên hoặc để mặc định

            entity.ToTable("BookVe");

            entity.Property(e => e.IdBookVe)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("idBookVe");

            entity.Property(e => e.IdKhach)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("idKhach");

            entity.Property(e => e.IdLich)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("idLich");

            entity.Property(e => e.GheNgoi)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.ThoiGianDat)
                .HasColumnType("date");

            entity.Property(e => e.TrangThai)
                .HasMaxLength(50); // Unicode mặc định là true cho NVARCHAR

            entity.HasOne(d => d.IdKhachNavigation)
                .WithMany(p => p.BookVes)
                .HasForeignKey(d => d.IdKhach)
                .HasConstraintName("FK_BookVe_KhachHang");

            entity.HasOne(d => d.IdLichNavigation)
                .WithMany(p => p.BookVes)
                .HasForeignKey(d => d.IdLich)
                .HasConstraintName("FK_BookVe_LichChieu");
        });


        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
