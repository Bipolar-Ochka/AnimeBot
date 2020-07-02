﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using TelegaNewBot.Models.Keyboards.ReplyKeyboards;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegaNewBot.Models.Keyboards.InlineKeyboards
{
    internal class ShikimoriKeyboard : InlineKeyboardDefault
    {
        public override string Name => "ShikimoriMenu";
        private string CodeUrl = @"https://shikimori.one/oauth/authorize?client_id=qiqJPDxgOqRGHTKqyluX-lJSQYTH9q0LD1Kel_vROXg&redirect_uri=urn%3Aietf%3Awg%3Aoauth%3A2.0%3Aoob&response_type=code&scope=user_rates";

        public override InlineKeyboardMarkup GetKeyboard()
        {
            var row1 = new List<InlineKeyboardButton>() { GetButton("User") };
            var row2 = new List<InlineKeyboardButton>() { GetButton("Anime") };
            var row3 = new List<InlineKeyboardButton>() { GetButton("Return to menu") };
            var keyb = new List<List<InlineKeyboardButton>>() { row1, row2, row3};
            return new InlineKeyboardMarkup(keyb);
        }

        public override Task Handler(Update upd, TelegramBotClient client)
        {
            //TODO: fill switch
            var butCommand = getButtonCommand(upd.CallbackQuery.Data);
            switch (butCommand)
            {
                case "User":
                    Bot.BotState = State.AuthCodeWait;
                    //client.SendTextMessageAsync(mes.Chat.Id, $"<a href=\"{CodeUrl}\">Перейдите по ссылке и отправьте в сообщении код авторизации</a>", parseMode: Telegram.Bot.Types.Enums.ParseMode.Html);
                    return client.EditMessageTextAsync(upd.CallbackQuery.InlineMessageId, $"<a href=\"{CodeUrl}\">Перейдите по ссылке и отправьте в сообщении код авторизации</a>", parseMode: Telegram.Bot.Types.Enums.ParseMode.Html);
                case "Anime":
                    break;
                case "Return to menu":                    
                    return client.EditMessageTextAsync(upd.CallbackQuery.InlineMessageId, "main menu");
                default:
                    break;
            }
        }
    }
}