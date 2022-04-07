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

namespace TeaShopBot.Commands.TeaCommands
{
    public class TeaTypeCommand : TelegramCommand
    {
        public override string Name => @"Чай";

        public override bool Contains(Message message)
        {
            if (message.Type != MessageType.Text)
                return false;

            return message.Text.Contains(Name);
        }

        public override async Task Execute(Update update, ITelegramBotClient client, CancellationToken cancellationToken)
        {
            var chatId = update.Message.Chat.Id;

            InlineKeyboardMarkup inlineKeyboardMarkup = new(new[]
            {
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData(text: "Красный", callbackData: "TКрасный"),
                            InlineKeyboardButton.WithCallbackData(text: "Зелёный", callbackData: "TЗелёный"),
                            InlineKeyboardButton.WithCallbackData(text: "Белый", callbackData: "TБелый"),
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData(text: "Улун", callbackData: "TУлун"),
                            InlineKeyboardButton.WithCallbackData(text: "Шу пуэр", callbackData: "TШу пуэр"),
                            InlineKeyboardButton.WithCallbackData(text: "Шен пуэр", callbackData: "TШен пуэр"),
                        },
                    });

            await client.SendTextMessageAsync(
                chatId: chatId,
                text: "Выбери сорт чая:",
                replyMarkup: inlineKeyboardMarkup,
                cancellationToken: cancellationToken);
        }
    }
}
