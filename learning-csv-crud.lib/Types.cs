namespace learning_csv_crud.lib;

public class JahresStatistik
{
    // Jahr
    public string Jahr { get; set; } = "0000";
    // Anzahl der Rettungswachen
    public int AnzahlRettungswachen { get; set; }
    // Anzahl der Krankenkraftwagen
    public int AnzahlKrankenkraftwagen { get; set; }
    // Durchgeführte Transporte insgesamt
    public int TransporteInsgesamt { get; set; }
    // darunter wegen Infektionskrankheiten
    public int TransporteInfektionskrankheiten { get; set; }
    // darunter wegen Frühgeburten
    public int TransporteFrühgeburten { get; set; }

    public static string GetHeader()
    {
        return "\"Jahr\";\"Anzahl der Rettungswachen\";\"Anzahl der Krankenkraftwagen\";\"Durchgeführte Transporte insgesamt\";\"darunter wegen Infektionskrankheiten\";\"darunter wegen Frühgeburten\"";
    }
    public static string GetHeaderForTable()
    {
        return GetHeader().Replace("\"", string.Empty).Replace(";", " | ");
    }
}

public static class JahresStatistikExtensions
{
    public static string ToTableRow(this JahresStatistik js)
    {
        return $"{js.Jahr} | {js.AnzahlRettungswachen:N0} | {js.AnzahlKrankenkraftwagen:N0} | {js.TransporteInsgesamt:N0} | {js.TransporteInfektionskrankheiten:N0} | {js.TransporteFrühgeburten:N0}";
    }
}

