namespace IoTController.API.Models
{
    public class DeviceDataModel
    {
        public string DeviceId { get; set; }
        public DateTime Timestamp { get; set; }
        public AccelerationData Acceleration { get; set; }
        public AngularVelocityData AngularVelocity { get; set; }
        public LocationData Location { get; set; }
        public double BatteryLevel { get; set; }

        public class AccelerationData
        {
            public double X { get; set; }
            public double Y { get; set; }
            public double Z { get; set; }
        }

        public class AngularVelocityData
        {
            public double X { get; set; }
            public double Y { get; set; }
            public double Z { get; set; }
        }

        public class LocationData
        {
            public double Latitude { get; set; }
            public double Longitude { get; set; }
        }
    }
}
