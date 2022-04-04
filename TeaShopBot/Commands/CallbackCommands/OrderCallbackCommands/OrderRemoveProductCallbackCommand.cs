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
using Telegram.Bot.Types.ReplyMarkups;

namespace TeaShopBot.Commands.CallbackCommands.OrderCallbackCommands
{
    public class OrderRemoveProductCallbackCommand : TelegramCallbackCommand
    {
        public override char CallbackDataCode => 'G';

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
                    long productId = Convert.ToInt64(update.CallbackQuery.Data.Substring(1));
                    await ChangeProductCount(productId);
                    OrderDTO order;

                    using (ShopContext context = new ShopContext())
                    {
                        UnitOfWork unit = new UnitOfWork(context);
                        var orderServise = new OrderService(unit);
                        order = await orderServise.GetActiveOrderAsync(chatId);
                    }

                    using (ShopContext context = new ShopContext())
                    {
                        UnitOfWork unit = new UnitOfWork(context);
                        var orderServise = new OrderService(unit);
                        string productName = await orderServise.DeleteProductFromOrderAsync(chatId, productId);

                        InlineKeyboardMarkup inlineKeyboard = new(new[]
                        {
                            new[]
                            {
                                InlineKeyboardButton.WithCallbackData(text: "🛒 Корзина 🛒", callbackData: "CCart"),
                            },
                        });

                        await client.SendTextMessageAsync(
                            chatId: chatId,
                            text: $"👍🏽 <b>{productName}</b> удалён из корзины!",
                            parseMode: ParseMode.Html,
                            replyMarkup: inlineKeyboard,
                            cancellationToken: cancellationToken);
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

        private async Task ChangeProductCount(long productId)
        {
            try
            {
                using (ShopContext context = new ShopContext())
                {
                    UnitOfWork repo = new UnitOfWork(context);
                    var product = await repo.Products.GetAsync(productId);
                    product.ProductCount += 1;
                    await repo.Products.UpdateAsync(product);
                }
            }
            catch(Exception)
            {
                throw;
            }
        }

    }
}
