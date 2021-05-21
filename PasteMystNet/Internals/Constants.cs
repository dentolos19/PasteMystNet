using System.Net.Http;

namespace PasteMystNet.Internals
{

    internal static class Constants
    {

        public static string BaseEndpoint { get; } = "https://paste.myst.rs";
        public static string ApiBaseEndpoint { get; } = BaseEndpoint + "/api/v2";

        public static string PostPasteEndpoint { get; } = ApiBaseEndpoint + "/paste";
        public static string GetPasteEndpoint { get; } = ApiBaseEndpoint + "/paste/{0}";
        public static string PatchPasteEndpoint { get; } = ApiBaseEndpoint + "/paste/{0}";
        public static string DeletePasteEndpoint { get; } = ApiBaseEndpoint + "/paste/{0}";

        public static string IdentifyByNameEndpoint { get; } = ApiBaseEndpoint + "/data/language?name={0}";
        public static string IdentifyByExtensionEndpoint { get; } = ApiBaseEndpoint + "/data/languageExt?extension={0}";
            
        public static string UserExistsEndpoint { get; } = ApiBaseEndpoint + "/user/{0}/exists";
        public static string GetUserEndpoint { get; } = ApiBaseEndpoint + "/user/{0}";
        public static string GetSelfEndpoint { get; } = ApiBaseEndpoint + "/user/self";
        public static string GetSelfPastesEndpoint { get; } = ApiBaseEndpoint + "/user/self/pastes";

        public static HttpClient HttpClient { get; } = new();

    }

}