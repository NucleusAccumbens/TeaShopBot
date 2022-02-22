using DATABASE.Entityes;
using DATABASE.Intarfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DATABASE.DataContext;
using Microsoft.EntityFrameworkCore;

namespace DATABASE.Repositories
{
    public class UserRepository : IRepository<User>
    {
        private readonly ShopContext _context;

        public UserRepository(ShopContext context)
        {
            this._context = context;
        }
        public async Task CreateAsync(User user)
        {
            try
            {
                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task DeleteAsync(long? userId)
        {
            try
            {
                var user = await _context.Users
                    .SingleAsync(user => user.UserId == userId);
                _context.Remove(user);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task DeleteAppUserByChatIdAsync(long chatId)
        {
            try
            {
                var user = await _context.Users
                    .FirstAsync(user => user.ChatId == chatId);
                _context.Remove(user);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Task<IEnumerable<User>> FindAsync(Func<User, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetAsync(long? userId)
        {
            try
            {
                return await _context.Users
                    .SingleAsync(user => user.UserId == userId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<User> GetUserByChatIdAsync(long chatId)
        {
            try
            {
                return await _context.Users
                    .FirstAsync(user => user.ChatId == chatId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            try
            {
                return await _context.Users
                    .ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task UpdateAsync(User user)
        {
            try
            {
                _context.Entry(user).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
