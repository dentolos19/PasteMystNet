using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace PasteMystNet.Tests
{

    public class DataTests
    {

        [TestCase("C#")]
        [TestCase("C++")]
        [TestCase("JavaScript")]
        public async Task GetLanguageByNameTest(string name)
        {
            var language = await PasteMystLanguage.GetLanguageByNameAsync(name);
            Assert.IsNotNull(language);
            var dump = ObjectDumper.Dump(language);
            Console.WriteLine(dump);
        }

        [TestCase("cs")]
        [TestCase("cpp")]
        [TestCase("js")]
        public async Task GetLanguageByExtensionTest(string extension)
        {
            var language = await PasteMystLanguage.GetLanguageByExtensionAsync(extension);
            Assert.IsNotNull(language);
            var dump = ObjectDumper.Dump(language);
            Console.WriteLine(dump);
        }

    }

}