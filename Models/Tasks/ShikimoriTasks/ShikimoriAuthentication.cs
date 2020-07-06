using ShikiHuiki;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegaNewBot.Models.Tasks.ShikimoriTasks
{
    internal class ShikimoriAuthentication : IBotTask
    {
        public async Task GetTask(Message mes, TelegramBotClient client)
        {
            try
            {
                if(mes.Text is null || mes.Text.Length != 43)
                {
                    await client.SendTextMessageAsync(mes.Chat.Id, $"Wrong code").ConfigureAwait(false);
                    return;
                }
                var shikiClient = new ShikimoriClient();
                var code = mes.Text;
                await client.SendTextMessageAsync(mes.Chat.Id, $"<i>Authorizing with code :</i>{Environment.NewLine}<code>{code}</code>", parseMode: Telegram.Bot.Types.Enums.ParseMode.Html);
                await shikiClient.ShikiLogin(code).ConfigureAwait(false);
                await client.SendTextMessageAsync(mes.Chat.Id, $"Logged as {shikiClient.GetNickname()}",replyMarkup:Bot.GetKeyboard(Keyboards.KeyboardTarget.ShikiMenu)?.GetKeyboard()).ConfigureAwait(false);
                Bot.animeAccounts[mes.From.Id] = shikiClient;
            }
            catch (Exception e)
            {
                await client.SendTextMessageAsync(mes.Chat.Id, $"Error {e.Message}{Environment.NewLine}Stack = {e.StackTrace}", replyToMessageId: mes.MessageId).ConfigureAwait(false);
                return;
            }
        }
    }
}