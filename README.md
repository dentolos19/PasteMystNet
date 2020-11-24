<h1 align="center">
  <br>
    <img src="./.github/icon.png" width="200">
  <br>
    PasteMyst.NET
  <br>
</h1>

<p align="center">
  <a href="https://dotnet.microsoft.com">
    <img src="https://img.shields.io/badge/Created%20For-.NET-blue?logo=microsoft&style=for-the-badge">
  </a>
  <a href="https://visualstudio.microsoft.com">
    <img src="https://img.shields.io/badge/Made%20With-Visual%20Studio-blue?logo=visual-studio&style=for-the-badge">
  </a>
  <a href="https://www.nuget.org/packages/PasteMystNet">
    <img src="https://img.shields.io/badge/Available%20On-NuGet-blue?logo=nuget&style=for-the-badge">
  </a>
</p>

A simple API wrapper for [PasteMyst](https://paste.myst.rs)! It is really simple to use and works with almost any platform, get started using this library by following the instructions below.

First of all, install the library on your project by entering this command into your package manager console, or just search it in the package manager.

`Install-Package PasteMystNet`

Now, follow the instructions below to understand how to use this library in your projects.

**FOR V2 API (VERSION 1.1.0 & BEYOND)**

There will be breaking changes with the release of version 1.1.0 of this library as [PasteMyst](https://paste.myst.rs) has updated their API to V2. To use the V1 API, please use the versions before version 1.1.0 of this library.

- [click here for new wiki usage page](https://github.com/dentolos19/PasteMystNet/wiki/Usage)
- [click here for version 1.1.2 example usage](https://github.com/dentolos19/PasteMystNet/blob/b1ad056965fc6ebb579114d0fdc124181fcb87cb/PasteMystTest/Program.cs)
- [click here for version 1.1.1 example usage](https://github.com/dentolos19/PasteMystNet/blob/b060464761e0e866db8222c8cd0dcb392e56ee5c/PasteMystTest/Program.cs)
- [click here for version 1.1.0 example usage](https://github.com/dentolos19/PasteMystNet/blob/d48a7d633ec4bf8a36180730aad0b7f9372132c8/PasteMystTest/Program.cs)

**FOR V1 API (BEFORE VERSION 1.1.0)**

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
