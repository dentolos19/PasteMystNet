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
            // auth = new PasteMystAuth("<token>");

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

            Console.WriteLine("Posting paste to server...");
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

            Console.WriteLine("Retrieving paste info from server...");
            var getResult = PasteMystPaste.GetPasteAsync(postResult.Id, auth).Result;
            if (getResult == null)
            {
                Console.WriteLine("Unable to get paste info from server!");
                goto End;
            }
            Console.WriteLine("Retrieved paste info from server!");
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

            Console.WriteLine("Retrieving language info from server...");
            var langResult = PasteMystLanguage.IdentifyByExtensionAsync("cs").Result;
            if (langResult == null)
            {
                Console.WriteLine("Unable to retrieve language info from server!");
                goto End;
            }
            Console.WriteLine("Retrieved language info from server!");
            // Console.WriteLine(ObjectDumper.Dump(langResult));

            #endregion

            #region Getting Users

            Console.WriteLine("Retrieving user info from server...");
            var userResult = PasteMystUser.GetUserAsync("virgincode").Result;
            if (userResult == null)
            {
                Console.WriteLine("Unable to retrieve user info from server!");
                goto End;
            }
            Console.WriteLine("User info retrieved from server!");
            // Console.WriteLine(ObjectDumper.Dump(userResult));

        #endregion

        End:
            Console.ReadKey();
            Environment.Exit(Environment.ExitCode);

        }

    }

}