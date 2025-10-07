using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebDienTu.Models;

public partial class DienTuStoreContext : DbContext
{
    public DienTuStoreContext()
    {
    }

    public DienTuStoreContext(DbContextOptions<DienTuStoreContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ChiTietDonHang> ChiTietDonHangs { get; set; }

    public virtual DbSet<DanhGia> DanhGia { get; set; }

    public virtual DbSet<DanhMuc> DanhMucs { get; set; }

    public virtual DbSet<DanhMucThuocTinh> DanhMucThuocTinhs { get; set; }

    public virtual DbSet<DonHang> DonHangs { get; set; }

    public virtual DbSet<GiaTriThuocTinh> GiaTriThuocTinhs { get; set; }

    public virtual DbSet<GiamGia> GiamGia { get; set; }

    public virtual DbSet<GioHangTam> GioHangTams { get; set; }

    public virtual DbSet<QuanTriVien> QuanTriViens { get; set; }

    public virtual DbSet<SanPham> SanPhams { get; set; }

    public virtual DbSet<SanPhamDaXem> SanPhamDaXems { get; set; }

    public virtual DbSet<ThuocTinh> ThuocTinhs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=RA1NEE\\RA1NEE;Database=DienTuStore;User Id=Hieu2005;Password=14092005;MultipleActiveResultSets=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ChiTietDonHang>(entity =>
        {
            entity.HasKey(e => e.MaChiTiet).HasName("PK__ChiTietD__CDF0A114EFBC46B8");

            entity.ToTable("ChiTietDonHang");

            entity.Property(e => e.DonGia).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.MaDonHangNavigation).WithMany(p => p.ChiTietDonHangs)
                .HasForeignKey(d => d.MaDonHang)
                .HasConstraintName("FK_ChiTietDonHang_DonHang");

            entity.HasOne(d => d.MaSanPhamNavigation).WithMany(p => p.ChiTietDonHangs)
                .HasForeignKey(d => d.MaSanPham)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ChiTietDo__MaSan__5BE2A6F2");
        });

        modelBuilder.Entity<DanhGia>(entity =>
        {
            entity.HasKey(e => e.MaDanhGia).HasName("PK__DanhGia__AA9515BFF016B395");

            entity.Property(e => e.BinhLuan).HasMaxLength(500);
            entity.Property(e => e.NgayDanhGia)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.MaNguoiDungNavigation).WithMany(p => p.DanhGia)
                .HasForeignKey(d => d.MaNguoiDung)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DanhGia__MaNguoi__5DCAEF64");

            entity.HasOne(d => d.MaSanPhamNavigation).WithMany(p => p.DanhGia)
                .HasForeignKey(d => d.MaSanPham)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DanhGia__MaSanPh__5EBF139D");
        });

        modelBuilder.Entity<DanhMuc>(entity =>
        {
            entity.HasKey(e => e.MaDanhMuc).HasName("PK__DanhMuc__B37508870EC6C4D9");

            entity.ToTable("DanhMuc");

            entity.Property(e => e.MoTa).HasMaxLength(250);
            entity.Property(e => e.TenDanhMuc).HasMaxLength(100);
        });

        modelBuilder.Entity<DanhMucThuocTinh>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__DanhMucT__3214EC079FFFA6FD");

            entity.ToTable("DanhMucThuocTinh");

            entity.Property(e => e.ThuocTinhId).HasColumnName("ThuocTinhID");

            entity.HasOne(d => d.MaDanhMucNavigation).WithMany(p => p.DanhMucThuocTinhs)
                .HasForeignKey(d => d.MaDanhMuc)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DanhMucThuocTinh_DanhMuc");

            entity.HasOne(d => d.ThuocTinh).WithMany(p => p.DanhMucThuocTinhs)
                .HasForeignKey(d => d.ThuocTinhId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DanhMucThuocTinh_ThuocTinh");
        });

        modelBuilder.Entity<DonHang>(entity =>
        {
            entity.HasKey(e => e.MaDonHang).HasName("PK__DonHang__129584AD7868CB4A");

            entity.ToTable("DonHang");

            entity.Property(e => e.NgayDatHang)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.TongTien).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TrangThai).HasDefaultValue(false);

            entity.HasOne(d => d.MaNguoiDungNavigation).WithMany(p => p.DonHangs)
                .HasForeignKey(d => d.MaNguoiDung)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DonHang__MaNguoi__619B8048");
        });

        modelBuilder.Entity<GiaTriThuocTinh>(entity =>
        {
            entity.HasKey(e => e.GiaTriId).HasName("PK__GiaTriTh__27E18C65828A7D72");

            entity.ToTable("GiaTriThuocTinh");

            entity.Property(e => e.GiaTriId).HasColumnName("GiaTriID");
            entity.Property(e => e.GiaTri).HasMaxLength(255);
            entity.Property(e => e.ThuocTinhId).HasColumnName("ThuocTinhID");

            entity.HasOne(d => d.MaSanPhamNavigation).WithMany(p => p.GiaTriThuocTinhs)
                .HasForeignKey(d => d.MaSanPham)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_GiaTriThuocTinh_SanPham");

            entity.HasOne(d => d.ThuocTinh).WithMany(p => p.GiaTriThuocTinhs)
                .HasForeignKey(d => d.ThuocTinhId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_GiaTriThuocTinh_ThuocTinh");
        });

        modelBuilder.Entity<GiamGia>(entity =>
        {
            entity.HasKey(e => e.MaKhuyenMai).HasName("PK__GiamGia__6F56B3BD11937E48");

            entity.Property(e => e.GiaTri).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Loai).HasMaxLength(50);
            entity.Property(e => e.NgayBatDau).HasColumnType("datetime");
            entity.Property(e => e.NgayKetThuc).HasColumnType("datetime");
            entity.Property(e => e.TenChuongTrinh).HasMaxLength(200);
            entity.Property(e => e.TrangThai).HasDefaultValue(true);
        });

        modelBuilder.Entity<GioHangTam>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__GioHangT__3214EC074067B00B");

            entity.ToTable("GioHangTam");

            entity.Property(e => e.TrangThai)
                .HasMaxLength(50)
                .HasDefaultValue("ChuaThanhToan");

            entity.HasOne(d => d.MaNguoiDungNavigation).WithMany(p => p.GioHangTams)
                .HasForeignKey(d => d.MaNguoiDung)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__GioHangTa__MaNgu__6477ECF3");

            entity.HasOne(d => d.MaSanPhamNavigation).WithMany(p => p.GioHangTams)
                .HasForeignKey(d => d.MaSanPham)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__GioHangTa__MaSan__656C112C");
        });

        modelBuilder.Entity<QuanTriVien>(entity =>
        {
            entity.HasKey(e => e.MaNguoiDung).HasName("PK__QuanTriV__C539D76243C5A593");

            entity.ToTable("QuanTriVien");

            entity.HasIndex(e => e.Email, "UQ__QuanTriV__A9D1053405D96A20").IsUnique();

            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.HoTen).HasMaxLength(100);
            entity.Property(e => e.MatKhau).HasMaxLength(100);
            entity.Property(e => e.NgayDangKy)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.VaiTro).HasDefaultValue(0);
        });

        modelBuilder.Entity<SanPham>(entity =>
        {
            entity.HasKey(e => e.MaSanPham).HasName("PK__SanPham__FAC7442DDCC06E89");

            entity.ToTable("SanPham");

            entity.Property(e => e.BaoHanh).HasMaxLength(50);
            entity.Property(e => e.Gia).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.GiaBan).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.HinhAnh).HasMaxLength(200);
            entity.Property(e => e.Loai).HasMaxLength(50);
            entity.Property(e => e.NgayThem)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.SoLuongTon).HasDefaultValue(0);
            entity.Property(e => e.TenSanPham).HasMaxLength(150);
            entity.Property(e => e.ThuongHieu).HasMaxLength(100);
            entity.Property(e => e.TrangThai).HasDefaultValue(true);
            entity.Property(e => e.XuatXu).HasMaxLength(100);

            entity.HasOne(d => d.MaDanhMucNavigation).WithMany(p => p.SanPhams)
                .HasForeignKey(d => d.MaDanhMuc)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__SanPham__MaDanhM__66603565");

            entity.HasMany(d => d.MaKhuyenMais).WithMany(p => p.MaSanPhams)
                .UsingEntity<Dictionary<string, object>>(
                    "SanPhamKhuyenMai",
                    r => r.HasOne<GiamGia>().WithMany()
                        .HasForeignKey("MaKhuyenMai")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__SanPham_K__MaKhu__6754599E"),
                    l => l.HasOne<SanPham>().WithMany()
                        .HasForeignKey("MaSanPham")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__SanPham_K__MaSan__68487DD7"),
                    j =>
                    {
                        j.HasKey("MaSanPham", "MaKhuyenMai").HasName("PK__SanPham___3C322F16B20BB90E");
                        j.ToTable("SanPham_KhuyenMai");
                    });
        });

        modelBuilder.Entity<SanPhamDaXem>(entity =>
        {
            entity.HasKey(e => e.MaDaXem).HasName("PK__SanPhamD__72FF565ADE867AB0");

            entity.ToTable("SanPhamDaXem");

            entity.Property(e => e.ThoiGianXem)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.MaNguoiDungNavigation).WithMany(p => p.SanPhamDaXems)
                .HasForeignKey(d => d.MaNguoiDung)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SanPhamDaXem_NguoiDung");

            entity.HasOne(d => d.MaSanPhamNavigation).WithMany(p => p.SanPhamDaXems)
                .HasForeignKey(d => d.MaSanPham)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SanPhamDaXem_SanPham");
        });

        modelBuilder.Entity<ThuocTinh>(entity =>
        {
            entity.HasKey(e => e.ThuocTinhId).HasName("PK__ThuocTin__3E8FF1EE094FE841");

            entity.ToTable("ThuocTinh");

            entity.Property(e => e.ThuocTinhId).HasColumnName("ThuocTinhID");
            entity.Property(e => e.TenThuocTinh).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
