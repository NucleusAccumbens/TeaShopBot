using DATABASE.DataContext;
using DATABASE.Entityes;
using DATABASE.Interfaces;
using DATABASE.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeaShopBLL.Interfaces;

namespace TeaShopBLL.Services
{
    public class UserService : IService<UserDTO>
    {
        private readonly UnitOfWork _repo;

        public UserService(UnitOfWork repo)
        {
            _repo = repo;
        }

        public async Task CreateAsync(UserDTO user)
        {
            try
            {
                var _user = new User() 
                {
                    ChatId = user.ChatId,
                    Name = user.Name,
                    IsAdmin = user.IsAdmin,
                };
                await _repo.Users.CreateAsync(_user);
                await _repo.SaveAsync();
            }
            catch(Exception)
            {
                throw;
            }
        }

        public async Task DeleteAsync(long userId)
        {
            try
            {
                await _repo.Users.DeleteAsync(userId);
                await _repo.SaveAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<UserDTO>> GetAllAsync()
        {
            try
            {
                var res = new List<UserDTO>();

                var _allUsers = await _repo.Users.GetAllAsync();
                foreach (var user in _allUsers)
                {
                    var _userDTO = new UserDTO()
                    {
                        UserId = user.UserId,
                        ChatId = user.ChatId,
                        IsAdmin = user.IsAdmin
                    };
                    res.Add(_userDTO);
                }
                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<UserDTO> GetAsync(long userId)
        {
            try
            {
                var _user = await _repo.Users.GetAsync(userId);
                var _userDTO = new UserDTO()
                {
                    UserId = _user.UserId,
                    ChatId = _user.ChatId,
                    IsAdmin = _user.IsAdmin
                };
                return _userDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task UpdateAsync(UserDTO user)
        {
            try
            {
                var _user = new User()
                {
                    UserId = user.UserId,
                    ChatId = user.ChatId,
                    IsAdmin = user.IsAdmin
                };
                await _repo.Users.UpdateAsync(_user);
                await _repo.SaveAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> CheckUserIsInDb(long chatId)
        {
            try
            {
                var user = await (_repo.Users as UserRepository).GetUserByChatIdAsync(chatId);

                if (user == null) return false;
                else return true;               
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> CheckUserIsAdmin(long chatId)
        {
            try
            {
                var user = await (_repo.Users as UserRepository).GetUserByChatIdAsync(chatId);

                if (user?.IsAdmin == true) return true;
                else return false;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
