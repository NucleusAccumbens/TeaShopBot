using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TeaShopBLL.DTO;
using TeaShopBot.Abstractions;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TeaShopBot.Commands.CallbackCommands
{
    public class AddTeaCallbackCommand : TelegramCallbackCommand
    {
        public override char CallbackDataCode => 'T';

        public override async Task<ProductDTO> CallbackExecute(Update update, ITelegramBotClient client, CancellationToken cancellationToken, TeaDTO tea)
        {
            await AddTeaCommand.TeaTypeCallbackExecute(update, client, cancellationToken, tea);
            //tea = await AddTeaCommand.TeaWeighteCallbackExecute(update, client, cancellationToken, tea);
            //tea = await AddTeaCommand.TeaFormCallbackExecute(update, client, cancellationToken, tea);
            return tea;
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
