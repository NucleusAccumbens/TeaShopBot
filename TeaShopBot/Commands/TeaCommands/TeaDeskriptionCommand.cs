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
    public class TeaDeskriptionCommand : TelegramCreateProductCommand
    {
        public override string Name => @"Описание чая: ";

        public override bool Contains(Message message)
        {
            if (message.Type != MessageType.Text)
                return false;
            string mes = message.Text;

            return mes.Contains(Name);
        }

        public override async Task<ProductDTO> Execute(Update update, ITelegramBotClient client, CancellationToken cancellationToken, ProductDTO tea)
        {
            tea.ProductDescription = update.Message.Text.Substring(14);
            var chatId = update.Message.Chat.Id;

            InlineKeyboardMarkup inlineKeyboardMarkup = new(new[]
            {
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData(text: "50 грамм", callbackData: "W50"),
                            InlineKeyboardButton.WithCallbackData(text: "100 грамм", callbackData: "W100"),
                            InlineKeyboardButton.WithCallbackData(text: "150 грамм", callbackData: "W150"),
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData(text: "200 грамм", callbackData: "W200"),
                            InlineKeyboardButton.WithCallbackData(text: "250 грамм", callbackData: "W250"),
                            InlineKeyboardButton.WithCallbackData(text: "375 грамм", callbackData: "W375"),
                        },
                    }); 

            await client.SendTextMessageAsync(
                chatId: chatId,
                text: $"Сорт чая: {TeaEnumParser.TeaTypeToString((tea as TeaDTO).TeaType)}\n" +
                $"Название чая: {tea.ProductName}\n" +
                $"Описание чая: {tea.ProductDescription}\n\n" +
                $"Далее выбери вес чая: ",
                replyMarkup: inlineKeyboardMarkup,
                cancellationToken: cancellationToken);
            return tea;
           }    
        }
}
