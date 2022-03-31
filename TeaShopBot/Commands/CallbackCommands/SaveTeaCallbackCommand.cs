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
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TeaShopBot.Commands.CallbackCommands
{
    public class SaveTeaCallbackCommand : TelegramAddProductCallbackCommand
    {
        public override char CallbackDataCode => 'S';
        public override bool Contains(CallbackQuery message)
        {
            char code = message.Data.ToString().FirstOrDefault();

            if (code != CallbackDataCode)
                return false;

            return message.Data.Contains(CallbackDataCode);
        }

        public override async Task CallbackExecute(Update update, ITelegramBotClient client, CancellationToken cancellationToken, ProductDTO tea)
        {
            var chatId = update.CallbackQuery.Message.Chat.Id;
            using (ShopContext context = new ShopContext())
            {
                try
                {
                    long id = update.CallbackQuery.Message.Chat.Id;
                    UnitOfWork _unit = new UnitOfWork(context);
                    var teaService = new TeaService(_unit);

                     await teaService.CreateAsync(tea as TeaDTO);

                     await client.SendTextMessageAsync(
                        chatId: chatId,
                        text: $"👍🏽 Чай успешно сохранён!",
                        cancellationToken: cancellationToken);
                }
                catch (Exception)
                {
                    await client.SendTextMessageAsync(
                        chatId: chatId,
                        text: $"Что-то пошло не так...",
                        cancellationToken: cancellationToken);
                }
            }
        }
    }
}
