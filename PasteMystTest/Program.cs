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

            PasteMystAuth auth = null;
            auth = new PasteMystAuth("7eJl8HEfEWo6W9mjPevSZgGlOBLm5AHUBWeide+wnU4=");

            var paste = new PasteMystPasteForm
            {
                Title = "PasteMyst.NET Test",
                ExpireDuration = PasteMystExpiration.OneHour,
                Pasties = new[]
                {
                    new PasteMystPastyForm
                    {
                        Title = "Example.txt",
                        Code = "Hello World!"
                    },
                    new PasteMystPastyForm
                    {
                        Title = "Library.txt",
                        Code = "Powered By PasteMyst.NET"
                    }
                }
            };

            #region Posting Paste

            Console.WriteLine("Posting Paste...");
            var postResult = paste.PostPasteAsync(auth).Result;
            if (postResult == null)
            {
                Console.WriteLine("Unable to post paste to server!");
                goto End;
            }
            Console.WriteLine("Posted paste to server!");
            // Console.WriteLine(ObjectDumper.Dump(postResult));

            #endregion

            #region Getting Paste

            Console.WriteLine("Getting Paste...");
            var getResult = PasteMystPaste.GetPasteAsync(postResult.Id, auth).Result;
            if (getResult == null)
            {
                Console.WriteLine("Unable to get paste from server!");
                goto End;
            }
            Console.WriteLine("Gotten paste to server!");
            // Console.WriteLine(ObjectDumper.Dump(getResult));

            #endregion

            #region Deleting Paste

            if (auth != null)
            {
                Console.WriteLine("Deleting Paste...");
                if (!PasteMystPaste.DeletePasteAsync(getResult.Id, auth).Result)
                {
                    Console.WriteLine("Unable to delete paste from server!");
                    goto End;
                }
                Console.WriteLine("Deleted paste from server!");
            }

            #endregion

            #region Identifying Languages

            // TODO

            #endregion

            #region Getting Users

            // TODO

            #endregion

            End:
            Console.ReadKey();
            Environment.Exit(Environment.ExitCode);

        }

    }

}