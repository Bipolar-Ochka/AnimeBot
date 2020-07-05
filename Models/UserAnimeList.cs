using ShikiHuiki.UserClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TelegaNewBot.Models.SortingList;
using TelegaNewBot.Models.Tasks.ShikimoriTasks;

namespace TelegaNewBot.Models
{
    internal class UserAnimeList
    {
        public DateTime UpdatedAt { get; set; }
        public List<SpecialUserAnimeRate> AnimeList { get { return this.AnimeList; } set { AnimeList = value; this.Page = new AnimePage(this.AnimeList); } }
        public UserAnimeList(List<SpecialUserAnimeRate>list, DateTime upd)
        {
            this.AnimeList = list;
            this.UpdatedAt = upd;
            this.Page = new AnimePage(this.AnimeList);
        }
        public AnimePage Page { get; private set; }
    }
}