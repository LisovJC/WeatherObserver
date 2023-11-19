using Microsoft.AspNetCore.Mvc;
using WeatherObserver.Data;
using WeatherObserver.Services.Interfaces;
using WeatherProcessor.Models;

namespace WeatherProcessor.Controllers
{
    public class UploadController : Controller
    {
        private readonly WeatherObserverDbContext _contextDB;
        private readonly IExcelService ExcelService;

        public UploadController(WeatherObserverDbContext _contextDB, IExcelService excelService)
        {
            this._contextDB = _contextDB;
            ExcelService = excelService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(IFormFileCollection files)
        {
            bool isCanUpload = false;
            //Пытаемся загрузить выбранные файлы
            foreach (var file in files)
            {
                isCanUpload = ExcelService.TryUploadFileToDB(file);
            }

            //Если загрузка удачна, то выводим сообщение об успехе, иначе о провале
            if (!isCanUpload)
            {
                return View(new UploadModel
                {
                    FileName = string.Join(", ", files.Select(_ => _.FileName)),
                    IsUploadFailed = true
                });
            }
            else
            {
                if (files.Count > 1)
                {
                    return View(new UploadModel
                    {
                        FileName = string.Join(", ", files.Select(_ => _.FileName))
                    });
                }
                else
                {
                    return View(new UploadModel
                    {
                        FileName = string.Join(", ", files.Select(_ => _.FileName))
                    });
                }
            }
        }
    }
}
