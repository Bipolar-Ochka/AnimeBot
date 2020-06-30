using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Telegram.Bot;
using Telegram.Bot.Types;
using ShikiHuiki;
using ShikiHuiki.UserClass;
using System.Text;
using ShikiHuiki.Constants;
using System.Threading.Tasks;

namespace TelegaNewBot.Models.Commands.ShikimoriCommands
{
    public class OngoingPagesCommand : CommandDefault
    {
        public override string Name => @"/ongoings";
        private bool isRunning = false;

        public override Task Exec(Message mes, TelegramBotClient client)
        {
            if (isRunning)
            {
                return Task.FromResult(0);
            }
            return ong(mes, client);
        }

        private async Task ong(Message mes, TelegramBotClient client)
        {
            this.isRunning = true;
            ShikimoriClient shikiClient;
            if (!Bot.animeAccounts.TryGetValue(mes.From.Id, out shikiClient))
            {
                await client.SendTextMessageAsync(mes.Chat.Id, $"You are not logged try /shiki [authCode] command", replyToMessageId: mes.MessageId);
                this.isRunning = false;
                return;
            }
            else
            {
                var list = new List<UserAnimeRate>();
                StringBuilder str = new StringBuilder();
                await shikiClient.GetAnime(list);
                if (list is null)
                {
                    await client.SendTextMessageAsync(mes.Chat.Id, $"List is empty");
                }                
                await client.SendTextMessageAsync(mes.Chat.Id, str.ToString());
                this.isRunning = false;
                return;
            }
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