using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace PasteMystNet.Tests
{

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
            Assert.IsNotNull(user);
            var dump = ObjectDumper.Dump(user);
            Console.WriteLine(dump);
        }

        [TestCase("vayHs/5xpELIybjpfB2uJ7xLU1JNaWfrJksIC/nxev8=")]
        public async Task GetSelfTest(string token)
        {
            var user = await PasteMystUser.GetUserAsync(new PasteMystToken(token));
            Assert.IsNotNull(user);
            var dump = ObjectDumper.Dump(user);
            Console.WriteLine(dump);
        }

        [TestCase("vayHs/5xpELIybjpfB2uJ7xLU1JNaWfrJksIC/nxev8=")]
        public async Task GetSelfPastesTest(string token)
        {
            var pastes = await PasteMystUser.GetUserPastesAsync(new PasteMystToken(token));
            Assert.IsNotNull(pastes);
            var dump = ObjectDumper.Dump(pastes);
            Console.WriteLine(dump);
        }

    }

}