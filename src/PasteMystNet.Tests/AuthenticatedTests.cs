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
    public async Task EditPasteTest()
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
        var editForm = new PasteMystEditForm(paste);
        editForm.Title += " (edited)";
        editForm.Tags.Add("edited");
        editForm.Pasties[0].Content += " I've been edited!";
        // TODO: fix the issue with adding a new pasty
        var editedPaste = await Client.EditPasteAsync(editForm);
        Console.WriteLine(ObjectDumper.Dump(editedPaste));
        Assert.Multiple(() =>
        {
            Assert.That(editedPaste.Title, Is.EqualTo(editForm.Title));
            Assert.That(editedPaste.Tags, Is.EquivalentTo(editForm.Tags));
            Assert.That(editedPaste.Pasties, Has.Count.EqualTo(editForm.Pasties.Count));
        });
    }
    
    [Test]
    public async Task DeletePasteTest()
    {
        var paste = await Client.CreatePasteAsync(new PasteMystPasteForm
        {
            Pasties =
            [
                new PasteMystPastyForm
                {
                    Content = "Hello, word!"
                }
            ]
        });
        await Client.DeletePasteAsync(paste.Id);
        Assert.That(async () => await Client.GetPasteAsync(paste.Id), Throws.InstanceOf<PasteMystException>());
    }
}