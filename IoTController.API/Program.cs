using IoTController.API.Hubs;
using IoTController.API.Services;
using IoTController.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Добавление сервисов в контейнер
builder.Services.AddControllers();

// Добавление контекста базы данных
builder.Services.AddDbContext<IoTContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Добавление SignalR
builder.Services.AddSignalR();

// Регистрация MqttService
builder.Services.AddSingleton<MqttService>();

// **Добавление Swagger**
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "IoTController.API", Version = "v1" });
});

// Настройка CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder => builder
            .WithOrigins("https://localhost:7226") // Замените 7226 на порт вашего клиента
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials());
});

var app = builder.Build();

// Настройка конвейера обработки запросов
if (app.Environment.IsDevelopment())
{
    // **Использование Swagger**
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "IoTController.API v1");
    });
}

app.UseHttpsRedirection();

// Используем CORS перед маршрутизацией
app.UseCors("CorsPolicy");

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    // Маршрутизация для SignalR хаба
    endpoints.MapHub<DeviceDataHub>("/deviceDataHub");
});

// Запуск MqttService
var mqttService = app.Services.GetRequiredService<MqttService>();
await mqttService.ConnectAsync();

app.Run();
