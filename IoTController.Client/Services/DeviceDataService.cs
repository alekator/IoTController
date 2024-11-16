using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading.Tasks;
using IoTController.Shared;

namespace IoTController.Client.Services
{
    public class DeviceDataService
    {
        private HubConnection _hubConnection;

        public event Action<DeviceData> OnDeviceDataReceived;

        public async Task StartConnection()
        {
            var hubUrl = "https://localhost:7161/deviceDataHub"; // Замените <API_PORT> на порт вашего API

            _hubConnection = new HubConnectionBuilder()
                .WithUrl(hubUrl)
                .Build();

            _hubConnection.On<DeviceData>("ReceiveDeviceData", (data) =>
            {
                OnDeviceDataReceived?.Invoke(data);
            });

            await _hubConnection.StartAsync();
        }

        public async Task StopConnection()
        {
            if (_hubConnection != null)
            {
                await _hubConnection.StopAsync();
                await _hubConnection.DisposeAsync();
            }
        }
    }
}
