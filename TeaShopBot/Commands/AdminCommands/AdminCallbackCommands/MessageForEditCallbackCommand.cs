using DATABASE.DataContext;
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

namespace TeaShopBot.Commands.AdminCommands.AdminCallbackCommands
{
    public class HoneyEditCallbackCommand : TelegramCallbackCommand
    {
        public override char CallbackDataCode => 'U';

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

                if (update.CallbackQuery.Data.Contains("UEditProductDescriptions"))
                {
                    long productId = Convert.ToInt64(update.CallbackQuery.Data.Substring(24));

                    await client.SendTextMessageAsync(
                           chatId: chatId,
                           text: $"Чтобы изменить описание, отправь сообщение с описанием в формате:\n\n" +
                           $"<b>Описание {productId}</b>: <i>какое-то описание...</i>",
                           parseMode: ParseMode.Html,
                           cancellationToken: cancellationToken);
                }
                if(update.CallbackQuery.Data.Contains("UEditProductCount"))
                {
                    long productId = Convert.ToInt64(update.CallbackQuery.Data.Substring(17));

                    await client.SendTextMessageAsync(
                           chatId: chatId,
                           text: $"Чтобы изменить количество, отправь сообщение в формате:\n\n" +
                           $"<b>Количество {productId}</b>: <i>какая-то цифра...</i>",
                           parseMode: ParseMode.Html,
                           cancellationToken: cancellationToken);
                }
                if(update.CallbackQuery.Data.Contains("UEditProductPrice"))
                {
                    long productId = Convert.ToInt64(update.CallbackQuery.Data.Substring(17));

                    await client.SendTextMessageAsync(
                           chatId: chatId,
                           text: $"Чтобы изменить цену, отправь сообщение в формате:\n\n" +
                           $"<b>Цена {productId}</b>: <i>какая-то цифра...</i>",
                           parseMode: ParseMode.Html,
                           cancellationToken: cancellationToken);
                }
                if(update.CallbackQuery.Data.Contains("UEditProductPhoto"))
                {
                    long productId = Convert.ToInt64(update.CallbackQuery.Data.Substring(17));

                    await client.SendTextMessageAsync(
                           chatId: chatId,
                           text: $"Чтобы изменить фото, отправь новую фотографию с подписью:\n\n" +
                           $"<b>Фото {productId}</b>",
                           parseMode: ParseMode.Html,
                           cancellationToken: cancellationToken);
                }
                if(update.CallbackQuery.Data.Contains("URemoveProduct"))
                {
                    long productId = Convert.ToInt64(update.CallbackQuery.Data.Substring(14));

                    try
                    {
                        using (ShopContext context = new ShopContext())
                        {
                            UnitOfWork repo = new UnitOfWork(context);
                            var productService = new ProductService(repo);
                            await productService.DeleteAsync(productId);
                        }

                        await client.SendTextMessageAsync(
                            chatId: chatId,
                            text: $"👌🏿 Продукт удалён из базы данных!",
                            cancellationToken: cancellationToken);
                    }
                    catch(Exception)
                    {
                        await GetExseptionMessage(chatId, client, cancellationToken);
                    }
                }
            }
        }

        private async Task GetExseptionMessage(long chatId, ITelegramBotClient client, CancellationToken cancellationToken)
        {
            await client.SendTextMessageAsync(
                chatId: chatId,
                text: $"🤦🏿‍♀️ Что-то пошло не так...",
                cancellationToken: cancellationToken);
        }

    }
}
