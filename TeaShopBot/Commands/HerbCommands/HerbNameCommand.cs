using DATABASE.Enums;
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

namespace TeaShopBot.Commands.HerbCommands
{
    public class HerbNameCommand : TelegramCreateProductCommand
    {
        public override string Name => "Название сбора: ";

        public override bool Contains(Message message)
        {
            if (message.Type != MessageType.Text)
                return false;

            return message.Text.Contains(Name);
        }

        public override async Task Execute(Update update, ITelegramBotClient client, CancellationToken cancellationToken, TeaDTO tea, HerbDTO herb, HoneyDTO honey)
        {

            var chatId = update.Message.Chat.Id;
            herb.ProductName = update.Message.Text.Substring(16);
            await client.SendTextMessageAsync(
                        chatId: chatId,
                        text: $"Регион: {HerbsEnumParser.HerbsRegionToString(herb.Region)}\n" +
                        $"Название сбора: {herb.ProductName}\n\n" +
                        $"Теперь отправь сообщение с описанием сбора: \n" +
                        $"<b>Описание сбора</b>: <i>какое-то описание...</i>",
                        parseMode: ParseMode.Html,
                        cancellationToken: cancellationToken);
        }
    }
}
