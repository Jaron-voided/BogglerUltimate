using System.Text.Json;

namespace BogglerUltimate.Boggle;

public class BoggleDictionary
{
    internal List<string> Words = new List<string>();
    
    public static BoggleDictionary GetBoggleDictionary()
    {
        string filePath = "/home/zeref-dragneel/Desktop/Boggle/Boggle/dictionary.json";
        string fileContent = File.ReadAllText(filePath);

        List<string> wordList = JsonSerializer.Deserialize<List<string>>(fileContent);

        BoggleDictionary boggleDictionary = new BoggleDictionary
        {
            Words = wordList
                .Where(word => !string.IsNullOrEmpty(word) && word.Length <= 16 && word.Length > 2)
                .Select(word => word.ToUpper())
                .ToList()
                
        };
        
        return boggleDictionary;
    }

}