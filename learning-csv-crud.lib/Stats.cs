namespace learning_csv_crud.lib;

public class StatsDecade
{
    public static StatsDecade[] Create(JahresStatistik[] db)
    {
        var groupedByDecade = db.GroupBy(x => int.Parse(x.Jahr) / 10 * 10)
                         .Select(g => new StatsDecade
                         {
                             Jahrzent = g.Key.ToString(),
                             DurchschnittRettungswachen = g.Average(x => x.AnzahlRettungswachen),
                             DurchschnittKrankenkraftwagen = g.Average(x => x.AnzahlKrankenkraftwagen),
                             SummeTransporte = g.Sum(x => x.TransporteInsgesamt),
                             SummeTransporteInfektionskrankheiten = g.Sum(x => x.TransporteInfektionskrankheiten),
                             SummeTransporteFrühgeburten = g.Sum(x => x.TransporteFrühgeburten)
                         });
        return groupedByDecade.ToArray();
    }

    public string Jahrzent { get; private set; } = "0000";
    public double DurchschnittRettungswachen { get; private set; }
    public double DurchschnittKrankenkraftwagen { get; private set; }
    public int SummeTransporte { get; private set; }
    public int SummeTransporteInfektionskrankheiten { get; private set; }
    public int SummeTransporteFrühgeburten { get; private set; }

    public static string GetHeaderForTable()
    {
        return "Jahrzehnt | Durchschnitt Rettungswachen | Durchschnitt Krankenkraftwagen | Summe Transporte | Summe Transporte Infektionskrankheiten | Summe Transporte Frühgeburten";
    }
}

public static class StatsDecadeExtensions
{
    public static string ToTableRow(this StatsDecade js)
    {
        return $"{js.Jahrzent} | {js.DurchschnittRettungswachen:N2} | {js.DurchschnittKrankenkraftwagen:N2} | {js.SummeTransporte:N0} | {js.SummeTransporteInfektionskrankheiten:N0} | {js.SummeTransporteFrühgeburten:N0}";
    }
}
