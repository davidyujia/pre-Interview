using EmployeeTaskManagement.Helpers;
using EmployeeTaskManagement.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EmployeeTaskManagement.Tests
{
    [TestClass]
    public class UnitTest
    {
        private static IConfiguration GetConfig()
        {
            return new ConfigurationBuilder().AddJsonFile("appSettings.json", optional: true).Build();
        }


        [TestMethod]
        public void GenerateToken()
        {
            var config = GetConfig();

            var jwt = new JwtHelpers(config);

            var token = jwt.GenerateToken("Test");

            Assert.IsNotNull(token);
        }

        private static EmployeeService CreateService()
        {
            return new EmployeeService(new LiteDB.LiteDatabase("test.db"));
        }

        [TestMethod]
        public void ServiceTest()
        {
            var service = CreateService();

            var id = service.Create(new Models.EmployeeViewModel());

            var data = service.Get(id);

            Assert.IsNotNull(data, "Create employee fail.");

            service.Update(new Models.EmployeeViewModel { Id = id, FirstName = "Test" });

            data = service.Get(id);

            Assert.IsTrue(data.FirstName == "Test", "Update employee fail.");

            var list = service.List();

            Assert.IsTrue(list.Length > 0, "List employee fail.");

            service.Delete(id);

            data = service.Get(id);

            Assert.IsNull(data, "Delete employee fail.");
        }
    }
}
