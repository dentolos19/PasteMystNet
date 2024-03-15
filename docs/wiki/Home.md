Welcome to **PasteMyst.NET**'s wiki!

## Getting Started

> **Note**: You need to have a [.NET Standard 2.1 compatible framework](https://learn.microsoft.com/dotnet/standard/net-standard?tabs=net-standard-2-1#select-net-standard-version) in order to be able to use this in your project!

Install the package inside your project via CLI or the package manager.

```
dotnet add package PasteMystNet
```

Then, inside your file, import the required namespaces.

```cs
using PasteMystNet;
```

Create the client. And if you have a token for your account, you may also input them into the constructor.

```cs
var client = new PasteMystClient();
var authorizedClient = new PasteMystClient("<YOUR_TOKEN_HERE>");
```

Create a form for a new paste.

```cs
var pasteForm = new PasteMystPasteForm
{
    Title = "My Paste",
    Pasties =
    [
        new PasteMystPastyForm
        {
            Content = "Hello, world!"
        }
    ]
};
```

Then, post the paste to the API.

```cs
var paste = await client.CreatePasteAsync(pasteForm);
Console.WriteLine("Paste URL: https://paste.myst.rs/" + paste.Id);
```

You're all set! 😁 If you want to learn more, check out [the tutorials page](./Tutorials).