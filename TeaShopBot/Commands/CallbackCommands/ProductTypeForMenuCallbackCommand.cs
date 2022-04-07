using DATABASE.DataContext;
using DATABASE.Enums;
using DATABASE.Interfaces;
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

namespace TeaShopBot.Commands.CallbackCommands
{
    public class ProductTypeForMenuCallbackCommand : TelegramCallbackCommand
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
                        new[]
                        {
                                InlineKeyboardButton.WithCallbackData(text: "🛒 Корзина 🛒", callbackData: "CCart"),
                                InlineKeyboardButton.WithCallbackData(text: "✨ Меню ✨", callbackData: "CMenu"),
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
                    InlineKeyboardMarkup inlineKeyboardMarkup = new(new[]
                    {
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData(text: "🏔️ Алтай 🏔️", callbackData: "LAltai"),
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData(text: "🌲 Карелия 🌲", callbackData: "LKarelia"),
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData(text: "⛰️ Кавказ ⛰️", callbackData: "LCaucasus"),
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData(text: "🗻 Сибирь 🗻", callbackData: "LSiberia"),
                        },
                        new[]
                        {
                                InlineKeyboardButton.WithCallbackData(text: "🛒 Корзина 🛒", callbackData: "CCart"),
                                InlineKeyboardButton.WithCallbackData(text: "✨ Меню ✨", callbackData: "CMenu"),
                        },
                    });

                    await client.SendTextMessageAsync(
                        chatId: chatId,
                        text: "🌱  Испокон веков люди использовали травы для приготовления напитков, профилактики и лечения недугов. " +
                        "Различные отвары, настойки и чаи приносят пользу для организма человека, повышая иммунитет и самочувствие. " +
                        "Однако не стоит считать их панацеей от всех болезней, но то, что там есть много антиоксидантов и витаминов, " +
                        "не оспорит даже самый убежденный скептик 🌱\n\n" +
                        "⛩ Чайный Автономный Округ ⛩ собрал лучшие травы из разных регионов! " +
                        "Выбери регион и ознакомься с ассортиментом ⬇️",
                        replyMarkup: inlineKeyboardMarkup,
                        cancellationToken: cancellationToken);
                }
                if (update.CallbackQuery.Data == "AHoney")
                {
                    try
                    {
                        using (ShopContext context = new ShopContext())
                        {
                            IUnitOfWork unit = new UnitOfWork(context);
                            var honeyService = new HoneyService(unit);
                            var honey = await honeyService.GetAllAsync();
                            int honeyCount = 0;

                            if (honey.Count == 0)
                            {
                                await GetMessageForHoneyCountIsNull(chatId, client, cancellationToken);
                                return;
                            }                              

                            foreach (var h in honey)
                            {
                                if (h.ProductPathToImage != null && h.InStock == true)
                                {
                                    InlineKeyboardMarkup inlineKeyboardMarkup = new(new[]
                                    {
                                            new[]
                                            {
                                                InlineKeyboardButton.WithCallbackData(text: "🛒 Добавить в корзину 🛒", callbackData: "CTeaAddToCard"),
                                            },
                                            new[]
                                            {
                                                InlineKeyboardButton.WithCallbackData(text: "🛒 Корзина 🛒", callbackData: "CCart"),
                                                InlineKeyboardButton.WithCallbackData(text: "✨ Меню ✨", callbackData: "CMenu"),
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

                                    honeyCount++;
                                }
                                if (h.ProductPathToImage == null && h.InStock == true)
                                {
                                    InlineKeyboardMarkup inlineKeyboardMarkup = new(new[]
                                    {
                                            new[]
                                            {
                                                InlineKeyboardButton.WithCallbackData(text: "🛒 Добавить в корзину 🛒", callbackData: "CTeaAddToCard"),
                                            },
                                            new[]
                                            {
                                                InlineKeyboardButton.WithCallbackData(text: "🛒 Корзина 🛒", callbackData: "CCart"),
                                                InlineKeyboardButton.WithCallbackData(text: "✨ Меню ✨", callbackData: "CMenu"),
                                            },
                                        });

                                    await client.SendTextMessageAsync(
                                        chatId: chatId,
                                        text: $"<b>Код:</b> {h.ProductId}\n\n" +
                                        $"<b>Название:</b> {h.ProductName}\n\n" +
                                        $"<b>Описание:</b> {h.ProductDescription}\n\n" +
                                        $"<b>Вес:</b> {HoneyEnumParser.HoneyWeightToString(h.HoneyWeight)}\n\n" +
                                        $"<b>Цена:</b> {h.ProductPrice}\n\n" +
                                        $"<b>В наличии:</b> {h.ProductCount}",
                                        parseMode: ParseMode.Html,
                                        replyMarkup: inlineKeyboardMarkup,
                                        cancellationToken: cancellationToken);

                                    honeyCount++;
                                }
                            }

                            if (honeyCount == 0) 
                                await GetMessageForHoneyCountIsNull(chatId, client, cancellationToken);
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
                if (update.CallbackQuery.Data == "AShop")
                {
                    await client.SendTextMessageAsync(
                       chatId: chatId,
                       text: "Создатель ⛩ Чайного Автономного Округа ⛩ Алексей с 2011 года занимается китайскими чаями! " +
                       "Он поставил себе задачей привозить качественный пуэр по доступной цене. " +
                       "Алексей 8 раз был в Китае, и теперь может сказать, что разбирается в чае!",
                       cancellationToken: cancellationToken);
                }
                if (update.CallbackQuery.Data == "AContact")
                {
                    await client.SendTextMessageAsync(
                       chatId: chatId,
                       text: "Связаться с администратором: @shanti_travels \n" +
                       "По вопросам создания бота: @noncredistka",
                       cancellationToken: cancellationToken);
                }
            }         
        }

        private async Task GetMessageForHoneyCountIsNull(long chatId, ITelegramBotClient client, CancellationToken cancellationToken)
        {
            await client.SendTextMessageAsync(
                                    chatId: chatId,
                                    text: "🤷🏼 Здесь пока ничего нет...\n" +
                                    "Но это временно, не пропусти обновления ✨",
                                    cancellationToken: cancellationToken);
        }
    }
}

