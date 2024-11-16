using Microsoft.Maui.Devices;
using Microsoft.Maui.Devices.Sensors;
using System;
using System.Threading.Tasks;

namespace IoTController.Mobile.Services
{
    public class SensorService
    {
        public event EventHandler<AccelerometerChangedEventArgs> AccelerometerDataReceived;
        public event EventHandler<GyroscopeChangedEventArgs> GyroscopeDataReceived;

        public void StartSensors()
        {

            if (Accelerometer.IsSupported)
            {
                Accelerometer.ReadingChanged += Accelerometer_ReadingChanged;
                Accelerometer.Start(SensorSpeed.UI);
            }


            if (Gyroscope.IsSupported)
            {
                Gyroscope.ReadingChanged += Gyroscope_ReadingChanged;
                Gyroscope.Start(SensorSpeed.UI);
            }


        }

        public void StopSensors()
        {

            if (Accelerometer.IsSupported)
            {
                Accelerometer.Stop();
                Accelerometer.ReadingChanged -= Accelerometer_ReadingChanged;
            }


            if (Gyroscope.IsSupported)
            {
                Gyroscope.Stop();
                Gyroscope.ReadingChanged -= Gyroscope_ReadingChanged;
            }


        }


        private void Accelerometer_ReadingChanged(object sender, AccelerometerChangedEventArgs e)
        {
            AccelerometerDataReceived?.Invoke(this, e);
        }

        private void Gyroscope_ReadingChanged(object sender, GyroscopeChangedEventArgs e)
        {
            GyroscopeDataReceived?.Invoke(this, e);
        }


        public double GetBatteryLevel()
        {
            return Battery.ChargeLevel * 100;
        }


        public async Task<Location> GetLocationAsync()
        {
            try
            {
                var status = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();
                if (status != PermissionStatus.Granted)
                {
                    status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
                    if (status != PermissionStatus.Granted)
                    {
                        return null;
                    }
                }

                return await Geolocation.GetLocationAsync(new GeolocationRequest
                {
                    DesiredAccuracy = GeolocationAccuracy.Medium,
                    Timeout = TimeSpan.FromSeconds(30)
                });
            }
            catch (Exception ex)
            {

                return null;
            }
        }
    }
}
