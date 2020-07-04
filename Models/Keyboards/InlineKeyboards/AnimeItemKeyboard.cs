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
    internal class AnimeItemKeyboard : InlineKeyboardDefault
    {
        public override string Name => "AnimeList";

        public override InlineKeyboardMarkup GetKeyboard()
        {
            throw new NotImplementedException();
        }

        public override Task Handler(Update upd, TelegramBotClient client)
        {
            throw new NotImplementedException();
        }
    }
}