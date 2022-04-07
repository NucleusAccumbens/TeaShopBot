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
    public class HerbRegionCommand : TelegramCommand
    {
        public override string Name => "Травы";

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
                            InlineKeyboardButton.WithCallbackData(text: "Карелия", callbackData: "hКарелия"),
                            InlineKeyboardButton.WithCallbackData(text: "Кавказ", callbackData: "hКавказ"),
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData(text: "Алтай", callbackData: "hАлтай"),
                            InlineKeyboardButton.WithCallbackData(text: "Сибирь", callbackData: "hСибирь"),
                        },
                    });

            await client.SendTextMessageAsync(
                chatId: chatId,
                text: "Выбери регион:",
                replyMarkup: inlineKeyboardMarkup,
                cancellationToken: cancellationToken);
        }
    }
}
