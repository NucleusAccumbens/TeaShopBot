using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TeaShopBot.Abstractions;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace TeaShopBot.Commands.CallbackCommands.OrderCallbackCommands
{
    public class OrderContactUserCallbackCommand : TelegramCallbackCommand
    {
        public override char CallbackDataCode => 'H';
        public override bool Contains(CallbackQuery message)
        {
            char code = message.Data.ToString().FirstOrDefault();

            if (code != CallbackDataCode)
                return false;

            return message.Data.Contains(CallbackDataCode);
        }

        public override async Task CallbackExecute(Update update, ITelegramBotClient client, CancellationToken cancellationToken)
        {
            var userChatId = update.CallbackQuery.Data.Substring(1);
            var chatId = update.CallbackQuery.Message.Chat.Id;

            await client.GetChatMemberAsync(
                chatId: chatId,
                userId: Convert.ToInt64(userChatId),
                cancellationToken: cancellationToken);
        }
    }
}
