using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NUnit.Framework;

namespace PasteMystNet.Tests
{

    [TestFixture]
    internal static class Operations
    {

        private static PasteMystAuth UserAuth { get; set; }
        private static PasteMystPasteForm TemplateForm { get; set; }

        [SetUp]
        public static void SetupTests()
        {
            TemplateForm = new PasteMystPasteForm
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
        }
        
        [Test]
        public static void PostPasteTest()
        {
            Assert.DoesNotThrowAsync(async () => _ = await TemplateForm.PostPasteAsync());
        }
        
        [Test]
        public static void GetPasteTest()
        {
            Assert.DoesNotThrowAsync(async () => _ = await PasteMystPaste.GetPasteAsync("4jec5of5"));
        }

        [Test]
        public static void PatchPasteTest()
        {
            if (UserAuth is null)
                return;
            Assert.DoesNotThrowAsync(async () =>
            {
                var before = await TemplateForm.PostPasteAsync(UserAuth);
                var edit = before.CreateEditForm();
                edit.Title += " (Edited)";
                _ = await edit.PatchPasteAsync(UserAuth);
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
                await PasteMystPaste.DeletePasteAsync(paste.Id, UserAuth);
                _ = await PasteMystPaste.GetPasteAsync(paste.Id);
            });
        }

        [Test]
        public static async Task LanguageDataTest()
        {
            Assert.IsNotNull(await PasteMystLanguage.IdentifyByExtensionAsync("cs"));
            Assert.IsNotNull(await PasteMystLanguage.IdentifyByNameAsync("C#"));
        }

        [Test]
        public static async Task UserDataTest()
        {
            _ = await PasteMystUser.UserExistsAsync("codemyst");
            Assert.IsNotNull(await PasteMystUser.GetUserAsync("codemyst"));
        }

    }

}