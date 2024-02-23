using DotNetEnv;

namespace PasteMystNet.Tests;

public class AuthenticatedTests
{
    private PasteMystClient Client { get; set; } = null!;

    [SetUp]
    public void Setup()
    {
        Env.Load();
        var token = Environment.GetEnvironmentVariable("AUTH_TOKEN");
        Client = new PasteMystClient(token);
    }

    [TearDown]
    public void TearDown()
    {
        Client.Dispose();
    }

    [Test]
    public async Task GetCurrentUserTest()
    {
        _ = await Client.GetCurrentUserAsync();
    }

    [Test]
    public async Task GetCurrentUserPastesTest()
    {
        _ = await Client.GetCurrentUsersPastesAsync();
    }
}