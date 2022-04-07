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
    public class OrderListCommand : TelegramCommand
    {
        public override string Name => "История заказов";

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
                    InlineKeyboardButton.WithCallbackData(text: "📗 Активные 📗", callbackData: "NActive"),
                    InlineKeyboardButton.WithCallbackData(text: "📜 В архиве 📜", callbackData: "NArchive"),
                },
            });

            await client.SendTextMessageAsync(
                        chatId: chatId,
                        text: $"❗️ Заказ считается активным, " +
                        $"если он не был отмечен админом как завершенный (то есть оплата " +
                        $"не была произведена или товар не был передан покупателю)\n\n" +
                        $"Выбери категорию заказа ⬇️",
                        parseMode: ParseMode.Html,
                        replyMarkup: inlineKeyboardMarkup,
                        cancellationToken: cancellationToken);
        }
    }
}
