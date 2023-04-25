using ElectronicShopStoreApp._1_DataAccess;
using ElectronicShopStoreApp._2_UI;
using ElectronicShopStoreApp._3_Other.FileAddressesExtension;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace ElectronicShopStoreApp.Tests
{
    public class BasicTests
    {
        private readonly ServiceProvider _serviceProvider;

        public BasicTests() 
        {
            var services = new ServiceCollection();

            services.AddSingleton<IApp, App>();
            services.AddSingleton<ILogicAndCommunicationProvider, LogicAndCommunicationProvider>();

            var fileWithSqlDatabaseAddress = FileAddressesManager.fileAddress_database_address;
            string mainDatabaseAddress = GetSqlDatabaseConnectionString(fileWithSqlDatabaseAddress);
            services.AddDbContext<ElectronicShopStoreDbContext>(options => options.UseSqlServer(mainDatabaseAddress));

            var serviceProvider = services.BuildServiceProvider();
            _serviceProvider = serviceProvider;
        }

        private string GetSqlDatabaseConnectionString(string fileWithSqlDatabaseAddress)
        {
            string address;

            using (var reader = new StreamReader(fileWithSqlDatabaseAddress))
            {
                address = reader.ReadLine();
            }

            return address;
        }

        [Fact]
        public void test()
        {
            Assert.Equal(1, 1);
        }

        [Fact]
        public void Add_test()
        {
            var logAndComProvider = _serviceProvider.GetService<ILogicAndCommunicationProvider>();

            Assert.Equal(3, logAndComProvider.Add(2, 1));
        }

        [Fact]
        public void PrivateAdd_test()
        {
            //Arrange
            var logAndComProvider = _serviceProvider.GetService<ILogicAndCommunicationProvider>();

            MethodInfo methodPrivateAdd = logAndComProvider
                .GetType()
                .GetMethod("PrivateAdd", 
                            BindingFlags.NonPublic | BindingFlags.Instance);

            object[] twoNumAsParams = new object[2] { 1, 2 };

            //Act
            var addingResult = methodPrivateAdd.Invoke(logAndComProvider, twoNumAsParams);

            //Assert
            Assert.Equal(3, addingResult);
        }
    }
}
