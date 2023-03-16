using Microsoft.EntityFrameworkCore;
using ElectronicShopStoreApp._1_DataAccess.Entities;

namespace ElectronicShopStoreApp._1_DataAccess
{
    public class ElectronicShopStoreDbContext : DbContext
    {
        public ElectronicShopStoreDbContext(DbContextOptions<ElectronicShopStoreDbContext> contextOptions):
            base(contextOptions)
        { }

        public DbSet<ElectronicDevice> devices { get; set; }
    }
}
