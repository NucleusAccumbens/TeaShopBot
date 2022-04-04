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
    internal class HerbImageCommand : TelegramFileCommand
    {
        public override string Name => "Фото сбора";

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
            herb.ProductPathToImage = fileId;

            InlineKeyboardMarkup inlineKeyboardMarkup = new(new[]
            {
                new[]
                {
                    InlineKeyboardButton.WithCallbackData(text: "Сохранить", callbackData: "K"),
                },
            });

            await client.SendPhotoAsync(
                        chatId: chatId,
                        photo: file,
                        caption: $"Регион: {HerbsEnumParser.HerbsRegionToString(herb.Region)}\n" +
                        $"Название сбора: {herb.ProductName}\n" +
                        $"Описание сбора: {herb.ProductDescription}\n" +
                        $"Вес сбора: {HerbsEnumParser.HerbsWeightToString(herb.Weight)}\n" +
                        $"Цена сбора: {herb.ProductPrice}\n" +
                        $"Количество сбора: {herb.ProductCount}\n\n" +
                        $"Сбор появится в наличии после нажатия на кнопку ⬇️",
                        replyMarkup: inlineKeyboardMarkup,
                        cancellationToken: cancellationToken);
        }
    }
}
