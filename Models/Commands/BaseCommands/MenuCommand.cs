using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Telegram.Bot;
using Telegram.Bot.Types;
using TelegaNewBot.Models.Keyboards.ReplyKeyboards;

namespace TelegaNewBot.Models.Commands.BaseCommands
{
    public class MenuCommand : CommandDefault
    {
        public override string Name => @"/menu";
        public override Task Exec(Message mes, TelegramBotClient client)
        {
            var keyb = new MainMenuKeyboard();
            return client.SendTextMessageAsync(mes.Chat.Id, "YOROKOBE SHOUNEN", replyMarkup:keyb.GetKeyboard());
        }
        public override bool Contains(Message inputMessage)
        {
            if (inputMessage.Type == Telegram.Bot.Types.Enums.MessageType.Text)
            {
                return base.Contains(inputMessage);
            }
            else
            {
                return false;
            }
        }
    }
}