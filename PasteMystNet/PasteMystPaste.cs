using Newtonsoft.Json;
using PasteMystNet.Internals;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace PasteMystNet
{

    /// <summary>This class is used to get, delete &amp; contain paste information from server.</summary>
    /// <seealso href="https://paste.myst.rs/api-docs/paste"/>
    public class PasteMystPaste
    {

        [JsonProperty(PropertyName = "_id")] public string Id { get; private set; }
        [JsonProperty(PropertyName = "ownerId")] public string? OwnerId { get; private set; }
        [JsonProperty(PropertyName = "title")] public string Title { get; private set; }
        [JsonProperty(PropertyName = "stars")] public int Stars { get; private set; }
        [JsonProperty(PropertyName = "isPrivate")] public bool IsPrivate { get; private set; }
        [JsonProperty(PropertyName = "isPublic")] public bool IsPublic { get; private set; }
        [JsonProperty(PropertyName = "encrypted")] public bool IsEncrypted { get; private set; }
        [JsonProperty(PropertyName = "tags")] public string[]? Tags { get; private set; }
        [JsonProperty(PropertyName = "pasties")] public PasteMystPasty[] Pasties { get; private set; }
        [JsonProperty(PropertyName = "edits")] public PasteMystEdit[]? Edits { get; private set; }
        [JsonProperty(PropertyName = "expiresIn")] public string ExpireDuration { get; private set; }
        [JsonProperty(PropertyName = "createdAt")] public long CreationUnixTime { get; private set; }
        [JsonProperty(PropertyName = "deletesAt")] public long DeletionUnixTime { get; private set; }
        [JsonIgnore] public string Url => Constants.BaseEndpoint + $"/{Id}";
        [JsonIgnore] public bool HasOwner => !string.IsNullOrEmpty(OwnerId);
        [JsonIgnore] public DateTime CreationTime => DateTimeOffset.FromUnixTimeSeconds(CreationUnixTime).DateTime;
        [JsonIgnore] public DateTime? DeletionTime => DeletionUnixTime <= 0 ? null : DateTimeOffset.FromUnixTimeSeconds(DeletionUnixTime).DateTime;
        
        /// <summary>Creates a form for editing the current paste. You can only edit pastes owned by your profile.</summary>
        public PasteMystEditForm CreateEditForm()
        {
            return new(this);
        }

        /// <summary>Retrieves paste from server. If you're accessing a private paste, provide <see cref="PasteMystAuth"/> for authorization.</summary>
        public static async Task<PasteMystPaste?> GetPasteAsync(string id, PasteMystAuth? auth = null)
        {
            try
            {
                var request = WebRequest.Create(string.Format(Constants.GetPasteEndpoint, id));
                request.Method = "GET";
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
                            var response = JsonConvert.DeserializeObject<Response>(content);
                            throw new Exception(response == null ? "The server returned an exception with unknown reasons." : $"The server returned an exception: {response.Message}");
                        }
                    case JsonException jsonError:
                        throw new Exception($"An error occurred during serialization: {jsonError.Message}");
                    default:
                        throw;
                }
            }
        }

        /// <summary>Deletes paste from server. You can only delete pastes owned by your profile.</summary>
        public static async Task DeletePasteAsync(string id, PasteMystAuth auth)
        {
            try
            {
                var request = WebRequest.Create(string.Format(Constants.DeletePasteEndpoint, id));
                request.Method = "DELETE";
                request.Headers.Add("Authorization", auth.Token);
                await request.GetResponseAsync();
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
                            var response = JsonConvert.DeserializeObject<Response>(content);
                            throw new Exception(response == null ? "The server returned an exception with unknown reasons." : $"The server returned an exception: {response.Message}");
                        }
                    default:
                        throw;
                }
            }
        }

        public override string ToString()
        {
            try
            {
                return JsonConvert.SerializeObject(this, Formatting.Indented);
            }
            catch (Exception error)
            {
                switch (error)
                {
                    case JsonException jsonError:
                        throw new Exception($"An error occurred during serialization: {jsonError.Message}");
                    default:
                        throw;
                }
            }
        }

    }

}