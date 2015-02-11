using System;

namespace TemperatureMonitor.WebService.DTOs
{
    public class SettingsDTO
    {
        public string SensorId { get; set; }
        public string Name { get; set; }
        public double? MaxTemperature { get; set; }
        public double? MinTemperature { get; set; }
    }
}