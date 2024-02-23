using System.Text;
using System.Text.Json;

namespace PasteMystNet;

public partial class PasteMystClient
{
    public async Task<PasteMystPaste> GetPasteAsync(string id)
    {
        var response = await _httpClient.GetAsync($"paste/{id}");
        await PasteMystUtils.ValidateResponseAsync(response);
        return JsonSerializer.Deserialize<PasteMystPaste>(await response.Content.ReadAsStringAsync())!;
    }

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

    public async Task EditPasteAsync(PasteMystEditForm form)
    {
        var response = await _httpClient.PutAsync
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
    }
    
    public async Task DeletePasteAsync(string id)
    {
        var response = await _httpClient.DeleteAsync($"paste/{id}");
        await PasteMystUtils.ValidateResponseAsync(response);
    }
}