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
        public async Task<OkResult> MessageGet([FromBody] Update update)
        {
            if (update == null) return Ok();
            var commands = Bot.Commands;
            var message = update.Message;
            var client = await Bot.LoadClient();
            foreach(Models.Commands.CommandDefault comd in commands)
            {
                if (comd.Contains(message) && message.Date >= Bot.startTime)
                {
                    await comd.Exec(message, client);
                    break;
                }
            }
            return Ok();
        }
    }
}
