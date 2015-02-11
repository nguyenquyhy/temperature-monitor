using Microsoft.WindowsAzure.Storage.Table;
using System;
using TemperatureMonitor.WebService.DTOs;

namespace TemperatureMonitor.WebService.Models
{
    public class TemperatureModel : TableEntity
    {
        public TemperatureModel()
        {

        }

        public TemperatureModel(TemperatureDTO dto)
        {
            Value = dto.Value;
        }

        public double Value { get; set; }

        public TemperatureDTO ToDTO()
        {
            return new TemperatureDTO
            {
                Id = PartitionKey,
                Value = Value
            };
        }
    }
}