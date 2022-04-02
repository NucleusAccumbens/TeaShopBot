using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeaShopBot.Abstractions;
using TeaShopBot.Commands;
using TeaShopBot.Commands.CallbackCommands;
using TeaShopBot.Commands.CallbackCommands.OrderCallbackCommands;
using TeaShopBot.Commands.FileCommands;
using TeaShopBot.Commands.TeaCommands;

namespace TeaShopBot.Services
{
    public class CommandService : ICommandService
    {
        private List<TelegramCommand> _commandList = new List<TelegramCommand>();
        private List<TelegramAddProductCallbackCommand> _addProductCallbackCommandList = new List<TelegramAddProductCallbackCommand>();
        private List<TelegramCreateProductCommand> _createProductCommandList = new List<TelegramCreateProductCommand>();
        private List<TelegramFileCommand> _fileCommandList = new List<TelegramFileCommand>();
        private List<TelegramCallbackCommand> _callbackCommandList = new List<TelegramCallbackCommand>();
        public List<TelegramCommand> GetCommands()
        {
            _commandList.Add(new StartCommand());
            _commandList.Add(new AddProductCommand());
            _commandList.Add(new TeaTypeCommand());
            _commandList.Add(new BackToSelectActionCommand()); 
            _commandList.Add(new AllTeaCommand());
            _commandList.Add(new MenuCommand());
            return _commandList;
        }

        public List<TelegramAddProductCallbackCommand> GetAddProductCallbackCommands()
        {
            _addProductCallbackCommandList.Add(new TeaTypeCallbackCommand());
            _addProductCallbackCommandList.Add(new TeaWeighteCallbackCommand());
            _addProductCallbackCommandList.Add(new TeaFormCallbackCommand());
            _addProductCallbackCommandList.Add(new SaveTeaCallbackCommand());
            return _addProductCallbackCommandList;
        }

        public List<TelegramCreateProductCommand> GetTelegramCreateProductCommands()
        {
            _createProductCommandList.Add(new TeaNameCommand());
            _createProductCommandList.Add(new TeaDeskriptionCommand());
            _createProductCommandList.Add(new TeaPriceCommand());
            _createProductCommandList.Add(new TeaCountCommand());
            return _createProductCommandList;
        }

        public List<TelegramFileCommand> GetTelegramFileCommands()
        {
            _fileCommandList.Add(new TeaImageCommand());
            return _fileCommandList;
        }

        public List<TelegramCallbackCommand> GetCallbackCommands()
        {
            _callbackCommandList.Add(new TeaTypeForMenuCallbackCommand());
            _callbackCommandList.Add(new TeaListCallbackCommand());
            _callbackCommandList.Add(new ProductAddToCardCallbackCommand());
            _callbackCommandList.Add(new OrderChangeCallbackCommand());
            _callbackCommandList.Add(new OrderChangePaymentMethodCallbackCommand());
            _callbackCommandList.Add(new OrderChangeReceiptMethodCallbackCommand());
            _callbackCommandList.Add(new OrderRemoveProductCallbackCommand());
            _callbackCommandList.Add(new OrderContactUserCallbackCommand());
            return _callbackCommandList;
        }
    }
}
