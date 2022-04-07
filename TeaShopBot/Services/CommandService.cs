using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeaShopBot.Abstractions;
using TeaShopBot.Commands;
using TeaShopBot.Commands.AdminCommands;
using TeaShopBot.Commands.AdminCommands.AdminCallbackCommands;
using TeaShopBot.Commands.AdminCommands.AdminFileCommand;
using TeaShopBot.Commands.CallbackCommands;
using TeaShopBot.Commands.CallbackCommands.HerbCallbackCommands;
using TeaShopBot.Commands.CallbackCommands.HoneyCallbackCommands;
using TeaShopBot.Commands.CallbackCommands.OrderCallbackCommands;
using TeaShopBot.Commands.FileCommands;
using TeaShopBot.Commands.HerbCommands;
using TeaShopBot.Commands.HoneyCommands;
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
            _commandList.Add(new ShopAddProductCommand());
            _commandList.Add(new TeaTypeCommand());
            _commandList.Add(new BackToSelectActionCommand()); 
            _commandList.Add(new AllTeaCommand());
            _commandList.Add(new MenuCommand());
            _commandList.Add(new HerbRegionCommand());
            _commandList.Add(new HoneyCommand());
            _commandList.Add(new UserListCommand());
            _commandList.Add(new OrderListCommand());
            _commandList.Add(new ShopEditCommand());
            _commandList.Add(new ProductEditDescriptionCommand());
            _commandList.Add(new ProductEditCountCommand());
            _commandList.Add(new ProductPriceEditCommand());
            return _commandList;
        }

        public List<TelegramAddProductCallbackCommand> GetAddProductCallbackCommands()
        {
            _addProductCallbackCommandList.Add(new TeaTypeCallbackCommand());
            _addProductCallbackCommandList.Add(new TeaWeightCallbackCommand());
            _addProductCallbackCommandList.Add(new TeaFormCallbackCommand());
            _addProductCallbackCommandList.Add(new TeaSaveCallbackCommand());
            _addProductCallbackCommandList.Add(new HerbRegionCallbackCommand());
            _addProductCallbackCommandList.Add(new HerbWeightCallbackCommand());
            _addProductCallbackCommandList.Add(new HerbSaveCallbackCommand());
            _addProductCallbackCommandList.Add(new HoneyWeightCallbackCommand());
            _addProductCallbackCommandList.Add(new HoneySaveCallbackCommand());
            return _addProductCallbackCommandList;
        }

        public List<TelegramCreateProductCommand> GetTelegramCreateProductCommands()
        {
            _createProductCommandList.Add(new TeaNameCommand());
            _createProductCommandList.Add(new TeaDescriptionCommand());
            _createProductCommandList.Add(new TeaPriceCommand());
            _createProductCommandList.Add(new TeaCountCommand());
            _createProductCommandList.Add(new HerbNameCommand());
            _createProductCommandList.Add(new HerbDescriptionCommand());
            _createProductCommandList.Add(new HerbPriceCommand());
            _createProductCommandList.Add(new HerbCountCommand());
            _createProductCommandList.Add(new HoneyNameCommand());
            _createProductCommandList.Add(new HoneyDescriptionCommand());
            _createProductCommandList.Add(new HoneyPriceCommand());
            _createProductCommandList.Add(new HoneyCountCommand());
            return _createProductCommandList;
        }

        public List<TelegramFileCommand> GetTelegramFileCommands()
        {
            _fileCommandList.Add(new TeaImageCommand());
            _fileCommandList.Add(new HerbImageCommand());
            _fileCommandList.Add(new HoneyImageCommand());
            _fileCommandList.Add(new ProductEditPhotoCommand());
            return _fileCommandList;
        }

        public List<TelegramCallbackCommand> GetCallbackCommands()
        {
            _callbackCommandList.Add(new ProductTypeForMenuCallbackCommand());
            _callbackCommandList.Add(new TeaListCallbackCommand());
            _callbackCommandList.Add(new ProductAddToCardCallbackCommand());
            _callbackCommandList.Add(new OrderChangeCallbackCommand());
            _callbackCommandList.Add(new OrderChangePaymentMethodCallbackCommand());
            _callbackCommandList.Add(new OrderChangeReceiptMethodCallbackCommand());
            _callbackCommandList.Add(new OrderRemoveProductCallbackCommand());
            _callbackCommandList.Add(new OrderContactUserCallbackCommand());
            _callbackCommandList.Add(new HerbsListCallbackCommand());
            _callbackCommandList.Add(new OrderHistoryCallbackCommand());
            _callbackCommandList.Add(new OrderStatusCallbackCommand());
            _callbackCommandList.Add(new ShopEditCallbackCommand());
            _callbackCommandList.Add(new ProductTypeForEditCallbackCommand());
            _callbackCommandList.Add(new HoneyEditCallbackCommand());
            _callbackCommandList.Add(new TeaListForEditCallbackCommand());
            _callbackCommandList.Add(new HerbListForEditCallbackCommand());
            return _callbackCommandList;
        }
    }
}
