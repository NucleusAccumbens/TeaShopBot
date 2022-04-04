using DATABASE.Entityes;
using DATABASE.Interfaces;
using Mapster;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeaShopBLL.DTO;
using TeaShopBLL.Interfaces;
using DATABASE.Repositories;

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
                var config = new MapperConfiguration(cfg => {
                    cfg.CreateMap<ProductDTO, Product>()
                    .Include<TeaDTO, Tea>()
                    .Include<HerbDTO, Herb>()
                    .Include<HoneyDTO, Honey>();
                    cfg.CreateMap<TeaDTO, Tea>();
                    cfg.CreateMap<HerbDTO, Herb>();
                    cfg.CreateMap<HoneyDTO, Honey>();
                    cfg.CreateMap<OrderDTO, Order>();
                });            
                var mapper = config.CreateMapper();
                var _order = mapper.Map<Order>(order);

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

        public async Task<string> DeleteProductFromOrderAsync(long? userChatId, long productId)
        {
            try
            {
                var config = new MapperConfiguration(cfg => {
                    cfg.CreateMap<ProductDTO, Product>()
                    .Include<TeaDTO, Tea>()
                    .Include<HerbDTO, Herb>()
                    .Include<HoneyDTO, Honey>();
                    cfg.CreateMap<TeaDTO, Tea>();
                    cfg.CreateMap<HerbDTO, Herb>();
                    cfg.CreateMap<HoneyDTO, Honey>();
                    cfg.CreateMap<OrderDTO, Order>();
                });
                var mapper = config.CreateMapper();

                string productName = await (_repo.Orders as OrderRepository).DeleteProductFromOrderAsync(userChatId, productId);
                await _repo.SaveAsync();
                return productName;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<OrderDTO>> GetAllAsync()
        {
            try
            {
                var config = new MapperConfiguration(cfg => {
                    cfg.CreateMap<Product, ProductDTO>()
                    .Include<Tea, TeaDTO>()
                    .Include<Herb, HerbDTO>()
                    .Include<Honey, HoneyDTO>();
                    cfg.CreateMap<Tea, TeaDTO>();
                    cfg.CreateMap<Herb, HerbDTO>();
                    cfg.CreateMap<Honey, HoneyDTO>();
                    cfg.CreateMap<Order, OrderDTO>();
                });
                var mapper = config.CreateMapper();

                var orders = await _repo.Orders.GetAllAsync();
                var orderList = new List<OrderDTO>();
                foreach (var order in orders)
                {
                    var orderDto = mapper.Map<OrderDTO>(order);
                    orderList.Add(orderDto);
                }               
                return orderList;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<OrderDTO>> GetAllUserOrdersAsync(long chatId)
        {
            try
            {
                var config = new MapperConfiguration(cfg => {
                    cfg.CreateMap<Product, ProductDTO>()
                    .Include<Tea, TeaDTO>()
                    .Include<Herb, HerbDTO>()
                    .Include<Honey, HoneyDTO>();
                    cfg.CreateMap<Tea, TeaDTO>();
                    cfg.CreateMap<Herb, HerbDTO>();
                    cfg.CreateMap<Honey, HoneyDTO>();
                    cfg.CreateMap<Order, OrderDTO>();
                });
                var mapper = config.CreateMapper();

                var orders = await _repo.Orders.GetAllAsync();
                var orderList = new List<OrderDTO>();
                foreach (var order in orders)
                {
                    if (order.UserChatId == chatId)
                    {
                        var orderDto = mapper.Map<OrderDTO>(order);
                        orderList.Add(orderDto);
                    }                    
                }
                return orderList;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<OrderDTO?> GetActiveOrderAsync(long userChatId)
        {
            try
            {
                var config = new MapperConfiguration(cfg => {
                    cfg.CreateMap<Product, ProductDTO>()
                    .Include<Tea, TeaDTO>()
                    .Include<Herb, HerbDTO>()
                    .Include<Honey, HoneyDTO>();
                    cfg.CreateMap<Tea, TeaDTO>();
                    cfg.CreateMap<Herb, HerbDTO>();
                    cfg.CreateMap<Honey, HoneyDTO>();
                    cfg.CreateMap<Order, OrderDTO>();
                });
                var mapper = config.CreateMapper();

                var order = await (_repo.Orders as OrderRepository).GetActiveOrderAsync(userChatId);
                if (order != null)
                {
                    var orderDto = mapper.Map<OrderDTO>(order);
                    return orderDto;
                }
                else return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<OrderDTO> GetAsync(long userChatId)
        {
            try
            {
                var config = new MapperConfiguration(cfg => {
                    cfg.CreateMap<Product, ProductDTO>()
                    .Include<Tea, TeaDTO>()
                    .Include<Herb, HerbDTO>()
                    .Include<Honey, HoneyDTO>();
                    cfg.CreateMap<Tea, TeaDTO>();
                    cfg.CreateMap<Herb, HerbDTO>();
                    cfg.CreateMap<Honey, HoneyDTO>();
                    cfg.CreateMap<Order, OrderDTO>();
                });
                var mapper = config.CreateMapper();

                var order = await _repo.Orders.GetAsync(userChatId);
                var orderDto = mapper.Map<OrderDTO>(order);
                return orderDto;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<OrderDTO> GetByOrderIdAsync(long orderId)
        {
            try
            {
                var config = new MapperConfiguration(cfg => {
                    cfg.CreateMap<Product, ProductDTO>()
                    .Include<Tea, TeaDTO>()
                    .Include<Herb, HerbDTO>()
                    .Include<Honey, HoneyDTO>();
                    cfg.CreateMap<Tea, TeaDTO>();
                    cfg.CreateMap<Herb, HerbDTO>();
                    cfg.CreateMap<Honey, HoneyDTO>();
                    cfg.CreateMap<Order, OrderDTO>();
                });
                var mapper = config.CreateMapper();

                var order = await (_repo.Orders as OrderRepository).GetByOrderIdAsync(orderId);
                var orderDto = mapper.Map<OrderDTO>(order);
                return orderDto;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task UpdateAsync(OrderDTO order)
        {
            try
            {
                var config = new MapperConfiguration(cfg => {
                    cfg.CreateMap<ProductDTO, Product>()
                    .Include<TeaDTO, Tea>()
                    .Include<HerbDTO, Herb>()
                    .Include<HoneyDTO, Honey>();
                    cfg.CreateMap<TeaDTO, Tea>();
                    cfg.CreateMap<HerbDTO, Herb>();
                    cfg.CreateMap<HoneyDTO, Honey>();
                    cfg.CreateMap<OrderDTO, Order>();
                });
                var mapper = config.CreateMapper();
                var _order = mapper.Map<Order>(order);

                await _repo.Orders.UpdateAsync(_order);
                await _repo.SaveAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
