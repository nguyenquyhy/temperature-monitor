using Microsoft.AspNet.SignalR;
using System;
using System.Threading.Tasks;

namespace TemperatureMonitor.WebService.Hubs
{
    public class TemperatureHub : Hub
    {
        public void Broadcast(string id, string temperature)
        {
            Clients.All.update(id, temperature);
        }
    }
}