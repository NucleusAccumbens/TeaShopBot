using DATABASE.DataContext;
using DATABASE.Entityes;
using DATABASE.Intarfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeaShopDAL.Repositories
{
    public class ProductRepository : IRepository<Product>
    {
        private readonly ShopContext _context;

        public ProductRepository(ShopContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Product product)
        {
            try
            {
                await _context.Products
                    .AddAsync(product);
                await _context
                    .SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task DeleteAsync(long? id)
        {
            try
            {
                var product = await _context.Products
                    .SingleAsync(p => p.ProductId == id);
                _context.Products
                    .Remove(product);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Task<IEnumerable<Product>> FindAsync(Func<Product, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            try
            {
                return await _context.Products
                    .ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Product> GetAsync(long? productId)
        {
            try
            {
                return await _context.Products
                    .Include(p => p.Orders)
                    .SingleAsync(p => p.ProductId == productId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task UpdateAsync(Product product)
        {
            try
            {
                _context.Entry(product).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
