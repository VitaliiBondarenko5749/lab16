using Dapper;
using System.Data.SqlClient;

class BuyerRepository : IBuyerRepository, IDisposable
{
    // Поля.
    private bool disposed = false;
    private const string CONNECTION_STRING = "Data Source=VITALII-PC;Initial Catalog=OOP_lab16;Integrated Security=True;" +
        "Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

    // Методи.

    // Метод який виводить всю інформацію про покупців // 1-й запит
    public async Task DisplayAllInfoAboutBuyers()
    {
        try
        {
            string sqlExpression = @"SELECT B.BuyerID, B.FullName, B.Sex, B.Email, B.City_ID, C.CityName " +
                "FROM Buyers AS B " +
                "INNER JOIN Cities AS C ON B.City_ID = C.CityID;";

            using (SqlConnection connection = new SqlConnection(CONNECTION_STRING))
            {
                await connection.OpenAsync();

                IEnumerable<Buyer> buyers = await connection.QueryAsync<Buyer, City, Buyer>(sqlExpression, 
                    (buyer, city) =>
                    {
                        buyer.City = city;
                       
                        return buyer;
                    }, splitOn:"City_ID");

                foreach (Buyer buyer in buyers)
                {
                    Console.WriteLine($"\nID: {buyer.BuyerID}\nName: {buyer.FullName}\nSex: {buyer.Sex}\nEmail: {buyer.Email}\n" +
                        $"City: {buyer.City.CityName}");
                }
            }
        }
        catch(Exception ex)
        {
            Console.WriteLine($"Something went wrong... Error: {ex.Message}");
        }
    }

    // Метод, який відображує всіх покупців // 2-й запит
    public async Task DisplayAllEmails()
    {
        try
        {
            string sqlExpression = @"SELECT B.Email FROM Buyers AS B;";

            using (SqlConnection connection = new SqlConnection(CONNECTION_STRING))
            {
                await connection.OpenAsync();

                IEnumerable<Buyer> buyers = await connection.QueryAsync<Buyer>(sqlExpression);

                foreach(var buyer in buyers)
                {
                    Console.WriteLine(buyer.Email);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Something went wrong... Error: {ex.Message}");
        }
    }

    // Відображення всіх покупців з конкретного міста // 7-й запит
    public async Task DisplayAllBuyers_BY_City()
    {
        try
        {
            Console.Write("\nEnter any city from this list: ");
            string cityName = Console.ReadLine();
            Console.Clear();
            const string sqlExpression = "SELECT B.FullName, B.Sex, B.Email, C.CityName " +
                                         "FROM Buyers AS B " +
                                         "INNER JOIN Cities AS C ON B.City_ID=C.CityID;";

            using(SqlConnection connection = new SqlConnection(CONNECTION_STRING))
            {
                await connection.OpenAsync();

                IEnumerable<Buyer> buyers = await connection.QueryAsync<Buyer, City, Buyer>(sqlExpression,
                    (buyer, city) =>
                    {
                        buyer.City = city;

                        return buyer;
                    }, splitOn:"CityName");

                foreach(Buyer buyer in buyers)
                {
                    if(buyer.City.CityName.Equals(cityName))
                    Console.WriteLine($"Name: {buyer.FullName}\nSex: {buyer.Sex}\nEmail: {buyer.Email}\n" +
                        $"City: {buyer.City.CityName}\n");
                }
            }

        }
        catch(Exception ex)
        {
            Console.WriteLine($"Something went wrong... Error: {ex.Message}");
        }
    }

    // Відображення всіх покупців з конкретної країни // 8-й запит
    public async Task DisplayALLBuyers_BY_Country()
    {
        try
        {
            Console.Write("\nEnter any country from this list: ");
            string countryName = Console.ReadLine();
            Console.Clear();
            const string sqlExpression = "SELECT B.BuyerID, B.FullName, B.Sex, B.Email, B.City_ID, C.CityID, Co.CountryID, Co.CountryName " +
                "FROM Buyers AS B " +
                "INNER JOIN Cities AS C ON B.City_ID=C.CityID " +
                "INNER JOIN Countries AS Co ON C.Country_ID=Co.CountryID;";

            using (SqlConnection connection = new SqlConnection(CONNECTION_STRING))
            {
                await connection.OpenAsync();

                IEnumerable<Buyer> buyers = await connection.QueryAsync<Buyer, Country, Buyer>(sqlExpression,
                    (buyer, country) =>
                    {
                        buyer.Country = country;

                        return buyer;
                    }, splitOn:"City_ID");

                foreach(Buyer buyer in buyers)
                {
                    if (buyer.Country.CountryName == countryName)
                        Console.WriteLine($"ID: {buyer.BuyerID}\nName: {buyer.FullName}\nSex: {buyer.Sex}\nEmail: {buyer.Email}\n" +
                            $"Country: {buyer.Country.CountryName}\n");
                }
            }
        }
        catch(Exception ex)
        {
            Console.WriteLine($"Something went wrong... Error: {ex.Message}");
        }
    }

    // Відображення кількості покупців у кожному місті // 10-й запит
    public async Task DisplayCountBuyers_BY_City()
    {
        try
        {
            const string procedureName = "[CountBuyersBY_City]";

            using (SqlConnection connection = new SqlConnection(CONNECTION_STRING))
            {
                await connection.OpenAsync();

                IEnumerable<Buyer> buyers = await connection.QueryAsync<Buyer, City, Buyer>(procedureName, (buyer, city) =>
                {
                    buyer.City = city;
                    return buyer;
                }, splitOn: "CityName", commandType: System.Data.CommandType.StoredProcedure);

                foreach(Buyer buyer in buyers)
                {
                    Console.WriteLine($"Count: {buyer.Count_Buyers}\nCity: {buyer.City.CityName}");
                }
            }
        }
        catch(Exception ex)
        {
            Console.WriteLine($"Something went wrong... Error: {ex.Message}");
        }
    }

    // Відображення кількості покупців у кожній країні // 11-й запит
    public async Task DisplayCountBuyers_BY_Country() 
    {
        try
        {
            const string procedureName = "[CountBuyersBY_Country]";

            using (SqlConnection connection  = new SqlConnection(CONNECTION_STRING))
            {
                await connection.OpenAsync();

                IEnumerable<Buyer> buyers = await connection.QueryAsync<Buyer, Country, Buyer>(procedureName, (buyer, country) =>
                {
                    buyer.Country = country;

                    return buyer;
                }, splitOn:"CountryName", commandType: System.Data.CommandType.StoredProcedure);

                foreach (Buyer buyer in buyers)
                {
                    Console.WriteLine($"Count: {buyer.Count_Buyers}\nCountry: {buyer.Country.CountryName}");
                }
            }
        }
        catch(Exception ex)
        {
            Console.WriteLine($"Something went wrong... Error: {ex.Message}");
        }
    }

    // Відобразити всі розділи, у яких зацікавлені конкретні покупці, конкретної держави // 13-й запит
    public async Task DisplayAllSectionBY_Buyers() 
    {
        try
        {
            await DisplayAllInfoAboutBuyers();
            Console.Write("\n\nEnter any BuyerID from this list: ");
            int buyerID = int.Parse(Console.ReadLine());
            Console.Clear();
            await new CountryRepository().DisplayAllCountries();
            Console.Write("\n\nEnter any Country name from this list: ");
            string countryName = Console.ReadLine();
            Console.Clear();
            const string procedureName = "[DisplaySectionsBY_Buyers]";

            using (SqlConnection connection = new SqlConnection(CONNECTION_STRING))
            {
                await connection.OpenAsync();

                IEnumerable<Buyer> buyers = await connection.QueryAsync<Buyer, Section, Country, Buyer>(procedureName, (buyer, section, country) =>
                {
                    
                        buyer.Country = country;
                        buyer.Sections.Add(section);
                    
                    return buyer;
                }, splitOn:"SectionName,CountryName");

                foreach(Buyer buyer in buyers)
                {
                    if (buyer.BuyerID == buyerID && buyer.Country.CountryName == countryName)
                    {
                        Console.WriteLine($"ID: {buyer.BuyerID}\nName: {buyer.FullName}\nCountry: {buyer.Country.CountryName}");
                        Console.Write("Sections: ");

                        foreach (Section section in buyer.Sections)
                        {
                            Console.Write(section.SectionName + " ");
                        }
                        Console.WriteLine("\n");
                    }
                }
            }
        }
        catch(Exception ex)
        {
            Console.WriteLine($"Something went wrong... Error: {ex.Message}");
        }
    }

    //Відобразити всі товари для конкретного покупця //16-й запит
    public async Task DisplayAllGoodsforsomeBuyer()
    {
        try
        {
            const string procedureName = "[DisplayALLGoodsforBuyer]";
            Console.Write("\nBuyerName: ");
            string buyerName = Console.ReadLine();

            using (SqlConnection connection = new SqlConnection(CONNECTION_STRING))
            {
                await connection.OpenAsync();

                IEnumerable<Buyer> buyers = await connection.QueryAsync<Buyer, Good, Buyer>(procedureName, (buyer, good) =>
                {
                    buyer.Goods.Add(good);
                    return buyer;
                }, splitOn: "GoodName", commandType: System.Data.CommandType.StoredProcedure);

                IEnumerable<Buyer> result = buyers.GroupBy(p => p.BuyerID).Select(g =>
                {
                    var groupedPost = g.First();
                    groupedPost.Goods = g.Select(p => p.Goods.Single()).ToList();
                    return groupedPost;
                });

                foreach (Buyer buyer in result)
                {
                    if (buyer.FullName.Contains(buyerName))
                    {
                        Console.WriteLine($"\nFullName: {buyer.FullName}");
                        Console.Write("Goods: ");
                        for(int i = 0; i<buyer.Goods.Count(); ++i)
                        {
                            Console.Write(buyer.Goods[i].GoodName);
                            if (i < buyer.Goods.Count() - 1)
                                Console.Write(", ");
                        }
                        Console.WriteLine();
                    }
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