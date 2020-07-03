using ShikiHuiki;
using ShikiHuiki.UserClass;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegaNewBot.Models.Tasks.ShikimoriTasks
{
    public class ShikimoriSpecialAnimeList : IBotTask
    {
        public IReadOnlyList<SpecialUserAnimeRate> AnimeList { get; private set; }
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
                var bag = new ConcurrentBag<SpecialUserAnimeRate>();
                await shikiClient.GetAnimeFull(bag, ShikiHuiki.Constants.AnimeStatus.Watching).ConfigureAwait(false);
                AnimeList = bag.ToList();
            }
        }
    }
}