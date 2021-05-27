using Newtonsoft.Json;
using PasteMystNet.Internals;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace PasteMystNet
{

    /// <summary>This class is used to retrieve &amp; contain user information from server.</summary>
    /// <seealso href="https://paste.myst.rs/api-docs/user"/>
    public class PasteMystUser
    {

        [JsonProperty(PropertyName = "_id")] public string Id { get; private set; }
        [JsonProperty(PropertyName = "username")] public string Username { get; private set; }
        [JsonProperty(PropertyName = "avatarUrl")] public string AvatarUrl { get; private set; }
        [JsonProperty(PropertyName = "defaultLang")] public string DefaultLanguage { get; private set; }
        [JsonProperty(PropertyName = "publicProfile")] public bool HasPublicProfile { get; private set; }
        [JsonProperty(PropertyName = "supporterLength")] public int SupporterLength { get; private set; }
        [JsonProperty(PropertyName = "contributor")] public bool IsContributor { get; private set; }
        [JsonIgnore] public string ProfileUrl => Constants.ApiBaseEndpoint + "/users/" + Username;
        [JsonIgnore] public bool IsSupporter => SupporterLength != 0;

        [JsonProperty(PropertyName = "stars")] public string[]? Stars { get; private set; }
        [JsonProperty(PropertyName = "serviceIds")] public IDictionary<string, string>? ServiceIds { get; private set; }

        /// <summary>Checks whether if user exists on server.</summary>
        public static async Task<bool> UserExistsAsync(string name)
        {
            var response = await Constants.HttpClient.GetAsync(string.Format(Constants.UserExistsEndpoint, name));
            return response.StatusCode == HttpStatusCode.OK;
        }

        /// <summary>Retrieves user's profile information from server. Returns <c>null</c> when user doesn't exists or didn't enable public profile.</summary>
        public static async Task<PasteMystUser?> GetUserAsync(string name)
        {
            var response = await Constants.HttpClient.GetAsync(string.Format(Constants.GetUserEndpoint, name));
            if (response.StatusCode != HttpStatusCode.OK)
                return null;
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<PasteMystUser>(content);
        }

        /// <summary>Retrieves user's profile information with additional information via the authentication token.</summary>
        public static async Task<PasteMystUser?> GetSelfAsync(PasteMystAuth auth)
        {
            try
            {
                var request = WebRequest.Create(Constants.GetSelfEndpoint);
                request.Method = "GET";
                request.Headers.Add("Authorization", auth.Token);
                using var response = await request.GetResponseAsync();
                using var reader = new StreamReader(response.GetResponseStream()!);
                var content = await reader.ReadToEndAsync();
                return JsonConvert.DeserializeObject<PasteMystUser>(content);
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

        /// <summary>Retrieves user profile's private pastes via the authentication token.</summary>
        public static async Task<string[]?> GetSelfPastesAsync(PasteMystAuth auth)
        {
            try
            {
                var request = WebRequest.Create(Constants.GetSelfPastesEndpoint);
                request.Method = "GET";
                request.Headers.Add("Authorization", auth.Token);
                using var response = await request.GetResponseAsync();
                using var reader = new StreamReader(response.GetResponseStream()!);
                var content = await reader.ReadToEndAsync();
                return JsonConvert.DeserializeObject<string[]>(content);
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