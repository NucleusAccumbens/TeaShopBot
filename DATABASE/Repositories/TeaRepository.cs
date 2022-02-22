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
    public class TeaRepository : IRepository<Tea>
    {
        private readonly ShopContext _context;

        public TeaRepository(ShopContext context)
        {
            _context = context;
        }
        public async Task CreateAsync(Tea tea)
        {
            try
            {
                 await _context.Teas.AddAsync(tea);
                 await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task DeleteAsync(long? teaId)
        {
            try
            {
                var tea = await _context.Teas
                    .SingleAsync(tea => tea.ProductId == teaId);
                _context.Remove(tea);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task DeleteTeaByNameAsync(string teaName)
        {
            try
            {
                var tea = await _context.Teas
                    .FirstAsync(tea => tea.ProductName == teaName);
                _context.Remove(tea);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Task<IEnumerable<Tea>> FindAsync(Func<Tea, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public async Task<Tea> GetAsync(long? teaId)
        {
            try
            {
                return await _context.Teas
                    .SingleAsync(tea => tea.ProductId == teaId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Tea> GetTeaByNameAsync(string teaName)
        {
            try
            {
                return await _context.Teas
                    .FirstAsync(tea => tea.ProductName == teaName);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Tea>> GetAllAsync()
        {

            try
            {
                return await _context.Teas.ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task UpdateAsync(Tea tea)
        {
            try
            {
                _context.Entry(tea).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
