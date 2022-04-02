using DATABASE.DataContext;
using DATABASE.Enums;
using DATABASE.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TeaShopBLL.DTO;
using TeaShopBLL.Services;
using TeaShopBot.Abstractions;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace TeaShopBot.Commands.CallbackCommands.OrderCallbackCommands
{
    public class OrderChangeCallbackCommand : TelegramCallbackCommand
    {
        public override char CallbackDataCode => 'D';
        public override bool Contains(CallbackQuery message)
        {
            char code = message.Data.ToString().FirstOrDefault();

            if (code != CallbackDataCode)
                return false;

            return message.Data.Contains(CallbackDataCode);
        }

        public override async Task CallbackExecute(Update update, ITelegramBotClient client, CancellationToken cancellationToken)
        {
            var chatId = update.CallbackQuery.Message.Chat.Id;

            if (update.CallbackQuery.Data != null)
            {
                if (update.CallbackQuery.Data == "DPaymentMethod")
                {
                    InlineKeyboardMarkup inlineKeyboardMarkup = new(new[]
                    {
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData(text: "💸 Переводом", callbackData: "ERemittance"),
                            InlineKeyboardButton.WithCallbackData(text: "💵 Наличными", callbackData: "ECash"),
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData(text: "⬅️ Назад", callbackData: "CCart"),
                        },
                    });

                    await client.SendTextMessageAsync(
                                chatId: chatId,
                                text: "Выбери способ оплаты ⬇️",
                                parseMode: ParseMode.Html,
                                replyMarkup: inlineKeyboardMarkup,
                                cancellationToken: cancellationToken);
                }
                if (update.CallbackQuery.Data == "DReceiptMethod")
                {
                    InlineKeyboardMarkup inlineKeyboardMarkup = new(new[]
                    {
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData(text: "🚶🏾 Самовывоз", callbackData: "EPickup"),
                            InlineKeyboardButton.WithCallbackData(text: "🚛 СДЭК", callbackData: "ESDAK"),
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData(text: "⬅️ Назад", callbackData: "CCart"),
                        },
                    });

                    await client.SendTextMessageAsync(
                                chatId: chatId,
                                text: "Выбери способ доставки ⬇️",
                                parseMode: ParseMode.Html,
                                replyMarkup: inlineKeyboardMarkup,
                                cancellationToken: cancellationToken);
                }
                if (update.CallbackQuery.Data == "DRemoveProduct")
                {
                    try
                    {
                        using (ShopContext context = new ShopContext())
                        {
                            UnitOfWork unit = new UnitOfWork(context);
                            var orderServise = new OrderService(unit);
                            var userOrder = await orderServise.GetActiveOrderAsync(chatId);

                            foreach (var product in userOrder.Products)
                            {
                                InlineKeyboardMarkup inlineKeyboardMarkup = new(new[]
                                {
                                    new[]
                                    {
                                        InlineKeyboardButton.WithCallbackData(text: "❌ Удалить", callbackData: "ERemove"),
                                    },
                                });

                                string weight = "";
                                if (product is TeaDTO)
                                {
                                    weight = $"{TeaEnumParser.TeaWeightToString((product as TeaDTO).TeaWeight)} грамм";
                                }
                                if (product is HerbDTO)
                                {
                                    weight = $"{HerbsEnumParser.HerbsWeightToString((product as HerbDTO).Weight)} грамм";
                                }
                                if (product is HoneyDTO)
                                {
                                    weight = $"{HoneyEnumParser.HoneyWeightToString((product as HoneyDTO).HoneyWeight)} грамм";
                                }

                                await client.SendTextMessageAsync(
                                        chatId: chatId,
                                        text: $"Удалить <b>{product.ProductName}</b> (<b>{weight}</b>) из корзины?",
                                        parseMode: ParseMode.Html,
                                        replyMarkup: inlineKeyboardMarkup,
                                        cancellationToken: cancellationToken);
                            }
                        }
                    }
                    catch (Exception)
                    {
                        await client.SendTextMessageAsync(
                            chatId: chatId,
                            text: "🤦🏿‍♀️ Что-то пошло не так...",
                            cancellationToken: cancellationToken);
                    }
                }
                if (update.CallbackQuery.Data == "DOrderConfirm")
                {

                }
            }
        }
    }
}
