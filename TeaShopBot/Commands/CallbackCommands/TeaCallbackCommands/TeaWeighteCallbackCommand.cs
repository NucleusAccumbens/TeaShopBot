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
    public class TeaWeighteCallbackCommand : TelegramAddProductCallbackCommand
    {
        public override char CallbackDataCode => 'W';
        public override bool Contains(CallbackQuery message)
        {
            char code = message.Data.ToString().FirstOrDefault();

            if (code != CallbackDataCode)
                return false;

            return message.Data.Contains(CallbackDataCode);
        }

        public override async Task<ProductDTO> CallbackExecute(Update update, ITelegramBotClient client, CancellationToken cancellationToken, ProductDTO tea)
        {
            var chatId = update.CallbackQuery.Message.Chat.Id;

            if (update.CallbackQuery.Data != null)
            {
                if (update.CallbackQuery.Data == "W50")
                {
                    (tea as TeaDTO).TeaWeight = TeaWeight.Fifty;
                    await SetTeaForm(update, client, cancellationToken, tea as TeaDTO);
                    return tea;
                }
                if (update.CallbackQuery.Data == "W100")
                {
                    (tea as TeaDTO).TeaWeight = TeaWeight.OneHundred;
                    await SetTeaForm(update, client, cancellationToken, tea as TeaDTO);
                    return tea;
                }
                if (update.CallbackQuery.Data == "W150")
                {
                    (tea as TeaDTO).TeaWeight = TeaWeight.OneHundredFifty;
                    await SetTeaForm(update, client, cancellationToken, tea as TeaDTO);
                    return tea;
                }
                if (update.CallbackQuery.Data == "W200")
                {
                    (tea as TeaDTO).TeaWeight = TeaWeight.TwoHundred;
                    await SetTeaForm(update, client, cancellationToken, tea as TeaDTO);
                    return tea;
                }
                if (update.CallbackQuery.Data == "W250")
                {
                    (tea as TeaDTO).TeaWeight = TeaWeight.TwoHundredFifty;
                    await SetTeaForm(update, client, cancellationToken, tea as TeaDTO);
                    return tea;
                }
                if (update.CallbackQuery.Data == "W357")
                {
                    (tea as TeaDTO).TeaWeight = TeaWeight.ThreeHundredFiftySeven;
                    await SetTeaForm(update, client, cancellationToken, tea as TeaDTO);
                    return tea;
                }
            }
            return tea;
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
