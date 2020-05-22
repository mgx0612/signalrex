using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GrainInteface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Orleans;

namespace SignalRChat.Controllers
{
    
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class HelloWorldController : ControllerBase
    {
        private readonly IGrainFactory _client;
        private readonly IHelloWorld _grain;

        public HelloWorldController(IGrainFactory client)
        {
            _client = client;
            _grain = _client.GetGrain<IHelloWorld>("simon");
        }

        [AllowAnonymous]
        [HttpGet]
        public Task<String> SayHello([FromQuery] string name)
        {
            return _grain.SayHello(name);
        }
    }
}
