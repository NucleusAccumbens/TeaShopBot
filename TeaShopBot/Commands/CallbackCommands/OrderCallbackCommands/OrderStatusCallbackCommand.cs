using DATABASE.DataContext;
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

namespace TeaShopBot.Commands.CallbackCommands.OrderCallbackCommands
{
    public class OrderStatusCallbackCommand : TelegramCallbackCommand
    {
        public override char CallbackDataCode => 'O';

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
                if (update.CallbackQuery.Data.Contains("OArchive"))
                {
                    try
                    {
                        long orderId = Convert.ToInt64(update.CallbackQuery.Data.Substring(8));
                        var order = new OrderDTO();
                        var products = new List<ProductDTO>();

                        using (ShopContext context = new ShopContext())
                        {
                            UnitOfWork unit = new UnitOfWork(context);
                            var orderServise = new OrderService(unit);
                            order = await orderServise.GetByOrderIdAsync(orderId);
                            products = order.Products;
                        }

                        using (ShopContext context = new ShopContext())
                        {
                            UnitOfWork unit = new UnitOfWork(context);
                            var orderServise = new OrderService(unit);

                            order.OrderStatus = false;
                            await orderServise.UpdateAsync(order);

                            await client.SendTextMessageAsync(
                                chatId: chatId,
                                text: $"👍🏽 Заказ перемещён в архив!",
                                parseMode: ParseMode.Html,
                                cancellationToken: cancellationToken);
                        }                      
                    }
                    catch (Exception ex)
                    {
                        await client.SendTextMessageAsync(
                            chatId: chatId,
                            text: $"🤦🏿‍♀️ Что-то пошло не так..." +
                            $"{ex.Message}",
                            cancellationToken: cancellationToken);
                    }
                }
                if (update.CallbackQuery.Data.Contains("OActive"))
                {
                    try
                    {
                        long orderId = Convert.ToInt64(update.CallbackQuery.Data.Substring(7));
                        var order = new OrderDTO();
                        var products = new List<ProductDTO>();

                        using (ShopContext context = new ShopContext())
                        {
                            UnitOfWork unit = new UnitOfWork(context);
                            var orderServise = new OrderService(unit);
                            order = await orderServise.GetByOrderIdAsync(orderId);
                            products = order.Products;
                        }

                        using (ShopContext context = new ShopContext())
                        {
                            UnitOfWork unit = new UnitOfWork(context);
                            var orderServise = new OrderService(unit);

                            order.OrderStatus = true;
                            await orderServise.UpdateAsync(order);

                            await client.SendTextMessageAsync(
                                chatId: chatId,
                                text: $"👍🏽 Статус заказа изменён на Активен!",
                                parseMode: ParseMode.Html,
                                cancellationToken: cancellationToken);
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
