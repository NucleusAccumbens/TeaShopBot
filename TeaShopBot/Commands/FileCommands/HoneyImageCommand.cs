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
using Telegram.Bot.Types.InputFiles;
using Telegram.Bot.Types.ReplyMarkups;

namespace TeaShopBot.Commands.FileCommands
{
    public class HoneyImageCommand : TelegramFileCommand
    {
        public override string Name => "Фото меда";

        public override bool Contains(Message message)
        {
            if (message.Type != MessageType.Photo)
                return false;
            string mes = message.Caption;

            return mes.Contains(Name);
        }

        public override async Task FileExecute(Update update, ITelegramBotClient client, CancellationToken cancellationToken, TeaDTO tea, HerbDTO herb, HoneyDTO honey)
        {
            var chatId = update.Message.Chat.Id;
            var fileId = update.Message.Photo[2].FileId;
            InputOnlineFile file = new InputOnlineFile(fileId);
            honey.ProductPathToImage = fileId;

            InlineKeyboardMarkup inlineKeyboardMarkup = new(new[]
            {
                new[]
                {
                    InlineKeyboardButton.WithCallbackData(text: "Сохранить", callbackData: "M"),
                },
            });

            await client.SendPhotoAsync(
                        chatId: chatId,
                        photo: file,
                        caption: $"Название мёда: {honey.ProductName}\n" +
                        $"Описание мёда: {honey.ProductDescription}\n" +
                        $"Вес мёда: {HoneyEnumParser.HoneyWeightToString(honey.HoneyWeight)}\n" +
                        $"Цена мёда: {honey.ProductPrice}\n" +
                        $"Количество мёда: {honey.ProductCount}\n" +
                        $"Мёд появится в наличии после нажатия на кнопку ⬇️",
                        replyMarkup: inlineKeyboardMarkup,
                        cancellationToken: cancellationToken);
        }
    }   
}
