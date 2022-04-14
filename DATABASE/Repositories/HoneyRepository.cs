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
    public class HoneyRepository : IRepository<Honey>
    {
        private readonly ShopContext _context;

        public HoneyRepository(ShopContext context)
        {
            this._context = context;
        }

        public async Task CreateAsync(Honey honey)
        {
            try
            {
                await _context.Honey.AddAsync(honey);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task DeleteAsync(long? honeyId)
        {
            try
            {
                var honey = await _context.Honey
                    .SingleAsync(honey => honey.ProductId == honeyId);
                _context.Honey.Remove(honey);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task DeleteHoneyByNameAsync(string honeyName)
        {
            try
            {
                var honey = await _context.Honey
                    .SingleAsync(honey => honey.ProductName == honeyName);
                _context.Honey.Remove(honey);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Honey> GetAsync(long? honeyId)
        {
            try
            {
                return await _context.Honey
                    .SingleAsync(honey => honey.ProductId == honeyId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Honey> GetHerbByNameAsync(string honeyName)
        {
            try
            {
                return await _context.Honey
                    .SingleAsync(honey => honey.ProductName == honeyName);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Honey>> GetAllAsync()
        {

            try
            {
                return await _context.Honey.ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task UpdateAsync(Honey honey)
        {
            try
            {
                _context.Entry(honey).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
