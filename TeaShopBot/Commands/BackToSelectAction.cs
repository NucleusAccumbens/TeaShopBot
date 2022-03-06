using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TeaShopBot.Abstractions;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace TeaShopBot.Commands
{
    public class BackToSelectAction : TelegramCommand
    {
        private StartCommand _startCommand;
        public override string Name => @"Назад к выбору действия";

        public override bool Contains(Message message)
        {
            if(message.Type != MessageType.Text)
                return false;

            return message.Text.Contains(Name);
        }

        public override async Task Execute(Message message, ITelegramBotClient client, CancellationToken cancellationToken)
        {
            _startCommand = new StartCommand();
            await _startCommand.Execute(message, client, cancellationToken);
        }
    }
}
