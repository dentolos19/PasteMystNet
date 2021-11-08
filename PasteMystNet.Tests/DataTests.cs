using System;
using System.Threading.Tasks;
using NUnit.Framework;

namespace PasteMystNet.Tests
{

    public class DataTests
    {

        [TestCase("C#")]
        public async Task GetLanguageByNameTest(string name)
        {
            var language = await PasteMystLanguage.GetLanguageByNameAsync(name);
            Assert.IsNotNull(language);
            var dump = ObjectDumper.Dump(language);
            Console.WriteLine(dump);
        }

        [TestCase("cs")]
        public async Task GetLanguageByExtensionTest(string extension)
        {
            var language = await PasteMystLanguage.GetLanguageByExtensionAsync(extension);
            Assert.IsNotNull(language);
            var dump = ObjectDumper.Dump(language);
            Console.WriteLine(dump);
        }

    }

}