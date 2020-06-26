using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegaNewBot.Models.Commands.BaseCommands
{
    public class MultiHelloCommand : HelloCommand
    {
        public override string Name => @"/multi";
        public override Task Exec(Message mes, TelegramBotClient client)
        {
            return multiHello(mes, client);
        }
        private async Task multiHello(Message mes, TelegramBotClient client)
        {
            var chatId = mes.Chat.Id;
            var mesId = mes.MessageId;
            await client.SendTextMessageAsync(chatId, $"Первый {mes.From.FirstName}", replyToMessageId: mesId, parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown);
            Thread.Sleep(3000);
            await client.SendTextMessageAsync(chatId, $"Второй {mes.From.FirstName}", replyToMessageId: mesId, parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown);
            Thread.Sleep(1000);
            await client.SendTextMessageAsync(chatId, $"Третий {mes.From.FirstName}", replyToMessageId: mesId, parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown);
            return;
        }
    }
}