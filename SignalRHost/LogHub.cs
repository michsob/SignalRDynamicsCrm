using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace SignalRHost
{
    [HubName("logHub")]
    public class LogHub : Hub
    {
        public void Send(string name, string message)
        {
            Clients.All.addLog(name, message);
        }
    }
}