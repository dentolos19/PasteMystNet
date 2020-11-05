using System;
using System.IO;
using System.Threading.Tasks;
using PasteMystNet;

namespace PasteMystTest
{

    internal static class Program
    {

        private static void Main(string[] args)
        {
            Console.Title = "PasteMystTest";
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Clear();
            string input;
            if (args.Length > 0)
            {
                input = args[0];
                goto Skip;
            }
            Console.Write("Input File For Post/Get Functions Testing > ");
            input = Console.ReadLine();
            Skip:
            var content = File.ReadAllText(input);
            /*
            var form = new PasteMystForm
            {
                Code = content,
                Expirations = PasteMystExpirations.OneHour,
                Languages = PasteMystLanguages.Autodetect
            };
            try
            {
                Console.WriteLine("Testing Post Function (Non-Async)...");
                var output = PasteMystService.Post(form);
                Console.WriteLine($"Test Completed! VFP: {output.Link} // {output.Languages}");
                Console.WriteLine("Testing Get Function (Non-Async)...");
                output = PasteMystService.Get(output.Id);
                var fileOutput = Path.Combine(AppDomain.CurrentDomain.BaseDirectory!, "PasteMystTest-NonAsync.txt");
                File.WriteAllText(fileOutput, output.Code);
                Console.WriteLine($"Test Completed! Written To {fileOutput}!");
                Task.Run(() => DoAsyncTest(form));
                Console.ReadKey();
                Environment.Exit(0);
            }
            catch (Exception error)
            {
                Console.WriteLine($"An error had occurred! {error.Message} Press any key to exit...");
                Console.ReadKey();
                Environment.Exit(0);
            }
            */
        }

        /*
        private static async void DoAsyncTest(PasteMystForm form)
        {
            try
            {
                Console.WriteLine("Testing Post Function (Async)...");
                var output = await PasteMystService.PostAsync(form);
                Console.WriteLine($"Test Completed! VFP: {output.Link} // {output.Languages}");
                Console.WriteLine("Testing Get Function (Async)...");
                output = await PasteMystService.GetAsync(output.Id);
                var fileOutput = Path.Combine(AppDomain.CurrentDomain.BaseDirectory!, "PasteMystTest-Async.txt");
                await File.WriteAllTextAsync(fileOutput, output.Code);
                Console.WriteLine($"Test Completed! Written To {fileOutput}!");
                Console.WriteLine("Overall Test Completed! Press any key to exit...");
            }
            catch (Exception error)
            {
                Console.WriteLine($"An error had occurred! {error.Message} Press any key to exit...");
            }
        }
        */

    }

}