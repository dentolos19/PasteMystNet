﻿namespace PasteMystNet.Tests;

public class UserTests
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
    [TestCase("jisidjas", false)]
    [TestCase("codemyst", true)]
    [TestCase("dentolos19", true)]
    public async Task UserExistsTest(string username, bool exists)
    {
        var userExists = await Client.UserExistsAsync(username);
        Assert.That(userExists, Is.EqualTo(exists));
    }

    [Test]
    public async Task GetUserTest()
    {
        var user = await Client.GetUserAsync("codemyst");
        Console.WriteLine(ObjectDumper.Dump(user));
    }
}