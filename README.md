<h1>
    <img src="public/icon.png" style="height: 1em"/>
    <span>PasteMyst.NET</span>
    <a href="https://nuget.org/packages/PasteMystNet">
      <img src="https://img.shields.io/nuget/v/PasteMystNet?label=NuGet&logo=nuget&style=flat-square"/>
    </a>
</h1>

A simple .NET API wrapper for [PasteMyst](https://paste.myst.rs)!

## ⚒️ Usage

### Prerequisities

- .NET Standard 2.1 [compatible framework](https://learn.microsoft.com/dotnet/standard/net-standard?tabs=net-standard-2-1#select-net-standard-version)
- The package itself: `dotnet add package PasteMystNet`

### Example

```cs
using System.Collections.Generic;
using PasteMystNet;

var client = new PasteMystClient();
var pasteForm = new PasteMystPasteForm
{
    ExpiresIn = PasteMystExpirations.OneDay,
    Pasties =
    [
        new PasteMystPastyForm
        {
            Title = "file.txt",
            Content = "This is a test."
        },
        new PasteMystPastyForm
        {
            Language = "Python",
            Content = "print(\"Hello World\")"
        }
    ]
};

var paste = await client.CreatePasteAsync(pasteForm);
```

For more examples for using this library, visit the [usage wiki](https://github.com/dentolos19/PasteMystNet/wiki/Usages) or check out the [unit tests](./PasteMystNet.Tests).

## 📜 License

Distributed under the MIT License. See [LICENSE](LICENSE) for more information.