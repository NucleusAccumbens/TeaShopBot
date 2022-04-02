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

        public Task CreateAsync(Product item)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(long? id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Product>> FindAsync(Func<Product, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Product>> GetAllAsync()
        {
            throw new NotImplementedException();
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

        public Task UpdateAsync(Product item)
        {
            throw new NotImplementedException();
        }
    }
}
