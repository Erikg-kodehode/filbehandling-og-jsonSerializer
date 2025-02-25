using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;

public static class JsonHandler
{
    public static void WriteJsonToFile(string path, List<Person> data)
    {
        string jsonString = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(path, jsonString);
    }

    public static List<Person> ReadJsonFromFile(string path)
    {
        if (!File.Exists(path)) return new List<Person>();
        string jsonString = File.ReadAllText(path);
        return JsonSerializer.Deserialize<List<Person>>(jsonString) ?? new List<Person>();
    }
}
