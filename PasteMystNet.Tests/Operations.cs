using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NUnit.Framework;

namespace PasteMystNet.Tests
{

    [TestFixture]
    internal static class Operations
    {

        private static PasteMystAuth UserAuth { get; }
        private static PasteMystPasteForm TemplateForm { get; } = new()
        {
            Title = "PasteMyst.NET",
            ExpireDuration = PasteMystExpirations.OneHour,
            Pasties = new List<PasteMystPastyForm>
            {
                new()
                {
                    Language = "Plain Text",
                    Code = "Hello World!"
                },
                new()
                {
                    Title = "test.py",
                    Code = "def main():" + "\n" +
                           "    print('Hello World')" + "\n" +
                           "\n" +
                           "main()"
                }
            },
        };

        [Test]
        public static void PostPasteTest()
        {
            Assert.DoesNotThrowAsync(async () =>
            {
                var paste = await TemplateForm.PostPasteAsync();
                Assert.IsNotNull(paste);
                Console.WriteLine("=====> PASTE <=====");
                Console.WriteLine(ObjectDumper.Dump(paste));
                Console.WriteLine();
            });
        }
        
        [Test]
        public static void GetPasteTest()
        {
            Assert.DoesNotThrowAsync(async () =>
            {
                var paste = await PasteMystPaste.GetPasteAsync("4jec5of5");
                Assert.IsNotNull(paste);
                Console.WriteLine("=====> PASTE <=====");
                Console.WriteLine(ObjectDumper.Dump(paste));
                Console.WriteLine();
            });
        }

        [Test]
        public static void PatchPasteTest()
        {
            if (UserAuth is null)
                return;
            Assert.DoesNotThrowAsync(async () =>
            {
                var paste = await TemplateForm.PostPasteAsync(UserAuth);
                Assert.IsNotNull(paste);
                Console.WriteLine("=====> PASTE <=====");
                Console.WriteLine(ObjectDumper.Dump(paste));
                Console.WriteLine();
                var edit = paste.CreateEditForm();
                edit.Title += " (Edited)";
                var editedPaste = await edit.PatchPasteAsync(UserAuth);
                Assert.IsNotNull(editedPaste);
                Console.WriteLine("=====> EDITED PASTE <=====");
                Console.WriteLine(ObjectDumper.Dump(editedPaste));
                Console.WriteLine();
            });
        }

        [Test]
        public static void DeletePasteTest()
        {
            if (UserAuth is null)
                return;
            Assert.ThrowsAsync<Exception>(async () =>
            {
                var paste = await TemplateForm.PostPasteAsync(UserAuth);
                Assert.IsNotNull(paste);
                await PasteMystPaste.DeletePasteAsync(paste.Id, UserAuth);
                _ = await PasteMystPaste.GetPasteAsync(paste.Id);
            });
        }

        [Test]
        public static async Task LanguageDataTest()
        {
            var identifyStep1 = await PasteMystLanguage.IdentifyByExtensionAsync("cs");
            Assert.IsNotNull(identifyStep1);
            Console.WriteLine("=====> IDENTIFY STEP 1 <=====");
            Console.WriteLine(ObjectDumper.Dump(identifyStep1));
            Console.WriteLine();
            var identifyStep2 = await PasteMystLanguage.IdentifyByNameAsync("C#");
            Assert.IsNotNull(identifyStep2);
            Console.WriteLine("=====> IDENTIFY STEP 2 <=====");
            Console.WriteLine(ObjectDumper.Dump(identifyStep2));
            Console.WriteLine();
        }

        [Test]
        [TestCase("codemyst")]
        [TestCase("virgincode")]
        public static async Task UserDataTest(string username)
        {
            Assert.IsTrue(await PasteMystUser.UserExistsAsync(username));
            var user = await PasteMystUser.GetUserAsync(username);
            Assert.IsNotNull(user);
            Console.WriteLine("=====> USER <=====");
            Console.WriteLine(ObjectDumper.Dump(user));
            Console.WriteLine();
            
        }

    }

}