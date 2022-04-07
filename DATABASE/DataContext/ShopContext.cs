using DATABASE.Entityes;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DATABASE.DataContext
{
    public class ShopContext : DbContext
    {
        private readonly string _connectionString =
            "Host=ec2-54-77-182-219.eu-west-1.compute.amazonaws.com;" +
            "Port=5432;" +
            "Database=d7ogg57688h1bb;" +
            "Username=vogndulbxckdox;" +
            "Password=8c2e136947db3b10a8a22150cc309d674a88ef16525134372f0c64c140c173d5;" +
            "Pooling=true;" +
            "SSL Mode=Require;" +
            "TrusrServerSertificate=true;";

        public ShopContext() 
            : base () 
        {
            //Database.EnsureDeleted();   // удаляем бд со старой схемой
            //Database.EnsureCreated();
        }

        private readonly StreamWriter _logStream = new("mylog.txt", true);

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_connectionString);
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
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Tea> Teas { get; set; }
        public DbSet<Herb> Herbs { get; set; }
        public DbSet<Honey> Honey { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}
