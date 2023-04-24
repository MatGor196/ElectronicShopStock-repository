using ElectronicShopStoreApp._3_Other.FileAddressesExtension;

namespace ElectronicShopStoreApp._3_Other.LogExtension
{
    public static class EventsClass
    {
        private static readonly string _eventLogFileAddress = FileAddressesManager.fileAddress_event_log;

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
