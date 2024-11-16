using System;
using System.ComponentModel.DataAnnotations;

namespace IoTController.API.Models
{
    public class DeviceData
    {
        [Key]
        public int Id { get; set; }

        public string DeviceId { get; set; }

        public DateTime Timestamp { get; set; }

        public double Temperature { get; set; }

        public double Humidity { get; set; }

        // Добавьте дополнительные поля по мере необходимости
    }
}
