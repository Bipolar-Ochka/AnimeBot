using ShikiHuiki;
using ShikiHuiki.Constants;
using ShikiHuiki.UserClass;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using TelegaNewBot.Models.SortingList;
using TelegaNewBot.Models.Tasks.ShikimoriTasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegaNewBot.Models.Keyboards.InlineKeyboards
{
    internal class AnimeSortKeyboard : InlineKeyboardDefault
    {
        public override string Name => "AnimeSort";
        internal AnimeStatus DefaultStatus { get; set; } = AnimeStatus.Watching;
        internal List<SpecialUserAnimeRate> animeList { get; private set; }
        private DateTime _updateTime;

        public override InlineKeyboardMarkup GetKeyboard()
        {
            var row1 = new List<InlineKeyboardButton>() { GetButton("Recently watching") };
            var row2 = new List<InlineKeyboardButton>() { GetButton("Close to finish") };
            var row3 = new List<InlineKeyboardButton>() { GetButton("Alphabetically") };
            var row4 = new List<InlineKeyboardButton>() { GetButton("Refresh"), GetButton("Return to menu") };
            var keyb = new List<List<InlineKeyboardButton>>() { row1, row2, row3, row4 };
            return new InlineKeyboardMarkup(keyb);
        }

        public override Task Handler(Update upd, TelegramBotClient client)
        {
            var butCommand = getButtonCommand(upd.CallbackQuery.Data);
            ShikimoriClient shikiClient;
            if (!Bot.animeAccounts.TryGetValue(upd.CallbackQuery.From.Id, out shikiClient))
            {
                return client.EditMessageTextAsync(upd.CallbackQuery.Message.Chat.Id, upd.CallbackQuery.Message.MessageId, "You are not logged",replyMarkup:Bot.Inlines[KeyboardTarget.ShikiMenu]?.GetKeyboard());
            }
            switch (butCommand)
            {
                case "Recently watching":
                    //TODO: return edited message with animes
                    break;
                case "Close to finish":
                    break;
                case "Alphabetically":
                    break;
                case "Return to menu":
                    return client.EditMessageTextAsync(upd.CallbackQuery.Message.Chat.Id, upd.CallbackQuery.Message.MessageId, "Shikimori", replyMarkup: Bot.Inlines[KeyboardTarget.ShikiMenu]?.GetKeyboard());
                case "Refresh":
                default:
                    break;
            }
            return Task.CompletedTask;
        }
        private async Task setList(int userId, ShikimoriClient client, AnimeStatus status, bool isNeedUpdate = false)
        {

            if (!Bot.animeAccountsLists.ContainsKey(userId) || DateTime.Now >= Bot.animeAccountsLists[userId]?.UpdatedAt.AddMinutes(10) || isNeedUpdate)
            {
                var bag = await ShikimoriSpecialAnimeList.GetTaskBag(client,status).ConfigureAwait(false);
                Bot.animeAccountsLists[userId] = new UserAnimeList(bag.ToList(),DateTime.Now);
            }
        }

        private List<SpecialUserAnimeRate> sortList(int userId,SortType type)
        {
            if (Bot.animeAccountsLists.ContainsKey(userId))
            {
                return Bot.animeAccountsLists[userId].AnimeList.SortAnime(type);
            }
            else
            {
                return null;
            }
        }


    }
}