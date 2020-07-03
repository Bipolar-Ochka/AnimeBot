using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegaNewBot.Models.Keyboards.InlineKeyboards
{
    internal class AnimeSortKeyboard : InlineKeyboardDefault
    {
        //TODO: выбор сортировки списка 
        public override string Name => "AnimeSort";

        public override InlineKeyboardMarkup GetKeyboard()
        {
            var row1 = new List<InlineKeyboardButton>() { GetButton("Recently watching") };
            var row2 = new List<InlineKeyboardButton>() { GetButton("Close to finish") };
            var row3 = new List<InlineKeyboardButton>() { GetButton("Alphabetically") };
            var row4 = new List<InlineKeyboardButton>() { GetButton("Return to menu") };
            var keyb = new List<List<InlineKeyboardButton>>() { row1, row2, row3, row4 };
            return new InlineKeyboardMarkup(keyb);
        }

        public override Task Handler(Update upd, TelegramBotClient client)
        {
            //TODO: fill switch
            var butCommand = getButtonCommand(upd.CallbackQuery.Data);
            switch (butCommand)
            {
                case "Recently watching":
                    break;
                case "Close to finish":
                    break;
                case "Alphabetically":
                    break;
                case "Return to menu":
                    break;
                default:
                    break;
            }
            return Task.CompletedTask;
        }
    }
}