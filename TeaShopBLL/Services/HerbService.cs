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
    public class HerbService : IService<HerbDTO>
    {
        private readonly IUnitOfWork _repo;

        public HerbService(IUnitOfWork repo)
        {
            _repo = repo;
        }

        public async Task CreateAsync(HerbDTO herb)
        {
            try
            {
                var _herb = new Herb()
                {
                    ProductName = herb.ProductName,
                    ProductDescription = herb.ProductDescription,
                    ProductPrice = herb.ProductPrice,
                    ProductCount = herb.ProductCount,
                    ProductPathToImage = herb.ProductPathToImage,
                    InStock = herb.InStock,
                    Weight = herb.Weight,
                    Region = herb.Region,
                };
                await _repo.Herbs.CreateAsync(_herb);
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
                await _repo.Herbs.DeleteAsync(productId);
                await _repo.SaveAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<HerbDTO>> GetAllAsync()
        {
            try
            {
                var res = new List<HerbDTO>();
                var allHerbs = await _repo.Herbs.GetAllAsync();
                foreach (var herb in allHerbs)
                {
                    var herbDTO = new HerbDTO()
                    {
                        ProductId = herb.ProductId,
                        ProductName = herb.ProductName,
                        ProductDescription = herb.ProductDescription,
                        ProductCount = herb.ProductCount,
                        ProductPathToImage = herb.ProductPathToImage,
                        ProductPrice = herb.ProductPrice,
                        Region = herb.Region,
                        Weight = herb.Weight         
                    };
                    res.Add(herbDTO);   
                }
                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<HerbDTO> GetAsync(long productId)
        {
            try
            {
                var _herb = await _repo.Herbs.GetAsync(productId);
                var herbDTO = new HerbDTO()
                {
                    ProductId = _herb.ProductId,
                    ProductName = _herb.ProductName,
                    ProductDescription = _herb.ProductDescription,
                    ProductCount = _herb.ProductCount,
                    ProductPathToImage = _herb.ProductPathToImage,
                    ProductPrice = _herb.ProductPrice,
                    Region = _herb.Region,
                    Weight = _herb.Weight
                };
                return herbDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task UpdateAsync(HerbDTO herb)
        {
            try
            {
                var _herb = new Herb()
                {
                    ProductId = herb.ProductId,
                    ProductName = herb.ProductName,
                    ProductDescription = herb.ProductDescription,
                    ProductCount = herb.ProductCount,
                    ProductPathToImage = herb.ProductPathToImage,
                    ProductPrice = herb.ProductPrice,
                    InStock = herb.InStock,
                    Region = herb.Region,
                    Weight = herb.Weight
                };

                await _repo.Herbs.UpdateAsync(_herb);
                await _repo.SaveAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
