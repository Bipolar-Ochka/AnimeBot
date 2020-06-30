using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegaNewBot.Models.Commands.BaseCommands
{
    public class TestCommand : CommandDefault
    {
        public override string Name => @"/test";
        public override Task Exec(Message mes, TelegramBotClient client)
        {
            var tagged = $"<a href=\"https://shikimori.one/system/animes/original/13541.jpg\"></a>";
            return client.SendTextMessageAsync(mes.Chat.Id, tagged, parseMode: Telegram.Bot.Types.Enums.ParseMode.Html, disableWebPagePreview: false);
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