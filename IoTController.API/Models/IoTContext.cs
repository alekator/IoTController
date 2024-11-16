using Microsoft.EntityFrameworkCore;

namespace IoTController.Shared;

public class IoTContext : DbContext
{
    public IoTContext(DbContextOptions<IoTContext> options) : base(options)
    {
    }

    public DbSet<DeviceData> DeviceData { get; set; }
}
