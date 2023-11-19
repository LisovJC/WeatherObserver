namespace WeatherProcessor.Models
{
    public class UploadModel
    {
        //Модель загружаемого файла
        public string FileName { get; set; }

        public bool IsUploadFailed { get; set; } = false;
    }
}
