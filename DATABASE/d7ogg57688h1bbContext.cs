using System;
using System.Collections.Generic;
using DATABASE.Entityes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TeaShopDAL
{
    public partial class d7ogg57688h1bbContext : DbContext
    {
        private readonly string _connectionString =
                        "Host=ec2-54-77-182-219.eu-west-1.compute.amazonaws.com;" +
                        "Port=5432;" +
                        "Database=d7ogg57688h1bb;" +
                        "Username=vogndulbxckdox;" +
                        "Password=8c2e136947db3b10a8a22150cc309d674a88ef16525134372f0c64c140c173d5;" +
                        "Pooling=true;" +
                        "SSL Mode=Require;" +
                        "Trust Server Certificate=True";
        public d7ogg57688h1bbContext()
        {
        }

        public d7ogg57688h1bbContext(DbContextOptions<d7ogg57688h1bbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql(_connectionString);
            }
        }

        public override void Dispose()
        {
            base.Dispose();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            OnModelCreatingPartial(modelBuilder);

            modelBuilder.Entity<User>().HasData(new User[]
            {
                new User
                {
                    UserId = 1,
                    ChatId = 444343256,
                    Username = "noncredistka",
                    Firstname = "nucleus accumbens",
                    IsAdmin = true
                },
                new User
                {
                    UserId = 2,
                    ChatId = 519140043,
                    Username = "shanti_travels",
                    Firstname = "Алексей Курлян",
                    IsAdmin = true
                }
            });

            modelBuilder.Entity<Product>()
                .HasMany(c => c.Orders)
                .WithMany(c => c.Products);

            modelBuilder.Entity<Order>()
                .HasMany(c => c.Products)
                .WithMany(c => c.Orders);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Tea> Teas { get; set; }
        public DbSet<Herb> Herbs { get; set; }
        public DbSet<Honey> Honey { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}
