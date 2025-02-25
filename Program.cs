using System;
using System.Threading.Tasks;

public class Program
{
    public static async Task Main()
    {
        Console.OutputEncoding = System.Text.Encoding.Unicode; // Force Unicode output
        string apiUrl = "http://localhost:3000/people";
        bool running = true;

        while (running)
        {
            Console.WriteLine("\nVelg en handling:");
            Console.WriteLine("1. Vis alle personer");
            Console.WriteLine("2. Søk etter en person (via ID)");
            Console.WriteLine("3. Legg til en ny person");
            Console.WriteLine("4. Slett en person (via ID)");
            Console.WriteLine("5. Avslutt");
            Console.Write("Ditt valg: ");

            string? choice = Console.ReadLine()?.Trim();


            switch (choice)
            {
                case "1":
                    await ApiHandler.DisplayAllPeople(apiUrl);
                    break;

                case "2":
                    await ApiHandler.SearchPerson(apiUrl);
                    break;

                case "3":
                    await ApiHandler.AddNewPerson(apiUrl);
                    break;

                case "4":
                    await ApiHandler.DeletePerson(apiUrl);
                    break;

                case "5":
                    running = false;
                    break;

                default:
                    Console.WriteLine("Ugyldig valg, prøv igjen.");
                    break;
            }
        }
    }
}
