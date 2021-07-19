using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PasteMystNet.Tests
{

    [TestFixture]
    internal static class Operations
    {

        private static PasteMystToken? AuthToken { get; }
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
            }
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
            if (AuthToken is null)
                return;
            Assert.DoesNotThrowAsync(async () =>
            {
                var paste = await TemplateForm.PostPasteAsync(AuthToken);
                Assert.IsNotNull(paste);
                Console.WriteLine("=====> PASTE <=====");
                Console.WriteLine(ObjectDumper.Dump(paste));
                Console.WriteLine();
                var edit = paste.CreateEditForm();
                edit.Title += " (Edited)";
                var editedPaste = await edit.PatchPasteAsync(AuthToken);
                Assert.IsNotNull(editedPaste);
                Console.WriteLine("=====> EDITED PASTE <=====");
                Console.WriteLine(ObjectDumper.Dump(editedPaste));
                Console.WriteLine();
            });
        }

        [Test]
        public static void DeletePasteTest()
        {
            if (AuthToken is null)
                return;
            Assert.ThrowsAsync<Exception>(async () =>
            {
                var paste = await TemplateForm.PostPasteAsync(AuthToken);
                Assert.IsNotNull(paste);
                await PasteMystPaste.DeletePasteAsync(paste.Id, AuthToken);
                _ = await PasteMystPaste.GetPasteAsync(paste.Id);
            });
        }

        [Test]
        public static async Task LanguageDataTest()
        {
            var identifyPart1 = await PasteMystLanguage.GetLanguageByExtensionAsync("cs");
            Assert.IsNotNull(identifyPart1);
            Console.WriteLine("=====> IDENTITY PART 1 <=====");
            Console.WriteLine(ObjectDumper.Dump(identifyPart1));
            Console.WriteLine();
            var identifyPart2 = await PasteMystLanguage.GetLanguageByNameAsync("C#");
            Assert.IsNotNull(identifyPart2);
            Console.WriteLine("=====> IDENTIFY PART 2 <=====");
            Console.WriteLine(ObjectDumper.Dump(identifyPart2));
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
            if (AuthToken is not null)
            {
                var self = await PasteMystUser.GetUserAsync(AuthToken);
                Assert.IsNotNull(user);
                Console.WriteLine("=====> SELF <=====");
                Console.WriteLine(ObjectDumper.Dump(self));
                Console.WriteLine();
                var selfPastes = await PasteMystUser.GetUserPastesAsync(AuthToken);
                Assert.IsNotNull(user);
                Console.WriteLine("=====> SELF PASTES <=====");
                Console.WriteLine(ObjectDumper.Dump(selfPastes));
                Console.WriteLine();
            }
        }

    }

}