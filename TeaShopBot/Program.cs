using DATABASE.DataContext;
using DATABASE.Repositories;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace TeaShopBot
{
    public class Program
    {
        private static readonly TelegramBot _bot = new TelegramBot();

        static void Main(string[] args)
        {


            var botClient = _bot.BotClient;
            using var cts = new CancellationTokenSource();

            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = { } 
            };

            botClient.StartReceiving(
                HandleUpdateAsync,
                HandleErrorAsync,
                receiverOptions,
                cancellationToken: cts.Token);

            var me = botClient.GetMeAsync().Result;

            Console.WriteLine($"Start listening for @{me.Username}");
            Console.ReadLine();
            cts.Cancel();

            async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
            {
                if (update.Type != UpdateType.Message)
                    return;
                if (update.Message!.Type != MessageType.Text)
                    return;

                var chatId = update.Message.Chat.Id;
                var messageText = update.Message.Text;

                Console.WriteLine($"Получено сообщение '{messageText}' от пользователя номер {chatId}.");

                Message sentMessage = await botClient.SendTextMessageAsync(
                    chatId: chatId,
                    text: "You said:\n" + messageText,
                    cancellationToken: cancellationToken);

                try
                {                   
                    var res = _bot.SaveUserInDb(update).Result;

                    if (res == true)
                    {
                        Console.WriteLine($"Пользователь с chatId {update.Message.Chat.Id} сохранён в базу данных. " +
                                          $"Имя пользователя: { update.Message.Chat.Username}.");
                    }
                    else
                    {
                        Console.WriteLine($"Пользователь с chatId {update.Message.Chat.Id} уже есть в базе данных. " +
                                          $"Имя пользователя: {update.Message.Chat.Username}.");
                    }
                        
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
                
            }

            Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
            {
                var ErrorMessage = exception switch
                {
                    ApiRequestException apiRequestException
                        => $"Ошибка Telegram API:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                    _ => exception.ToString()
                };

                Console.WriteLine(ErrorMessage);
                return Task.CompletedTask;
            }

        }
    }
}
