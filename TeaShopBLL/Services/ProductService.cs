using AutoMapper;
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
    public class ProductService : IService<ProductDTO>
    {
        private readonly IUnitOfWork _repo;

        public ProductService(IUnitOfWork repo)
        {
            _repo = repo;
        }

        public Task CreateAsync(ProductDTO item)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(long itemId)
        {
            throw new NotImplementedException();
        }

        public Task<List<ProductDTO>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<ProductDTO> GetAsync(long productId)
        {
            try
            {
                var config = new MapperConfiguration(cfg => {
                    cfg.CreateMap<Order, OrderDTO>();
                    cfg.CreateMap<Product, ProductDTO>()
                    .Include<Tea, TeaDTO>()
                    .Include<Herb, HerbDTO>()
                    .Include<Honey, HoneyDTO>();
                    cfg.CreateMap<Tea, TeaDTO>();
                    cfg.CreateMap<Herb, HerbDTO>();
                    cfg.CreateMap<Honey, HoneyDTO>();
                });
                var mapper = config.CreateMapper();

                var product = await _repo.Products.GetAsync(productId);
                var productDTO = mapper.Map<ProductDTO>(product);
                return productDTO;
            }
            catch (Exception)
            {
                throw;
            };
        }

        public Task UpdateAsync(ProductDTO item)
        {
            throw new NotImplementedException();
        }
    }
}
