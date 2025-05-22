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
        public string[] Remove(string filePath, string entryName, int lineToDelete)
        {
            Output information = (text) => Console.WriteLine(text);

            string[] lines = File.ReadAllLines(filePath);

            // iterujemy po indeksach tablicy lines i usuwamy ten, który odpowiada podanemu numerowi
            lines = lines.Where((line, index) => index != lineToDelete - 1).ToArray();

            File.WriteAllLines(filePath, lines);

            information($"Entry {entryName} was deleted successfully.");

            return lines;
        }
    }
}
