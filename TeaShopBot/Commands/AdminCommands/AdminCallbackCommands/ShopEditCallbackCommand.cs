using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TeaShopBot.Abstractions;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace TeaShopBot.Commands.AdminCommands.AdminCallbackCommands
{
    public class ShopEditCallbackCommand : TelegramCallbackCommand
    {
        public override char CallbackDataCode => 'P';

        public override bool Contains(CallbackQuery message)
        {
            char code = message.Data.ToString().FirstOrDefault();

            if (code != CallbackDataCode)
                return false;

            return message.Data.Contains(CallbackDataCode);
        }

        public override async Task CallbackExecute(Update update, ITelegramBotClient client, CancellationToken cancellationToken)
        {
            
            if (update.CallbackQuery.Data != null)
            {
                var chatId = update.CallbackQuery.Message.Chat.Id;

                if (update.CallbackQuery.Data == "PProducts")
                {
                    InlineKeyboardMarkup inlineKeyboardMarkup = new(new[]
                    {
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData(text: "🍃 Редактировать чай 🍃", callbackData: "QTea"),
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData(text: "🌱 Редактировать травы 🌱", callbackData: "QHerb"),
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData(text: "🍯 Редактировать мёд 🍯", callbackData: "QHoney"),
                        },
                    });

                    await client.SendTextMessageAsync(
                        chatId: chatId,
                        text: "Выбери категорию товара для редактирования⬇️",
                        replyMarkup: inlineKeyboardMarkup,
                        cancellationToken: cancellationToken);
                }
                if (update.CallbackQuery.Data == "PSale")
                {
                    
                    await client.SendTextMessageAsync(
                        chatId: chatId,
                        text: "🤷🏼 Эта функция временно недоступна...",
                        cancellationToken: cancellationToken);
                }
            }
        }

    }
}
