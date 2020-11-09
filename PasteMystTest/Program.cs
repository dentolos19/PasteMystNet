using System;
using PasteMystNet;

namespace PasteMystTest
{

    internal static class Program
    {

        private static void Main()
        {
            Console.Title = "PasteMyst.NET";
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Clear();

            var paste = new PasteMystPasteForm
            {
                Title = "PasteMyst.NET - Test",
                ExpireDuration = PasteMystExpiration.OneHour,
                Tags = new []
                {
                    "test",
                    "dotnet"
                },
                Pasties = new []
                {
                    new PasteMystPastyForm
                    {
                        Title = "Test.txt",
                        Code = "Hello World"
                    }
                }
            };
            Console.WriteLine(paste.PostPasteAsync(new PasteMystAuth("7eJl8HEfEWo6W9mjPevSZgGlOBLm5AHUBWeide+wnU4=")).Result);
            
            Console.ReadKey();
            Environment.Exit(Environment.ExitCode);
        }

    }

}