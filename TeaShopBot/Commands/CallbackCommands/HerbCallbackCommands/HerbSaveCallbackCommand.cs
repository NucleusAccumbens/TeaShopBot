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

namespace TeaShopBot.Commands.CallbackCommands.HerbCallbackCommands
{
    public class HerbSaveCallbackCommand : TelegramAddProductCallbackCommand
    {
        public override char CallbackDataCode => 'K';

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
                    var herbService = new HerbService(_unit);

                    await herbService.CreateAsync(herb);

                    await client.SendTextMessageAsync(
                       chatId: chatId,
                       text: $"👍🏽 Сбор успешно сохранён!",
                       cancellationToken: cancellationToken);
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
