using System;
using System.Collections.Generic;
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

            Selection:

            Console.Clear();
            Console.WriteLine("PasteMyst.NET");
            Console.WriteLine();
            Console.WriteLine("[1] Posting, Editing, Getting & Deleting Paste");
            Console.WriteLine("[2] Identifying Languages");
            Console.WriteLine("[3] Getting Users Info");
            Console.WriteLine();
            Console.Write("> ");
            var input = Console.ReadKey();
            Console.WriteLine();
            Console.WriteLine();
            switch (input.Key)
            {
                case ConsoleKey.D1:
                    goto SelectionOne;
                case ConsoleKey.D2:
                    goto SelectionTwo;
                case ConsoleKey.D3:
                    goto SelectionThree;
                default:
                    goto Selection;
            }

            SelectionOne:

            PasteMystAuth auth = null;
            // auth = new PasteMystAuth("<TOKEN>");

            var paste = new PasteMystPasteForm
            {
                Title = "PasteMyst.NET",
                ExpireDuration = PasteMystExpiration.OneHour,
                Pasties = new List<PasteMystPastyForm>
                {
                    new PasteMystPastyForm
                    {
                        Language = "Plain Text", // Pasty without syntax highlighting
                        Code = "Hello World!"
                    },
                    new PasteMystPastyForm // Pasty without a language specification will be automatically set to "Autodetect"
                    {
                        Title = "test.py",
                        Code = "def main():" + "\n" +
                               "    print('Hello World')" + "\n" +
                               "\n" +
                               "main()"
                    }
                },
            };

            if (auth != null)
            {
                paste.Tags.Add("dotnet");
                paste.Tags.Add("visualstudio");
            }

            #region Posting Paste

            PasteMystPaste postResult;

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

                PasteMystPaste editResult;

                try
                {
                    var form = postResult.CreateEditForm();
                    form.Title += " (Edited)";
                    form.IsPublic = true;
                    form.Pasties[0].Code += " This is an edit.";
                    form.Tags.Add("edited");
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

            PasteMystPaste getResult;

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

            SelectionTwo:

            #region Identifying Languages

            Console.WriteLine("===================================");
            Console.WriteLine();

            Console.WriteLine("Retrieving language info from server...");
            var langResult = PasteMystLanguage.IdentifyByExtensionAsync("cs").Result;
            if (langResult == null) // Returns null if operation fails
            {
                Console.WriteLine("Language does not exist on server! (Identify Via Extension)");
                goto End;
            }
            langResult = PasteMystLanguage.IdentifyByNameAsync(langResult.Name).Result;
            if (langResult == null) // Returns null if operation fails
            {
                Console.WriteLine("Language does not exist on server! (Identify Via Name)");
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

            SelectionThree:

            #region Getting Users Info

            Console.WriteLine("===================================");
            Console.WriteLine();

            Console.WriteLine("Retrieving user info from server...");
            var userExists = PasteMystUser.UserExistsAsync("virgincode").Result;
            if (!userExists)
            {
                Console.WriteLine("User does not exists on server!");
                goto End;
            }
            var userResult = PasteMystUser.GetUserAsync("virgincode").Result;
            if (userResult == null) // Returns null if operation fails
            {
                Console.WriteLine("User can't be retrieved from server!");
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