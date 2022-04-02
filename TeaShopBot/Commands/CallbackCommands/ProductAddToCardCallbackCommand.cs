using DATABASE.DataContext;
using DATABASE.Entityes;
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
using DATABASE.Enums;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using TeaShopDAL.Enums;

namespace TeaShopBot.Commands.CallbackCommands
{
    public class ProductAddToCardCallbackCommand : TelegramCallbackCommand
    {
        public override char CallbackDataCode => 'C';

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

                if (update.CallbackQuery.Data == "CTeaAddToCard")
                {
                    var productDTO = await GetProduct(update, client, cancellationToken);

                    try
                    {
                        using (ShopContext context = new ShopContext())
                        {

                            UnitOfWork _unit = new UnitOfWork(context);
                            var product = await _unit.Products.GetAsync(productDTO.ProductId);
                            var orders = await (_unit.Orders as OrderRepository).GetAllUserOrdersAsync(chatId);

                            int activeOrdersCounter = 0;

                            if (orders != null)
                            {
                                foreach (var o in orders)
                                {
                                    if (o.OrderStatus == true && o.Products != null)
                                    {
                                        if (o.Products.Contains(product))
                                        {
                                            await client.SendTextMessageAsync(
                                                chatId: chatId,
                                                text: $"💥 <b>{product.ProductName}</b> уже есть в корзине!\n",
                                                parseMode: ParseMode.Html,
                                                cancellationToken: cancellationToken);
                                            return;
                                        }

                                        o.Products.Add(product);
                                        await _unit.SaveAsync();
                                        activeOrdersCounter++;
                                    }
                                }
                            }
                            if (activeOrdersCounter == 0)
                            {
                                var order = new Order()
                                {
                                    IsDeleted = false,
                                    OrderStatus = true,
                                    UserChatId = chatId,
                                    Products = new List<Product>()
                                };
                                order.Products.Add(product);
                                await _unit.Orders.CreateAsync(order);
                            }


                            await client.SendTextMessageAsync(
                                chatId: chatId,
                                text: $"👍🏽 <b>{product.ProductName}</b> добавлен в корозину!\n",
                                parseMode: ParseMode.Html,
                                cancellationToken: cancellationToken);
                        }
                    }
                    catch(Exception ex)
                    {
                        await client.SendTextMessageAsync(
                            chatId: chatId,
                            text: "🤦🏿‍♀️ Что-то пошло не так...",
                            cancellationToken: cancellationToken);
                    }    
                }
            }
            if (update.CallbackQuery.Data == "CCart")
            {
                var chatId = update.CallbackQuery.Message.Chat.Id;

                try
                {                 
                    using (ShopContext context = new ShopContext())
                    {
                        UnitOfWork unit = new UnitOfWork(context);
                        var orderServise = new OrderService(unit);
                        var userOrder = await orderServise.GetActiveOrderAsync(chatId);
                        string message = GetCartMessage(userOrder);

                        if (userOrder.Products.Count == 0)
                        {
                            InlineKeyboardMarkup inlineKeyboard = new(new[]
                            {
                                new[]
                                {
                                    InlineKeyboardButton.WithCallbackData(text: "✨ Меню ✨", callbackData: "CMenu"),
                                },
                            });
                            await client.SendTextMessageAsync(
                                        chatId: chatId,
                                        text: $"🤷🏻‍♀️ Корзина пуста...Положи в неё что-нибудь ⬇️",
                                        parseMode: ParseMode.Html,
                                        replyMarkup: inlineKeyboard,
                                        cancellationToken: cancellationToken);
                            return;
                        }

                        InlineKeyboardMarkup inlineKeyboardMarkup = new(new[]
                        {
                            new[]
                            {
                                InlineKeyboardButton.WithCallbackData(text: "💳 Способ оплаты", callbackData: "DPaymentMethod"),
                                InlineKeyboardButton.WithCallbackData(text: "🛸 Способ доставки", callbackData: "DReceiptMethod"),
                            },
                                                        new[]
                            {
                                InlineKeyboardButton.WithCallbackData(text: "❌ Удалить товар", callbackData: "DRemoveProduct"),
                            },
                            new[]
                            {
                                InlineKeyboardButton.WithCallbackData(text: "🤝 Подтвердить заказ", callbackData: "DOrderConfirm"),
                            },
                        });


                        await client.SendTextMessageAsync(
                                chatId: chatId,
                                text: message,
                                parseMode: ParseMode.Html,
                                replyMarkup: inlineKeyboardMarkup,
                                cancellationToken: cancellationToken);

                    }
                }
                catch (Exception)
                {
                    await ExeptionMessage(chatId, client, cancellationToken);
                }
            }
            if (update.CallbackQuery.Data == "CMenu")
            {
                var command = new MenuCommand();
                await command.Execute(update, client, cancellationToken);
            }
        }

        private async Task<ProductDTO> GetProduct(Update update, ITelegramBotClient client, CancellationToken cancellationToken)
        {
            var chatId = update.CallbackQuery.Message.Chat.Id;
            var productData = update.CallbackQuery.Message.Caption;
            var ch = '\n';
            var productId = productData.Substring(4, productData.IndexOf(ch) - 4);
            int id = Convert.ToInt32(productId.Trim());
            ProductDTO product;

            try
            {
                using (ShopContext context = new ShopContext())
                {
                    UnitOfWork _unit = new UnitOfWork(context);
                    var productService = new ProductService(_unit);
                    product = await productService.GetAsync(id);
                }
                return product;
            }
            catch (Exception)
            {
                await ExeptionMessage(chatId, client, cancellationToken);
                return null;
            }
        }

        private async Task ExeptionMessage(long chatId, ITelegramBotClient client, CancellationToken cancellationToken)
        {
             await client.SendTextMessageAsync(
                chatId: chatId,
                text: "🤦🏿‍♀️ Что-то пошло не так...",
                cancellationToken: cancellationToken);
        }

        private string GetCartMessage(OrderDTO order)
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
                $"<b>💳 Способ оплаты</b>: {OrderEnumParser.PaymentMethodToString(order.PaymentMethod)}";
            return message;
        }

    }
}
