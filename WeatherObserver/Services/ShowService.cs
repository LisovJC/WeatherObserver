using System.Diagnostics;
using WeatherObserver.Models;

namespace WeatherObserver.Services
{
    public static class ShowService
    {
        //Сервис для вывода и фильтрации данных в таблицу
        public static List<WeatherShowModel> YearsReports { get; set; } = new();

        public static List<string> GetListOfMonths()
        {
            List<string> months = new();
            var monthsEnum = Enum.GetValues(typeof(Months)).Cast<Months>();
            foreach (var month in monthsEnum)
            {
                months.Add(month.ToString().ToLower());
            }

            return months;
        }

        public static void SetReportsData(List<WeatherExcelInfo> data)
        {
            try
            {
                YearsReports = new();
                int tempYear = data[0].Year;
                List<WeatherModel> MonthsReports = new();
                foreach (var report in data)
                {
                    if (report.Year == tempYear)
                    {
                        foreach (var row in report.WeatherDatas)
                        {
                            MonthsReports.Add(row);
                        }
                    }
                    else
                    {
                        YearsReports.Add(new()
                        {
                            Reports = MonthsReports,
                            SelectYear = tempYear
                        });
                        MonthsReports = new();
                        tempYear = report.Year;
                        foreach (var row in report.WeatherDatas)
                        {
                            MonthsReports.Add(row);
                        }
                    }

                    if (report == data.LastOrDefault())
                    {
                        YearsReports.Add(new()
                        {
                            Reports = MonthsReports,
                            SelectYear = tempYear
                        });
                    }
                }
            }
            catch (Exception ex)
            {

                Debug.WriteLine(ex.Message);
            }
        }

        public static List<WeatherModel> GetReportOfYear(int year)
        {
            foreach (var yearReports in YearsReports)
            {
                if (yearReports.SelectYear == year)
                {
                    return yearReports.Reports;
                }
            }

            return new();
        }

        public static List<string> GetMonthsInReport(List<WeatherModel> report)
        {
            List<string> months = new();
            report.Sort((x, y) => DateTime.Compare(x.Date, y.Date));
            string monthTemp = report[0].Date.ToString("MMMM");

            foreach (var month in report)
            {
                if (monthTemp != month.Date.ToString("MMMM"))
                {
                    months.Add(monthTemp);
                    monthTemp = month.Date.ToString("MMMM");
                }

                if(month == report.LastOrDefault())
                {
                    months.Add(month.Date.ToString("MMMM"));
                }
            }

            return months;
        }

        public static List<WeatherModel> GetReportOfMonth(string month, int year)
        {
            List<WeatherModel> report = new();

            foreach (var y in YearsReports)
            {
                if(y.SelectYear == year)
                {
                    foreach (var m in y.Reports)
                    {
                        if(month != "-1")
                        {
                            if (m.Date.ToString("MMMM") == month.ToLower())
                            {
                                report.Add(m);
                            }
                        }
                        else
                        {
                            report.Add(m);
                        }
                    }
                }
            }
            return report;
        }

        public static List<int> GetListOfYears()
        {
            List<int> years = new();
            foreach (var year in YearsReports)
            {
                years.Add(year.SelectYear);
            }

            return years;

        }
    }
}
