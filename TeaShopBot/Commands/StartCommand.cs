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
                    new KeyboardButton[] { "🎁 Добавить товар", "📃 История заказов" },
                    new KeyboardButton[] { "✏️ Редактировать", "👨‍👨‍👦 Список пользователей" },
                    new KeyboardButton[] { "✨ Меню ✨" },
                })
                {
                    ResizeKeyboard = true
                };

                await botClient.SendTextMessageAsync(
                    chatId: chatId,
                    text: "Привет, админ! 🖖🏻\n\n" +
                    "Управление ботом доступно только администраторам.\n" +
                    "Другие пользователи видят только кнопку ✨ Mеню ✨.\n\n" +
                    "🔥 Используй соответствующие кнопки, чтобы добавить товар, " +
                    "посмотреть историю заказов или список пользователей, " +
                    "а также чтобы редактировать информацию о товарах, доставке, скидках и т.д. 🔥",
                    replyMarkup: replyKeyboardMarkup,
                    cancellationToken: cancellationToken);
            }
            else
            {
                ReplyKeyboardMarkup replyKeyboardMarkup = new(new[]
                {
                    new KeyboardButton[] { "✨ Меню ✨" },
                })
                {
                    ResizeKeyboard = true
                };

                await botClient.SendTextMessageAsync(
                    chatId: chatId,
                    text: "Привет, Дорогой чайный друг🦊!\n" +
                    "Добро пожаловать🌞  в наш чайный бот!\n" +
                    "Выбирай чаи на любой вкус!🙏🏻⛩🙏🏻\n\n" +
                    "Чтобы ознакомиться с ассортиментом и совершить покупку, переходи в\n" +
                    " ✨ Mеню ✨ ⬇️",
                    replyMarkup: replyKeyboardMarkup,
                    cancellationToken: cancellationToken);
            } 
        }
    }
}
