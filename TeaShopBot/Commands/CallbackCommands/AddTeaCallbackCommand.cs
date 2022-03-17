using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TeaShopBot.Abstractions;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TeaShopBot.Commands.CallbackCommands
{
    public class AddTeaCallbackCommand : TelegramCallbackCommand
    {
        public override char CallbackDataCode => 'T';

        public override async Task CallbackExecute(Update update, ITelegramBotClient client, CancellationToken cancellationToken)
        {
            var tea = await AddTeaCommand.TeaTypeCallbackExecute(update, client, cancellationToken);
            //tea = await AddTeaCommand.SetTeaNameCallbackQuery(update, client, cancellationToken, tea);
            tea = await AddTeaCommand.TeaWeighteCallbackExecute(update, client, cancellationToken, tea);
            tea = await AddTeaCommand.TeaFormCallbackExecute(update, client, cancellationToken, tea);
            return;
        }

        public override bool Contains(CallbackQuery message)
        {
            char code = message.Data.ToString().FirstOrDefault();
            
            if (code != CallbackDataCode)
                return false;

            return message.Data.Contains(CallbackDataCode);
        }
    }
}
