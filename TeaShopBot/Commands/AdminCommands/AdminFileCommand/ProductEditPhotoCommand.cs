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

namespace TeaShopBot.Commands.AdminCommands.AdminFileCommand
{
    public class ProductEditPhotoCommand : TelegramFileCommand
    {
        public override string Name => "Фото";

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
            long productId;

            try
            {
                if (long.TryParse(update.Message.Caption.Substring(4), out productId))
                {
                    productId = Convert.ToInt64(update.Message.Caption.Substring(4));
                    var product = await GetProduct(productId);
                    var strinfWeight = "";

                    if (product != null && product is TeaDTO)
                    {
                        strinfWeight = TeaEnumParser.TeaWeightToString((product as TeaDTO).TeaWeight);
                    }
                    if (product != null && product is HerbDTO)
                    {
                        strinfWeight = HerbsEnumParser.HerbsWeightToString((product as HerbDTO).Weight);
                    }
                    if (product != null && product is HoneyDTO)
                    {
                        strinfWeight = HoneyEnumParser.HoneyWeightToString((product as HoneyDTO).HoneyWeight);
                    }

                    if (product == null)
                    {
                        await client.SendTextMessageAsync(
                            chatId: chatId,
                            text: $"🤷🏻‍♂️ Продукта с кодом {productId} нет в базе данных...",
                            cancellationToken: cancellationToken);

                        return;
                    }

                    honey.ProductPathToImage = fileId;
                    await UpdateProduct(product);

                    await client.SendPhotoAsync(
                        chatId: chatId,
                        photo: file,
                        caption: $"💪🏾 Фото товара изменено!\n\n" +
                        $"Название: {honey.ProductName}\n" +
                        $"Описание: {honey.ProductDescription}\n" +
                        $"Вес: {strinfWeight}\n" +
                        $"Цена: {honey.ProductPrice}\n" +
                        $"Количество: {honey.ProductCount}\n",
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
        }

        private async Task<ProductDTO> GetProduct(long productId)
        {
            try
            {
                using (ShopContext context = new ShopContext())
                {
                    UnitOfWork repo = new UnitOfWork(context);
                    var productService = new ProductService(repo);
                    var product = await productService.GetAsync(productId);
                    return product;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        private async Task UpdateProduct(ProductDTO product)
        {
            try
            {
                using (ShopContext context = new ShopContext())
                {
                    UnitOfWork repo = new UnitOfWork(context);
                    var productService = new ProductService(repo);
                    await productService.UpdateAsync(product);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
