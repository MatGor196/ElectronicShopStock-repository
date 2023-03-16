using ElectronicShopStoreApp._1_DataAccess;
using ElectronicShopStoreApp._1_DataAccess.DataProviders;
using ElectronicShopStoreApp._3_UI;

namespace ElectronicShopStoreApp
{
    public class App : IApp
    {
        private readonly ElectronicShopStoreDbContext _mainDbContext;
        private readonly IUserCommunicationProvider _userCommunicationProvider;
        public App(ElectronicShopStoreDbContext mainDbContext,
                   IUserCommunicationProvider userCommunicationProvider) 
        {
            _mainDbContext = mainDbContext;
            _mainDbContext.Database.EnsureCreated();
            _userCommunicationProvider = userCommunicationProvider;
        }
        private void DisplayBeginningMenu()
        {
            Console.WriteLine("Witaj!");
            Console.WriteLine("Oto prosta aplikacja symulująca magazyn sklepu z elektroniką");
            Console.WriteLine("Z jej użyciem możesz monitorować ilość towaru w sklepie");
            Console.WriteLine();
            Console.WriteLine("Co chcesz zrobić?:");
            Console.WriteLine();
            Console.WriteLine("1. Zobaczyć wszystkie towary");
            Console.WriteLine("2. Zobaczyć towary spełniające kryteria...");
            Console.WriteLine("3. Dodać towary");
            Console.WriteLine("4. Usunąć towary");
            Console.WriteLine("5. Zmienić obsługiwane modele towarów");
            Console.WriteLine("6. Wyjście");
            Console.WriteLine();
            Console.Write("Podaj liczbę [1-6]: ");
        }

        public void Run()
        {
            //SaveStandardDevicesToSqlDatabase();

            bool appRun = true;
            while(appRun)
            {
                DisplayBeginningMenu();
                var userinputStr = Console.ReadLine();

                if (int.TryParse(userinputStr, out int userInput))
                    switch (userInput)
                    {
                        case 1:
                            _userCommunicationProvider.ShowAllItems_MenuMethod();
                            break;
                        case 2:
                            _userCommunicationProvider.ShowAllItemsByCriteria_MenuMethod();
                            break;
                        case 3:
                            _userCommunicationProvider.AddItems_MenuMethod();
                            break;
                        case 4:
                            _userCommunicationProvider.RemoveItems_MenuMethod();
                            break;
                        case 5:
                            _userCommunicationProvider.ChangeModelsOfItems_MenuMethod();
                            break;
                        case 6:
                            appRun = false;
                            break;
                        default:
                            break;
                    }

                Console.Clear();
            }
        }

        private void SaveStandardDevicesToSqlDatabase()
        {
            var csvReader = new CsvReader();
            var modelsCsvFileAddress = "C:\\Users\\user\\Desktop\\Projekty_C#\\MatGor196\\ElectronicShopStore-repository\\ElectronicShopStoreApp\\ElectronicShopStoreApp\\1_DataAccess\\Files\\devices_models.csv";
            var devices = csvReader.ProcessElectronicDevices(modelsCsvFileAddress);

            foreach (var device in devices)
            {
                //device.InStock = 0;
                _mainDbContext.Add(device);
            }

            _mainDbContext.SaveChanges();
        }
    }
}
