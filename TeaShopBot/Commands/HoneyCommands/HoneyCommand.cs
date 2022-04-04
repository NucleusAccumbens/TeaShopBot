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

namespace TeaShopBot.Commands.HoneyCommands
{
    public class HoneyCommand : TelegramCommand
    {
        public override string Name => "Мёд";

        public override bool Contains(Message message)
        {
            if (message.Type != MessageType.Text)
                return false;
            string mes = message.Text;

            return mes.Contains(Name);
        }

        public override async Task Execute(Update update, ITelegramBotClient client, CancellationToken cancellationToken)
        {
            var chatId = update.Message.Chat.Id;
            await client.SendTextMessageAsync(
                        chatId: chatId,
                        text: $"Чтобы установить название мёда, отправь сообщение: \n" +
                        $"<b>Название меда</b>: <i>какое-то название...</i>",
                        parseMode: ParseMode.Html,
                        cancellationToken: cancellationToken);
        }
    }
}
