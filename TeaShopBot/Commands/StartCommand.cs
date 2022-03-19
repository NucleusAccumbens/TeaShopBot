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

namespace TeaShopBot.Commands
{
    public class StartCommand : TelegramCommand
    {
        public override string Name => @"/start";

        public override bool Contains(Message message)
        {
            if (message.Type != MessageType.Text)
                return false;

            return message.Text.Contains(Name);
        }

        public override async Task Execute(Update update, ITelegramBotClient botClient, CancellationToken cancellationToken)
        {
            var chatId = update.Message.Chat.Id;
            ITelegramBot bot = new TelegramBot();
            bool isAdmin = await bot.CheckUserIsAdmin(chatId);

            if (isAdmin == true)
            {
                ReplyKeyboardMarkup replyKeyboardMarkup = new(new[]
                {
                    new KeyboardButton[] { "Добавить товар", "Посмотреть историю заказов" },
                    new KeyboardButton[] { "Редактировать", "Посмотреть список пользователей" },
                    new KeyboardButton[] { "Главное меню" },
                })
                {
                    ResizeKeyboard = true
                };

                await botClient.SendTextMessageAsync(
                    chatId: chatId,
                    text: "Привет, Дорогой чайный друг🦊!\n" +
                    "Добро пожаловать🌞  в наш чайный бот!\n" +
                    "Выбирай чаи на любой вкус!🙏🏻⛩🙏🏻",
                    replyMarkup: replyKeyboardMarkup,
                    cancellationToken: cancellationToken);
            }
            else
            {
                ReplyKeyboardMarkup replyKeyboardMarkup = new(new[]
                {
                    new KeyboardButton[] { "Меню" },
                })
                {
                    ResizeKeyboard = true
                };

                await botClient.SendTextMessageAsync(
                    chatId: chatId,
                    text: "Чай в ассортименте!",
                    replyMarkup: replyKeyboardMarkup,
                    cancellationToken: cancellationToken);
            } 
        }
    }
}
