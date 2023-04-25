
namespace ElectronicShopStoreApp._2_UI
{
    public interface ILogicAndCommunicationProvider
    {
        void ShowAllItems_MenuMethod();
        void ShowAllItemsByCriteria_MenuMethod();
        void AddItems_MenuMethod();
        void RemoveItems_MenuMethod();
        void ChangeModelsOfItems_MenuMethod();

        int Add(int x, int y);
    }
}
