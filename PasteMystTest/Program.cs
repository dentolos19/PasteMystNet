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
                        Code = "Hello World"
                    },
                    new PasteMystPastyForm
                    {
                        Title = "Library.txt",
                        Code = "Powered By PasteMyst.NET"
                    }
                }
            };

            PasteMystPaste postResult;
            PasteMystPaste getResult;

            #region Posting Paste

            try
            {
                Console.WriteLine("Posting paste to server...");
                postResult = paste.PostPasteAsync(auth).Result;
                Console.WriteLine("Posted paste to server!");
                // Console.WriteLine(ObjectDumper.Dump(postResult));
            }
            catch (Exception error) // Returns exception if operation fails
            {
                Console.WriteLine($"An error had occurred: {error.Message}");
                goto End;
            }

            #endregion

            #region Getting Paste

            try
            {
                Console.WriteLine("Retrieving paste info from server...");
                getResult = PasteMystPaste.GetPasteAsync(postResult.Id, auth).Result;
                Console.WriteLine("Retrieved paste info from server!");
                // Console.WriteLine(ObjectDumper.Dump(getResult));
            }
            catch (Exception error) // Returns exception if operation fails
            {
                Console.WriteLine($"An error had occurred: {error.Message}");
                goto End;
            }

            #endregion

            #region Deleting Paste

            try
            {
                if (auth != null)
                {
                    Console.WriteLine("Deleting Paste...");
                    _ = PasteMystPaste.DeletePasteAsync(getResult.Id, auth);
                    Console.WriteLine("Deleted paste from server!");
                }
            }
            catch (Exception error) // Returns exception if operation fails
            {
                Console.WriteLine($"An error had occurred: {error.Message}");
                goto End;
            }

            #endregion

            #region Identifying Languages

            Console.WriteLine("Retrieving language info from server...");
            var langResult = PasteMystLanguage.IdentifyByExtensionAsync("cs").Result;
            if (langResult == null) // Returns null if operation fails
            {
                Console.WriteLine("Language does not exist on server!");
                goto End;
            }
            Console.WriteLine("Retrieved language info from server!");
            // Console.WriteLine(ObjectDumper.Dump(langResult));

            #endregion

            #region Getting Users

            Console.WriteLine("Retrieving user info from server...");
            var userResult = PasteMystUser.GetUserAsync("virgincode").Result;
            if (userResult == null) // Returns null if operation fails
            {
                Console.WriteLine("User does not exist on server!");
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