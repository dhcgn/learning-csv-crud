using System.Text.Json.Nodes;

namespace learning_csv_crud.lib;

public static class CRUD
{
    public static JahresStatistik[] Add(JahresStatistik[] jahresStatistiken, JahresStatistik jahresStatistik)
    {
        var jahresStatistikenList = jahresStatistiken.ToList();
        jahresStatistikenList.Add(jahresStatistik);
        return jahresStatistikenList.OrderBy(js => js.Jahr).ToArray();
    }

    public static JahresStatistik[] Update(JahresStatistik[] jahresStatistiken, JahresStatistik jahresStatistik)
    {
        var jahresStatistikenList = jahresStatistiken.ToList();
        var index = jahresStatistikenList.FindIndex(js => js.Jahr == jahresStatistik.Jahr);
        jahresStatistikenList[index] = jahresStatistik;
        return jahresStatistikenList.ToArray();
    }

    public static JahresStatistik[] Delete(JahresStatistik[] jahresStatistiken, string jahr)
    {
        var jahresStatistikenList = jahresStatistiken.ToList();
        var index = jahresStatistikenList.FindIndex(js => js.Jahr == jahr);
        jahresStatistikenList.RemoveAt(index);
        return jahresStatistikenList.ToArray();
    } 
}