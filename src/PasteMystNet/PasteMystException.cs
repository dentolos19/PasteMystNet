namespace PasteMystNet;

public class PasteMystException(
    string message,
    Exception inner,
    Uri requestUrl,
    string? requestContent
) : Exception(message, inner)
{
    public Uri RequestUrl { get; } = requestUrl;
    public string? RequestContent { get; } = requestContent;
}