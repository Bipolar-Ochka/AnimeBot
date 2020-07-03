using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegaNewBot.Models.Commands.BaseCommands
{
    public class ShikimoriCommand : CommandDefault
    {
        public override string Name => @"/shikimori";

        public override Task Exec(Message mes, TelegramBotClient client)
        {
            var chatId = mes.Chat.Id;
            return client.SendTextMessageAsync(chatId, $"Shikimori",replyMarkup: Bot.Inlines[Keyboards.KeyboardTarget.ShikiMenu]?.GetKeyboard());
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