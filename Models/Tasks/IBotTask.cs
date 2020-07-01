using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegaNewBot.Models.Tasks
{
    internal interface IBotTask
    {
        Task GetTask(Message mes, TelegramBotClient client);
    }
}
