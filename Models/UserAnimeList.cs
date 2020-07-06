using ShikiHuiki.UserClass;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using TelegaNewBot.Models.SortingList;
using TelegaNewBot.Models.Tasks.ShikimoriTasks;

namespace TelegaNewBot.Models
{
    internal class UserAnimeList
    {
        public DateTime UpdatedAt { get; set; }
        public int UserId { get; private set; }
        public List<SpecialUserAnimeRate> AnimeList { get; set; }
        public UserAnimeList(List<SpecialUserAnimeRate>list, DateTime upd,int userId)
        {
            this.AnimeList = list;
            Trace.Indent();
            Trace.WriteLine("list set");
            this.UpdatedAt = upd;
            Trace.WriteLine("upd set");
            this.Page = new AnimePage(this.AnimeList,userId);
            Trace.WriteLine("page set");
            this.UserId = userId;
            Trace.WriteLine("id set");
            Trace.Unindent();
        }
        public AnimePage Page { get; private set; }
    }
}