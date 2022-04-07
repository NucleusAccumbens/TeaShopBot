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

namespace TeaShopBot.Commands
{
    public class ShopAddProductCommand : TelegramCommand
    {
        public override string Name => @"Добавить товар";

        public override bool Contains(Message message)
        {
            if (message.Type != MessageType.Text)
                return false;

            return message.Text.Contains(Name);
        }

        public override async Task Execute(Update update, ITelegramBotClient client, CancellationToken cancellationToken)
        {
            var chatId = update.Message.Chat.Id;

            ReplyKeyboardMarkup replyKeyboardMarkup = new(new[]
            {
                new KeyboardButton[] { "🍃 Чай 🍃",  "🌱 Травы 🌱"},
                new KeyboardButton[] { "🍯 Мёд 🍯", "⬅️ Назад" }
            })
            {
                ResizeKeyboard = true
            };

            await client.SendTextMessageAsync(
                chatId: chatId,
                text: "Выбери категорию товара ⬇️",
                replyMarkup: replyKeyboardMarkup,
                cancellationToken: cancellationToken);
        }
    }
}
