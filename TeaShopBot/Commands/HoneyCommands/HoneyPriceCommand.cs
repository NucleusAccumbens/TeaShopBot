using DATABASE.DataContext;
using DATABASE.Enums;
using DATABASE.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TeaShopBLL.DTO;
using TeaShopBLL.Services;
using TeaShopBot.Abstractions;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace TeaShopBot.Commands.HoneyCommands
{
    public class HoneyPriceCommand : TelegramCreateProductCommand
    {
        public override string Name => "Цена меда: ";

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
                honey.ProductPrice = Convert.ToDecimal(update.Message.Text.Substring(11));
                await client.SendTextMessageAsync(
                       chatId: chatId,
                       text: $"Название мёда: {honey.ProductName}\n" +
                       $"Описание мёда: {honey.ProductDescription}\n" +
                       $"Вес мёда: {HoneyEnumParser.HoneyWeightToString(honey.HoneyWeight)}\n" +
                       $"Цена мёда: {honey.ProductPrice}\n\n" +
                       $"Теперь укажи количество мёда: \n" +
                       $"<b>Количество меда</b>: <i>какая-то цифра...</i>",
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
