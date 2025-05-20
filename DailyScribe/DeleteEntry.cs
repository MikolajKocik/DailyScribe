using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyScribe
{
    /// <summary>
    /// Obsługuje usuwanie wybranej linii z notatki tekstowej.
    /// </summary>
    /// <remarks>
    /// Wczytuje plik, wyświetla numerowane linie użytkownikowi, a następnie usuwa wybraną z nich.
    /// Linia jest całkowicie usuwana z tablicy, a wynik nadpisuje oryginalny plik.
    /// </remarks>
    
    public class DeleteEntry
    {
        public string[] Remove(string filePath, string entryName)
        {
            Output information = (text) => Console.WriteLine(text);

            string[] lines = File.ReadAllLines(filePath);

            if (lines.Length == 0)
            {
                information($"Nothing to delete: {lines}");
                return lines;
            }

            for (int i = 0; i < lines.Length; i++)
            {              
                information($"{i + 1} line. {lines[i]}");
            }

            information("Enter the line number you want to delete:");

            int lineNumber;

            while (!int.TryParse(Console.ReadLine()?.Trim(), out lineNumber) || lineNumber < 1 || lineNumber > lines.Length)
            {
                information("Invalid line number, try again: ");
            }

            // iterujemy po indeksach tablicy lines i usuwamy ten, który odpowiada podanemu numerowi
            lines = lines.Where((line, index) => index != lineNumber - 1).ToArray();

            File.WriteAllLines(filePath, lines);

            information($"Entry {entryName} was deleted successfully.");

            return lines;
        }
    }
}
