using learning_csv_crud.lib;

namespace learning_csv_crud.test;

public class StatsDecadeTest
{
    [Fact]
    public void Create_Success()
    {
        #region Arrange

        var db = new JahresStatistik[]
        {
            new() {Jahr = "2010", AnzahlKrankenkraftwagen = 1, AnzahlRettungswachen = 2, TransporteFrühgeburten = 3, TransporteInfektionskrankheiten = 4, TransporteInsgesamt = 5},
            new() {Jahr = "2019", AnzahlKrankenkraftwagen = 2, AnzahlRettungswachen = 3, TransporteFrühgeburten = 4, TransporteInfektionskrankheiten = 5, TransporteInsgesamt = 6},
            new() {Jahr = "2020", AnzahlKrankenkraftwagen = 3, AnzahlRettungswachen = 4, TransporteFrühgeburten = 5, TransporteInfektionskrankheiten = 6, TransporteInsgesamt = 7},
            new() {Jahr = "2021", AnzahlKrankenkraftwagen = 4, AnzahlRettungswachen = 5, TransporteFrühgeburten = 6, TransporteInfektionskrankheiten = 7, TransporteInsgesamt = 8},
        };

        #endregion
        
        #region Act

        var stats = StatsDecade.Create(db);

        #endregion
        
        #region Assert

        stats.Should().HaveCount(2);
        stats[0].Jahrzent.Should().Be("2010");
        stats[1].Jahrzent.Should().Be("2020");

        stats[0].DurchschnittKrankenkraftwagen.Should().Be(1.5);
        stats[0].DurchschnittRettungswachen.Should().Be(2.5);
        stats[0].SummeTransporteFrühgeburten.Should().Be(7);
        stats[0].SummeTransporteInfektionskrankheiten.Should().Be(9);
        stats[0].SummeTransporte.Should().Be(11);

        stats[1].DurchschnittKrankenkraftwagen.Should().Be(3.5);
        stats[1].DurchschnittRettungswachen.Should().Be(4.5);
        stats[1].SummeTransporteFrühgeburten.Should().Be(11);
        stats[1].SummeTransporteInfektionskrankheiten.Should().Be(13);
        stats[1].SummeTransporte.Should().Be(15);

        #endregion
    }
}