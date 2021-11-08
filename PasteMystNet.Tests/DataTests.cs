using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PasteMystNet.Tests
{

    [TestClass]
    public class DataTests
    {

        [TestMethod]
        [DataTestMethod]
        [DataRow("C#")]
        public async Task GetLanguageByNameTest(string name)
        {
            var language = await PasteMystLanguage.GetLanguageByNameAsync(name);
            Assert.IsNotNull(language);
            var dump = ObjectDumper.Dump(language);
            Console.WriteLine(dump);
        }

        [TestMethod]
        [DataTestMethod]
        [DataRow("cs")]
        public async Task GetLanguageByExtensionTest(string extension)
        {
            var language = await PasteMystLanguage.GetLanguageByExtensionAsync(extension);
            Assert.IsNotNull(language);
            var dump = ObjectDumper.Dump(language);
            Console.WriteLine(dump);
        }

    }

}