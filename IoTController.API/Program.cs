using IoTController.API.Hubs;
using IoTController.API.Services;
using IoTController.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// ���������� �������� � ���������
builder.Services.AddControllers();

// ���������� ��������� ���� ������
builder.Services.AddDbContext<IoTContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// ���������� SignalR
builder.Services.AddSignalR();

// ����������� MqttService
builder.Services.AddSingleton<MqttService>();

// **���������� Swagger**
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "IoTController.API", Version = "v1" });
});

// ��������� CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder => builder
            .WithOrigins("https://localhost:7226") // �������� 7226 �� ���� ������ �������
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials());
});

var app = builder.Build();

// ��������� ��������� ��������� ��������
if (app.Environment.IsDevelopment())
{
    // **������������� Swagger**
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "IoTController.API v1");
    });
}

app.UseHttpsRedirection();

// ���������� CORS ����� ��������������
app.UseCors("CorsPolicy");

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    // ������������� ��� SignalR ����
    endpoints.MapHub<DeviceDataHub>("/deviceDataHub");
});

// ������ MqttService
var mqttService = app.Services.GetRequiredService<MqttService>();
await mqttService.ConnectAsync();

app.Run();
