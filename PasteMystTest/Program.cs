using System;
using System.IO;
using System.Threading.Tasks;
using PasteMystNet;

namespace PasteMystTest
{

    internal static class Program
    {

        private static void Main()
        {
            Console.Title = "PasteMystTest";
            Console.Write("Input File > ");
            var input = Console.ReadLine();
            var content = File.ReadAllText(input);
            var form = new PasteMystForm
            {
                Code = content,
                Expiration = PasteMystExpiration.OneHour,
                Language = PasteMystLanguage.Autodetect
            };
            Console.WriteLine("Testing Post Function (Non-Async)...");
            var output = PasteMystService.Post(form);
            Console.WriteLine($"Test Completed! VFP: {output.Id} // {output.Language}");
            Console.WriteLine("Testing Get Function (Non-Async)...");
            output = PasteMystService.Get(output.Id);
            Console.WriteLine($"Test Completed! VFP: {output.Id} // {output.Language}");
            Task.Run(() => DoAsyncTest(form));
            Console.ReadKey();
            Environment.Exit(0);
        }

        private static async void DoAsyncTest(PasteMystForm form)
        {
            Console.WriteLine("Testing Post Function (Async)...");
            var output = await PasteMystService.PostAsync(form);
            Console.WriteLine($"Test Completed! VFP: {output.Id} // {output.Language}");
            Console.WriteLine("Testing Get Function (Async)...");
            output = await PasteMystService.GetAsync(output.Id);
            Console.WriteLine($"Test Completed! VFP: {output.Id} // {output.Language}");
            Console.WriteLine("Overall Test Completed! Press Any Key To Exit...");
        }

    }

}