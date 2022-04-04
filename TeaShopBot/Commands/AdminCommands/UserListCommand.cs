using DATABASE.DataContext;
using DATABASE.Interfaces;
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
using Telegram.Bot.Types.ReplyMarkups;

namespace TeaShopBot.Commands
{
    public class UserListCommand : TelegramCommand
    {
        public override string Name => "Список пользователей";

        public override bool Contains(Message message)
        {
            if (message.Type != MessageType.Text)
                return false;

            return message.Text.Contains(Name);
        }

        public override async Task Execute(Update update, ITelegramBotClient client, CancellationToken cancellationToken)
        {
            var chatId = update.Message.Chat.Id;
            string mes = await GetMessage();

            await client.SendTextMessageAsync(
                chatId: chatId,
                text: mes,
                parseMode: ParseMode.Html,
                cancellationToken: cancellationToken);
        }

        private async Task<string> GetMessage()
        {
            string mes = "";
            using (ShopContext context = new ShopContext())
            {
                UnitOfWork unit = new UnitOfWork(context);
                var userService = new UserService(unit);
                var allUsers = await userService.GetAllAsync();

                foreach (var user in allUsers)
                {
                    if (user.IsAdmin == true) mes += "<b>Статус:</b> Админ\n";
                    mes += $"<b>Username:</b> @{user.Username}\n" +
                    $"<b>Имя:</b> {user.Firstname}\n\n";
                }
            }
            return mes; 
        }
    }
}
