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

namespace TeaShopBot.Commands.AdminCommands
{
    public class ProductEditDescriptionCommand : TelegramCommand
    {
        public override string Name => "Описание";

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
            long productId;
            var text = update.Message.Text;
            var index = text.IndexOf(':');
            var probablyProductId = text.Substring(8, index - 8);

            try
            {
                if (long.TryParse(probablyProductId, out productId))
                {
                    productId = Convert.ToInt64(probablyProductId);
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

                    if (product != null)
                    {
                        product.ProductDescription = text.Substring(index + 1);
                        await UpdateProduct(product);

                        await client.SendTextMessageAsync(
                            chatId: chatId,
                            text: $"💪🏾 Описание товара изменено!\n\n" +
                            $"Название: {product.ProductName}\n" +
                            $"Описание: {product.ProductDescription}\n" +
                            $"Вес: {strinfWeight}\n" +
                            $"Цена: {product.ProductPrice}\n" +
                            $"Количество: {product.ProductCount}\n",
                            cancellationToken: cancellationToken);

                        return;
                    }

                    await client.SendTextMessageAsync(
                            chatId: chatId,
                            text: $"🤷🏻‍♂️ Товара с кодом {productId} нет в базе данных...",
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
