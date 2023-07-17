using System.Data.Common;
using System.Text.Json.Nodes;
using learning_csv_crud.lib;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Super Duper Statistik Tool");
        Console.WriteLine("Rettungswachen und Krankentransportdienst der Berufsfeuerwehr");

        string path;
        if (args.Length == 0)
        {
            Console.WriteLine("Bitte geben Sie den Pfad zur CSV-Datei an.");
            path = Console.ReadLine();
        }
        else
        {
            path = args[0];
        }

        var (isValid, message) = IO.IsValidCsvDatabase(path);
        if (!isValid)
        {
            Console.WriteLine(message);
            return;
        }

        while (true)
        {
            Console.WriteLine("Beenden mit Strg + C");

            var db = IO.ReadCsvDatabase(path);
            Console.WriteLine($"Es sind {db.Length} Datensätze vorhanden.");
            Console.WriteLine(JahresStatistik.GetHeaderForTable());
            foreach (var js in db)
            {
                System.Console.WriteLine(js.ToTableRow());
            }

            Console.WriteLine("Bitte geben Sie die Aktion ein h_inzufügen, l_öschen, ä_ndern");
            var ops = Console.Read();
            Console.WriteLine("Bitte geben Sie ein Jahr ein:");
            var jahr = Console.ReadLine();

            switch (ops)
            {
                case 'h':
                    Console.WriteLine("Bitte geben Sie die Anzahl Rettungswachen ein:");
                    var anzahlRettungswachen = int.Parse(Console.ReadLine());
                    Console.WriteLine("Bitte geben Sie die Anzahl Krankenkraftwagen ein:");
                    var anzahlKrankenkraftwagen = int.Parse(Console.ReadLine());
                    Console.WriteLine("Bitte geben Sie die Anzahl Transporte insgesamt ein:");
                    var transportInsgesamt = int.Parse(Console.ReadLine());
                    Console.WriteLine("Bitte geben Sie die Anzahl Transporte Infektionskrankheiten ein:");
                    var transportInfektionskrankheiten = int.Parse(Console.ReadLine());
                    Console.WriteLine("Bitte geben Sie die Anzahl Transporte Frühgeburten ein:");
                    var transportFrühgeburten = int.Parse(Console.ReadLine());
                    var jahresStatistik = new JahresStatistik
                    {
                        Jahr = jahr,
                        AnzahlRettungswachen = anzahlRettungswachen,
                        AnzahlKrankenkraftwagen = anzahlKrankenkraftwagen,
                        TransporteInsgesamt = transportInsgesamt,
                        TransporteInfektionskrankheiten = transportInfektionskrankheiten,
                        TransporteFrühgeburten = transportFrühgeburten
                    };
                    db = CRUD.Add(db, jahresStatistik);
                    IO.WriteCsvDatabase(path, db);
                    break;
                case 'l':
                    db = CRUD.Delete(db, jahr);
                    IO.WriteCsvDatabase(path, db);
                    break;
                case 'ä':
                    var js = db.FirstOrDefault(x => x.Jahr == jahr);
                    if (js == null)
                    {
                        Console.WriteLine("Kein Datensatz gefunden");
                        break;
                    }
                    Console.WriteLine("Datensatz gefunden: " + js.ToTableRow());
                    Console.WriteLine("Welches Spalte möchten Sie verändern?");
                    Console.WriteLine("1: Anzahl der Rettungswachen");
                    Console.WriteLine("2: Anzahl der Krankenkraftwagen");
                    Console.WriteLine("3: Durchgeführte Transporte insgesamt");
                    Console.WriteLine("4: darunter wegen Infektionskrankheiten");
                    Console.WriteLine("5: darunter wegen Frühgeburten");

                     var field = Console.Read();
                     switch (field)
                     {
                        case '1':
                            Console.WriteLine("Bitte geben Sie die Anzahl Rettungswachen ein:");
                            js.AnzahlRettungswachen = int.Parse(Console.ReadLine());
                            break;
                        case '2':                         
                            Console.WriteLine("Bitte geben Sie die Anzahl Krankenkraftwagen ein:");
                            js.AnzahlKrankenkraftwagen = int.Parse(Console.ReadLine());
                            break;
                        case '3':
                            Console.WriteLine("Bitte geben Sie die Anzahl Transporte insgesamt ein:");
                            js.TransporteInsgesamt = int.Parse(Console.ReadLine());
                            break;
                        case '4':
                            Console.WriteLine("Bitte geben Sie die Anzahl Transporte Infektionskrankheiten ein:");
                            js.TransporteInfektionskrankheiten = int.Parse(Console.ReadLine());
                            break;
                        case '5':
                            Console.WriteLine("Bitte geben Sie die Anzahl Transporte Frühgeburten ein:");
                            js.TransporteFrühgeburten = int.Parse(Console.ReadLine());
                            break;                            
                        default: break;
                     }
                    CRUD.Update(db, js);
                    IO.WriteCsvDatabase(path, db);
                    break; 
                default: 
                    Console.WriteLine("Ungültige Eingabe");
                    break;
            }

        }
    }
}