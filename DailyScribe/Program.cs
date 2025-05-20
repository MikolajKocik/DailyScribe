using DailyScribe.Cryptography.AES;
using System.Security.Cryptography;

namespace DailyScribe;

public class Program
{
    public const int iVSize = 16;
    static async Task Main(string[] args)
    {
        // AES section

        Console.WriteLine("Provide file url directory");

        var urlAesDestination = Console.ReadLine()?.Trim();

        while (string.IsNullOrEmpty(urlAesDestination))
        {
            Console.WriteLine("Invalid url, try again: ");
            urlAesDestination = Console.ReadLine()?.Trim();
        }

        var plaintext = await File.ReadAllTextAsync(urlAesDestination);

        var masterKey = RandomNumberGenerator.GetBytes(32);

        string? encrypted = null;

        // ------------------------------

        int choice;

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

            switch(choice)
            {
                case 1:
                    NewEntry create = new NewEntry();
                    create.Entry();
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

                    read.Entries(logPath);
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

                    if(!File.Exists(fullPath))
                    {
                        Console.WriteLine("File not found");
                        break;                   
                    }

                    editText.Edit(fullPath, userInput);
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

                    deleteText.Remove(getPath, response);                   

                    break;
                case 5:

                    encrypted = AES_encryption.Encrypt(plaintext, masterKey);
                    Console.WriteLine(encrypted);
                    break;
                case 6:

                    if (encrypted == null)
                    {
                        Console.WriteLine("Nothing to decrypt. Please encrypt something first");
                        break;
                    }
                    var decrypted = AES_decryption.Decrypt(encrypted, masterKey);
                    Console.WriteLine(decrypted);

                    break;

                default:
                    Console.WriteLine("Closing program...");
                    break;
            }

        }while (choice != 7);
    }
}