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
                    InlineKeyboardButton.WithCallbackData(text: "Красный", callbackData: "1"),
                    InlineKeyboardButton.WithCallbackData(text: "Зелёный", callbackData: "2"),
                    InlineKeyboardButton.WithCallbackData(text: "Белый", callbackData: "3"),
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData(text: "Улун", callbackData: "4"),
                    InlineKeyboardButton.WithCallbackData(text: "Шу пуэр", callbackData: "5"),
                    InlineKeyboardButton.WithCallbackData(text: "Шен пуэр", callbackData: "6"),
                },
            });

            await client.SendTextMessageAsync(
                chatId: chatId,
                text: "Выбери сорт чая:",
                replyMarkup: inlineKeyboardMarkup,
                cancellationToken: cancellationToken);
        }


        public static async Task<TeaDTO> TeaTypeCallbackExecute(Update update, ITelegramBotClient client, CancellationToken cancellationToken)
        {

            if (update.CallbackQuery.Data != null)
            {
                var tea = new TeaDTO();
                var chatId = update.CallbackQuery.Message.Chat.Id;

                if (update.CallbackQuery.Data == "1")
                {
                    tea.TeaType = TeaTypes.Red;
                    await client.SendTextMessageAsync(
                        chatId: chatId,
                        text: "Ты выбрал 'Красный' ",
                        cancellationToken: cancellationToken);
                    await SetTeaWeight(update, client, cancellationToken, tea);
                    return tea;
                }
                if (update.CallbackQuery.Data == "2")
                {
                     tea.TeaType = TeaTypes.Green;
                    await client.SendTextMessageAsync(
                        chatId: chatId,
                        text: "Ты выбрал 'Зелёный' ",
                        cancellationToken: cancellationToken);
                    await SetTeaWeight(update, client, cancellationToken, tea);
                    return tea;
                }
                if (update.CallbackQuery.Data == "3")
                {
                    tea.TeaType = TeaTypes.White;
                    await client.SendTextMessageAsync(
                        chatId: chatId,
                        text: "Ты выбрал 'Белый' ",
                        cancellationToken: cancellationToken);
                    await SetTeaWeight(update, client, cancellationToken, tea);
                    return tea;
                }
                if (update.CallbackQuery.Data == "4")
                {
                    tea.TeaType = TeaTypes.Oolong;
                    await client.SendTextMessageAsync(
                        chatId: chatId,
                        text: "Ты выбрал 'Улун' ",
                        cancellationToken: cancellationToken);
                    await SetTeaWeight(update, client, cancellationToken, tea);
                    return tea;
                }
                if (update.CallbackQuery.Data == "5")
                {
                    tea.TeaType = TeaTypes.ShuPuer;
                    await client.SendTextMessageAsync(
                        chatId: chatId,
                        text: "Ты выбрал 'Шу пуэр' ",
                        cancellationToken: cancellationToken);
                    await SetTeaWeight(update, client, cancellationToken, tea);
                    return tea;
                }
                if (update.CallbackQuery.Data == "6")
                {
                    tea.TeaType = TeaTypes.ShenPuer;
                    await client.SendTextMessageAsync(
                        chatId: chatId,
                        text: "Ты выбрал 'Шен пуэр' ",
                        cancellationToken: cancellationToken);
                    await SetTeaWeight(update, client, cancellationToken, tea);
                    return tea;
                }
            }
            return new TeaDTO();
        }

        private static async Task SetTeaWeight(Update update, ITelegramBotClient client, CancellationToken cancellationToken, TeaDTO tea)
        {
            var chatId = update.CallbackQuery.Message.Chat.Id;

            InlineKeyboardMarkup inlineKeyboardMarkup = new(new[]
            {
                new[]
                {
                    InlineKeyboardButton.WithCallbackData(text: "50", callbackData: "50"),
                    InlineKeyboardButton.WithCallbackData(text: "100", callbackData: "100"),
                    InlineKeyboardButton.WithCallbackData(text: "150", callbackData: "150"),
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData(text: "200", callbackData: "200"),
                    InlineKeyboardButton.WithCallbackData(text: "250", callbackData: "250"),
                    InlineKeyboardButton.WithCallbackData(text: "375", callbackData: "375"),
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
                if (update.CallbackQuery.Data == "50")
                {
                    tea.TeaWeight = TeaWeight.Fifty;
                    await client.SendTextMessageAsync(
                        chatId: chatId,
                        text: "Ты выбрал 50 грамм ",
                        cancellationToken: cancellationToken);
                    await SetTeaForm(update, client, cancellationToken, tea);
                    return tea;
                }
                if (update.CallbackQuery.Data == "100")
                {
                    tea.TeaWeight = TeaWeight.OneHundred;
                    await client.SendTextMessageAsync(
                        chatId: chatId,
                        text: "Ты выбрал 100 грамм ",
                        cancellationToken: cancellationToken);
                    await SetTeaForm(update, client, cancellationToken, tea);
                    return tea;
                }
                if (update.CallbackQuery.Data == "150")
                {
                    tea.TeaWeight = TeaWeight.OneHundredFifty;
                    await client.SendTextMessageAsync(
                        chatId: chatId,
                        text: "Ты выбрал 150 грамм ",
                        cancellationToken: cancellationToken);
                    await SetTeaForm(update, client, cancellationToken, tea);
                    return tea;
                }
                if (update.CallbackQuery.Data == "200")
                {
                    tea.TeaWeight = TeaWeight.TwoHundred;
                    await client.SendTextMessageAsync(
                        chatId: chatId,
                        text: "Ты выбрал 200 грамм ",
                        cancellationToken: cancellationToken);
                    await SetTeaForm(update, client, cancellationToken, tea);
                    return tea;
                }
                if (update.CallbackQuery.Data == "250")
                {
                    tea.TeaWeight = TeaWeight.TwoHundredFifty;
                    await client.SendTextMessageAsync(
                        chatId: chatId,
                        text: "Ты выбрал 250 грамм ",
                        cancellationToken: cancellationToken);
                    await SetTeaForm(update, client, cancellationToken, tea);
                    return tea;
                }
                if (update.CallbackQuery.Data == "375")
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
                    InlineKeyboardButton.WithCallbackData(text: "Пресованный", callbackData: "Пресованный"),
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData(text: "Рассыпной", callbackData: "Рассыпной"),
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
                if (update.CallbackQuery.Data == "Пресованный")
                {
                    tea.TeaWeight = TeaWeight.Fifty;
                    await client.SendTextMessageAsync(
                        chatId: chatId,
                        text: "Ты выбрал 'Пресованный' ",
                        cancellationToken: cancellationToken);
                    return tea;
                }
                if (update.CallbackQuery.Data == "Рассыпной")
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
