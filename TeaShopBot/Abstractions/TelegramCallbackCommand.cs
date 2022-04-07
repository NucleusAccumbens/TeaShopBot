using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TeaShopBot.Abstractions
{
    public abstract class TelegramCallbackCommand
    {
        public abstract char CallbackDataCode { get; }
        public abstract Task CallbackExecute(Update update, ITelegramBotClient client, CancellationToken cancellationToken);
        public abstract bool Contains(CallbackQuery message);
    }
}
