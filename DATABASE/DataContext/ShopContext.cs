using DATABASE.Entityes;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DATABASE.DataContext
{
    public class ShopContext : DbContext
    {
        private readonly string _connectionString =
            "Data Source=User;Initial Catalog=TeaShopLocal;Integrated Security=True";

        public ShopContext() 
            : base () 
        {
            //Database.EnsureDeleted();   // удаляем бд со старой схемой
            //Database.EnsureCreated();
        }

        private readonly StreamWriter _logStream = new("mylog.txt", true);

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
            optionsBuilder.LogTo(_logStream.WriteLine, LogLevel.Trace);  // Логгирование 
        }

        public override void Dispose()
        {
            base.Dispose();
            _logStream.Dispose();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Предзаполнение БД - обязательно проинициализировать первичный ключ (Id)
            modelBuilder.Entity<User>().HasData(new User[]
            {
                new User
                {
                    UserId = 1,
                    ChatId = 444343256,
                    Username = "noncredistka",
                    IsAdmin = true
                },
                new User
                {
                    UserId = 2,
                    ChatId = 519140043,
                    Username = "shanti_travels",
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
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Tea> Teas { get; set; }
        public DbSet<Herb> Herbs { get; set; }
        public DbSet<Honey> Honey { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}
