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
            Console.Write("> ");
            var input = Console.ReadLine();
            Console.WriteLine("Posting...");
            var form = new PasteMystForm
            {
                Code = File.ReadAllText(input),
                Expiration = PasteMystExpiration.OneDay,
                Language = PasteMystLanguage.Autodetect
            };
            var output = PasteMystService.Post(form);
            Console.WriteLine("Posted! Checking info...");
            Console.WriteLine($"Id: {output.Id}");
            Console.WriteLine($"Creation Date: {output.Date}");
            Console.WriteLine($"Expiration: {output.Expiration}");
            Console.WriteLine($"Language {output.Language}");
            Console.ReadKey();
            Environment.Exit(0);
        }

    }

}