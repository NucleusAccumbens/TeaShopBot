using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeaShopBot.Abstractions
{
    public interface ICommandService
    {
        List<TelegramCommand> GetCommands();
        List<TelegramCallbackCommand> GetCallbackCommands();
        List<TelegramSaveProductCommand> GetTelegramSaveProductCommands();
    }
}
