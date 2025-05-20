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
        public void Entry()
        {

            Output information = (text) => Console.WriteLine(text);

            information("Name your header (min. 3 characters, letters and spaces only, max 10 characters)");

            var header = Console.ReadLine()?.Trim();

            string headerPattern = @"^[a-zA-Z\s]+$";

            while (string.IsNullOrWhiteSpace(header) || !Regex.IsMatch(header, headerPattern)
                || (header.Length < 3 || header.Length > 10))
            {
                information("Your header must consist of 3 to 10 characters (letters and spaces only). Invalid input, try again:");
                header = Console.ReadLine()!;
            }

            information("Provide your text message");

            var body = Console.ReadLine()?.Trim();

            while (string.IsNullOrWhiteSpace(body) || body.Length > 300)
            {
                information("Maximum limit of message characters is 300, try again");
                body = Console.ReadLine()!;
            }

            information("Provide url destination note");
            var url = Console.ReadLine()?.Trim();

            string pattern = @"^(?:[a-zA-Z]:\\|\\\\)(?:[^<>:""/\\|?*]+\\)*[^<>:""/\\|?*]+$";

            while (url is null || !Regex.IsMatch(url, pattern, RegexOptions.IgnoreCase))
            {
                information("Provided url is not valid, try again");
                url = Console.ReadLine()!;
            }

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
                writer.WriteLine(body);

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
