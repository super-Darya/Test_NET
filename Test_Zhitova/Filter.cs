using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Test_Zhitova
{
    public static class Filter
    {
        // Инициализация логера
        private static Logger logger = LogManager.GetCurrentClassLogger();
        // Метод для фильтрации исходных данных по району (District) и промежутку времени (от _firstDeliveryTime до _firstDeliveryTime + 30 мин)
        public static List<Order> FilterOrders(List<Order> orders, string _cityDistrict, DateTime? parsedDateInput, string format)
        {
            logger.Info("Начало фильтрации заказов");
       
            List<Order> ordersListResult = [];

            foreach (var order in orders)
            {
                order.District = order.District.Trim();
                order.DateDeliveryTime = order.DateDeliveryTime.Trim();

                // Если район не совпадает, то переходим к следующему заказу
                if (_cityDistrict != order.District)
                {
                    continue;
                }

                var parsedDate = Validation.CheckValidDate(order.DateDeliveryTime, format);

                // Если дата не соответствует шаблону, то переходим к следующему заказу
                if (!parsedDate.HasValue)
                {
                    continue;
                }

                // Проверка попадания времени заказа в промежуток от _firstDeliveryTime до _firstDeliveryTime + 30 мин
                if (parsedDate >= parsedDateInput && parsedDate <= parsedDateInput.Value.AddMinutes(30))
                {
                    logger.Info($"Добавление заказа в список: {order.ID};{order.Weight};{order.District};{order.DateDeliveryTime}");
                    ordersListResult.Add(order);
                }
            }
            return ordersListResult;
        }

        // Метод для подготовки данных к записи в файл 
        public static string OutputFilterOrders(List<Order> ordersListResult)
        {
            string results;

            logger.Info("Запись результата в файл");

            if (ordersListResult.Count == 0)
            {
                results = "Заказы не найдены";
            }
            else
            {
                var options = new JsonSerializerOptions
                {
                    WriteIndented = true // Форматирование для читабельности
                };
                // Сериализуем в JSON
                results = JsonSerializer.Serialize(ordersListResult, options);
            }
            return results;
        }
    }
}
