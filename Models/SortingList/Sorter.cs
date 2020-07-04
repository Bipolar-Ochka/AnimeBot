using ShikiHuiki.UserClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TelegaNewBot.Models.SortingList
{
    internal enum SortType
    {
        Recently,
        Close,
        Alphabet
    }
    internal static class Sorter
    {
        public static List<SpecialUserAnimeRate> SortAnime(this List<SpecialUserAnimeRate> list,SortType type)
        {
            switch (type)
            {
                case SortType.Recently:
                    return list.OrderByDescending(item => item.UpdatedAt.DateTime).ToList();
                case SortType.Close:
                    return list.OrderBy(item => (item.AnimeInfo.EpisodesAired - item.EpisodesWatched)).ToList();
                case SortType.Alphabet:
                    return list.OrderBy(item => item.AnimeInfo.Name).ToList();
                default:
                    return list;
            }
        }
    }
}