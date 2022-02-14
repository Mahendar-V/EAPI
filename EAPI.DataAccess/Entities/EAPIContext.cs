using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace EAPI.DataAccess.Entities
{
    public partial class EAPIContext : DbContext
    {
        public EAPIContext()
        {
        }

        public EAPIContext(DbContextOptions<EAPIContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=LPI105\\SQLEXPRESS;Initial Catalog=EAPI;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("ORDERS");

                entity.Property(e => e.OrderId).HasColumnName("ORDER_ID");

                entity.Property(e => e.Cost).HasColumnName("COST");

                entity.Property(e => e.Description)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("DESCRIPTION");

                entity.Property(e => e.OrderDate)
                    .HasColumnType("datetime")
                    .HasColumnName("ORDER_DATE");

                entity.Property(e => e.OrderNumber).HasColumnName("ORDER_NUMBER");

                entity.Property(e => e.PId).HasColumnName("P_ID");

                entity.Property(e => e.Quantity).HasColumnName("QUANTITY");

                entity.Property(e => e.ShippingAddress)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("SHIPPING_ADDRESS");

                entity.Property(e => e.UserId).HasColumnName("USER_ID");

                entity.HasOne(d => d.PIdNavigation)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.PId)
                    .HasConstraintName("FK_ORDERS_EAPI_ITEMS");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_ORDERS_EAPI_USERS");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.PId);

                entity.ToTable("PRODUCTS");

                entity.Property(e => e.PId).HasColumnName("P_ID");

                entity.Property(e => e.Description)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("DESCRIPTION");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("NAME");

                entity.Property(e => e.Price).HasColumnName("PRICE");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("USERS");

                entity.Property(e => e.UserId).HasColumnName("USER_ID");

                entity.Property(e => e.Address)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("ADDRESS");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("EMAIL");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("USER_NAME");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
