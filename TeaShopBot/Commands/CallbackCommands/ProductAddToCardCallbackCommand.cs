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
using TeaShopDAL.Enums;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

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
                    var tea = await GetTea(update, client, cancellationToken);

                    try
                    {
                        using (ShopContext context = new ShopContext())
                        {
                            UnitOfWork _unit = new UnitOfWork(context);
                            //var teaService = new TeaService(_unit);
                            //var tea = await teaService.GetAsync(id);

                            var order = new OrderDTO()
                            {
                                IsDeleted = false,
                                OrderNumber = 1,
                                OrderStatus = true,
                                Comment = "...",
                                PaymentMethod = PaymentMethods.Cash,
                                ReceiptMethod = ReceiptMethods.Pickup,
                                UserChatId = chatId,
                                Products = new List<ProductDTO>(),
                            };
                            order.Products.Add(tea);

                            var orderService = new OrderService(_unit);
                            await orderService.CreateAsync(order);

                            await client.SendTextMessageAsync(
                                chatId: chatId,
                                text: $"✅ Товар <b>{tea.ProductName}</b> добавлен в корозину!",
                                parseMode: ParseMode.Html,
                                cancellationToken: cancellationToken);
                        }
                    }
                    catch(Exception ex)
                    {
                        await client.SendTextMessageAsync(
                            chatId: chatId,
                            text: ex.Message,
                            cancellationToken: cancellationToken);
                    }    
                }
            }
        }

        private async Task<TeaDTO> GetTea(Update update, ITelegramBotClient client, CancellationToken cancellationToken)
        {
            var chatId = update.CallbackQuery.Message.Chat.Id;
            var productData = update.CallbackQuery.Message.Caption;
            var ch = '\n';
            var productId = productData.Substring(4, productData.IndexOf(ch) - 4);
            productId.Trim();
            int id = Convert.ToInt32(productId);
            var tea = new TeaDTO();

            try
            {
                using (ShopContext context = new ShopContext())
                {
                    UnitOfWork _unit = new UnitOfWork(context);
                    var teaService = new TeaService(_unit);
                    tea = await teaService.GetAsync(id);

                    await client.SendTextMessageAsync(
                        chatId: chatId,
                        text: $"✅ Товар <b>{tea.ProductName}</b>, с кодом <b>{tea.ProductId}</b> получен из базы данных!",
                        parseMode: ParseMode.Html,
                        cancellationToken: cancellationToken);

                }
                return tea;
            }
            catch (Exception ex)
            {
                await client.SendTextMessageAsync(
                chatId: chatId,
                text: ex.Message,
                cancellationToken: cancellationToken);

                return tea;
            }
        }

    }
}
