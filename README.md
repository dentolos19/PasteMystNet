<h1 align="center">
  <br>
    <img src="./.github/icon.png" width="200">
  <br>
    PasteMyst.NET
  <br>
</h1>

<p align="center">
  <a href="https://dotnet.microsoft.com">
    <img src="https://img.shields.io/badge/Powered%20By-.NET-blue?logo=microsoft&style=for-the-badge">
  </a>
  <a href="https://visualstudio.microsoft.com">
    <img src="https://img.shields.io/badge/Made%20With-Visual%20Studio-blue?logo=visual-studio&style=for-the-badge">
  </a>
  <a href="https://www.nuget.org/packages/PasteMystNet">
    <img src="https://img.shields.io/badge/Available%20On-NuGet-blue?logo=nuget&style=for-the-badge">
  </a>
</p>

A simple API wrapper for [PasteMyst](https://paste.myst.rs)! It is really simple to use and works with almost any platform, get started using this library by following the instructions below.

**PLEASE NOTE**: There will be breaking changes with the release of version 1.1.0 of this library as PasteMyst has updated their API to V2. To use the V1 API, please use the versions before version 1.1.0 of this library.

First of all, install the library on your project by entering this command into your package manager console, or just search it in the package manager.

`Install-Package PasteMystNet`

Follow the code snippet below. There's comments of what different functions or properties do.

```cs
using System.Diagnostics;
using System.IO;
using PasteMystNet; // Before version 1.1.0

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