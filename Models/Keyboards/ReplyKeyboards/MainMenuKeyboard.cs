using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegaNewBot.Models.Keyboards.ReplyKeyboards
{
    internal class MainMenuKeyboard : KeyboardDefault
    {
        public override string Name => @"MainMenu_";

        public override ReplyKeyboardMarkup GetKeyboard()
        {
            var keyRow1 = new List<KeyboardButton>()
            {
                new KeyboardButton(@"/hello"),
                new KeyboardButton(@"/test"),
                new KeyboardButton(@"/shikimori")
            };
            var keyTable = new List<List<KeyboardButton>>()
            {
                keyRow1,
            };
            var keys = new ReplyKeyboardMarkup(keyTable, resizeKeyboard: true, oneTimeKeyboard: true);
            return keys;
        }

        public override Task Handler(string buttonData, Message mes, TelegramBotClient client)
        {
            throw new NotImplementedException();
        }
    }
}