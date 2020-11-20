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
                    new PasteMystPastyForm // Pasty without title gives untitled document with code content
                    {
                        Language = "Plain Text", // Pasty without syntax highlighting
                        Code = "Hello World!"
                    },
                    new PasteMystPastyForm // Pasty without a language definition will be automatically set to "Autodetect"
                    {
                        Title = "test.py",
                        Code = "def main():" + "\n" +
                               "    print('Hello World')" + "\n" +
                               "\n" +
                               "main()"
                    }
                }
            };

            PasteMystPaste postResult;
            PasteMystPaste editResult;
            PasteMystPaste getResult;

            #region Posting Paste

            Console.WriteLine("===================================");
            Console.WriteLine();

            try
            {
                Console.WriteLine("Posting paste to server...");
                postResult = paste.PostPasteAsync(auth).Result;
                Console.WriteLine("Posted paste to server!");
            }
            catch (Exception error) // Returns exception if operation fails
            {
                Console.WriteLine($"An error had occurred: {error.Message}");
                goto End;
            }

            Console.WriteLine();
            Console.WriteLine(ObjectDumper.Dump(postResult));
            Console.WriteLine();
            Console.WriteLine("==== PRESS ANY KEY TO CONTINUE ====");
            Console.ReadKey();
            Console.WriteLine();

            #endregion

            #region Editing Paste

            if (auth != null)
            {

                Console.WriteLine("===================================");
                Console.WriteLine();

                try
                {
                    var form = postResult.CreateEditForm();
                    form.Title += " (Edited)";
                    form.IsPublic = true;
                    form.Pasties[0].Code += " This is an edit.";
                    Console.WriteLine("Patching paste to server...");
                    editResult = form.PatchPasteAsync(auth).Result;
                    Console.WriteLine("Patched paste to server!");
                }
                catch (Exception error) // Returns exception if operation fails
                {
                    Console.WriteLine($"An error had occurred: {error.Message}");
                    goto End;
                }

                Console.WriteLine();
                Console.WriteLine(ObjectDumper.Dump(editResult));
                Console.WriteLine();
                Console.WriteLine("==== PRESS ANY KEY TO CONTINUE ====");
                Console.ReadKey();
                Console.WriteLine();

            }

            #endregion

            #region Getting Paste

            Console.WriteLine("===================================");
            Console.WriteLine();

            try
            {
                Console.WriteLine("Retrieving paste info from server...");
                getResult = PasteMystPaste.GetPasteAsync(postResult.Id, auth).Result;
                Console.WriteLine("Retrieved paste info from server!");
            }
            catch (Exception error) // Returns exception if operation fails
            {
                Console.WriteLine($"An error had occurred: {error.Message}");
                goto End;
            }

            Console.WriteLine();
            Console.WriteLine(ObjectDumper.Dump(getResult));
            Console.WriteLine();
            Console.WriteLine("==== PRESS ANY KEY TO CONTINUE ====");
            Console.ReadKey();
            Console.WriteLine();

            #endregion

            #region Deleting Paste

            if (auth != null)
            {

                Console.WriteLine("===================================");
                Console.WriteLine();

                try
                {
                    Console.WriteLine("Deleting Paste...");
                    _ = PasteMystPaste.DeletePasteAsync(getResult.Id, auth);
                    Console.WriteLine("Deleted paste from server!");
                }
                catch (Exception error) // Returns exception if operation fails
                {
                    Console.WriteLine($"An error had occurred: {error.Message}");
                    goto End;
                }

                Console.WriteLine();
                Console.WriteLine("==== PRESS ANY KEY TO CONTINUE ====");
                Console.ReadKey();
                Console.WriteLine();

            }

            #endregion

            #region Identifying Languages

            Console.WriteLine("===================================");
            Console.WriteLine();

            Console.WriteLine("Retrieving language info from server...");
            var langResult = PasteMystLanguage.IdentifyByExtensionAsync("cs").Result;
            if (langResult == null) // Returns null if operation fails
            {
                Console.WriteLine("Language does not exist on server!");
                goto End;
            }
            Console.WriteLine("Retrieved language info from server!");

            Console.WriteLine();
            Console.WriteLine(ObjectDumper.Dump(langResult));
            Console.WriteLine();
            Console.WriteLine("==== PRESS ANY KEY TO CONTINUE ====");
            Console.ReadKey();
            Console.WriteLine();

            #endregion

            #region Getting Users

            Console.WriteLine("===================================");
            Console.WriteLine();

            Console.WriteLine("Retrieving user info from server...");
            var userResult = PasteMystUser.GetUserAsync("virgincode").Result;
            if (userResult == null) // Returns null if operation fails
            {
                Console.WriteLine("User does not exist on server!");
                goto End;
            }
            Console.WriteLine("User info retrieved from server!");

            Console.WriteLine();
            Console.WriteLine(ObjectDumper.Dump(userResult));

            #endregion

            End:
            Console.WriteLine();
            Console.WriteLine("====== PRESS ANY KEY TO EXIT ======");
            Console.ReadKey();
            Environment.Exit(Environment.ExitCode);

        }

    }

}