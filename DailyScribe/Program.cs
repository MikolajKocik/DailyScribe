using System;

namespace DailyScribe;

class Program
{
    static void Main(string[] args)
    {
        int choice;

        do
        {
            Terminal terminal = new Terminal();
            terminal.View();

            Console.WriteLine("Choose option below");
            Console.WriteLine("(1) Add new entry");
            Console.WriteLine("(2) Load entry");
            Console.WriteLine("(3) Close");

            Console.WriteLine();

            while (!int.TryParse(Console.ReadLine()?.Trim()!, out choice) || choice < 1 || choice > 3)
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
                default:
                    Console.WriteLine("Closing program...");
                    break;
            }

        }while (choice != 3);
    }
}