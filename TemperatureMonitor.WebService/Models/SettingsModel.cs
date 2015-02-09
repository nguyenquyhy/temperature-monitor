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

        public SettingsModel(string partitionKey, SettingsDTO dto) : base (partitionKey, dto.DeviceId)
        {
            MaxTemperature = dto.MaxTemperature;
            MinTemperature = dto.MinTemperature;
        }

        public double? MaxTemperature { get; set; }
        public double? MinTemperature { get; set; }

        public SettingsDTO ToDTO()
        {
            return new SettingsDTO
            {
                DeviceId = RowKey,
                MaxTemperature = MaxTemperature,
                MinTemperature = MinTemperature
            };
        }
    }
}