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
    public class TeaService : IService<TeaDTO>
    {
        private readonly IUnitOfWork _repo;

        public TeaService(IUnitOfWork repo)
        {
            _repo = repo;
        }

        public async Task CreateAsync(TeaDTO tea)
        {
            try
            {
                var _tea = new Tea()
                {
                    ProductName = tea.ProductName,
                    ProductDescription = tea.ProductDescription,
                    ProductPrice = tea.ProductPrice,
                    ProductCount = tea.ProductCount,
                    ProductPathToImage = tea.ProductPathToImage,
                    InStock = tea.InStock,
                    TeaType = tea.TeaType,
                    TeaForm = tea.TeaForm,
                    TeaWeight = tea.TeaWeight,
                };
                await _repo.Teas.CreateAsync(_tea);
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
                await _repo.Teas.DeleteAsync(productId);
                await _repo.SaveAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<TeaDTO>> GetAllAsync()
        {
            try
            {
                var res = new List<TeaDTO>();
                var allTeas = await _repo.Teas.GetAllAsync();
                foreach (var tea in allTeas)
                {
                    var teaDTO = new TeaDTO()
                    {
                        ProductId = tea.ProductId,
                        ProductName = tea.ProductName,
                        ProductDescription = tea.ProductDescription,
                        ProductCount = tea.ProductCount,
                        ProductPrice = tea.ProductPrice,
                        TeaForm = tea.TeaForm,
                        TeaType = tea.TeaType,
                        TeaWeight = tea.TeaWeight
                    };
                    res.Add(teaDTO);
                }
                return res;
            }
            catch (Exception)
            {
                throw;
            };
        }

        public async Task<TeaDTO> GetAsync(long productId)
        {
            try
            {
                var tea = await _repo.Teas.GetAsync(productId);
                
                var teaDTO = new TeaDTO()
                    {
                        ProductId = tea.ProductId,
                        ProductName = tea.ProductName,
                        ProductDescription = tea.ProductDescription,
                        ProductCount = tea.ProductCount,
                        ProductPrice = tea.ProductPrice,
                        TeaForm = tea.TeaForm,
                        TeaType = tea.TeaType,
                        TeaWeight = tea.TeaWeight
                    };
                return teaDTO;
            }
            catch (Exception)
            {
                throw;
            };
        }

        public async Task UpdateAsync(TeaDTO tea)
        {
            var _tea = new Tea()
            {
                ProductId = tea.ProductId,
                ProductName = tea.ProductName,
                ProductDescription = tea.ProductDescription,
                ProductPrice = tea.ProductPrice,
                ProductCount = tea.ProductCount,
                TeaType = tea.TeaType,
                TeaForm = tea.TeaForm,
                TeaWeight = tea.TeaWeight
            };

            try
            {
                await _repo.Teas.UpdateAsync(_tea);
                await _repo.SaveAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
