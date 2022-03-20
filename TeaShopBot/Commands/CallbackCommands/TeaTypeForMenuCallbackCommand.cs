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

namespace TeaShopBot.Commands.CallbackCommands
{
    public class TeaTypeForMenuCallbackCommand : TelegramCallbackCommand
    {
        public override char CallbackDataCode => 'A';

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

                if (update.CallbackQuery.Data == "ATea")
                {
                    InlineKeyboardMarkup inlineKeyboardMarkup = new(new[]
                    {
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData(text: "🌹 Красные 🌹", callbackData: "BRed"),
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData(text: "🍃 Зелёные 🍃", callbackData: "BGreen"),
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData(text: "🐚 Белые 🐚", callbackData: "BWhite"),
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData(text: "🐉 Улуны 🐉", callbackData: "BOolong"),
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData(text: "🐲 Шу Пуэры 🐲", callbackData: "BShuPuer"),
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData(text: "🌚 Шен Пуэры 🌚", callbackData: "BShenPuer"),
                        },
                    });

                    await client.SendTextMessageAsync(
                        chatId: chatId,
                        text: "🍃  Существует всего 6 основных сортов чая, которые производятся из чайного листа: " +
                        "черный, зеленый, белый, желтый, улун и пуэр. " +
                        "Друг от друга они отличаются степенью ферментации и способом обработки, " +
                        "растение же всегда одно – Camellia sinensis, представляющая собой куст или дерево 🍃\n\n" +
                        "⛩ Чайный Автономный Округ ⛩ предлагает разнообразие сортов! " +
                        "Выбери сорт чая, который подойдёт именно тебе  ⬇️",
                        replyMarkup: inlineKeyboardMarkup,
                        cancellationToken: cancellationToken);
                }
                if (update.CallbackQuery.Data == "AHerb")
                {
                    await client.SendTextMessageAsync(
                       chatId: chatId,
                       text: "🤷🏼 Здесь пока ничего нет...\n" +
                       "Но это временно, не пропусти обновления 🌱",
                       cancellationToken: cancellationToken);
                }
                if (update.CallbackQuery.Data == "AHoney")
                {
                    await client.SendTextMessageAsync(
                       chatId: chatId,
                       text: "🤷🏼 Здесь пока ничего нет...\n" +
                       "Но это временно, не пропусти обновления 🍯",
                       cancellationToken: cancellationToken);
                }
            }         
        }
    }
}

