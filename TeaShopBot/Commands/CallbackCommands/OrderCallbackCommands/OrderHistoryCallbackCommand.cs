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
    public class OrderHistoryCallbackCommand : TelegramCallbackCommand
    {
        public override char CallbackDataCode => 'N';

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
                try
                {

                    using (ShopContext context = new ShopContext())
                    {
                        UnitOfWork unit = new UnitOfWork(context);
                        var orderServise = new OrderService(unit);
                        var allOrders = await orderServise.GetAllAsync();

                        if (update.CallbackQuery.Data == "NActive")
                        {
                            var activeOrders = new List<OrderDTO>();
                            foreach (var order in allOrders)
                            {
                                if (order.OrderStatus == true)
                                {
                                    string mes = GetOrderInfo(order);

                                    InlineKeyboardMarkup inlineKeyboard = new(new[]
                                    {
                                        new[]
                                        {
                                            InlineKeyboardButton.WithCallbackData(text: "Перевести в статус 📜 В архиве 📜", callbackData: $"OArchive{order.OrderId}"),
                                        },
                                    });

                                    await client.SendTextMessageAsync(
                                        chatId: chatId,
                                        text: mes,
                                        replyMarkup: inlineKeyboard,
                                        parseMode: ParseMode.Html,
                                        cancellationToken: cancellationToken);
                                    activeOrders.Add(order);
                                }
                            }
                            if (activeOrders.Count == 0)
                                await client.SendTextMessageAsync(
                                    chatId: chatId,
                                    text: "🤷🏻‍♀️ В этой категории нет заказов...",
                                    parseMode: ParseMode.Html,
                                    cancellationToken: cancellationToken);
                        }
                        if (update.CallbackQuery.Data == "NArchive")
                        {
                            var archiveOrders = new List<OrderDTO>();
                            foreach (var order in allOrders)
                            {
                                if (order.OrderStatus == false)
                                {
                                    string mes = GetOrderInfo(order);

                                    InlineKeyboardMarkup inlineKeyboard = new(new[]
                                    {
                                        new[]
                                        {
                                            InlineKeyboardButton.WithCallbackData(text: "Перевести в статус 📗 Активен 📗", callbackData: $"OActive{order.OrderId}"),
                                        },
                                    });

                                    await client.SendTextMessageAsync(
                                        chatId: chatId,
                                        text: mes,
                                        replyMarkup: inlineKeyboard,
                                        parseMode: ParseMode.Html,
                                        cancellationToken: cancellationToken);

                                    archiveOrders.Add(order);
                                }
                            }
                            if (archiveOrders.Count == 0)
                                await client.SendTextMessageAsync(
                                    chatId: chatId,
                                    text: "🤷🏻‍♀️ В этой категории нет заказов...",
                                    parseMode: ParseMode.Html,
                                    cancellationToken: cancellationToken);
                        }

                        
                    }
                }
                catch (Exception ex)
                {
                    await client.SendTextMessageAsync(
                        chatId: chatId,
                        text: $"🤦🏿‍♀️ Что-то пошло не так...\n" +
                        $"{ex.Message}",
                        cancellationToken: cancellationToken);
                }
            }
        }

        private string GetOrderInfo(OrderDTO order)
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
                $"<b>Номер заказа</b>: {order.OrderId}\n\n";
            return message;
        }

    }
}
