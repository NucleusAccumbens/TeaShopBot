using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeaShopBLL.DTO;

namespace TeaShopBLL.Interfaces
{
    public interface IService<T> where T : class
    {
        Task CreateAsync(T item);
        Task UpdateAsync(T item);
        Task DeleteAsync(long itemId);
        Task<T> GetAsync(long itemtId);
        Task<List<T>> GetAllAsync();
    }
}
