using System.Text.Json;

Console.WriteLine("Running... on " + DateTimeOffset.Now);

List<Earthquake> earthquakes = [];

for (int year = 2000; year <= DateTimeOffset.Now.Year; year++)
{
    Parallel.For(1, 13, month =>
    {
        var date = new DateTimeOffset(year, month, 1, 0, 0, 0, TimeSpan.Zero);
        Console.WriteLine($"Getting earthquakes for {date}...");

        try
        {
            var result = SismicCrawler.GetEarthquakes(date);
            lock (earthquakes)
            {
                earthquakes.AddRange(result);
            }
            Console.WriteLine($"{date} ready");
        }
        catch
        {
            Console.WriteLine($"Failed to get earthquakes for {date}");
        }
    });

    Console.WriteLine($"Done for {year}");
}


File.WriteAllText("earthquakes.json", JsonSerializer.Serialize(earthquakes));
Console.WriteLine("Done!");
