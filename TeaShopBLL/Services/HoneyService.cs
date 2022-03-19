using DATABASE.Entityes;
using DATABASE.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeaShopBLL.DTO;
using TeaShopBLL.Interfaces;

namespace TeaShopBLL.Services
{
    public class HoneyService : IService<HoneyDTO>
    {
        private readonly IUnitOfWork _repo;

        public HoneyService(IUnitOfWork repo)
        {
            _repo = repo;
        }

        public async Task CreateAsync(HoneyDTO honey)
        {
            try
            {
                var _honey = new Honey()
                {
                    ProductName = honey.ProductName,
                    ProductDescription = honey.ProductDescription,
                    ProductCount = honey.ProductCount,
                    ProductPrice = honey.ProductPrice,
                    ProductPathToImage = honey.ProductPathToImage,
                    InStock = honey.InStock,
                    HoneyWeight = honey.HoneyWeight
                };

                await _repo.Honey.CreateAsync(_honey);
                await _repo.SaveAsync();
            }
            catch(Exception)
            {
                throw;
            }
        }

        public async Task DeleteAsync(long productId)
        {
            try
            {
                await _repo.Honey.DeleteAsync(productId);
                await _repo.SaveAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<HoneyDTO>> GetAllAsync()
        {
            try
            {
                var res = new List<HoneyDTO>();
                var allHoney = await _repo.Honey.GetAllAsync();
                foreach(var honey in allHoney)
                {
                    var _honeyDTO = new HoneyDTO()
                    {
                        ProductId = honey.ProductId,
                        ProductName = honey.ProductName,
                        ProductDescription = honey.ProductDescription,
                        ProductCount = honey.ProductCount,
                        ProductPrice = honey.ProductPrice,
                        HoneyWeight = honey.HoneyWeight
                    };
                    res.Add(_honeyDTO);
                }
                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<HoneyDTO> GetAsync(long productId)
        {
            try
            {
                var _honey = await _repo.Honey.GetAsync(productId);
                var _honeyDTO = new HoneyDTO()
                {
                    ProductId = _honey.ProductId,
                    ProductName = _honey.ProductName,
                    ProductDescription = _honey.ProductDescription,
                    ProductCount = _honey.ProductCount,
                    ProductPrice = _honey.ProductPrice,
                    HoneyWeight = _honey.HoneyWeight
                };
                return _honeyDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task UpdateAsync(HoneyDTO product)
        {
            try
            {
                await _repo.Honey.DeleteAsync(product.ProductId);
                await _repo.SaveAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
