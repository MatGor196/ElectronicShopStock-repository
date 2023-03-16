using ElectronicShopStoreApp._1_DataAccess.Entities;

namespace ElectronicShopStoreApp._1_DataAccess.DataProviders
{
    public interface ICsvReader
    {
        List<ElectronicDevice> ProcessElectronicDevices(string filePath);
    }
}
