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

namespace TeaShopBot.Commands.CallbackCommands.HerbCallbackCommands
{
    public class HerbRegionCallbackCommand : TelegramAddProductCallbackCommand
    {
        public override char CallbackDataCode => 'h';
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

                if (update.CallbackQuery.Data == "hКарелия")
                {
                    herb.Region = HerbsRegion.Karelia;
                    await SetHerbName(update, client, cancellationToken, herb);
                }
                if (update.CallbackQuery.Data == "hКавказ")
                {
                    herb.Region = HerbsRegion.Caucasus;
                    await SetHerbName(update, client, cancellationToken, herb);
                }
                if (update.CallbackQuery.Data == "hАлтай")
                {
                    herb.Region = HerbsRegion.Altai;
                    await SetHerbName(update, client, cancellationToken, herb);
                }
                if (update.CallbackQuery.Data == "hСибирь")
                {
                    herb.Region = HerbsRegion.Siberia;
                    await SetHerbName(update, client, cancellationToken, herb);
                }
               
            }
        }

        private static async Task<HerbDTO> SetHerbName(Update update, ITelegramBotClient client, CancellationToken cancellationToken, HerbDTO herb)
        {
            var chatId = update.CallbackQuery.Message.Chat.Id;
            await client.SendTextMessageAsync(
                        chatId: chatId,
                        text: $"Регион: {HerbsEnumParser.HerbsRegionToString(herb.Region)}\n\n" +
                        $"Чтобы установить название сбора, отправь сообщение:\n" +
                        $"<b>Название сбора</b>: <i>какое-то название...</i>",
                        parseMode: ParseMode.Html,
                        cancellationToken: cancellationToken);
            return herb;
        }
    }
}
