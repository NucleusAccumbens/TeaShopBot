using DATABASE.Enums;
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
using Telegram.Bot.Types.Enums;

namespace TeaShopBot.Commands.CallbackCommands
{
    public class TeaTypeCallbackCommand : TelegramAddProductCallbackCommand
    {
        public override char CallbackDataCode => 'T';

        public override bool Contains(CallbackQuery message)
        {
            char code = message.Data.ToString().FirstOrDefault();
            
            if (code != CallbackDataCode)
                return false;
            
            return message.Data.Contains(CallbackDataCode);
        }

        public override async Task CallbackExecute(Update update, ITelegramBotClient client, CancellationToken cancellationToken, TeaDTO tea, HerbDTO herb, HoneyDTO honey)
        {

            if (update.CallbackQuery.Data != null)
            {
                var chatId = update.CallbackQuery.Message.Chat.Id;

                if (update.CallbackQuery.Data == "TКрасный")
                {
                    tea.TeaType = TeaTypes.Red;
                    await SetTeaName(update, client, cancellationToken, tea);
                }
                if (update.CallbackQuery.Data == "TЗелёный")
                {
                    tea.TeaType = TeaTypes.Green;
                    await SetTeaName(update, client, cancellationToken, tea);
                }
                if (update.CallbackQuery.Data == "TБелый")
                {
                    tea.TeaType = TeaTypes.White;
                    await SetTeaName(update, client, cancellationToken, tea);
                }
                if (update.CallbackQuery.Data == "TУлун")
                {
                    tea.TeaType = TeaTypes.Oolong;
                    await SetTeaName(update, client, cancellationToken, tea);
                }
                if (update.CallbackQuery.Data == "TШу пуэр")
                {
                    tea.TeaType = TeaTypes.ShuPuer;
                    await SetTeaName(update, client, cancellationToken, tea);
                }
                if (update.CallbackQuery.Data == "TШен пуэр")
                {
                    tea.TeaType = TeaTypes.ShenPuer;
                    await SetTeaName(update, client, cancellationToken, tea);
                }
            }
        }

        private static async Task<TeaDTO> SetTeaName(Update update, ITelegramBotClient client, CancellationToken cancellationToken, TeaDTO tea)
        {
            var chatId = update.CallbackQuery.Message.Chat.Id;
            await client.SendTextMessageAsync(
                        chatId: chatId,
                        text: $"Сорт чая: {TeaEnumParser.TeaTypeToString(tea.TeaType)}\n\n" +
                        $"Чтобы установить название чая, отправь сообщение:\n" +
                        $"<b>Название чая</b>: <i>какое-то название...</i>",
                        parseMode: ParseMode.Html,
                        cancellationToken: cancellationToken); 
            return tea;
        }
    }
}
