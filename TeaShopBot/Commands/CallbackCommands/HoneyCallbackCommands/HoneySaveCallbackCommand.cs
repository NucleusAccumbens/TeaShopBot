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

namespace TeaShopBot.Commands.CallbackCommands.HoneyCallbackCommands
{
    public class HoneySaveCallbackCommand : TelegramAddProductCallbackCommand
    {
        public override char CallbackDataCode => 'M';

        public override bool Contains(CallbackQuery message)
        {
            char code = message.Data.ToString().FirstOrDefault();

            if (code != CallbackDataCode)
                return false;

            return message.Data.Contains(CallbackDataCode);
        }

        public override async Task CallbackExecute(Update update, ITelegramBotClient client, CancellationToken cancellationToken, TeaDTO tea, HerbDTO herb, HoneyDTO honey)
        {
            var chatId = update.CallbackQuery.Message.Chat.Id;
            using (ShopContext context = new ShopContext())
            {
                try
                {
                    long id = update.CallbackQuery.Message.Chat.Id;
                    UnitOfWork _unit = new UnitOfWork(context);
                    var honeyService = new HoneyService(_unit);

                    await honeyService.CreateAsync(honey);

                    await client.SendTextMessageAsync(
                       chatId: chatId,
                       text: $"👍🏽 Мёд успешно сохранён!",
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
