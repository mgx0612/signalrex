using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using GrainInteface;

namespace Grains
{
    public class HelloWorldGrain : Orleans.Grain, IHelloWorld
    {
        private readonly ILogger<HelloWorldGrain> _logger;

        public HelloWorldGrain(ILogger<HelloWorldGrain> logger)
        {
            this._logger = logger;
        }

        public Task<string> SayHello(string name) => Task.FromResult($"Hello {name} world!");
    }


}