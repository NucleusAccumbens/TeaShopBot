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

namespace TeaShopBot.Commands.HoneyCommands
{
    public class HoneyCountCommand : TelegramCreateProductCommand
    {
        public override string Name => "Количество меда: ";

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
                honey.ProductCount = Convert.ToInt32(update.Message.Text.Substring(17));
                await client.SendTextMessageAsync(
                       chatId: chatId,
                       text: $"Название мёда: {honey.ProductName}\n" +
                       $"Описание мёда: {honey.ProductDescription}\n" +
                       $"Вес мёда: {HoneyEnumParser.HoneyWeightToString(honey.HoneyWeight)}\n" +
                       $"Цена мёда: {honey.ProductPrice}\n" +
                       $"Количество мёда: {honey.ProductCount}\n\n" +
                       $"Осталось только загрузить фото! Отправь фотографию с подписью:\n" +
                       $"Фото меда",
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
