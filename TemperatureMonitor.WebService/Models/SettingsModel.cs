using Microsoft.WindowsAzure.Storage.Table;
using System;
using TemperatureMonitor.WebService.DTOs;

namespace TemperatureMonitor.WebService.Models
{
    public class SettingsModel : TableEntity
    {
        public SettingsModel()
        {

        }

        public SettingsModel(string partitionKey, SettingsDTO dto) : base (partitionKey, dto.SensorId)
        {
            Name = dto.Name;
            MaxTemperature = dto.MaxTemperature;
            MinTemperature = dto.MinTemperature;
        }

        public string Name { get; set; }
        public double? MaxTemperature { get; set; }
        public double? MinTemperature { get; set; }

        public SettingsDTO ToDTO()
        {
            return new SettingsDTO
            {
                SensorId = RowKey,
                Name = Name,
                MaxTemperature = MaxTemperature,
                MinTemperature = MinTemperature
            };
        }
    }
}