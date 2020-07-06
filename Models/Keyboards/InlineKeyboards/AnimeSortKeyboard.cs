using ShikiHuiki;
using ShikiHuiki.Constants;
using ShikiHuiki.UserClass;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
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
            Bot.BotState = State.Processing;
            var butCommand = getButtonCommand(upd.CallbackQuery.Data);
            ShikimoriClient shikiClient;
            if (!Bot.animeAccounts.TryGetValue(upd.CallbackQuery.From.Id, out shikiClient))
            {
                return client.EditMessageTextAsync(upd.CallbackQuery.Message.Chat.Id, upd.CallbackQuery.Message.MessageId, "You are not logged",replyMarkup:Bot.Inlines[KeyboardTarget.ShikiMenu]?.GetKeyboard());
            }
            if(butCommand == "Return to menu")
            {
                return client.EditMessageTextAsync(upd.CallbackQuery.Message.Chat.Id, upd.CallbackQuery.Message.MessageId, "Shikimori", replyMarkup: Bot.Inlines[KeyboardTarget.ShikiMenu]?.GetKeyboard());
            }
            switch (butCommand)
            {
                case "Recently watching":
                case "Close to finish":
                case "Alphabetically":
                case "Refresh":
                    return soloMethod(upd, client, shikiClient, DefaultStatus);
                default:
                    break;
            }
            return Task.CompletedTask;
        }   
        private async Task soloMethod(Update upd, TelegramBotClient client,ShikimoriClient shikiClient, AnimeStatus status)
        {
            var userId = upd.CallbackQuery.From.Id;
            var mesId = upd.CallbackQuery.Message.MessageId;
            var chatId = upd.CallbackQuery.Message.Chat.Id;
            if(Bot.GetUserAnime(userId) is null)
            {
                await Task.Yield();
                var bag = await shikiClient.V2AnimeReturn(status).ConfigureAwait(false);
                await Task.Yield();
                Trace.WriteLine($"var bag {bag.Count} \n {Environment.StackTrace}");
                var lst = new UserAnimeList(bag.ToList(), DateTime.Now, userId);
                Trace.WriteLine($"lst {lst.UpdatedAt}");
                Bot.animeAccountsLists[userId] = new UserAnimeList(bag.ToList(), DateTime.Now, userId);
                Trace.WriteLine("dict");
            }
            var page = Bot.GetAnimePage(userId);
            var keyb = Bot.GetKeyboard(KeyboardTarget.AnimeItemsMenu) as AnimeEntriesListKeyboard;
            var markup = await keyb.GetKeyboard(page, MovePage.Current).ConfigureAwait(false);
            Trace.WriteLine("markup");
            await client.EditMessageTextAsync(chatId,mesId,"Anime",replyMarkup:markup).ConfigureAwait(false);
            Trace.WriteLine("edit mes");
        }
    }
}