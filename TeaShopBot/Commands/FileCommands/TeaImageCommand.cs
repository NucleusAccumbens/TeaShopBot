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


        public override async Task<ProductDTO> FileExecute(Update update, ITelegramBotClient client, CancellationToken cancellationToken, ProductDTO tea)
        {
            var chatId = update.Message.Chat.Id;
            if (tea == null || tea is HerbDTO || tea is HoneyDTO)
            {
                tea = new TeaDTO();
            }
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
                        caption: $"Сорт чая: {TeaEnumParser.TeaTypeToString((tea as TeaDTO).TeaType)}\n" +
                        $"Название чая: {tea.ProductName}\n" +
                        $"Описание чая: {tea.ProductDescription}\n" +
                        $"Вес чая: {TeaEnumParser.TeaWeightToString((tea as TeaDTO).TeaWeight)}\n" +
                        $"Форма хранения чая: {TeaEnumParser.TeaFormToString((tea as TeaDTO).TeaForm)}\n" +
                        $"Цена чая: {tea.ProductPrice}\n" +
                        $"Количество чая: {tea.ProductCount}\n\n" +
                        $"Чай появится в наличии после нажатия на кнопку ⬇️",
                        replyMarkup: inlineKeyboardMarkup,
                        cancellationToken: cancellationToken);
            return tea;
        }
    }
}
