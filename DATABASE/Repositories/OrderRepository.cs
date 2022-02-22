using DATABASE.DataContext;
using DATABASE.Entityes;
using DATABASE.Intarfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATABASE.Repositories
{
    public class OrderRepository : IRepository<Order>
    {
        private readonly ShopContext _context;

        public OrderRepository(ShopContext context)
        {
            this._context = context;
        }

        public async Task CreateAsync(Order order)
        {
            try
            {
                await _context.Orders.AddAsync(order);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task DeleteAsync(long? orderId)
        {
            try
            {
                var order = await _context.Orders
                    .SingleAsync(order => order.OrderId == orderId);
                _context.Orders.Remove(order);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Task<IEnumerable<Order>> FindAsync(Func<Order, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public async Task<Order> GetAsync(long? orderId)
        {
            try
            {
                return await _context.Orders
                    .SingleAsync(order => order.OrderId == orderId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            try
            {
                return await _context.Orders
                    .ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task UpdateAsync(Order order)
        {
            try
            {
                _context.Entry(order).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            };
        }
    }
}
