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
    public class TeaImageCommand : TelegramFileCommand
    {
        public override string Name => @"Фото чая";

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
            tea.ProductPathToImage = fileId;

            InlineKeyboardMarkup inlineKeyboardMarkup = new(new[]
            {
                new[]
                {
                    InlineKeyboardButton.WithCallbackData(text: "Сохранить", callbackData: "S"),
                },
            });

            await client.SendPhotoAsync(
                        chatId: chatId,
                        photo: file,
                        caption: $"Сорт чая: {TeaEnumParser.TeaTypeToString(tea.TeaType)}\n" +
                        $"Название чая: {tea.ProductName}\n" +
                        $"Описание чая: {tea.ProductDescription}\n" +
                        $"Вес чая: {TeaEnumParser.TeaWeightToString(tea.TeaWeight)}\n" +
                        $"Форма хранения чая: {TeaEnumParser.TeaFormToString(tea.TeaForm)}\n" +
                        $"Цена чая: {tea.ProductPrice}\n" +
                        $"Количество чая: {tea.ProductCount}\n\n" +
                        $"Чай появится в наличии после нажатия на кнопку ⬇️",
                        replyMarkup: inlineKeyboardMarkup,
                        cancellationToken: cancellationToken);
        }
    }
}
