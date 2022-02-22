using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TeaShopBLL;
using TeaShopBLL.Interfaces;
using TeaShopBLL.Services;

namespace TeaShopBotAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly IService<UserDTO> _service;

        public TestController(IService<UserDTO> service)
        {
            _service = service;
        }
        
        [HttpGet]
        public ActionResult<List<string>> Get()
        {
           var _userNames = new List<string>();

           var _allUsers = _service.GetAllAsync().Result;
           foreach (var user in _allUsers)
           {
               _userNames.Add(user.ChatId.ToString());
           }

           if (_userNames.Count == 0)
           {
               return BadRequest("Нет зарегестрирпованных пользователей");
           }
           
           return _userNames;
        }
    }
}
