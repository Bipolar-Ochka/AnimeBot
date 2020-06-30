using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Telegram.Bot;
using Telegram.Bot.Types;
using ShikiHuiki;

namespace TelegaNewBot.Models.Commands.ShikimoriCommands
{
    public class ShikimoreAuthorizationCommand : CommandDefault
    {
        public override string Name => @"/shiki";
        private bool isRunning = false;

        public override Task Exec(Message mes, TelegramBotClient client)
        {
            if (isRunning)
            {
                return Task.FromResult(0);
            }
            return Aue(mes, client);
        }

        private async Task Aue(Message mes, TelegramBotClient client)
        {
            isRunning = true;
            ShikimoriClient shikiClient;
            if (!Bot.animeAccounts.TryGetValue(mes.From.Id, out shikiClient))
            {
                try
                {
                    shikiClient = new ShikimoriClient();
                    var code = mes.Text.Trim().Split(' ').Last().Trim();
                    await client.SendTextMessageAsync(mes.Chat.Id, $"Authorizing with code={code}", replyToMessageId: mes.MessageId);
                    await shikiClient.ShikiLogin(code).ConfigureAwait(false);
                    await client.SendTextMessageAsync(mes.Chat.Id, $"Logged as {shikiClient.GetNickname()}", replyToMessageId: mes.MessageId).ConfigureAwait(false);
                    //shikiClient.ErrorTextEvent += new Action<string>(async errorStr => await client.SendTextMessageAsync(mes.Chat.Id, $"{errorStr}", replyToMessageId: mes.MessageId).ConfigureAwait(false));
                    Bot.animeAccounts.Add(mes.From.Id, shikiClient);
                    this.isRunning = false;
                }
                catch(Exception e)
                {
                    await client.SendTextMessageAsync(mes.Chat.Id, $"Error {e.Message} Stack={e.StackTrace}", replyToMessageId: mes.MessageId).ConfigureAwait(false);
                    this.isRunning = false;
                    return;
                }
            }
        }
        public override bool Contains(Message inputMessage)
        {
            if (inputMessage.Type == Telegram.Bot.Types.Enums.MessageType.Text)
            {
                return base.Contains(inputMessage);
            }
            else
            {
                return false;
            }
        }
    }
}