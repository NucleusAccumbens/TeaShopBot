using DATABASE.Entityes;
using DATABASE.Enums;
using DATABASE.Interfaces;
using DATABASE.Repositories;
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
                var _tea = tea.Adapt<Tea>();
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

        public async Task<List<TeaDTO>> GetAllAsync()
        {
            try
            {
                var res = new List<TeaDTO>();
                var allTeas = await _repo.Teas.GetAllAsync();
                foreach (var tea in allTeas)
                {
                    var teaDTO = tea.Adapt<TeaDTO>();
                    res.Add(teaDTO);
                }
                return res;
            }
            catch (Exception)
            {
                throw;
            };
        }

        public async Task<List<TeaDTO>> GetAllRedTeaAsync()
        {
            try
            {
                var res = new List<TeaDTO>();
                var allTeas = await _repo.Teas.GetAllAsync();

                foreach (var tea in allTeas)
                {
                    if (tea.TeaType == TeaTypes.Red)
                    {
                        var teaDTO = tea.Adapt<TeaDTO>();
                        res.Add(teaDTO);
                    }                 
                }
                return res;
            }
            catch (Exception)
            {
                throw;
            };
        }

        public async Task<List<TeaDTO>> GetAllGreenTeaAsync()
        {
            try
            {
                var res = new List<TeaDTO>();
                var allTeas = await _repo.Teas.GetAllAsync();

                foreach (var tea in allTeas)
                {
                    if (tea.TeaType == TeaTypes.Green)
                    {
                        var teaDTO = tea.Adapt<TeaDTO>();
                        res.Add(teaDTO);
                    }
                }
                return res;
            }
            catch (Exception)
            {
                throw;
            };
        }

        public async Task<List<TeaDTO>> GetAllWhiteTeaAsync()
        {
            try
            {
                var res = new List<TeaDTO>();
                var allTeas = await _repo.Teas.GetAllAsync();

                foreach (var tea in allTeas)
                {
                    if (tea.TeaType == TeaTypes.White)
                    {
                        var teaDTO = tea.Adapt<TeaDTO>();
                        res.Add(teaDTO);
                    }
                }
                return res;
            }
            catch (Exception)
            {
                throw;
            };
        }

        public async Task<List<TeaDTO>> GetAllOolongTeaAsync()
        {
            try
            {
                var res = new List<TeaDTO>();
                var allTeas = await _repo.Teas.GetAllAsync();

                foreach (var tea in allTeas)
                {
                    if (tea.TeaType == TeaTypes.Oolong)
                    {
                        var teaDTO = tea.Adapt<TeaDTO>();
                        res.Add(teaDTO);
                    }
                }
                return res;
            }
            catch (Exception)
            {
                throw;
            };
        }

        public async Task<List<TeaDTO>> GetAllShenPuerAsync()
        {
            try
            {
                var res = new List<TeaDTO>();
                var allTeas = await _repo.Teas.GetAllAsync();

                foreach (var tea in allTeas)
                {
                    if (tea.TeaType == TeaTypes.ShenPuer)
                    {
                        var teaDTO = tea.Adapt<TeaDTO>();
                        res.Add(teaDTO);
                    }
                }
                return res;
            }
            catch (Exception)
            {
                throw;
            };
        }

        public async Task<List<TeaDTO>> GetAllShuPuerAsync()
        {
            try
            {
                var res = new List<TeaDTO>();
                var allTeas = await _repo.Teas.GetAllAsync();

                foreach (var tea in allTeas)
                {
                    if (tea.TeaType == TeaTypes.ShuPuer)
                    {
                        var teaDTO = tea.Adapt<TeaDTO>();
                        res.Add(teaDTO);
                    }
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
                var teaDTO = tea.Adapt<TeaDTO>();
                return teaDTO;
            }
            catch (Exception)
            {
                throw;
            };
        }

        public async Task UpdateAsync(TeaDTO tea)
        {
            try
            {
                var _tea = tea.Adapt<Tea>();
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
