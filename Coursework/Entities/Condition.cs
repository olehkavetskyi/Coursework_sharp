namespace Coursework.Entities;

public class Condition
{
    public required int Id { get; set; }
    public double Temperature { get; set; }
    public double Humidity { get; set; }

    public Condition(double temperature, double humidity)
    {
        Temperature = temperature;
        Humidity = humidity;
    }

    public override string ToString()
    {
        return $"Temperature: {Temperature}°C, Humidity: {Humidity}%";
    }

    internal void UpdateTemperature(double newTemperature)
    {
        Temperature = newTemperature;
    }

    internal void UpdateHumidity(double newHumidity)
    {
        Humidity = newHumidity;
    }
}

