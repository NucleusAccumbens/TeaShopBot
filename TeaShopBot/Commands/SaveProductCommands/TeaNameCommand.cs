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

namespace TeaShopBot.Commands.SaveProductCommands
{
    public class TeaNameCommand : TelegramSaveProductCommand
    {
        public override string Name => @"Название чая: ";

        public override bool Contains(Message message)
        {
            if (message.Type != MessageType.Text)
                return false;
            string mes = message.Text;

            return mes.Contains(Name);
        }

        public override async Task<ProductDTO> Execute(Update update, ITelegramBotClient client, CancellationToken cancellationToken, ProductDTO tea)
        {
            tea.ProductName = update.Message.Text.Substring(13);
            var chatId = update.Message.Chat.Id;
            await client.SendTextMessageAsync(
                        chatId: chatId,
                        text: $"Сорт чая: {(tea as TeaDTO).TeaType}\n" +
                              $"Название чая: {tea.ProductName}\n\n" +
                              $"Теперь отправь сообщение с описанием чая: \n" +
                              $"<<Описание: какое-то описание...",
                        cancellationToken: cancellationToken);
            return tea;
        }
    }
}
