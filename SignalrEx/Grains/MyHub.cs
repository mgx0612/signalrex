using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using Orleans;
using GrainInteface;
using Microsoft.AspNetCore.Authorization;

namespace Grains
{
    [Authorize]
    public class MyHub:Hub
    {
        private readonly IGrainFactory _grains;
        public MyHub(IGrainFactory grains)
        {
            _grains = grains;
        }

        public async Task SendMessage(string message)
        {
            var user = Context.UserIdentifier;
            var grain = _grains.GetGrain<IHelloWorld>("simon");
            var res = await grain.SayHello(Context.ConnectionId + message);
            await Clients.All.SendAsync("ReceiveMessage", user, "send from Hub"+ res);
        }
    }
}
