using IoTController.Mobile.Models;
using IoTController.Mobile.Services;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Devices.Sensors;
using System;
using System.Threading.Tasks;

namespace IoTController.Mobile
{
    public partial class MainPage : ContentPage
    {
        private SensorService _sensorService;
        private DataSenderService _dataSenderService;
        private DeviceDataModel _deviceData;
        private bool _isCollectingData;

        public MainPage()
        {
            InitializeComponent();

            _sensorService = new SensorService();
            _dataSenderService = new DataSenderService();

            _sensorService.AccelerometerDataReceived += OnAccelerometerDataReceived;
            _sensorService.GyroscopeDataReceived += OnGyroscopeDataReceived;

            _deviceData = new DeviceDataModel
            {
                DeviceId = "YourDeviceId",
                Acceleration = new DeviceDataModel.AccelerationData(),
                AngularVelocity = new DeviceDataModel.AngularVelocityData(),
            };
        }

        private void OnStartButtonClicked(object sender, EventArgs e)
        {
            _isCollectingData = true;
            _sensorService.StartSensors();
            StartSendingData();
        }

        private void OnStopButtonClicked(object sender, EventArgs e)
        {
            _isCollectingData = false;
            _sensorService.StopSensors();
        }

        private void OnAccelerometerDataReceived(object sender, AccelerometerChangedEventArgs e)
        {
            var data = e.Reading;
            _deviceData.Acceleration.X = data.Acceleration.X;
            _deviceData.Acceleration.Y = data.Acceleration.Y;
            _deviceData.Acceleration.Z = data.Acceleration.Z;

            AccelerometerLabel.Text = $"Акселерометр: X={data.Acceleration.X:F2}, Y={data.Acceleration.Y:F2}, Z={data.Acceleration.Z:F2}";
        }

        private void OnGyroscopeDataReceived(object sender, GyroscopeChangedEventArgs e)
        {
            var data = e.Reading;
            _deviceData.AngularVelocity.X = data.AngularVelocity.X;
            _deviceData.AngularVelocity.Y = data.AngularVelocity.Y;
            _deviceData.AngularVelocity.Z = data.AngularVelocity.Z;

            GyroscopeLabel.Text = $"Гироскоп: X={data.AngularVelocity.X:F2}, Y={data.AngularVelocity.Y:F2}, Z={data.AngularVelocity.Z:F2}";
        }

        private async void StartSendingData()
        {
            while (_isCollectingData)
            {
                _deviceData.Timestamp = DateTime.UtcNow;
                _deviceData.BatteryLevel = _sensorService.GetBatteryLevel();
                BatteryLabel.Text = $"Заряд батареи: {_deviceData.BatteryLevel:F0}%";

                _deviceData.Location = await GetLocationDataAsync();

                if (_deviceData.Location != null)
                {
                    LocationLabel.Text = $"Местоположение: Lat={_deviceData.Location.Latitude:F6}, Lon={_deviceData.Location.Longitude:F6}";
                }
                else
                {
                    LocationLabel.Text = "Местоположение: Недоступно";
                }

                await _dataSenderService.SendDataAsync(_deviceData);

                await Task.Delay(5000);
            }
        }

        private async Task<DeviceDataModel.LocationData> GetLocationDataAsync()
        {
            var location = await _sensorService.GetLocationAsync();
            if (location != null)
            {
                return new DeviceDataModel.LocationData
                {
                    Latitude = location.Latitude,
                    Longitude = location.Longitude
                };
            }
            return null;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            RequestPermissions();
        }

        private async void RequestPermissions()
        {
            var status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
            if (status != PermissionStatus.Granted)
            {

            }


        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            _sensorService.StopSensors();
            _isCollectingData = false;
        }
    }
}
