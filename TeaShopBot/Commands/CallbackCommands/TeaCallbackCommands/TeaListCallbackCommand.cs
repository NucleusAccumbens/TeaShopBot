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

namespace TeaShopBot.Commands.CallbackCommands
{
    public class TeaListCallbackCommand : TelegramCallbackCommand
    {
        public override char CallbackDataCode => 'B';

        public override bool Contains(CallbackQuery message)
        {
            char code = message.Data.ToString().FirstOrDefault();

            if (code != CallbackDataCode)
                return false;

            return message.Data.Contains(CallbackDataCode);
        }

        public override async Task CallbackExecute(Update update, ITelegramBotClient client, CancellationToken cancellationToken)
        {
            if (update.CallbackQuery.Data != null)
            {
                var chatId = update.CallbackQuery.Message.Chat.Id;

                using (ShopContext context = new ShopContext())
                {
                    UnitOfWork _unit = new UnitOfWork(context);
                    var teaService = new TeaService(_unit);

                    if (update.CallbackQuery.Data == "BRed")
                    {
                        try
                        {                         
                            var teaList = await teaService.GetAllRedTeaAsync();
                            await GetSpetialTeaListAsync(chatId, client, cancellationToken, teaList);
                        }
                        catch (Exception)
                        {
                            await ExeptionMassegeAsync(chatId, client, cancellationToken);
                        }
                    }
                    if (update.CallbackQuery.Data == "BGreen")
                    {
                        try
                        {
                            var teaList = await teaService.GetAllGreenTeaAsync();
                            await GetSpetialTeaListAsync(chatId, client, cancellationToken, teaList);
                        }
                        catch (Exception)
                        {
                            await ExeptionMassegeAsync(chatId, client, cancellationToken); ;
                        }
                    }
                    if (update.CallbackQuery.Data == "BWhite")
                    {
                        try
                        {
                            var teaList = await teaService.GetAllWhiteTeaAsync();
                            await GetSpetialTeaListAsync(chatId, client, cancellationToken, teaList);
                        }
                        catch (Exception)
                        {
                            await ExeptionMassegeAsync(chatId, client, cancellationToken); ;
                        }
                    }
                    if (update.CallbackQuery.Data == "BOolong")
                    {
                        try
                        {
                            var teaList = await teaService.GetAllOolongTeaAsync();
                            await GetSpetialTeaListAsync(chatId, client, cancellationToken, teaList);
                        }
                        catch (Exception)
                        {
                            await ExeptionMassegeAsync(chatId, client, cancellationToken);
                        }
                    }
                    if (update.CallbackQuery.Data == "BShuPuer")
                    {
                        try
                        {
                            var teaList = await teaService.GetAllShuPuerAsync();
                            await GetSpetialTeaListAsync(chatId, client, cancellationToken, teaList);
                        }
                        catch (Exception)
                        {
                            await ExeptionMassegeAsync(chatId, client, cancellationToken);
                        }
                    }
                    if (update.CallbackQuery.Data == "BShenPuer")
                    {
                        try
                        {
                            var teaList = await teaService.GetAllShenPuerAsync();
                            await GetSpetialTeaListAsync(chatId, client, cancellationToken, teaList);
                        }
                        catch (Exception)
                        {
                            await ExeptionMassegeAsync(chatId, client, cancellationToken);
                        }
                    }
                }
            }
        }

        private async Task GetSpetialTeaListAsync(long chatId, ITelegramBotClient client, CancellationToken cancellationToken, List<TeaDTO> teaList)
        {
            try
            {
                if (teaList.Count == 0)
                {
                    await client.SendTextMessageAsync(
                            chatId: chatId,
                            text: "🤷🏼 Здесь пока ничего нет...\n" +
                            "Но это временно, не пропусти обновления ✨",
                            cancellationToken: cancellationToken);
                }

                foreach (var tea in teaList)
                {
                    if (tea.ProductPathToImage != null && tea.InStock == true)
                    {
                        InlineKeyboardMarkup inlineKeyboardMarkup = new(new[]
                        {
                            new[]
                            {
                                InlineKeyboardButton.WithCallbackData(text: "🛒 Добавить в корзину 🛒", callbackData: "CTeaAddToCard"),
                            },
                            new[]
                            {
                                InlineKeyboardButton.WithCallbackData(text: "🛒 Корзина 🛒", callbackData: "CCart"),
                                InlineKeyboardButton.WithCallbackData(text: "🍃 Выбрать сорт чая 🍃", callbackData: "ATea"),
                            },
                        });

                        await client.SendPhotoAsync(
                            chatId: chatId,
                            photo: tea.ProductPathToImage,
                            caption: $"<b>Код:</b> {tea.ProductId}\n\n" +
                            $"<b>Название:</b> {tea.ProductName}\n\n" +
                            $"<b>Описание:</b> {tea.ProductDescription}\n\n" +
                            $"<b>Вес:</b> {TeaEnumParser.TeaWeightToString(tea.TeaWeight)}\n\n" +
                            $"<b>Форма хранения:</b> {TeaEnumParser.TeaFormToString(tea.TeaForm)}\n\n" +
                            $"<b>Цена:</b> {tea.ProductPrice}\n\n" +
                            $"<b>В наличии:</b> {tea.ProductCount}",
                            parseMode: ParseMode.Html,
                            replyMarkup: inlineKeyboardMarkup,
                            cancellationToken: cancellationToken);
                    }
                    if (tea.ProductPathToImage == null && tea.InStock == true)
                    {
                        InlineKeyboardMarkup inlineKeyboardMarkup = new(new[]
                       {
                            new[]
                            {
                                InlineKeyboardButton.WithCallbackData(text: "🛒 Добавить в корзину 🛒", callbackData: "CTeaAddToCard"),
                            },
                             new[]
                            {
                                InlineKeyboardButton.WithCallbackData(text: "🛒 Корзина 🛒", callbackData: "CCart"),
                            },
                        });

                        await client.SendTextMessageAsync(
                            chatId: chatId,
                            text: $"<b>Код:</b> {tea.ProductId}\n\n" +
                            $"<b>Название:</b> {tea.ProductName}\n\n" +
                            $"<b>Описание:</b> {tea.ProductDescription}\n\n" +
                            $"<b>Вес:</b> {TeaEnumParser.TeaWeightToString(tea.TeaWeight)} грамм\n\n" +
                            $"<b>Форма хранения:</b> {TeaEnumParser.TeaFormToString(tea.TeaForm)}\n\n" +
                            $"<b>Цена:</b> {tea.ProductPrice}\n\n" +
                            $"<b>В наличии:</b> {tea.ProductCount}",
                            parseMode: ParseMode.Html,
                            replyMarkup: inlineKeyboardMarkup,
                            cancellationToken: cancellationToken);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            
        }

        private async Task ExeptionMassegeAsync(long chatId, ITelegramBotClient client, CancellationToken cancellationToken)
        {
            await client.SendTextMessageAsync(
                chatId: chatId,
                text: "🤦🏿‍♀️ Что-то пошло не так...",
                cancellationToken: cancellationToken);
        }
    }
}
