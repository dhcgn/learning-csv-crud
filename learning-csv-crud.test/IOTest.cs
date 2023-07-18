using System.Diagnostics;
using learning_csv_crud.lib;

namespace learning_csv_crud.test;

public class IO_Test : IDisposable
{
    readonly string file;
    public IO_Test()
    {
        file = System.IO.Path.GetTempFileName();
    }

    public void Dispose()
    {
        System.IO.File.Delete(file);
    }

    [Fact]
    public void IsValidCsvDatabase_EmptyFile_Error()
    {
        #region Arrange
        #endregion
        
        #region Act

        var (success, error) = IO.IsValidCsvDatabase(file);

        #endregion
        
        #region Assert

        success.Should().BeFalse();
        error.Should().Be("File is empty");

        #endregion
    }

    [Fact]
    public void IsValidCsvDatabase_Success()
    {
        #region Arrange

        var lines = new List<string>
        {
            JahresStatistik.GetHeader(),
            "2019;1;2;3;4;5",
            "2020;1;2;3;4;5"
        };
        File.WriteAllLines(file, lines);

        #endregion
        
        #region Act

        var (success, error) = IO.IsValidCsvDatabase(file);

        #endregion
        
        #region Assert

        success.Should().BeTrue();
        error.Should().BeEmpty();

        #endregion
    }

    [Fact]
    public void ReadCsvDatabase_Test()
    {
        #region Arrange

        var lines = new List<string>
        {
            JahresStatistik.GetHeader(),
            "2019;1;2;3;4;5",
            "2020;1;2;3;4;5"
        };
        File.WriteAllLines(file, lines);

        #endregion
        
        #region Act

        var db = IO.ReadCsvDatabase(file);

        #endregion
        
        #region Assert

        db.Should().NotBeNull();
        db.Length.Should().Be(2);
        db.First().Jahr.Should().Be("2019");
        db.Last().Jahr.Should().Be("2020");

        #endregion
    }

    [Fact]
    public void WriteCsvDatabase_Test()
    {
          #region Arrange

        var lines = new List<string>
        {
            JahresStatistik.GetHeader(),
            "2019;1;2;3;4;5",
            "2020;1;2;3;4;5"
        };
        File.WriteAllLines(file, lines);
        var db = IO.ReadCsvDatabase(file);
        var js = new JahresStatistik(){
            Jahr = "1990"
        };
        db = CRUD.Add(db, js);

        #endregion
        
        #region Act

        IO.WriteCsvDatabase(file, db);

        #endregion
        
        #region Assert

        db = IO.ReadCsvDatabase(file);
        db.Should().NotBeNull();
        db.Length.Should().Be(3);
        db.First().Jahr.Should().Be("1990");
        db.Last().Jahr.Should().Be("2020");

        #endregion
    }
}
