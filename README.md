<h1>
    <img src="docs/icon.png" style="height: 1em;"/>
    <span>PasteMyst.NET</span>
    <a href="https://nuget.org/packages/PasteMystNet">
      <img src="https://img.shields.io/nuget/v/PasteMystNet?label=NuGet&logo=nuget&style=flat-square"/>
    </a>
</h1>

A simple .NET API wrapper for [PasteMyst](https://paste.myst.rs)!

## ⚒️ Usage

Install the library in your project.

- .NET CLI: `dotnet add package PasteMystNet`
- Package Manager CLI: `Install-Package PasteMystNet`

```cs
using System.Collections.Generic;
using PasteMystNet;

var pasteForm = new PasteMystPasteForm
{
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
            Language = "Python",
            Code = "print(\"Hello World\")"
        }
    }
};

var paste = await pasteForm.PostPasteAsync();
```

For more examples for using this library, visit the [usage wiki](https://github.com/dentolos19/PasteMystNet/wiki/Usages) or check out the [unit tests](./PasteMystNet.Tests).