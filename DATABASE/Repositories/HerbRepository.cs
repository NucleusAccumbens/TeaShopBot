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
    public class HerbRepository : IRepository<Herb>
    {
        private readonly ShopContext _context;

        public HerbRepository(ShopContext context)
        {
            this._context = context;
        }

        public async Task CreateAsync(Herb herb)
        {
            try
            {
                await _context.Herbs.AddAsync(herb);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task DeleteAsync(long? herbId)
        {
            try
            {
                var herb = await _context.Herbs
                    .SingleAsync(herb => herb.ProductId == herbId);
                _context.Herbs.Remove(herb);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task DeleteHerbByNameAsync(string herbName)
        {
            try
            {
                var herb = await _context.Herbs
                    .SingleAsync(herb => herb.ProductName == herbName);
                _context.Herbs.Remove(herb);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Task<IEnumerable<Herb>> FindAsync(Func<Herb, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public async Task<Herb> GetAsync(long? herbId)
        {
            try
            {
                return await _context.Herbs
                    .SingleAsync(herb => herb.ProductId == herbId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Herb> GetHerbByNameAsync(string herbName)
        {
            try
            {
                return await _context.Herbs
                    .SingleAsync(herb => herb.ProductName == herbName);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Herb>> GetAllAsync()
        {

            try
            {
                return await _context.Herbs.ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task UpdateAsync(Herb herb)
        {
            try
            {
                _context.Entry(herb).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
