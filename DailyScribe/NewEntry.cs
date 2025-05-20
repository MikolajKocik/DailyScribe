using System.Text.RegularExpressions;

namespace DailyScribe
{
    public delegate void Output(string message); 
    public class NewEntry
    {
        public void Entry()
        {
            Output information = (text) => Console.WriteLine(text);

            information("Name your header (min.3 characters)");
            information("Provide your text message");

            var header = Console.ReadLine()
                ?? throw new ArgumentNullException("Header is null");

            var body = Console.ReadLine() 
                ?? throw new ArgumentNullException("Body is null");

            string headerPattern = @"^[a-zA-Z\s]+$";
                
            while(string.IsNullOrWhiteSpace(header) || !Regex.IsMatch(header, headerPattern, RegexOptions.IgnoreCase)
                || (header.Length < 3 || header.Length > 10))
            {
                information("Your message must consist of 3 to 10 characters, not valid input, try again");
                header = Console.ReadLine()!;
            }

            while (string.IsNullOrWhiteSpace(body) || body.Length > 300)
            {
                information("Maximum limit of message characters is 300, try again");
                body = Console.ReadLine()!;
            }

            information("Provide url destination note");
            var url = Console.ReadLine();

            string pattern = @"^((?:[a-zA-Z]:\\(?:[^\\\/:\*\?""<>\|\r\n]*\\)*[^\\\/:\*\?""<>\|\r\n]*)
                          |(?:\\\\[^\\\/:\*\?""<>\|\r\n]+\\[^\\\/:\*\?""<>\|\r\n]+(?:\\(?:[^\\\/:\*\?""<>\|\r\n]*\\)*[^\\\/:\*\?""<>\|\r\n]*)?))$";


            while (url is null || !Regex.IsMatch(url, pattern, RegexOptions.IgnoreCase)
)                   information("Provided url is not valid, try again"); url = Console.ReadLine(); 
            
            var fileName = $"{DateTime.Now.Hour}-{header}.txt";

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
