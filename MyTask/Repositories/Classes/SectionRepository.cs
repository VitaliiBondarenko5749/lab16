using System.Data.SqlClient;
using Dapper;

class SectionRepository : ISectionRepository, IDisposable
{
    // Поля.
    private bool disposed = false;
    private const string CONNECTION_STRING = "Data Source=VITALII-PC;Initial Catalog=OOP_lab16;Integrated Security=True;" +
        "Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

    // Методи.

    //Метод для відображення списку розділів // 3-й запит
    public async Task DisplayAllSections()
    {
        try
        {
            string sqlExpression = @"SELECT S.SectionID, S.SectionName FROM Sections AS S;";

            using (SqlConnection connection = new SqlConnection(CONNECTION_STRING))
            {
                await connection.OpenAsync();

                IEnumerable<Section> sections = await connection.QueryAsync<Section>(sqlExpression);

                foreach(Section section in sections)
                {
                    Console.WriteLine($"\nID: {section.SectionID}\nName: {section.SectionName}");
                }
                
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Something went wrong... Error: {ex.Message}");
        }
    }

    // Dispose назначений для очищення мусору
    protected virtual void Dispose(bool disposing)
    {
        if (!this.disposed)
        {
            if (disposing)
            {
                this.Dispose();
            }
        }
        this.disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}