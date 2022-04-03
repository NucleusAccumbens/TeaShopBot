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
using Telegram.Bot.Types.ReplyMarkups;

namespace TeaShopBot.Commands.TeaCommands
{
    public class TeaCountCommand : TelegramCreateProductCommand
    {
        public override string Name => @"Количество чая: ";

        public override bool Contains(Message message)
        {
            if (message.Type != MessageType.Text)
                return false;
            string mes = message.Text;

            return mes.Contains(Name);
        }

        public override async Task<ProductDTO> Execute(Update update, ITelegramBotClient client, CancellationToken cancellationToken, ProductDTO tea)
        {
            var chatId = update.Message.Chat.Id;
            if (tea == null || tea is HerbDTO || tea is HoneyDTO)
            {
                tea = new TeaDTO();
            }

            try
            {
                tea.ProductCount = Convert.ToInt32(update.Message.Text.Substring(16));
                await client.SendTextMessageAsync(
                            chatId: chatId,
                            text: $"Сорт чая: {TeaEnumParser.TeaTypeToString((tea as TeaDTO).TeaType)}\n" +
                            $"Название чая: {tea.ProductName}\n" +
                            $"Описание чая: {tea.ProductDescription}\n" +
                            $"Вес чая: {TeaEnumParser.TeaWeightToString((tea as TeaDTO).TeaWeight)}\n" +
                            $"Форма хранения чая: {TeaEnumParser.TeaFormToString((tea as TeaDTO).TeaForm)}\n" +
                            $"Цена чая: {tea.ProductPrice}\n" +
                            $"Количество чая: {tea.ProductCount}\n\n" +
                            $"Осталось только загрузить фото! Отправь фотографию с подписью:\n" +
                            $"Фото чая",
                            cancellationToken: cancellationToken);
                return tea;
            }
            catch (Exception)
            {
                await client.SendTextMessageAsync(
                            chatId: chatId,
                            text: $"Неверный формат количества! Укажи цифру.",
                            cancellationToken: cancellationToken);
                return tea;
            }
        }
    }
}
