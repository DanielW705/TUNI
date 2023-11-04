using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TUNIWEB.Hubs
{
    public class ChatHubs : Hub
    {
        public Task Sendmessage(string message)
        {
            return Clients.All.SendAsync("ReciveMessage",message);
        }
    }
}
