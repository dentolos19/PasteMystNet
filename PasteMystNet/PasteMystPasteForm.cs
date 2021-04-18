using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PasteMystNet.Internals;

namespace PasteMystNet
{

    /// <summary>
    /// This class is used to post paste to server.
    /// </summary>
    /// <seealso href="https://paste.myst.rs/api-docs/paste"/>
    public class PasteMystPasteForm
    {

        [JsonProperty(PropertyName = "tags", NullValueHandling = NullValueHandling.Ignore)] private string? _tags;

        [JsonProperty(PropertyName = "title", NullValueHandling = NullValueHandling.Ignore)] public string? Title { get; set; }
        [JsonProperty(PropertyName = "isPrivate")] public bool IsPrivate { get; set; }
        [JsonProperty(PropertyName = "isPublic")] public bool IsPublic { get; set; }
        [JsonProperty(PropertyName = "pasties")] public IList<PasteMystPastyForm>? Pasties { get; set; } = new List<PasteMystPastyForm>();
        [JsonProperty(PropertyName = "expiresIn")] public string ExpireDuration { get; set; } = PasteMystExpirations.Never;
        [JsonIgnore] public IList<string>? Tags { get; set; } = new List<string>();

        /// <summary>
        /// Posts paste to server. If you're uploading a paste to your profile, provide <see cref="PasteMystAuth"/> for authorization.
        /// </summary>
        public async Task<PasteMystPaste?> PostPasteAsync(PasteMystAuth? auth = null)
        {
            if ((IsPrivate || IsPublic) && auth == null)
                throw new ArgumentNullException(nameof(auth));
            if (Tags != null && Tags.Count > 0 && auth == null)
                throw new ArgumentNullException(nameof(auth));
            if (Pasties == null || Pasties.Count <= 0)
                throw new Exception($"{nameof(Pasties)} must not be null or empty.");
            foreach (var paste in Pasties)
            {
                var pasteId = $"{nameof(Pasties)}[{Pasties.IndexOf(paste)}]";
                if (string.IsNullOrEmpty(paste.Title))
                    paste.Title = string.Empty;
                if (string.IsNullOrEmpty(paste.Language))
                    paste.Language = "Autodetect";
                if (string.IsNullOrEmpty(paste.Code))
                    throw new Exception($"{pasteId} doesn't contain code content.");
            }
            if (Tags != null)
                _tags = string.Join(",", Tags);
            try
            {
                var data = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(this));
                var request = WebRequest.Create(PasteMystConstants.PostPasteEndpoint);
                request.Method = "POST";
                request.ContentType = "application/json";
                request.ContentLength = data.Length;
                using (var stream = await request.GetRequestStreamAsync())
                    await stream.WriteAsync(data, 0, data.Length);
                if (auth != null)
                    request.Headers.Add("Authorization", auth.Token);
                using var response = await request.GetResponseAsync();
                using var reader = new StreamReader(response.GetResponseStream()!);
                var content = await reader.ReadToEndAsync();
                return JsonConvert.DeserializeObject<PasteMystPaste>(content);
            }
            catch (Exception error)
            {
                switch (error)
                {
                    case WebException webError:
                    {
                        using var reader = new StreamReader(webError.Response.GetResponseStream()!);
                        var content = await reader.ReadToEndAsync();
                        if (string.IsNullOrEmpty(content))
                            throw new Exception("The server returned an exception with unknown reasons.");
                        var response = JsonConvert.DeserializeObject<PasteMystResponse>(content);
                        throw new Exception(response == null ? "The server returned an exception with unknown reasons." : $"The server returned an exception: {response.Message}");
                    }
                    case JsonException jsonError:
                        throw new Exception($"An error occurred during serialization: {jsonError.Message}");
                    default:
                        throw;
                }
            }
        }

    }

}