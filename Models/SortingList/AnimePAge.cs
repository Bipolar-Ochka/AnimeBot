using ShikiHuiki.UserClass;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public AnimePage(List<SpecialUserAnimeRate> src)
        {
            this._source = src;
            this.CurrentPage = 1;
            this.LimitPage = Convert.ToInt32(Math.Ceiling((double)src.Count / _size));
        }
        public InlineKeyboardMarkup getCurrentPage(List<InlineKeyboardButton> menu)
        {
            var page = _source.Skip((CurrentPage - 1 <=0)?0:CurrentPage*_size).Take(_size);            
            var keyb = new List<List<InlineKeyboardButton>>();
            foreach(var item in page)
            {
                var button = new InlineKeyboardButton();
                button.CallbackData = $"AnimeItems_{item.AnimeId}";
                button.Text = $"({item.EpisodesWatched}/{item.AnimeInfo?.EpisodesAired}) {item.AnimeInfo?.NameRus.Take(20)}...";
                var list = new List<InlineKeyboardButton>() {
                    button
                };
                keyb.Add(list);
            }
            keyb.Add(menu);
            return new InlineKeyboardMarkup(keyb);
        }
        public InlineKeyboardMarkup getNextPage(List<InlineKeyboardButton> menu)
        {
            this.CurrentPage += 1;
            return getCurrentPage(menu);
        }
        public InlineKeyboardMarkup getPrevPage(List<InlineKeyboardButton> menu)
        {
            this.CurrentPage -= 1;
            return getCurrentPage(menu);
        }
        public InlineKeyboardMarkup getFirstPage(List<InlineKeyboardButton> menu)
        {
            this.CurrentPage = 1;
            return getCurrentPage(menu);
        }
    }
}