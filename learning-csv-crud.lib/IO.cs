namespace learning_csv_crud.lib;

public static class IO
{
    public static (bool success, string error) IsValidCsvDatabase(string path)
    {
        if (!File.Exists(path))
        {
            return (false, "File does not exist");
        }

        var lines = File.ReadAllLines(path);
        if (lines.Length == 0)
        {
            return (false, "File is empty");
        }

        var header = lines[0];
        if (header != JahresStatistik.GetHeader())
        {
            return (false, "Invalid header");
        }

        if (lines.Skip(1).Any(line => line.Split(';').Length != 6))
        {
            return (false, "Invalid cloumn count");
        }

        return (true, string.Empty);
    }

    public static JahresStatistik[] ReadCsvDatabase(string path)
    {
        var lines = File.ReadAllLines(path);
        var jahresStatistiken = lines.Skip(1).Select(line =>
        {
            var values = line.Split(';');
            if (values.Length != 6)
            {
                throw new Exception("Invalid line");
            }
            var jahresStatistik = new JahresStatistik
            {
                Jahr = values[0].Replace("\"", string.Empty),
                AnzahlRettungswachen = Convert.ToInt32(values[1]),
                AnzahlKrankenkraftwagen = Convert.ToInt32(values[2]),
                TransporteInsgesamt = Convert.ToInt32(values[3]),
                TransporteInfektionskrankheiten = Convert.ToInt32(values[4]),
                TransporteFrühgeburten = Convert.ToInt32(values[5])
            };
            return jahresStatistik;
        }).OrderBy(js => js.Jahr).ToArray();

        return jahresStatistiken;
    }

    public static void WriteCsvDatabase(string path, JahresStatistik[] jahresStatistiken)
    {
        var lines = new List<string>
        {
            JahresStatistik.GetHeader()
        };
        foreach (var jahresStatistik in jahresStatistiken)
        {
            var line = $"{jahresStatistik.Jahr};{jahresStatistik.AnzahlRettungswachen};{jahresStatistik.AnzahlKrankenkraftwagen};{jahresStatistik.TransporteInsgesamt};{jahresStatistik.TransporteInfektionskrankheiten};{jahresStatistik.TransporteFrühgeburten}";
            lines.Add(line);
        }
        File.WriteAllLines(path, lines);
    }
}
