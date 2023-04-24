using ElectronicShopStoreApp;
using ElectronicShopStoreApp._1_DataAccess;
using ElectronicShopStoreApp._2_UI;
using ElectronicShopStoreApp._3_Other.FileAddressesExtension;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();

services.AddSingleton<IApp, App>();
services.AddSingleton<IUserCommunicationProvider, UserCommunicationProvider>();

var fileWithSqlDatabaseAddress = FileAddressesManager.fileAddress_database_address;
if (!File.Exists(fileWithSqlDatabaseAddress))
{
    Console.WriteLine("Błąd: nie znaleziono pliku z adresem bazy danych");
    return -1;
}

string mainDatabaseAddress = GetSqlDatabaseConnectionString(fileWithSqlDatabaseAddress);
services.AddDbContext<ElectronicShopStoreDbContext>(options => options.UseSqlServer(mainDatabaseAddress));

var serviceProvider = services.BuildServiceProvider();
var app = serviceProvider.GetService<IApp>();

app.Run();

return 0;

static string GetSqlDatabaseConnectionString(string fileWithSqlDatabaseAddress)
{
    string address;

    using (var reader = new StreamReader(fileWithSqlDatabaseAddress))
    {
        address = reader.ReadLine();
    }

    return address;
}