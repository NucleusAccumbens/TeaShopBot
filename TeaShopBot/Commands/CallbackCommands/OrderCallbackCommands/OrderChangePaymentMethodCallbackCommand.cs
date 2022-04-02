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
    internal class OrderChangePaymentMethodCallbackCommand : TelegramCallbackCommand
    {
        public override char CallbackDataCode => 'E';

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
                if (update.CallbackQuery.Data == "ERemittance")
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

                            userOrder.PaymentMethod = PaymentMethods.Remittance;
                            await orderServise.UpdateAsync(userOrder);

                            await client.SendTextMessageAsync(
                                chatId: chatId,
                                text: $"👍🏽 Способ оплаты изменён на: <b>{OrderEnumParser.PaymentMethodToString(userOrder.PaymentMethod)}</b>",
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
                if (update.CallbackQuery.Data == "ECash")
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

                            userOrder.PaymentMethod = PaymentMethods.Cash;
                            await orderServise.UpdateAsync(userOrder);

                            await client.SendTextMessageAsync(
                                chatId: chatId,
                                text: $"👍🏽 Способ оплаты изменён на: <b>{OrderEnumParser.PaymentMethodToString(userOrder.PaymentMethod)}</b>",
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
