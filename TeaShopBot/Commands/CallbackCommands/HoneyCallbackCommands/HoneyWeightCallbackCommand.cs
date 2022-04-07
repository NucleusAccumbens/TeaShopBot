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

namespace TeaShopBot.Commands.CallbackCommands.HoneyCallbackCommands
{
    public class HoneyWeightCallbackCommand : TelegramAddProductCallbackCommand
    {
        public override char CallbackDataCode => 'w';

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

            if (update.CallbackQuery.Data != null)
            {
                if (update.CallbackQuery.Data == "w350")
                {
                    honey.HoneyWeight = HoneyWeight.ThreeHundredFifty;
                    await SetHoneyPrice(chatId, client, cancellationToken, honey);
                }
                if (update.CallbackQuery.Data == "w950")
                {
                    honey.HoneyWeight = HoneyWeight.NineHundredFifty;
                    await SetHoneyPrice(chatId, client, cancellationToken, honey);
                }
            }
        }

        private static async Task SetHoneyPrice(long chatId, ITelegramBotClient client, CancellationToken cancellationToken, HoneyDTO honey)
        {
            await client.SendTextMessageAsync(
                        chatId: chatId,
                        text: $"Название мёда: {honey.ProductName}\n" +
                        $"Описание мёда: {honey.ProductDescription}\n" +
                        $"Вес мёда: {HoneyEnumParser.HoneyWeightToString(honey.HoneyWeight)}\n\n" +
                        $"Теперь укажи цену мёда: \n" +
                        $"<b>Цена меда</b>: <i>какая-то цифра...</i>",
                        parseMode: ParseMode.Html,
                        cancellationToken: cancellationToken);
        }

    }
}
