using DATABASE.DataContext;
using DATABASE.Enums;
using DATABASE.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TeaShopBLL.DTO;
using TeaShopBLL.Services;
using TeaShopBot.Abstractions;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace TeaShopBot.Commands.HoneyCommands
{
    public class HoneyDescriptionCommand : TelegramCreateProductCommand
    {
        public override string Name => "Описание меда: ";

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

                honey.ProductDescription = update.Message.Text.Substring(15);

                InlineKeyboardMarkup inlineKeyboardMarkup = new(new[]
                {
                new[]
                {
                    InlineKeyboardButton.WithCallbackData(text: "350 грамм", callbackData: "w350"),
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData(text: "950 грамм", callbackData: "w950"),
                },
                });

                await client.SendTextMessageAsync(
                            chatId: chatId,
                            text: $"Название мёда: {honey.ProductName}\n" +
                            $"Описание мёда: {honey.ProductDescription}\n\n" +
                            $"Далее выбери вес мёда: ",
                            parseMode: ParseMode.Html,
                            replyMarkup: inlineKeyboardMarkup,
                            cancellationToken: cancellationToken);
            }
            catch (Exception)
            {
                await client.SendTextMessageAsync(
                chatId: chatId,
                text: $"🤦🏿‍♀️ Что-то пошло не так...",
                cancellationToken: cancellationToken);
            }
        }
    }
}
