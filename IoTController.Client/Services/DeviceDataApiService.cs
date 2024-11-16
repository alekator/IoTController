using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using IoTController.Models;

public class DeviceDataApiService
{
    private readonly HttpClient _httpClient;

    public DeviceDataApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<DeviceDataModel>> GetDeviceDataAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<DeviceDataModel>>("api/deviceData");
    }
}
