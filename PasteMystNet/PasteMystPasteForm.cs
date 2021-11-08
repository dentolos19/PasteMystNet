using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using PasteMystNet.Core;

namespace PasteMystNet
{

    public class PasteMystPasteForm
    {

        [JsonPropertyName("tags")] private string? _tags; // TODO: ignore null value

        [JsonPropertyName("title")] public string Title { get; set; } = string.Empty;
        [JsonPropertyName("isPrivate")] public bool IsPrivate { get; set; }
        [JsonPropertyName("isPublic")] public bool IsPublic { get; set; }
        [JsonPropertyName("pasties")] public IList<PasteMystPastyForm>? Pasties { get; set; } = new List<PasteMystPastyForm>();
        [JsonPropertyName("expiresIn")] public string ExpireDuration { get; set; } = PasteMystExpirations.Never;
        [JsonIgnore] public IList<string>? Tags { get; set; } = new List<string>();

        public async Task<PasteMystPaste> PostPasteAsync(PasteMystToken? token = null)
        {
            if ((IsPrivate || IsPublic) && token == null)
                throw new ArgumentNullException(nameof(token));
            if (Tags is { Count: > 0 } && token == null)
                throw new ArgumentNullException(nameof(token));
            if (Pasties is not { Count: > 0 })
                throw new Exception($"{nameof(Pasties)} must not be null or empty.");
            foreach (var paste in Pasties)
                if (string.IsNullOrEmpty(paste.Code))
                    throw new Exception($"{nameof(Pasties)}[{Pasties.IndexOf(paste)}] doesn't contain code content.");
            if (Tags != null)
                _tags = string.Join(",", Tags);
            using var client = new HttpClient();
            if (token != null)
                client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", token.ToString());
            var response = await client.PostAsync(Constants.PostPasteEndpoint, new StringContent(JsonSerializer.Serialize(this), Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<PasteMystPaste>(content);
        }

    }

}