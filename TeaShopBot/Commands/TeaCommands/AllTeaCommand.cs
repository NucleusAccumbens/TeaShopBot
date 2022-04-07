using DATABASE.DataContext;
using DATABASE.Enums;
using DATABASE.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TeaShopBLL.Services;
using TeaShopBot.Abstractions;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InputFiles;

namespace TeaShopBot.Commands.TeaCommands
{
    public class AllTeaCommand : TelegramCommand
    {
        public override string Name => @"Весь чай";

        public override bool Contains(Message message)
        {
            if (message.Type != MessageType.Text)
                return false;
            string mes = message.Text;

            return mes.Contains(Name);
        }

        public override async Task Execute(Update update, ITelegramBotClient client, CancellationToken cancellationToken)
        {
            var chatId = update.Message.Chat.Id;
            using (ShopContext context = new ShopContext())
            {
                try
                {
                    UnitOfWork _unit = new UnitOfWork(context);
                    var teaService = new TeaService(_unit);

                    var teaList = await teaService.GetAllAsync();
                    foreach (var tea in teaList)
                    {
                        if (tea.ProductPathToImage != null && tea.InStock == true)
                        {
                            await client.SendPhotoAsync(
                                chatId: chatId,
                                photo: tea.ProductPathToImage,
                                caption: $"<b>Название:</b> {tea.ProductName}\n\n" +
                                $"<b>Описание:</b> {tea.ProductDescription}\n\n" +
                                $"<b>Вес:</b> {TeaEnumParser.TeaWeightToString(tea.TeaWeight)}\n\n" +
                                $"<b>Форма хранения:</b> {TeaEnumParser.TeaFormToString(tea.TeaForm)}\n\n" +
                                $"<b>Цена:</b> {tea.ProductPrice}\n\n" +
                                $"<b>В наличии:</b> {tea.ProductCount}",
                                parseMode: ParseMode.Html,
                                cancellationToken: cancellationToken);
                        }
                        if (tea.ProductPathToImage == null && tea.InStock == true)
                        {
                            await client.SendTextMessageAsync(
                                chatId: chatId,
                                text: $"<b>Название:</b> {tea.ProductName}\n\n" +
                                $"<b>Описание:</b> {tea.ProductDescription}\n\n" +
                                $"<b>Вес:</b> {TeaEnumParser.TeaWeightToString(tea.TeaWeight)} грамм\n\n" +
                                $"<b>Форма хранения:</b> {TeaEnumParser.TeaFormToString(tea.TeaForm)}\n\n" +
                                $"<b>Цена:</b> {tea.ProductPrice}\n\n" +
                                $"<b>В наличии:</b> {tea.ProductCount}",
                                parseMode: ParseMode.Html,
                                cancellationToken: cancellationToken);
                        }
                    }
                }
                catch (Exception ex)
                {
                    await client.SendTextMessageAsync(
                        chatId: chatId,
                        text: ex.Message,
                        cancellationToken: cancellationToken);
                }
            }
        }
    }
}
