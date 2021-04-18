using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PasteMystNet.Internals;

namespace PasteMystNet
{

    /// <summary>
    /// This class is used to patch paste to server.
    /// </summary>
    /// <seealso href="https://paste.myst.rs/api-docs/paste"/>
    public class PasteMystEditForm
    {

        [JsonProperty(PropertyName = "tags", NullValueHandling = NullValueHandling.Ignore)] private string? _tags;
        [JsonIgnore] private readonly string _id;

        [JsonProperty(PropertyName = "title")] public string Title { get; set; }
        [JsonProperty(PropertyName = "isPrivate", NullValueHandling = NullValueHandling.Ignore)] public bool? IsPrivate { get; set; }
        [JsonProperty(PropertyName = "isPublic", NullValueHandling = NullValueHandling.Ignore)] public bool? IsPublic { get; set; }
        [JsonProperty(PropertyName = "pasties")] public IList<PasteMystPastyForm>? Pasties { get; set; }
        [JsonIgnore] public IList<string>? Tags { get; set; } = new List<string>();

        internal PasteMystEditForm(PasteMystPaste paste)
        {
            _id = paste.Id;
            Title = paste.Title;
            Pasties = paste.Pasties.Select(pasty => new PasteMystPastyForm
            {
                Id = pasty.Id,
                Title = pasty.Title,
                Language = pasty.Language,
                Code = pasty.Code
            }).ToList();
            if (paste.Tags.Length > 0)
                Tags = paste.Tags.ToList();
        }

        /// <summary>
        /// Patches paste to server.
        /// </summary>
        public async Task<PasteMystPaste?> PatchPasteAsync(PasteMystAuth auth)
        {
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
            Tags ??= new List<string>();
            if (Tags.Count > 0)
                _tags = string.Join(",", Tags);
            try
            {
                var data = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(this));
                var request = WebRequest.Create(string.Format(PasteMystConstants.PatchPasteEndpoint, _id));
                request.Method = "PATCH";
                request.Headers.Add("Authorization", auth.Token);
                request.ContentType = "application/json";
                request.ContentLength = data.Length;
                using (var stream = await request.GetRequestStreamAsync())
                    await stream.WriteAsync(data, 0, data.Length);
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