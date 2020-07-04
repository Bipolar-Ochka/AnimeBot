using ShikiHuiki.UserClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TelegaNewBot.Models.Tasks.ShikimoriTasks;

namespace TelegaNewBot.Models
{
    internal class UserAnimeList
    {
        public DateTime UpdatedAt { get; set; }
        public List<SpecialUserAnimeRate> AnimeList {get; set;}
        public UserAnimeList(List<SpecialUserAnimeRate>list, DateTime upd)
        {
            this.AnimeList = list;
            this.UpdatedAt = upd;
        }
    }
}