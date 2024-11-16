using Microsoft.EntityFrameworkCore;

namespace IoTController.API.Models
{
    public class IoTContext : DbContext
    {
        public IoTContext(DbContextOptions<IoTContext> options)
            : base(options)
        {
        }

        public DbSet<DeviceDataModel> DeviceData { get; set; }

    }
}
