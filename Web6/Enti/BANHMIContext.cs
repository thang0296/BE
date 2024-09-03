using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Web6.Enti
{
    public partial class BANHMIContext : DbContext
    {
        public BANHMIContext()
        {
        }

        public BANHMIContext(DbContextOptions<BANHMIContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<Category1> Categories1 { get; set; } = null!;
        public virtual DbSet<Customer> Customers { get; set; } = null!;
        public virtual DbSet<HoaD0n> HoaD0ns { get; set; } = null!;
        public virtual DbSet<HoaDonCaTegory> HoaDonCaTegories { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<OrderDetail> OrderDetails { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<RoleAction> RoleActions { get; set; } = null!;
        public virtual DbSet<Supplier> Suppliers { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<UserIn4> UserIn4s { get; set; } = null!;
        public virtual DbSet<UserRole> UserRoles { get; set; } = null!;
        //public virtual DbSet<WebAction> WebActions { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=Admin\\THANG1302;Initial Catalog=BANHMI;Integrated Security=True;Encrypt=True;Trust Server Certificate=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.Id).HasComment("Mã loại");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.NameVn)
                    .HasMaxLength(50)
                    .HasColumnName("NameVN")
                    .HasComment("Tên chủng loại");
            });

            modelBuilder.Entity<Category1>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Category");

                entity.Property(e => e.MauSac)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.MoTa).HasColumnType("text");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasMaxLength(20)
                    .HasComment("Mã khách hàng");

                entity.Property(e => e.Activated).HasComment("Đã kích hoạt hay chưa");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .HasComment("Email");

                entity.Property(e => e.Fullname)
                    .HasMaxLength(50)
                    .HasComment("Họ và tên");

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .HasComment("Mật khẩu đăng nhập");

                entity.Property(e => e.Photo)
                    .HasMaxLength(50)
                    .HasDefaultValueSql("(N'Photo.gif')")
                    .HasComment("Hình");
            });

            modelBuilder.Entity<HoaD0n>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("HoaD0n");

                entity.Property(e => e.MaHoaDon)
                    .HasMaxLength(10)
                    .IsFixedLength();
            });

            modelBuilder.Entity<HoaDonCaTegory>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("HoaDonCaTegory");

                entity.Property(e => e.Id)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.IdCategory)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.IdHoaDon)
                    .HasMaxLength(10)
                    .IsFixedLength();
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(e => e.Id).HasComment("Mã hóa đơn");

                entity.Property(e => e.Address)
                    .HasMaxLength(60)
                    .HasComment("Địa chỉ nhận");

                entity.Property(e => e.CustomerId)
                    .HasMaxLength(20)
                    .HasComment("Mã khách hàng");

                entity.Property(e => e.Description)
                    .HasMaxLength(1000)
                    .HasComment("Ghi chú thêm");

                entity.Property(e => e.OrderDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Ngày đặt hàng");

                entity.Property(e => e.Receiver)
                    .HasMaxLength(50)
                    .HasComment("Tên người nhận");

                entity.Property(e => e.RequireDate)
                    .HasColumnType("datetime")
                    .HasComment("Ngày cần có hàng");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK_Orders_Customers");
            });

            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.Property(e => e.Id).HasComment("Mã chi tiết");

                entity.Property(e => e.OrderId).HasComment("Mã hóa đơn");

                entity.Property(e => e.ProductId).HasComment("Mã hàng hóa");

                entity.Property(e => e.Quantity)
                    .HasDefaultValueSql("((1))")
                    .HasComment("Số lượng mua");

                entity.Property(e => e.UnitPrice).HasComment("Đơn giá bán");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK_OrderDetails_Orders");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderDetails_Products");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.Id).HasComment("Mã hàng hóa");

                entity.Property(e => e.Available)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.CategoryId).HasComment("Mã loại, FK");

                entity.Property(e => e.Description)
                    .HasMaxLength(2000)
                    .HasComment("Mô tả hàng hóa");

                entity.Property(e => e.Discount).HasDefaultValueSql("(rand())");

                entity.Property(e => e.Image)
                    .HasMaxLength(50)
                    .HasDefaultValueSql("(N'Product.gif')")
                    .HasComment("Hình ảnh");

                entity.Property(e => e.Name)
                    .HasMaxLength(40)
                    .HasComment("Tên hàng hóa");

                entity.Property(e => e.ProductDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Ngày sản xuất");

                entity.Property(e => e.Quantity).HasDefaultValueSql("((100))");

                entity.Property(e => e.SupplierId)
                    .HasMaxLength(50)
                    .HasDefaultValueSql("(N'NK')")
                    .HasComment("Mã nhà cung cấp");

                entity.Property(e => e.UnitBrief)
                    .HasMaxLength(50)
                    .HasComment("Mô tả đơn vị tính");

                entity.Property(e => e.UnitPrice).HasComment("Đơn giá");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK_HangHoa_Loai1");

                entity.HasOne(d => d.Supplier)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.SupplierId)
                    .HasConstraintName("FK_Products_Suppliers");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.Id).HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<RoleAction>(entity =>
            {
                entity.Property(e => e.RoleId).HasMaxLength(50);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.RoleActions)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_Permissions_Roles");

                //entity.HasOne(d => d.WebAction)
                //    .WithMany(p => p.RoleActions)
                //    .HasForeignKey(d => d.WebActionId)
                //    .HasConstraintName("FK_Permissions_Actions");
            });

            modelBuilder.Entity<Supplier>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .HasComment("Mã nhà cung cấp");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .HasComment("Email");

                entity.Property(e => e.Logo)
                    .HasMaxLength(50)
                    .HasDefaultValueSql("(N'Logo.gif')")
                    .HasComment("Logo nhà cung cấp");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasComment("Tên công ty");

                entity.Property(e => e.Phone)
                    .HasMaxLength(50)
                    .HasComment("Số điện thoại liên lạc");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Id).HasMaxLength(50);

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.FullName).HasMaxLength(50);

                entity.Property(e => e.Password).HasMaxLength(50);

                entity.Property(e => e.Phone).HasMaxLength(50);
            });

            modelBuilder.Entity<UserIn4>(entity =>
            {
                //entity.HasNoKey();

                entity.ToTable("UserIn4");

                entity.Property(e => e.DiaChi)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Id)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Sdt)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Ten)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Tuoi)
                    .HasMaxLength(10)
                    .IsFixedLength();
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.Property(e => e.RoleId).HasMaxLength(50);

                entity.Property(e => e.UserId).HasMaxLength(50);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.UserRoles)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_UserRole_Role");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserRoles)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_UserRole_User");
            });

            //modelBuilder.Entity<WebAction>(entity =>
            //{
            //    entity.Property(e => e.Description).HasMaxLength(50);

            //    entity.Property(e => e.Name).HasMaxLength(50);
            //});

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
