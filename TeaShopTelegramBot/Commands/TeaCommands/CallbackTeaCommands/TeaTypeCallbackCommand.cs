﻿using Application.TlgUsers.Interfaces;
using TeaShopTelegramBot.Common.Abstractions;
using TeaShopTelegramBot.Exceptions;
using TeaShopTelegramBot.Messages.TeaMessages;
using TeaShopTelegramBot.Models;

namespace TeaShopTelegramBot.Commands.TeaCommands.CallbackTeaCommands;

public class TeaTypeCallbackCommand : BaseCallbackCommand
{
    private readonly IMemoryCachService _memoryCachService;

    private readonly TeaNameMessage _teaNameMessage = new();

    public TeaTypeCallbackCommand(IMemoryCachService memoryCachService, IGetUserLanguageQuery getUserLanguageQuery)
    {
        _memoryCachService = memoryCachService;
    }

    public override char CallbackDataCode => 'F';

    public override async Task CallbackExecute(Update update, ITelegramBotClient client)
    {
        if (update.CallbackQuery != null && update.CallbackQuery.Data != null)
        {
            if (update.CallbackQuery.Data == "FRed")
            {
                await EditMenuMessage(update, client, TeaType.Red);

                return;
            }
            if (update.CallbackQuery.Data == "FGreen")
            {
                await EditMenuMessage(update, client, TeaType.Green);

                return;
            }
            if (update.CallbackQuery.Data == "FWhite")
            {
                await EditMenuMessage(update, client, TeaType.White);

                return;
            }
            if (update.CallbackQuery.Data == "FOloong")
            {
                await EditMenuMessage(update, client, TeaType.Oolong);

                return;
            }
            if (update.CallbackQuery.Data == "FShuPuer")
            {
                await EditMenuMessage(update, client, TeaType.ShuPuer);

                return;
            }
            if (update.CallbackQuery.Data == "FShenPuer")
            {
                await EditMenuMessage(update, client, TeaType.ShenPuer);
            }
            if (update.CallbackQuery.Data == "FCraft")
            {
                await EditMenuMessage(update, client, TeaType.CraftTeas);
            }
        }
    }

    private async Task EditMenuMessage(Update update, ITelegramBotClient client, TeaType teaType)
    {
        if (update.CallbackQuery != null && update.CallbackQuery.Message != null)
        {
            long chatId = update.CallbackQuery.Message.Chat.Id;

            int messageId = update.CallbackQuery.Message.MessageId;

            try
            {
                var teaDto = SetTeaTypeAndCommandState(teaType, update);

                await _teaNameMessage.GetMessage(chatId, messageId, client, teaDto);
            }
            catch (MemoryCachException ex)
            {
                await ex.SendExceptionMessage(chatId, client);
            }
        }
    }

    private TeaDto SetTeaTypeAndCommandState(TeaType teaType, Update update)
    {
        var tea = new TeaDto()
        {
            TeaType = teaType
        };

        _memoryCachService.SetMemoryCach(tea);

        _memoryCachService.SetMemoryCach("teaName", update);

        return tea;
    }
}

