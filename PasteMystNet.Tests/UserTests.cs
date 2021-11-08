using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PasteMystNet.Tests
{

    [TestClass]
    public class UserTests
    {

        [TestMethod]
        [DataTestMethod]
        [DataRow("codemyst")]
        [DataRow("virgincode")]
        public async Task UserExistsTest(string username)
        {
            var exists = await PasteMystUser.UserExistsAsync(username);
            Assert.IsTrue(exists);
        }

        [TestMethod]
        [DataTestMethod]
        [DataRow("codemyst")]
        [DataRow("virgincode")]
        public async Task GetUserTest(string username)
        {
            var user = await PasteMystUser.GetUserAsync(username);
            Assert.IsNotNull(user);
            var dump = ObjectDumper.Dump(user);
            Console.WriteLine(dump);
        }

        [TestMethod]
        [DataTestMethod]
        [DataRow("vayHs/5xpELIybjpfB2uJ7xLU1JNaWfrJksIC/nxev8=")]
        public async Task GetSelfTest(string token)
        {
            var user = await PasteMystUser.GetUserAsync(new PasteMystToken(token));
            Assert.IsNotNull(user);
            var dump = ObjectDumper.Dump(user);
            Console.WriteLine(dump);
        }

        [TestMethod]
        [DataTestMethod]
        [DataRow("vayHs/5xpELIybjpfB2uJ7xLU1JNaWfrJksIC/nxev8=")]
        public async Task GetSelfPastesTest(string token)
        {
            var pastes = await PasteMystUser.GetUserPastesAsync(new PasteMystToken(token));
            Assert.IsNotNull(pastes);
            var dump = ObjectDumper.Dump(pastes);
            Console.WriteLine(dump);
        }

    }

}