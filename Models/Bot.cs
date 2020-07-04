﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Telegram.Bot;
using ShikiHuiki;
using TelegaNewBot.Models.Tasks.ShikimoriTasks;
using ShikiHuiki.UserClass;
using TelegaNewBot.Models.Keyboards;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types.Enums;

namespace TelegaNewBot.Models
{
    enum State
    {
        Default,
        AuthCodeWait
    }
    public static class Bot
    {
        private static TelegramBotClient client;
        private static List<Commands.CommandDefault> commandDefaults;
        internal static Dictionary<KeyboardTarget, InlineKeyboardDefault> Inlines { get; private set; }
        internal static Dictionary<int, ShikimoriClient> animeAccounts = new Dictionary<int, ShikimoriClient>();
        internal static Dictionary<int, UserAnimeList> animeAccountsLists = new Dictionary<int, UserAnimeList>();
        internal static DateTime startTime;
        internal static State BotState;

        public static IReadOnlyCollection<Commands.CommandDefault> Commands { get => commandDefaults.AsReadOnly(); }

        public static async Task<TelegramBotClient> LoadClient()
        {
            if (client != null)
                return client;

            InitComms();
            InitKeys();
            client = new TelegramBotClient(Settings.ApiKey);
            var hook = string.Format(Settings.Url, "bot/message/update");
            await client.SetWebhookAsync(hook, allowedUpdates: new UpdateType[0]);
            startTime = DateTime.Now;
            BotState = State.Default;
            return client;
        }

        private static void InitKeys()
        {
            if(Inlines == null)
            {
                Inlines = new Dictionary<KeyboardTarget, InlineKeyboardDefault>()
                {
                    { KeyboardTarget.ShikiMenu, new Keyboards.InlineKeyboards.ShikimoriKeyboard() },
                    {KeyboardTarget.AnimeSortMenu, new Keyboards.InlineKeyboards.AnimeSortKeyboard() },
                };
                
            }
        }

        private static void InitComms()
        {
            if(commandDefaults == null)
            {
                commandDefaults = new List<Commands.CommandDefault>();
                commandDefaults.Add(new Commands.BaseCommands.HelloCommand());
                commandDefaults.Add(new Commands.BaseCommands.MultiHelloCommand());
                commandDefaults.Add(new Commands.ShikimoriCommands.ShikimoreAuthorizationCommand());
                commandDefaults.Add(new Commands.ShikimoriCommands.OngoingAnimeCommand());
                commandDefaults.Add(new Commands.BaseCommands.MenuCommand());
                commandDefaults.Add(new Commands.BaseCommands.TestCommand());
                commandDefaults.Add(new Commands.BaseCommands.ShikimoriCommand());
            }
        }

    }
}