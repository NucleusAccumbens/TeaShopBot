using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TeaShopBot.Abstractions;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TeaShopBot.Commands.TeaCommands
{
    public class SetTeaWeightCommand : TelegramCommand
    {
        public override string Name => throw new NotImplementedException();

        public override bool Contains(Message message)
        {
            throw new NotImplementedException();
        }

        public override Task Execute(Update update, ITelegramBotClient client, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
