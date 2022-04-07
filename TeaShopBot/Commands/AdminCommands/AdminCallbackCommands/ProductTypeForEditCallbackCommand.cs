using DATABASE.DataContext;
using DATABASE.Enums;
using DATABASE.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TeaShopBLL.Services;
using TeaShopBot.Abstractions;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace TeaShopBot.Commands.AdminCommands.AdminCallbackCommands
{
    public class ProductTypeForEditCallbackCommand : TelegramCallbackCommand
    {
        public override char CallbackDataCode => 'Q';

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

                if (update.CallbackQuery.Data == "QTea")
                {

                    InlineKeyboardMarkup inlineKeyboardMarkup = new(new[]
                    {
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData(text: "🌹 Красные 🌹", callbackData: "RRed"),
                            InlineKeyboardButton.WithCallbackData(text: "🍃 Зелёные 🍃", callbackData: "RGreen"),
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData(text: "🐚 Белые 🐚", callbackData: "RWhite"),
                            InlineKeyboardButton.WithCallbackData(text: "🐉 Улуны 🐉", callbackData: "ROolong"),
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData(text: "🐲 Шу Пуэры 🐲", callbackData: "RShu puer"),
                            InlineKeyboardButton.WithCallbackData(text: "🌚 Шен Пуэры 🌚", callbackData: "RShen puer"),
                        },
                    });

                    await client.SendTextMessageAsync(
                        chatId: chatId,
                        text: "Выбери сорт чая для редактирования ⬇️",
                        replyMarkup: inlineKeyboardMarkup,
                        cancellationToken: cancellationToken);
                }
                if (update.CallbackQuery.Data == "QHerb")
                {
                    InlineKeyboardMarkup inlineKeyboardMarkup = new(new[]
                   {
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData(text: "🏔️ Алтай 🏔️", callbackData: "rAltai"),
                            InlineKeyboardButton.WithCallbackData(text: "⛰️ Кавказ ⛰️", callbackData: "rCaucasus"),
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData(text: "🌲 Карелия 🌲", callbackData: "rKarelia"),
                            InlineKeyboardButton.WithCallbackData(text: "🗻 Сибирь 🗻", callbackData: "rSiberia"),
                        },
                    });

                    await client.SendTextMessageAsync(
                        chatId: chatId,
                        text: "Выбери регион для редактирования сбора ⬇️",
                        replyMarkup: inlineKeyboardMarkup,
                        cancellationToken: cancellationToken);
                }
                if (update.CallbackQuery.Data == "QHoney")
                {
                    try
                    {
                        using (ShopContext context = new ShopContext())
                        {
                            UnitOfWork repo = new UnitOfWork(context);
                            var honeyService = new HoneyService(repo);
                            var honey = await honeyService.GetAllAsync();

                            if (honey.Count == 0)
                            {
                                await client.SendTextMessageAsync(
                                    chatId: chatId,
                                    text: $"🍯 В этой категории пока нет товаров!",
                                    cancellationToken: cancellationToken);
                            }
                            if (honey != null && honey.Count != 0)
                            {
                                foreach (var h in honey)
                                {
                                    InlineKeyboardMarkup inlineKeyboardMarkup = new(new[]
                                            {
                                            new[]
                                            {
                                                InlineKeyboardButton.WithCallbackData(text: "🖌 Редактировать описание", callbackData: $"UEditProductDescriptions{h.ProductId}"),
                                            },
                                            new[]
                                            {
                                                InlineKeyboardButton.WithCallbackData(text: "🖌 Редактировать количество", callbackData: $"UEditProductCount{h.ProductId}"),
                                            },
                                            new[]
                                            {
                                                InlineKeyboardButton.WithCallbackData(text: "🖌 Редактировать цену", callbackData: $"UEditProductPrice{h.ProductId}"),
                                            },
                                            new[]
                                            {
                                                InlineKeyboardButton.WithCallbackData(text: "🖌 Редактировать фото", callbackData: $"UEditProductPhoto{h.ProductId}"),
                                            },
                                            new[]
                                            {
                                                InlineKeyboardButton.WithCallbackData(text: "❌ Удалить", callbackData: $"URemoveProduct{h.ProductId}"),
                                            },
                                        });

                                    await client.SendPhotoAsync(
                                        chatId: chatId,
                                        photo: h.ProductPathToImage,
                                        caption: $"<b>Код:</b> {h.ProductId}\n\n" +
                                        $"<b>Название:</b> {h.ProductName}\n\n" +
                                        $"<b>Описание:</b> {h.ProductDescription}\n\n" +
                                        $"<b>Вес:</b> {HoneyEnumParser.HoneyWeightToString(h.HoneyWeight)}\n\n" +
                                        $"<b>Цена:</b> {h.ProductPrice}\n\n" +
                                        $"<b>В наличии:</b> {h.ProductCount}",
                                        parseMode: ParseMode.Html,
                                        replyMarkup: inlineKeyboardMarkup,
                                        cancellationToken: cancellationToken);
                                }
                            }
                        }
                    }
                    catch (Exception)
                    {
                        await client.SendTextMessageAsync(
                            chatId: chatId,
                            text: $"🤦🏿‍♀️ Что-то пошло не так...",
                            cancellationToken: cancellationToken);
                    }

                }
            }
        }

    }
}
