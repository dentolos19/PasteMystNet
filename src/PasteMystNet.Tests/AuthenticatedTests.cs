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
        var user = await Client.GetCurrentUserAsync();
        Console.WriteLine(ObjectDumper.Dump(user));
    }

    [Test]
    public async Task GetCurrentUserPastesTest()
    {
        var pastes = await Client.GetCurrentUsersPastesAsync();
        Console.WriteLine(ObjectDumper.Dump(pastes));
    }
    
    [Test]
    public async Task CreatePasteTest()
    {
        var pasteForm = new PasteMystPasteForm
        {
            Title = "PasteMyst.NET Temporary Paste",
            IsPrivate = true,
            ExpiresIn = PasteMystExpirations.OneHour,
            Tags = [
                "unit tests"
            ],
            Pasties = [
                new PasteMystPastyForm
                {
                    Content = "Hello, world!"
                }
            ]
        };
        var paste = await Client.CreatePasteAsync(pasteForm);
        Console.WriteLine(ObjectDumper.Dump(paste));
        Assert.Multiple(() =>
        {
            Assert.That(paste.Title, Is.EqualTo(pasteForm.Title));
            Assert.That(paste.IsPrivate, Is.EqualTo(pasteForm.IsPrivate));
            Assert.That(paste.ExpiresIn, Is.EqualTo(pasteForm.ExpiresIn));
            Assert.That(paste.Tags, Is.EquivalentTo(pasteForm.Tags));
            Assert.That(paste.Pasties, Has.Count.EqualTo(pasteForm.Pasties.Count));
        });
    }

    [Test]
    public Task EditPasteTest()
    {
        // TODO: implement this test
        return Task.CompletedTask;
    }
    
    [Test]
    public Task DeletePasteTest()
    {
        // TODO: implement this test
        return Task.CompletedTask;
    }
}