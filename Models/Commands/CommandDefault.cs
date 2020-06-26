using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Telegram.Bot.Types;
using Telegram.Bot;
using System.Threading.Tasks;

namespace TelegaNewBot.Models.Commands
{
    public abstract class CommandDefault
    {
        public abstract string Name { get; }

        public abstract Task Exec(Message mes, TelegramBotClient client);

        public virtual bool Contains(Message inputMessage)
        {
            //return inputMessage.Text.Contains(this.Name) && inputMessage.Text.Contains(Settings.Name);
            return inputMessage.Text.Contains(this.Name);
        }
    }
}