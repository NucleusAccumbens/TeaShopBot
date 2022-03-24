using DATABASE.Entityes;
using DATABASE.Interfaces;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeaShopBLL.DTO;
using TeaShopBLL.Interfaces;

namespace TeaShopBLL.Services
{
    public class OrderService : IService<OrderDTO>
    {
        private readonly IUnitOfWork _repo;

        public OrderService(IUnitOfWork repo)
        {
            _repo = repo;
        }

        public async Task CreateAsync(OrderDTO order)
        {
            try
            {
                var _order = order.Adapt<Order>();               
                await _repo.Orders.CreateAsync(_order);
                await _repo.SaveAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Task DeleteAsync(long itemId)
        {
            throw new NotImplementedException();
        }

        public Task<List<OrderDTO>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<OrderDTO> GetAsync(long itemtId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(OrderDTO item)
        {
            throw new NotImplementedException();
        }
    }
}
