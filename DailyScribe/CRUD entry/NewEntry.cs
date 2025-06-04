using System.Text.RegularExpressions;

namespace DailyScribe
{
    public delegate void Output(string message);

    /// <summary>
    /// Obsługuje proces tworzenia nowej notatki.
    /// </summary>
    /// <remarks>
    /// - Zapisuje plik .txt z treścią notatki.
    /// - Dodaje nazwę pliku do entries.log w tym samym folderze.
    /// - entries.log używany jest później w klasie ReadEntry do odczytu listy.
    /// </remarks>

    public class NewEntry
    {
        public void Entry(string header, string body, string url)
        {

            if (string.IsNullOrWhiteSpace(url))
            {
                throw new ArgumentNullException(nameof(url), "URL cannot be null or empty");
            }

            Output information = (text) => Console.WriteLine(text);         

            var fileName = $"{DateTime.Now:HH-mm}-{header}.txt";

            // Tworzy ścieżkę do pliku .txt w folderze wybranym przez użytkownika (url).
            var filePath = Path.Combine(url!, fileName);

            // Tworzy ścieżkę do pliku entries.log w folderze wybranym przez użytkownika (url).
            // Ten plik zawiera listę nazw wszystkich zapisanych notatek (.txt),
            // dzięki czemu można je później łatwo załadować w opcji "Load entry".
            var logPath = Path.Combine(url!, "entries.log");

            try
            {
                using StreamWriter writer = new StreamWriter(filePath);
                writer.Write(body);

                // ostatecznie zapisuje plik entries.log z nazwą pliku .txt + z nową linią
                File.AppendAllText(logPath, fileName + Environment.NewLine);
            }
            catch (Exception e)
            {
                information("Exception" + e.Message);
            }
        }
    }
}
