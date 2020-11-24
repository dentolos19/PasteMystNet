﻿using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PasteMystNet.Internals;

namespace PasteMystNet
{

    public class PasteMystPaste
    {

        private const string GetPasteEndpoint = "https://paste.myst.rs/api/v2/paste/{0}";
        private const string DeletePasteEndpoint = "https://paste.myst.rs/api/v2/paste/{0}";

        [JsonProperty(PropertyName = "createdAt")] private readonly long _createdAt;
        [JsonProperty(PropertyName = "expiresIn")] private readonly string _expiresIn;
        [JsonProperty(PropertyName = "deletesAt")] private readonly long _deletesAt;

        [JsonProperty(PropertyName = "_id")] public string Id { get; private set; }
        [JsonProperty(PropertyName = "ownerId")] public string OwnerId { get; private set; }
        [JsonProperty(PropertyName = "title")] public string Title { get; private set; }
        [JsonProperty(PropertyName = "stars")] public uint Stars { get; private set; }
        [JsonProperty(PropertyName = "isPrivate")] public bool IsPrivate { get; private set; }
        [JsonProperty(PropertyName = "isPublic")] public bool IsPublic { get; private set; }
        [JsonProperty(PropertyName = "encrypted")] public bool IsEncrypted { get; private set; }
        [JsonProperty(PropertyName = "tags")] public string[] Tags { get; private set; }
        [JsonProperty(PropertyName = "pasties")] public PasteMystPasty[] Pasties { get; private set; }
        [JsonProperty(PropertyName = "edits")] public PasteMystEdit[] Edits { get; private set; }
        [JsonIgnore] public string Url => $"https://paste.myst.rs/{Id}";
        [JsonIgnore] public bool HasOwner => !string.IsNullOrEmpty(OwnerId);
        [JsonIgnore] public DateTime CreationTime => DateTimeOffset.FromUnixTimeSeconds(_createdAt).DateTime;
        [JsonIgnore] public PasteMystExpiration ExpireDuration => Enum.GetValues(typeof(PasteMystExpiration)).Cast<PasteMystExpiration>().FirstOrDefault(item => item.GetStringRepresentation() == _expiresIn);
        [JsonIgnore] public DateTime DeletionTime => DateTimeOffset.FromUnixTimeSeconds(_deletesAt).DateTime;

        public PasteMystEditForm CreateEditForm()
        {
            return new PasteMystEditForm(this);
        }

        public static async Task<PasteMystPaste> GetPasteAsync(string id, PasteMystAuth auth = null)
        {
            try
            {
                var request = WebRequest.Create(string.Format(GetPasteEndpoint, id));
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
                        var response = JsonConvert.DeserializeObject<PasteMystResponse>(content);
                        throw new Exception($"The server returned an exception: {response.Message}");
                    }
                    case JsonException jsonError:
                        throw new Exception($"An error occurred during serialization: {jsonError.Message}");
                    default:
                        throw;
                }
            }
        }

        public static async Task DeletePasteAsync(string id, PasteMystAuth auth)
        {
            try
            {
                var request = WebRequest.Create(string.Format(DeletePasteEndpoint, id));
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
                        var response = JsonConvert.DeserializeObject<PasteMystResponse>(content);
                        throw new Exception($"The server returned an exception: {response.Message}");
                    }
                    default:
                        throw;
                }
            }
        }

    }

}