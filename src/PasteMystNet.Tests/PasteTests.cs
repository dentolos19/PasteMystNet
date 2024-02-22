namespace PasteMystNet.Tests;

public class PasteTests
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
    [TestCase("b0zis5k8")]
    public async Task GetPasteTest(string pasteId)
    {
        var paste = await _client.GetPasteAsync(pasteId);
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
        var paste = await _client.CreatePasteAsync(pasteForm);
        Assert.Multiple(() =>
        {
            Assert.That(paste.Title, Is.EqualTo(pasteForm.Title));
            Assert.That(paste.ExpiresIn, Is.EqualTo(pasteForm.ExpiresIn));
            Assert.That(paste.Pasties.Count, Is.EqualTo(pasteForm.Pasties.Count));
        });
    }
}