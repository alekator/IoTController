using IoTController.Mobile.Models;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace IoTController.Mobile.Services
{
    public class DataSenderService
    {
        private static readonly HttpClient httpClient = new HttpClient();

        public async Task SendDataAsync(DeviceDataModel data)
        {
            try
            {
                string apiUrl = "https://yourserver.com/api/deviceData";

                var response = await httpClient.PostAsJsonAsync(apiUrl, data);

                if (response.IsSuccessStatusCode)
                {
                }
                else
                {

                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
