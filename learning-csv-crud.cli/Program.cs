using learning_csv_crud.lib;

internal static class Program
{
    private static void Main(string[] args)
    {
        PrintInstructions();

        var path = GetCsvPath(args);

        var (isValid, message) = IO.IsValidCsvDatabase(path);
        if (!isValid)
        {
            Console.WriteLine(message);
            Console.WriteLine("Beende Programm");
            return;
        }

        while (true)
        {
            var db = IO.ReadCsvDatabase(path);
            DisplayStatistics(db);
            PerformOperationsOnDatabase(db, path);
        }
    }

    private static void PrintInstructions()
    {
        Console.WriteLine("Super Duper Statistik Tool");
        Console.WriteLine("Rettungswachen und Krankentransportdienst der Berufsfeuerwehr");

        Console.WriteLine("Beenden mit Strg + C");
    }

    private static string GetCsvPath(string[] args)
    {
        if (args.Length != 1)
        {
            Console.WriteLine("Bitte geben Sie den Pfad zur CSV-Datei an.");
            var pathFromConsole = Console.ReadLine();
            if (string.IsNullOrEmpty(pathFromConsole))
            {
                return String.Empty;
            }
            return pathFromConsole;
        }
        else
        {
            return args[0];
        }
    }

    private static void DisplayStatistics(JahresStatistik[] db)
    {
        Console.WriteLine($"Es sind {db.Length} Datensätze vorhanden.");
        Console.WriteLine(JahresStatistik.GetHeaderForTable());
        foreach (var js in db)
        {
            Console.WriteLine(js.ToTableRow());
        }

        Console.WriteLine("Statistik nach Jahrzehnten:");
        Console.WriteLine(StatsDecade.GetHeaderForTable());
        foreach (var stats in StatsDecade.Create(db))
        {
            Console.WriteLine(stats.ToTableRow());
        }
    }

    private static void PerformOperationsOnDatabase(JahresStatistik[] db, string path)
    {
        Console.WriteLine("Bitte geben Sie die Aktion ein h_inzufügen, l_öschen, a_endern");
        var operation = Console.ReadLine();
        if (string.IsNullOrEmpty(operation) || operation.Length != 1)
        {
            Console.WriteLine($"Ungültige Eingabe: {operation}");
            return;
        }
        var ops = operation.First();

        Console.WriteLine("Bitte geben Sie ein Jahr ein:");
        var jahr = Console.ReadLine();
        if (string.IsNullOrEmpty(jahr) || jahr.Length != 4 || !int.TryParse(jahr, out _))
        {
            Console.WriteLine("Ungültige Eingabe");
            return;
        }

        switch (ops)
        {
            case 'h':
                HandleAddOperation(db, jahr, path);
                break;
            case 'l':
                HandleDeleteOperation(db, jahr, path);
                break;
            case 'a':
                HandleUpdateOperation(db, jahr, path);
                break;
            default:
                Console.WriteLine($"Ungültige Eingabe: {ops}");
                break;
        }
    }

    private static void HandleAddOperation(JahresStatistik[] db, string jahr, string path)
    {
        if (CRUD.Exists(db, jahr))
        {
            Console.WriteLine("Datensatz bereits vorhanden");
            return;
        }
        var jahresStatistik = CreateNew(jahr);
        db = CRUD.Add(db, jahresStatistik);
        IO.WriteCsvDatabase(path, db);
    }

    private static void HandleDeleteOperation(JahresStatistik[] db, string jahr, string path)
    {
        db = CRUD.Delete(db, jahr);
        IO.WriteCsvDatabase(path, db);
    }

    private static void HandleUpdateOperation(JahresStatistik[] db, string jahr, string path)
    {
        if (!CRUD.Exists(db, jahr))
        {
            Console.WriteLine("Kein Datensatz gefunden");
            return;
        }
        var js = db.First(x => x.Jahr == jahr);
        js = Change(js);
        CRUD.Update(db, js);
        IO.WriteCsvDatabase(path, db);
    }

    private static JahresStatistik Change(JahresStatistik js)
    {
        Console.WriteLine($"Datensatz gefunden: {js.ToTableRow()}");
        Console.WriteLine("Welches Spalte möchten Sie verändern?");
        Console.WriteLine("1: Anzahl der Rettungswachen");
        Console.WriteLine("2: Anzahl der Krankenkraftwagen");
        Console.WriteLine("3: Durchgeführte Transporte insgesamt");
        Console.WriteLine("4: darunter wegen Infektionskrankheiten");
        Console.WriteLine("5: darunter wegen Frühgeburten");

        var field = Console.ReadLine()?.FirstOrDefault();
        switch (field)
        {
            case '1':
                js.AnzahlRettungswachen = GetUpdatedValue("Bitte geben Sie die Anzahl Rettungswachen ein:");
                break;
            case '2':
                js.AnzahlKrankenkraftwagen = GetUpdatedValue("Bitte geben Sie die Anzahl Krankenkraftwagen ein:");
                break;
            case '3':
                js.TransporteInsgesamt = GetUpdatedValue("Bitte geben Sie die Anzahl Transporte insgesamt ein:");
                break;
            case '4':
                js.TransporteInfektionskrankheiten = GetUpdatedValue("Bitte geben Sie die Anzahl Transporte Infektionskrankheiten ein:");
                break;
            case '5':
                js.TransporteFrühgeburten = GetUpdatedValue("Bitte geben Sie die Anzahl Transporte Frühgeburten ein:");
                break;
            default:
                break;
        }
        return js;
    }

    private static int GetUpdatedValue(string instruction)
    {
        Console.WriteLine(instruction);
        return Convert.ToInt32(Console.ReadLine());
    }

    private static JahresStatistik CreateNew(string jahr)
    {
        var anzahlRettungswachen = GetUpdatedValue("Bitte geben Sie die Anzahl Rettungswachen ein:");
        var anzahlKrankenkraftwagen = GetUpdatedValue("Bitte geben Sie die Anzahl Krankenkraftwagen ein:");
        var transportInsgesamt = GetUpdatedValue("Bitte geben Sie die Anzahl Transporte insgesamt ein:");
        var transportInfektionskrankheiten = GetUpdatedValue("Bitte geben Sie die Anzahl Transporte Infektionskrankheiten ein:");
        var transportFrühgeburten = GetUpdatedValue("Bitte geben Sie die Anzahl Transporte Frühgeburten ein:");

        var jahresStatistik = new JahresStatistik
        {
            Jahr = jahr,
            AnzahlRettungswachen = anzahlRettungswachen,
            AnzahlKrankenkraftwagen = anzahlKrankenkraftwagen,
            TransporteInsgesamt = transportInsgesamt,
            TransporteInfektionskrankheiten = transportInfektionskrankheiten,
            TransporteFrühgeburten = transportFrühgeburten
        };
        return jahresStatistik;
    }
}