using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DailyScribe
{
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
