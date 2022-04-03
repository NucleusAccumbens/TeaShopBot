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
    public class TeaFormCallbackCommand : TelegramAddProductCallbackCommand
    {
        public override char CallbackDataCode => 'F';

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

            if (tea == null || tea is HerbDTO || tea is HoneyDTO)
            {
                tea = new TeaDTO();
            }

            if (update.CallbackQuery.Data != null)
            {
                if (update.CallbackQuery.Data == "FПресованный")
                {
                    (tea as TeaDTO).TeaForm = TeaForms.Pressed;
                    await client.SendTextMessageAsync(
                        chatId: chatId,
                        text: $"Сорт чая: {TeaEnumParser.TeaTypeToString((tea as TeaDTO).TeaType)}\n" +
                        $"Название чая: {tea.ProductName}\n" +
                        $"Описание чая: {tea.ProductDescription}\n" +
                        $"Вес чая: {TeaEnumParser.TeaWeightToString((tea as TeaDTO).TeaWeight)}\n" +
                        $"Форма хранения чая: {TeaEnumParser.TeaFormToString((tea as TeaDTO).TeaForm)}\n\n" +
                        $"Теперь укажи цену чая: \n" +
                        $"<b>Цена чая</b>: <i>какя-то цифра...</i>",
                        parseMode: ParseMode.Html,
                        cancellationToken: cancellationToken);
                    return tea;
                }
                if (update.CallbackQuery.Data == "FРассыпной")
                {
                    (tea as TeaDTO).TeaForm = TeaForms.Loose;
                    await client.SendTextMessageAsync(
                        chatId: chatId,
                        text: $"Сорт чая: {TeaEnumParser.TeaTypeToString((tea as TeaDTO).TeaType)}\n" +
                        $"Название чая: {tea.ProductName}\n" +
                        $"Описание чая: {tea.ProductDescription}\n" +
                        $"Вес чая: {TeaEnumParser.TeaWeightToString((tea as TeaDTO).TeaWeight)}\n" +
                        $"Форма хранения чая: {TeaEnumParser.TeaFormToString((tea as TeaDTO).TeaForm)}\n\n" +
                        $"Теперь укажи цену чая: \n" +
                        $"<b>Цена чая</b>: <i>какя-то цифра...</i>",
                        parseMode: ParseMode.Html,
                        cancellationToken: cancellationToken);
                    return tea;
                }
            }
            return tea;
        }
    }
}
