using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeaShopBot.Abstractions;
using TeaShopBot.Commands;
using TeaShopBot.Commands.CallbackCommands;
using TeaShopBot.Commands.FileCommands;
using TeaShopBot.Commands.TeaCommands;

namespace TeaShopBot.Services
{
    public class CommandService : ICommandService
    {
        private List<TelegramCommand> _commandsList = new List<TelegramCommand>();
        private List<TelegramCallbackCommand> _callbackCommandList = new List<TelegramCallbackCommand>();
        private List<TelegramCreateProductCommand> _telegramCreateProductCommands = new List<TelegramCreateProductCommand>();
        private List<TelegramFileCommand> _telegramFileCommands = new List<TelegramFileCommand>();
        public List<TelegramCommand> GetCommands()
        {
            _commandsList.Add(new StartCommand());
            _commandsList.Add(new AddProductCommand());
            _commandsList.Add(new TeaTypeCommand());
            _commandsList.Add(new BackToSelectActionCommand()); 
            _commandsList.Add(new BackToSelectCategoryCommand());
            return _commandsList;
        }

        public List<TelegramCallbackCommand> GetCallbackCommands()
        {
            _callbackCommandList.Add(new TeaTypeCallbackCommand());
            _callbackCommandList.Add(new TeaWeighteCallbackCommand());
            _callbackCommandList.Add(new TeaFormCallbackCommand());
            _callbackCommandList.Add(new SaveTeaCallbackCommand());
            return _callbackCommandList;
        }

        public List<TelegramCreateProductCommand> GetTelegramCreateProductCommands()
        {
            _telegramCreateProductCommands.Add(new TeaNameCommand());
            _telegramCreateProductCommands.Add(new TeaDeskriptionCommand());
            _telegramCreateProductCommands.Add(new TeaPriceCommand());
            _telegramCreateProductCommands.Add(new TeaCountCommand());
            return _telegramCreateProductCommands;
        }

        public List<TelegramFileCommand> GetTelegramFileCommands()
        {
            _telegramFileCommands.Add(new TeaImageCommand());
            return _telegramFileCommands;
        }
    }
}
