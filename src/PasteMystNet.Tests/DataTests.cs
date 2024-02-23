namespace PasteMystNet.Tests;

public class DataTests
{
    private PasteMystClient Client { get; set; } = null!;

    [SetUp]
    public void Setup()
    {
        Client = new PasteMystClient();
    }

    [TearDown]
    public void TearDown()
    {
        Client.Dispose();
    }

    [Test]
    [TestCase("python", "Python")]
    [TestCase("java", "Java")]
    [TestCase("c#", "C#")]
    public async Task GetLanguageByNameTest(string name, string expectedName)
    {
        var language = await Client.GetLanguageByNameAsync(name);
        Console.WriteLine($"Requested Name: {name}");
        Console.WriteLine($"Response Name: {language.Name}");
        Console.WriteLine($"Expected Name: {expectedName}");
        Console.WriteLine();
        Console.WriteLine(ObjectDumper.Dump(language));
        Assert.That(language.Name, Is.EqualTo(expectedName));
    }

    [Test]
    [TestCase("cs", "C#")]
    [TestCase("py", "Python")]
    public async Task GetLanguageByExtensionTest(string extension, string expectedName)
    {
        var language = await Client.GetLanguageByExtensionAsync(extension);
        Console.WriteLine($"Requested Extension: {extension}");
        Console.WriteLine($"Response Name: {language.Name}");
        Console.WriteLine($"Expected Name: {expectedName}");
        Console.WriteLine();
        Console.WriteLine(ObjectDumper.Dump(language));
        Assert.That(language.Name, Is.EqualTo(expectedName));
    }

    [Test]
    public async Task GetActivePastesTest()
    {
        var activePastes = await Client.GetActivePastesAsync();
        Console.WriteLine("Active Pastes: " + activePastes);
        Assert.That(activePastes, Is.GreaterThan(0));
    }
}