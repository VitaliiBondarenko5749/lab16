using Dapper;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;

class ShareRepository : IShareRepository, IDisposable
{
    // Поля.
    private bool disposed = false;
    private const string CONNECTION_STRING = "Data Source=VITALII-PC;Initial Catalog=OOP_lab16;Integrated Security=True;" +
        "Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

    // Методи.

    // Відображення всіх акцій для конкретної країни // 9-й запит
    public async Task DisplayAllSharesBY_Country()
    {
        try
        {
            Console.Write("\nEnter any country from this list: ");
            string countryName = Console.ReadLine();
            Console.Clear();
            const string sqlExpression = "SELECT Sh.ShareID, Sh.ShareStartDate, Sh.ShareFinishDate, C_Sh.Share_ID, C_Sh.Country_ID, Co.CountryID, Co.CountryName " +
                "FROM Shares AS Sh " +
                "INNER JOIN Countries_Shares AS C_Sh ON Sh.ShareID=C_Sh.Share_ID " +
                "INNER JOIN Countries AS Co ON C_Sh.Country_ID=Co.CountryID;";

            using (SqlConnection connection = new SqlConnection(CONNECTION_STRING))
            {
                await connection.OpenAsync();

                IEnumerable<Share> shares = await connection.QueryAsync<Share, Country, Share>(sqlExpression,
                    (share, country) =>
                    {
                        share.Countries.Add(country);

                        return share;
                    }, splitOn:"ShareID,CountryID");

                var result = shares.GroupBy(p => p.ShareID).Select(g =>
                {
                    var groupedShare = g.First();
                    groupedShare.Countries = g.Select(p => p.Countries.Single()).ToList();
                    return groupedShare;
                });

                foreach(var r in result)
                {
                    for (int i = 0; i < r.Countries.Count; ++i)
                    {
                        if (r.Countries[i].CountryName == countryName)
                        {
                            Console.WriteLine($"ID: {r.ShareID}\nBegin: {r.ShareStartDate}\nEnd: {r.ShareFinishDate}");
                            Console.Write("Country: ");

                            Console.Write(r.Countries[i].CountryName + " \n\n");
                       
                            break;
                        }
                    }
                }
            }
        }
        catch(Exception ex)
        {
            Console.WriteLine($"Something went wrong... Error: {ex.Message}");
        }
    }

    // Відобразити всі акції товару конкретного розділу за вказаний проміжок часу //15-й запит
    public async Task DisplayALLShares_andGood_byEnteredTime()
    {
        try
        {
            const string ProcedureName = "[DisplayShareoftheGoodbyenteredTime]";

            //Синтаксис: '2022-01-01'
            Console.Write("\nStartDate: ");
            string[] startDate = Console.ReadLine().Split('-');
            Console.Write("\nEndDate: ");
            string[] endDate = Console.ReadLine().Split('-');

            using (SqlConnection connection = new SqlConnection(CONNECTION_STRING))
            {
                await connection.OpenAsync();

                IEnumerable<Share> shares = await connection.QueryAsync<Share, Good, Share>(ProcedureName, (share, good) =>
                {
                    share.Good = good;
                    return share;
                }, splitOn:"GoodName");

                IEnumerable<Share> result = shares.Where(x => x.ShareStartDate.Year>=(int.Parse(startDate[0])) &&
                x.ShareStartDate.Month>=(int.Parse(startDate[1])) && x.ShareStartDate.Day>=(int.Parse(startDate[2])) 
                && x.ShareFinishDate.Year >= (int.Parse(endDate[0])) && x.ShareFinishDate.Month >= (int.Parse(endDate[1]))
                && x.ShareFinishDate.Day>=(int.Parse(endDate[2])));

                foreach(Share share in result)
                {
                    Console.WriteLine($"\nStartDate: {share.ShareStartDate}\nEndDate: {share.ShareFinishDate}" +
                        $"\nGoodName: {share.Good.GoodName}\n");
                }
            }
        }
        catch(Exception ex)
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