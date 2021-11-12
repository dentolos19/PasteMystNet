# <img src="./.github/icon.png" width="24"/> PasteMyst.NET [![](https://img.shields.io/nuget/v/PasteMystNet?label=NuGet&logo=nuget&style=flat-square)](https://www.nuget.org/packages/PasteMystNet)

[![](https://img.shields.io/badge/Powered%20By-.NET-blue?logo=microsoft&style=flat-square)](https://dotnet.microsoft.com)
[![](https://img.shields.io/badge/Made%20With-Visual%20Studio-blue?logo=visual-studio&style=flat-square)](https://visualstudio.microsoft.com)

A simple .NET API wrapper for [PasteMyst](https://paste.myst.rs)!

## Usage

Install the library in your project.

* .NET CLI: `dotnet add package PasteMystNet`
* Package Manager CLI: `Install-Package PasteMystNet`

Try using the library in your project.

```cs
using System.Collections.Generic;
using PasteMystNet;

var userToken = new PasteMystToken("<token>"); // token for posting (private) pastes to user's profile
var pasteForm = new PasteMystPasteForm // form for information about the paste to be posted
{
    Title = "My paste!",
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
    },
    Tags = new List<string> // this is only available for private pastes
    {
        "file",
        "python",
        "basic"
    }
}

// var paste = await pasteForm.PostPasteAsync(); // post the paste
var paste = await pasteForm.PostPasteAsync(userToken); // post the (private) paste to user's profile
```

For more examples for using this library, visit the [wiki](https://github.com/dentolos19/PasteMystNet/wiki/Usage) or check out the [unit tests](./PasteMystNet.Tests).