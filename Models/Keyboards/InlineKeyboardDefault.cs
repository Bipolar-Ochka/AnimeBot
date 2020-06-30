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
    internal abstract class InlineKeyboardDefault
    {
        public abstract string Name { get; }
        public abstract InlineKeyboardMarkup GetKeyboard();
        public abstract Task Handler(string buttonData, Message mes, TelegramBotClient client);
        protected InlineKeyboardButton GetButton(string buttonName)
        {
            var button = new InlineKeyboardButton();
            button.CallbackData = $"{Name}{buttonName}";
            button.Text = buttonName;
            return button;
        }

        public virtual bool Contains(string messageString)
        {
            return messageString.Contains(this.Name);
        }
    }
}