using System.Text.RegularExpressions;

namespace DailyScribe
{
    public delegate void Output(string message);
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

            if (!Directory.Exists(url))
            {
                try
                {
                    information("Directory did not exist, so it was createad");
                    Directory.CreateDirectory(url!);
                }
                catch (Exception e)
                {
                    information("Couldnt create directory" + e.Message);
                    return;
                }
            }

            var fileName = $"{DateTime.Now:HH-mm}-{header}.txt";

            try
            {
                using StreamWriter writer = new StreamWriter(Path.Combine(url!, fileName));
                writer.WriteLine(body);
            }
            catch (Exception e)
            {
                information("Exception" + e.Message);
            }
        }
    }
}
