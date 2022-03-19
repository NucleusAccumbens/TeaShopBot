using DATABASE.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TeaShopBot.Abstractions
{
    public interface ITelegramBot
    {
        Task<bool> SaveUserInDb(Update ubdate);
        Task<bool> CheckUserIsAdmin(long chatId);
        List<TelegramCommand> GetCommands();
        List<TelegramCallbackCommand> GetCallbackCommands();
        List<TelegramCreateProductCommand> GetTelegramCreateProductCommands();
        List<TelegramFileCommand> GetTelegramFileCommands();
    }
}
