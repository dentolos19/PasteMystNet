namespace PasteMystNet.Core;

internal static class PasteMystConstants
{

    public static string WebsiteUrl => "https://paste.myst.rs";
    public static string BaseEndpoint => WebsiteUrl + "/api/v2";

    // paste endpoints

    public static string PostPasteEndpoint => BaseEndpoint + "/paste";
    public static string GetPasteEndpoint => BaseEndpoint + "/paste/{0}";
    public static string PatchPasteEndpoint => BaseEndpoint + "/paste/{0}";
    public static string DeletePasteEndpoint => BaseEndpoint + "/paste/{0}";

    // data endpoints

    public static string GetLanguageByNameEndpoint => BaseEndpoint + "/data/language?name={0}";
    public static string GetLanguageByExtensionEndpoint => BaseEndpoint + "/data/languageExt?extension={0}";

    // user endpoints

    public static string UserExistsEndpoint => BaseEndpoint + "/user/{0}/exists";
    public static string GetUserEndpoint => BaseEndpoint + "/user/{0}";
    public static string GetSelfEndpoint => BaseEndpoint + "/user/self";
    public static string GetSelfPastesEndpoint => BaseEndpoint + "/user/self/pastes";

}