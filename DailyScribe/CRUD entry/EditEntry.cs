using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyScribe
{
    /// <summary>
    /// Edytuje wskazany wiersz w pliku tekstowym (notatce).
    /// </summary>
    /// <param name="filePath">Ścieżka do pliku notatki</param>
    /// <param name="entryName">Nazwa notatki wyświetlana użytkownikowi</param>
    /// <returns>Zawartość pliku po edycji jako tablica stringów</returns>

    public class EditEntry
    {
        public string[] Edit(string filePath, string entryName, string newText, int lineNumber)
        {
            Output information = (text) => Console.WriteLine(text);

            string[] lines = File.ReadAllLines(filePath);

            if (!filePath.Equals(typeof(File)))
                throw new UriFormatException("Bad url format");

            if (lines.Length == 0)
                throw new IndexOutOfRangeException("Nothing to change, no lines here");

            if (string.IsNullOrEmpty(newText))
                throw new ArgumentException("Invalid parameter body");

            // odejmujemy -1 ze względu na wcześniejsze dodanie w pętli for
            lines[lineNumber - 1] = newText;

            File.WriteAllLines(filePath, lines);

            information($"Entry '{entryName}' has been edited successfully.");

            return lines;
        }
    }
}
