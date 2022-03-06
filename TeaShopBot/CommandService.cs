using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeaShopBot.Abstractions;
using TeaShopBot.Commands;

namespace TeaShopBot
{
    public class CommandService : ICommandService
    {
        private List<TelegramCommand> _commandsList = new List<TelegramCommand>();

        public List<TelegramCommand> GetCommands()
        {
            _commandsList.Add(new StartCommand());
            _commandsList.Add(new AddProductCommand());
            _commandsList.Add(new AddTeaCommand());
            _commandsList.Add(new BackToSelectAction()); ;
            return _commandsList;
        }
    }
}
