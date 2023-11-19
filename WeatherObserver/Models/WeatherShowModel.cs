namespace WeatherObserver.Models
{
    public class WeatherShowModel
    {
        //Модель отчета для вывода в таблицу
        public int SelectYear { get; set; }

        public List<string> Months { get; set; } = new();
        public List<int> Years { get; set; } = new();

        public string SelectMonth { get; set; } = "-1";

        public List<WeatherModel> Reports { get; set; } = new();
    }
}
