using System;
using System.ComponentModel;
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

            Console.WriteLine("PasteMystTest");

            Console.WriteLine();
            Console.WriteLine("Testing: \"PasteMystUser.UserExistsAsync()\" // codemyst");

            Console.WriteLine();
            var userExistsResult = PasteMystUser.UserExistsAsync("codemyst").Result;
            Console.WriteLine("Result: " + userExistsResult);

            Console.WriteLine();
            Console.WriteLine("Testing: \"PasteMystUser.UserExistsAsync()\" // derpmyst");

            Console.WriteLine();
            userExistsResult = PasteMystUser.UserExistsAsync("derpmyst").Result;
            Console.WriteLine("Result: " + userExistsResult);

            Console.WriteLine();
            Console.WriteLine("Testing: \"PasteMystUser.GetUserAsync()\" // codemyst");

            Console.WriteLine();
            var getUserResult = PasteMystUser.GetUserAsync("codemyst").Result;
            if (getUserResult == null)
            {
                Console.WriteLine("Result: null");
            }
            else
            {
                foreach (PropertyDescriptor type in TypeDescriptor.GetProperties(getUserResult))
                {
                    Console.WriteLine($"{type.Name} = {type.GetValue(getUserResult)}");
                }
            }

            Console.WriteLine();
            Console.WriteLine("Testing: \"PasteMystPaste.GetPasteAsync()\" // pbiigv0u");

            Console.WriteLine();
            var getPasteResult = PasteMystPaste.GetPasteAsync("pbiigv0u").Result;
            if (getPasteResult == null)
            {
                Console.WriteLine("Result: null");
            }
            else
            {
                foreach (PropertyDescriptor type in TypeDescriptor.GetProperties(getPasteResult))
                {
                    Console.WriteLine($"{type.Name} = {type.GetValue(getPasteResult)}");
                }
            }

            Console.ReadKey();
            Environment.Exit(0);

        }

    }

}