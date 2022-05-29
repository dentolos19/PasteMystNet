using System;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;

namespace PasteMystNet.Tests;

public class DataTests
{

    // [TestCase("C#")]
    // [TestCase("C++")]
    [TestCase("JavaScript")]
    public async Task GetLanguageByNameTest(string name)
    {
        var language = await PasteMystLanguage.GetLanguageByNameAsync(name);
        Console.WriteLine(ObjectDumper.Dump(language));
        if (language.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
            Assert.Pass();
        Assert.Fail();
    }

    [TestCase("cs")]
    [TestCase("cpp")]
    [TestCase("js")]
    public async Task GetLanguageByExtensionTest(string extension)
    {
        var language = await PasteMystLanguage.GetLanguageByExtensionAsync(extension);
        Console.WriteLine(ObjectDumper.Dump(language));
        Assert.Contains(extension, language.Extensions);
    }

    [Test]
    public async Task GetTotalActivePastesTest()
    {
        var count = await PasteMystPaste.GetTotalActivePastesAsync();
        Console.WriteLine(count);
        Assert.Pass();
    }

}