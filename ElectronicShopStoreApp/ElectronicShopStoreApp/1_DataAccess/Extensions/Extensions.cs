
using ElectronicShopStoreApp._1_DataAccess.Entities;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ElectronicShopStoreApp._1_DataAccess.DataProviders.Extensions
{
    public static class Extensions
    {
        public static IEnumerable<ElectronicDevice> ToElectronicDevices(this IEnumerable<string> source)
        {
            foreach (var line in source) 
            {
                var columns = line.Split(',');

                yield return new ElectronicDevice()
                {
                    Company = columns[0],
                    DeviceType = columns[1],
                    Model = columns[2],
                    BasePrice = decimal.Parse(columns[3]),
                    InternetRating = int.Parse(columns[4]),
                    InStock = int.Parse(columns[5])
                };
            }
        }
    }
}
