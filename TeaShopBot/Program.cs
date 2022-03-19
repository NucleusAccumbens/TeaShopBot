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

                if (update.Message.Type == MessageType.Photo)
                {
                    var chatId = update.Message.Chat.Id;
                    var fileId = update.Message.Photo[2].FileId;
                    Console.WriteLine(fileId);
                    InputOnlineFile file = new InputOnlineFile(fileId);
                    await botClient.SendPhotoAsync(chatId, file);
                }

                if ()
                {
                    update.Message.Caption.Contains()
                }

                if (update.Type == UpdateType.CallbackQuery)
                {
                    var callbackQuery = update.CallbackQuery;
                    var callbackCommands = _bot.GetCallbackCommands();

                    foreach (var callbackCommand in callbackCommands)
                    {
                        if (callbackCommand.Contains(callbackQuery))
                        {
                            await callbackCommand.CallbackExecute(update, botClient, cancellationToken, _tea);
                            break;
                        }
                    }
                }

                if (update.Message != null && update.Message.Type == MessageType.Text)
                {
                    var chatId = update.Message.Chat.Id;
                    var message = update.Message;

                    Console.WriteLine($"Получено сообщение '{message.Text}' от пользователя номер {chatId}.");

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
                            await addProductCommand.Execute(update, botClient, cancellationToken, _tea);
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

        }
    }
}
