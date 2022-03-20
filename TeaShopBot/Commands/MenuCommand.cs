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
    public class MenuCommand : TelegramCommand
    {
        public override string Name => @"Меню";

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
                            InlineKeyboardButton.WithCallbackData(text: "🍃 Чай 🍃", callbackData: "ATea"),
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData(text: "🌱 Травы 🌱", callbackData: "AHerb"),
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData(text: "🍯 Мёд 🍯", callbackData: "AHoney"),
                        },
            });

            await client.SendTextMessageAsync(
                chatId: chatId,
                text: "Выбери категорию ⬇️",
                replyMarkup: inlineKeyboardMarkup,
                cancellationToken: cancellationToken);
        }
    }
}
