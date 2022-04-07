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
using Telegram.Bot.Types.InputFiles;
using Telegram.Bot.Types.ReplyMarkups;

namespace TeaShopBot.Commands.FileCommands
{
    public class HoneyImageCommand : TelegramFileCommand
    {
        public override string Name => "Фото меда: ";

        public override bool Contains(Message message)
        {
            if (message.Type != MessageType.Photo)
                return false;
            string mes = message.Caption;

            return mes.Contains(Name);
        }

        public override async Task FileExecute(Update update, ITelegramBotClient client, CancellationToken cancellationToken, TeaDTO tea, HerbDTO herb, HoneyDTO honey)
        {
            var chatId = update.Message.Chat.Id;
            var fileId = update.Message.Photo[2].FileId;
            InputOnlineFile file = new InputOnlineFile(fileId);
            long honeyId;

            try
            {
                if (long.TryParse(update.Message.Caption.Substring(9), out honeyId))
                {
                    honeyId = Convert.ToInt64(update.Message.Caption.Substring(9));
                    honey = await GetHoney(honeyId);

                    if (honey == null)
                    {
                        await client.SendTextMessageAsync(
                            chatId: chatId,
                            text: $"🤷🏻‍♂️ Мёда с кодом {honeyId} нет в базе данных...",
                            cancellationToken: cancellationToken);

                        return;
                    }

                    honey.ProductPathToImage = fileId;
                    await UpdateHoney(honey);

                    await client.SendPhotoAsync(
                        chatId: chatId,
                        photo: file,
                        caption: $"💪🏾 Фото меда изменено!\n\n" +
                        $"Название мёда: {honey.ProductName}\n" +
                        $"Описание мёда: {honey.ProductDescription}\n" +
                        $"Вес мёда: {HoneyEnumParser.HoneyWeightToString(honey.HoneyWeight)}\n" +
                        $"Цена мёда: {honey.ProductPrice}\n" +
                        $"Количество мёда: {honey.ProductCount}\n",
                        cancellationToken: cancellationToken);

                    return;
                }
            }
            catch (Exception)
            {
                await client.SendTextMessageAsync(
                chatId: chatId,
                text: $"🤦🏿‍♀️ Что-то пошло не так...",
                cancellationToken: cancellationToken);
            }

            honey.ProductPathToImage = fileId;

            InlineKeyboardMarkup inlineKeyboardMarkup = new(new[]
            {
                new[]
                {
                    InlineKeyboardButton.WithCallbackData(text: "Сохранить", callbackData: "M"),
                },
            });

            await client.SendPhotoAsync(
                        chatId: chatId,
                        photo: file,
                        caption: $"Название мёда: {honey.ProductName}\n" +
                        $"Описание мёда: {honey.ProductDescription}\n" +
                        $"Вес мёда: {HoneyEnumParser.HoneyWeightToString(honey.HoneyWeight)}\n" +
                        $"Цена мёда: {honey.ProductPrice}\n" +
                        $"Количество мёда: {honey.ProductCount}\n" +
                        $"Мёд появится в наличии после нажатия на кнопку ⬇️",
                        replyMarkup: inlineKeyboardMarkup,
                        cancellationToken: cancellationToken);
        }

        private async Task<HoneyDTO> GetHoney(long honeyId)
        {
            try
            {
                using (ShopContext context = new ShopContext())
                {
                    UnitOfWork repo = new UnitOfWork(context);
                    var honeyService = new HoneyService(repo);
                    var honey = await honeyService.GetAsync(honeyId);
                    return honey;
                }
            }
            catch(Exception)
            {
                return null;
            }
        }

        private async Task UpdateHoney(HoneyDTO honey)
        {
            try
            {
                using (ShopContext context = new ShopContext())
                {
                    UnitOfWork repo = new UnitOfWork(context);
                    var honeyService = new HoneyService(repo);
                    await honeyService.UpdateAsync(honey);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

    }   
}
