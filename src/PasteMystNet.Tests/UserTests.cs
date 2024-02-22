using System;
using System.Threading.Tasks;
using NUnit.Framework;

namespace PasteMystNet.Tests;

public class UserTests
{
    [TestCase("codemyst")]
    [TestCase("virgincode")]
    public async Task UserExistsTest(string username)
    {
        var exists = await PasteMystUser.UserExistsAsync(username);
        Assert.IsTrue(exists);
    }

    [TestCase("codemyst")]
    [TestCase("virgincode")]
    public async Task GetUserTest(string username)
    {
        var user = await PasteMystUser.GetUserAsync(username);
        Console.WriteLine(ObjectDumper.Dump(user));
        if (user.Username.Equals(username, StringComparison.OrdinalIgnoreCase))
            Assert.Pass();
        Assert.Fail();
    }

    [TestCase("vayHs/5xpELIybjpfB2uJ7xLU1JNaWfrJksIC/nxev8=")]
    public async Task GetSelfTest(string token)
    {
        var user = await PasteMystUser.GetUserAsync(new PasteMystToken(token));
        Console.WriteLine(ObjectDumper.Dump(user));
        Assert.Pass();
    }

    [TestCase("vayHs/5xpELIybjpfB2uJ7xLU1JNaWfrJksIC/nxev8=")]
    public async Task GetSelfPastesTest(string token)
    {
        var pastes = await PasteMystUser.GetUserPastesAsync(new PasteMystToken(token));
        Console.WriteLine(ObjectDumper.Dump(pastes));
        Assert.Pass();
    }
}