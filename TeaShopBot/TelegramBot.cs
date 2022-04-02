using DATABASE.DataContext;
using DATABASE.Interfaces;
using DATABASE.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TeaShopBLL;
using TeaShopBLL.Services;
using TeaShopBot.Abstractions;
using TeaShopBot.Commands;
using TeaShopBot.Commands.CallbackCommands;
using TeaShopBot.Services;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace TeaShopBot
{
    public class TelegramBot : ITelegramBot
    {
        private static readonly string _token = "5234530253:AAE0rmehJ5dWrsrrwsdySvNjaZ25B-uibd8";
        private readonly ICommandService _commandService = new CommandService();

        public ITelegramBotClient BotClient
        {
            get
            {
                return new TelegramBotClient(_token);
            }
        } 

        public async Task SetWebhookAsync()
        {
            await BotClient.SetWebhookAsync("");
        }

        private async Task<bool> CheckUserIsInDb(Update ubdate)
        {
            using (ShopContext context = new ShopContext())
            {
                try
                {
                    long id = ubdate.Message.Chat.Id;
                    UnitOfWork _unit = new UnitOfWork(context);
                    var userService = new UserService(_unit);

                    return await userService.CheckUserIsInDb(id);
                }
                catch (Exception)
                {
                    throw;
                }
            }              
        }

        public async Task<bool> SaveUserInDb(Update ubdate)
        {
            try
            {
                bool userIsInDb = await CheckUserIsInDb(ubdate);

                if (userIsInDb == true)
                {
                    return false;
                }
                else
                {
                    using (ShopContext context = new ShopContext())
                    {
                        UnitOfWork _unit = new UnitOfWork(context);
                        var userService = new UserService(_unit);
                        var user = new UserDTO()
                        {
                            ChatId = ubdate.Message.Chat.Id,
                            Username = ubdate.Message.Chat.Username,
                            IsAdmin = false,
                        };                          
                        
                        await userService.CreateAsync(user);
                    }
                    return true;
                }                  
            }
            catch(Exception)
            {
                throw;
            }
        }
        public async Task<bool> CheckUserIsAdmin(long chatId)
        {
            try
            {
                using (ShopContext context = new ShopContext())
                {
                    UnitOfWork _unit = new UnitOfWork(context);
                    var userService = new UserService(_unit);
                    return await userService.CheckUserIsAdmin(chatId);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<TelegramCommand> GetCommands()
        {
            return _commandService.GetCommands();
        }

        public List<TelegramAddProductCallbackCommand> GetAddProductCallbackCommands()
        {
            return _commandService.GetAddProductCallbackCommands();
        }

        public List<TelegramCreateProductCommand> GetTelegramCreateProductCommands()
        {
            return _commandService.GetTelegramCreateProductCommands();
        }

        public List<TelegramFileCommand> GetTelegramFileCommands()
        {
            return _commandService.GetTelegramFileCommands();
        }

        public List<TelegramCallbackCommand> GetCallbackCommands()
        {
            return _commandService.GetCallbackCommands();
        }
    }
}
