using System;
using System.Collections.Generic;
using BookStore.Bl;
using Domains;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace BookStore.Models;
public partial class BookStoreContext : IdentityDbContext<ApplicationUser>
{
    public BookStoreContext()
    {
    }

    public BookStoreContext(DbContextOptions<BookStoreContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TbAuthor> TbAuthors { get; set; }

    public virtual DbSet<TbBook> TbBooks { get; set; }

    public virtual DbSet<TbCategory> TbCategories { get; set; }

    public virtual DbSet<TbCustomerDeliverInfo> TbCustomerDeliverInfos { get; set; }

    public virtual DbSet<TbDeliveryMan> TbDeliveryMen { get; set; }

    public virtual DbSet<TbDiscount> TbDiscounts { get; set; }

    public virtual DbSet<TbGovernorate> TbGovernorates { get; set; }

    public virtual DbSet<TbPublish> TbPublishes { get; set; }

    public virtual DbSet<TbPurchaseInvoice> TbPurchaseInvoices { get; set; }

    public virtual DbSet<TbPurchaseInvoiceBook> TbPurchaseInvoiceBooks { get; set; }

    public virtual DbSet<TbSalesInvoice> TbSalesInvoices { get; set; }

    public virtual DbSet<TbSalesInvoiceBook> TbSalesInvoiceBooks { get; set; }
    public virtual DbSet<TbSupplier> TbSuppliers { get; set; }
    public virtual DbSet<TbSlider> TbSliders { get; set; } = null!;
    public virtual DbSet<TbSettings> TbSettings { get; set; } = null!;
    public virtual DbSet<TbPages> TbPages { get; set; } = null!;
    public virtual DbSet<VwBook> VwBooks { get; set; }
    public virtual DbSet<VwSalesInvoice> VwSalesInvoices { get; set; }
    public virtual DbSet<VwSalesInvoiceBook> VwSalesInvoiceBooks { get; set; }
    public virtual DbSet<VwPurchaseInvoiceBook> VwPurchaseInvoiceBooks { get; set; }
    public virtual DbSet<VwPurchaseInvoice> VwPurchaseInvoices { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

    }
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<TbAuthor>(entity =>
        {
            entity.HasKey(e => e.AuthorId);

            entity.ToTable("TbAuthor");

            entity.Property(e => e.AuthorName).HasMaxLength(100);
        });

        modelBuilder.Entity<TbBook>(entity =>
        {
            entity.HasKey(e => e.BookId);

            entity.HasIndex(e => e.Isbn, "UK_Isbn_TbBooks").IsUnique();

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Isbn).HasMaxLength(50);
            entity.Property(e => e.PublishYear).HasMaxLength(10);
            entity.Property(e => e.PurchasePrice).HasColumnType("decimal(8, 2)");
            entity.Property(e => e.SalesPrice).HasColumnType("decimal(8, 2)");
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");

            entity.HasOne(d => d.Author).WithMany(p => p.TbBooks)
                .HasForeignKey(d => d.AuthorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TbBooks_TbAuthor");

            entity.HasOne(d => d.Category).WithMany(p => p.TbBooks)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TbBooks_TbCategories");

            entity.HasOne(d => d.Discount).WithMany(p => p.TbBooks).HasForeignKey(d => d.DiscountId);

            entity.HasOne(d => d.Publish).WithMany(p => p.TbBooks)
                .HasForeignKey(d => d.PublishId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TbBooks_TbPublish");
        });

        modelBuilder.Entity<TbCategory>(entity =>
        {
            entity.HasKey(e => e.CategoryId);

            entity.Property(e => e.CategoryName).HasMaxLength(100);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<TbCustomerDeliverInfo>(entity =>
        {
            entity.HasKey(e => e.CustomerDeliverId);

            entity.ToTable("TbCustomerDeliverInfo");

            entity.Property(e => e.Adress).HasMaxLength(200);
            entity.Property(e => e.CutomerName).HasMaxLength(50);
            entity.Property(e => e.PhoneNumber).HasMaxLength(16);

            entity.HasOne(d => d.Governorate).WithMany(p => p.TbCustomerDeliverInfos)
                .HasForeignKey(d => d.GovernorateId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TbCustomerDeliverInfo_TbGovernorates");
        });

        modelBuilder.Entity<TbDeliveryMan>(entity =>
        {
            entity.HasKey(e => e.DeliveryManId);

            entity.ToTable("TbDeliveryMan");

            entity.Property(e => e.Address).HasMaxLength(100);
            entity.Property(e => e.Budget).HasColumnType("decimal(8, 2)");
            entity.Property(e => e.DeliveryManName).HasMaxLength(100);
        });

        modelBuilder.Entity<TbDiscount>(entity =>
        {
            entity.HasKey(e => e.DiscountId);

            entity.ToTable("TbDiscount");

            entity.Property(e => e.ExpiryDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<TbGovernorate>(entity =>
        {
            entity.HasKey(e => e.GovernorateId);

            entity.Property(e => e.DeliveryPrice).HasColumnType("decimal(8, 2)");
            entity.Property(e => e.GovernorateName).HasMaxLength(50);
        });

        modelBuilder.Entity<TbPublish>(entity =>
        {
            entity.HasKey(e => e.PublishId);

            entity.ToTable("TbPublish");

            entity.Property(e => e.Publisher)
                .HasMaxLength(100)
                .HasDefaultValue("Unknown");
        });

        modelBuilder.Entity<TbPurchaseInvoice>(entity =>
        {
            entity.HasKey(e => e.PurchaseInvoiceId);

            entity.ToTable("TbPurchaseInvoice");

            entity.Property(e => e.InvoiceDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.TotalPrice).HasColumnType("decimal(8, 2)");
            entity.Property(e => e.UpdatedBy).HasMaxLength(100);
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

            entity.HasOne(d => d.Supplier).WithMany(p => p.TbPurchaseInvoices)
                .HasForeignKey(d => d.SupplierId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TbPurchaseInvoice_TbSuppliers");
        });

        modelBuilder.Entity<TbPurchaseInvoiceBook>(entity =>
        {
            entity.HasKey(e => e.InvoiceBookId);

            entity.ToTable("TbPurchaseInvoiceBook");

            entity.Property(e => e.Price).HasColumnType("decimal(8, 2)");

            entity.HasOne(d => d.Book).WithMany(p => p.TbPurchaseInvoiceBooks)
                .HasForeignKey(d => d.BookId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TbPurchaseInvoiceBook_TbBooks");

            entity.HasOne(d => d.Invoice).WithMany(p => p.TbPurchaseInvoiceBooks)
                .HasForeignKey(d => d.InvoiceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TbPurchaseInvoiceBook_TbPurchaseInvoice");
        });

        modelBuilder.Entity<TbSalesInvoice>(entity =>
        {
            entity.HasKey(e => e.InvoiceId);

            entity.ToTable("TbSalesInvoice");

            entity.Property(e => e.CreatedBy).HasMaxLength(100);
            entity.Property(e => e.DeliveryState).HasMaxLength(50);
            entity.Property(e => e.InvoiceDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.TotalPrice).HasColumnType("decimal(8, 2)");
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

            entity.HasOne(d => d.CustomerDeliver).WithMany(p => p.TbSalesInvoices)
                .HasForeignKey(d => d.CustomerDeliverId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TbSalesInvoice_TbCustomerDeliverInfo");

            entity.HasOne(d => d.DeliveryMan).WithMany(p => p.TbSalesInvoices)
                .HasForeignKey(d => d.DeliveryManId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TbSalesInvoice_TbDeliveryMan");
        });

        modelBuilder.Entity<TbSalesInvoiceBook>(entity =>
        {
            entity.HasKey(e => e.InvoiceBookId);

            entity.ToTable("TbSalesInvoiceBook");

            entity.Property(e => e.Price).HasColumnType("decimal(8, 2)");

            entity.HasOne(d => d.Book).WithMany(p => p.TbSalesInvoiceBooks)
                .HasForeignKey(d => d.BookId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TbSalesInvoiceBook_TbBooks");

            entity.HasOne(d => d.Invoice).WithMany(p => p.TbSalesInvoiceBooks)
                .HasForeignKey(d => d.InvoiceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TbSalesInvoiceBook_TbSalesInvoice");
        });

        modelBuilder.Entity<TbSupplier>(entity =>
        {
            entity.HasKey(e => e.SupplierId);

            entity.Property(e => e.PhoneNumber).HasMaxLength(50);
            entity.Property(e => e.SupplierName).HasMaxLength(100);
        });

        modelBuilder.Entity<TbSlider>(entity =>
        {
            entity.HasKey(e => e.SliderId);

            entity.ToTable("TbSlider");

            entity.Property(e => e.Description).HasMaxLength(500);

            entity.Property(e => e.ImageName).HasMaxLength(200);

            entity.Property(e => e.Title).HasMaxLength(200);
        });

        modelBuilder.Entity<TbSettings>(entity =>
        {
            entity.HasKey(e => e.Id);
        });

        modelBuilder.Entity<TbPages>(entity =>
        {
            entity.HasKey(e => e.PageId);

            entity.Property(e => e.Title).HasMaxLength(500);
        });

        modelBuilder.Entity<VwBook>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VwBooks");

            entity.Property(e => e.AuthorName).HasMaxLength(100);
            entity.Property(e => e.CategoryName).HasMaxLength(100);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.ExpiryDate).HasColumnType("datetime");
            entity.Property(e => e.Isbn).HasMaxLength(50);
            entity.Property(e => e.PublishYear).HasMaxLength(10);
            entity.Property(e => e.Publisher).HasMaxLength(100);
            entity.Property(e => e.PurchasePrice).HasColumnType("decimal(8, 2)");
            entity.Property(e => e.SalesPrice).HasColumnType("decimal(8, 2)");
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<VwSalesInvoice>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VwSalesInvoice");

            entity.Property(e => e.Adress).HasMaxLength(200);
            entity.Property(e => e.CreatedBy).HasMaxLength(100);
            entity.Property(e => e.CutomerName).HasMaxLength(50);
            entity.Property(e => e.DeliveryManName).HasMaxLength(100);
            entity.Property(e => e.DeliveryPrice).HasColumnType("decimal(8, 2)");
            entity.Property(e => e.DeliveryState).HasMaxLength(50);
            entity.Property(e => e.GovernorateName).HasMaxLength(50);
            entity.Property(e => e.InvoiceDate).HasColumnType("datetime");
            entity.Property(e => e.PhoneNumber).HasMaxLength(16);
            entity.Property(e => e.TotalPrice).HasColumnType("decimal(8, 2)");
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
        });
        modelBuilder.Entity<VwSalesInvoiceBook>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VwSalesInvoiceBooks");

        });
        modelBuilder.Entity<VwPurchaseInvoiceBook>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VwPurchaseInvoiceBooks");
        });
        modelBuilder.Entity<VwPurchaseInvoice>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VwPurchaseInvoices");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
