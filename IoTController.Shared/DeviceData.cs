using System;

namespace IoTController.Shared
{
    public class DeviceData
    {
        public int Id { get; set; }
        public string DeviceId { get; set; }
        public DateTime Timestamp { get; set; }
        public double Temperature { get; set; }
        public double Humidity { get; set; }
    }
}
