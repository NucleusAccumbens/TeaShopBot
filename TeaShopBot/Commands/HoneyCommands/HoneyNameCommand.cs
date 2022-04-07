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

namespace TeaShopBot.Commands.HoneyCommands
{
    public class HoneyNameCommand : TelegramCreateProductCommand
    {
        public override string Name => "Название меда: ";

        public override bool Contains(Message message)
        {
            if (message.Type != MessageType.Text)
                return false;
            string mes = message.Text;

            return mes.Contains(Name);
        }

        public override async Task Execute(Update update, ITelegramBotClient client, CancellationToken cancellationToken, TeaDTO tea, HerbDTO herb, HoneyDTO honey)
        {
            honey.ProductName = update.Message.Text.Substring(15);
            var chatId = update.Message.Chat.Id;
            await client.SendTextMessageAsync(
                        chatId: chatId,
                        text: $"Название мёда: {honey.ProductName}\n\n" +
                        $"Теперь отправь сообщение с описанием мёда: \n" +
                        $"<b>Описание меда</b>: <i>какое-то описание...</i>",
                        parseMode: ParseMode.Html,
                        cancellationToken: cancellationToken);
        }
    }
}
