using Dapper;
using System.Data;
using System.Data.SqlClient;

class CityRepository : ICityRepository, IDisposable
{
    // Поля.
    private bool disposed = false;
    private const string CONNECTION_STRING = "Data Source=VITALII-PC;Initial Catalog=OOP_lab16;Integrated Security=True;" +
        "Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";


    // Методи.

    // Методи призначений для виведення всіх ігор //5-й запит
    public async Task DisplayAllCitites()
    {
        try
        {
            const string sqlExpression = "SELECT C.CityID, C.CityName, C.Country_ID, CO.CountryName, CO.CountryID FROM Cities AS C " +
                "INNER JOIN Countries AS CO ON CO.CountryID=C.Country_ID;";

            using (SqlConnection connection = new SqlConnection(CONNECTION_STRING))
            {
                await connection.OpenAsync();

                IEnumerable<City> cities = await connection.QueryAsync<City,Country,City>(sqlExpression,
                    (city, country) =>
                    {
                        city.Country = country;

                        return city;
                    }, splitOn:"Country_ID");

                foreach(City city in cities)
                {
                    Console.WriteLine($"ID: {city.CityID}\nName: {city.CityName}\nCountry: {city.Country.CountryName}\n");
                }
            }
        }
        catch(Exception ex)
        {
            Console.WriteLine($"Something went wrong... Error: {ex.Message}");
        }
    }
    
    // Метод, який показує середню кількість міст // 14-й запит
    public async Task DisplayAVG_Cities()
    {
        try
        {
            const string procedureName = "[DisplayAVG_CitiesBYCountry]";

            using (SqlConnection connection = new SqlConnection(CONNECTION_STRING))
            {
                await connection.OpenAsync();

                IEnumerable<City> cites = await connection.QueryAsync<City, Country, City>(procedureName, (city, country) =>
                {
                    city.Country = country;
                    return city;
                }, splitOn:"CountryName");

                foreach(City city in cites)
                {
                    Console.WriteLine($"AVG: {city.AVG_Cities}\nCountry: {city.Country.CountryName}\n");
                }
            }
        }
        catch(Exception ex)
        {
            Console.WriteLine($"Something went wrong... Error: {ex.Message}");
        }
    }

    // Показати топ 3 міст за кількістю покупців //19-й запит
    public async Task DisplayTOP3CitiesbyCountOfBuyers()
    {
        try
        {
            const string nameProcedure = "[DisplayTOP3CitiesbyCountOfBuyers]";

            using (SqlConnection connection = new SqlConnection(CONNECTION_STRING))
            {
                await connection.OpenAsync();

                IEnumerable<City> citites = await connection.QueryAsync<City>(nameProcedure,
                    commandType: CommandType.StoredProcedure);

                foreach(City city in citites)
                {
                    Console.WriteLine($"City: {city.CityName}\nCount of buyers: {city.Count_Buyers}\n");
                }
            }
        }
        catch(Exception ex)
        {
            Console.WriteLine($"Something went wrong.. Error: {ex.Message}");
        }
    }

    // Показати найкраще місто за кількістю покупців //20-й запит
    public async Task DisplayTheBestCityBY_CountofBuyers()
    {
        try
        {
            const string procedureName = "[DisplayTheBestCityBY_CountofBuyers]";

            using (SqlConnection connection = new SqlConnection(CONNECTION_STRING))
            {
                await connection.OpenAsync();

                City city = await connection.QueryFirstAsync<City>(procedureName,
                    commandType: CommandType.StoredProcedure);

                Console.WriteLine($"City: {city.CityName}\nCount of buyers: {city.Count_Buyers}\n");
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