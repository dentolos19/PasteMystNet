using System;
using System.IO;
using PasteMystNet;

namespace PasteMystTest
{

    internal static class Program
    {

        private static void Main()
        {
            Console.Title = "PasteMystTest";
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Clear();
            Console.Write("Input File To Post > ");
            var input = Console.ReadLine();
            Console.WriteLine("Posting...");
            var form = new PasteMystForm
            {
                Code = File.ReadAllText(input),
                Expiration = PasteMystExpiration.OneDay,
                Language = PasteMystLanguage.Autodetect
            };
            var info1 = PasteMystService.Post(form);
            Console.WriteLine("Posted! Checking info...");
            Console.WriteLine($"Id: {info1.Id}");
            Console.WriteLine($"Creation Date: {info1.Date}");
            Console.WriteLine($"Expiration: {info1.Expiration}");
            Console.WriteLine($"Language: {info1.Language}");
            Console.WriteLine($"Info checked! Press any key to try GET function.");
            Console.ReadKey();
            Console.WriteLine("Fetching...");
            var info2 = PasteMystService.Get(info1.Id);
            Console.WriteLine("Fetched! Checking info...");
            Console.WriteLine($"Id: {info2.Id}");
            Console.WriteLine($"Creation Date: {info2.Date}");
            Console.WriteLine($"Expiration: {info2.Expiration}");
            Console.WriteLine($"Language: {info2.Language}");
            Console.WriteLine($"Info checked! Press any key to exit.");
            Console.ReadKey();
            Environment.Exit(0);
        }

    }

}