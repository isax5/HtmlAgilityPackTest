using System.Text.Json;

Console.WriteLine("Running... on " + DateTimeOffset.Now);

List<Earthquake> earthquakes = new List<Earthquake>();

var dateLimit = new DateTimeOffset(2000, 2, 1, 0, 0, 0, TimeSpan.Zero);

for (DateTimeOffset date = new DateTimeOffset(2000, 1, 1, 0, 0, 0, TimeSpan.Zero); date <= DateTimeOffset.Now; date = date.AddDays(1))
// for (DateTimeOffset date = new DateTimeOffset(2000, 1, 1, 0, 0, 0, TimeSpan.Zero); date <= dateLimit; date = date.AddDays(1))
{
    Console.WriteLine($"Getting earthquakes for {date}...");

    try
    {
        var result = SismicCrawler.GetEarthquakes(date);
        earthquakes.AddRange(result);
        Console.WriteLine($"{date} ready");
    }
    catch
    {
        Console.WriteLine($"Failed to get earthquakes for {date}");
    }
}

// Save earthquakes to file as json using system.text.json
File.WriteAllText("earthquakes.json", JsonSerializer.Serialize(earthquakes));
