
namespace ElectronicShopStoreApp._2_ApplicationServices.LogExtension
{
    public static class EventsClass
    {
        private static readonly string _eventLogFileAddress = "C:\\Users\\user\\Desktop\\Projekty_C#\\MatGor196\\ElectronicShopStore-repository\\ElectronicShopStoreApp\\ElectronicShopStoreApp\\1_DataAccess\\Files\\event_log.txt";

        public static void SaveAddingOfItemToEventLog(string model)
        {
            using (var writer = File.AppendText(_eventLogFileAddress))
            {
                writer.WriteLine($"Item Added: {model}, {DateTime.Now}");
            }
        }

        public static void SaveRemovingOfItemToEventLog(string model)
        {
            using (var writer = File.AppendText(_eventLogFileAddress))
            {
                writer.WriteLine($"Item Removed: {model}, {DateTime.Now}");
            }
        }

        public static void SaveAddingOfModelToEventLog(string model)
        {
            using (var writer = File.AppendText(_eventLogFileAddress))
            {
                writer.WriteLine($"Model Added: {model}, {DateTime.Now}");
            }
        }

        public static void SaveRemovingOfModelToEventLog(string model)
        {
            using (var writer = File.AppendText(_eventLogFileAddress))
            {
                writer.WriteLine($"Model Removed: {model}, {DateTime.Now}");
            }
        }
    }
}
