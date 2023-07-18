using learning_csv_crud.lib;

namespace learning_csv_crud.test;

public class IO_Test : IClassFixture<IOFixture>
{
    readonly IOFixture fixture;
    public IO_Test(IOFixture fixture)
    {
        this.fixture = fixture;
    }

    [Fact]
    public void IsValidCsvDatabase_EmptyFile_Error()
    {
        #region Arrange
        #endregion
        
        #region Act

        var (success, error) = IO.IsValidCsvDatabase(fixture.File);

        #endregion
        
        #region Assert

        success.Should().BeFalse();
        error.Should().Be("File is empty");

        #endregion
    }

    [Fact]
    public void ReadCsvDatabase_Test()
    {
    }

    [Fact]
    public void WriteCsvDatabase_Test()
    {
    }
}

public class IOFixture : IDisposable
{
    public string File { get; set; }

    public IOFixture()
    {
        File = System.IO.Path.GetTempFileName();
    }

    public void Dispose()
    {
        System.IO.File.Delete(File);
    }
}