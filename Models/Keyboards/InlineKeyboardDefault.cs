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
    internal enum KeyboardTarget
    {
        ShikiMenu,
        AnimeSortMenu,
        AnimeItemsMenu,
        AnimeTitle,
    }
    internal abstract class InlineKeyboardDefault
    {
        public abstract string Name { get; }
        public abstract InlineKeyboardMarkup GetKeyboard();
        public abstract Task Handler (Update upd, TelegramBotClient client);
        protected InlineKeyboardButton GetButton(string buttonName)
        {
            var button = new InlineKeyboardButton();
            button.CallbackData = $"{Name}_{buttonName}";
            button.Text = buttonName;
            return button;
        }

        protected string getButtonCommand(string queryData)
        {
            return queryData.Trim().Split('_').Last();
        }
        public virtual bool Contains(string messageString)
        {
            return messageString.Contains(this.Name);
        }
    }
}