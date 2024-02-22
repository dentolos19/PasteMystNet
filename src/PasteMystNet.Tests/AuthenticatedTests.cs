using DotNetEnv;

namespace PasteMystNet.Tests;

public class AuthenticatedTests
{
    private PasteMystClient _client = null!;

    [SetUp]
    public void Setup()
    {
        Env.Load();
        var token = Environment.GetEnvironmentVariable("AUTH_TOKEN");
        _client = new PasteMystClient(token);
    }

    [TearDown]
    public void TearDown()
    {
        _client.Dispose();
    }
}