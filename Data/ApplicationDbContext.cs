using Microsoft.EntityFrameworkCore;
using WebBanHang.Models;
using System;
using System.Collections.Generic;

namespace WebBanHang.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Voucher> Vouchers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Cấu hình kiểu dữ liệu
            modelBuilder.Entity<Product>().Property(p => p.Price).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<Order>().Property(o => o.TotalAmount).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<Order>().Property(o => o.Discount).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<OrderItem>().Property(oi => oi.UnitPrice).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<Voucher>().Property(v => v.DiscountValue).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<Voucher>().Property(v => v.MinAmount).HasColumnType("decimal(18,2)");

            // Dữ liệu mẫu cho Category
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Laptop" },
                new Category { Id = 2, Name = "Laptop Gaming" },
                new Category { Id = 3, Name = "Màn hình" },
                new Category { Id = 4, Name = "Bàn phím" },
                new Category { Id = 5, Name = "Chuột" },
                new Category { Id = 6, Name = "Tai nghe" },
                new Category { Id = 7, Name = "Bàn ghế" },
                new Category { Id = 8, Name = "Phụ kiện" }
            );

            // Dữ liệu mẫu cho Product
            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "Laptop Lenovo IdeaPad", Description = "i5-13420H/512GB/24GB", Price = 15000000m, ImageUrl = "/Image/laptop1.png", CategoryId = 1 },
                new Product { Id = 2, Name = "Laptop Avita PURA", Description = "i5-1235U/512GB/14GB", Price = 16000000m, ImageUrl = "/Image/laptop2.png", CategoryId = 1 },
                new Product { Id = 3, Name = "Laptop ASUS ExpertBook", Description = "i5-13500H/512GB/16GB", Price = 17000000m, ImageUrl = "/Image/laptop3.png", CategoryId = 1 },
                new Product { Id = 4, Name = "Laptop Lenovo ThinkBook", Description = "i5-13500H/512GB/16GB", Price = 15500000m, ImageUrl = "/Image/laptop4.png", CategoryId = 1 },
                new Product { Id = 5, Name = "Laptop ASUS Vivobook", Description = "i5-12500H/512GB/16GB", Price = 18000000m, ImageUrl = "/Image/laptop5.png", CategoryId = 1 },
                new Product { Id = 6, Name = "Laptop Dell Inspiron", Description = "i3-1305U/512GB/8GB", Price = 16500000m, ImageUrl = "/Image/laptop6.png", CategoryId = 1 },
                new Product { Id = 7, Name = "Laptop Dell XPS", Description = "i5-13420H/512GB/8GB", Price = 19000000m, ImageUrl = "/Image/laptop7.png", CategoryId = 1 },
                new Product { Id = 8, Name = "Laptop LG Gram", Description = "Ultra-7-155H/512GB/16GB", Price = 17500000m, ImageUrl = "/Image/laptop8.png", CategoryId = 1 },
                new Product { Id = 9, Name = "Laptop MSI Prestige", Description = "Ultra-5-125H/512GB/16GB", Price = 20000000m, ImageUrl = "/Image/laptop9.png", CategoryId = 1 },
                new Product { Id = 10, Name = "Laptop Lenovo Yoga", Description = "Ultra-7-258V/512GB/1TB", Price = 21000000m, ImageUrl = "/Image/laptop10.png", CategoryId = 1 },
                new Product { Id = 11, Name = "Laptop Gaming 1", Description = "Laptop gaming cấu hình khủng 1", Price = 22000000m, ImageUrl = "/images/gaming1.jpg", CategoryId = 2 },
                new Product { Id = 12, Name = "Laptop Gaming 2", Description = "Laptop gaming cấu hình khủng 2", Price = 23000000m, ImageUrl = "/images/gaming2.jpg", CategoryId = 2 },
                new Product { Id = 13, Name = "Laptop Gaming 3", Description = "Laptop gaming cấu hình khủng 3", Price = 24000000m, ImageUrl = "/images/gaming3.jpg", CategoryId = 2 },
                new Product { Id = 14, Name = "Laptop Gaming 4", Description = "Laptop gaming cấu hình khủng 4", Price = 25000000m, ImageUrl = "/images/gaming4.jpg", CategoryId = 2 },
                new Product { Id = 15, Name = "Laptop Gaming 5", Description = "Laptop gaming cấu hình khủng 5", Price = 26000000m, ImageUrl = "/images/gaming5.jpg", CategoryId = 2 },
                new Product { Id = 16, Name = "Laptop Gaming 6", Description = "Laptop gaming cấu hình khủng 6", Price = 27000000m, ImageUrl = "/images/gaming6.jpg", CategoryId = 2 },
                new Product { Id = 17, Name = "Laptop Gaming 7", Description = "Laptop gaming cấu hình khủng 7", Price = 28000000m, ImageUrl = "/images/gaming7.jpg", CategoryId = 2 },
                new Product { Id = 18, Name = "Laptop Gaming 8", Description = "Laptop gaming cấu hình khủng 8", Price = 29000000m, ImageUrl = "/images/gaming8.jpg", CategoryId = 2 },
                new Product { Id = 19, Name = "Laptop Gaming 9", Description = "Laptop gaming cấu hình khủng 9", Price = 30000000m, ImageUrl = "/images/gaming9.jpg", CategoryId = 2 },
                new Product { Id = 20, Name = "Laptop Gaming 10", Description = "Laptop gaming cấu hình khủng 10", Price = 31000000m, ImageUrl = "/images/gaming10.jpg", CategoryId = 2 },
                new Product { Id = 21, Name = "Màn hình 1", Description = "Màn hình sắc nét 1", Price = 4000000m, ImageUrl = "/images/monitor1.jpg", CategoryId = 3 },
                new Product { Id = 22, Name = "Màn hình 2", Description = "Màn hình sắc nét 2", Price = 4200000m, ImageUrl = "/images/monitor2.jpg", CategoryId = 3 },
                new Product { Id = 23, Name = "Màn hình 3", Description = "Màn hình sắc nét 3", Price = 4300000m, ImageUrl = "/images/monitor3.jpg", CategoryId = 3 },
                new Product { Id = 24, Name = "Màn hình 4", Description = "Màn hình sắc nét 4", Price = 4400000m, ImageUrl = "/images/monitor4.jpg", CategoryId = 3 },
                new Product { Id = 25, Name = "Màn hình 5", Description = "Màn hình sắc nét 5", Price = 4600000m, ImageUrl = "/images/monitor5.jpg", CategoryId = 3 },
                new Product { Id = 26, Name = "Màn hình 6", Description = "Màn hình sắc nét 6", Price = 4700000m, ImageUrl = "/images/monitor6.jpg", CategoryId = 3 },
                new Product { Id = 27, Name = "Màn hình 7", Description = "Màn hình sắc nét 7", Price = 4500000m, ImageUrl = "/images/monitor7.jpg", CategoryId = 3 },
                new Product { Id = 28, Name = "Màn hình 8", Description = "Màn hình sắc nét 8", Price = 4800000m, ImageUrl = "/images/monitor8.jpg", CategoryId = 3 },
                new Product { Id = 29, Name = "Màn hình 9", Description = "Màn hình sắc nét 9", Price = 4900000m, ImageUrl = "/images/monitor9.jpg", CategoryId = 3 },
                new Product { Id = 30, Name = "Màn hình 10", Description = "Màn hình sắc nét 10", Price = 5000000m, ImageUrl = "/images/monitor10.jpg", CategoryId = 3 },
                new Product { Id = 31, Name = "Bàn phím 1", Description = "Bàn phím cơ học 1", Price = 800000m, ImageUrl = "/images/keyboard1.jpg", CategoryId = 4 },
                new Product { Id = 32, Name = "Bàn phím 2", Description = "Bàn phím cơ học 2", Price = 850000m, ImageUrl = "/images/keyboard2.jpg", CategoryId = 4 },
                new Product { Id = 33, Name = "Bàn phím 3", Description = "Bàn phím cơ học 3", Price = 820000m, ImageUrl = "/images/keyboard3.jpg", CategoryId = 4 },
                new Product { Id = 34, Name = "Bàn phím 4", Description = "Bàn phím cơ học 4", Price = 880000m, ImageUrl = "/images/keyboard4.jpg", CategoryId = 4 },
                new Product { Id = 35, Name = "Bàn phím 5", Description = "Bàn phím cơ học 5", Price = 900000m, ImageUrl = "/images/keyboard5.jpg", CategoryId = 4 },
                new Product { Id = 36, Name = "Bàn phím 6", Description = "Bàn phím cơ học 6", Price = 920000m, ImageUrl = "/images/keyboard6.jpg", CategoryId = 4 },
                new Product { Id = 37, Name = "Bàn phím 7", Description = "Bàn phím cơ học 7", Price = 950000m, ImageUrl = "/images/keyboard7.jpg", CategoryId = 4 },
                new Product { Id = 38, Name = "Bàn phím 8", Description = "Bàn phím cơ học 8", Price = 970000m, ImageUrl = "/images/keyboard8.jpg", CategoryId = 4 },
                new Product { Id = 39, Name = "Bàn phím 9", Description = "Bàn phím cơ học 9", Price = 990000m, ImageUrl = "/images/keyboard9.jpg", CategoryId = 4 },
                new Product { Id = 40, Name = "Bàn phím 10", Description = "Bàn phím cơ học 10", Price = 1000000m, ImageUrl = "/images/keyboard10.jpg", CategoryId = 4 }
            );

            // Dữ liệu mẫu cho Voucher
            modelBuilder.Entity<Voucher>().HasData(
                new Voucher
                {
                    Id = 1,
                    Code = "SALE25",
                    DiscountType = DiscountType.Percentage,
                    DiscountValue = 25m,
                    MinAmount = 200000m,
                    ExpiryDate = new DateTime(2025, 12, 31, 23, 59, 59),
                    IsActive = true
                },
                new Voucher
                {
                    Id = 2,
                    Code = "WELCOME50K",
                    DiscountType = DiscountType.FixedAmount,
                    DiscountValue = 50000m,
                    MinAmount = 300000m,
                    ExpiryDate = new DateTime(2025, 10, 11, 23, 59, 59),
                    IsActive = true
                }
            );
        }
    }
}