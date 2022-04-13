using System;
using System.Threading;
using System.Threading.Tasks;
using TeaShopBLL.DTO;
using TeaShopBot.Commands;
using TeaShopBot.Commands.TeaCommands;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InputFiles;

namespace TeaShopBot
{
    public class Program
    {
        private static readonly TelegramBot _bot = new TelegramBot();
        private static TeaDTO _tea = new TeaDTO();
        private static HerbDTO _herb = new HerbDTO();
        private static HoneyDTO _honey = new HoneyDTO();


        static void Main(string[] args)
        {
            var botClient = _bot.BotClient;
            using var cts = new CancellationTokenSource();

            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = { } 
            };

            var me = botClient.GetMeAsync().Result;

            Console.WriteLine($"Start listening for @{me.Username}");
            Console.ReadLine();
            cts.Cancel();

            botClient.StartReceiving(
                HandleUpdateAsync,
                HandleErrorAsync,
                receiverOptions,
                cancellationToken: cts.Token);


            async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
            {

                if (update.Message != null && update.Message.Type == MessageType.Photo)
                {
                    var fileCommands = _bot.GetTelegramFileCommands();
                    foreach (var fileCommand in fileCommands)
                    {
                        if (fileCommand.Contains(update.Message))
                        {
                            await fileCommand.FileExecute(update, botClient, cancellationToken, _tea, _herb, _honey);
                            break;
                        }
                        
                    }
                }

                if (update.Type == UpdateType.CallbackQuery)
                {
                    var callbackQuery = update.CallbackQuery;

                    var addProductCallbackCommands = _bot.GetAddProductCallbackCommands();
                    foreach (var callbackCommand in addProductCallbackCommands)
                    {
                        if (callbackCommand.Contains(callbackQuery))
                        {
                            await callbackCommand.CallbackExecute(update, botClient, cancellationToken, _tea, _herb, _honey);
                            break;
                        }
                    }

                    var callbackCommands = _bot.GetCallbackCommands();
                    foreach (var callbackCommand in callbackCommands)
                    {
                        if (callbackCommand.Contains(callbackQuery))
                        {
                            await callbackCommand.CallbackExecute(update, botClient, cancellationToken);
                            break;
                        }
                    }
                }

                if (update.Message != null && update.Message.Type == MessageType.Text)
                {
                    var chatId = update.Message.Chat.Id;
                    var message = update.Message;

                    Console.WriteLine($"Получено сообщение '{message.Text}' от пользователя номер {chatId}. ");

                    try
                    {
                        var res = _bot.SaveUserInDb(update).Result;

                        if (res == true)
                        {
                            Console.WriteLine($"Пользователь с chatId {update.Message.Chat.Id} сохранён в базу данных. " +
                                              $"Имя пользователя: {update.Message.Chat.Username}.");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }

                    var commands = _bot.GetCommands();

                    foreach (var command in commands)
                    {
                        if (command.Contains(message))
                        {
                            await command.Execute(update, botClient, cancellationToken);
                            break;
                        }
                    }

                    var addProductCommands = _bot.GetTelegramCreateProductCommands();

                    foreach (var addProductCommand in addProductCommands)
                    {
                        if (addProductCommand.Contains(message))
                        {
                            await addProductCommand.Execute(update, botClient, cancellationToken, _tea, _herb, _honey);
                            break;
                        }
                    }

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

            Thread.Sleep(int.MaxValue);

        }
    }
}
