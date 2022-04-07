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
    public class OrderChangeReceiptMethodCallbackCommand : TelegramCallbackCommand
    {
        public override char CallbackDataCode => 'F';

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
                if (update.CallbackQuery.Data == "FPickup")
                {
                    try
                    {
                        var userOrder = new OrderDTO();

                        using (ShopContext context = new ShopContext())
                        {
                            UnitOfWork unit = new UnitOfWork(context);
                            var orderServise = new OrderService(unit);
                            userOrder = await orderServise.GetActiveOrderAsync(chatId);
                        }

                        using (ShopContext context = new ShopContext())
                        {
                            UnitOfWork unit = new UnitOfWork(context);
                            var orderServise = new OrderService(unit);

                            userOrder.ReceiptMethod = ReceiptMethods.Pickup;
                            await orderServise.UpdateAsync(userOrder);

                            await client.SendTextMessageAsync(
                                chatId: chatId,
                                text: $"👍🏽 Способ доставки изменён на: <b>{OrderEnumParser.ReceiptMethodToString(userOrder.ReceiptMethod)}</b>",
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
                if (update.CallbackQuery.Data == "FSDAK")
                {
                    try
                    {
                        var userOrder = new OrderDTO();

                        using (ShopContext context = new ShopContext())
                        {
                            UnitOfWork unit = new UnitOfWork(context);
                            var orderServise = new OrderService(unit);
                            userOrder = await orderServise.GetActiveOrderAsync(chatId);
                        }

                        using (ShopContext context = new ShopContext())
                        {
                            UnitOfWork unit = new UnitOfWork(context);
                            var orderServise = new OrderService(unit);

                            userOrder.ReceiptMethod = ReceiptMethods.CDEK;
                            await orderServise.UpdateAsync(userOrder);

                            await client.SendTextMessageAsync(
                                chatId: chatId,
                                text: $"👍🏽 Способ доставки изменён на: <b>{OrderEnumParser.ReceiptMethodToString(userOrder.ReceiptMethod)}</b>",
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
