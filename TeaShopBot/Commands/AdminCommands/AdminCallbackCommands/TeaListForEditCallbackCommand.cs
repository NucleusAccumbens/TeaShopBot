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
    public class TeaListForEditCallbackCommand : TelegramCallbackCommand
    {
        public override char CallbackDataCode => 'R';

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

                if (update.CallbackQuery.Data == "RRed")
                {
                    try
                    {
                        using (ShopContext context = new ShopContext())
                        {
                            UnitOfWork repo = new UnitOfWork(context);
                            var teaService = new TeaService(repo);
                            var teas = await teaService.GetAllRedTeaAsync();

                            await GetSpetialTeaListWithMessage(chatId, client, cancellationToken, teas);
                        }
                    }
                    catch (Exception)
                    {
                        await GetExeptionMessage(chatId, client, cancellationToken);
                    }
                }
                if (update.CallbackQuery.Data == "RGreen")
                {
                    try
                    {
                        using (ShopContext context = new ShopContext())
                        {
                            UnitOfWork repo = new UnitOfWork(context);
                            var teaService = new TeaService(repo);
                            var teas = await teaService.GetAllGreenTeaAsync();

                            await GetSpetialTeaListWithMessage(chatId, client, cancellationToken, teas);
                        }
                    }
                    catch (Exception)
                    {
                        await GetExeptionMessage(chatId, client, cancellationToken);
                    }
                }
                if (update.CallbackQuery.Data == "RWhite")
                {
                    try
                    {
                        using (ShopContext context = new ShopContext())
                        {
                            UnitOfWork repo = new UnitOfWork(context);
                            var teaService = new TeaService(repo);
                            var teas = await teaService.GetAllWhiteTeaAsync();

                            await GetSpetialTeaListWithMessage(chatId, client, cancellationToken, teas);
                        }
                    }
                    catch (Exception)
                    {
                        await GetExeptionMessage(chatId, client, cancellationToken);
                    }
                }
                if (update.CallbackQuery.Data == "ROolong")
                {
                    try
                    {
                        using (ShopContext context = new ShopContext())
                        {
                            UnitOfWork repo = new UnitOfWork(context);
                            var teaService = new TeaService(repo);
                            var teas = await teaService.GetAllOolongTeaAsync();

                            await GetSpetialTeaListWithMessage(chatId, client, cancellationToken, teas);
                        }
                    }
                    catch (Exception)
                    {
                        await GetExeptionMessage(chatId, client, cancellationToken);
                    }
                }
                if (update.CallbackQuery.Data == "RShu puer")
                {
                    try
                    {
                        using (ShopContext context = new ShopContext())
                        {
                            UnitOfWork repo = new UnitOfWork(context);
                            var teaService = new TeaService(repo);
                            var teas = await teaService.GetAllShuPuerAsync();

                            await GetSpetialTeaListWithMessage(chatId, client, cancellationToken, teas);
                        }
                    }
                    catch (Exception)
                    {
                        await GetExeptionMessage(chatId, client, cancellationToken);
                    }
                }
                if (update.CallbackQuery.Data == "RShen puer")
                {
                    try
                    {
                        using (ShopContext context = new ShopContext())
                        {
                            UnitOfWork repo = new UnitOfWork(context);
                            var teaService = new TeaService(repo);
                            var teas = await teaService.GetAllShenPuerAsync();

                            await GetSpetialTeaListWithMessage(chatId, client, cancellationToken, teas);
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

        private async Task GetSpetialTeaListWithMessage(long chatId, ITelegramBotClient client, CancellationToken cancellationToken, List<TeaDTO> teas)
        {

            if (teas.Count == 0)
            {
                await client.SendTextMessageAsync(
                    chatId: chatId,
                    text: $"🍃 В этой категории пока нет товаров!",
                    cancellationToken: cancellationToken);
            }
            if (teas != null && teas.Count != 0)
            {
                foreach (var t in teas)
                {
                    InlineKeyboardMarkup inlineKeyboardMarkup = new(new[]
                    {
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData(text: "🖌 Редактировать описание", callbackData: $"UEditProductDescriptions{t.ProductId}"),
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData(text: "🖌 Редактировать количество", callbackData: $"UEditProductCount{t.ProductId}"),
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData(text: "🖌 Редактировать цену", callbackData: $"UEditProductPrice{t.ProductId}"),
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData(text: "🖌 Редактировать фото", callbackData: $"UEditProductPhoto{t.ProductId}"),
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData(text: "❌ Удалить", callbackData: $"URemoveProduct{t.ProductId}"),
                        },
                        });

                    await client.SendPhotoAsync(
                        chatId: chatId,
                        photo: t.ProductPathToImage,
                        caption: $"<b>Код:</b> {t.ProductId}\n\n" +
                        $"<b>Название:</b> {t.ProductName}\n\n" +
                        $"<b>Описание:</b> {t.ProductDescription}\n\n" +
                        $"<b>Вес:</b> {TeaEnumParser.TeaWeightToString(t.TeaWeight)}\n\n" +
                        $"<b>Цена:</b> {t.ProductPrice}\n\n" +
                        $"<b>В наличии:</b> {t.ProductCount}",
                        parseMode: ParseMode.Html,
                        replyMarkup: inlineKeyboardMarkup,
                        cancellationToken: cancellationToken);
                }
            }
        }

    }
}
