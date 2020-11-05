using System;
using System.Text.Json.Serialization;

namespace PasteMystNet
{

    public class PasteMystPaste
    {
        
        [JsonPropertyName("createdAt")] private ulong CreatedAt { get; }
        [JsonPropertyName("expiresIn")] private string ExpiresIn { get; }
        [JsonPropertyName("deletesAt")] private ulong DeletesAt { get; }

        [JsonPropertyName("_id")] public string Id { get; }
        [JsonPropertyName("ownerId")] public string OwnerId { get; }
        [JsonPropertyName("title")] public string Title { get; }
        [JsonIgnore] public DateTimeOffset CreationDate => DateTimeOffset.FromUnixTimeSeconds((long)CreatedAt);
        [JsonIgnore] public PasteMystExpiration Expiration => ParseExpiration(ExpiresIn);
        [JsonIgnore] public DateTimeOffset DeletionDate => DateTimeOffset.FromUnixTimeSeconds((long)DeletesAt);
        [JsonPropertyName("stars")] public ulong Stars { get; }
        [JsonPropertyName("isPrivate")] public bool IsPrivate { get; }
        [JsonPropertyName("isPublic")] public bool IsPublic { get; }
        [JsonPropertyName("tags")] public string[] Tags { get; }
        [JsonPropertyName("pasties")] public PasteMystPasty[] Pasties { get; }
        // [JsonPropertyName("edits")] public PasteMystEdit[] Edits { get; } // TODO

        private PasteMystExpiration ParseExpiration(string expiration)
        {
            return PasteMystExpiration.Unknown; // TODO
        }
        
    }

}