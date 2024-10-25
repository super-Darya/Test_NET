Подключенные пакеты:

- Microsoft.Extensions.Configuration Version="8.0.0"
- Microsoft.Extensions.Configuration.Json Version="8.0.1"
- NLog Version="5.3.4"

1. Запуск через командную строку

Запуск cmd (с правами администратора при отсутствии доступа)

Перейти в директорию ..\Test_NET-master\Test_Zhitova

Запуск проекта с командной строки: dotnet run [район] ["дата первой доставки"] [путь к файлу с логами] [путь к файлу с результатом
выборки]

Пример: dotnet run area1 "2024-10-24 00:00:01" log.log result.txt 

При отсутствии хотя бы одного аргумента, берутся параметры из файла конфигурации appsetting.json (находится в директории ..\Test_NET-master\Test_Zhitova)

Исходные данные берутся из файла test_data_delivery.json (находится в директории ..\Test_NET-master\Test_Zhitova)

На выходе два файла: с логами, с отфильтрованными заказами (в директории ..\Test_NET-master\Test_Zhitova)


2. Запуск в Visual Studio

Параметры берутся из файла конфигурации appsetting.json (требуется добавить в директорию ..\Test_Zhitova\Test_Zhitova\bin\Debug\net8.0)

На выходе два файла: с логами, с отфильтрованными заказами (в директории ..\Test_NET-master\Test_Zhitova\bin\Debug\net8.0)

Исходные данные берутся из файла test_data_delivery.json (требуется добавить в директорию ..\Test_NET-master\Test_Zhitova\bin\Debug\net8.0)


Реализованы MSTest 

Подключенные пакеты:

- coverlet.collector Version="6.0.0"
- Microsoft.NET.Test.Sdk Version="17.8.0"
- MSTest.TestAdapter Version="3.1.1"
- MSTest.TestFramework Version="3.1.1" 

Для запуска вкладка "Тест" > "Запуск всех тестов"