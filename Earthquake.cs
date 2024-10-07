public class Earthquake
{
    public DateTime Date { get; set; }
    public required string Location { get; set; } = string.Empty;
    public DateTimeOffset DateUTC { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public double Depth { get; set; }
    public double Magnitude { get; set; }
    public required string MagnitudeUnit { get; set; } = string.Empty;

    public override string ToString()
     => $"Fecha Local: {Date}\nLocation: {Location}\nFecha UTC: {DateUTC}\nLatitud: {Latitude}\nLongitud: {Longitude}\nProfundidad: {Depth}\nMagnitud: {Magnitude}\nMagnitud Unit: {MagnitudeUnit}";
}
