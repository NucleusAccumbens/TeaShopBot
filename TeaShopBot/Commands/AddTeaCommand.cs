using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TeaShopBot.Abstractions;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace TeaShopBot.Commands
{
    public class AddTeaCommand : TelegramCommand
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
                    InlineKeyboardButton.WithCallbackData(text: "Красный", callbackData: "1"),
                    InlineKeyboardButton.WithCallbackData(text: "Зелёный", callbackData: "2"),
                    InlineKeyboardButton.WithCallbackData(text: "Белый", callbackData: "3"),
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData(text: "Улун", callbackData: "4"),
                    InlineKeyboardButton.WithCallbackData(text: "Шу пуэр", callbackData: "5"),
                    InlineKeyboardButton.WithCallbackData(text: "Шен пуэр", callbackData: "6"),
                },
            });

            await client.SendTextMessageAsync(
                chatId: chatId,
                text: "Выбери сорт чая:",
                replyMarkup: inlineKeyboardMarkup,
                cancellationToken: cancellationToken);
        }


        public static async Task CallbackExecute(Update update, ITelegramBotClient client, CancellationToken cancellationToken)
        {

            if (update.CallbackQuery.Data != null)
            {
                var chatId = update.Message.Chat.Id;

                if (update.CallbackQuery.Data == "1")
                {
                    await client.SendTextMessageAsync(
                        chatId: chatId,
                        text: "Выбери сорт чая:",
                        cancellationToken: cancellationToken);
                }
            }
        }
    }
}
