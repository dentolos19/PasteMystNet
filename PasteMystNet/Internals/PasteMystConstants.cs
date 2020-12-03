using System.Net.Http;

namespace PasteMystNet.Internals
{

    internal static class PasteMystConstants
    {

        public static string PostPasteEndpoint { get; } = "https://paste.myst.rs/api/v2/paste";
        public static string GetPasteEndpoint { get; } = "https://paste.myst.rs/api/v2/paste/{0}";
        public static string PatchPasteEndpoint { get; } = "https://paste.myst.rs/api/v2/paste/{0}";
        public static string DeletePasteEndpoint { get; } = "https://paste.myst.rs/api/v2/paste/{0}";

        public static string IdentifyByNameEndpoint { get; } = "https://paste.myst.rs/api/v2/data/language?name={0}";
        public static string IdentifyByExtensionEndpoint { get; } = "https://paste.myst.rs/api/v2/data/languageExt?extension={0}";

        public static string UserExistsEndpoint { get; } = "https://paste.myst.rs/api/v2/user/{0}/exists";
        public static string GetUserEndpoint { get; } = "https://paste.myst.rs/api/v2/user/{0}";

        public static HttpClient HttpClient { get; } = new HttpClient();

    }

}