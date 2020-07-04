using ShikiHuiki;
using ShikiHuiki.Constants;
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
    public class ShikimoriSpecialAnimeList
    {
        public IReadOnlyList<SpecialUserAnimeRate> AnimeList { get; private set; }
        public async Task GetTask(ShikimoriClient shikiClient, AnimeStatus status)
        {
            var bag = new ConcurrentBag<SpecialUserAnimeRate>();
            await shikiClient.GetAnimeFull(bag, status).ConfigureAwait(false);
            AnimeList = bag.ToList();
        }
        public static async Task<ConcurrentBag<SpecialUserAnimeRate>> GetTaskBag(ShikimoriClient shikiClient, AnimeStatus status)
        {
            return await shikiClient.GetAnimeFullReturn(status).ConfigureAwait(false);
        }
    }
}