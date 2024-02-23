using System.Text;
using System.Text.Json;

namespace PasteMystNet;

public partial class PasteMystClient
{
    /// <summary>
    /// Gets the paste by it's ID. Requires authorization if you're accessing a private paste.
    /// </summary>
    public async Task<PasteMystPaste> GetPasteAsync(string id)
    {
        var response = await _httpClient.GetAsync($"paste/{id}");
        await PasteMystUtils.ValidateResponseAsync(response);
        return JsonSerializer.Deserialize<PasteMystPaste>(await response.Content.ReadAsStringAsync())!;
    }

    /// <summary>
    /// Creates a new paste.
    /// </summary>
    public async Task<PasteMystPaste> CreatePasteAsync(PasteMystPasteForm form)
    {
        var response = await _httpClient.PostAsync
        (
            "paste",
            new StringContent
            (
                form.ToJson().ToString(),
                Encoding.UTF8,
                "application/json"
            )
        );
        await PasteMystUtils.ValidateResponseAsync(response);
        return JsonSerializer.Deserialize<PasteMystPaste>(await response.Content.ReadAsStringAsync())!;
    }

    /// <summary>
    /// Edits a paste. Requires authorization.
    /// </summary>
    public async Task<PasteMystPaste> EditPasteAsync(PasteMystEditForm form)
    {
        var response = await _httpClient.PatchAsync
        (
            $"paste/{form.Id}",
            new StringContent
            (
                form.ToJson().ToString(),
                Encoding.UTF8,
                "application/json"
            )
        );
        await PasteMystUtils.ValidateResponseAsync(response);
        return JsonSerializer.Deserialize<PasteMystPaste>(await response.Content.ReadAsStringAsync())!;
    }

    /// <summary>
    /// Deletes a paste. Requires authorization. Requires authorization.
    /// </summary>
    public async Task DeletePasteAsync(string id)
    {
        var response = await _httpClient.DeleteAsync($"paste/{id}");
        await PasteMystUtils.ValidateResponseAsync(response);
    }
}