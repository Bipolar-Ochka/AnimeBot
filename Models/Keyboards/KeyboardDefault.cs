using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegaNewBot.Models.Keyboards
{
    internal abstract class KeyboardDefault
    {
        public abstract string Name { get; }
        public abstract ReplyKeyboardMarkup GetKeyboard();
        public abstract Task Handler(string buttonData, Message mes, TelegramBotClient client);

        public virtual bool Contains(string messageString)
        {
            return messageString.Contains(this.Name);
        }
    }
}