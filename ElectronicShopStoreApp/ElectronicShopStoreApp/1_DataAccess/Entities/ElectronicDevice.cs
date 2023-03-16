
namespace ElectronicShopStoreApp._1_DataAccess.Entities
{
    public class ElectronicDevice : ElectronicDeviceBase
    {
        public string ToString() => $"{Model} \n W magazynie: {InStock} \n {DeviceType}, {Company}, {BasePrice}, {InternetRating}/5 \n";
    }
}