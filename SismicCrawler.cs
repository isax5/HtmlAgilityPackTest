using System.Globalization;
using HtmlAgilityPack;

public abstract class SismicCrawler
{
    public static List<Earthquake> GetEarthquakes(DateTimeOffset date)
    {
        string url = @$"https://www.sismologia.cl/sismicidad/catalogo/{date.Year}/{date.Month.ToString("00")}/{date.Year}{date.Month.ToString("00")}{date.Day.ToString("00")}.html";

        var web = new HtmlWeb();
        var doc = web.Load(url);

        if (doc is not HtmlDocument)
        {
            throw new Exception("Failed to load HTML document");
        }

        // Seleccionamos las filas de la tabla de sismos
        var rows = doc.DocumentNode.SelectNodes("//table[@class='sismologia detalle']/tr");

        var clCulture = new CultureInfo("es-CL");
        var usCulture = new CultureInfo("en-US");

        var list = new List<Earthquake>();

        if (rows != null)
        {
            // Iterar sobre cada fila, excluyendo la primera que es la cabecera
            for (int i = 1; i < rows.Count; i++)
            {
                var columns = rows[i].SelectNodes("td");

                if (columns != null)
                {
                    // Extraer los datos de las columnas
                    string localDate = columns[0].SelectSingleNode(".//a").InnerHtml.Trim();
                    string location = columns[0].SelectSingleNode(".//br/following-sibling::text()").InnerHtml.Trim();
                    string dateUTC = columns[1].InnerText.Trim();
                    string latitud = columns[2].SelectSingleNode(".//br/preceding-sibling::text()").InnerHtml.Trim(); ;
                    string longitud = columns[2].SelectSingleNode(".//br/following-sibling::text()").InnerHtml.Trim();
                    string depth = columns[3].InnerText.Trim().Split(' ')[0];
                    var magnitudNode = columns[4].InnerText.Trim().Split(' ');
                    string magnitude = magnitudNode[0];
                    string magnitudeUnit = magnitudNode[1];

                    var earthquake = new Earthquake()
                    {
                        Date = DateTime.Parse(localDate),
                        Location = location,
                        DateUTC = DateTimeOffset.Parse(dateUTC),
                        Latitude = double.Parse(latitud, clCulture),
                        Longitude = double.Parse(longitud, clCulture),
                        Depth = double.Parse(depth, usCulture),
                        Magnitude = double.Parse(magnitude, usCulture),
                        MagnitudeUnit = magnitudeUnit,
                    };

                    list.Add(earthquake);
                }
            }
        }

        return list;
    }
}
