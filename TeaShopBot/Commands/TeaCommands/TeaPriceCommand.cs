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

namespace TeaShopBot.Commands.TeaCommands
{
    public class TeaPriceCommand : TelegramCreateProductCommand
    {
        public override string Name => @"Цена чая: ";

        public override bool Contains(Message message)
        {
            if (message.Type != MessageType.Text)
                return false;
            string mes = message.Text;

            return mes.Contains(Name);
        }

        public override async Task Execute(Update update, ITelegramBotClient client, CancellationToken cancellationToken, TeaDTO tea, HerbDTO herb, HoneyDTO honey)
        {
            var chatId = update.Message.Chat.Id;

            try
            {
                tea.ProductPrice = Convert.ToDecimal(update.Message.Text.Substring(10));
                await client.SendTextMessageAsync(
                            chatId: chatId,
                            text: $"Сорт чая: {TeaEnumParser.TeaTypeToString(tea.TeaType)}\n" +
                            $"Название чая: {tea.ProductName}\n" +
                            $"Описание чая: {tea.ProductDescription}\n" +
                            $"Вес чая: {TeaEnumParser.TeaWeightToString(tea.TeaWeight)}\n" +
                            $"Форма хранения чая: {TeaEnumParser.TeaFormToString(tea.TeaForm)}\n" +
                            $"Цена чая: {tea.ProductPrice}\n\n" +
                            $"Теперь укажи количество: \n" +
                            $"<b>Количество чая</b>: <i>какя-то цифра...</i>",
                            parseMode: ParseMode.Html,
                            cancellationToken: cancellationToken);
            }
            catch (FormatException)
            {
                await client.SendTextMessageAsync(
                            chatId: chatId,
                            text: $"🙅🏻‍♀️ Неверный формат цены! Укажи цифру.",
                            cancellationToken: cancellationToken);
            }
        }
    }
}
