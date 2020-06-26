﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Telegram.Bot;
using ShikiHuiki;

namespace TelegaNewBot.Models
{
    public static class Bot
    {
        private static TelegramBotClient client;
        private static List<Commands.CommandDefault> commandDefaults;
        internal static Dictionary<int, ShikimoriClient> animeAccounts = new Dictionary<int, ShikimoriClient>();
        internal static DateTime startTime;

        public static IReadOnlyCollection<Commands.CommandDefault> Commands { get => commandDefaults.AsReadOnly(); }

        public static async Task<TelegramBotClient> LoadClient()
        {
            if (client != null)
                return client;

            InitComms();
            client = new TelegramBotClient(Settings.ApiKey);
            var hook = string.Format(Settings.Url, "bot/message/update");
            await client.SetWebhookAsync(hook);
            startTime = DateTime.Now;
            return client;
        }

        private static void InitComms()
        {
            if(commandDefaults == null)
            {
                commandDefaults = new List<Commands.CommandDefault>();
                commandDefaults.Add(new Commands.BaseCommands.HelloCommand());
                commandDefaults.Add(new Commands.ShikimoreAuthorizationCommand());
                commandDefaults.Add(new Commands.OngoingAnimeCommand());
                commandDefaults.Add(new Commands.BaseCommands.MultiHelloCommand());
            }
        }
    }
}