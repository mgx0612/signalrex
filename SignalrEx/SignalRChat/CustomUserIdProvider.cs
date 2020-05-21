using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
namespace SignalRChat
{
    public class CustomUserIdProvider : IUserIdProvider
    {
        private readonly ILogger<CustomUserIdProvider> _logger;
        
        public CustomUserIdProvider (ILogger<CustomUserIdProvider> logger)
        {
            _logger = logger;
        }

        public string GetUserId(HubConnectionContext connection)
        {
            foreach (var claim in connection.User.Claims)
            {
                _logger.LogInformation("connection.User claim type {Type} and value {Value} ", claim.Type, claim.Value);
            }

            return connection.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }
    }
}
