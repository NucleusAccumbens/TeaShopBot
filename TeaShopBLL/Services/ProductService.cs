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

        public async Task CreateAsync(ProductDTO productDto)
        {
            try
            {
                var config = new MapperConfiguration(cfg => {
                    cfg.CreateMap<OrderDTO, Order>();
                    cfg.CreateMap<ProductDTO, Product>()
                    .Include<TeaDTO, Tea>()
                    .Include<HerbDTO, Herb>()
                    .Include<HoneyDTO, Honey>();
                    cfg.CreateMap<TeaDTO, Tea>();
                    cfg.CreateMap<HerbDTO, Herb>();
                    cfg.CreateMap<HoneyDTO, Honey>();
                });
                var mapper = config.CreateMapper();

                var product = mapper.Map<Product>(productDto);
                await _repo.Products.CreateAsync(product);
                await _repo.SaveAsync();

            }
            catch (Exception)
            {
                throw;
            };
        }

        public async Task DeleteAsync(long itemId)
        {
            await _repo.Products.DeleteAsync(itemId);
            await _repo.SaveAsync();
        }

        public async Task<List<ProductDTO>> GetAllAsync()
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

                var productsDto = new List<ProductDTO>();
                var allProducts = await _repo.Products.GetAllAsync();
                foreach (var product in allProducts)
                {
                    var productDto = mapper.Map<ProductDTO>(product);
                    productsDto.Add(productDto);
                }
                return productsDto;
            }
            catch (Exception)
            {
                throw;
            };
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

        public async Task UpdateAsync(ProductDTO productDto)
        {
            try
            {
                var config = new MapperConfiguration(cfg => {
                    cfg.CreateMap<OrderDTO, Order>();
                    cfg.CreateMap<ProductDTO, Product>()
                    .Include<TeaDTO, Tea>()
                    .Include<HerbDTO, Herb>()
                    .Include<HoneyDTO, Honey>();
                    cfg.CreateMap<TeaDTO, Tea>();
                    cfg.CreateMap<HerbDTO, Herb>();
                    cfg.CreateMap<HoneyDTO, Honey>();
                });
                var mapper = config.CreateMapper();

                var product = mapper.Map<Product>(productDto);
                await _repo.Products.UpdateAsync(product);
                await _repo.SaveAsync();

            }
            catch (Exception)
            {
                throw;
            };
        }
    }
}
