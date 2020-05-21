using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Authorization;

namespace SignalRChat
{
    [Authorize]
    public class ChatHub :Hub
    {
        public async Task SendMessage(string message)
        {
           var user = Context.UserIdentifier;
           await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}
