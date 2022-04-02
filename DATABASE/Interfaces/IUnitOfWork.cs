using DATABASE.Entityes;
using DATABASE.Intarfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATABASE.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<User> Users { get; }
        IRepository<Order> Orders { get; }
        IRepository<Tea> Teas { get; }
        IRepository<Herb> Herbs { get; }
        IRepository<Honey> Honey { get; }
        IRepository<Product> Products { get; }
        Task SaveAsync(); 
    }
}
