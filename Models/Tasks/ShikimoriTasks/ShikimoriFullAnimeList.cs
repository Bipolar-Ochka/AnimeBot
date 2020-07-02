using ShikiHuiki;
using ShikiHuiki.UserClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegaNewBot.Models.Tasks.ShikimoriTasks
{
    public class ShikimoriFullAnimeList : IBotTask
    {
        public IReadOnlyList<UserAnimeRate> AnimeList { get; private set; } = null;
        public async Task GetTask(Message mes, TelegramBotClient client)
        {
            ShikimoriClient shikiClient;
            if (!Bot.animeAccounts.TryGetValue(mes.From.Id, out shikiClient))
            {
                await client.SendTextMessageAsync(mes.Chat.Id, $"You are not logged to get animelist");
                return;
            }
            else
            {
                var list = new List<UserAnimeRate>();
                await shikiClient.GetAnime(list).ConfigureAwait(false);
                if(list is null)
                {
                    await client.SendTextMessageAsync(mes.Chat.Id, $"Anime list is empty");
                }
                else
                {
                    AnimeList = list.AsReadOnly();
                }
                return;
            }
        }
    }
}