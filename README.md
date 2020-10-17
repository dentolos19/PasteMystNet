<h1 align="center">
  <br>
    <img src="./.github/icon.png" width="200">
  <br>
    PasteMyst.NET
  <br>
</h1>

<p align="center">
  <a>
    <img src="https://badgen.net/badge/icon/Cross-Platform%20%7C%20.NET%20Standard%202.0?icon=terminal&label">
  </a>
  <a>
    <img src="https://badgen.net/badge/icon/Visual%20Studio%202019?icon=visualstudio&label">
  </a>
</p>

A simple API wrapper for [PasteMyst](https://paste.myst.rs)! It is really simple to use and works with almost any platform, get started using this library by following the instructions below.

First of all, install the library on your project by entering this command into your package manager console, or just search it in the package manager.

```Install-Package PasteMystNet```

Follow the code snippet below. There's comments of what different functions or properties do.

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
Process.Start(Response.Link); // Open posted file in browser
```
