using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using WeatherObserver.Models;

namespace WeatherObserver.Services.Interfaces
{
    public interface IExcelService
    {
        public bool TryUploadFileToDB(IFormFile file);

        public List<WeatherModel> ParseExcelFile(IFormFile file, string nameOfSheet);
    }
}
