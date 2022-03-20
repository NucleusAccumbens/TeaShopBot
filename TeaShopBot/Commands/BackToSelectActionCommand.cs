using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TeaShopBLL.DTO;
using TeaShopBot.Abstractions;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace TeaShopBot.Commands
{
    public class BackToSelectActionCommand : TelegramCommand
    {
        public override string Name => @"Назад";

        public override bool Contains(Message message)
        {
            if(message.Type != MessageType.Text)
                return false;

            return message.Text.Contains(Name);
        }

        public override async Task Execute(Update update, ITelegramBotClient client, CancellationToken cancellationToken)
        {
            var startCommand = new StartCommand();
            await startCommand.Execute(update, client, cancellationToken);
        }
    }
}
