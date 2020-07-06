using ShikiHuiki.UserClass;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegaNewBot.Models.SortingList
{
    public enum MovePage
    {
        Prev,
        Current,
        Next
    }
    internal class AnimePage
    {
        public int CurrentPage { get; private set; }
        public int LimitPage { get; private set; }
        private List<SpecialUserAnimeRate> _source;
        private int _size = 5;
        private int userId;
        public AnimePage(List<SpecialUserAnimeRate> src, int userId)
        {
            this._source = src;
            Trace.Indent();
            Trace.WriteLine("src set");
            this.CurrentPage = 1;
            Trace.WriteLine("cur page set");
            this.LimitPage = Convert.ToInt32(Math.Ceiling((double)src.Count / _size));
            Trace.WriteLine("limit set");
            this.userId = userId;
            Trace.WriteLine("id set");
            Trace.Unindent();
        }
        public async Task<InlineKeyboardMarkup> getCurrentPage(List<InlineKeyboardButton> menu)
        {
            var page = _source.Skip((CurrentPage - 1 <=0)?0:CurrentPage*_size).Take(_size);            
            var keyb = new List<List<InlineKeyboardButton>>();
            await Task.Delay(1000).ConfigureAwait(false);
            foreach(var item in page)
            {
                if(item.AnimeInfo is null)
                {
                    await item.GetAnimeInfo().ConfigureAwait(false);
                }
                var button = new InlineKeyboardButton();
                button.CallbackData = $"AnimeItems_{item.AnimeId}";
                button.Text = $"({item.EpisodesWatched}/{item.AnimeInfo?.EpisodesAired}) {item.AnimeInfo.NameRus}...";
                var list = new List<InlineKeyboardButton>() {
                    button
                };
                keyb.Add(list);
            }
            keyb.Add(menu);
            return new InlineKeyboardMarkup(keyb);
        }
        public Task<InlineKeyboardMarkup> getNextPage(List<InlineKeyboardButton> menu)
        {
            this.CurrentPage += 1;
            return getCurrentPage(menu);
        }
        public Task<InlineKeyboardMarkup> getPrevPage(List<InlineKeyboardButton> menu)
        {
            this.CurrentPage -= 1;
            return getCurrentPage(menu);
        }
        public Task<InlineKeyboardMarkup> getFirstPage(List<InlineKeyboardButton> menu)
        {
            this.CurrentPage = 1;
            return getCurrentPage(menu);
        }
    }
}