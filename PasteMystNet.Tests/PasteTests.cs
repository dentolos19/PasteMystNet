using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PasteMystNet.Tests
{

    [TestClass]
    public class PasteTests
    {

        [TestMethod]
        [DataTestMethod]
        [DataRow("b0zis5k8")]
        public async Task GetPasteTest(string id)
        {
            var paste = await PasteMystPaste.GetPasteAsync(id);
            Assert.IsNotNull(paste);
            var dump = ObjectDumper.Dump(paste);
            Console.WriteLine(dump);
        }

        [TestMethod]
        public async Task PostPasteTest()
        {
            var pasteForm = new PasteMystPasteForm
            {
                ExpireDuration = PasteMystExpirations.OneDay,
                Pasties = new List<PasteMystPastyForm>
                {
                    new()
                    {
                        Code = "Test 1"
                    },
                    new()
                    {
                        Code = "Test 2"
                    }
                }
            };
            var paste = await pasteForm.PostPasteAsync();
            Assert.IsNotNull(paste);
            var dump = ObjectDumper.Dump(paste);
            Console.WriteLine(dump);
        }

        [TestMethod]
        [DataTestMethod]
        [DataRow("vayHs/5xpELIybjpfB2uJ7xLU1JNaWfrJksIC/nxev8=")]
        public async Task PostPrivatePasteTest(string token)
        {
            var pasteForm = new PasteMystPasteForm
            {
                ExpireDuration = PasteMystExpirations.OneDay,
                Pasties = new List<PasteMystPastyForm>
                {
                    new()
                    {
                        Code = "Test 1"
                    },
                    new()
                    {
                        Code = "Test 2"
                    }
                },
                Tags = new List<string>
                {
                    "test"
                }
            };
            var paste = await pasteForm.PostPasteAsync(new PasteMystToken(token));
            Assert.IsNotNull(paste);
            var dump = ObjectDumper.Dump(paste);
            Console.WriteLine(dump);
        }

        [TestMethod]
        [DataTestMethod]
        [DataRow("vayHs/5xpELIybjpfB2uJ7xLU1JNaWfrJksIC/nxev8=")]
        public async Task DeletePasteTest(string token)
        {
            var userToken = new PasteMystToken(token);
            var pasteForm = new PasteMystPasteForm
            {
                ExpireDuration = PasteMystExpirations.OneDay,
                Pasties = new List<PasteMystPastyForm>
                {
                    new()
                    {
                        Code = "Test"
                    }
                }
            };
            var paste = await pasteForm.PostPasteAsync(userToken);
            Assert.IsNotNull(paste);
            var result = await PasteMystPaste.DeletePasteAsync(paste.Id, userToken);
            Assert.IsTrue(result);
        }

    }

}