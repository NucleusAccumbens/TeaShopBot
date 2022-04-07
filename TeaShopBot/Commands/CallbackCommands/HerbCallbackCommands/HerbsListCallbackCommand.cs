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

namespace TeaShopBot.Commands.CallbackCommands.OrderCallbackCommands
{
    public class HerbsListCallbackCommand : TelegramCallbackCommand
    {
        public override char CallbackDataCode => 'L';

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
                    var herbService = new HerbService(_unit);

                    if (update.CallbackQuery.Data == "LAltai")
                    {
                        try
                        {
                            var herbList = await herbService.GetAllAltaiHerbsAsync();
                            await GetSpetialHerbList(chatId, client, cancellationToken, herbList);
                        }
                        catch (Exception)
                        {
                            await ExeptionMassegeAsync(chatId, client, cancellationToken);
                        }
                    }
                    if (update.CallbackQuery.Data == "LKarelia")
                    {
                        try
                        {
                            var herbList = await herbService.GetAllKareliaHerbsAsync();
                            await GetSpetialHerbList(chatId, client, cancellationToken, herbList);
                        }
                        catch (Exception)
                        {
                            await ExeptionMassegeAsync(chatId, client, cancellationToken); ;
                        }
                    }
                    if (update.CallbackQuery.Data == "LCaucasus")
                    {
                        try
                        {
                            var herbList = await herbService.GetAllCaucasusHerbsAsync();
                            await GetSpetialHerbList(chatId, client, cancellationToken, herbList);
                        }
                        catch (Exception)
                        {
                            await ExeptionMassegeAsync(chatId, client, cancellationToken); ;
                        }
                    }
                    if (update.CallbackQuery.Data == "LSiberia")
                    {
                        try
                        {
                            var herbList = await herbService.GetAllSiberiaaHerbsAsync();
                            await GetSpetialHerbList(chatId, client, cancellationToken, herbList);
                        }
                        catch (Exception)
                        {
                            await ExeptionMassegeAsync(chatId, client, cancellationToken);
                        }
                    }
                }
            }
        }

        private async Task GetSpetialHerbList(long chatId, ITelegramBotClient client, CancellationToken cancellationToken, List<HerbDTO> herbList)
        {
            if (herbList.Count == 0)
            {
                await client.SendTextMessageAsync(
                        chatId: chatId,
                        text: "🤷🏼 Здесь пока ничего нет...\n" +
                        "Но это временно, не пропусти обновления ✨",
                        cancellationToken: cancellationToken);
            }

            foreach (var herb in herbList)
            {
                if (herb.ProductPathToImage != null && herb.InStock == true)
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
                                InlineKeyboardButton.WithCallbackData(text: "🌱 Выбрать регион 🌱", callbackData: "AHerb"),
                            },
                        });

                    await client.SendPhotoAsync(
                        chatId: chatId,
                        photo: herb.ProductPathToImage,
                        caption: $"<b>Код:</b> {herb.ProductId}\n\n" +
                        $"<b>Название:</b> {herb.ProductName}\n\n" +
                        $"<b>Описание:</b> {herb.ProductDescription}\n\n" +
                        $"<b>Вес:</b> {HerbsEnumParser.HerbsWeightToString(herb.Weight)}\n\n" +
                        $"<b>Цена:</b> {herb.ProductPrice}\n\n" +
                        $"<b>В наличии:</b> {herb.ProductCount}",
                        parseMode: ParseMode.Html,
                        replyMarkup: inlineKeyboardMarkup,
                        cancellationToken: cancellationToken);
                }
                if (herb.ProductPathToImage == null && herb.InStock == true)
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
                                InlineKeyboardButton.WithCallbackData(text: "✨ Меню ✨", callbackData: "CMenu"),
                            },
                        });

                    await client.SendTextMessageAsync(
                        chatId: chatId,
                        text: $"<b>Код:</b> {herb.ProductId}\n\n" +
                        $"<b>Название:</b> {herb.ProductName}\n\n" +
                        $"<b>Описание:</b> {herb.ProductDescription}\n\n" +
                        $"<b>Вес:</b> {HerbsEnumParser.HerbsWeightToString(herb.Weight)}\n\n" +
                        $"<b>Цена:</b> {herb.ProductPrice}\n\n" +
                        $"<b>В наличии:</b> {herb.ProductCount}",
                        parseMode: ParseMode.Html,
                        replyMarkup: inlineKeyboardMarkup,
                        cancellationToken: cancellationToken);
                }
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
