using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.UI.WebControls;
using TelegaNewBot.Models;
using TelegaNewBot.Models.Tasks.ShikimoriTasks;
using Telegram.Bot.Types;

namespace TelegaNewBot.Controllers
{
    [Route(@"bot/message/update")]
    public class MessageRecievedController : ApiController
    {
        [HttpGet]
        public string Get()
        {
            return "ALL FINE";
        }
        [HttpPost]
        public async Task<OkResult> MessageGet([FromBody]Update update)
        {
            if (update == null) return Ok();
            var client = await Bot.LoadClient();
            //await client.SendTextMessageAsync(update.Message.Chat.Id, $"{update.Type}");
            switch (update.Type)
            {
                case Telegram.Bot.Types.Enums.UpdateType.Message:                    
                    var message = update.Message;
                    if(Bot.BotState == State.Default)
                    {
                        var commands = Bot.Commands;
                        foreach(Models.Commands.CommandDefault comd in commands)
                        {
                            if (comd.Contains(message) && message.Date >= Bot.startTime)
                            {
                                await comd.Exec(message, client);
                                break;
                            }
                        }
                    }
                    else if(Bot.BotState == State.AuthCodeWait)
                    {
                        var task = new ShikimoriAuthentication();
                        await task.GetTask(message, client);
                        Bot.BotState = State.Default;
                    }
                    break;
                case Telegram.Bot.Types.Enums.UpdateType.CallbackQuery:
                    if(update.CallbackQuery.Message is null)
                    {
                        break;
                    }
                    var fromButtonData = update.CallbackQuery.Data;
                    await client.EditMessageTextAsync(update.CallbackQuery.Message.Chat.Id,update.CallbackQuery.Message.MessageId, fromButtonData, replyMarkup: Bot.Inlines[Models.Keyboards.KeyboardTarget.ShikiMenu]?.GetKeyboard()).ConfigureAwait(false) ;
                    var keyb = Bot.Inlines;
                    foreach (var k in keyb)
                    {
                        if (k.Value.Contains(fromButtonData))
                        {
                           await k.Value.Handler(update, client);
                        }
                    }
                    break;
                default:
                    break;
            }
            return Ok();
        }
    }
}
