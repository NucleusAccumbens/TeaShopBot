using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATABASE.Intarfaces
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetAsync(long? id);
        Task<IEnumerable<T>> FindAsync(Func<T, Boolean> predicate);
        Task CreateAsync(T item);
        Task UpdateAsync(T item);
        Task DeleteAsync(long? id);
    }
}
