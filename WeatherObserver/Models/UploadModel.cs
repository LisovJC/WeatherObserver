namespace WeatherProcessor.Models
{
    public class UploadModel
    {
        //������ ������������ �����
        public string FileName { get; set; }

        public bool IsUploadFailed { get; set; } = false;
    }
}
