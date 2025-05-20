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
        public string[] Edit(string filePath, string entryName)
        {
            Output information = (text) => Console.WriteLine(text);

            string[] lines = File.ReadAllLines(filePath);

            if (lines.Length == 0)
            {
                information($"Nothing to change: {lines}");
                return lines;
            }

            for (int i = 0; i < lines.Length; i++)
            {
                information($"{i + 1} line. {lines[i]}");
            }

            information("Enter the line number you want to edit:");

            int lineNumber;

            while(!int.TryParse(Console.ReadLine()?.Trim(), out lineNumber) || lineNumber < 1 || lineNumber > lines.Length)
            {
                information("Invalid line number, try again: ");
            }


            information("Enter the new text for the line:");

            string newText = Console.ReadLine()?.Trim()!;

            while (string.IsNullOrEmpty(newText))
            {
                information("Invalid input. Please enter the new text for the line:");
                newText = Console.ReadLine()?.Trim()!;
            }

            // odejmujemy -1 ze względu na wcześniejsze dodanie w pętli for
            lines[lineNumber - 1] = newText;

            File.WriteAllLines(filePath, lines);

            information($"Entry '{entryName}' has been edited successfully.");

            return lines;
        }
    }
}
