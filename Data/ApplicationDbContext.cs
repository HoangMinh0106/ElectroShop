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
        public DbSet<Review> Reviews { get; set; }
        
        public DbSet<Notification> Notifications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Product>().Property(p => p.Price).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<Order>().Property(o => o.TotalAmount).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<Order>().Property(o => o.Discount).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<OrderItem>().Property(oi => oi.UnitPrice).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<Voucher>().Property(v => v.DiscountValue).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<Voucher>().Property(v => v.MinAmount).HasColumnType("decimal(18,2)");

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


            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "Laptop Lenovo IdeaPad", Description = "i5-13420H/512GB/24GB", Price = 15000000m, ImageUrl = "/Image/laptop1.png", CategoryId = 1 },
                new Product { Id = 2, Name = "Laptop Avita PURA", Description = "i5-1235U/512GB/14GB", Price = 16000000m, ImageUrl = "/Image/laptop2.png", CategoryId = 1 },
                new Product { Id = 4, Name = "Laptop Lenovo ThinkBook", Description = "i5-13500H/512GB/16GB", Price = 15500000m, ImageUrl = "/Image/laptop4.png", CategoryId = 1 },
                new Product { Id = 7, Name = "Laptop Dell XPS", Description = "i5-13420H/512GB/8GB", Price = 19000000m, ImageUrl = "/Image/laptop7.png", CategoryId = 1 },
                new Product { Id = 9, Name = "Laptop MSI Prestige", Description = "Ultra-5-125H/512GB/16GB", Price = 20000000m, ImageUrl = "/Image/laptop9.png", CategoryId = 1 },
                new Product { Id = 10, Name = "Laptop Lenovo Yoga", Description = "Ultra-7-258V/512GB/16GB", Price = 21000000m, ImageUrl = "/Image/laptop10.png", CategoryId = 1 },
                new Product { Id = 11, Name = "Laptop gaming HP Victus 16", Description = "i7-13700HX/512GB/16GB", Price = 22000000m, ImageUrl = "/Image/gaming1.png", CategoryId = 2 },
                new Product { Id = 12, Name = "Laptop gaming Lenovo LOQ 15ARP9", Description = "R5-7235HS/512GB/24GB", Price = 23000000m, ImageUrl = "/Image/gaming2.png", CategoryId = 2 },
                new Product { Id = 13, Name = "Laptop gaming Acer Nitro V ", Description = "i5-13420H/512GB/32GB", Price = 24000000m, ImageUrl = "/Image/gaming3.png", CategoryId = 2 },
                new Product { Id = 14, Name = "Laptop gaming Lenovo LOQ 15IAX9E", Description = "i5-12450HX/512GB/16GB", Price = 25000000m, ImageUrl = "/Image/gaming4.png", CategoryId = 2 },
                new Product { Id = 15, Name = "Laptop gaming MSI Thin 15", Description = "i5-13420H/512GB/16GB", Price = 26000000m, ImageUrl = "/Image/gaming5.png", CategoryId = 2 },
                new Product { Id = 16, Name = "Laptop gaming Gigabyte G5", Description = "i7-13620H/512GB/16GB", Price = 27000000m, ImageUrl = "/Image/gaming6.png", CategoryId = 2 },
                new Product { Id = 17, Name = "Laptop gaming MSI Katana 15", Description = "i7-13620H/512GB/8GB", Price = 28000000m, ImageUrl = "/Image/gaming7.png", CategoryId = 2 },
                new Product { Id = 21, Name = "Màn hình Asus TUF GAMING", Description = "25inch/180Hz", Price = 4000000m, ImageUrl = "/Image/monitor1.png", CategoryId = 3 },
                new Product { Id = 22, Name = "Màn hình ViewSonic VA2215-H", Description = "22inch/100Hz", Price = 4200000m, ImageUrl = "/Image/monitor2.png", CategoryId = 3 },
                new Product { Id = 23, Name = "Màn hình Acer KG240Y-X1", Description = "23inch/200Hz", Price = 4300000m, ImageUrl = "/Image/monitor3.png", CategoryId = 3 },
                new Product { Id = 24, Name = "Màn hình LG 27GR75Q-B", Description = "27inch/165Hz", Price = 4400000m, ImageUrl = "/Image/monitor4.png", CategoryId = 3 },
                new Product { Id = 31, Name = "Bàn phím AULA HERO68HE", Description = "Hồng gradient/Black King magnetic switch", Price = 900000m, ImageUrl = "/Image/keyboard1.png", CategoryId = 4 },
                new Product { Id = 32, Name = "Bàn phím AKKO 3098B", Description = "Plus Black & Gold Fairy (Silent) switch", Price = 850000m, ImageUrl = "/Image/keyboard2.png", CategoryId = 4 },
                new Product { Id = 33, Name = "Bàn phím Leobog AMG65 TM", Description = "Trắng/Light feather switch", Price = 820000m, ImageUrl = "/Image/keyboard3.png", CategoryId = 4 },
                new Product { Id = 34, Name = "Bàn phím Keychron B1P", Description = "Space Gray Scissor", Price = 880000m, ImageUrl = "/Image/keyboard4.png", CategoryId = 4 },
                new Product { Id = 35, Name = "Chuột ASUS ROG Strix Impact", Description = "Không dây/Tốc độ cao", Price = 1090000m, ImageUrl = "/Image/mouse1.png", CategoryId = 5 },
                new Product { Id = 36, Name = "Chuột Pulsar ", Description = "Có dây X2H Medium White", Price = 1249000m, ImageUrl = "/Image/mouse1.png", CategoryId = 5 },
                new Product { Id = 37, Name = "Chuột DareU EM901X", Description = "RGB Superlight Wireless Pink", Price = 680000m, ImageUrl = "/Image/mouse3.png", CategoryId = 5 },
                new Product { Id = 38, Name = "Chuột Razer DeathAdder Essential ", Description = "RZ01-03850100-R3M1", Price = 400000m, ImageUrl = "/Image/mouse4.png", CategoryId = 5 }
            );



            modelBuilder.Entity<Voucher>().HasData(
                new Voucher
                {
                    Id = 1,
                    Code = "SALE25",
                    DiscountType = DiscountType.Percentage,
                    DiscountValue = 25m,
                    MinAmount = 200000m,
                    ExpiryDate = new DateTime(2025, 12, 31, 23, 59, 59),
                    IsActive = false,
                    IsTemplate = true
                },
                new Voucher
                {
                    Id = 2,
                    Code = "WELCOME50K",
                    DiscountType = DiscountType.FixedAmount,
                    DiscountValue = 50000m,
                    MinAmount = 300000m,
                    ExpiryDate = new DateTime(2025, 10, 11, 23, 59, 59),
                    IsActive = true, // Các thuộc tính còn lại sẽ dùng giá trị mặc định (false)
                    IsTemplate = false
                }
            );
        }
    }
}