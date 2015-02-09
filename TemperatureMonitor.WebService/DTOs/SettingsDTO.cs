using System;

namespace TemperatureMonitor.WebService.DTOs
{
    public class SettingsDTO
    {
        public string DeviceId { get; set; }
        public double? MaxTemperature { get; set; }
        public double? MinTemperature { get; set; }
    }
}