using DATABASE.DataContext;
using DATABASE.Entityes;
using DATABASE.Intarfaces;
using DATABASE.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATABASE.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ShopContext _context;
        private UserRepository _userRepository;
        private OrderRepository _orderRepository;
        private TeaRepository _teaRepository;
        private HerbRepository _herbRepository;
        private HoneyRepository _honeyRepository;

        public UnitOfWork(ShopContext context)
        {
            _context = context;
        }

        public IRepository<User> Users
        {
            get
            {
                if(_userRepository == null)
                   _userRepository = new UserRepository(_context);
                return _userRepository;
            }
        }

        public UserRepository UsersRepo
        {
            get
            {
                if (_userRepository == null)
                    _userRepository = new UserRepository(_context);
                return _userRepository;
            }
        }

        public IRepository<Order> Orders
        {
            get
            {
                if(_orderRepository == null)
                   _orderRepository = new OrderRepository(_context);
                return _orderRepository;
            }
        }

        public IRepository<Tea> Teas
        {
            get
            {
                if(_teaRepository == null)
                   _teaRepository = new TeaRepository(_context);
                return _teaRepository;
            }
        }

        public IRepository<Herb> Herbs
        {
            get
            {
                if(_herbRepository == null)
                   _herbRepository = new HerbRepository(_context);
                return _herbRepository;
            }
        }

        public IRepository<Honey> Honey
        {
            get
            {
                if(_honeyRepository == null)
                   _honeyRepository = new HoneyRepository(_context);
                 return _honeyRepository;
            }
        }

        private bool _disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                this._disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
