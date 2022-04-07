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
using Telegram.Bot.Types.ReplyMarkups;

namespace TeaShopBot.Commands.HerbCommands
{
    public class HerbDescriptionCommand : TelegramCreateProductCommand
    {
        public override string Name => "Описание сбора: ";

        public override bool Contains(Message message)
        {
            if (message.Type != MessageType.Text)
                return false;

            return message.Text.Contains(Name);
        }

        public override async Task Execute(Update update, ITelegramBotClient client, CancellationToken cancellationToken, TeaDTO tea, HerbDTO herb, HoneyDTO honey)
        {
            var chatId = update.Message.Chat.Id;
            herb.ProductDescription = update.Message.Text.Substring(16);

            InlineKeyboardMarkup inlineKeyboardMarkup = new(new[]
            {
                new[]
                {
                    InlineKeyboardButton.WithCallbackData(text: "50 грамм", callbackData: "I50"),
                    InlineKeyboardButton.WithCallbackData(text: "100 грамм", callbackData: "I100"),
                    InlineKeyboardButton.WithCallbackData(text: "150 грамм", callbackData: "I150"),
                },
                new[]
                {
                     InlineKeyboardButton.WithCallbackData(text: "200 грамм", callbackData: "I200"),
                     InlineKeyboardButton.WithCallbackData(text: "250 грамм", callbackData: "I250"),
                },
            });

            await client.SendTextMessageAsync(
                        chatId: chatId,
                        text: $"Регион: {HerbsEnumParser.HerbsRegionToString(herb.Region)}\n" +
                        $"Название сбора: {herb.ProductName}\n" +
                        $"Описание сбора: {herb.ProductDescription}\n\n" +
                        $"Выбери вес сбора: \n",
                        parseMode: ParseMode.Html,
                        replyMarkup: inlineKeyboardMarkup,
                        cancellationToken: cancellationToken);
        }
    }
}
