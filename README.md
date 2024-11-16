# IoT Controller

Welcome to the **IoT Controller**! This bad boy is all about monitoring your devices in real-time, visualizing data, handling anomalies, and giving you the power to control your devices from one spot. Let's dive in!

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
- [Using Your Android Phone as an IoT Device](#using-your-android-phone-as-an-iot-device)
  - [Steps](#steps)
  - [Sensors Used](#sensors-used)
- [Contributing](#contributing)
- [License](#license)
- [Contact](#contact)

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

- Set up rules to detect anomalies (e.g., temperature above 80°C).
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
- **Android Device** or Emulator for testing the mobile app
- **MQTT Broker** (like Mosquitto)

### Installation

Clone the repo:

```bash
git clone https://github.com/yourusername/IoTController.git
