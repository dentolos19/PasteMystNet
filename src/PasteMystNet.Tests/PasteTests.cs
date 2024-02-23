using System.Globalization;

namespace PasteMystNet.Tests;

public class PasteTests
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
    [TestCase("b0zis5k8")]
    public async Task GetPasteTest(string pasteId)
    {
        var paste = await Client.GetPasteAsync(pasteId);
        Assert.That(paste.Id, Is.EqualTo(pasteId));
    }

    [Test]
    public async Task CreatePasteTest()
    {
        var pasteForm = new PasteMystPasteForm
        {
            Title = "Test Paste",
            ExpiresIn = PasteMystExpirations.OneHour,
            Pasties =
            [
                new PasteMystPastyForm
                {
                    Title = "Test Pasty",
                    Content = "Hello, world!",
                }
            ]
        };
        var paste = await Client.CreatePasteAsync(pasteForm);
        Console.WriteLine($"Paste ID: {paste.Id}");
        Console.WriteLine($"Creation At: {paste.CreatedAt} // {paste.CreatedAtTime.ToString(CultureInfo.CurrentCulture)}");
        Console.WriteLine($"Deleted At: {paste.DeletedAt} // {paste.DeletedAtTime.ToString(CultureInfo.CurrentCulture)}");
        Assert.Multiple(() =>
        {
            Assert.That(paste.Title, Is.EqualTo(pasteForm.Title));
            Assert.That(paste.ExpiresIn, Is.EqualTo(pasteForm.ExpiresIn));
            Assert.That(paste.Pasties, Has.Count.EqualTo(pasteForm.Pasties.Count));
        });
    }
}