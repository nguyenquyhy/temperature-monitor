using System;

namespace TemperatureMonitor.WebService.DTOs
{
    public class TemperatureDTO
    {
        public string SensorId { get; set; }
        public double Value { get; set; }
    }
}