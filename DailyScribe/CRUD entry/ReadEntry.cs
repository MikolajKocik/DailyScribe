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
        public string[] Entries(string logPath, string[] entries, int getNumber)
        {
            // delegata
            Output information = (text) => Console.WriteLine(text);

            if (getNumber > entries.Length)
                throw new IndexOutOfRangeException("Provided number is out of the entries array bounds.");

            var fileName = entries[getNumber - 1];

            var directoryPath = Path.GetDirectoryName(logPath);

            var filePath = Path.Combine(directoryPath!, fileName);

            using StreamReader reader = new StreamReader(filePath);

            string content = reader.ReadToEnd();

            information(content);

            string[] stringArray = content.Split(", ", content.Length);

            return stringArray;
        }
    }
}
