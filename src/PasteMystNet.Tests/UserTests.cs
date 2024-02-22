namespace PasteMystNet.Tests;

public class UserTests
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

    [Test]
    [TestCase("jisidjas", false)]
    [TestCase("codemyst", true)]
    public async Task UserExistsTest(string username, bool exists)
    {
        var userExists = await _client.UserExistsAsync(username);
        Assert.That(userExists, Is.EqualTo(exists));
    }

    [Test]
    public Task GetUserTest()
    {
        Assert.DoesNotThrowAsync(async () => await _client.GetUserAsync("codemyst"));
        return Task.CompletedTask;
    }
}