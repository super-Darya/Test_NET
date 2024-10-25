using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_Zhitova
{
    // Модель данных с полями, указанными в ТЗ
    public class Order
    {
        public int ID { get; set; }
        public double Weight { get; set; }
        public required string District { get; set; }
        public required string DateDeliveryTime { get; set; }

        // Переопределение методов для успешного тестирования ссылочных типов (test3, test4)
        public override bool Equals(object? obj)
        {
            if (obj is Order other)
            {
                return ID == other.ID &&
                       Weight == other.Weight &&
                       District == other.District &&
                       DateDeliveryTime == other.DateDeliveryTime;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(ID, Weight, District, DateDeliveryTime);
        }
    }
}
