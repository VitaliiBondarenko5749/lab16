using Dapper;
using System.Data.SqlClient;

class Program 
{
    public static async Task Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.Unicode;
        Console.InputEncoding = System.Text.Encoding.Unicode;

        while (true)
        {
            await DisplayMenu();

            Console.Write("\nEnter any command: ");
            int command = int.Parse(Console.ReadLine());
            Console.Clear();

            if (command == 21)
                return;

            switch (command)
            {
                case 1: await new BuyerRepository().DisplayAllInfoAboutBuyers(); break;

                case 2: await new BuyerRepository().DisplayAllEmails(); break;

                case 3: await new SectionRepository().DisplayAllSections(); break;

                case 4: await new GoodRepository().DisplayAllGoods(); break;

                case 5: await new CityRepository().DisplayAllCitites(); break;

                case 6: await new CountryRepository().DisplayAllCountries(); break;

                case 7: await new CityRepository().DisplayAllCitites();
                    await new BuyerRepository().DisplayAllBuyers_BY_City(); break;

                case 8: await new CountryRepository().DisplayAllCountries();
                    await new BuyerRepository().DisplayALLBuyers_BY_Country(); break;

                case 9: await new CountryRepository().DisplayAllCountries();
                    await new ShareRepository().DisplayAllSharesBY_Country(); break;

                case 10: await new BuyerRepository().DisplayCountBuyers_BY_City(); break;

                case 11: await new BuyerRepository().DisplayCountBuyers_BY_Country(); break;

                case 12: await new CountryRepository().DisplayCountCitiesGROUPED_BY_Countries(); break;

                case 13: await new CityRepository().DisplayAVG_Cities(); break;

                case 14: await new SectionRepository().DisplayAllSections(); break;

                case 15: await new ShareRepository().DisplayALLShares_andGood_byEnteredTime(); break;

                case 16: await new BuyerRepository().DisplayAllInfoAboutBuyers();
                    await new BuyerRepository().DisplayAllGoodsforsomeBuyer(); break;

                case 17: await new CountryRepository().DisplayTOP3CountriesBY_CountOF_Buyers(); break;

                case 18: await new CountryRepository().DisplayTheBestCountryByCountOfBuyers(); break;

                case 19: await new CityRepository().DisplayTOP3CitiesbyCountOfBuyers(); break;

                case 20: await new CityRepository().DisplayTheBestCityBY_CountofBuyers(); break;
            }

            Console.ReadKey();
            Console.Clear();
        }

    }

    private static async Task DisplayMenu()
    {
        Console.WriteLine("\n1. Display all buyers\n" +
            "2. Display all buyer emails\n" +
            "3. Display section lists\n" +
            "4. Display good lists\n" +
            "5. Display all cities\n" +
            "6. Display all countries\n" +
            "7. Display buyers from specisic city\n" +
            "8. Display buyers from specific country\n" +
            "9. Display a share for specisfic country\n" +
            "10. Display count of buyers in every city\n" +
            "11. Display count of buyers in every country\n" +
            "12. Display count of cities in every country\n" +
            "13. Display an average count of cities in every country\n" +
            "14. Display all sections, that are being interested by specific buyers of specific country\n" +
            "15. Display all shares of good for entered interval of the time\n" +
            "16. Display all goods for specific buyer\n" +
            "17. Display top 3 countries by count of buyers\n" +
            "18. Display the best country by count of buyers\n" +
            "19. Display top 3 cities by count of buyers\n" +
            "20. Display the best city by count of buyers\n" +
            "21. Exit\n"
            );
    }
}
