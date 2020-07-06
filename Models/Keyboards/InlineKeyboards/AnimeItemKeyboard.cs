using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegaNewBot.Models.Keyboards.InlineKeyboards
{
    internal class AnimeItemKeyboard : InlineKeyboardDefault
    {
        public override string Name => "AnimeTitle";

        public override InlineKeyboardMarkup GetKeyboard()
        {
            var row1 = new List<InlineKeyboardButton>() { GetButton("+1"), GetButton("Back")};
            var keyb = new List<List<InlineKeyboardButton>>() { row1};
            return new InlineKeyboardMarkup(keyb);
        }

        public override Task Handler(Update upd, TelegramBotClient client)
        {
            var command = getButtonCommand(upd.CallbackQuery.Data);
            switch (command)
            {
                case "+1":
                    return Task.CompletedTask;
                case "Back":
                    return editMessage(upd, client);
                default:
                    return Task.CompletedTask;
            }
        }
        private async Task editMessage(Update upd,TelegramBotClient client)
        {
            var page = Bot.GetAnimePage(upd.CallbackQuery.From.Id);
            var keyb = Bot.GetKeyboard(KeyboardTarget.AnimeItemsMenu) as AnimeEntriesListKeyboard;
            var mark = await keyb.GetKeyboard(page, SortingList.MovePage.Current).ConfigureAwait(false);
            await client.EditMessageTextAsync(upd.CallbackQuery.Message.Chat.Id, upd.CallbackQuery.Message.MessageId, $"{page?.CurrentPage ?? 0}/{page?.LimitPage ?? 0}", replyMarkup:mark).ConfigureAwait(false);
        }
    }
}