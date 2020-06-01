using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Authorization;
using Orleans;
using GrainInteface;

namespace SignalRChat
{
    [Authorize]
    public class ChatHub :Hub
    {
        private readonly IGrainFactory _grains;
        public ChatHub(IGrainFactory grains)
        {
            _grains = grains;
        }
        
        public async Task SendMessage(string message)
        {
           var user = Context.UserIdentifier;
           var grain = _grains.GetGrain<IHelloWorld>("simon");
           var res = await grain.SayHello(user);
           await Clients.All.SendAsync("ReceiveMessage", user, Context.ConnectionId + res + message);
        }
        
    }
}
