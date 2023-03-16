using ElectronicShopStoreApp._1_DataAccess.Entities;
using ElectronicShopStoreApp._1_DataAccess.DataProviders.Extensions;
namespace ElectronicShopStoreApp._1_DataAccess.DataProviders
{
    public class CsvReader : ICsvReader
    {
        public List<ElectronicDevice> ProcessElectronicDevices(string filePath)
        {
            if (!File.Exists(filePath))
                return new List<ElectronicDevice>();

            var electronicDevices = File.ReadAllLines(filePath)
                                        .Skip(1)
                                        .Where(l => l.Length > 0)
                                        .ToElectronicDevices()
                                        .ToList();

            return electronicDevices;
        }
    }
}
