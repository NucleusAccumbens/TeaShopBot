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
using Telegram.Bot.Types.ReplyMarkups;

namespace TeaShopBot.Commands.CallbackCommands
{
    public class TeaWeightCallbackCommand : TelegramAddProductCallbackCommand
    {
        public override char CallbackDataCode => 'W';
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
                if (update.CallbackQuery.Data == "W50")
                {
                    tea.TeaWeight = TeaWeight.Fifty;
                    await SetTeaForm(update, client, cancellationToken, tea);
                }
                if (update.CallbackQuery.Data == "W100")
                {
                    tea.TeaWeight = TeaWeight.OneHundred;
                    await SetTeaForm(update, client, cancellationToken, tea);
                }
                if (update.CallbackQuery.Data == "W150")
                {
                    tea.TeaWeight = TeaWeight.OneHundredFifty;
                    await SetTeaForm(update, client, cancellationToken, tea);
                }
                if (update.CallbackQuery.Data == "W200")
                {
                    tea.TeaWeight = TeaWeight.TwoHundred;
                    await SetTeaForm(update, client, cancellationToken, tea);
                }
                if (update.CallbackQuery.Data == "W250")
                {
                    tea.TeaWeight = TeaWeight.TwoHundredFifty;
                    await SetTeaForm(update, client, cancellationToken, tea);
                }
                if (update.CallbackQuery.Data == "W357")
                {
                    tea.TeaWeight = TeaWeight.ThreeHundredFiftySeven;
                    await SetTeaForm(update, client, cancellationToken, tea);
                }
            }
        }

        private static async Task SetTeaForm(Update update, ITelegramBotClient client, CancellationToken cancellationToken, TeaDTO tea)
        {
            var chatId = update.CallbackQuery.Message.Chat.Id;

            InlineKeyboardMarkup inlineKeyboardMarkup = new(new[]
            {
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData(text: "Пресованный", callbackData: "FПресованный"),
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData(text: "Рассыпной", callbackData: "FРассыпной"),
                        },
                    });

            await client.SendTextMessageAsync(
                 chatId: chatId,
                 text: $"Сорт чая: {TeaEnumParser.TeaTypeToString(tea.TeaType)}\n" +
                 $"Название чая: {tea.ProductName}\n" +
                 $"Описание чая: {tea.ProductDescription}\n" +
                 $"Вес чая: {TeaEnumParser.TeaWeightToString(tea.TeaWeight)}\n\n" +
                 $"Теперь выбери форму хранения чая: ",
                 replyMarkup: inlineKeyboardMarkup,
                 cancellationToken: cancellationToken);
        }
    }
}
