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
    public class HoneyService : IService<HoneyDTO>
    {
        private readonly IUnitOfWork _repo;

        public HoneyService(IUnitOfWork repo)
        {
            _repo = repo;
        }

        public async Task CreateAsync(HoneyDTO honeyDto)
        {
            try
            {
                var _honey = honeyDto.Adapt<Honey>();
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

        public async Task<List<HoneyDTO>> GetAllAsync()
        {
            try
            {
                var res = new List<HoneyDTO>();
                var allHoney = await _repo.Honey.GetAllAsync();
                foreach(var honey in allHoney)
                {
                    var _honeyDTO = honey.Adapt<HoneyDTO>();
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
                var _honeyDTO = _honey.Adapt<HoneyDTO>();
                return _honeyDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task UpdateAsync(HoneyDTO honeyDto)
        {
            try
            {
                var _honey = honeyDto.Adapt<Honey>();
                await _repo.Honey.UpdateAsync(_honey);
                await _repo.SaveAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
