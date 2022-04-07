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

namespace TeaShopBot.Commands.AdminCommands.AdminCallbackCommands
{
    public class HerbListForEditCallbackCommand : TelegramCallbackCommand
    {
        public override char CallbackDataCode => 'r';

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

                if (update.CallbackQuery.Data == "rAltai")
                {
                    try
                    {
                        using (ShopContext context = new ShopContext())
                        {
                            UnitOfWork repo = new UnitOfWork(context);
                            var herbService = new HerbService(repo);
                            var herbs = await herbService.GetAllAltaiHerbsAsync();

                            await GetSpetialHerbListWithMessage(chatId, client, cancellationToken, herbs);
                        }
                    }
                    catch (Exception)
                    {
                        await GetExeptionMessage(chatId, client, cancellationToken);
                    }
                }
                if (update.CallbackQuery.Data == "rCaucasus")
                {
                    try
                    {
                        using (ShopContext context = new ShopContext())
                        {
                            UnitOfWork repo = new UnitOfWork(context);
                            var herbService = new HerbService(repo);
                            var herbs = await herbService.GetAllCaucasusHerbsAsync();

                            await GetSpetialHerbListWithMessage(chatId, client, cancellationToken, herbs);
                        }
                    }
                    catch (Exception)
                    {
                        await GetExeptionMessage(chatId, client, cancellationToken);
                    }
                }
                if (update.CallbackQuery.Data == "rKarelia")
                {
                    try
                    {
                        using (ShopContext context = new ShopContext())
                        {
                            UnitOfWork repo = new UnitOfWork(context);
                            var herbService = new HerbService(repo);
                            var herbs = await herbService.GetAllKareliaHerbsAsync();

                            await GetSpetialHerbListWithMessage(chatId, client, cancellationToken, herbs);
                        }
                    }
                    catch (Exception)
                    {
                        await GetExeptionMessage(chatId, client, cancellationToken);
                    }
                }
                if (update.CallbackQuery.Data == "rSiberia")
                {
                    try
                    {
                        using (ShopContext context = new ShopContext())
                        {
                            UnitOfWork repo = new UnitOfWork(context);
                            var herbService = new HerbService(repo);
                            var herbs = await herbService.GetAllSiberiaaHerbsAsync();

                            await GetSpetialHerbListWithMessage(chatId, client, cancellationToken, herbs);
                        }
                    }
                    catch (Exception)
                    {
                        await GetExeptionMessage(chatId, client, cancellationToken);
                    }
                }
            }
        }

        private async Task GetExeptionMessage(long chatId, ITelegramBotClient client, CancellationToken cancellationToken)
        {
            await client.SendTextMessageAsync(
                            chatId: chatId,
                            text: $"🤦🏿‍♀️ Что-то пошло не так...",
                            cancellationToken: cancellationToken);
        }

        private async Task GetSpetialHerbListWithMessage(long chatId, ITelegramBotClient client, CancellationToken cancellationToken, List<HerbDTO> herbs)
        {

            if (herbs.Count == 0)
            {
                await client.SendTextMessageAsync(
                    chatId: chatId,
                    text: $"🌱 В этой категории пока нет товаров!",
                    cancellationToken: cancellationToken);
            }
            if (herbs != null && herbs.Count != 0)
            {
                foreach (var herb in herbs)
                {
                    InlineKeyboardMarkup inlineKeyboardMarkup = new(new[]
                    {
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData(text: "🖌 Редактировать описание", callbackData: $"UEditProductDescriptions{herb.ProductId}"),
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData(text: "🖌 Редактировать количество", callbackData: $"UEditProductCount{herb.ProductId}"),
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData(text: "🖌 Редактировать цену", callbackData: $"UEditProductPrice{herb.ProductId}"),
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData(text: "🖌 Редактировать фото", callbackData: $"UEditProductPhoto{herb.ProductId}"),
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData(text: "❌ Удалить", callbackData: $"URemoveProduct{herb.ProductId}"),
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
            }
        }
    }
}
