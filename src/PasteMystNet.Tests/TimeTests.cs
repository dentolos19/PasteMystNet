namespace PasteMystNet.Tests;

public class TimeTests
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
    [TestCase(1708644974, PasteMystExpirations.OneHour, 1708648574)]
    public async Task ExpiresInToUnixTimeTest(long unixTime, string expiresIn, long expectedUnixTime)
    {
        var resultantUnixTime = await Client.ExpiresInToUnixTimeAsync(unixTime, expiresIn);
        Console.WriteLine($"Unix Time: {unixTime}");
        Console.WriteLine($"Expires In: {expiresIn}");
        Console.WriteLine($"Resultant Unix Time: {resultantUnixTime}");
        Console.WriteLine($"Expected Unix Time: {expectedUnixTime}");
        Assert.That(resultantUnixTime, Is.EqualTo(expectedUnixTime));
    }
}