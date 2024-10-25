using Test_Zhitova;

namespace MSTest_Zhitova
{
    [TestClass]
    public class UnitTest1
    {
        // Проверка конвертации даты из string в DateTime (позитивный тест)
        [TestMethod("test1")]
        public void TestMethod1()
        {
            string firstDeliveryTime = "2024-10-24 00:01:01";
            DateTime expected = new(2024, 10, 24, 00, 01, 01);
            string format = "yyyy-MM-dd HH:mm:ss";
            var result = Validation.CheckValidDate(firstDeliveryTime, format);

            Assert.AreEqual(expected, result, "Возникла ошибка");
        }
        // Проверка конвертации даты из string в DateTime (негативный тест)
        [TestMethod("test2")]
        public void TestMethod2()
        {
            string firstDeliveryTime = "2024-10-24 00:01";
            DateTime? expected = null;
            string format = "yyyy-MM-dd HH:mm:ss";
            var result = Validation.CheckValidDate(firstDeliveryTime, format);

            Assert.AreEqual(expected, result, "Возникла ошибка");
        }
        // Проверка метода фильтрации при наличии подходящих данных
        [TestMethod("test3")]
        public void TestMethod3()
        {
            string cityDistrict = "area1";
            string format = "yyyy-MM-dd HH:mm:ss";

            DateTime parsedDateInput = new(2024, 10, 24, 00, 01, 01);

            List<Order> orders =
            [
                new Order { ID = 1, Weight = 5.0, District = "area3", DateDeliveryTime = "2024-10-24 00:01:01" },
                new Order { ID = 2, Weight = 10.5, District = "area2", DateDeliveryTime = "2024-10-24 00:10:01" },
                new Order { ID = 3, Weight = 3.6, District = "area1", DateDeliveryTime = "2024-10-24 00:20:01" }
            ];

            List<Order> expected = 
            [
                new Order { ID = 3, Weight = 3.6, District = "area1", DateDeliveryTime = "2024-10-24 00:20:01" }
            ];
      
            var result = Filter.FilterOrders(orders, cityDistrict, parsedDateInput, format);

            CollectionAssert.AreEqual(expected, result, "Возникла ошибка");
        }
        // Проверка метода фильтрации при отсутствии подходящих данных
        [TestMethod("test4")]
        public void TestMethod4()
        {
            string cityDistrict = "area1";
            string format = "yyyy-MM-dd HH:mm:ss";

            DateTime parsedDateInput = new(2024, 10, 24, 00, 30, 01);

            List<Order> orders =
            [
                new Order { ID = 1, Weight = 5.0, District = "area3", DateDeliveryTime = "2024-10-24 00:01:01" },
                new Order { ID = 2, Weight = 10.5, District = "area2", DateDeliveryTime = "2024-10-24 00:10:01" },
                new Order { ID = 3, Weight = 3.6, District = "area1", DateDeliveryTime = "2024-10-24 00:20:01" }
            ];

            List<Order> expected = [];

            var result = Filter.FilterOrders(orders, cityDistrict, parsedDateInput, format);

            CollectionAssert.AreEqual(expected, result, "Возникла ошибка");
        }
    }
}