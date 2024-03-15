## Editing a paste

Editing a paste is very easy. You need a authorized client to do this.

```cs
var client = new PasteMystClient("<YOUR_TOKEN_HERE>");
```

Get the paste that you want to edit.

```cs
var paste = await client.GetPasteAsync("<PASTE_ID>");
```

Create edit form for that paste and start editing.

```cs
var editForm = new PasteMystEditForm(paste);
editForm.Title += " (edited)";
editForm.Pasties[0].Content += "\n\nI've been edited!";
editForm.Tags.Add("edited");
```

Patch the edits into the API.

```cs
var editedPaste = await client.EditPasteAsync(editForm);
```

You're all done! 😁