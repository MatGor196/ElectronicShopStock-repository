using ElectronicShopStoreApp;
using ElectronicShopStoreApp._1_DataAccess;
using ElectronicShopStoreApp._3_UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();

services.AddSingleton<IApp, App>();
services.AddSingleton<IUserCommunicationProvider, UserCommunicationProvider>();

var fileWithSqlDatabaseAddress = "C:\\Users\\user\\Desktop\\Programowanie\\GitHub\\ElectronicShopStore\\ElectronicShopStoreApp\\ElectronicShopStoreApp\\1_DataAccess\\Files\\database_address.txt";
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

//Data Source=SOURCE_SERVER;Initial Catalog=ElectronicShopStoreDatabase;Integrated Security=True;Encrypt=False