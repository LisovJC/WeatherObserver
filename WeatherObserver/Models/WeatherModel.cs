using Microsoft.EntityFrameworkCore;
using NPOI.HPSF;

namespace WeatherObserver.Models
{
    //Модель отчета
    public class WeatherModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }

        public double Temperature { get; set; }

        public double RelativeHumidity { get; set; }

        public double DewPoint { get; set; }

        public double AtmosphericPressure { get; set; }

        public string WindDirection { get; set; } = "";

        public double WindSpeed { get; set; }

        public double CloudCover { get; set; }

        public double LowerСloudLimit { get; set; }

        public double HorizontalVisibility { get; set; }

        public string WeatherPhenomena { get; set; } = "";

    }

    public enum TableHeaders
    {
        Date,
        
        Time,
        
        Temperature,
        
        RelativeHumidity, 
        
        DewPoint,
        
        AtmosphericPressure,
        
        WindDirection,
        
        WindSpeed,
        
        CloudCover, 
        
        LowerСloudLimit, 
        
        HorizontalVisibility, 
        
        WeatherPhenomena
}
}
