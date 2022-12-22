using Dapper;
using System.Data.SqlClient;

class CountryRepository : ICountryRepository, IDisposable 
{
    // Поля.
    private bool disposed = false;
    private const string CONNECTION_STRING = "Data Source=VITALII-PC;Initial Catalog=OOP_lab16;Integrated Security=True;" +
        "Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

    // Методи.
    // Dispose назначений для очищення мусору

    // Відображення всіх країн // 6-й запит
    public async Task DisplayAllCountries()
    {
        try
        {
            const string sqlExpression = "SELECT * FROM Countries;";

            using (SqlConnection connection = new SqlConnection(CONNECTION_STRING))
            {
                await connection.OpenAsync();

                IEnumerable<Country> countries = await connection.QueryAsync<Country>(sqlExpression);

                foreach(Country country in countries)
                {
                    Console.WriteLine($"ID: {country.CountryID}\nName: {country.CountryName}\n");
                }
            }
        }
        catch(Exception ex)
        {
            Console.WriteLine($"Something went wrong... Error {ex.Message}");
        }
    }

    // Відобразити кількість міст у кожній країні // 12-й запит
    public async Task DisplayCountCitiesGROUPED_BY_Countries()
    {
        try
        {
            const string procedureName = "[CountCitiesBY_Country]";

            using (SqlConnection connection = new SqlConnection(CONNECTION_STRING))
            {
                await connection.OpenAsync();

                IEnumerable<City> cities = await connection.QueryAsync<City, Country, City>(procedureName, (city, country) =>
                {
                    city.Country = country;

                    return city;
                }, splitOn: "CountryName");

                foreach(City city in cities)
                {
                    Console.WriteLine($"Count: {city.Count_Cities}\nCountry: {city.Country.CountryName}\n");
                }
            }
        }
        catch(Exception ex)
        {
            Console.WriteLine($"Something went wrong... Error: {ex.Message}");
        }
    }

    // Відобразити топ 3 країн за кількістю покупців // 17-й запит
    public async Task DisplayTOP3CountriesBY_CountOF_Buyers()
    {
        try
        {
            const string procedureName = "[DisplayTOP3_CountriesBYCountOF_Buyers]";

            using (SqlConnection connection = new SqlConnection(CONNECTION_STRING))
            {
                await connection.OpenAsync();

                IEnumerable<Country> countries = await connection.QueryAsync<Country>(procedureName,
                    commandType: System.Data.CommandType.StoredProcedure);

                foreach(Country country in countries)
                {
                    Console.WriteLine($"Country Name: {country.CountryName}\nCount of buyers: {country.Count_Buyers}\n");
                }                
            }
        }
        catch(Exception ex)
        {
            Console.WriteLine($"Something went wrong... Error: {ex.Message}");
        }
    }

    // Відобразити найкращу країну за кількістю покупців // 18-й запит
    public async Task DisplayTheBestCountryByCountOfBuyers()
    {
        try
        {
            const string nameProcedure = "[DisplayTheBestCountryByCountOfBuyers]";

            using (SqlConnection connection = new SqlConnection(CONNECTION_STRING))
            {
                await connection.OpenAsync();

                Country country = await connection.QueryFirstAsync<Country>(nameProcedure,
                    commandType: System.Data.CommandType.StoredProcedure);

                Console.WriteLine($"Country: {country.CountryName}\nCount of buyers: {country.Count_Buyers}");
            }
        }
        catch(Exception ex)
        {
            Console.WriteLine($"Something went wrong... Error: {ex.Message}");
        }
    }
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
