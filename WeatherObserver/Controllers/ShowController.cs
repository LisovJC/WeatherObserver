using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using WeatherObserver.Data;
using WeatherObserver.Models;
using WeatherObserver.Services;

namespace WeatherObserver.Controllers
{
    public class ShowController : Controller
    {
        
        private readonly WeatherObserverDbContext _contextDB;

        public ShowController(WeatherObserverDbContext _contextDB)
        {
            this._contextDB = _contextDB;
        }

        public IActionResult Index(int year = -1, string month = "-1")
        {
            List<WeatherModel> report = new();
            List<string> months = new();
            List<int> years = new();
            WeatherShowModel weatherReport = new();
            try
            {
                //Проверка на отсутствие фильтров
                if ((year == -1 && month == "-1") || (year == -1 && month != "-1"))
                {
                    //Получаем даные из БД
                    var data = _contextDB.weather
                    .Include(w => w.WeatherDatas)
                    .ToList();

                    //Сортируем в порядке возрастания даты
                    data.Sort((x, y) => x.Year.CompareTo(y.Year));
                    //Устанавливаем в статическом сервисе полученные данные, для того, что бы обращаться к ним
                    ShowService.SetReportsData(data);
                    //Получаем список всех лет, которые используются в данных
                    years = ShowService.GetListOfYears();
                    //Заполняем лист данными, для вывода в таблицу
                    foreach (var yearReport in ShowService.YearsReports)
                    {
                        foreach (var reportOfWeather in yearReport.Reports)
                        {
                            report.Add(reportOfWeather);
                        }
                    }
                    report.Sort((x, y) => x.Date.CompareTo(y.Date));
                    //Получаем список месяцов и выводим данные в таблицу
                    months = ShowService.GetListOfMonths();
                    weatherReport = new() { Reports = report, SelectYear = -1, Months = months, Years = years, SelectMonth = "-1" };
                }
                else
                {
                    //Если есть фильтры, то отфильтровываем и выводим в таблицу результат
                    years = ShowService.GetListOfYears();
                    report = ShowService.GetReportOfYear(year);
                    months = ShowService.GetMonthsInReport(report);
                    report = ShowService.GetReportOfMonth(month, year);
                    weatherReport = new() { Reports = report, SelectYear = year, Months = months, Years = years, SelectMonth = month };
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return View(weatherReport);
        }
    }
}
