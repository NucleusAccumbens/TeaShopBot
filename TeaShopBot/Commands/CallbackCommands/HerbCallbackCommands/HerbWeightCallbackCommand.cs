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
    public class HerbWeightCallbackCommand : TelegramAddProductCallbackCommand
    {
        public override char CallbackDataCode => 'I';

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
                if (update.CallbackQuery.Data == "I50")
                {
                    herb.Weight = HerbsWeight.Fifty;
                    await SetHerbPrice(chatId, client, cancellationToken, herb);
                }
                if (update.CallbackQuery.Data == "I100")
                {
                    herb.Weight = HerbsWeight.OneHundred;
                    await SetHerbPrice(chatId, client, cancellationToken, herb);
                }
                if (update.CallbackQuery.Data == "I150")
                {
                    herb.Weight = HerbsWeight.OneHundredFifty;
                    await SetHerbPrice(chatId, client, cancellationToken, herb);
                }
                if (update.CallbackQuery.Data == "I200")
                {
                    herb.Weight = HerbsWeight.TwoHundred;
                    await SetHerbPrice(chatId, client, cancellationToken, herb);
                }
                if (update.CallbackQuery.Data == "I250")
                {
                    herb.Weight = HerbsWeight.TwoHundredFifty;
                    await SetHerbPrice(chatId, client, cancellationToken, herb);
                }
            }
        }

        private static async Task SetHerbPrice(long chatId, ITelegramBotClient client, CancellationToken cancellationToken, HerbDTO herb)
        {
            await client.SendTextMessageAsync(
                        chatId: chatId,
                        text: $"Регион: {HerbsEnumParser.HerbsRegionToString(herb.Region)}\n" +
                        $"Название сбора: {herb.ProductName}\n" +
                        $"Описание сбора: {herb.ProductDescription}\n" +
                        $"Вес сбора: {HerbsEnumParser.HerbsWeightToString(herb.Weight)}\n\n" +
                        $"Теперь укажи цену сбора: \n" +
                        $"<b>Цена сбора</b>: <i>какая-то цифра...</i>",
                        parseMode: ParseMode.Html,
                        cancellationToken: cancellationToken);
        }
    }
}
