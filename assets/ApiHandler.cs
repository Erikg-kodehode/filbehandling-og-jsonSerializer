using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

public static class ApiHandler
{
    public static async Task DisplayAllPeople(string apiUrl)
    {
        string allPeople = await FetchDataFromApi(apiUrl);
        Console.WriteLine("\nPersoner i API:");
        PrintFormattedJson(allPeople);
    }

    public static async Task SearchPerson(string apiUrl)
    {
        Console.Write("Skriv inn navnet på personen du vil søke etter: ");
        string? personName = Console.ReadLine()?.Trim();
        if (!string.IsNullOrWhiteSpace(personName))
        {
            string personData = await FetchDataFromApi($"{apiUrl}/{personName}");
            Console.WriteLine("\nResultat:");
            PrintFormattedJson(personData);
        }
    }

    public static async Task AddNewPerson(string apiUrl)
    {
        Console.Write("Skriv inn navn: ");
        string? name = Console.ReadLine();

        Console.Write("Skriv inn alder: ");
        string? ageInput = Console.ReadLine();

        Console.Write("Skriv inn by: ");
        string? city = Console.ReadLine();

        Console.Write("Skriv inn land: ");
        string? country = Console.ReadLine();

        if (!string.IsNullOrWhiteSpace(name) && int.TryParse(ageInput, out int age) && !string.IsNullOrWhiteSpace(city) && !string.IsNullOrWhiteSpace(country))
        {
            var newPerson = new Person { Name = name, Age = age, City = city, Country = country };
            await SendDataToApi(apiUrl, newPerson);
        }
        else
        {
            Console.WriteLine("Ugyldig input, prøv igjen.");
        }
    }

    public static async Task DeletePerson(string apiUrl)
    {
        Console.Write("Skriv inn navnet på personen du vil slette: ");
        string? deleteName = Console.ReadLine()?.Trim();
        if (!string.IsNullOrWhiteSpace(deleteName))
        {
            await DeletePersonFromApi(apiUrl, deleteName);
        }
    }

    private static async Task<string?> FetchDataFromApi(string url)
    {
        using (HttpClient client = new HttpClient())
        {
            HttpResponseMessage response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
    }

    private static async Task SendDataToApi(string url, Person person)
    {
        using (HttpClient client = new HttpClient())
        {
            string json = JsonSerializer.Serialize(person);
            StringContent content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(url, content);
            response.EnsureSuccessStatusCode();
            Console.WriteLine("Data sendt til API.");
        }
    }

    private static async Task DeletePersonFromApi(string url, string name)
    {
        using (HttpClient client = new HttpClient())
        {
            HttpResponseMessage response = await client.DeleteAsync($"{url}/{name}");
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Person slettet.");
            }
            else
            {
                Console.WriteLine("Kunne ikke slette personen.");
            }
        }
    }

    private static void PrintFormattedJson(string jsonString)
    {
        var jsonElement = JsonSerializer.Deserialize<JsonElement>(jsonString);
        string formattedJson = JsonSerializer.Serialize(jsonElement, new JsonSerializerOptions { WriteIndented = true });
        Console.WriteLine(formattedJson);
    }
}
