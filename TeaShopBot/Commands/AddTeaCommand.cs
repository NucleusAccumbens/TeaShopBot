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

namespace TeaShopBot.Commands
{
    public class AddTeaCommand : TelegramCommand
    {
        public override string Name => @"Чай";

        public override bool Contains(Message message)
        {
            if (message.Type != MessageType.Text)
                return false;

            return message.Text.Contains(Name);
        }

        public override async Task Execute(Update update, ITelegramBotClient client, CancellationToken cancellationToken)
        {
            var chatId = update.Message.Chat.Id;

            InlineKeyboardMarkup inlineKeyboardMarkup = new(new[]
            {
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData(text: "Красный", callbackData: "TКрасный"),
                            InlineKeyboardButton.WithCallbackData(text: "Зелёный", callbackData: "TЗелёный"),
                            InlineKeyboardButton.WithCallbackData(text: "Белый", callbackData: "TБелый"),
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData(text: "Улун", callbackData: "TУлун"),
                            InlineKeyboardButton.WithCallbackData(text: "Шу пуэр", callbackData: "TШу пуэр"),
                            InlineKeyboardButton.WithCallbackData(text: "Шен пуэр", callbackData: "TШен пуэр"),
                        },
                    });

            await client.SendTextMessageAsync(
                chatId: chatId,
                text: "Выбери сорт чая:",
                replyMarkup: inlineKeyboardMarkup,
                cancellationToken: cancellationToken);
        }


        public static async Task<TeaDTO> TeaTypeCallbackExecute(Update update, ITelegramBotClient client, CancellationToken cancellationToken, TeaDTO tea)
        {

            if (update.CallbackQuery.Data != null)
            {
                var chatId = update.CallbackQuery.Message.Chat.Id;

                if (update.CallbackQuery.Data == "TКрасный")
                {
                    tea.TeaType = TeaTypes.Red;
                    await client.SendTextMessageAsync(
                        chatId: chatId,
                        text: "Ты выбрал 'Красный' ",
                        cancellationToken: cancellationToken);
                    await SetTeaName(update, client, cancellationToken, tea);
                    return tea;
                }
                if (update.CallbackQuery.Data == "TЗелёный")
                {
                    tea.TeaType = TeaTypes.Green;
                    await client.SendTextMessageAsync(
                        chatId: chatId,
                        text: "Ты выбрал 'Зелёный' ",
                        cancellationToken: cancellationToken);
                    await SetTeaName(update, client, cancellationToken, tea);
                    return tea;
                }
                if (update.CallbackQuery.Data == "TБелый")
                {
                    tea.TeaType = TeaTypes.White;
                    await client.SendTextMessageAsync(
                        chatId: chatId,
                        text: "Ты выбрал 'Белый' ",
                        cancellationToken: cancellationToken);
                    await SetTeaName(update, client, cancellationToken, tea);
                    return tea;
                }
                if (update.CallbackQuery.Data == "TУлун")
                {
                    tea.TeaType = TeaTypes.Oolong;
                    await client.SendTextMessageAsync(
                        chatId: chatId,
                        text: "Ты выбрал 'Улун' ",
                        cancellationToken: cancellationToken);
                    await SetTeaName(update, client, cancellationToken, tea);
                    return tea;
                }
                if (update.CallbackQuery.Data == "TШу пуэр")
                {
                    tea.TeaType = TeaTypes.ShuPuer;
                    await client.SendTextMessageAsync(
                        chatId: chatId,
                        text: "Ты выбрал 'Шу пуэр' ",
                        cancellationToken: cancellationToken);
                    await SetTeaName(update, client, cancellationToken, tea);
                    return tea;
                }
                if (update.CallbackQuery.Data == "TШен пуэр")
                {
                    tea.TeaType = TeaTypes.ShenPuer;
                    await client.SendTextMessageAsync(
                        chatId: chatId,
                        text: "Ты выбрал 'Шен пуэр' ",
                        cancellationToken: cancellationToken);
                    await SetTeaName(update, client, cancellationToken, tea);
                    return tea;
                }
            }
            return new TeaDTO();
        }

        private static async Task<TeaDTO> SetTeaName(Update update, ITelegramBotClient client, CancellationToken cancellationToken, TeaDTO tea)
        {
            var chatId = update.CallbackQuery.Message.Chat.Id;
            await client.SendTextMessageAsync(
                        chatId: chatId,
                        text: "Чтобы установить название чая, отправь сообщение:\n<<Название чая: какое-то название...>>",
                        cancellationToken: cancellationToken);
            return tea;
        }

        private static async Task SetTeaWeight(Update update, ITelegramBotClient client, CancellationToken cancellationToken, TeaDTO tea)
        {
            var chatId = update.CallbackQuery.Message.Chat.Id;

            InlineKeyboardMarkup inlineKeyboardMarkup = new(new[]
            {
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData(text: "50", callbackData: "T50"),
                            InlineKeyboardButton.WithCallbackData(text: "100", callbackData: "T100"),
                            InlineKeyboardButton.WithCallbackData(text: "150", callbackData: "T150"),
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData(text: "200", callbackData: "T200"),
                            InlineKeyboardButton.WithCallbackData(text: "250", callbackData: "T250"),
                            InlineKeyboardButton.WithCallbackData(text: "375", callbackData: "T375"),
                        },
                    });

            await client.SendTextMessageAsync(
                chatId: chatId,
                text: "Выбери вес чая:",
                replyMarkup: inlineKeyboardMarkup,
                cancellationToken: cancellationToken);
        }

        public static async Task<TeaDTO> TeaWeighteCallbackExecute(Update update, ITelegramBotClient client, CancellationToken cancellationToken, TeaDTO tea)
        {
            var chatId = update.CallbackQuery.Message.Chat.Id;

            if (update.CallbackQuery.Data != null)
            {
                if (update.CallbackQuery.Data == "T50")
                {
                    tea.TeaWeight = TeaWeight.Fifty;
                    await client.SendTextMessageAsync(
                        chatId: chatId,
                        text: "Ты выбрал 50 грамм ",
                        cancellationToken: cancellationToken);
                    await SetTeaForm(update, client, cancellationToken, tea);
                    return tea;
                }
                if (update.CallbackQuery.Data == "T100")
                {
                    tea.TeaWeight = TeaWeight.OneHundred;
                    await client.SendTextMessageAsync(
                        chatId: chatId,
                        text: "Ты выбрал 100 грамм ",
                        cancellationToken: cancellationToken);
                    await SetTeaForm(update, client, cancellationToken, tea);
                    return tea;
                }
                if (update.CallbackQuery.Data == "T150")
                {
                    tea.TeaWeight = TeaWeight.OneHundredFifty;
                    await client.SendTextMessageAsync(
                        chatId: chatId,
                        text: "Ты выбрал 150 грамм ",
                        cancellationToken: cancellationToken);
                    await SetTeaForm(update, client, cancellationToken, tea);
                    return tea;
                }
                if (update.CallbackQuery.Data == "T200")
                {
                    tea.TeaWeight = TeaWeight.TwoHundred;
                    await client.SendTextMessageAsync(
                        chatId: chatId,
                        text: "Ты выбрал 200 грамм ",
                        cancellationToken: cancellationToken);
                    await SetTeaForm(update, client, cancellationToken, tea);
                    return tea;
                }
                if (update.CallbackQuery.Data == "T250")
                {
                    tea.TeaWeight = TeaWeight.TwoHundredFifty;
                    await client.SendTextMessageAsync(
                        chatId: chatId,
                        text: "Ты выбрал 250 грамм ",
                        cancellationToken: cancellationToken);
                    await SetTeaForm(update, client, cancellationToken, tea);
                    return tea;
                }
                if (update.CallbackQuery.Data == "T375")
                {
                    tea.TeaWeight = TeaWeight.ThreeHundredSeventyFive;
                    await client.SendTextMessageAsync(
                        chatId: chatId,
                        text: "Ты выбрал 375 грамм ",
                        cancellationToken: cancellationToken);
                    await SetTeaForm(update, client, cancellationToken, tea);
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
                            InlineKeyboardButton.WithCallbackData(text: "Пресованный", callbackData: "TПресованный"),
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData(text: "Рассыпной", callbackData: "TРассыпной"),
                        },
                    });

            await client.SendTextMessageAsync(
                chatId: chatId,
                text: "Выбери форму чая:",
                replyMarkup: inlineKeyboardMarkup,
                cancellationToken: cancellationToken);
        }

        public static async Task<TeaDTO> TeaFormCallbackExecute(Update update, ITelegramBotClient client, CancellationToken cancellationToken, TeaDTO tea)
        {
            var chatId = update.CallbackQuery.Message.Chat.Id;

            if (update.CallbackQuery.Data != null)
            {
                if (update.CallbackQuery.Data == "TПресованный")
                {
                    tea.TeaWeight = TeaWeight.Fifty;
                    await client.SendTextMessageAsync(
                        chatId: chatId,
                        text: "Ты выбрал 'Пресованный' ",
                        cancellationToken: cancellationToken);
                    return tea;
                }
                if (update.CallbackQuery.Data == "TРассыпной")
                {
                    tea.TeaWeight = TeaWeight.OneHundred;
                    await client.SendTextMessageAsync(
                        chatId: chatId,
                        text: "Ты выбрал 'Рассыпной' ",
                        cancellationToken: cancellationToken);
                    return tea;
                }
            }
            return tea;
        }
    }
}
