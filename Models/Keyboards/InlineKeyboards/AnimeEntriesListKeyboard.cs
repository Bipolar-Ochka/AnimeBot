using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TelegaNewBot.Models.SortingList;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegaNewBot.Models.Keyboards.InlineKeyboards
{
    internal class AnimeEntriesListKeyboard : InlineKeyboardDefault
    {
        public override string Name => "AnimeItems";

        public override InlineKeyboardMarkup GetKeyboard()
        {
            var rowLast = new List<InlineKeyboardButton>() { GetButton("Prev"), GetButton("Menu"), GetButton("Next") };
            var keyb = new List<List<InlineKeyboardButton>>() { rowLast };
            return new InlineKeyboardMarkup(keyb);
        }
        public InlineKeyboardMarkup GetKeyboard(AnimePage page, MovePage move)
        {
            bool isLastPage = page.CurrentPage == page.LimitPage;
            bool isFirstPage = page.CurrentPage == 1;
            bool isNextLastPage = page.CurrentPage + 1 == page.LimitPage;
            bool isPrevLastPage = page.CurrentPage <= 2;
            List<InlineKeyboardButton> menu = null;
            switch (move)
            {
                case MovePage.Current:
                    if (isLastPage)
                    {
                        menu = new List<InlineKeyboardButton>()
                        {
                            GetButton("Prev"), GetButton("Menu")
                        };
                    }else if (isFirstPage)
                    {
                        menu = new List<InlineKeyboardButton>()
                        {
                             GetButton("Menu"), GetButton("Next")
                        };
                    }
                    else
                    {
                        menu = new List<InlineKeyboardButton>()
                        {
                             GetButton("Prev"), GetButton("Menu"), GetButton("Next")
                        };
                    }
                    break;
                case MovePage.Next:
                    if (isNextLastPage)
                    {
                        menu = new List<InlineKeyboardButton>()
                        {
                            GetButton("Prev"), GetButton("Menu")
                        };
                    }
                    else
                    {
                        menu = new List<InlineKeyboardButton>()
                        {
                             GetButton("Prev"), GetButton("Menu"), GetButton("Next")
                        };
                    }
                    break;
                case MovePage.Prev:
                    if (isPrevLastPage)
                    {
                        menu = new List<InlineKeyboardButton>()
                        {
                             GetButton("Menu"), GetButton("Next")
                        };
                    }
                    else
                    {
                        menu = new List<InlineKeyboardButton>()
                        {
                             GetButton("Prev"), GetButton("Menu"), GetButton("Next")
                        };
                    }
                    break;
            }
            return page.getPrevPage(menu);
        }

        public override Task Handler(Update upd, TelegramBotClient client)
        {
            //TODO: fill handler
            var command = getButtonCommand(upd.CallbackQuery.Data);
            var animeId = 0;
            if(int.TryParse(command,out animeId))
            {

            }
            else
            {
                switch (command)
                {
                    case "Prev":
                        break;
                    case "Next":
                        break;
                    case "Menu":
                        break;
                }
            }
        }
    }
}