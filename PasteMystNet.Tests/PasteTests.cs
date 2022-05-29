using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NUnit.Framework;

namespace PasteMystNet.Tests;

public class PasteTests
{

    [TestCase("b0zis5k8")]
    public async Task GetPasteTest(string id)
    {
        var paste = await PasteMystPaste.GetPasteAsync(id);
        Assert.IsNotNull(paste);
        var dump = ObjectDumper.Dump(paste);
        Console.WriteLine(dump);
    }

    [Test]
    public async Task PostPasteTest()
    {
        var pasteForm = new PasteMystPasteForm
        {
            Title = "Hello World",
            ExpireDuration = PasteMystExpirations.OneDay,
            Pasties = new List<PasteMystPastyForm>
            {
                new()
                {
                    Code = "This is a test."
                },
                new()
                {
                    Language = "Python",
                    Code = "print(\"Hello World\")"
                }
            }
        };
        var paste = await pasteForm.PostPasteAsync();
        Assert.IsNotNull(paste);
        var dump = ObjectDumper.Dump(paste);
        Console.WriteLine(dump);
    }

    [TestCase("vayHs/5xpELIybjpfB2uJ7xLU1JNaWfrJksIC/nxev8=")]
    public async Task PostPrivatePasteTest(string token)
    {
        var pasteForm = new PasteMystPasteForm
        {
            Title = "Hello World",
            ExpireDuration = PasteMystExpirations.OneDay,
            Pasties = new List<PasteMystPastyForm>
            {
                new()
                {
                    Title = "file.txt",
                    Code = "This is a test."
                },
                new()
                {
                    Title = "script.py",
                    Code = "print(\"Hello World\")"
                }
            },
            Tags = new List<string>
            {
                "test",
                "python",
                "basic"
            }
        };
        var paste = await pasteForm.PostPasteAsync(new PasteMystToken(token));
        Assert.IsNotNull(paste);
        var dump = ObjectDumper.Dump(paste);
        Console.WriteLine(dump);
    }

    [TestCase("vayHs/5xpELIybjpfB2uJ7xLU1JNaWfrJksIC/nxev8=")]
    public async Task PatchPasteTest(string token)
    {
        var userToken = new PasteMystToken(token);
        var pasteForm = new PasteMystPasteForm
        {
            Title = "Hello World",
            ExpireDuration = PasteMystExpirations.OneDay,
            Pasties = new List<PasteMystPastyForm>
            {
                new()
                {
                    Title = "file.txt",
                    Code = "This is a test."
                },
                new()
                {
                    Title = "script.py",
                    Code = "print(\"Hello World\")"
                }
            },
            Tags = new List<string>
            {
                "test",
                "python",
                "basic"
            }
        };
        var paste = await pasteForm.PostPasteAsync(userToken);
        Assert.IsNotNull(paste);
        var editForm = paste.CreateEditForm();
        editForm.Title += " (Edited)";
        editForm.Pasties[0].Title = "file_edited.txt";
        editForm.Pasties[0].Code += " This file has been edited!";
        // editForm.Pasties[1].Language = "Python";
        editForm.Tags.Add("edited");
        paste = await editForm.PatchPasteAsync(userToken);
        Assert.IsNotNull(paste);
        var dump = ObjectDumper.Dump(paste);
        Console.WriteLine(dump);
    }

    [TestCase("vayHs/5xpELIybjpfB2uJ7xLU1JNaWfrJksIC/nxev8=")]
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
        await PasteMystPaste.DeletePasteAsync(paste.Id, userToken);
    }

}