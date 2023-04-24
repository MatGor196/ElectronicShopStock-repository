using ElectronicShopStoreApp._1_DataAccess;
using ElectronicShopStoreApp._1_DataAccess.Entities;
using ElectronicShopStoreApp._2_ApplicationServices.LogExtension;

namespace ElectronicShopStoreApp._3_UI
{
    public class UserCommunicationProvider : IUserCommunicationProvider
    {
        private readonly ElectronicShopStoreDbContext _mainDbContex;

        public UserCommunicationProvider(ElectronicShopStoreDbContext mainDbContex)
        {
            _mainDbContex = mainDbContex;
        }
        public void ShowAllItems_MenuMethod()
        {
            Console.WriteLine();

            var allDevices = _mainDbContex.devices.ToList();
            foreach (var device in allDevices)
            {
                Console.WriteLine(device.ToString());
            }

            Console.ReadLine();
        }

        public void ShowAllItemsByCriteria_MenuMethod()
        {
            Console.WriteLine();
            Console.WriteLine("Co chcesz zrobić?");
            Console.WriteLine("1. Zobaczyć towary po typach urządzeń");
            Console.WriteLine("2. Zobaczyć towary po producencie");
            Console.WriteLine("3. Zobaczyć towary po ocenie internetowej");
            Console.WriteLine("4. Zobaczyć towary z ceną mniejszą niż");
            Console.WriteLine("5. Zobaczyć towary z ceną większą niż");
            Console.WriteLine("6. Wróć");
            Console.Write("Podaj liczbę [1-6]: ");
            var inputStr = Console.ReadLine();

            Console.WriteLine();
            if (int.TryParse(inputStr, out int input))
                switch (input)
                {
                    case 1:
                        SeeDevicesByType();
                        break;
                    case 2:
                        SeeDevicesByCompany();
                        break;
                    case 3:
                        SeeDevicesByInternetRating();
                        break;
                    case 4:
                        SeeDevicesWithPriceLessThan();
                        break;
                    case 5:
                        SeeDevicesWithPriceBiggerThan();
                        break; 
                    case 6:
                        break;
                    default:
                        Console.WriteLine("Zły zakres");
                        break;
                }
            else
                Console.WriteLine("Nieprawidłowe dane wejściowe");

            Console.ReadLine();
        }

        private void SeeDevicesWithPriceBiggerThan()
        {
            Console.Write("Podaj cenę: ");
            var inputPriceStr = Console.ReadLine();

            if (!decimal.TryParse(inputPriceStr, out decimal inputPrice))
            {
                Console.WriteLine("Błędne dane");
                Console.ReadLine();
                return;
            }

            Console.WriteLine();

            var properDevices = _mainDbContex.devices
                                    .Where(d => d.BasePrice > inputPrice)
                                    .ToList();

            DisplayProperDevices(properDevices);
        }

        private void SeeDevicesWithPriceLessThan()
        {
            Console.Write("Podaj cenę: ");
            var inputPriceStr = Console.ReadLine();

            if (!decimal.TryParse(inputPriceStr, out decimal inputPrice))
            {
                Console.WriteLine("Błędne dane");
                Console.ReadLine();
                return;
            }

            Console.WriteLine();

            var properDevices = _mainDbContex.devices
                                    .Where(d => d.BasePrice < inputPrice)
                                    .ToList();

            DisplayProperDevices(properDevices);
        }

        private void SeeDevicesByInternetRating()
        {
            Console.Write("Podaj ocenę internetową [1-5]: ");
            var inputRatingStr = Console.ReadLine();

            if(InproperInternetRating(inputRatingStr))
            {
                Console.WriteLine("Błędne dane");
                Console.ReadLine();
                return;
            }

            var inputRating = int.Parse(inputRatingStr);

            Console.WriteLine();

            var properDevices = _mainDbContex.devices
                                    .Where(d => d.InternetRating == inputRating)
                                    .ToList();

            DisplayProperDevices(properDevices);
        }

        private bool InproperInternetRating(string? inputRatingStr)
        {
            if(!int.TryParse(inputRatingStr, out int result))
                return true;

            var inputRating = int.Parse(inputRatingStr);
            if (inputRating < 1 || inputRating > 5)
                return true;

            return false;
        }

        private void SeeDevicesByCompany()
        {
            Console.Write("Podaj producenta: ");

            var inputCompany = Console.ReadLine();
            Console.WriteLine();

            var properDevices = _mainDbContex.devices
                                    .Where(d => d.Company == inputCompany)
                                    .ToList();

            DisplayProperDevices(properDevices);
        }

        private void SeeDevicesByType()
        {
            Console.Write("Podaj typ urządzenia: ");

            var inputType = Console.ReadLine();
            Console.WriteLine();

            var properDevices = _mainDbContex.devices
                                    .Where(d => d.DeviceType == inputType)
                                    .ToList();

            DisplayProperDevices(properDevices);
        }

        private void DisplayProperDevices(List<ElectronicDevice> properDevices)
        {
            if (properDevices.Count == 0)
                Console.WriteLine("Nic nie znaleziono");
            else
            {
                foreach (var device in properDevices)
                {
                    Console.WriteLine(device.ToString());
                }
            }
        }

        public void AddItems_MenuMethod()
        {
            Console.WriteLine();
            Console.WriteLine("Jeśli chcesz skończyć wpisz: w");
            
            while(true)
            {
                Console.WriteLine();
                Console.Write("Podaj nazwę modelu towaru do dodania: ");
                var inputModelName = Console.ReadLine();

                if (inputModelName == "w")
                    break;

                if (ItemInDatabase(inputModelName))
                    AddItemToDatabase(inputModelName);
                else
                    Console.WriteLine("Nie ma takiego modelu w bazie danych");
            }

            Console.ReadLine();
        }

        private void AddItemToDatabase(string? inputModelName)
        {
            var device = _mainDbContex.devices.Single(d => d.Model == inputModelName);
            device.InStock += 1;
            _mainDbContex.SaveChanges();

            EventsClass.SaveAddingOfItemToEventLog(inputModelName);
        }

        private bool ItemInDatabase(string? inputModelName)
        {
            if (inputModelName == null)
                return false;

            return _mainDbContex.devices.Any(d => d.Model == inputModelName);
        }

        public void RemoveItems_MenuMethod()
        {
            Console.WriteLine();
            Console.WriteLine("Jeśli chcesz skończyć wpisz: w");

            while (true)
            {
                Console.WriteLine();
                Console.Write("Podaj nazwę modelu towaru do usunięcia: ");
                var inputModelName = Console.ReadLine();

                if (inputModelName == "w")
                    break;

                if (ItemInDatabase(inputModelName))
                    RemoveItemFromDatabase(inputModelName);
                else
                    Console.WriteLine("Nie ma takiego modelu w bazie danych");
            }

            Console.ReadLine();
        }

        private void RemoveItemFromDatabase(string? inputModelName)
        {
            var device = _mainDbContex.devices.Single(d => d.Model == inputModelName);

            if(device.InStock <= 0)
            {
                Console.WriteLine("Brak w magazynie");
                return;
            }

            device.InStock -= 1;

            _mainDbContex.SaveChanges();

            EventsClass.SaveRemovingOfItemToEventLog(inputModelName);
        }

        public void ChangeModelsOfItems_MenuMethod()
        {
            Console.WriteLine();
            Console.WriteLine("Co chcesz zrobić?");
            Console.WriteLine("1. Dodać model");
            Console.WriteLine("2. Usunąć model");
            Console.WriteLine("3. Wróć");
            Console.Write("Podaj liczbę [1-3]: ");
            var inputStr = Console.ReadLine();

            if(int.TryParse(inputStr, out int input))
                switch(input)
                {
                    case 1:
                        AddModelToDatabase();
                        break;
                    case 2:
                        RemoveModelFromDatabase();
                        break;
                    case 3:
                        break;
                    default:
                        Console.WriteLine("Zły zakres");
                        break;
                }
            else
                Console.WriteLine("Nieprawidłowe dane wejściowe");
        }

        private void AddModelToDatabase()
        {
            Console.WriteLine();
            Console.Write("Podaj typ urządzenia: ");
            var deviceType = Console.ReadLine();
            Console.Write("Podaj nazwę modelu: ");
            var model = Console.ReadLine();
            Console.Write("Podaj producenta: ");
            var company = Console.ReadLine();
            Console.Write("Podaj internet rating [1-5]: ");
            var internetRating = int.Parse(Console.ReadLine());
            Console.Write("Podaj cenę bazową w sklepie: ");
            var basePrice = decimal.Parse(Console.ReadLine());
            Console.Write("Podaj aktualną ilość w magazynie: ");
            var inStock = int.Parse(Console.ReadLine());

            var device = new ElectronicDevice()
            {
                Company = company,
                DeviceType = deviceType,
                Model = model,
                BasePrice = basePrice,
                InternetRating = internetRating,
                InStock = inStock
            };

            _mainDbContex.Add(device);
            _mainDbContex.SaveChanges();

            Console.WriteLine("Dodano z sukcesem");

            for (int i = 0; i < inStock; i++)
                EventsClass.SaveAddingOfItemToEventLog(model);
            EventsClass.SaveAddingOfModelToEventLog(model);

            Console.ReadLine();
        }

        private void RemoveModelFromDatabase()
        {
            Console.WriteLine();
            Console.Write("Podaj nazwę modelu do usunięcia: ");
            var model = Console.ReadLine();

            if(ItemInDatabase(model))
            {
                var deviceToRemove = _mainDbContex.devices.First(d => d.Model == model);
                var amountInStock = deviceToRemove.InStock;
                _mainDbContex.devices.Remove(deviceToRemove);
                _mainDbContex.SaveChanges();

                Console.WriteLine("Usunięto z sukcesem");

                EventsClass.SaveRemovingOfModelToEventLog(model);
                for (int i = 0; i < amountInStock; i++)
                    EventsClass.SaveRemovingOfItemToEventLog(model);
            }
            else
                Console.WriteLine("Nie ma takiego modelu w bazie danych");

            Console.ReadLine();
        }
    }
}
