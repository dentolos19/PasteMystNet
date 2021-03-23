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
        public static async Task PostPasteTest()
        {
            _ = await TemplateForm.PostPasteAsync();
        }

        [Test]
        public static async Task GetPasteTest()
        {
            _ = await PasteMystPaste.GetPasteAsync("4jec5of5");
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