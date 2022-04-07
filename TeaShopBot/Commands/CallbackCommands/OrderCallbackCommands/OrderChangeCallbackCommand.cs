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
using TeaShopDAL.Enums;
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
                            InlineKeyboardButton.WithCallbackData(text: "🚶🏾 Самовывоз", callbackData: "FPickup"),
                            InlineKeyboardButton.WithCallbackData(text: "🚛 СДЭК", callbackData: "FSDAK"),
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
                                        InlineKeyboardButton.WithCallbackData(text: "❌ Удалить", callbackData: $"G{product.ProductId}"),
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

                            InlineKeyboardMarkup inlineKeyboard = new(new[]
                            {
                                new[]
                                {
                                    InlineKeyboardButton.WithCallbackData(text: "🛒 Корзина 🛒", callbackData: "CCart"),
                                },
                            });

                            await client.SendTextMessageAsync(
                                        chatId: chatId,
                                        text: $"Вернуться в корзину ⬇️",
                                        parseMode: ParseMode.Html,
                                        replyMarkup: inlineKeyboard,
                                        cancellationToken: cancellationToken);
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
                    OrderDTO userOrder;
                    string userName = update.CallbackQuery.Message.Chat.Username;
                    
                    using (ShopContext context = new ShopContext())
                    {
                        UnitOfWork unit = new UnitOfWork(context);
                        var orderServise = new OrderService(unit);
                        userOrder = await orderServise.GetActiveOrderAsync(chatId);
                    }

                    using (ShopContext context = new ShopContext())
                    {
                        UnitOfWork unit = new UnitOfWork(context);
                        var userServise = new UserService(unit);
                        string info = GetOrderInfo(userOrder, userName);

                        foreach(var admin in await userServise.GetAllAdminAsync())
                        {
                            InlineKeyboardMarkup inlineKeyboard = new(new[]
                            {
                                new[]
                                {
                                    InlineKeyboardButton.WithCallbackData(text: "🤝 Сделка состоялась 🤝", callbackData: $"OArchive{userOrder.OrderId}"),
                                },
                            });

                            await client.SendTextMessageAsync(
                            chatId: admin.ChatId,
                            text: $"{info}\n" +
                            $"❗️ Если в пункте <b>От пользователя</b> нет ссылки, заказчик свяжется с тобой самостоятельно.\n" +
                            $"✨ Чтобы понять, какой заказ ему принадлежит, уточни номер заказа!\n\n" +
                            $"❗️❗️ Когда заказчик получит свой заказ, нажми\n" +
                            $"🤝 Сделка состоялась 🤝\n" +
                            $"Заказ будет помещён в архив.",
                            parseMode: ParseMode.Html,
                            replyMarkup: inlineKeyboard,
                            cancellationToken: cancellationToken);
                        }                       
                    }

                    if (update.CallbackQuery.Message.Chat.Username == null)
                    {
                        await client.SendTextMessageAsync(
                            chatId: chatId,
                            text: "💪🏾 Заказ успешно подтверждён!\n\n" +
                            "😬 Упс...в твоём профиле отсутствует <b>Имя пользователя</b>, админ не сможет выйти с тобой на связь...\n" +
                            $"🔥 Но не стоит беспокоиться! Информация о заказе уже у админа! Напиши @shanti_travels и сообщи номер заказа:\n" +
                            $"<b>№ {userOrder.OrderId}</b>",
                            parseMode: ParseMode.Html,
                            cancellationToken: cancellationToken) ;
                        return;
                    }

                    await client.SendTextMessageAsync(
                            chatId: chatId,
                            text: "💪🏾 Заказ успешно подтверждён! Для уточнения деталей с тобой свяжется администратор.",
                            cancellationToken: cancellationToken);
                }
            }
        }

        private string GetOrderInfo(OrderDTO order, string userName)
        {
            string message = "";
            foreach (var product in order.Products)
            {
                if (product is TeaDTO)
                {
                    message += $"🍃 <b>{product.ProductName}</b>\n";
                    message += $"⚖️ {TeaEnumParser.TeaWeightToString((product as TeaDTO).TeaWeight)} грамм\n";
                }
                if (product is HoneyDTO)
                {
                    message += $"🍯 <b>{product.ProductName}</b>\n";
                    message += $"⚖️ {HoneyEnumParser.HoneyWeightToString((product as HoneyDTO).HoneyWeight)} грамм\n";
                }
                if (product is HerbDTO)
                {
                    message += $"🌱 <b>{product.ProductName}</b>\n";
                    message += $"⚖️ {HerbsEnumParser.HerbsWeightToString((product as HerbDTO).Weight)} грамм\n";
                }
                message += $"💰 {product.ProductPrice}\n\n";
            }
            message += $"<b>💰 Общая стоимость</b>: {order.TotalProductPrice}\n" +
                $"<b>🛸 Способ доставки</b>: {OrderEnumParser.ReceiptMethodToString(order.ReceiptMethod)}\n" +
                $"<b>💳 Способ оплаты</b>: {OrderEnumParser.PaymentMethodToString(order.PaymentMethod)}\n" +
                $"<b>Номер заказа</b>: {order.OrderId}\n" +
                $"<b>🧑🏼‍🎤 От пользователя</b>: @{userName}\n\n";
            return message;
        }
    }
}
