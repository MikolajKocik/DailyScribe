using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DailyScribe
{  
   /// <summary>
   /// Obsługuje wybieranie i odczytywanie zawartości zapisanych notatek z plików tekstowych.
   /// </summary>
   /// <remarks>
   /// Wczytuje plik entries.log zawierający listę nazw notatek,
   /// pozwala użytkownikowi wybrać jedną z nich i wyświetla jej zawartość.
   /// </remarks>
   
    public class ReadEntry
    {
        public void Entries(string logPath)
        {
            // delegata
            Output information = (text) => Console.WriteLine(text);

            string[] entries = File.ReadAllLines(logPath);

            for (int i = 0; i < entries.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {entries[i]}");
            }

            information("Choose number");

            int choice;

            while (!int.TryParse(Console.ReadLine()?.Trim(), out choice) || choice < 1 || choice > entries.Length) 
            {
                information("Invalid choice / bad format, try again: ");
            }

            var fileName = entries[choice - 1];

            var directoryPath = Path.GetDirectoryName(logPath);

            var filePath = Path.Combine(directoryPath!, fileName);

            using StreamReader reader = new StreamReader(filePath);

            string content = reader.ReadToEnd();

            information(content);
        }
    }
}
