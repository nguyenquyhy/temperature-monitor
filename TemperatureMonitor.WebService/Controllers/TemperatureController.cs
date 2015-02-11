using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Mvc;
using TemperatureMonitor.WebService.Hubs;
using Microsoft.AspNet.SignalR.Infrastructure;
using Microsoft.AspNet.SignalR;
using TemperatureMonitor.WebService.Models;
using Microsoft.Framework.ConfigurationModel;
using TemperatureMonitor.WebService.DTOs;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace TemperatureMonitor.WebService.Controllers
{
    [Route("api/[controller]")]
    public class TemperatureController : Controller
    {
        private IHubContext temperatureHub;

        public TemperatureController(IConnectionManager connectionManager)
        {
            temperatureHub = connectionManager.GetHubContext<TemperatureHub>();
        }

        // POST api/Temperature
        [HttpPost]
        public void Post([FromBody]TemperatureDTO value)
        {
            temperatureHub.Clients.All.update(value.Id, value.Value);
        }

    }
}
