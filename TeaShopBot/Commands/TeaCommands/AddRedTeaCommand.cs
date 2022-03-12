using DATABASE.DataContext;
using DATABASE.Enums;
using DATABASE.Repositories;
using Microsoft.EntityFrameworkCore;
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

namespace TeaShopBot.Commands
{
    public class AddRedTeaCommand : TelegramCommand
    {
        public override string Name => @"Красный";

        public override bool Contains(Message message)
        {
            if (message.Type != MessageType.Text)
                return false;
            return message.Text.Contains(Name);
        }

        public override async Task Execute(Update update, ITelegramBotClient client, CancellationToken cancellationToken)
        {
            var chatId = update.Message.Chat.Id;
            try
            {
                using (ShopContext context = new ShopContext())
                {
                    var unitOfWork = new UnitOfWork(context);
                    var teaService = new TeaService(unitOfWork);

                    var tea = new TeaDTO()
                    {
                        TeaType = TeaTypes.Red
                    };

                    //int teaWeight = SetTeaWeight(update, client, cancellationToken).Result;

                    //if (teaWeight == 50)
                    //{
                    //    tea.TeaWeight = TeaWeight.Fifty;
                    //}
                    //if (teaWeight == 100)
                    //{
                    //    tea.TeaWeight = TeaWeight.OneHundred;
                    //}
                    //if (teaWeight == 150)
                    //{
                    //    tea.TeaWeight = TeaWeight.OneHundredFifty;
                    //}
                    //if (teaWeight == 200)
                    //{
                    //    tea.TeaWeight = TeaWeight.TwoHundred;
                    //}
                    //if (teaWeight == 250)
                    //{
                    //    tea.TeaWeight = TeaWeight.TwoHundredFifty;
                    //}
                    //if (teaWeight == 375)
                    //{
                    //    tea.TeaWeight = TeaWeight.ThreeHundredSeventyFive;
                    //}

                    //string teaForm = SetTeaForm(update, client, cancellationToken).Result;

                    //if (teaForm == "Рассыпной")
                    //{
                    //    tea.TeaForm = TeaForms.Loose;
                    //}
                    //if (teaForm == "Пресованный")
                    //{
                    //    tea.TeaForm = TeaForms.Pressed;
                    //}

                    await teaService.CreateAsync(tea);
                }
            }
            catch (DbUpdateException)
            {
                Message sentMessage = await client.SendTextMessageAsync(
                                chatId: chatId,
                                text: "Чай с таким названием уже существует. Придумай другое название: ",
                                cancellationToken: cancellationToken);
            }
            catch (AggregateException)
            {
                await client.SendTextMessageAsync(
                    chatId: chatId,
                    text: "Введи число, вместо строки: ",
                    cancellationToken: cancellationToken);
            }

        }

        private async Task<int> SetTeaWeight(Update update, ITelegramBotClient client, CancellationToken cancellationToken)
        {
            var chatId = update.Message.Chat.Id;

            ReplyKeyboardMarkup replyKeyboardMarkup = new(new[]
            {
                new KeyboardButton[] {"50",  "100", "150"},
                new KeyboardButton[] {"200", "250", "375"},
            })
            {
                ResizeKeyboard = true
            };

            await client.SendTextMessageAsync(
                chatId: chatId,
                text: "Выбери вес чая:",
                replyMarkup: replyKeyboardMarkup,
                cancellationToken: cancellationToken);

            return Convert.ToInt32(update.Message.Text);
        }

        private async Task<string> SetTeaForm(Update update, ITelegramBotClient client, CancellationToken cancellationToken)
        {
            var chatId = update.Message.Chat.Id;

            ReplyKeyboardMarkup replyKeyboardMarkup = new(new[]
            {
                new KeyboardButton[] {"Рассыпной"},
                new KeyboardButton[] {"Пресованный"},
            })
            {
                ResizeKeyboard = true
            };

            await client.SendTextMessageAsync(
                chatId: chatId,
                text: "Выбери вес чая:",
                replyMarkup: replyKeyboardMarkup,
                cancellationToken: cancellationToken);

            return update.Message.Text;
        }

        private async Task<string> SetName(Update update, ITelegramBotClient client, CancellationToken cancellationToken)
        {
            var chatId = update.Message.Chat.Id;

            await client.SendTextMessageAsync(
                chatId: chatId,
                text: "Введи название чая: ",
                cancellationToken: cancellationToken);

            return update.Message.Text;
        }

    }
}
