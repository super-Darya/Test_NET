using NLog;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 

namespace Test_Zhitova
{
    public static class Validation
    {
        // Инициализация логера
        private static Logger logger = LogManager.GetCurrentClassLogger();

        // Метод для проверки соответствия строки с датой заданному шаблону
        public static DateTime? CheckValidDate(string orderDate, string format)
        {
            bool isValidDateInput = DateTime.TryParseExact(orderDate, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime validDate);

            if (isValidDateInput)
            {
                // При успешной конвертации строки в тип DateTime, возвращается это значение;
                return validDate;
            }
            else
            {
                logger.Error($"Дата {orderDate}  не соответсвует шаблону: {validDate}");
                return null;
            }
        }
        // Метод для проверки строк (District и файл с исходными данными) на пустоту
        // При отсутствии любого из этих данных дальнейшая работа невозможна
        public static void CheckValidString(string orderString)
        {
            if (string.IsNullOrEmpty(orderString))
            {
                logger.Fatal("Исключение: Получен пустой параметр District или файл с исходными данными");
                throw new ArgumentException("Получен пустой параметр District или файл с исходными данными");
            }
        }
    }
}
