using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.SignalR;
using GrainInteface;

namespace Grains
{
    public class HelloWorldGrain : Orleans.Grain, IHelloWorld
    {
        private ILogger<HelloWorldGrain> _logger;
        private IHubContext<MyHub> _hub;

        public HelloWorldGrain(ILogger<HelloWorldGrain> logger, IHubContext<MyHub> hub)
        {
            this._logger = logger;
            this._hub = hub;
        }

        public async Task<string> SayHello(string name)
        {
            _logger.LogInformation("SayHello is called");
            var msg = $"send directly from grain Hello world {name}!";
            await _hub.Clients.All.SendAsync("ReceiveMessage", "user", msg);
            return "Hello world!";
        }
    }


}