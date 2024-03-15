## Getting Started

Firstly, install the library into your project.

- .NET CLI: `dotnet add package PasteMystNet`
- Package Manager CLI: `Install-Package PasteMystNet`

Then, include required namespaces in your file.

```cs
using System.Collections.Generic;
using PasteMystNet;
```

## Posting Paste

```cs
var pasteForm = new PasteMystPasteForm
{
    ExpireDuration = PasteMystExpirations.OneDay, // the paste gets deleted in one day
    Pasties = new List<PasteMystPastyForm>
    {
        new() // first pasty
        {
            Title = "file.txt",
            Code = "This is a test."
        },
        new() // second pasty
        {
            Language = "Python", // specifies the pasty as python code
            Code = "print(\"Hello World\")"
        }
    }
};

var paste = await pasteForm.PostPasteAsync(); // posts the paste into their database
var paste = await pasteForm.PostPasteAsync(new PasteMystToken("<TOKEN>")); // does the same thing but assigns the paste to a user with specified token
```

## Editing Paste

This function can only be used when dealing with pastes with owners.

```cs
var token = new PasteMystToken("<TOKEN>");
var paste = await PasteMystPaste.GetPasteAsync("<ID>", token); // gets an owned paste with the specified id; requires access with a token

var pasteForm = postResult.CreateEditForm(); // creates a form for editing the paste
pasteForm.Title += " (Edited)"; // edits the title
pasteForm.Pasties[0].Code += " This is an edit."; // edits the paste's pasty content
pasteForm.IsPublic = true; // sets the paste to be displayed in the user's profile
pasteForm.Tags.Add("edited"); // adds a new tag

var editedPaste = await form.PatchPasteAsync(token); // patches the new paste into their database; editing the old one
```

## Getting Paste

```cs
var paste = await PasteMystPaste.GetPasteAsync("<ID>"); // gets the paste with the specified id
var paste = await PasteMystPaste.GetPasteAsync("<ID>", new PasteMystToken("<TOKEN>")); // gets an owned paste with the specified id; requires access with a token
```

## Deleting Paste

This function can only be used when dealing with pastes with owners.

```cs
await PasteMystPaste.DeletePasteAsync("<ID>", new PasteMystToken("<TOKEN>")); // deletes the paste with the specified id; requires access with a token
```

## Getting Language Info

```cs
var languageInfo = await PasteMystLanguage.GetLanguageByNameAsync("C#"); // gets info about the language with the specified name
var languageInfo = await PasteMystLanguage.GetLanguageByExtensionAsync("cs"); // gets info about the language with the specified extension
```

## Getting User Info

```cs
var userExists = await PasteMystUser.UserExistsAsync("codemyst"); // checks whether the user exists in their database

var userInfo = await PasteMystUser.GetUserAsync("codemyst"); // gets info about the user with the specified username

var token = new PasteMystToken("<TOKEN>");
var userInfo = await PasteMystUser.GetUserAsync(token); // gets info about the user with the specified token
var userPastes = await PasteMystUser.GetUserPastesAsync(token); // gets the user's pastes with the specified token
```