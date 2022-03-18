using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeaShopBot.Abstractions;
using TeaShopBot.Commands;
using TeaShopBot.Commands.CallbackCommands;
using TeaShopBot.Commands.SaveProductCommands;

namespace TeaShopBot.Services
{
    public class CommandService : ICommandService
    {
        private List<TelegramCommand> _commandsList = new List<TelegramCommand>();
        private List<TelegramCallbackCommand> _callbackCommandList = new List<TelegramCallbackCommand>();
        private List<TelegramSaveProductCommand> _telegramSaveProductCommands = new List<TelegramSaveProductCommand>();
        public List<TelegramCommand> GetCommands()
        {
            _commandsList.Add(new StartCommand());
            _commandsList.Add(new AddProductCommand());
            _commandsList.Add(new AddTeaCommand());
            _commandsList.Add(new BackToSelectActionCommand()); 
            _commandsList.Add(new BackToSelectCategoryCommand());
            return _commandsList;
        }

        public List<TelegramCallbackCommand> GetCallbackCommands()
        {
            _callbackCommandList.Add(new AddTeaCallbackCommand());
            return _callbackCommandList;
        }

        public List<TelegramSaveProductCommand> GetTelegramSaveProductCommands()
        {
            _telegramSaveProductCommands.Add(new TeaNameCommand());
            return _telegramSaveProductCommands;
        }
    }
}
