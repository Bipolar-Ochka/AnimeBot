using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegaNewBot.Models.Commands.BaseCommands
{
    public class HelloCommand : CommandDefault
    {
        public override string Name => @"/hello";

        public override Task Exec(Message mes, TelegramBotClient client)
        {
            var chatId = mes.Chat.Id;
            var mesId = mes.MessageId;
            return client.SendTextMessageAsync(chatId, $"Здарова бандит {mes.From.FirstName} id={mes.From.Id}", replyToMessageId: mesId, parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown);

        }
        
        public override bool Contains(Message inputMessage)
        {
            if(inputMessage.Type == Telegram.Bot.Types.Enums.MessageType.Text)
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