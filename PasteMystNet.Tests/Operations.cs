using System.Collections.Generic;
using System.Threading.Tasks;
using NUnit.Framework;

namespace PasteMystNet.Tests
{

    [TestFixture]
    internal static class Operations
    {

        private static PasteMystPasteForm TemplateForm { get; set; }

        [SetUp]
        public static void SetupTests()
        {
            TemplateForm = new PasteMystPasteForm
            {
                Title = "PasteMyst.NET",
                ExpireDuration = PasteMystExpiration.OneHour,
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
        public static async Task TestPastePoster()
        {
            _ = await TemplateForm.PostPasteAsync();
        }

        [Test]
        public static async Task TestPasteGetter()
        {
            _ = await PasteMystPaste.GetPasteAsync("4jec5of5");
        }

        [Test]
        public static async Task TestLanguageIdentifier()
        {
            _ = await PasteMystLanguage.IdentifyByExtensionAsync("cs");
            _ = await PasteMystLanguage.IdentifyByNameAsync("C#");
        }

        [Test]
        public static async Task TestUserGetter()
        {
            _ = await PasteMystUser.UserExistsAsync("codemyst");
            _ = await PasteMystUser.GetUserAsync("codemyst");
        }

    }

}