using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        string inputFilePath = "dps.txt";
        string outputFilePath = "result.txt";
        string detectsUrl = "https://raw.githubusercontent.com/shadowardik/troxill-fucker/refs/heads/main/detect";

        if (!File.Exists(inputFilePath))
        {
            Console.WriteLine($"Файл {inputFilePath} не найден.");
            return;
        }

        string[] detects = await detecti_joskie(detectsUrl);
        string description = "troxill finded.";

        try
        {
            string[] lines = File.ReadAllLines(inputFilePath);
            using (StreamWriter writer = new StreamWriter(outputFilePath))
            {
                foreach (string line in lines)
                {
                    foreach (string detect in detects)
                    {
                        if (line.Contains(detect))
                        {
                            writer.WriteLine($"{line} - {description}");
                            break;
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Произошла ошибка: {ex.Message}");
        }
    }

    static async Task<string[]> detecti_joskie(string url)
    {
        using (HttpClient client = new HttpClient())
        {
            string content = await client.GetStringAsync(url);
            return content.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}
