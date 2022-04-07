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
    public class HerbPriceCommand : TelegramCreateProductCommand
    {
        public override string Name => "Цена сбора: ";

        public override bool Contains(Message message)
        {
            if (message.Type != MessageType.Text)
                return false;
            string mes = message.Text;

            return mes.Contains(Name);
        }

        public override async Task Execute(Update update, ITelegramBotClient client, CancellationToken cancellationToken, TeaDTO tea, HerbDTO herb, HoneyDTO honey)
        {
            var chatId = update.Message.Chat.Id;

            try
            {
                herb.ProductPrice = Convert.ToDecimal(update.Message.Text.Substring(12));
                await client.SendTextMessageAsync(
                        chatId: chatId,
                        text: $"Регион: {HerbsEnumParser.HerbsRegionToString(herb.Region)}\n" +
                        $"Название сбора: {herb.ProductName}\n" +
                        $"Описание сбора: {herb.ProductDescription}\n" +
                        $"Вес сбора: {HerbsEnumParser.HerbsWeightToString(herb.Weight)}\n" +
                        $"Цена сбора: {herb.ProductPrice}\n\n" +
                        $"Теперь укажи количество: \n" +
                        $"<b>Количество сбора</b>: <i>какя-то цифра...</i>",
                        parseMode: ParseMode.Html,
                        cancellationToken: cancellationToken);
            }
            catch (FormatException)
            {
                await client.SendTextMessageAsync(
                            chatId: chatId,
                            text: $"🙅🏻‍♀️ Неверный формат цены! Укажи цифру.",
                            cancellationToken: cancellationToken);
            }
        }
    }
}
