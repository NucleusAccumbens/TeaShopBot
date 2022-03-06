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
    public class BackToSelectCategoryCommand : TelegramCommand
    {
        public override string Name => @"Назад к выбору категории товара";

        public override bool Contains(Message message)
        {
            if (message.Type != MessageType.Text)
                return false;

            return message.Text.Contains(Name);
        }

        public override Task Execute(Message message, ITelegramBotClient client, CancellationToken cancellationToken)
        {
            var addProductCommand = new AddProductCommand();
            return addProductCommand.Execute(message, client, cancellationToken);
        }
    }
}
