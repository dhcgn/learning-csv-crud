namespace learning_csv_crud.lib;

public static class CRUD
{
    public static JahresStatistik[] Add(JahresStatistik[] jahresStatistiken, JahresStatistik jahresStatistik)
    {
        var jahresStatistikenList = jahresStatistiken.ToList();
        jahresStatistikenList.Add(jahresStatistik);
        return jahresStatistikenList.ToArray();
    }

    public static JahresStatistik[] Update(JahresStatistik[] jahresStatistiken, JahresStatistik jahresStatistik)
    {
        var jahresStatistikenList = jahresStatistiken.ToList();
        var index = jahresStatistikenList.FindIndex(x => x.Jahr == jahresStatistik.Jahr);
        jahresStatistikenList[index] = jahresStatistik;
        return jahresStatistikenList.ToArray();
    }

    public static JahresStatistik[] Delete(JahresStatistik[] jahresStatistiken, string jahr)
    {
        var jahresStatistikenList = jahresStatistiken.ToList();
        var index = jahresStatistikenList.FindIndex(x => x.Jahr == jahr);
        jahresStatistikenList.RemoveAt(index);
        return jahresStatistikenList.ToArray();
    } 
}