using learning_csv_crud.lib;

namespace learning_csv_crud.test;

public class Crud_Test
{
    [Fact]
    public void Add_Test()
    {
        #region Arrange

        var db = Array.Empty<JahresStatistik>();

        #endregion

        #region Act

        db = CRUD.Add(db, new JahresStatistik { Jahr = "2020" });

        #endregion

        #region Assert

        db.Should().HaveCount(1);
        db[0].Jahr.Should().Be("2020");

        #endregion
    }

    [Fact]
    public void Delete_Test()
    {
        #region Arrange

        var db = new[]
        {
            new JahresStatistik { Jahr = "2020" },
            new JahresStatistik { Jahr = "2021" }
        };

        #endregion

        #region Act

        db = CRUD.Delete(db, "2021");

        #endregion

        #region Assert

        db.Should().HaveCount(1);
        db[0].Jahr.Should().Be("2020");

        #endregion
    }

    
    [Fact]
    public void Update_Test()
    {
        #region Arrange

        var db = new[]
        {
            new JahresStatistik { Jahr = "2020" , AnzahlKrankenkraftwagen = 0 },
        };

        #endregion

        #region Act

        db = CRUD.Update(db, new JahresStatistik { Jahr = "2020", AnzahlKrankenkraftwagen = 1 });

        #endregion

        #region Assert

        db.Should().HaveCount(1);
        db[0].Jahr.Should().Be("2020");
        db[0].AnzahlKrankenkraftwagen.Should().Be(1);

        #endregion
    }

}