using Microsoft.EntityFrameworkCore;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System.Diagnostics;
using WeatherObserver.Data;
using WeatherObserver.Models;
using WeatherObserver.Services.Interfaces;

namespace WeatherObserver.Services
{
    //Сервис для парсинга эксель файлов
    public class ExcelService : IExcelService
    {
        private readonly WeatherObserverDbContext _contextDB;

        public ExcelService(WeatherObserverDbContext _contextDB)
        {
            this._contextDB = _contextDB;
        }

        public bool TryUploadFileToDB(IFormFile file)
        {
            int Year = 0;
            bool isLetUpload = false;

            try
            {
                Year = int.Parse(file.FileName.Split(new[] { '_', '.' }, StringSplitOptions.RemoveEmptyEntries)[^2]);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message + $"\nФайл содержит неверное имя! Текущее имя: {file.FileName}");
                return false;
            }

            var months = Enum.GetValues(typeof(Months)).Cast<Months>();

            foreach (var month in months)
            {
                if (_contextDB.weather.Any(w => w.Year == Year && w.Month == month))
                {
                    Debug.WriteLine($"{month} {Year}-го уже есть в базе данных!");
                    continue;
                }
                else
                {
                    Debug.WriteLine($"{month} {Year} готов к загрузке! \nЗапускаю процесс преобразования и загрузки в базу данных!");
                    string nameOfSheet = month + " " + Year;
                    WeatherExcelInfo weatherSheetInfo = new() { Year = Year, Month = month, WeatherDatas = ParseExcelFile(file, nameOfSheet) };

                    if (weatherSheetInfo.WeatherDatas != null)
                    {
                        isLetUpload = true;
                        _contextDB.weather.Add(weatherSheetInfo);
                        _contextDB.SaveChanges();
                    }
                    else return false;
                }
            }

            if(isLetUpload)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<WeatherModel> ParseExcelFile(IFormFile file, string nameOfSheet)
        {
            try
            {
                XSSFWorkbook book;
                using (var ms = new MemoryStream())
                {
                    file.CopyTo(ms);
                    ms.Position = 0;
                    book = new XSSFWorkbook(ms);
                }

                ISheet sheet = book.GetSheet(nameOfSheet);
                List<WeatherModel> weatherSheet = new();
                int indexWithOutHeaders = 4;
                int indexOfList = 0;

                for (int i = indexWithOutHeaders; i < sheet.LastRowNum + 1; i++)
                {
                    IRow row = sheet.GetRow(i);
                    weatherSheet.Add(new WeatherModel());
                    int countCells = row.LastCellNum;

                    for (int j = 0; j < countCells; j++)
                    {
                        try
                        {
                            string cellValue;
                            if (row.GetCell(j).CellType == CellType.String)
                            {
                                cellValue = row.GetCell(j).StringCellValue;
                            }
                            else
                            {
                                cellValue = row.GetCell(j).NumericCellValue.ToString();
                            }
                            if (i > 3)
                            {
                                switch (j)
                                {
                                    case 0:
                                        {
                                            weatherSheet[indexOfList].Date = DateTime.Parse(cellValue);
                                        }
                                        break;
                                    case 1:
                                        {
                                            weatherSheet[indexOfList].Time = TimeSpan.Parse(cellValue);
                                            TimeSpan sp = new();
                                            DateTime dt = new();
                                            sp = TimeSpan.Parse(cellValue);
                                            dt = weatherSheet[indexOfList].Date.AddHours(sp.Hours);
                                            weatherSheet[indexOfList].Date = dt;

                                        }
                                        break;
                                    case 2:
                                        {
                                            weatherSheet[indexOfList].Temperature = double.Parse(cellValue);
                                        }
                                        break;
                                    case 3:
                                        {
                                            weatherSheet[indexOfList].RelativeHumidity = double.Parse(cellValue);
                                        }
                                        break;
                                    case 4:
                                        {
                                            weatherSheet[indexOfList].DewPoint = double.Parse(cellValue);
                                        }
                                        break;
                                    case 5:
                                        {
                                            weatherSheet[indexOfList].AtmosphericPressure = double.Parse(cellValue);
                                        }
                                        break;
                                    case 6:
                                        {
                                            weatherSheet[indexOfList].WindDirection = cellValue;
                                        }
                                        break;
                                    case 7:
                                        {
                                            weatherSheet[indexOfList].WindSpeed = double.Parse(cellValue);
                                        }
                                        break;
                                    case 8:
                                        {
                                            weatherSheet[indexOfList].CloudCover = double.Parse(cellValue);
                                        }
                                        break;
                                    case 9:
                                        {
                                            weatherSheet[indexOfList].LowerСloudLimit = double.Parse(cellValue);
                                        }
                                        break;
                                    case 10:
                                        {
                                            weatherSheet[indexOfList].HorizontalVisibility = double.Parse(cellValue);
                                        }
                                        break;
                                    case 11:
                                        {
                                            weatherSheet[indexOfList].WeatherPhenomena = cellValue;
                                        }
                                        break;

                                    default:
                                        break;
                                }
                            }
                            Debug.WriteLine(cellValue);
                        }
                        catch (Exception ex)
                        {

                            Debug.WriteLine(ex.Message + $"В {i + 1} строке, {j + 1} столбце");
                        }
                    }
                    indexOfList++;
                }
                return weatherSheet;
            }
            catch (Exception ex)
            {

                Debug.WriteLine(ex.Message);
                return null;
            }
        }
    }
}
