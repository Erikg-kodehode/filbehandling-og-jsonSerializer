using System;
using System.IO;

public static class FileHandler
{
    public static void WriteToFile(string path, string content)
    {
        File.WriteAllText(path, content);
    }

    public static string ReadFromFile(string path)
    {
        return File.Exists(path) ? File.ReadAllText(path) : "Fil ikke funnet";
    }
}
