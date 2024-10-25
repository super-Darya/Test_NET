Подключенные пакеты:

- Microsoft.Extensions.Configuration
- Microsoft.Extensions.Configuration.Json
- NLog


1. Запуск через командную строку

Запуск cmd (с правами администратора при отсутствии доступа)

Перейти в директорию ..\Test_Zhitova\Test_Zhitova

Запуск проекта с командной строки: dotnet run [район] ["дата первой доставки"] [путь к файлу с логами] [путь к файлу с результатом
выборки]

Пример: dotnet run area1 "2024-10-24 00:00:01" log.log result.txt 

При отсутствии хотя бы одного аргумента, берутся параметры из файла конфигурации appsetting.json (находится в директории ..\Test_Zhitova\Test_Zhitova)

Исходные данные берутся из файла test_data_delivery.json (находится в директории ..\Test_Zhitova\Test_Zhitova)

На выходе два файла: с логами, с отфильтрованными заказами (в директории ..\Test_Zhitova\Test_Zhitova)


2. Запуск в Visual Studio

Параметры берутся из файла конфигурации appsetting.json (находится в директории ..\Test_Zhitova\Test_Zhitova\bin\Debug\net8.0)

На выходе два файла: с логами, с отфильтрованными заказами (в директории ..\Test_Zhitova\Test_Zhitova\bin\Debug\net8.0)

Исходные данные берутся из файла test_data_delivery.json (находится в директории ..\Test_Zhitova\Test_Zhitova\bin\Debug\net8.0)


Реализованы MSTest 

Для запуска вкладка "Тест" > "Запуск всех тестов"