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
    public class AddProductCommand : TelegramCommand
    {
        public override string Name => @"Добавить товар";

        public override bool Contains(Message message)
        {
            if (message.Type != MessageType.Text)
                return false;

            return message.Text.Contains(Name);
        }

        public override async Task Execute(Message message, ITelegramBotClient client, CancellationToken cancellationToken)
        {
            var chatId = message.Chat.Id;

            ReplyKeyboardMarkup replyKeyboardMarkup = new(new[]
            {
                new KeyboardButton[] {"Чай",  "Травы"},
                new KeyboardButton[] {"Мёд", "Назад к выбору действия"}
            })
            {
                ResizeKeyboard = true
            };

            Message sentMessage = await client.SendTextMessageAsync(
                chatId: chatId,
                text: "Выбери категорию товара:",
                replyMarkup: replyKeyboardMarkup,
                cancellationToken: cancellationToken);
        }
    }
}
