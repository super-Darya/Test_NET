using System;
using System.Collections.Generic;
using System.IO;
using System.Globalization;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using System.Text.RegularExpressions;
using Test_Zhitova;
using NLog.Config;
using NLog.Targets;
using NLog;

namespace Test_Zhitova
{
    class Program
    {
        // Инициализация логера
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        public static void Main(string[] args)
        {
            string filePath = Directory.GetCurrentDirectory() + "\\test_data_delivery.json"; // Путь до исходных данных

            string _cityDistrict;
            string _firstDeliveryTime;
            string _logFilePath;
            string _resultFilePath;

            try
            {
                // Проверка на количество аргументов
                if (args.Length < 4)
                {
                    Console.WriteLine("Недостаточно аргументов. Необходимые: _cityDistrict _firstDeliveryDateTime, _logFilePath, _resultFilePath");
                    
                    var configuration = new ConfigurationBuilder()
                            .SetBasePath(Directory.GetCurrentDirectory())
                            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                            .Build();

                    // Получение параметров из файла конфигурации
                    _cityDistrict = configuration["DeliverySettings:CityDistrict"]!;
                    _firstDeliveryTime = configuration["DeliverySettings:FirstDeliveryDateTime"]!;
                    _logFilePath = configuration["DeliverySettings:DeliveryLog"]!;
                    _resultFilePath = configuration["DeliverySettings:DeliveryOrder"]!;
                }
                // Если через консоль переданы все 4 параметра
                else
                {
                    _cityDistrict = args[0];
                    _firstDeliveryTime = args[1];
                    _logFilePath = args[2];
                    _resultFilePath = args[3];
                }


                // Настройка логирования
                #region NLog Initializator

                var config = new NLog.Config.LoggingConfiguration();
                LogManager.Configuration = new LoggingConfiguration();
                // Шаблон
                const string LayoutFile = @"[${date:format=yyyy-MM-dd HH\:mm\:ss}] [${logger}/${uppercase: ${level}}] [THREAD: ${threadid}] >> ${message} ${exception: format=ToString}";

                var logfile = new FileTarget();

                config.AddRule(LogLevel.Trace, LogLevel.Fatal, logfile);

                logfile.CreateDirs = true;
                logfile.FileName = _logFilePath;
                logfile.AutoFlush = true;
                logfile.LineEnding = LineEndingMode.CRLF;
                logfile.Layout = LayoutFile;
                logfile.FileNameKind = FilePathKind.Absolute;
                logfile.ConcurrentWrites = false;
                logfile.KeepFileOpen = true;

                NLog.LogManager.Configuration = config;

                #endregion NLog Initializator

                // Проверка на существование файла
                if (!File.Exists(filePath))
                {
                    throw new ArgumentException("Файл по данному пути не был найден");
                }
                // Читаем содержимое файла
                string jsonData = File.ReadAllText(filePath);

                Validation.CheckValidString(jsonData);

                // Десериализация JSON в список объектов
                var orders = JsonSerializer.Deserialize<List<Order>>(jsonData);

                Validation.CheckValidString(_cityDistrict!);

                string format = "yyyy-MM-dd HH:mm:ss";

                var parsedDateInput = Validation.CheckValidDate(_firstDeliveryTime!.Trim(), format);

                if (!parsedDateInput.HasValue)
                {
                    throw new ArgumentException("Невалидный формат данных в параметре _firstDeliveryTime");
                }

                List<Order> ordersListResult = Filter.FilterOrders(orders!, _cityDistrict!, parsedDateInput, format);

                string json = Filter.OutputFilterOrders(ordersListResult);

                // Записываем в файл
                File.WriteAllText(_resultFilePath!, json);
            }
            catch (Exception e)
            {
                logger.Fatal($"Ошибка: {e.Message}");
                Console.WriteLine($"Ошибка: {e.Message}");
            }
        }
    }
}
