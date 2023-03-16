
namespace ElectronicShopStoreApp._1_DataAccess.Entities
{
    public abstract class ElectronicDeviceBase : IEntity
    {
        public int Id { get; set; }
        public string Company { get; set; }
        public string DeviceType { get; set; }
        public string Model { get; set; }
        public decimal BasePrice { get; set; }
        public int InternetRating { get; set; }
        public int InStock { get; set; }
    }
}
