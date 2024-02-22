namespace PasteMystNet.Tests;

public class DataTests
{
    private PasteMystClient _client = null!;

    [SetUp]
    public void Setup()
    {
        _client = new PasteMystClient();
    }

    [TearDown]
    public void TearDown()
    {
        _client.Dispose();
    }
    
    [Test] // TODO: fix this test
    [TestCase("python", "Python")]
    [TestCase("java", "Java")]
    public async Task GetLanguageByNameTest(string name, string expectedName)
    {
        var language = await _client.GetLanguageByNameAsync("Java");
        Console.WriteLine($"Requested Name: {name}");
        Console.WriteLine($"Response Name: {language.Name}");
        Console.WriteLine($"Expected Name: {expectedName}");
        Assert.That(language.Name, Is.EqualTo(expectedName));
    }
    
    [Test] // TODO: fix this test
    [TestCase("cs", "C#")]
    [TestCase("py", "Python")]
    public async Task GetLanguageByExtensionTest(string extension, string expectedName)
    {
        var language = await _client.GetLanguageByExtensionAsync("cs");
        Console.WriteLine($"Requested Extension: {extension}");
        Console.WriteLine($"Response Name: {language.Name}");
        Console.WriteLine($"Expected Name: {expectedName}");
        Assert.That(language.Name, Is.EqualTo(expectedName));
    }
    
    [Test]
    public async Task GetActivePastesTest()
    {
        var number = await _client.GetActivePastesAsync();
        Console.WriteLine(number);
    }
}