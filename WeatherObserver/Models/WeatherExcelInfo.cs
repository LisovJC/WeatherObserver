namespace WeatherObserver.Models
{
    //Модель файла для сохранения в БД
    public class WeatherExcelInfo
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public int Year { get; set; }

        public Months Month { get; set; }

        public List<WeatherModel> WeatherDatas { get; set; } = new();

    }

    public enum Months
    {
        Январь,

        Февраль,

        Март,

        Апрель,

        Май, 
        
        Июнь,

        Июль,

        Август,

        Сентябрь,

        Октябрь,

        Ноябрь,

        Декабрь
    }
}
