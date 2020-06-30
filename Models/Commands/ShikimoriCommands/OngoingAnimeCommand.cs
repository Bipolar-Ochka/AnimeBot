using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Telegram.Bot;
using Telegram.Bot.Types;
using ShikiHuiki;
using ShikiHuiki.UserClass;
using System.Text;
using ShikiHuiki.Constants;

namespace TelegaNewBot.Models.Commands.ShikimoriCommands
{
    public class OngoingAnimeCommand : CommandDefault
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
                    await client.SendTextMessageAsync(mes.Chat.Id, $"List is empty", replyToMessageId: mes.MessageId);
                }
                foreach (var item in list.Where(item=> item.Status == AnimeParams.AnimeStatusString[AnimeStatus.Watching]))
                {
                    str.Append($"{item.AnimeInfo.NameRus}{Environment.NewLine}Просмотрено {item.EpisodesWatched} из {item.AnimeInfo.EpisodesAired}{Environment.NewLine}");
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