using Dapper;
using System.Data.SqlClient;

class GoodRepository : IGoodRepository, IDisposable
{
    // Поля.
    private bool disposed = false;
    private const string CONNECTION_STRING = "Data Source=VITALII-PC;Initial Catalog=OOP_lab16;Integrated Security=True;" +
        "Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

    // Методи.

    //Метод для відображення списку розділів // 4-й запит
    public async Task DisplayAllGoods()
    {
        try
        {
            const string sqlExpression = @"SELECT G.GoodID, G.GoodName, G.Section_ID, G.Share_ID, S.SectionID, S.SectionName, Sh.ShareStartDate, Sh.ShareFinishDate " +
                "FROM Goods AS G " +
                "INNER JOIN Sections AS S ON G.Section_ID=S.SectionID " +
                "INNER JOIN Shares AS Sh ON G.Share_ID=Sh.ShareID;";

            using (SqlConnection connection = new SqlConnection(CONNECTION_STRING))
            {
                await connection.OpenAsync();

                IEnumerable<Good> goods = await connection.QueryAsync<Good>(sqlExpression);

                foreach(var good in goods)
                {
                    Console.WriteLine($"ID: {good.GoodID}\nName: {good.GoodName}");
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