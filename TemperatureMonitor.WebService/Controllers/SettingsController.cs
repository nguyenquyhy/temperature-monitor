using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Mvc;
using TemperatureMonitor.WebService.Models;
using TemperatureMonitor.WebService.DTOs;
using Microsoft.Framework.ConfigurationModel;
using TemperatureMonitor.WebService.Logics;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace TemperatureMonitor.WebService.Controllers
{
    [Route("api/[controller]")]
    public class SettingsController : Controller
    {
        private BaseConfigurationSource configuration;
        private TableStorageLogic tableStorage;

        public SettingsController(BaseConfigurationSource configuration)
        {
            this.configuration = configuration;
            this.tableStorage = new TableStorageLogic(configuration);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<SettingsDTO> Get(string id)
        {
            var settings = await tableStorage.GetAsync<SettingsModel>(configuration.Data["Settings_TableName"], "Settings", id);
            if (settings != null)
                return settings.ToDTO();
            else
                return new SettingsDTO { DeviceId = id };
        }

        // POST api/values
        [HttpPost]
        public async Task Post([FromBody]SettingsDTO value)
        {
            if (!ModelState.IsValid)
            {
                Context.Response.StatusCode = 400;
            }
            else
            {
                var settings = new SettingsModel("Settings", value);
                await tableStorage.AddOrReplaceAsync(configuration.Data["Settings_TableName"], settings);
                Context.Response.StatusCode = 201;
            }
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task Delete(string id)
        {
            var settings = await tableStorage.GetAsync<SettingsModel>(configuration.Data["Settings_TableName"], "Settings", id);
            if (settings != null)
            {
                await tableStorage.RemoveAsync(configuration.Data["Settings_TableName"], settings);
                Context.Response.StatusCode = 204;
            }
            else
            {
                Context.Response.StatusCode = 404;
            }
        }
    }
}
