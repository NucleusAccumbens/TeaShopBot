using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace TeaShopBot.Abstractions
{
    public interface IMessageService
    {
        void AnalyzeTextMessage(Message message);

    }
}
