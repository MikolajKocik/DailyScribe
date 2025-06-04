using DailyScribe.Cryptography.AES;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace DailyScribe;

public class Program
{
    public const int iVSize = 16;  

    static async Task Main(string[] args)
    {
        string urlAesDestination;

        var masterKey = RandomNumberGenerator.GetBytes(32);

        var encryptMode = new AES_encryption();

        int choice;

        try
        {
            do
            {
                Terminal terminal = new Terminal();
                terminal.View();

                Console.WriteLine("Choose option below");
                Console.WriteLine("(1) Add new entry");
                Console.WriteLine("(2) Load entry");
                Console.WriteLine("(3) Edit entry");
                Console.WriteLine("(4) Delete entry");
                Console.WriteLine("(5) Encrypt file");
                Console.WriteLine("(6) Decrypt file");
                Console.WriteLine("(7) Close");

                Console.WriteLine();

                while (!int.TryParse(Console.ReadLine()?.Trim()!, out choice) || choice < 1 || choice > 6)
                {
                    Console.WriteLine($"Invalid choice: {choice}, try again: ");
                }

                switch (choice)
                {
                    case 1:
                        NewEntry create = new NewEntry();

                        Console.WriteLine("Name your header (min. 3 characters, letters and spaces only, max 10 characters)");

                        var header = Console.ReadLine()?.Trim();

                        string headerPattern = @"^[a-zA-Z\s]+$";

                        while (string.IsNullOrWhiteSpace(header) || !Regex.IsMatch(header, headerPattern)
                            || (header.Length < 3 || header.Length > 10))
                        {
                            Console.WriteLine("Your header must consist of 3 to 10 characters (letters and spaces only). Invalid input, try again:");
                            header = Console.ReadLine()!;
                        }

                        Console.WriteLine("Provide your text message");

                        var body = Console.ReadLine()?.Trim();

                        while (string.IsNullOrWhiteSpace(body) || body.Length > 300)
                        {
                            Console.WriteLine("Maximum limit of message characters is 300, try again");
                            body = Console.ReadLine()!;
                        }

                        Console.WriteLine("Provide url destination note");
                        var url = Console.ReadLine()?.Trim();

                        string pattern = @"^(?:[a-zA-Z]:\\|\\\\)(?:[^<>:""/\\|?*]+\\)*[^<>:""/\\|?*]+$";

                        while (url is null || !Regex.IsMatch(url, pattern, RegexOptions.IgnoreCase))
                        {
                            Console.WriteLine("Provided url is not valid, try again");
                            url = Console.ReadLine()!;
                        }

                        create.Entry(header, body, url);
                        break;

                    case 2:
                        ReadEntry read = new ReadEntry();

                        Console.WriteLine("Enter url of notes (with no file name)");

                        var folderPath = Console.ReadLine()?.Trim()!;

                        // łączymy ścieżkę folderu z nazwą pliku entries.log z sekcji "1"
                        var logPath = Path.Combine(folderPath, "entries.log");

                        while (string.IsNullOrEmpty(logPath) || !File.Exists(logPath))
                        {
                            Console.WriteLine(@"Enter folder path where your notes were saved (e.g. C:\\Users\\You\\Documents\\MyNotes):");
                            logPath = Console.ReadLine()?.Trim();
                        }

                        string[] entries = File.ReadAllLines(logPath);

                        for (int i = 0; i < entries.Length; i++)
                        {
                            Console.WriteLine($"{i + 1}. {entries[i]}");
                        }

                        Console.WriteLine("Choose number");

                        int getNumber;

                        while (!int.TryParse(Console.ReadLine()?.Trim(), out getNumber) || getNumber < 1 || getNumber > entries.Length)
                        {
                            Console.WriteLine("Invalid choice / bad format, try again: ");
                        }

                        read.Entries(logPath, entries, getNumber);
                        break;

                    case 3:
                        EditEntry editText = new EditEntry();

                        Console.WriteLine("Enter url of notes (with no file name)");

                        var editFolderPath = Console.ReadLine()?.Trim()!;

                        while (string.IsNullOrEmpty(editFolderPath) || !Directory.Exists(editFolderPath))
                        {
                            Console.WriteLine("Invalid path, try again: ");
                            editFolderPath = Console.ReadLine()?.Trim()!;
                        }

                        Console.WriteLine("Enter file name");

                        var userInput = Console.ReadLine()?.Trim()!;

                        while (string.IsNullOrEmpty(userInput))
                        {
                            Console.WriteLine("Invalid file name, try again: ");
                            userInput = Console.ReadLine()?.Trim()!;
                        }

                        var fullPath = Path.GetFullPath(editFolderPath, userInput);

                        if (!File.Exists(fullPath))
                        {
                            Console.WriteLine("File not found");
                            break;
                        }

                        string[] lines = File.ReadAllLines(fullPath);

                        if (lines.Length == 0)
                        {
                            Console.WriteLine($"Nothing to change: {lines}");
                            return;
                        }

                        for (int i = 0; i < lines.Length; i++)
                        {
                            Console.WriteLine($"{i + 1} line. {lines[i]}");
                        }

                        Console.WriteLine("Enter the line number you want to edit: ");

                        int lineNumber;

                        while (!int.TryParse(Console.ReadLine()?.Trim(), out lineNumber) || lineNumber < 1 || lineNumber > lines.Length)
                        {
                            Console.WriteLine("Invalid line number, try again: ");
                        }

                        string newText = Console.ReadLine()?.Trim()!;

                        while (string.IsNullOrEmpty(newText))
                        {
                            Console.WriteLine("Invalid input. Please enter the new text for the line:");
                            newText = Console.ReadLine()?.Trim()!;
                        }

                        editText.Edit(fullPath, userInput, newText, lineNumber);
                        break;

                    case 4:
                        DeleteEntry deleteText = new DeleteEntry();

                        Console.WriteLine("Enter url of notes (with no file name)");

                        var deleteFolderPath = Console.ReadLine()?.Trim()!;

                        while (string.IsNullOrEmpty(deleteFolderPath) || !Directory.Exists(deleteFolderPath))
                        {
                            Console.WriteLine("Invalid path, try again: ");
                            deleteFolderPath = Console.ReadLine()?.Trim()!;
                        }

                        Console.WriteLine("Enter file name");

                        var response = Console.ReadLine()?.Trim()!;

                        while (string.IsNullOrEmpty(response))
                        {
                            Console.WriteLine("Invalid file name, try again: ");
                            response = Console.ReadLine()?.Trim()!;
                        }

                        var getPath = Path.GetFullPath(deleteFolderPath, response);

                        if (!File.Exists(getPath))
                        {
                            Console.WriteLine("File not found");
                            break;
                        }

                        Console.WriteLine("Provide line number to delete ");

                        string[] textLines = File.ReadAllLines(getPath);

                        if (textLines.Length == 0)
                        {
                            Console.WriteLine($"Nothing to delete: {textLines}");
                            break;
                        }

                        for (int i = 0; i < textLines.Length; i++)
                        {
                            Console.WriteLine($"{i + 1} line. {textLines[i]}");
                        }

                        string? input = Console.ReadLine()?.Trim();

                        if (string.IsNullOrEmpty(input))
                        {
                            Console.WriteLine("Invalid input, try again: ");
                            input = Console.ReadLine()?.Trim();
                        }

                        int lineToDelete;

                        while (!int.TryParse(input, out lineToDelete) || lineToDelete <= 0)
                        {
                            Console.WriteLine("Invalid input, try again");
                            input = Console.ReadLine()?.Trim();
                        }

                        deleteText.Remove(getPath, response, lineToDelete);

                        break;
                    case 5:

                        Console.WriteLine("Provide file url directory");

                        urlAesDestination = Console.ReadLine()?.Trim()!;

                        while (string.IsNullOrEmpty(urlAesDestination))
                        {
                            Console.WriteLine("Invalid url, try again: ");
                            urlAesDestination = Console.ReadLine()?.Trim()!;
                        }

                        var plaintext = await File.ReadAllTextAsync(urlAesDestination);

                        var encrypted = encryptMode.Encrypt(plaintext, masterKey);
                        Console.WriteLine(encrypted);
                        break;
                    case 6:

                        Console.WriteLine("Provide file url directory");

                        urlAesDestination = Console.ReadLine()?.Trim()!;

                        while (string.IsNullOrEmpty(urlAesDestination))
                        {
                            Console.WriteLine("Invalid url, try again: ");
                            urlAesDestination = Console.ReadLine()?.Trim()!;
                        }

                        var text = await File.ReadAllTextAsync(urlAesDestination);

                        var ciphertext = encryptMode.Encrypt(text, masterKey);

                        if (ciphertext == null)
                        {
                            Console.WriteLine("Nothing to decrypt. Please encrypt something first");
                            break;
                        }

                        var decryptMode = new AES_decryption();
                        var decrypted = decryptMode.Decrypt(ciphertext, masterKey);
                        Console.WriteLine(decrypted);

                        break;

                    default:
                        Console.WriteLine("Closing program...");
                        break;
                }

            } while (choice != 7);
        }
        catch (ArgumentOutOfRangeException ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
        catch (UriFormatException ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
        catch (DirectoryNotFoundException ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
        catch (IndexOutOfRangeException ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.InnerException?.Message}");
        }
    }
}