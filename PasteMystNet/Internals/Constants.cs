using System.Net.Http;

namespace PasteMystNet.Internals
{

    internal static class Constants
    {

        public static string BaseEndpoint { get; } = "https://paste.myst.rs";

        public static string PostPasteEndpoint { get; } = BaseEndpoint + "/api/v2/paste";
        public static string GetPasteEndpoint { get; } = BaseEndpoint + "/api/v2/paste/{0}";
        public static string PatchPasteEndpoint { get; } = BaseEndpoint + "/api/v2/paste/{0}";
        public static string DeletePasteEndpoint { get; } = BaseEndpoint + "/api/v2/paste/{0}";

        public static string IdentifyByNameEndpoint { get; } = BaseEndpoint + "/api/v2/data/language?name={0}";
        public static string IdentifyByExtensionEndpoint { get; } = BaseEndpoint + "/api/v2/data/languageExt?extension={0}";

        public static string UserExistsEndpoint { get; } = BaseEndpoint + "/api/v2/user/{0}/exists";
        public static string GetUserEndpoint { get; } = BaseEndpoint + "/api/v2/user/{0}";

        public static HttpClient HttpClient { get; } = new();

    }

}