# PasteMyst.NET [![Badge](https://app.codacy.com/project/badge/Grade/bf0ae44bdf78494f8e287f29cf65d680)](https://www.codacy.com/manual/dentolos19/PasteMystNet?utm_source=github.com&amp;utm_medium=referral&amp;utm_content=dentolos19/PasteMystNet&amp;utm_campaign=Badge_Grade) [![NuGet](https://img.shields.io/nuget/v/PasteMystNet)](https://www.nuget.org/packages/PasteMystNet)

A simple API wrapper for [PasteMyst](https://paste.myst.rs)! It is really simple to use and works with almost any platform, get started using this library by following the instructions below.

First of all, install the library on your project by entering this command into your package manager console.

```Install-Package PasteMystNet -Version 1.0.0```

Follow the code snippet below, there's small comments of what different functions or properties do.

```cs
using System.Diagnostics;
using System.IO;
using PasteMystNet;

var codeContent = File.ReadAllText("test.java"); // Reads file code content

var form = new PasteMystForm
{
    Code = codeContent, // Your code content
    Expiration = PasteMystExpiration.OneWeek, // Decide when your code expires
    Language = PasteMystLanguage.Java // The language of the code content
};
var response = PasteMystService.Post(form); // Posts to server and retrieve info
Process.Start("https://paste.myst.rs/" + response.Id); // Open posted file in browser
```

This code snippet above should give you an idea of how is it used, I'm too lazy to explain futher but if you have any questions just DM me on Discord, @dennise#6996.