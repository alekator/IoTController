
# IoT Controller

The IoT Controller is a demonstration project showcasing skills in building a full-stack application for IoT systems. It provides functionality for real-time device monitoring, data visualization, and basic device management.
This project serves as a practical example of integrating a modern tech stack to implement front-end and back-end development, real-time communication, and mobile app integration.

## Table of Contents

- [Overview](#overview)
- [Features](#features)
  - [Real-Time Monitoring](#real-time-monitoring)
  - [Data Visualization](#data-visualization)
  - [Anomaly Notifications](#anomaly-notifications)
  - [Device Management](#device-management)
  - [Historical Data](#historical-data)
- [Tech Stack](#tech-stack)
- [Getting Started](#getting-started)
  - [Prerequisites](#prerequisites)
  - [Installation](#installation)
  - [Configuration](#configuration)
  - [Running the App](#running-the-app)
  - [Common Issues](#common-issues)
- [Using Your Android/Ios Phone as an IoT Device](#using-your-android/ios-phone-as-an-iot-device)
  - [Steps](#steps)
  - [Sensors Used](#sensors-used)
- [License](#license)

## Overview

The **IoT Controller** is a full-stack application designed to monitor IoT devices, visualize sensor data, send notifications on anomalies, and control devices remotely. It's built using modern technologies and is perfect for enthusiasts who want to get their hands dirty with IoT.

## Features

### Real-Time Monitoring

- Display real-time data from sensors like temperature, humidity, pressure, and device status.
- Live updates ensure you're always in the loop.

### Data Visualization

- Visualize data using line charts, bar charts, and pie charts.
- Get a clear picture of your device metrics at a glance.

### Anomaly Notifications

- Set up rules to detect anomalies (e.g., temperature above 80В°C).
- Receive notifications via Email, Telegram, or Push Notifications when something's off.

### Device Management

- Turn devices on or off remotely.
- Send commands like "change operating mode" or "restart".

### Historical Data

- View measurement history in charts or tables.
- Export data to CSV or Excel for further analysis.

## Tech Stack

Here's the stack we're rocking:

- **Front-end**: Blazor WebAssembly
- **Back-end**: ASP.NET Core Web API
- **Mobile App**: .NET MAUI (Android)
- **Real-time Communication**: SignalR
- **Messaging Protocol**: MQTT
- **Database**: SQLite (can be swapped out for SQL Server or PostgreSQL)
- **Data Visualization**: MudBlazor
- **Authentication**: JWT Tokens (planned)
- **Notifications**: SMTP for Emails, Telegram Bot API, Push Notifications (FCM)

## Getting Started

### Prerequisites

Make sure you've got the following installed:

- **Visual Studio 2022** with .NET 6+ SDK
- **.NET MAUI** workload for mobile app development
- **Node.js** (if needed for any front-end tooling)
- **SQLite** (or your preferred DBMS)
- **Android or iOS Device** or Emulator for testing the mobile app
- **MQTT Broker** (like Mosquitto)

### Installation

Clone the repo:

```bash
git clone https://github.com/yourusername/IoTController.git
```

## Configuration

### Back-end (IoTController.API)

#### Database Setup:

1. Navigate to `appsettings.json` in the `IoTController.API` project.
2. Configure your connection string:

   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Data Source=IoTController.db"
   }
   ```

3. Run migrations to set up the database:

   ```bash
   dotnet ef migrations add InitialCreate
   dotnet ef database update
   ```

#### MQTT Broker Configuration:

Update MQTT settings in `appsettings.json`:

```json
"MqttSettings": {
  "BrokerAddress": "your.mqtt.broker.address",
  "BrokerPort": 1883,
  "Topic": "devices/+/data"
}
```

#### SignalR Configuration:

Ensure SignalR hubs are configured in `Program.cs`.

#### Notifications Configuration:

- Set up SMTP settings for email notifications.
- Configure Telegram Bot API token.

### Front-end (IoTController.Client)

#### API Endpoint Configuration:

In `Program.cs`, set the base address for `HttpClient`:

```csharp
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:5001/") });
```

#### MudBlazor Setup:

Ensure MudBlazor is installed and configured for data visualization.

### Mobile App (IoTController.Mobile)

#### Update Server URL:

In `DataSenderService.cs`, set the correct API endpoint:

```csharp
string apiUrl = "https://yourserver.com/api/deviceData";
```

#### Permissions:

Ensure all necessary permissions are requested in `MainPage.xaml.cs` and declared in `AndroidManifest.xml`.

#### Device ID:

Set a unique `DeviceId` in `MainPage.xaml.cs`:

```csharp
_deviceData = new DeviceDataModel
{
    DeviceId = "YourDeviceId",
    // ...
};
```

## Running the App

### Back-end

1. Open `IoTController.API` in Visual Studio.
2. Run the project (Ctrl + F5).
3. Ensure the API is running at `https://localhost:5001`.

### Front-end

1. Open `IoTController.Client` in Visual Studio.
2. Run the project (Ctrl + F5).
3. Access the app at `https://localhost:5002`.

### Mobile App

1. Connect your Android device or start an emulator.
2. Open `IoTController.Mobile` in Visual Studio.
3. Select your device/emulator as the deployment target.
4. Run the project.
5. Grant any necessary permissions on the device.

## Common Issues

- **CORS Errors**: Ensure CORS is configured to allow requests from your front-end.
- **API Connection Issues**: Double-check your API URLs and ensure the back-end is running.
- **Database Errors**: Ensure migrations have been applied and the database is accessible.

## Using Your Android/Ios Phone as an IoT Device

We've turned your Android/Ios phone into a makeshift IoT device!

### Steps

1. **Build and Deploy the Mobile App**:
   - Follow the steps in the [Running the App](#running-the-app) section.

2. **Start Data Collection**:
   - Open the app on your device.
   - Click on "Start Data Collection".
   - The app will start sending sensor data to the server every 5 seconds.

3. **View Data on the Front-end**:
   - Navigate to the "Device Data" page.
   - You should see real-time updates from your phone's sensors.

### Sensors Used

- **Accelerometer**: Measures device acceleration.
- **Gyroscope**: Measures device rotation.
- **GPS**: Gets the device's location.
- **Battery**: Monitors battery level.
- **You can add more option** It is easy to implement by following the template

## License

This project is licensed under the MIT License.
