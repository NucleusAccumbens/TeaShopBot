using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TeaShopBLL.DTO;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TeaShopBot.Abstractions
{
    public abstract class TelegramCreateProductCommand
    {
        public abstract string Name { get; }
        public abstract Task Execute(Update update, ITelegramBotClient client, CancellationToken cancellationToken, ProductDTO product);
        public abstract bool Contains(Message message);
    }
}
