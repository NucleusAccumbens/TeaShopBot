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
        List<TelegramAddProductCallbackCommand> GetAddProductCallbackCommands();
        List<TelegramCreateProductCommand> GetTelegramCreateProductCommands();
        List<TelegramFileCommand> GetTelegramFileCommands();
        List<TelegramCallbackCommand> GetCallbackCommands();
        
    }
}
